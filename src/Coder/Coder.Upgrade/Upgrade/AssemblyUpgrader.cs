using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Agebull.EntityModel.Config;
using Newtonsoft.Json;

namespace Agebull.EntityModel.RobotCoder.Upgrade
{
    /// <summary>
    /// 程序集自升级
    /// </summary>
    public class AssemblyUpgrader
    {
        /// <summary>
        /// 读取的帮助XML
        /// </summary>
        public List<XmlMember> HelpXml;
        /// <summary>
        /// 打开的程序集名称
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 得到所有配置类
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, ClassUpgradeConfig> GetEnumConfig()
        {
            var cb = typeof(ConfigBase);
            if (cb.Assembly.Location != null)
            {
                string path = cb.Assembly.Location.Replace("dll", "xml");
                HelpXml = !File.Exists(path) ? new List<XmlMember>() : XmlMember.LoadHelpXml(path);
            }
            var types = cb.Assembly.GetTypes().Where(p => p.IsEnum).ToList();
            Dictionary<string, ClassUpgradeConfig> typeDictionary = new Dictionary<string, ClassUpgradeConfig>();
            foreach (var type in types)
            {
                typeDictionary.Add(type.Name, GetEnumConfig(type));
            }
            return typeDictionary;
        }
        /// <summary>
        /// 读取类配置
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ClassUpgradeConfig GetEnumConfig(Type type)
        {
            ClassUpgradeConfig config = new ClassUpgradeConfig
            {
                Name = type.Name,
                ConfigType = type,
                TypeName = type.GetTypeName(),
                BaseType = type.BaseType.GetTypeName(),
                IsDataAttribute = type.GetCustomAttribute(typeof(DataContractAttribute)) != null,
                IsJsonAttribute = type.GetCustomAttribute(typeof(JsonObjectAttribute)) != null
            };
            GetInfo(config, type);
            foreach (var name in type.GetEnumNames())
            {
                var field = new FieldUpgradeConfig
                {
                    Name = name,
                };
                var member = HelpXml.FirstOrDefault(p => p.Name == type.FullName + "." + name);
                if (member != null)
                {
                    field.Description = member.Summary;
                    field.Caption = member.DisplayName;
                    field.Description = member.Remark ?? member.Summary;
                }
                config.Fields.Add(name, field);
            }
            return config;
        }

        /// <summary>
        /// 得到所有配置类
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, ClassUpgradeConfig> GetConfig()
        {
            var cb = typeof(SimpleConfig);
            {
                string path = cb.Assembly.Location.Replace("dll", "xml");
                HelpXml = !File.Exists(path) ? new List<XmlMember>() : XmlMember.LoadHelpXml(path);
            }
            var types = cb.Assembly.GetTypes().Where(p => p.IsSubclassOf(cb)).ToList();
            types.Add(cb);
            Dictionary<string, ClassUpgradeConfig> typeDictionary = new Dictionary<string, ClassUpgradeConfig>();
            foreach (var type in types)
            {
                typeDictionary.Add(type.Name, GetConfig(type));
            }
            return typeDictionary;
        }

        /// <summary>
        /// 得到所有配置类
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, ClassUpgradeConfig> GetConfig(Assembly assembly)
        {
            {
                string path = assembly.Location.Replace("dll", "xml");
                HelpXml = !File.Exists(path) ? new List<XmlMember>() : XmlMember.LoadHelpXml(path);
            }
            var types = assembly.GetTypes().Where(p => p.IsSubclassOf(typeof(SimpleConfig))).ToList();
            Dictionary<string, ClassUpgradeConfig> typeDictionary = new Dictionary<string, ClassUpgradeConfig>();
            foreach (var type in types)
            {
                typeDictionary.Add(type.Name, GetConfig(type));
            }
            return typeDictionary;
        }
        private const BindingFlags flag = BindingFlags.DeclaredOnly | //指定只应考虑在所提供类型的层次结构级别上声明的成员。不考虑继承成员。
                                          BindingFlags.Instance | //指定实例成员将包括在搜索中。
                                          BindingFlags.Public | //指定公共成员将包括在搜索中。
                                          BindingFlags.NonPublic; //指定非公共成员将包括在搜索中。

