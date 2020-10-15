// // /*****************************************************
// // (c)2016-2016 Copy right Agebull.hu
// // 作者:
// // 工程:CodeRefactor
// // 建立:2016-06-06
// // 修改:2016-06-22
// // *****************************************************/

#region 引用

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;

#endregion

namespace Gboxt.Common.DataAccess.Schemas
{
    /// <summary>
    ///     枚举值配置
    /// </summary>
    public sealed partial class EnumConfig
    {
        #region *设计

        /// <summary>
        /// 是否位域
        /// </summary>
        [DataMember, JsonProperty("IsFlagEnum", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isFlagEnum;

        /// <summary>
        /// 是否位域
        /// </summary>
        /// <remark>
        /// 是否位域
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("*设计"), DisplayName("是否位域"), Description("是否位域")]
        public bool IsFlagEnum
        {
            get
            {
                return _isFlagEnum;
            }
            set
            {
                if (_isFlagEnum == value)
                    return;
                BeforePropertyChanged(nameof(IsFlagEnum), _isFlagEnum, value);
                _isFlagEnum = value;
                OnPropertyChanged(nameof(IsFlagEnum));
            }
        }

        /// <summary>
        /// 连接对应的字段
        /// </summary>
        [DataMember, JsonProperty("LinkField", NullValueHandling = NullValueHandling.Ignore)]
        internal Guid _linkField;

        /// <summary>
        /// 连接对应的字段
        /// </summary>
        /// <remark>
        /// 是否位域
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("*设计"), DisplayName("连接对应的字段"), Description("是否位域")]
        public Guid LinkField
        {
            get
            {
                return _linkField;
            }
            set
            {
                if (_linkField == value)
                    return;
                BeforePropertyChanged(nameof(LinkField), _linkField, value);
                _linkField = value;
                OnPropertyChanged(nameof(LinkField));
            }
        }
        #endregion *设计 
        #region 

        /// <summary>
        /// 子级
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public override IEnumerable<ConfigBase> MyChilds => _items;

        /// <summary>
        /// 子级
        /// </summary>
        [DataMember, JsonProperty("_items", NullValueHandling = NullValueHandling.Ignore)]
        internal ObservableCollection<EnumItem> _items;

        /// <summary>
        /// 子级节点
        /// </summary>
        /// <remark>
        /// 子级节点
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("系统"), DisplayName("子级节点"), Description("子级节点")]
        public ObservableCollection<EnumItem> Items
        {
            get
            {
                if (_items != null)
                    return _items;
                _items = new ObservableCollection<EnumItem>();
                BeforePropertyChanged(nameof(Items), null, _items);
                return _items;
            }
            set
            {
                if (_items == value)
                    return;
                BeforePropertyChanged(nameof(Items), _items, value);
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }
        #endregion
        
    }
}