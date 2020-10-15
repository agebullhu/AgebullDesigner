using System.Collections.Generic;
using Agebull.Common.DataModel;
using Agebull.EntityModel.Config;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Designer.AssemblyAnalyzer
{
    /// <summary>
    /// �ֶ�����
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class AttributeInfo : SimpleConfig
    {
        /// <summary>
        /// ��Ӧ����������
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Dictionary<string, List<NameValue>> Values { get; set; } = new Dictionary<string, List<NameValue>>();

        /// <summary>
        /// ��Ӧ����������
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<NameValue> Constructors { get; set; } = new List<NameValue>();
        
    }
}