        /// <summary>
        /// 读取类配置
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ClassUpgradeConfig GetConfig(Type type)
        {
            bool isArray = false;
            if (type.IsSupperInterface(typeof(System.Collections.IList)))
            {
                isArray = true;
                type = type.GetGenericArguments()[0];
            }
            ClassUpgradeConfig config = new ClassUpgradeConfig
            {
                Name = type.Name,
                ConfigType = type,
                IsArray = isArray,
                TypeName = type.GetTypeName(),
                BaseType = type.BaseType.GetTypeName(),
                IsDataAttribute = type.GetCustomAttribute(typeof(DataContractAttribute)) != null,
                IsJsonAttribute = type.GetCustomAttribute(typeof(JsonObjectAttribute)) != null
            };
            GetInfo(config, type);
            foreach (var field in type.GetFields(flag))
            {
                config.Fields.Add(field.Name.Trim('_'), GetConfig(type, field));
            }
            foreach (var field in type.GetProperties(flag))
            {
                config.Properties.Add(field.Name, GetConfig(type, field));
            }
            foreach (var property in config.Properties)
            {
                FieldUpgradeConfig field;
                if (!config.Fields.TryGetValue(property.Key, out field))
                    continue;
                field.PairProperty = property.Key;
                property.Value.PairField = field.Name;
                if (field.IsJsonAttribute)
                {
                    property.Value.JsonName = field.JsonName;
                }
            }
            return config;
        }

        private FieldUpgradeConfig GetConfig(Type type, FieldInfo field)
        {
            FieldUpgradeConfig config = new FieldUpgradeConfig
            {
                Name = field.Name,
                ConfigType = field.FieldType,
                TypeName = field.FieldType.GetTypeName(),
                IsDataAttribute = field.GetCustomAttribute(typeof(DataMemberAttribute)) != null,
            };
            var json = field.GetCustomAttribute<JsonPropertyAttribute>();
            if (json != null)
            {
                config.IsJsonAttribute = true;
                config.JsonName = !string.IsNullOrWhiteSpace(json.PropertyName) ? json.PropertyName : config.Name;
            }
            GetInfo(config, type, field);
            return config;
        }

        private PropertyUpgradeConfig GetConfig(Type type, PropertyInfo property)
        {
            PropertyUpgradeConfig config = new PropertyUpgradeConfig
            {
                Name = property.Name,
                ConfigType = property.PropertyType,
                TypeName = property.PropertyType.GetTypeName(),
                CanRead = property.CanRead,
                CanWrite = property.CanWrite,
                IsDataAttribute = property.GetCustomAttribute(typeof(DataMemberAttribute)) != null,

            };
            config.IsList = config.TypeName.Contains("Collection<");
            var json = property.GetCustomAttribute<JsonPropertyAttribute>();
            if (json != null)
            {
                config.IsJsonAttribute = true;
                config.JsonName = !string.IsNullOrWhiteSpace(json.PropertyName) ? json.PropertyName : config.Name;
            }
            GetInfo(config, type, property);
            return config;
        }

        private void GetInfo(UpgradeConfig config, Type type)
        {
            var member = HelpXml.FirstOrDefault(p => p.Name == type.FullName);
            if (member != null)
            {
                config.Description = member.Summary;
                config.Caption = member.DisplayName;
                config.Description = member.Remark ?? member.Summary;
            }
        }

        private void GetInfo(UpgradeConfig config, Type type, FieldInfo field)
        {
            var member = HelpXml.FirstOrDefault(p => p.Name == type.FullName + "." + field.Name);
            if (member != null)
            {
                config.Description = member.Summary;
                config.Caption = member.DisplayName;
                config.Description = member.Remark ?? member.Summary;
            }
            var descript = field.GetCustomAttribute<DescriptionAttribute>();
            if (descript != null)
                config.Description = descript.Description;
            var category = field.GetCustomAttribute<CategoryAttribute>();
            if (category != null)
                config.Category = category.Category;
            var displayName = field.GetCustomAttribute<DisplayNameAttribute>();
            if (displayName != null)
                config.Caption = displayName.DisplayName;
        }

        private void GetInfo(UpgradeConfig config, Type type, PropertyInfo property)
        {
            var member = HelpXml.FirstOrDefault(p => p.Name == type.FullName + "." + property.Name);
            if (member != null)
            {
                config.Description = member.Summary;
                config.Caption = member.DisplayName;
                config.Description = member.Remark ?? member.Summary;
            }
            var descript = property.GetCustomAttribute<DescriptionAttribute>();
            if (descript != null)
                config.Description = descript.Description;
            var category = property.GetCustomAttribute<CategoryAttribute>();
            if (category != null)
                config.Category = category.Category;
            var displayName = property.GetCustomAttribute<DisplayNameAttribute>();
            if (displayName != null)
                config.Caption = displayName.DisplayName;
        }
    }
}