/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2017/7/12 22:06:40*/
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
    /// 类型(C++)定义,即C++中的typedef
    /// </summary>
    [DataContract,JsonObject(MemberSerialization.OptIn)]
    public partial class TypedefItem : ParentConfigBase
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public TypedefItem()
        {
        }

        #endregion

 
        #region 子级

        /// <summary>
        /// 子级
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        internal IEnumerable<ConfigBase> _myChilds;

        /// <summary>
        /// 子级
        /// </summary>
        /// <remark>
        /// 子级
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"子级"),DisplayName(@"子级"),Description("子级")]
        public IEnumerable<ConfigBase> MyChilds
        {
            get
            {
                return _myChilds;
            }
            set
            {
                if(_myChilds == value)
                    return;
                this.BeforePropertyChanged(nameof(MyChilds), _myChilds,value);
                this._myChilds = value;
                this.OnPropertyChanged(nameof(MyChilds));
            }
        }

        /// <summary>
        /// 子级
        /// </summary>
        [DataMember,JsonProperty("Items", NullValueHandling = NullValueHandling.Ignore)]
        internal Dictionary<string,EnumItem> _items;

        /// <summary>
        /// 子级
        /// </summary>
        /// <remark>
        /// 子级
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"子级"),DisplayName(@"子级"),Description("子级")]
        public Dictionary<string,EnumItem> Items
        {
            get
            {
                return _items;
            }
            set
            {
                if(_items == value)
                    return;
                this.BeforePropertyChanged(nameof(Items), _items,value);
                this._items = value;
                this.OnPropertyChanged(nameof(Items));
            }
        } 
        #endregion 
        #region 类型定义

        /// <summary>
        /// 原始类型
        /// </summary>
        [DataMember,JsonProperty("KeyWork", NullValueHandling = NullValueHandling.Ignore)]
        internal string _keyWork;

        /// <summary>
        /// 原始类型
        /// </summary>
        /// <remark>
        /// 类型名称对应的语言关键字
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"类型定义"),DisplayName(@"原始类型"),Description("类型名称对应的语言关键字")]
        public string KeyWork
        {
            get
            {
                return _keyWork;
            }
            set
            {
                if(_keyWork == value)
                    return;
                this.BeforePropertyChanged(nameof(KeyWork), _keyWork,value);
                this._keyWork = value;
                this.OnPropertyChanged(nameof(KeyWork));
            }
        }

        /// <summary>
        /// 数组名称
        /// </summary>
        [DataMember,JsonProperty("ArrayLen", NullValueHandling = NullValueHandling.Ignore)]
        internal string _arrayLen;

        /// <summary>
        /// 数组名称
        /// </summary>
        /// <remark>
        /// 数组名称
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"类型定义"),DisplayName(@"数组名称"),Description("数组名称")]
        public string ArrayLen
        {
            get
            {
                return _arrayLen;
            }
            set
            {
                if(_arrayLen == value)
                    return;
                this.BeforePropertyChanged(nameof(ArrayLen), _arrayLen,value);
                this._arrayLen = value;
                this.OnPropertyChanged(nameof(ArrayLen));
            }
        } 
        #endregion

    }
}