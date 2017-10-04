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
    /// 单独保存的配置
    /// </summary>
    [DataContract,JsonObject(MemberSerialization.OptIn)]
    public partial class FileConfigBase : ClassifyConfig
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public FileConfigBase()
        {
        }

        #endregion

 
        #region 系统

        /// <summary>
        /// 保存地址
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        internal string _fileName;

        /// <summary>
        /// 保存地址
        /// </summary>
        /// <remark>
        /// 保存地址
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"系统"),DisplayName(@"保存地址"),Description("保存地址")]
        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                if(_fileName == value)
                    return;
                this.BeforePropertyChanged(nameof(FileName), _fileName,value);
                this._fileName = value;
                this.OnPropertyChanged(nameof(FileName));
            }
        } 
        #endregion

    }
}