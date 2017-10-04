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
    /// 关联
    /// </summary>
    [DataContract,JsonObject(MemberSerialization.OptIn)]
    public partial class TableReleation : ConfigBase
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public TableReleation()
        {
        }

        #endregion

 
        #region 

        /// <summary>
        /// 名称
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        internal string _displayName;

        /// <summary>
        /// 名称
        /// </summary>
        /// <remark>
        /// 名称
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@""),DisplayName(@"名称"),Description("名称")]
        public string DisplayName
        {
            get
            {
                return _displayName;
            }
            set
            {
                if(_displayName == value)
                    return;
                this.BeforePropertyChanged(nameof(DisplayName), _displayName,value);
                this._displayName = value;
                this.OnPropertyChanged(nameof(DisplayName));
            }
        }

        /// <summary>
        /// 上级
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        internal EntityConfig _parent;

        /// <summary>
        /// 上级
        /// </summary>
        /// <remark>
        /// 上级
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@""),DisplayName(@"上级"),Description("上级")]
        public EntityConfig Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                if(_parent == value)
                    return;
                this.BeforePropertyChanged(nameof(Parent), _parent,value);
                this._parent = value;
                this.OnPropertyChanged(nameof(Parent));
            }
        } 
        #endregion 
        #region 数据关联

        /// <summary>
        /// 关联表的外键名称
        /// </summary>
        [DataMember,JsonProperty("ForeignKey", NullValueHandling = NullValueHandling.Ignore)]
        internal string _foreignKey;

        /// <summary>
        /// 关联表的外键名称
        /// </summary>
        /// <remark>
        /// 关联表的外键名称
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"数据关联"),DisplayName(@"关联表的外键名称"),Description("关联表的外键名称")]
        public string ForeignKey
        {
            get
            {
                return _foreignKey;
            }
            set
            {
                if(_foreignKey == value)
                    return;
                this.BeforePropertyChanged(nameof(ForeignKey), _foreignKey,value);
                this._foreignKey = value;
                this.OnPropertyChanged(nameof(ForeignKey));
            }
        }

        /// <summary>
        /// 与关联表的外键对应的字段名称
        /// </summary>
        [DataMember,JsonProperty("PrimaryKey", NullValueHandling = NullValueHandling.Ignore)]
        internal string _primaryKey;

        /// <summary>
        /// 与关联表的外键对应的字段名称
        /// </summary>
        /// <remark>
        /// 上级关联到当前对象名称
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"数据关联"),DisplayName(@"与关联表的外键对应的字段名称"),Description("上级关联到当前对象名称")]
        public string PrimaryKey
        {
            get
            {
                return _primaryKey;
            }
            set
            {
                if(_primaryKey == value)
                    return;
                this.BeforePropertyChanged(nameof(PrimaryKey), _primaryKey,value);
                this._primaryKey = value;
                this.OnPropertyChanged(nameof(PrimaryKey));
            }
        }

        /// <summary>
        /// 关联表
        /// </summary>
        [DataMember,JsonProperty("Friend", NullValueHandling = NullValueHandling.Ignore)]
        internal string _friend;

        /// <summary>
        /// 关联表
        /// </summary>
        /// <remark>
        /// 关联表
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"数据关联"),DisplayName(@"关联表"),Description("关联表")]
        public string Friend
        {
            get
            {
                return _friend;
            }
            set
            {
                if(_friend == value)
                    return;
                this.BeforePropertyChanged(nameof(Friend), _friend,value);
                this._friend = value;
                this.OnPropertyChanged(nameof(Friend));
            }
        }

        /// <summary>
        /// 关系类型的说明文字
        /// </summary>
        const string Releation_Description = @"0[与Friend的平等关系的1对1],1[存在对Friend的上下级关系的1对1],2[1对多]";

        /// <summary>
        /// 关系类型
        /// </summary>
        [DataMember,JsonProperty("Releation", NullValueHandling = NullValueHandling.Ignore)]
        internal int _releation;

        /// <summary>
        /// 关系类型
        /// </summary>
        /// <remark>
        /// 0[与Friend的平等关系的1对1],1[存在对Friend的上下级关系的1对1],2[1对多]
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"数据关联"),DisplayName(@"关系类型"),Description(Releation_Description)]
        public int Releation
        {
            get
            {
                return _releation;
            }
            set
            {
                if(_releation == value)
                    return;
                this.BeforePropertyChanged(nameof(Releation), _releation,value);
                this._releation = value;
                this.OnPropertyChanged(nameof(Releation));
            }
        }

        /// <summary>
        /// 扩展条件
        /// </summary>
        [DataMember,JsonProperty("Condition", NullValueHandling = NullValueHandling.Ignore)]
        internal string _condition;

        /// <summary>
        /// 扩展条件
        /// </summary>
        /// <remark>
        /// 扩展条件
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"数据关联"),DisplayName(@"扩展条件"),Description("扩展条件")]
        public string Condition
        {
            get
            {
                return _condition;
            }
            set
            {
                if(_condition == value)
                    return;
                this.BeforePropertyChanged(nameof(Condition), _condition,value);
                this._condition = value;
                this.OnPropertyChanged(nameof(Condition));
            }
        } 
        #endregion

    }
}