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
    public partial class ClassifyGroupConfig`1 : ConfigBase
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public ClassifyGroupConfig`1()
        {
        }

        #endregion

 
        #region 

        /// <summary>
        /// Classifies
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        internal ConfigCollection<ClassifyItem<TConfig>> _classifies;

        /// <summary>
        /// Classifies
        /// </summary>
        /// <remark>
        /// Classifies
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@""),DisplayName(@"Classifies"),Description("Classifies")]
        public ConfigCollection<ClassifyItem<TConfig>> Classifies
        {
            get
            {
                if (_classifies != null)
                    return _classifies;
                _classifies = new ConfigCollection<ClassifyItem<TConfig>>();
                OnPropertyChanged(nameof(Classifies));
                return _classifies;
            }
            set
            {
                if(_classifies == value)
                    return;
                this.BeforePropertyChanged(nameof(Classifies), _classifies,value);
                this._classifies = value;
                this.OnPropertyChanged(nameof(Classifies));
            }
        } 
        #endregion

    }
}