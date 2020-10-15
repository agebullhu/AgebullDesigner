// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Gboxt.Common.SimpleDataAccess
// 建立:2014-12-03
// 修改:2014-12-03
// *****************************************************/

#region 引用

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Agebull.EntityModel.Config;
using Agebull.Common.Reflection;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Agebull.Common;
using System.Diagnostics;
using Agebull.EntityModel.Designer.AssemblyAnalyzer;

#endregion

namespace Agebull.EntityModel.Designer
{
    public sealed class AssemblyImporter
    {
        public bool IncludeBaseType { get; set; }
        #region 程序集

        private static readonly Dictionary<string, Assembly> loaded = new Dictionary<string, Assembly>();
        void LoadAssembly(string path, string name)
        {
            if (loaded.ContainsKey(name))
                return;
            var file = Path.Combine(path, name);
            try
            {
                var asm = Assembly.LoadFile(file);
                loaded.Add(name, asm);
                Trace.WriteLine(file, "Success");
                foreach (var friend in asm.GetReferencedAssemblies())
                {
                    var dll = friend.Name + ".dll";
                    if (FindSdk(dll, out file))
                    {
                        LoadAssembly(Path.GetDirectoryName(file), Path.GetFileName(file));
                    }
                    else
                    {
                        Trace.WriteLine(dll, "Error");
                    }
                }
            }
            catch
            {
                Trace.WriteLine(file, "Error");
            }
        }
        Assembly LoadReflection(string file)
        {
            try
            {
                return Assembly.LoadFile(file);
            }
            catch (ReflectionTypeLoadException ex)
            {
                foreach (var err in ex.LoaderExceptions)
                {
                    if (FindSdk(err.Message, out file))
                    {
                        LoadAssembly(Path.GetDirectoryName(file), Path.GetFileName(file));
                    }
                    else
                    {
                        Trace.WriteLine(err.Message, "Error");
                    }
                }
                return LoadReflection(file);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex, "Error");
                return null;
            }
        }
        bool FindSdk(string dll, out string file)
        {
            var sdk = @"C:\Program Files\dotnet\sdk\2.1.300";
            var nuget = @"C:\Users\Agebull\.nuget\packages";
            return Find(nuget, dll, out file) ||
                Find(sdk, dll, out file);
        }

        bool Find(string path, string name, out string fullName)
        {
            if (!Path.GetFileName(path).ToLower().Contains("net4"))
            {
                foreach (var file in Directory.GetFiles(path))
                {
                    if (file.IndexOf(name, StringComparison.Ordinal) > 0)
                    {
                        fullName = file;
                        return true;
                    }
                }
            }
            foreach (var folder in Directory.GetDirectories(path))
            {
                var fn = Path.GetFileName(folder)?.ToLower();
                if (fn != null && fn.Contains("linux"))
                    continue;
                if (Find(folder, name, out fullName))
                {
                    return true;
                }
            }
            fullName = null;
            return false;
        }

        private Assembly _assembly;
        private readonly List<Type> _types = new List<Type>();

        /// <summary>
        ///     读取实体结构
        /// </summary>
        private void ReadTypes()
        {
            try
            {
                var types = _assembly.GetTypes();
                _types.AddRange(types);
            }
            catch (ReflectionTypeLoadException ex)
            {
                _types.AddRange(ex.Types.Where(p => p != null));
                //foreach (FileNotFoundException err in ex.LoaderExceptions)
                //{
                //    string dll = err.FileName.Split(',')[0].Trim() + ".dll";
                //    if (FindSdk(dll, out var file))
                //    {
                //        LoadAssembly(Path.GetDirectoryName(file), Path.GetFileName(file));
                //    }
                //    else
                //    {
                //        System.Diagnostics.Trace.WriteLine(dll, "Error");
                //    }
                //}
            }
        }

        #endregion
        #region 外部调用
        public AssemblyImporter(ProjectConfig project, string file)
        {
            _project = project;
            if (project == null)
            {
                SolutionConfig.Current.Add(_project = new ProjectConfig
                {
                    Name = Path.GetFileNameWithoutExtension(file)
                });
            }
        }
        /// <summary>
        ///     分析出的表结构
        /// </summary>
        private readonly ProjectConfig _project;

