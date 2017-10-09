using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Agebull.Common;
using Agebull.Common.DataModel;
using Agebull.EntityModel.Config;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Designer.AssemblyAnalyzer
{
    /// <summary>
    /// 程序集分析
    /// </summary>
    internal class AssemblyUpgrader
    {
        #region 基本

        /// <summary>
        /// 读取的帮助XML
        /// </summary>
        private List<XmlMember> _helpXml;
        /// <summary>
        /// 打开的程序集名称
        /// </summary>
        public string FileName { get; set; }

        private Assembly _assembly;

        public AssemblyConfig Config { get; private set; }

        private const BindingFlags flag = BindingFlags.DeclaredOnly | //指定只应考虑在所提供类型的层次结构级别上声明的成员。不考虑继承成员。
                                          BindingFlags.Static | BindingFlags.Instance |
                                          BindingFlags.Public | BindingFlags.NonPublic; //指定非公共成员将包括在搜索中。

        #endregion

        #region 流程

        /// <summary>
        /// 准备
        /// </summary>
        /// <returns></returns>
        public bool Prepare()
        {
            var dir = Path.GetDirectoryName(FileName);
            var root = Path.GetDirectoryName(GetType().Assembly.Location);
            IOHelper.CopyPath(dir, root);
            var file = Path.Combine(root, Path.GetFileName(FileName));
            try
            {
                _assembly = Assembly.LoadFile(file);
            }
            catch (Exception e)
            {
                return false;
            }
            if (_assembly == null)
                return false;
            Config = new AssemblyConfig
            {
                Name = _assembly.FullName,
                FileName = FileName
            };
            string path = FileName.Replace("dll", "xml");
            _helpXml = !File.Exists(path) ? new List<XmlMember>() : XmlMember.Load(path);
            return true;
        }

        /// <summary>
        /// 分析
        /// </summary>
        /// <returns></returns>
        public void Analyze()
        {
            var types = _assembly.GetTypes().Where(p => p.Name[0] != '<').ToList();

            foreach (var type in types)
            {
                Config.Types.Add(Analyze(type));
            }
        }
        /// <summary>
        /// 结束分析
        /// </summary>
        public void End()
        {
            foreach (var config in Config.Types)
            {
                foreach (var property in config.Properties)
                {
                    FieldConfig field;
                    if (!config.Fields.TryGetValue(property.Key, out field))
                    {
                        if (!config.Fields.TryGetValue("_" + property.Key, out field))
                            continue;
                    }
                    field.PairProperty = property.Key;
                    property.Value.PairField = field.Name;
                    if (field.IsJsonAttribute)
                    {
                        property.Value.JsonName = field.JsonName;
                    }
                }
                if (config.Fields.Count == 0)
                    config.Fields = null;
                if (config.Properties.Count == 0)
                    config.Properties = null;
                if (config.Interfaces.Count == 0)
                    config.Interfaces = null;
                if (config.Methods.Count == 0)
                    config.Methods = null;
            }


        }
        #endregion


        /// <summary>
        /// 读取类配置
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private TypeConfig Analyze(Type type)
        {
            TypeConfig config = new TypeConfig
            {
                Name = type.Name,
                ConfigType = type,
                TypeName = type.GetTypeName()
            };
            GetInfo(config, type);
            if (type.IsEnum)
            {
                AnalyzeEnum(type, config);
                return config;
            }
            CheckInterface(type, config);
            if (type.IsInterface)
            {
                config.NetType = NetType.Interface;
            }
            else
            {
                GetConstructors(type, config);
                config.NetType = type.IsClass ? NetType.Class : NetType.Struct;
            }

            if (type.IsGenericType)
            {
                config.Generics = new List<string>();
                foreach (var gen in type.GetGenericArguments())
                {
                    config.Generics.Add(gen.GetTypeName());
                }
            }
            if (type.IsSupperInterface(typeof(System.Collections.IEnumerable)))
            {
                config.IsArray = true;
            }

            foreach (var field in type.GetFields(flag))
            {
                config.Fields.Add(field.Name, GetConfig(type, field));
            }
            foreach (var property in type.GetProperties(flag))
            {
                config.Properties.Add(property.Name, GetConfig(type, property));
            }
            foreach (var property in type.GetMethods(flag))
            {
                config.Methods.Add(GetConfig(type, property));
            }
            return config;
        }

        /// <summary>
        /// 读取类配置
        /// </summary>
        /// <param name="type"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        private void AnalyzeEnum(Type type, TypeConfig config)
        {
            config.NetType = NetType.Enum;
            foreach (var field in type.GetFields())
            {
                config.Fields.Add(field.Name, GetConfig(type, field));
            }
        }


        private FieldConfig GetConfig(Type type, FieldInfo field)
        {
            FieldConfig config = new FieldConfig
            {
                Name = field.Name,
                ConfigType = field.FieldType,
                TypeName = field.FieldType.GetTypeName()
            };
            GetInfo(config, type, field);
            return config;
        }

        private PropertyConfig GetConfig(Type type, PropertyInfo property)
        {
            PropertyConfig config = new PropertyConfig
            {
                Name = property.Name,
                ConfigType = property.PropertyType,
                TypeName = property.PropertyType.GetTypeName(),
                CanRead = property.CanRead,
                CanWrite = property.CanWrite
            };
            config.IsList = config.TypeName.Contains("Collection<");
            GetInfo(config, type, property);
            return config;
        }

        #region 方法分析

        private void CheckInterface(Type type, TypeConfig config)
        {
            foreach (var it in type.GetInterfaces())
            {
                config.Interfaces.Add(it.GetTypeName());
            }
        }

        private void GetConstructors(Type type, TypeConfig config)
        {
            var ctors = type.GetConstructors();
            int cnt = 1;
            foreach (var method in ctors)
            {
                var info = new MethodConfig
                {
                    Name = $"ctor.{cnt++}"
                };
                foreach (var para in method.GetParameters())
                {
                    CheckParameter(para, info);
                }
                if (info.Argument.Count == 0)
                    info.Argument = null;
                config.Methods.Add(info);
            }
        }
        private MethodConfig GetConfig(Type type, MethodInfo method)
        {
            var info = new MethodConfig
            {
                Name = method.Name
            };
            GetInfo(info, type, method);
            foreach (var para in method.GetParameters())
            {
                CheckParameter(para, info);
            }
            if (method.ReturnType != typeof(void))
            {
                info.Result = new FieldConfig
                {
                    TypeName = method.ReturnType.GetTypeName()
                };
            }
            return info;
        }

        private static void CheckParameter(ParameterInfo para, MethodConfig info)
        {
            var paraInfo = new FieldConfig
            {
                Name = para.Name,
                TypeName = para.ParameterType.GetTypeName()
            };
            if (para.HasDefaultValue)
                paraInfo.DefaultValue = para.DefaultValue == null ? "null" : para.DefaultValue.ToString();
            GetExtendValue(para, paraInfo);
            CheckAttributes(paraInfo, para.GetCustomAttributesData());
            info.Argument.Add(paraInfo.Name, paraInfo);
        }

        #endregion
        #region 读取注释及说明性Attribute

        private static void GetExtendValue(object obj, UpgradeConfig paraInfo)
        {
            foreach (var pro in obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (!pro.CanRead || pro.Name == "DeclaringMethod" || pro.Name == "GenericParameterAttributes" || pro.Name == "Name" ||
                    pro.Name == "GenericParameterPosition" || pro.Name == "FieldHandle" || pro.Name == "MetadataToken" ||
                    pro.Name == "MemberType" || pro.Name == "TypeHandle" || pro.Name == "AssemblyQualifiedName" || pro.Name == "GUID")
                    continue;

                try
                {
                    var value = pro.GetValue(obj);
                    if (value == null)
                        continue;
                    if (value is string)
                    {
                        paraInfo.Values.Add(pro.Name, (string)value);
                    }
                    else if (value is bool)
                    {
                        if ((bool)value)
                            paraInfo.Values.Add(pro.Name, "true");
                    }
                    else if (value.GetType().IsValueType)
                        paraInfo.Values.Add(pro.Name, value.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            if (paraInfo.Values.Count == 0)
                paraInfo.Values = null;
        }


        private void GetInfo(TypeConfig config, Type type)
        {
            if (type.BaseType != typeof(object))
                config.BaseType = type.BaseType.GetTypeName();
            var member = _helpXml.FirstOrDefault(p => p.Name == type.FullName);
            if (member != null)
            {
                config.Description = member.Summary;
                config.Caption = member.DisplayName;
                config.Description = member.Remark ?? member.Summary;
            }
            GetExtendValue(type, config);
            CheckAttributes(config, type);
        }

        private void GetInfo(UpgradeConfig config, Type type, MemberInfo field)
        {
            var member = _helpXml.FirstOrDefault(p => p.Name == type.FullName + "." + field.Name);
            if (member != null)
            {
                config.Description = member.Summary;
                config.Caption = member.DisplayName;
                config.Description = member.Remark ?? member.Summary;
            }
            GetExtendValue(field, config);
            CheckAttributes(config, field);
        }
        #endregion

        #region 属性分析


        //https://msdn.microsoft.com/zh-cn/library/system.reflection.customattributedata.namedarguments(v=vs.110).aspx
        private void CheckAttributes(UpgradeConfig config, MemberInfo member)
        {
            var descript = member.GetCustomAttribute<DescriptionAttribute>();
            if (descript != null)
                config.Description = descript.Description;
            var category = member.GetCustomAttribute<CategoryAttribute>();
            if (category != null)
                config.Category = category.Category;
            var displayName = member.GetCustomAttribute<DisplayNameAttribute>();
            if (displayName != null)
                config.Caption = displayName.DisplayName;

            var json = member.GetCustomAttribute<JsonPropertyAttribute>();
            if (json != null)
            {
                config.IsJsonAttribute = true;
                config.JsonName = !string.IsNullOrWhiteSpace(json.PropertyName) ? json.PropertyName : config.Name;
            }
            config.IsDataAttribute = member.GetCustomAttribute(typeof(DataMemberAttribute)) != null;
            CheckAttributes(config, member.GetCustomAttributesData());
        }

        private static void CheckAttributes(UpgradeConfig config, IList<CustomAttributeData> attributes)
        {
            foreach (CustomAttributeData cad in attributes)
            {
                var info = new AttributeInfo
                {
                    Name = cad.AttributeType.GetTypeName(),
                };
                config.CustomAttributes.Add(cad.AttributeType.FullName, info);
                foreach (CustomAttributeTypedArgument cata in cad.ConstructorArguments)
                {
                    info.Constructors.AddRange(GetValues(cata));
                }
                if (cad.NamedArguments != null)
                    foreach (CustomAttributeNamedArgument cana in cad.NamedArguments)
                    {
                        info.Values.Add(cana.MemberInfo.Name, GetValues(cana.TypedValue));
                    }
            }
            if (config.CustomAttributes.Count == 0)
                config.CustomAttributes = null;
        }

        private static List<NameValue> GetValues(CustomAttributeTypedArgument cata)
        {
            var values = new List<NameValue>();
            if (cata.Value.GetType() == typeof(ReadOnlyCollection<CustomAttributeTypedArgument>))
            {
                foreach (CustomAttributeTypedArgument cataElement in
                    (ReadOnlyCollection<CustomAttributeTypedArgument>)cata.Value)
                {
                    values.Add(new NameValue(cataElement.ArgumentType.GetTypeName(), cataElement.Value));
                }
            }
            else
            {
                values.Add(new NameValue(cata.ArgumentType.GetTypeName(), cata.Value));
            }
            return values;
        }

        #endregion
    }
}