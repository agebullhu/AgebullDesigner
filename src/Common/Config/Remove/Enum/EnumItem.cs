using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Gboxt.Common.DataAccess.Schemas
{
    /// <summary>
    ///     枚举值节点
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public sealed partial class EnumItem : ConfigBase
    {
        /// <summary>
        /// 值
        /// </summary>
        [DataMember, JsonProperty("Value", NullValueHandling = NullValueHandling.Ignore)]
        internal string _value;

        /// <summary>
        /// 值
        /// </summary>
        /// <remark>
        /// 值
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("*设计"), DisplayName("值"), Description("值")]
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (_value == value)
                    return;
                BeforePropertyChanged(nameof(Value), _value, value);
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }
    }
}