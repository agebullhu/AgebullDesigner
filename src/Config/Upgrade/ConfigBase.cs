/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2017/7/12 22:06:39*/
/*****************************************************
©2008-2017 Copy right by agebull.hu(胡天水)
作者:agebull.hu(胡天水)
工程:Agebull.Common.Config
建立:2014-12-03
修改:2017-07-12
*****************************************************/

using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Runtime.Serialization;
using Agebull.Common;
using Agebull.Common.DataModel;

using Newtonsoft.Json;

namespace Gboxt.Common.DataAccess.Schemas
{
    /// <summary>
    /// 配置基础
    /// </summary>
    [DataContract,JsonObject(MemberSerialization.OptIn)]
    public partial class ConfigBase : SimpleConfig
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public ConfigBase()
        {
        }

        #endregion

 
        #region 设计器支持

        /// <summary>
        /// 当前解决方案实例
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        internal SolutionConfig _solution;

        /// <summary>
        /// 当前解决方案实例
        /// </summary>
        /// <remark>
        /// 当前解决方案实例
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"设计器支持"),DisplayName(@"当前解决方案实例"),Description("当前解决方案实例")]
        public SolutionConfig Solution
        {
            get
            {
                return _solution;
            }
            set
            {
                if(_solution == value)
                    return;
                this.BeforePropertyChanged(nameof(Solution), _solution,value);
                this._solution = value;
                this.OnPropertyChanged(nameof(Solution));
            }
        }

        /// <summary>
        /// 当前配置类型
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        internal string _type;

        /// <summary>
        /// 当前配置类型
        /// </summary>
        /// <remark>
        /// 当前配置类型
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"设计器支持"),DisplayName(@"当前配置类型"),Description("当前配置类型")]
        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                if(_type == value)
                    return;
                this.BeforePropertyChanged(nameof(Type), _type,value);
                this._type = value;
                this.OnPropertyChanged(nameof(Type));
            }
        }

        /// <summary>
        /// 是否参照对象
        /// </summary>
        [DataMember,JsonProperty("_isReference", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isReference;

        /// <summary>
        /// 是否参照对象
        /// </summary>
        /// <remark>
        /// 是否参照对象，是则永远只读
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"设计器支持"),DisplayName(@"是否参照对象"),Description("是否参照对象，是则永远只读")]
        public bool IsReference
        {
            get
            {
                return _isReference;
            }
            set
            {
                if(_isReference == value)
                    return;
                this.BeforePropertyChanged(nameof(IsReference), _isReference,value);
                this._isReference = value;
                this.OnPropertyChanged(nameof(IsReference));
            }
        }

        /// <summary>
        /// 标识
        /// </summary>
        [DataMember,JsonProperty("_key", NullValueHandling = NullValueHandling.Ignore)]
        internal Guid _key;

        /// <summary>
        /// 标识
        /// </summary>
        /// <remark>
        /// 名称
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"设计器支持"),DisplayName(@"标识"),Description("名称")]
        public Guid Key
        {
            get
            {
                return _key;
            }
            set
            {
                if(_key == value)
                    return;
                this.BeforePropertyChanged(nameof(Key), _key,value);
                this._key = value;
                this.OnPropertyChanged(nameof(Key));
            }
        }

        /// <summary>
        /// 唯一标识
        /// </summary>
        [DataMember,JsonProperty("Identity", NullValueHandling = NullValueHandling.Ignore)]
        internal int _identity;

        /// <summary>
        /// 唯一标识
        /// </summary>
        /// <remark>
        /// 唯一标识
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"设计器支持"),DisplayName(@"唯一标识"),Description("唯一标识")]
        public int Identity
        {
            get
            {
                return _identity;
            }
            set
            {
                if(_identity == value)
                    return;
                this.BeforePropertyChanged(nameof(Identity), _identity,value);
                this._identity = value;
                this.OnPropertyChanged(nameof(Identity));
            }
        }

        /// <summary>
        /// 编号
        /// </summary>
        [DataMember,JsonProperty("Index", NullValueHandling = NullValueHandling.Ignore)]
        internal int _index;

        /// <summary>
        /// 编号
        /// </summary>
        /// <remark>
        /// 编号
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"设计器支持"),DisplayName(@"编号"),Description("编号")]
        public int Index
        {
            get
            {
                return _index;
            }
            set
            {
                if(_index == value)
                    return;
                this.BeforePropertyChanged(nameof(Index), _index,value);
                this._index = value;
                this.OnPropertyChanged(nameof(Index));
            }
        }

        /// <summary>
        /// 废弃
        /// </summary>
        [DataMember,JsonProperty("Discard", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _discard;

        /// <summary>
        /// 废弃
        /// </summary>
        /// <remark>
        /// 废弃
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"设计器支持"),DisplayName(@"废弃"),Description("废弃")]
        public bool Discard
        {
            get
            {
                return _discard;
            }
            set
            {
                if(_discard == value)
                    return;
                this.BeforePropertyChanged(nameof(Discard), _discard,value);
                this._discard = value;
                this.OnPropertyChanged(nameof(Discard));
            }
        }

        /// <summary>
        /// 冻结
        /// </summary>
        [DataMember,JsonProperty("IsFreeze", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isFreeze;

        /// <summary>
        /// 冻结
        /// </summary>
        /// <remark>
        /// 如为真,此配置的更改将不生成代码
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"设计器支持"),DisplayName(@"冻结"),Description("如为真,此配置的更改将不生成代码")]
        public bool IsFreeze
        {
            get
            {
                return _isFreeze;
            }
            set
            {
                if(_isFreeze == value)
                    return;
                this.BeforePropertyChanged(nameof(IsFreeze), _isFreeze,value);
                this._isFreeze = value;
                this.OnPropertyChanged(nameof(IsFreeze));
            }
        }

        /// <summary>
        /// 标记删除
        /// </summary>
        [DataMember,JsonProperty("_isDelete", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isDelete;

        /// <summary>
        /// 标记删除
        /// </summary>
        /// <remark>
        /// 如为真,保存时删除
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"设计器支持"),DisplayName(@"标记删除"),Description("如为真,保存时删除")]
        public bool IsDelete
        {
            get
            {
                return _isDelete;
            }
            set
            {
                if(_isDelete == value)
                    return;
                this.BeforePropertyChanged(nameof(IsDelete), _isDelete,value);
                this._isDelete = value;
                this.OnPropertyChanged(nameof(IsDelete));
            }
        }

        /// <summary>
        /// 曾用名
        /// </summary>
        [DataMember,JsonProperty("_oldNames", NullValueHandling = NullValueHandling.Ignore)]
        internal List<string> _oldNames;

        /// <summary>
        /// 曾用名
        /// </summary>
        /// <remark>
        /// 曾用名
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"设计器支持"),DisplayName(@"曾用名"),Description("曾用名")]
        public List<string> OldNames
        {
            get
            {
                return _oldNames;
            }
            set
            {
                if(_oldNames == value)
                    return;
                this.BeforePropertyChanged(nameof(OldNames), _oldNames,value);
                this._oldNames = value;
                this.OnPropertyChanged(nameof(OldNames));
            }
        }

        /// <summary>
        /// 曾用名
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        internal string _nameHistory;

        /// <summary>
        /// 曾用名
        /// </summary>
        /// <remark>
        /// 曾用名
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"设计器支持"),DisplayName(@"曾用名"),Description("曾用名")]
        public string NameHistory
        {
            get
            {
                return _nameHistory;
            }
            set
            {
                if(_nameHistory == value)
                    return;
                this.BeforePropertyChanged(nameof(NameHistory), _nameHistory,value);
                this._nameHistory = value;
                this.OnPropertyChanged(nameof(NameHistory));
            }
        } 
        #endregion 
        #region 扩展配置

        /// <summary>
        /// 扩展配置
        /// </summary>
        [DataMember,JsonProperty("_extendConfig", NullValueHandling = NullValueHandling.Ignore)]
        internal List<ConfigItem> _extendConfig;

        /// <summary>
        /// 扩展配置
        /// </summary>
        /// <remark>
        /// 扩展配置
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"扩展配置"),DisplayName(@"扩展配置"),Description("扩展配置")]
        public List<ConfigItem> ExtendConfig
        {
            get
            {
                return _extendConfig;
            }
            set
            {
                if(_extendConfig == value)
                    return;
                this.BeforePropertyChanged(nameof(ExtendConfig), _extendConfig,value);
                this._extendConfig = value;
                this.OnPropertyChanged(nameof(ExtendConfig));
            }
        }

        /// <summary>
        /// 扩展配置
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        internal ConfigItemList _extendConfigList;

        /// <summary>
        /// 扩展配置
        /// </summary>
        /// <remark>
        /// 扩展配置
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"扩展配置"),DisplayName(@"扩展配置"),Description("扩展配置")]
        public ConfigItemList ExtendConfigList
        {
            get
            {
                return _extendConfigList;
            }
            set
            {
                if(_extendConfigList == value)
                    return;
                this.BeforePropertyChanged(nameof(ExtendConfigList), _extendConfigList,value);
                this._extendConfigList = value;
                this.OnPropertyChanged(nameof(ExtendConfigList));
            }
        }

        /// <summary>
        /// 扩展配置
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        internal ConfigItemListBool _extendConfigListBool;

        /// <summary>
        /// 扩展配置
        /// </summary>
        /// <remark>
        /// 扩展配置
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"扩展配置"),DisplayName(@"扩展配置"),Description("扩展配置")]
        public ConfigItemListBool ExtendConfigListBool
        {
            get
            {
                return _extendConfigListBool;
            }
            set
            {
                if(_extendConfigListBool == value)
                    return;
                this.BeforePropertyChanged(nameof(ExtendConfigListBool), _extendConfigListBool,value);
                this._extendConfigListBool = value;
                this.OnPropertyChanged(nameof(ExtendConfigListBool));
            }
        }

        /// <summary>
        /// 读写扩展配置
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        internal string _item;

        /// <summary>
        /// 读写扩展配置
        /// </summary>
        /// <remark>
        /// 读写扩展配置
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"扩展配置"),DisplayName(@"读写扩展配置"),Description("读写扩展配置")]
        public string Item
        {
            get
            {
                return _item;
            }
            set
            {
                if(_item == value)
                    return;
                this.BeforePropertyChanged(nameof(Item), _item,value);
                this._item = value;
                this.OnPropertyChanged(nameof(Item));
            }
        }

        /// <summary>
        /// 引用对象键
        /// </summary>
        [DataMember,JsonProperty("ReferenceKey", NullValueHandling = NullValueHandling.Ignore)]
        internal Guid _referenceKey;

        /// <summary>
        /// 引用对象键
        /// </summary>
        /// <remark>
        /// 引用对象键，指内部对象的引用
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"扩展配置"),DisplayName(@"引用对象键"),Description("引用对象键，指内部对象的引用")]
        public Guid ReferenceKey
        {
            get
            {
                return _referenceKey;
            }
            set
            {
                if(_referenceKey == value)
                    return;
                this.BeforePropertyChanged(nameof(ReferenceKey), _referenceKey,value);
                this._referenceKey = value;
                this.OnPropertyChanged(nameof(ReferenceKey));
            }
        } 
        #endregion 
        #region 对象关联

        /// <summary>
        /// 标签（对应关系等）
        /// </summary>
        [DataMember,JsonProperty("Tag", NullValueHandling = NullValueHandling.Ignore)]
        internal string _tag;

        /// <summary>
        /// 标签（对应关系等）
        /// </summary>
        /// <remark>
        /// 值
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"对象关联"),DisplayName(@"标签（对应关系等）"),Description("值")]
        public string Tag
        {
            get
            {
                return _tag;
            }
            set
            {
                if(_tag == value)
                    return;
                this.BeforePropertyChanged(nameof(Tag), _tag,value);
                this._tag = value;
                this.OnPropertyChanged(nameof(Tag));
            }
        } 
        #endregion

    }
}