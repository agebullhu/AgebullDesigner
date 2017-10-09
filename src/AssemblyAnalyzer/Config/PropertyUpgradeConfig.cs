using Newtonsoft.Json;

namespace Agebull.EntityModel.Designer.AssemblyAnalyzer
{
    /// <summary>
    /// ��������
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class PropertyConfig : UpgradeConfig
    {
        /// <summary>
        /// JSON���л�����
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string JsonName { get; set; }
        /// <summary>
        /// ��Ӧ���ֶ�
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string PairField { get; set; }

        /// <summary>
        /// �Ƿ��б�
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool IsList { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Code { get; set; }

        /// <summary>
        /// �ܷ��
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool CanRead { get; set; }
        /// <summary>
        /// �ܷ�д
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool CanWrite { get; set; }

        public FieldConfig Field { get; set; }
    }
}