using System;
using System.Collections.Generic;
using Agebull.EntityModel.Config;
using Newtonsoft.Json;

namespace Agebull.EntityModel.RobotCoder.Upgrade
{
    /// <summary>
    /// 升级配置
    /// </summary>
    public class UpgradeRoot : ConfigBase
    {
        /// <summary>
        /// 升级的配置对象
        /// </summary>
        public ConfigCollection<ClassUpgradeConfig> ConfigTypes { get; } = new ConfigCollection<ClassUpgradeConfig>();
    }

    /// <summary>
    /// 升级配置
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class UpgradeConfig : FileConfigBase
    {
        /// <summary>
        /// 是否有DataMember或是DataContract属性
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool IsDataAttribute { get; set; }
        /// <summary>
        /// 是否有JSON属性
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool IsJsonAttribute { get; set; }
        /// <summary>
        /// 分组
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Category { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string TypeName { get; set; }
        /// <summary>
        /// 是否数组
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool IsArray { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public Type ConfigType { get; set; }
    }
    /// <summary>
    /// 类配置
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class ClassUpgradeConfig : UpgradeConfig
    {
        /// <summary>
        /// 方法
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, MethodsUpgradeConfig> Methods = new Dictionary<string, MethodsUpgradeConfig>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 字段
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, FieldUpgradeConfig> Fields = new Dictionary<string, FieldUpgradeConfig>(StringComparer.OrdinalIgnoreCase);
        /// <summary>
        /// 属性
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, PropertyUpgradeConfig> Properties = new Dictionary<string, PropertyUpgradeConfig>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 基类
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BaseType { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public ClassifyGroupConfig<PropertyUpgradeConfig> ClassifyGroup { get; set; }
    }
    /// <summary>
    /// 字段配置
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class MethodsUpgradeConfig : UpgradeConfig
    {
        /// <summary>
        /// 返回对象
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ClassUpgradeConfig Result { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, ClassUpgradeConfig> Argument { get; set; }
    }

    /// <summary>
    /// 字段配置
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class FieldUpgradeConfig : UpgradeConfig
    {
        /// <summary>
        /// JSON序列化名称
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string JsonName { get; set; }
        /// <summary>
        /// 对应的属性名称
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string PairProperty { get; set; }
    }
    /// <summary>
    /// 属性配置
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class PropertyUpgradeConfig : UpgradeConfig
    {
        /// <summary>
        /// JSON序列化名称
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string JsonName { get; set; }
        /// <summary>
        /// 对应的字段
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string PairField { get; set; }

        /// <summary>
        /// 是否列表
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool IsList { get; set; }

        /// <summary>
        /// 代码
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; }

        /// <summary>
        /// 能否读
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool CanRead { get; set; }
        /// <summary>
        /// 能否写
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool CanWrite { get; set; }

        public FieldUpgradeConfig Field { get; set; }
    }
}