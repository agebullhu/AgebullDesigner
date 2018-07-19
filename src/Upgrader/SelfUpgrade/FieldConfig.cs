using Newtonsoft.Json;

namespace Agebull.EntityModel.Designer.AssemblyAnalyzer
{
    /// <summary>
    /// 字段配置
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class FieldConfig : UpgradeConfig
    {
        /// <summary>
        /// 对应的属性名称
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string PairProperty { get; set; }
    }
}