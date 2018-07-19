using System;
using System.Collections.Generic;
using Agebull.EntityModel.Config;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Designer.AssemblyAnalyzer
{
    /// <summary>
    /// 类配置
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class TypeConfig : UpgradeConfig
    {
        /// <summary>
        /// 类型
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public NetType NetType { get; set; }

        /// <summary>
        /// 泛型参数
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<string> Generics { get; set; }

        /// <summary>
        /// 方法
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<MethodConfig> Methods { get; set; } = new List<MethodConfig>();

        /// <summary>
        /// 字段
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Dictionary<string, FieldConfig> Fields { get; set; } = new Dictionary<string, FieldConfig>(StringComparer.OrdinalIgnoreCase);
        /// <summary>
        /// 属性
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Dictionary<string, PropertyUpgradeConfig> Properties { get; set; } = new Dictionary<string, PropertyUpgradeConfig>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 基类
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string BaseType { get; set; }

        /// <summary>
        /// 基类
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<string> Interfaces { get; set; } = new List<string>();

    }
    /// <summary>
    /// .NET的类型
    /// </summary>
    public enum NetType
    {
        /// <summary>
        /// 类
        /// </summary>
        Class,
        /// <summary>
        /// 结构
        /// </summary>
        Struct,
        /// <summary>
        /// 枚举
        /// </summary>
        Enum,
        /// <summary>
        /// 接口
        /// </summary>
        Interface,
    }
}