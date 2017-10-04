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
    /// 带子级的配置
    /// </summary>
    [DataContract,JsonObject(MemberSerialization.OptIn)]
    public partial class ParentConfigBase : FileConfigBase
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public ParentConfigBase()
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
        #endregion 
        #region 数据模型

        /// <summary>
        /// 简称
        /// </summary>
        [DataMember,JsonProperty("_abbreviation", NullValueHandling = NullValueHandling.Ignore)]
        internal string _abbreviation;

        /// <summary>
        /// 简称
        /// </summary>
        /// <remark>
        /// 简称
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"数据模型"),DisplayName(@"简称"),Description("简称")]
        public string Abbreviation
        {
            get
            {
                return _abbreviation;
            }
            set
            {
                if(_abbreviation == value)
                    return;
                this.BeforePropertyChanged(nameof(Abbreviation), _abbreviation,value);
                this._abbreviation = value;
                this.OnPropertyChanged(nameof(Abbreviation));
            }
        } 
        #endregion

    }
}