        /// <summary>
        ///     打开并分析文件
        /// </summary>
        /// <param name="project"></param>
        /// <param name="file"></param>
        public static ProjectConfig Import(ProjectConfig project, string file)
        {
            var importer = new AssemblyImporter(project, file);
            try
            {
                importer._assembly = importer.LoadReflection(file);
                importer.ReadTypes();
                importer.ReadEntities();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return importer._project;
        }

        /// <summary>
        ///     打开并分析文件
        /// </summary>
        /// <param name="project"></param>
        /// <param name="assembly"></param>
        public static ProjectConfig Import(ProjectConfig project, Assembly assembly)
        {
            var importer = new AssemblyImporter(project, assembly.Location)
            {
                IncludeBaseType = false
            };
            try
            {
                importer._assembly = assembly;
                importer.ReadTypes();
                importer.ReadEntities();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return importer._project;
        }
        #endregion
        #region 内部分析

        /// <summary>
        ///     读取实体结构
        /// </summary>
        private void ReadEntities()
        {
            foreach (var type in _types)
            {
                if (type.IsSubclassOf(typeof(Enum)))
                {
                    ReadEnum(type);
                }
                else
                {
                    ReadEntity(type);
                }
            }
        }
        private void ReadEnum(Type type)
        {
            EnumConfig config = _project.Enums.FirstOrDefault(p => p.Name == type.Name) ?? new EnumConfig
            {
                Name = type.Name
            };
            _project.Add(config);
            GetInfo(config, type);
            foreach (var field in type.GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                var col = config.Items.FirstOrDefault(p=>p.Name == field.Name);
                if (col == null)
                {
                    config.Add(col= new EnumItem
                    {
                        Name = field.Name,
                        Value = Convert.ToInt64(field.GetValue(null)).ToString()
                    });
                }
                GetInfo(col, type, field);
            }
        }


        private void ReadEntity(Type type)
        {
            var enName = ReflectionHelper.GetTypeName(type);
            if (!char.IsLetter(enName[0]))
                return;
            if (type.GetAttribute<DataContractAttribute>() == null && type.GetAttribute<JsonObjectAttribute>() == null)
            {
                Trace.WriteLine(enName, "Skip");
                return;
            }
            
           var entity = _project.Entities.FirstOrDefault(p => p.Name == type.Name || p.Name == enName);
            if (entity != null)
            {
                entity.Name = enName;
            }
            else
            {
                entity = new EntityConfig
                {
                    Name = enName,
                    ReadTableName = enName
                };
                _project.Add(entity);
            }
            entity.NoDataBase = true;
            Trace.WriteLine(enName, "Entity");
            GetInfo(entity, type);
            ReadEntity(entity, type);
        }
        void ReadEntity(EntityConfig entity, Type type)
        {
            try
            {
                if (IncludeBaseType && type.BaseType != typeof(object))
                {
                    ReadEntity(entity, type.BaseType);
                }
            }
            catch
            {
                Trace.WriteLine(entity.Name, "Exception");
            }

            if (type.IsGenericType)
            {
                entity.ReferenceType = type.GetGenericParameter();
                entity.Name = entity.Name.Split(new char[] { '<' }, 2)[0];
            }
            entity.ModelBase = type.BaseType != typeof(object) ? type.BaseType.GetTypeName() : null;
            entity.IsInterface = type.IsInterface;
            entity.Interfaces = type.GetInterfaces().Select(ReflectionHelper.GetTypeName).LinkToString(",");
            PropertyInfo[] properties;
            FieldInfo[] fields;
            try
            {
                fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            }
            catch
            {
                Trace.WriteLine(entity.Name, "Exception");
                return;
            }
            var dc = type.GetAttribute<DataContractAttribute>();
            var jo = type.GetAttribute<JsonObjectAttribute>();
            foreach (var pro in properties)
            {
                if (pro.IsSpecialName || !char.IsLetter(pro.Name[0]))
                {
                    continue;
                }
                bool interFace = pro.Name.Contains('.');
                var name = interFace ? pro.Name.Split('(')[0].Split('.').Last() : pro.Name;
                var col = entity.Properties.FirstOrDefault(p => string.Equals(name, p.Name, StringComparison.OrdinalIgnoreCase));
                if (col == null)
                {
                    entity.Add(col = new FieldConfig
                    {
                        Name = name
                    });
                }
                else if (interFace)//重名接口
                    continue;
                col.Option.Index = entity.Properties.Count;
                GetInfo(col, type, pro);
                
                col.IsInterfaceField = interFace;
                CheckPropertyType(type, entity, col, pro, pro.PropertyType, jo != null, dc != null);

                col.CanGet = pro.GetGetMethod() != null;
                col.CanSet = pro.GetSetMethod() != null;
            }
            foreach (var field in fields)
            {
                if (field.IsSpecialName || field.Name[0]!='_' && !char.IsLetter(field.Name[0]))
                {
                    continue;
                }
                string name = field.Name.Trim('_');
                var col = entity.Properties.FirstOrDefault(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
                if (col == null)
                {
                    entity.Add(col = new FieldConfig
                    {
                        Name = field.Name
                    });
                    col.Option.Index = entity.Properties.Count;
                    GetInfo(col, type, field);
                }
                CheckPropertyType(type, entity, col, field, field.FieldType, jo != null, dc != null);
            }
        }
        private static void CheckPropertyType(Type type,EntityConfig entity, FieldConfig prperty, MemberInfo field, Type fieldType,bool json,bool dataMember)
        {
            try
            {
                prperty.Nullable = fieldType.IsSubclassOf(typeof(Nullable<>));
                Type type1 = prperty.Nullable ? fieldType.GetGenericArguments()[0] : fieldType;
                if (type1.IsArray)
                {
                    prperty.ReferenceType = ReflectionHelper.GetTypeName(type1);
                    prperty.IsArray = true;
                    type1 = type1.MakeArrayType();
                }
                else if (type1.IsSupperInterface(typeof(IDictionary<,>)))
                {
                    prperty.ReferenceType = ReflectionHelper.GetTypeName(type1);
                    var pars = type1.GetGenericArguments();
                    prperty.IsDictionary = true;
                    prperty.CsType = ReflectionHelper.GetTypeName(type1);
                    type1 = pars[1];
                }
                else if (type1.IsGenericType && type1.IsSupperInterface(typeof(IEnumerable<>)))
                {
                    prperty.ReferenceType = ReflectionHelper.GetTypeName(type1);
                    prperty.IsArray = true;
                    type1 = type1.GetGenericArguments()[0];
                }

                CsharpHelper.CheckType(prperty, type1.GetTypeName());
                if (type1.IsEnum)
                {
                    prperty.CustomType = ReflectionHelper.GetTypeName(type1);
                    prperty.CsType = "int";
                    prperty.IsEnum = true;
                }
                else if (!type1.IsBaseType())
                {
                    //prperty.CustomType = ReflectionHelper.GetTypeName(type1);
                    prperty.NoStorage = true;
                    prperty.LinkTable = prperty.CustomType;
                }
            }
            catch  (Exception ex)
            {
                prperty.CsType = "object";
                Trace.WriteLine(ex, $"Error : {type.FullName}.{field.Name}");
            }
            if (json)
            {
                var ji = field.GetAttribute<JsonIgnoreAttribute>();
                if (ji != null)
                {
                    prperty.NoneJson = true;
                }
                else
                {
                    var jp = field.GetAttribute<JsonPropertyAttribute>();
                    if (jp == null)
                        return;
                    prperty.NoneJson = false;
                    if (!string.IsNullOrWhiteSpace(jp.PropertyName))
                        prperty.JsonName = jp.PropertyName;
                }
            }
            else if (dataMember)
            {
                var id = field.GetAttribute<IgnoreDataMemberAttribute>();
                if (id != null)
                {
                    prperty.NoneJson = true;
                }
                var dm = field.GetAttribute<DataMemberAttribute>();
                if (dm == null) return;
                prperty.NoneJson = false;
                if (!string.IsNullOrWhiteSpace(dm.Name))
                    prperty.JsonName = dm.Name;
            }
        }

        #endregion
        #region XML文档


        private void GetInfo(ClassifyConfig config, Type type)
        {
            var member = XmlMember.Find(type);
            if (member != null)
            {
                config.Caption = member.DisplayName;
                config.Description = member.Summary;
                config.Remark = member.Remark;
            }
            var ca = type.GetAttribute<DisplayNameAttribute>();
            if (ca != null)
                config.Caption = ca.DisplayName;
            var ct = type.GetAttribute<CategoryAttribute>();
            if (ct != null)
            {
                config.Classify = ct.Category;
            }
            var de = type.GetAttribute<DescriptionAttribute>();
            if (de != null)
            {
                config.Description = de.Description;
            }
        }
        private void GetInfo(EnumItem config, Type type, MemberInfo field)
        {
            string name = $"{type.FullName}.{field.Name}";
            var member = XmlMember.Find(name);
            if (member != null)
            {
                config.Description = member.Summary;
                config.Caption = member.DisplayName;
                config.Remark = member.Remark;
            }
            var ca = field.GetAttribute<DisplayNameAttribute>();
            if (ca != null)
                config.Caption = ca.DisplayName;
            var de = field.GetAttribute<DescriptionAttribute>();
            if (de != null)
            {
                config.Description = de.Description;
            }
        }

        private void GetInfo(FieldConfig config, Type type, MemberInfo field)
        {
            string name = $"{type.FullName}.{field.Name}";
            var member = XmlMember.Find(name);
            if (member != null)
            {
                config.Description = member.Summary;
                config.Caption = member.DisplayName;
                config.Remark = member.Remark;
            }
            var ca = field.GetAttribute<DisplayNameAttribute>();
            if (ca != null)
                config.Caption = ca.DisplayName;
            var ct = field.GetAttribute<CategoryAttribute>();
            if (ct != null)
            {
                config.Group = ct.Category;
            }
            var de = field.GetAttribute<DescriptionAttribute>();
            if (de != null)
            {
                config.Description = de.Description;
            }
        }

        #endregion
    }
}