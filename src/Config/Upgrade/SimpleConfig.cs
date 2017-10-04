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
    /// 配置基础
    /// </summary>
    [DataContract,JsonObject(MemberSerialization.OptIn)]
    public partial class SimpleConfig : NotificationObject
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public SimpleConfig()
        {
        }

        #endregion

 
        #region *设计

        /// <summary>
        /// 名称
        /// </summary>
        [DataMember,JsonProperty("_name", NullValueHandling = NullValueHandling.Ignore)]
        internal string _name;

        /// <summary>
        /// 名称
        /// </summary>
        /// <remark>
        /// 名称
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"*设计"),DisplayName(@"名称"),Description("名称")]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if(_name == value)
                    return;
                this.BeforePropertyChanged(nameof(Name), _name,value);
                this._name = value;
                this.OnPropertyChanged(nameof(Name));
            }
        }

        /// <summary>
        /// 标题
        /// </summary>
        [DataMember,JsonProperty("_caption", NullValueHandling = NullValueHandling.Ignore)]
        internal string _caption;

        /// <summary>
        /// 标题
        /// </summary>
        /// <remark>
        /// 标题
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"*设计"),DisplayName(@"标题"),Description("标题")]
        public string Caption
        {
            get
            {
                return _caption;
            }
            set
            {
                if(_caption == value)
                    return;
                this.BeforePropertyChanged(nameof(Caption), _caption,value);
                this._caption = value;
                this.OnPropertyChanged(nameof(Caption));
            }
        }

        /// <summary>
        /// 说明
        /// </summary>
        [DataMember,JsonProperty("_description", NullValueHandling = NullValueHandling.Ignore)]
        internal string _description;

        /// <summary>
        /// 说明
        /// </summary>
        /// <remark>
        /// 说明
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"*设计"),DisplayName(@"说明"),Description("说明")]
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if(_description == value)
                    return;
                this.BeforePropertyChanged(nameof(Description), _description,value);
                this._description = value;
                this.OnPropertyChanged(nameof(Description));
            }
        } 
        #endregion

    }
}