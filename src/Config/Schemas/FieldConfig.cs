using Newtonsoft.Json;

namespace Agebull.EntityModel.Designer.AssemblyAnalyzer
{
    /// <summary>
    /// �ֶ�����
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class FieldConfig : UpgradeConfig
    {
        /// <summary>
        /// ��Ӧ����������
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string PairProperty { get; set; }
    }
}