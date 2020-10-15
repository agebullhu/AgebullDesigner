using System.Collections.Generic;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Designer.AssemblyAnalyzer
{
    /// <summary>
    /// 字段配置
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class MethodConfig : UpgradeConfig
    {
        /// <summary>
        /// 返回对象
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public FieldConfig Result { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Dictionary<string, FieldConfig> Argument { get; set; } = new Dictionary<string, FieldConfig>();
    }
}