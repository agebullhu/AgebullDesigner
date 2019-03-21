/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/2 23:11:57*/
#region
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using Newtonsoft.Json;

using Agebull.Common;
using Agebull.EntityModel.Common;
using Agebull.EntityModel.Common;
using Agebull.EntityModel.Interfaces;



#endregion

namespace Agebull.Common.OAuth
{
    /// <summary>
    /// 行政区域
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class GovernmentAreaData : IStateData , IHistoryData , IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public GovernmentAreaData()
        {
            Initialize();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        partial void Initialize();
        #endregion

        #region 基本属性


        /// <summary>
        /// 修改主键
        /// </summary>
        public void ChangePrimaryKey(long id)
        {
            _id = id;
        }
        /// <summary>
        /// 标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _id;

        partial void OnIdGet();

        partial void OnIdSet(ref long value);

        partial void OnIdLoad(ref long value);

        partial void OnIdSeted();

        
        /// <summary>
        /// 标识
        /// </summary>
        /// <remarks>
        /// 标题
        /// </remarks>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("id", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"标识")]
        public long Id
        {
            get
            {
                OnIdGet();
                return this._id;
            }
            set
            {
                if(this._id == value)
                    return;
                //if(this._id > 0)
                //    throw new Exception("主键一旦设置就不可以修改");
                OnIdSet(ref value);
                this._id = value;
                this.OnPropertyChanged(_DataStruct_.Real_Id);
                OnIdSeted();
            }
        }
        /// <summary>
        /// 编码
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _code;

        partial void OnCodeGet();

        partial void OnCodeSet(ref string value);

        partial void OnCodeSeted();

        
        /// <summary>
        /// 编码
        /// </summary>
        /// <remarks>
        /// 代码
        /// </remarks>
        /// <value>
        /// 可存储16个字符.合理长度应不大于16.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("code", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"编码")]
        public  string Code
        {
            get
            {
                OnCodeGet();
                return this._code;
            }
            set
            {
                if(this._code == value)
                    return;
                OnCodeSet(ref value);
                this._code = value;
                OnCodeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Code);
            }
        }
        /// <summary>
        /// 全称
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _fullName;

        partial void OnFullNameGet();

        partial void OnFullNameSet(ref string value);

