/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2017/7/12 22:06:39*/
/*****************************************************
©2008-2017 Copy right by agebull.hu(胡天水)
作者:agebull.hu(胡天水)
工程:Agebull.Common.Config
建立:2014-12-03
修改:2017-07-12
*****************************************************/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 枚举配置
    /// </summary>
    [DataContract,JsonObject(MemberSerialization.OptIn)]
    public partial class EnumConfig : ParentConfigBase
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public EnumConfig()
        {
        }

        #endregion

 
        #region 数据模型

        /// <summary>
        /// 是否位域
        /// </summary>
        [DataMember,JsonProperty("IsFlagEnum", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isFlagEnum;

        /// <summary>
        /// 是否位域
        /// </summary>
        /// <remark>
        /// 是否位域
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"数据模型"),DisplayName(@"是否位域"),Description("是否位域")]
        public bool IsFlagEnum
        {
            get
            {
                return _isFlagEnum;
            }
            set
            {
                if(_isFlagEnum == value)
                    return;
                BeforePropertyChanged(nameof(IsFlagEnum), _isFlagEnum,value);
                _isFlagEnum = value;
                OnPropertyChanged(nameof(IsFlagEnum));
            }
        } 
        #endregion 
        #region 设计器支持

        /// <summary>
        /// 连接对应的字段
        /// </summary>
        [DataMember,JsonProperty("LinkField", NullValueHandling = NullValueHandling.Ignore)]
        internal Guid _linkField;

        /// <summary>
        /// 连接对应的字段
        /// </summary>
        /// <remark>
        /// 是否位域
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"设计器支持"),DisplayName(@"连接对应的字段"),Description("是否位域")]
        public Guid LinkField
        {
            get
            {
                return _linkField;
            }
            set
            {
                if(_linkField == value)
                    return;
                BeforePropertyChanged(nameof(LinkField), _linkField,value);
                _linkField = value;
                OnPropertyChanged(nameof(LinkField));
            }
        }

        /// <summary>
        /// 子级(继承)
        /// </summary>
        /// <remark>
        /// 子级(继承)
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"设计器支持"),DisplayName(@"子级(继承)"),Description("子级(继承)")]
        public override IEnumerable<ConfigBase> MyChilds => _items;

        /// <summary>
        /// 枚举节点
        /// </summary>
        [DataMember,JsonProperty("_items", NullValueHandling = NullValueHandling.Ignore)]
        internal ObservableCollection<EnumItem> _items;

        /// <summary>
        /// 枚举节点
        /// </summary>
        /// <remark>
        /// 枚举节点
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"设计器支持"),DisplayName(@"枚举节点"),Description("枚举节点")]
        public ObservableCollection<EnumItem> Items
        {
            get
            {
                if (_items != null)
                    return _items;
                _items = new ObservableCollection<EnumItem>();
                OnPropertyChanged(nameof(Items));
                return _items;
            }
            set
            {
                if(_items == value)
                    return;
                BeforePropertyChanged(nameof(Items), _items,value);
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        } 
        #endregion

    }
}