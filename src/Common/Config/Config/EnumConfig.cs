﻿/*design by:agebull designer date:2017/7/12 22:06:39*/
/*****************************************************
©2008-2017 Copy right by agebull.hu(胡天水)
作者:agebull.hu(胡天水)
工程:Agebull.Common.Config
建立:2014-12-03
修改:2017-07-12
*****************************************************/

using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 枚举配置
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class EnumConfig : ProjectChildConfigBase
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
        /// 外部定义
        /// </summary>
        [DataMember, JsonProperty("isOut", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isOut;

        /// <summary>
        /// 外部定义
        /// </summary>
        /// <remark>
        /// 外部定义
        /// </remark>
        [JsonIgnore]
        [Category(@"数据模型"), DisplayName(@"外部定义"), Description("外部定义")]
        public bool IsOut
        {
            get => _isOut;
            set
            {
                if (_isOut == value)
                    return;
                BeforePropertyChange(nameof(IsOut), _isOut, value);
                _isOut = value;
                OnPropertyChanged(nameof(IsOut));
            }
        }
        /// <summary>
        /// 是否位域
        /// </summary>
        [DataMember, JsonProperty("IsFlagEnum", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isFlagEnum;

        /// <summary>
        /// 是否位域
        /// </summary>
        /// <remark>
        /// 是否位域
        /// </remark>
        [JsonIgnore]
        [Category(@"数据模型"), DisplayName(@"是否位域"), Description("是否位域")]
        public bool IsFlagEnum
        {
            get => _isFlagEnum;
            set
            {
                if (_isFlagEnum == value)
                    return;
                BeforePropertyChange(nameof(IsFlagEnum), _isFlagEnum, value);
                _isFlagEnum = value;
                OnPropertyChanged(nameof(IsFlagEnum));
            }
        }
        #endregion
        #region 设计器支持

        /// <summary>
        /// 枚举节点
        /// </summary>
        [DataMember, JsonProperty("_items", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal ConfigCollection<EnumItem> _items;

        /// <summary>
        /// 枚举节点
        /// </summary>
        /// <remark>
        /// 枚举节点
        /// </remark>
        [JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"枚举节点"), Description("枚举节点")]
        public ConfigCollection<EnumItem> Items
        {
            get
            {
                if (_items != null)
                    return _items;
                _items = new ConfigCollection<EnumItem>(this, nameof(Items));
                RaisePropertyChanged(nameof(Items));
                return _items;
            }
            set
            {
                if (_items == value)
                    return;
                BeforePropertyChange(nameof(Items), _items, value);
                _items = value;
                if (value != null)
                {
                    value.Name = nameof(Items);
                    value.Parent = this;
                }
                OnPropertyChanged(nameof(Items));
            }
        }


        /// <summary>
        /// 加入子级
        /// </summary>
        /// <param name="propertyConfig"></param>
        public void Add(EnumItem propertyConfig)
        {
            if (!Items.Contains(propertyConfig))
            {
                propertyConfig.Parent = this;
                Items.Add(propertyConfig);
            }
        }

        /// <summary>
        /// 加入子级
        /// </summary>
        /// <param name="propertyConfig"></param>
        public void Remove(EnumItem propertyConfig)
        {
            Items.Remove(propertyConfig);
        }
        #endregion

    }
}