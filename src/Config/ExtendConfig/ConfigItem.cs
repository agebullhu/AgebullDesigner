using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Gboxt.Common.DataAccess.Schemas
{
    /// <summary>
    /// 扩展配置节点
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public class ConfigItem
    {
        /// <summary>
        /// 名称
        /// </summary>
        [DataMember, JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [DataMember, JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }

        /// <summary>返回表示当前对象的字符串。</summary>
        /// <returns>表示当前对象的字符串。</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"{Name}:{Value}";
        }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string Type { get; set; }
    }
}