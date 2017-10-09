using System.Collections.Generic;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Designer.AssemblyAnalyzer
{
    /// <summary>
    /// �ֶ�����
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class MethodConfig : UpgradeConfig
    {
        /// <summary>
        /// ���ض���
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public FieldConfig Result { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Dictionary<string, FieldConfig> Argument { get; set; } = new Dictionary<string, FieldConfig>();
    }
}