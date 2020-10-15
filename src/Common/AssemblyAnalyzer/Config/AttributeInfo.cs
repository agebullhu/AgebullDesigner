using System.Collections.Generic;
using Agebull.Common.DataModel;
using Agebull.EntityModel.Config;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Designer.AssemblyAnalyzer
{
    /// <summary>
    /// 字段配置
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class AttributeInfo : SimpleConfig
    {
        /// <summary>
        /// 对应的属性名称
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Dictionary<string, List<NameValue>> Values { get; set; } = new Dictionary<string, List<NameValue>>();

        /// <summary>
        /// 对应的属性名称
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<NameValue> Constructors { get; set; } = new List<NameValue>();
        
    }
}