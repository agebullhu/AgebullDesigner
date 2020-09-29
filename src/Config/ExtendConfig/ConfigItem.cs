using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 扩展配置节点
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public class ConfigItem : NotificationObject
    {
        [DataMember, JsonProperty("name",  DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        private string _name;

        [DataMember, JsonProperty("value",  DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        private string _value;

        [DataMember, JsonProperty("type",  DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        private string _type = "string";

        /// <summary>
        /// 名称
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public string Name
        {
            get => _name;
            set
            {
                if (_name == value)
                    return;
                _name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        /// <summary>
        /// 数据类型
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public string Type
        {
            get => _type;
            set
            {
                if (_type == value)
                    return;
                _type = value;
                RaisePropertyChanged(nameof(Type));
            }
        }

        /// <summary>
        /// 值
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public string Value
        {
            get => _value;
            set
            {
                if (_value == value)
                    return;
                _value = value;
                RaisePropertyChanged(nameof(Value));
            }
        }

        /// <summary>返回表示当前对象的字符串。</summary>
        /// <returns>表示当前对象的字符串。</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"{Name}:{Value}({Type})";
        }
    }
}