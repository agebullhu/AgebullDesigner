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
    public partial class ClassifyConfig : ConfigBase
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public ClassifyConfig()
        {
        }

        #endregion

 
        #region 设计器支持

        /// <summary>
        /// 分类
        /// </summary>
        [DataMember,JsonProperty("_Classify", NullValueHandling = NullValueHandling.Ignore)]
        internal string _classify;

        /// <summary>
        /// 分类
        /// </summary>
        /// <remark>
        /// 分类(仅引用可行)
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"设计器支持"),DisplayName(@"分类"),Description("分类(仅引用可行)")]
        public string Classify
        {
            get
            {
                return _classify;
            }
            set
            {
                if(_classify == value)
                    return;
                this.BeforePropertyChanged(nameof(Classify), _classify,value);
                this._classify = value;
                this.OnPropertyChanged(nameof(Classify));
            }
        } 
        #endregion

    }
}