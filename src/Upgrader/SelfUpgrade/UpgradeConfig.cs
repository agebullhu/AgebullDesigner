using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using Agebull.EntityModel.Config;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Designer.AssemblyAnalyzer
{
    /// <summary>
    /// 升级配置
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class UpgradeConfig : SimpleConfig
    {
        /// <summary>
        /// 分类
        /// </summary>
        /// <remark>
        /// 分类(仅引用可行)
        /// </remark>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Category("*设计"), DisplayName("分类"), Description("分类(仅引用可行)")]
        public string Classify
        {
            get;
            set;
        }
        /// <summary>
        /// 关联的属性
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Dictionary<string, AttributeInfo> CustomAttributes { get; set; } = new Dictionary<string, AttributeInfo>();


        /// <summary>
        /// 属性值
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Dictionary<string, string> Values { get; set; } = new Dictionary<string, string>();
        
        /// <summary>
        /// 是否有DataMember或是DataContract属性
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool IsDataAttribute { get; set; }
        /// <summary>
        /// JSON序列化名称
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string JsonName { get; set; }
        /// <summary>
        /// 是否有JSON属性
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool IsJsonAttribute { get; set; }
        /// <summary>
        /// 分组
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Category { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string TypeName { get; set; }
        /// <summary>
        /// 是否数组
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool IsArray { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public Type ConfigType { get; set; }


        /// <summary>
        /// 缺省值
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string DefaultValue { get; set; }
    }
}