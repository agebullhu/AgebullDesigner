using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace Gboxt.Common.DataAccess.Schemas
{
    /// <summary>
    /// TypedefItem
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class TypedefItem : ParentConfigBase
    {
        #region 子级


        /// <summary>
        /// 子级
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public override IEnumerable<ConfigBase> MyChilds => Items.Values;

        /// <summary>
        /// 子级
        /// </summary>
        [DataMember, JsonProperty("Items", NullValueHandling = NullValueHandling.Ignore), Category("no supper")]
        private Dictionary<string, EnumItem> _items;

        /// <summary>
        /// 子级
        /// </summary>
        [IgnoreDataMember, JsonIgnore,Browsable(false)]
        public Dictionary<string, EnumItem> Items
        {
            get
            {
                if (_items != null)
                    return _items;
                _items = new Dictionary<string, EnumItem>();
                BeforePropertyChanged(nameof(Items), null, _items);
                return _items;
            }
            set
            {
                if (_items == value)
                    return;
                BeforePropertyChanged(nameof(Items), _items, value);
                _items = value;
                RaisePropertyChanged(nameof(Items));
            }
        }

        #endregion

        #region 属性

        /// <summary>
        /// 类型名称对应的语言关键字
        /// </summary>
        [DataMember, JsonProperty("KeyWork", NullValueHandling = NullValueHandling.Ignore)]
        internal string _keyWork;

        /// <summary>
        /// 类型名称对应的语言关键字
        /// </summary>
        /// <remark>
        /// 类型名称对应的语言关键字
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("类型名称对应的语言关键字"), Description("类型名称对应的语言关键字")]
        public string KeyWork
        {
            get
            {
                return _keyWork;
            }
            set
            {
                if (_keyWork == value)
                    return;
                BeforePropertyChanged(nameof(KeyWork), _keyWork, value);
                _keyWork = value;
                OnPropertyChanged(nameof(KeyWork));
            }
        }

        /// <summary>
        /// 数组名称
        /// </summary>
        [DataMember, JsonProperty("ArrayLen", NullValueHandling = NullValueHandling.Ignore)]
        internal string _arrayLen;


        /// <summary>
        /// 数组名称
        /// </summary>
        /// <remark>
        /// 数组名称
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("数组名称"), Description("数组名称")]
        public string ArrayLen
        {
            get
            {
                return _arrayLen;
            }
            set
            {
                if (_arrayLen == value)
                    return;
                BeforePropertyChanged(nameof(ArrayLen), _arrayLen, value);
                _arrayLen = value;
                OnPropertyChanged(nameof(ArrayLen));
            }
        }

        #endregion

    }
}