using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config.V2021
{
    /// <summary>
    /// 页面区域
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class GridItemConfig : PageItemConfig
    {

        [DataMember, JsonProperty("OrderField", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _orderField;

        [DataMember, JsonProperty("OrderDesc", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _orderDesc;

        /// <summary>
        /// 默认排序字段
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"默认排序字段"), Description("默认排序字段")]
        public string OrderField
        {
            get => _orderField ?? Entity.PrimaryField;
            set
            {
                if (_orderField == value)
                    return;
                BeforePropertyChanged(nameof(OrderField), _orderField, value);
                _orderField = value;
                OnPropertyChanged(nameof(OrderField));
            }
        }

        /// <summary>
        /// 默认反序
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"默认反序"), Description("默认反序")]
        public bool OrderDesc
        {
            get => _orderDesc;
            set
            {
                if (_orderDesc == value)
                    return;
                BeforePropertyChanged(nameof(OrderDesc), _orderDesc, value);
                _orderDesc = value;
                OnPropertyChanged(nameof(OrderDesc));
            }
        }

    }
}