        partial void OnFullNameSeted();

        
        /// <summary>
        /// 全称
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("fullName", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"全称")]
        public  string FullName
        {
            get
            {
                OnFullNameGet();
                return this._fullName;
            }
            set
            {
                if(this._fullName == value)
                    return;
                OnFullNameSet(ref value);
                this._fullName = value;
                OnFullNameSeted();
                this.OnPropertyChanged(_DataStruct_.Real_FullName);
            }
        }
        /// <summary>
        /// 简称
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _shortName;

        partial void OnShortNameGet();

        partial void OnShortNameSet(ref string value);

        partial void OnShortNameSeted();

        
        /// <summary>
        /// 简称
        /// </summary>
        /// <remarks>
        /// 短名称
        /// </remarks>
        /// <value>
        /// 可存储20个字符.合理长度应不大于20.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("shortName", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"简称")]
        public  string ShortName
        {
            get
            {
                OnShortNameGet();
                return this._shortName;
            }
            set
            {
                if(this._shortName == value)
                    return;
                OnShortNameSet(ref value);
                this._shortName = value;
                OnShortNameSeted();
                this.OnPropertyChanged(_DataStruct_.Real_ShortName);
            }
        }
        /// <summary>
        /// 树形名称
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _treeName;

        partial void OnTreeNameGet();

        partial void OnTreeNameSet(ref string value);

        partial void OnTreeNameSeted();

        
        /// <summary>
        /// 树形名称
        /// </summary>
        /// <remarks>
        /// 树的名字
        /// </remarks>
        /// <value>
        /// 可存储500个字符.合理长度应不大于500.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("treeName", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"树形名称")]
        public  string TreeName
        {
            get
            {
                OnTreeNameGet();
                return this._treeName;
            }
            set
            {
                if(this._treeName == value)
                    return;
                OnTreeNameSet(ref value);
                this._treeName = value;
                OnTreeNameSeted();
                this.OnPropertyChanged(_DataStruct_.Real_TreeName);
            }
        }
        /// <summary>
        /// 级别
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public int _orgLevel;

        partial void OnOrgLevelGet();

        partial void OnOrgLevelSet(ref int value);

        partial void OnOrgLevelSeted();

        
        /// <summary>
        /// 级别
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("orgLevel", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"级别")]
        public  int OrgLevel
        {
            get
            {
                OnOrgLevelGet();
                return this._orgLevel;
            }
            set
            {
                if(this._orgLevel == value)
                    return;
                OnOrgLevelSet(ref value);
                this._orgLevel = value;
                OnOrgLevelSeted();
                this.OnPropertyChanged(_DataStruct_.Real_OrgLevel);
            }
        }
        /// <summary>
        /// 层级的序号
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public int _levelIndex;

        partial void OnLevelIndexGet();

        partial void OnLevelIndexSet(ref int value);

        partial void OnLevelIndexSeted();

        
        /// <summary>
        /// 层级的序号
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("levelIndex", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"层级的序号")]
        public  int LevelIndex
        {
            get
            {
                OnLevelIndexGet();
                return this._levelIndex;
            }
            set
            {
                if(this._levelIndex == value)
                    return;
                OnLevelIndexSet(ref value);
                this._levelIndex = value;
                OnLevelIndexSeted();
                this.OnPropertyChanged(_DataStruct_.Real_LevelIndex);
            }
        }
        /// <summary>
        /// 上级标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _parentId;

        partial void OnParentIdGet();

        partial void OnParentIdSet(ref long value);

        partial void OnParentIdSeted();

        
        /// <summary>
        /// 上级标识
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("parentId", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"上级标识")]
        public  long ParentId
        {
            get
            {
                OnParentIdGet();
                return this._parentId;
            }
            set
            {
                if(this._parentId == value)
                    return;
                OnParentIdSet(ref value);
                this._parentId = value;
                OnParentIdSeted();
                this.OnPropertyChanged(_DataStruct_.Real_ParentId);
            }
        }
        /// <summary>
        /// 备注
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _memo;

        partial void OnMemoGet();

        partial void OnMemoSet(ref string value);

        partial void OnMemoSeted();

        
        /// <summary>
        /// 备注
        /// </summary>
        /// <remarks>
        /// 备忘录
        /// </remarks>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("memo", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"备注")]
        public  string Memo
        {
            get
            {
                OnMemoGet();
                return this._memo;
            }
            set
            {
                if(this._memo == value)
                    return;
                OnMemoSet(ref value);
                this._memo = value;
                OnMemoSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Memo);
            }
        }
        /// <summary>
        /// 冻结更新
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public bool _isFreeze;

        partial void OnIsFreezeGet();

        partial void OnIsFreezeSet(ref bool value);

        partial void OnIsFreezeSeted();

        
        /// <summary>
        /// 冻结更新
        /// </summary>
        /// <remarks>
        /// 无论在什么数据状态,一旦设置且保存后,数据将不再允许执行Update的操作,作为Update的统一开关.取消的方法是单独设置这个字段的值
        /// </remarks>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("isFreeze", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"冻结更新")]
        public  bool IsFreeze
        {
            get
            {
                OnIsFreezeGet();
                return this._isFreeze;
            }
            set
            {
                if(this._isFreeze == value)
                    return;
                OnIsFreezeSet(ref value);
                this._isFreeze = value;
                OnIsFreezeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_IsFreeze);
            }
        }
        /// <summary>
        /// 数据状态
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public DataStateType _dataState;

        partial void OnDataStateGet();

        partial void OnDataStateSet(ref DataStateType value);

        partial void OnDataStateSeted();

        
        /// <summary>
        /// 数据状态
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("dataState", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"数据状态")]
        public  DataStateType DataState
        {
            get
            {
                OnDataStateGet();
                return this._dataState;
            }
            set
            {
                if(this._dataState == value)
                    return;
                OnDataStateSet(ref value);
                this._dataState = value;
                OnDataStateSeted();
                this.OnPropertyChanged(_DataStruct_.Real_DataState);
            }
        }
        /// <summary>
        /// 数据状态的可读内容
        /// </summary>
        [IgnoreDataMember,JsonIgnore,DisplayName("数据状态")]
        public string DataState_Content => DataState.ToCaption();

        /// <summary>
        /// 数据状态的数字属性
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public  int DataState_Number
        {
            get => (int)this.DataState;
            set => this.DataState = (DataStateType)value;
        }
        /// <summary>
        /// 制作时间
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public DateTime _addDate;

        partial void OnAddDateGet();

        partial void OnAddDateSet(ref DateTime value);

        partial void OnAddDateSeted();

        
        /// <summary>
        /// 制作时间
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("addDate", NullValueHandling = NullValueHandling.Ignore) , JsonConverter(typeof(MyDateTimeConverter)) , DisplayName(@"制作时间")]
        public  DateTime AddDate
        {
            get
            {
                OnAddDateGet();
                return this._addDate;
            }
            set
            {
                if(this._addDate == value)
                    return;
                OnAddDateSet(ref value);
                this._addDate = value;
                OnAddDateSeted();
                this.OnPropertyChanged(_DataStruct_.Real_AddDate);
            }
        }
        /// <summary>
        /// 最后修改者
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _lastReviserId;

        partial void OnLastReviserIdGet();

        partial void OnLastReviserIdSet(ref long value);

        partial void OnLastReviserIdSeted();

        
        /// <summary>
        /// 最后修改者
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("lastReviserId", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"最后修改者")]
        public  long LastReviserId
        {
            get
            {
                OnLastReviserIdGet();
                return this._lastReviserId;
            }
            set
            {
                if(this._lastReviserId == value)
                    return;
                OnLastReviserIdSet(ref value);
                this._lastReviserId = value;
                OnLastReviserIdSeted();
                this.OnPropertyChanged(_DataStruct_.Real_LastReviserId);
            }
        }
        /// <summary>
        /// 最后修改日期
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public DateTime _lastModifyDate;

        partial void OnLastModifyDateGet();

        partial void OnLastModifyDateSet(ref DateTime value);

        partial void OnLastModifyDateSeted();

        
        /// <summary>
        /// 最后修改日期
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("lastModifyDate", NullValueHandling = NullValueHandling.Ignore) , JsonConverter(typeof(MyDateTimeConverter)) , DisplayName(@"最后修改日期")]
        public  DateTime LastModifyDate
        {
            get
            {
                OnLastModifyDateGet();
                return this._lastModifyDate;
            }
            set
            {
                if(this._lastModifyDate == value)
                    return;
                OnLastModifyDateSet(ref value);
                this._lastModifyDate = value;
                OnLastModifyDateSeted();
                this.OnPropertyChanged(_DataStruct_.Real_LastModifyDate);
            }
        }
        /// <summary>
        /// 制作人
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _authorId;

        partial void OnAuthorIdGet();

        partial void OnAuthorIdSet(ref long value);

        partial void OnAuthorIdSeted();

        
        /// <summary>
        /// 制作人
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("authorId", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"制作人")]
        public  long AuthorId
        {
            get
            {
                OnAuthorIdGet();
                return this._authorId;
            }
            set
            {
                if(this._authorId == value)
                    return;
                OnAuthorIdSet(ref value);
                this._authorId = value;
                OnAuthorIdSeted();
                this.OnPropertyChanged(_DataStruct_.Real_AuthorId);
            }
        }

        #region 接口属性

        #endregion
        #region 扩展属性

        #endregion
        #endregion


        #region 名称的属性操作

    

        /// <summary>
        ///     设置属性值
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        protected override void SetValueInner(string property, object value)
        {
            if(property == null) return;
            switch(property.Trim().ToLower())
            {
            case "id":
                this.Id = (long)Convert.ToDecimal(value);
                return;
            case "code":
                this.Code = value == null ? null : value.ToString();
                return;
            case "fullname":
                this.FullName = value == null ? null : value.ToString();
                return;
            case "shortname":
                this.ShortName = value == null ? null : value.ToString();
                return;
            case "treename":
                this.TreeName = value == null ? null : value.ToString();
                return;
            case "orglevel":
                this.OrgLevel = (int)Convert.ToDecimal(value);
                return;
            case "levelindex":
                this.LevelIndex = (int)Convert.ToDecimal(value);
                return;
            case "parentid":
                this.ParentId = (long)Convert.ToDecimal(value);
                return;
            case "memo":
                this.Memo = value == null ? null : value.ToString();
                return;
            case "isfreeze":
                if (value != null)
                {
                    int vl;
                    if (int.TryParse(value.ToString(), out vl))
                    {
                        this.IsFreeze = vl != 0;
                    }
                    else
                    {
                        this.IsFreeze = Convert.ToBoolean(value);
                    }
                }
                return;
            case "datastate":
                if (value != null)
                {
                    if(value is int)
                    {
                        this.DataState = (DataStateType)(int)value;
                    }
                    else if(value is DataStateType)
                    {
                        this.DataState = (DataStateType)value;
                    }
                    else
                    {
                        var str = value.ToString();
                        DataStateType val;
                        if (DataStateType.TryParse(str, out val))
                        {
                            this.DataState = val;
                        }
                        else
                        {
                            int vl;
                            if (int.TryParse(str, out vl))
                            {
                                this.DataState = (DataStateType)vl;
                            }
                        }
                    }
                }
                return;
            case "adddate":
                this.AddDate = Convert.ToDateTime(value);
                return;
            case "lastreviserid":
                this.LastReviserId = (long)Convert.ToDecimal(value);
                return;
            case "lastmodifydate":
                this.LastModifyDate = Convert.ToDateTime(value);
                return;
            case "authorid":
                this.AuthorId = (long)Convert.ToDecimal(value);
                return;
            }

            //System.Diagnostics.Trace.WriteLine(property + @"=>" + value);

        }

    

        /// <summary>
        ///     设置属性值
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        protected override void SetValueInner(int index, object value)
        {
            switch(index)
            {
            case _DataStruct_.Id:
                this.Id = Convert.ToInt64(value);
                return;
            case _DataStruct_.Code:
                this.Code = value == null ? null : value.ToString();
                return;
            case _DataStruct_.FullName:
                this.FullName = value == null ? null : value.ToString();
                return;
            case _DataStruct_.ShortName:
                this.ShortName = value == null ? null : value.ToString();
                return;
            case _DataStruct_.TreeName:
                this.TreeName = value == null ? null : value.ToString();
                return;
            case _DataStruct_.OrgLevel:
                this.OrgLevel = Convert.ToInt32(value);
                return;
            case _DataStruct_.LevelIndex:
                this.LevelIndex = Convert.ToInt32(value);
                return;
            case _DataStruct_.ParentId:
                this.ParentId = Convert.ToInt64(value);
                return;
            case _DataStruct_.Memo:
                this.Memo = value == null ? null : value.ToString();
                return;
            case _DataStruct_.IsFreeze:
                this.IsFreeze = Convert.ToBoolean(value);
                return;
            case _DataStruct_.DataState:
                this.DataState = (DataStateType)value;
                return;
            case _DataStruct_.AddDate:
                this.AddDate = Convert.ToDateTime(value);
                return;
            case _DataStruct_.LastReviserId:
                this.LastReviserId = Convert.ToInt64(value);
                return;
            case _DataStruct_.LastModifyDate:
                this.LastModifyDate = Convert.ToDateTime(value);
                return;
            case _DataStruct_.AuthorId:
                this.AuthorId = Convert.ToInt64(value);
                return;
            }
        }


        /// <summary>
        ///     读取属性值
        /// </summary>
        /// <param name="property"></param>
        protected override object GetValueInner(string property)
        {
            switch(property)
            {
            case "id":
                return this.Id;
            case "code":
                return this.Code;
            case "fullname":
                return this.FullName;
            case "shortname":
                return this.ShortName;
            case "treename":
                return this.TreeName;
            case "orglevel":
                return this.OrgLevel;
            case "levelindex":
                return this.LevelIndex;
            case "parentid":
                return this.ParentId;
            case "memo":
                return this.Memo;
            case "isfreeze":
                return this.IsFreeze;
            case "datastate":
                return this.DataState.ToCaption();
            case "adddate":
                return this.AddDate;
            case "lastreviserid":
                return this.LastReviserId;
            case "lastmodifydate":
                return this.LastModifyDate;
            case "authorid":
                return this.AuthorId;
            }

            return null;
        }


        /// <summary>
        ///     读取属性值
        /// </summary>
        /// <param name="index"></param>
        protected override object GetValueInner(int index)
        {
            switch(index)
            {
                case _DataStruct_.Id:
                    return this.Id;
                case _DataStruct_.Code:
                    return this.Code;
                case _DataStruct_.FullName:
                    return this.FullName;
                case _DataStruct_.ShortName:
                    return this.ShortName;
                case _DataStruct_.TreeName:
                    return this.TreeName;
                case _DataStruct_.OrgLevel:
                    return this.OrgLevel;
                case _DataStruct_.LevelIndex:
                    return this.LevelIndex;
                case _DataStruct_.ParentId:
                    return this.ParentId;
                case _DataStruct_.Memo:
                    return this.Memo;
                case _DataStruct_.IsFreeze:
                    return this.IsFreeze;
                case _DataStruct_.DataState:
                    return this.DataState;
                case _DataStruct_.AddDate:
                    return this.AddDate;
                case _DataStruct_.LastReviserId:
                    return this.LastReviserId;
                case _DataStruct_.LastModifyDate:
                    return this.LastModifyDate;
                case _DataStruct_.AuthorId:
                    return this.AuthorId;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(GovernmentAreaData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as GovernmentAreaData;
            if(sourceEntity == null)
                return;
            this._id = sourceEntity._id;
            this._code = sourceEntity._code;
            this._fullName = sourceEntity._fullName;
            this._shortName = sourceEntity._shortName;
            this._treeName = sourceEntity._treeName;
            this._orgLevel = sourceEntity._orgLevel;
            this._levelIndex = sourceEntity._levelIndex;
            this._parentId = sourceEntity._parentId;
            this._memo = sourceEntity._memo;
            this._isFreeze = sourceEntity._isFreeze;
            this._dataState = sourceEntity._dataState;
            this._addDate = sourceEntity._addDate;
            this._lastReviserId = sourceEntity._lastReviserId;
            this._lastModifyDate = sourceEntity._lastModifyDate;
            this._authorId = sourceEntity._authorId;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(GovernmentAreaData source)
        {
                this.Id = source.Id;
                this.Code = source.Code;
                this.FullName = source.FullName;
                this.ShortName = source.ShortName;
                this.TreeName = source.TreeName;
                this.OrgLevel = source.OrgLevel;
                this.LevelIndex = source.LevelIndex;
                this.ParentId = source.ParentId;
                this.Memo = source.Memo;
                this.IsFreeze = source.IsFreeze;
                this.DataState = source.DataState;
                this.AddDate = source.AddDate;
                this.LastReviserId = source.LastReviserId;
                this.LastModifyDate = source.LastModifyDate;
                this.AuthorId = source.AuthorId;
        }
        #endregion

        #region 后期处理
        

        /// <summary>
        /// 单个属性修改的后期处理(保存后)
        /// </summary>
        /// <param name="subsist">当前实体生存状态</param>
        /// <param name="modifieds">修改列表</param>
        /// <remarks>
        /// 对当前对象的属性的更改,请自行保存,否则将丢失
        /// </remarks>
        protected override void OnLaterPeriodBySignleModified(EntitySubsist subsist,byte[] modifieds)
        {
            if (subsist == EntitySubsist.Deleting)
            {
                OnIdModified(subsist,false);
                OnCodeModified(subsist,false);
                OnFullNameModified(subsist,false);
                OnShortNameModified(subsist,false);
                OnTreeNameModified(subsist,false);
                OnOrgLevelModified(subsist,false);
                OnLevelIndexModified(subsist,false);
                OnParentIdModified(subsist,false);
                OnMemoModified(subsist,false);
                OnIsFreezeModified(subsist,false);
                OnDataStateModified(subsist,false);
                OnAddDateModified(subsist,false);
                OnLastReviserIdModified(subsist,false);
                OnLastModifyDateModified(subsist,false);
                OnAuthorIdModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnIdModified(subsist,true);
                OnCodeModified(subsist,true);
                OnFullNameModified(subsist,true);
                OnShortNameModified(subsist,true);
                OnTreeNameModified(subsist,true);
                OnOrgLevelModified(subsist,true);
                OnLevelIndexModified(subsist,true);
                OnParentIdModified(subsist,true);
                OnMemoModified(subsist,true);
                OnIsFreezeModified(subsist,true);
                OnDataStateModified(subsist,true);
                OnAddDateModified(subsist,true);
                OnLastReviserIdModified(subsist,true);
                OnLastModifyDateModified(subsist,true);
                OnAuthorIdModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[15] > 0)
            {
                OnIdModified(subsist,modifieds[_DataStruct_.Real_Id] == 1);
                OnCodeModified(subsist,modifieds[_DataStruct_.Real_Code] == 1);
                OnFullNameModified(subsist,modifieds[_DataStruct_.Real_FullName] == 1);
                OnShortNameModified(subsist,modifieds[_DataStruct_.Real_ShortName] == 1);
                OnTreeNameModified(subsist,modifieds[_DataStruct_.Real_TreeName] == 1);
                OnOrgLevelModified(subsist,modifieds[_DataStruct_.Real_OrgLevel] == 1);
                OnLevelIndexModified(subsist,modifieds[_DataStruct_.Real_LevelIndex] == 1);
                OnParentIdModified(subsist,modifieds[_DataStruct_.Real_ParentId] == 1);
                OnMemoModified(subsist,modifieds[_DataStruct_.Real_Memo] == 1);
                OnIsFreezeModified(subsist,modifieds[_DataStruct_.Real_IsFreeze] == 1);
                OnDataStateModified(subsist,modifieds[_DataStruct_.Real_DataState] == 1);
                OnAddDateModified(subsist,modifieds[_DataStruct_.Real_AddDate] == 1);
                OnLastReviserIdModified(subsist,modifieds[_DataStruct_.Real_LastReviserId] == 1);
                OnLastModifyDateModified(subsist,modifieds[_DataStruct_.Real_LastModifyDate] == 1);
                OnAuthorIdModified(subsist,modifieds[_DataStruct_.Real_AuthorId] == 1);
            }
        }

        /// <summary>
        /// 标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnIdModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 编码修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnCodeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 全称修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnFullNameModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 简称修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnShortNameModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 树形名称修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnTreeNameModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 级别修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOrgLevelModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 层级的序号修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnLevelIndexModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 上级标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnParentIdModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 备注修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnMemoModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 冻结更新修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnIsFreezeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 数据状态修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnDataStateModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 制作时间修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAddDateModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 最后修改者修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnLastReviserIdModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 最后修改日期修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnLastModifyDateModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 制作人修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAuthorIdModified(EntitySubsist subsist,bool isModified);
        #endregion

        #region 数据结构

        /// <summary>
        /// 实体结构
        /// </summary>
        [IgnoreDataMember,Browsable (false)]
        public override EntitySturct __Struct
        {
            get
            {
                return _DataStruct_.Struct;
            }
        }
        /// <summary>
        /// 实体结构
        /// </summary>
        public class _DataStruct_
        {
            /// <summary>
            /// 实体名称
            /// </summary>
            public const string EntityName = @"GovernmentArea";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"行政区域";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"行政区域";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0x0;
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityPrimaryKey = "Id";
            
            
            /// <summary>
            /// 标识的数字标识
            /// </summary>
            public const byte Id = 1;
            
            /// <summary>
            /// 标识的实时记录顺序
            /// </summary>
            public const int Real_Id = 0;

            /// <summary>
            /// 编码的数字标识
            /// </summary>
            public const byte Code = 3;
            
            /// <summary>
            /// 编码的实时记录顺序
            /// </summary>
            public const int Real_Code = 1;

            /// <summary>
            /// 全称的数字标识
            /// </summary>
            public const byte FullName = 4;
            
            /// <summary>
            /// 全称的实时记录顺序
            /// </summary>
            public const int Real_FullName = 2;

            /// <summary>
            /// 简称的数字标识
            /// </summary>
            public const byte ShortName = 5;
            
            /// <summary>
            /// 简称的实时记录顺序
            /// </summary>
            public const int Real_ShortName = 3;

            /// <summary>
            /// 树形名称的数字标识
            /// </summary>
            public const byte TreeName = 6;
            
            /// <summary>
            /// 树形名称的实时记录顺序
            /// </summary>
            public const int Real_TreeName = 4;

            /// <summary>
            /// 级别的数字标识
            /// </summary>
            public const byte OrgLevel = 7;
            
            /// <summary>
            /// 级别的实时记录顺序
            /// </summary>
            public const int Real_OrgLevel = 5;

            /// <summary>
            /// 层级的序号的数字标识
            /// </summary>
            public const byte LevelIndex = 8;
            
            /// <summary>
            /// 层级的序号的实时记录顺序
            /// </summary>
            public const int Real_LevelIndex = 6;

            /// <summary>
            /// 上级标识的数字标识
            /// </summary>
            public const byte ParentId = 9;
            
            /// <summary>
            /// 上级标识的实时记录顺序
            /// </summary>
            public const int Real_ParentId = 7;

            /// <summary>
            /// 备注的数字标识
            /// </summary>
            public const byte Memo = 11;
            
            /// <summary>
            /// 备注的实时记录顺序
            /// </summary>
            public const int Real_Memo = 8;

            /// <summary>
            /// 冻结更新的数字标识
            /// </summary>
            public const byte IsFreeze = 12;
            
            /// <summary>
            /// 冻结更新的实时记录顺序
            /// </summary>
            public const int Real_IsFreeze = 9;

            /// <summary>
            /// 数据状态的数字标识
            /// </summary>
            public const byte DataState = 13;
            
            /// <summary>
            /// 数据状态的实时记录顺序
            /// </summary>
            public const int Real_DataState = 10;

            /// <summary>
            /// 制作时间的数字标识
            /// </summary>
            public const byte AddDate = 14;
            
            /// <summary>
            /// 制作时间的实时记录顺序
            /// </summary>
            public const int Real_AddDate = 11;

            /// <summary>
            /// 最后修改者的数字标识
            /// </summary>
            public const byte LastReviserId = 15;
            
            /// <summary>
            /// 最后修改者的实时记录顺序
            /// </summary>
            public const int Real_LastReviserId = 12;

            /// <summary>
            /// 最后修改日期的数字标识
            /// </summary>
            public const byte LastModifyDate = 16;
            
            /// <summary>
            /// 最后修改日期的实时记录顺序
            /// </summary>
            public const int Real_LastModifyDate = 13;

            /// <summary>
            /// 制作人的数字标识
            /// </summary>
            public const byte AuthorId = 17;
            
            /// <summary>
            /// 制作人的实时记录顺序
            /// </summary>
            public const int Real_AuthorId = 14;

            /// <summary>
            /// 实体结构
            /// </summary>
            public static readonly EntitySturct Struct = new EntitySturct
            {
                EntityName = EntityName,
                Caption    = EntityCaption,
                Description= EntityDescription,
                PrimaryKey = EntityPrimaryKey,
                EntityType = EntityIdentity,
                Properties = new Dictionary<int, PropertySturct>
                {
                    {
                        Real_Id,
                        new PropertySturct
                        {
                            Index        = Id,
                            Name         = "Id",
                            Title        = "标识",
                            Caption      = @"标识",
                            Description  = @"标题",
                            ColumnName   = "id",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Code,
                        new PropertySturct
                        {
                            Index        = Code,
                            Name         = "Code",
                            Title        = "编码",
                            Caption      = @"编码",
                            Description  = @"代码",
                            ColumnName   = "code",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_FullName,
                        new PropertySturct
                        {
                            Index        = FullName,
                            Name         = "FullName",
                            Title        = "全称",
                            Caption      = @"全称",
                            Description  = @"全称",
                            ColumnName   = "full_name",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_ShortName,
                        new PropertySturct
                        {
                            Index        = ShortName,
                            Name         = "ShortName",
                            Title        = "简称",
                            Caption      = @"简称",
                            Description  = @"短名称",
                            ColumnName   = "short_name",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_TreeName,
                        new PropertySturct
                        {
                            Index        = TreeName,
                            Name         = "TreeName",
                            Title        = "树形名称",
                            Caption      = @"树形名称",
                            Description  = @"树的名字",
                            ColumnName   = "tree_name",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_OrgLevel,
                        new PropertySturct
                        {
                            Index        = OrgLevel,
                            Name         = "OrgLevel",
                            Title        = "级别",
                            Caption      = @"级别",
                            Description  = @"级别",
                            ColumnName   = "org_level",
                            PropertyType = typeof(int),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_LevelIndex,
                        new PropertySturct
                        {
                            Index        = LevelIndex,
                            Name         = "LevelIndex",
                            Title        = "层级的序号",
                            Caption      = @"层级的序号",
                            Description  = @"层级的序号",
                            ColumnName   = "level_index",
                            PropertyType = typeof(int),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_ParentId,
                        new PropertySturct
                        {
                            Index        = ParentId,
                            Name         = "ParentId",
                            Title        = "上级标识",
                            Caption      = @"上级标识",
                            Description  = @"上级标识",
                            ColumnName   = "parent_id",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Memo,
                        new PropertySturct
                        {
                            Index        = Memo,
                            Name         = "Memo",
                            Title        = "备注",
                            Caption      = @"备注",
                            Description  = @"备忘录",
                            ColumnName   = "memo",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_IsFreeze,
                        new PropertySturct
                        {
                            Index        = IsFreeze,
                            Name         = "IsFreeze",
                            Title        = "冻结更新",
                            Caption      = @"冻结更新",
                            Description  = @"无论在什么数据状态,一旦设置且保存后,数据将不再允许执行Update的操作,作为Update的统一开关.取消的方法是单独设置这个字段的值",
                            ColumnName   = "is_freeze",
                            PropertyType = typeof(bool),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_DataState,
                        new PropertySturct
                        {
                            Index        = DataState,
                            Name         = "DataState",
                            Title        = "数据状态",
                            Caption      = @"数据状态",
                            Description  = @"数据状态",
                            ColumnName   = "data_state",
                            PropertyType = typeof(DataStateType),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_AddDate,
                        new PropertySturct
                        {
                            Index        = AddDate,
                            Name         = "AddDate",
                            Title        = "制作时间",
                            Caption      = @"制作时间",
                            Description  = @"制作时间",
                            ColumnName   = "add_date",
                            PropertyType = typeof(DateTime),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_LastReviserId,
                        new PropertySturct
                        {
                            Index        = LastReviserId,
                            Name         = "LastReviserId",
                            Title        = "最后修改者",
                            Caption      = @"最后修改者",
                            Description  = @"最后修改者",
                            ColumnName   = "last_reviser_id",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_LastModifyDate,
                        new PropertySturct
                        {
                            Index        = LastModifyDate,
                            Name         = "LastModifyDate",
                            Title        = "最后修改日期",
                            Caption      = @"最后修改日期",
                            Description  = @"最后修改日期",
                            ColumnName   = "last_modify_date",
                            PropertyType = typeof(DateTime),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_AuthorId,
                        new PropertySturct
                        {
                            Index        = AuthorId,
                            Name         = "AuthorId",
                            Title        = "制作人",
                            Caption      = @"制作人",
                            Description  = @"制作人",
                            ColumnName   = "author_id",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    }
                }
            };
        }
        #endregion

    }
}