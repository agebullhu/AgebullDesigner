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
    /// 分类配置
    /// </summary>
    [DataContract,JsonObject(MemberSerialization.OptIn)]
    public partial class ClassifyItem`1 : ParentConfigBase
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public ClassifyItem`1()
        {
        }

        #endregion

 
        #region 

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
        [Category(@""),DisplayName(@"子级"),Description("子级")]
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
        [IgnoreDataMember,JsonIgnore]
        internal ConfigCollection<TConfig> _items;

        /// <summary>
        /// 子级
        /// </summary>
        /// <remark>
        /// 子级
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@""),DisplayName(@"子级"),Description("子级")]
        public ConfigCollection<TConfig> Items
        {
            get
            {
                if (_items != null)
                    return _items;
                _items = new ConfigCollection<TConfig>();
                OnPropertyChanged(nameof(Items));
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

    }
}