using Newtonsoft.Json;

namespace Agebull.EntityModel.Designer.AssemblyAnalyzer
{
    /// <summary>
    /// 属性配置
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class PropertyConfig : UpgradeConfig
    {
        /// <summary>
        /// JSON序列化名称
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string JsonName { get; set; }
        /// <summary>
        /// 对应的字段
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string PairField { get; set; }

        /// <summary>
        /// 是否列表
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool IsList { get; set; }

        /// <summary>
        /// 代码
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Code { get; set; }

        /// <summary>
        /// 能否读
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool CanRead { get; set; }
        /// <summary>
        /// 能否写
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool CanWrite { get; set; }

        public FieldConfig Field { get; set; }
    }
}