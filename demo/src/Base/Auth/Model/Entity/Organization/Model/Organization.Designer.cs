/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/2 23:21:22*/
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
    /// 机构
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class OrganizationData : IStateData , IHistoryData , IAuditData , IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public OrganizationData()
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
        [DataMember , JsonProperty("Id", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"标识")]
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
        /// 机构类型
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public OrganizationType _type;

        partial void OnTypeGet();

        partial void OnTypeSet(ref OrganizationType value);

        partial void OnTypeSeted();

        
        /// <summary>
        /// 机构类型
        /// </summary>
        /// <remarks>
        /// 类型
        /// </remarks>
        [DataMember , JsonProperty("Type", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"机构类型")]
        public  OrganizationType Type
        {
            get
            {
                OnTypeGet();
                return this._type;
            }
            set
            {
                if(this._type == value)
                    return;
                OnTypeSet(ref value);
                this._type = value;
                OnTypeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Type);
            }
        }
        /// <summary>
        /// 机构类型的可读内容
        /// </summary>
        [IgnoreDataMember,JsonIgnore,DisplayName("机构类型")]
        public string Type_Content => Type.ToCaption();

        /// <summary>
        /// 机构类型的数字属性
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public  int Type_Number
        {
            get => (int)this.Type;
            set => this.Type = (OrganizationType)value;
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
        /// 可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Code", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"编码")]
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
        /// 不能为空.可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataMember , JsonProperty("FullName", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"全称")]
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
        /// 不能为空.可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataMember , JsonProperty("ShortName", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"简称")]
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
        /// 可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("TreeName", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"树形名称")]
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
        [DataMember , JsonProperty("OrgLevel", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"级别")]
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
        [DataMember , JsonProperty("LevelIndex", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"层级的序号")]
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
        /// 边界机构标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _boundaryId;

        partial void OnBoundaryIdGet();

        partial void OnBoundaryIdSet(ref long value);

        partial void OnBoundaryIdSeted();

        
        /// <summary>
        /// 边界机构标识
        /// </summary>
        /// <remarks>
        /// 边界机构标识：不互相访问的锁定标识。如公司独立管理，公司间不互相访问则设置为公司的标识，部门不独立管理则不可设置为部门标识。
        /// </remarks>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("OrgId", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"边界机构标识")]
        public  long BoundaryId
        {
            get
            {
                OnBoundaryIdGet();
                return this._boundaryId;
            }
            set
            {
                if(this._boundaryId == value)
                    return;
                OnBoundaryIdSet(ref value);
                this._boundaryId = value;
                OnBoundaryIdSeted();
                this.OnPropertyChanged(_DataStruct_.Real_BoundaryId);
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
        [DataMember , JsonProperty("Memo", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"备注")]
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
        /// <summary>
        /// 审核人
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _auditorId;

        partial void OnAuditorIdGet();

        partial void OnAuditorIdSet(ref long value);

        partial void OnAuditorIdSeted();

        
        /// <summary>
        /// 审核人
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("auditorId", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"审核人")]
        public  long AuditorId
        {
            get
            {
                OnAuditorIdGet();
                return this._auditorId;
            }
            set
            {
                if(this._auditorId == value)
                    return;
                OnAuditorIdSet(ref value);
                this._auditorId = value;
                OnAuditorIdSeted();
                this.OnPropertyChanged(_DataStruct_.Real_AuditorId);
            }
        }
        /// <summary>
        /// 审核时间
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public DateTime _auditDate;

        partial void OnAuditDateGet();

        partial void OnAuditDateSet(ref DateTime value);

        partial void OnAuditDateSeted();

        
        /// <summary>
        /// 审核时间
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("auditDate", NullValueHandling = NullValueHandling.Ignore) , JsonConverter(typeof(MyDateTimeConverter)) , DisplayName(@"审核时间")]
        public  DateTime AuditDate
        {
            get
            {
                OnAuditDateGet();
                return this._auditDate;
            }
            set
            {
                if(this._auditDate == value)
                    return;
                OnAuditDateSet(ref value);
                this._auditDate = value;
                OnAuditDateSeted();
                this.OnPropertyChanged(_DataStruct_.Real_AuditDate);
            }
        }
        /// <summary>
        /// 审核状态
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public AuditStateType _auditState;

        partial void OnAuditStateGet();

        partial void OnAuditStateSet(ref AuditStateType value);

        partial void OnAuditStateSeted();

        
        /// <summary>
        /// 审核状态
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("auditState", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"审核状态")]
        public  AuditStateType AuditState
        {
            get
            {
                OnAuditStateGet();
                return this._auditState;
            }
            set
            {
                if(this._auditState == value)
                    return;
                OnAuditStateSet(ref value);
                this._auditState = value;
                OnAuditStateSeted();
                this.OnPropertyChanged(_DataStruct_.Real_AuditState);
            }
        }
        /// <summary>
        /// 审核状态的可读内容
        /// </summary>
        [IgnoreDataMember,JsonIgnore,DisplayName("审核状态")]
        public string AuditState_Content => AuditState.ToCaption();

        /// <summary>
        /// 审核状态的数字属性
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public  int AuditState_Number
        {
            get => (int)this.AuditState;
            set => this.AuditState = (AuditStateType)value;
        }
        /// <summary>
        /// 上级机构代码
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _superOrgcode;

        partial void OnSuperOrgcodeGet();

        partial void OnSuperOrgcodeSet(ref string value);

        partial void OnSuperOrgcodeSeted();

        
        /// <summary>
        /// 上级机构代码
        /// </summary>
        /// <remarks>
        /// 无上级机构时,传当前注册管理机构代码,允许更新
        /// </remarks>
        /// <value>
        /// 可存储10个字符.合理长度应不大于10.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("super_orgcode", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"上级机构代码")]
        public  string SuperOrgcode
        {
            get
            {
                OnSuperOrgcodeGet();
                return this._superOrgcode;
            }
            set
            {
                if(this._superOrgcode == value)
                    return;
                OnSuperOrgcodeSet(ref value);
                this._superOrgcode = value;
                OnSuperOrgcodeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_SuperOrgcode);
            }
        }
        /// <summary>
        /// 注册管理机构代码
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _managOrgcode;

        partial void OnManagOrgcodeGet();

        partial void OnManagOrgcodeSet(ref string value);

        partial void OnManagOrgcodeSeted();

        
        /// <summary>
        /// 注册管理机构代码
        /// </summary>
        /// <value>
        /// 可存储10个字符.合理长度应不大于10.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("manag_orgcode", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"注册管理机构代码")]
        public  string ManagOrgcode
        {
            get
            {
                OnManagOrgcodeGet();
                return this._managOrgcode;
            }
            set
            {
                if(this._managOrgcode == value)
                    return;
                OnManagOrgcodeSet(ref value);
                this._managOrgcode = value;
                OnManagOrgcodeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_ManagOrgcode);
            }
        }
        /// <summary>
        /// 注册管理机构名称
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _managOrgname;

        partial void OnManagOrgnameGet();

        partial void OnManagOrgnameSet(ref string value);

        partial void OnManagOrgnameSeted();

        
        /// <summary>
        /// 注册管理机构名称
        /// </summary>
        /// <value>
        /// 可存储50个字符.合理长度应不大于50.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("manag_orgname", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"注册管理机构名称")]
        public  string ManagOrgname
        {
            get
            {
                OnManagOrgnameGet();
                return this._managOrgname;
            }
            set
            {
                if(this._managOrgname == value)
                    return;
                OnManagOrgnameSet(ref value);
                this._managOrgname = value;
                OnManagOrgnameSeted();
                this.OnPropertyChanged(_DataStruct_.Real_ManagOrgname);
            }
        }
        /// <summary>
        /// 所在市级编码
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _cityCode;

        partial void OnCityCodeGet();

        partial void OnCityCodeSet(ref string value);

        partial void OnCityCodeSeted();

        
        /// <summary>
        /// 所在市级编码
        /// </summary>
        /// <remarks>
        /// 参照数据字典【行政区划】,允许更新
        /// </remarks>
        /// <value>
        /// 可存储6个字符.合理长度应不大于6.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("city_code", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"所在市级编码")]
        public  string CityCode
        {
            get
            {
                OnCityCodeGet();
                return this._cityCode;
            }
            set
            {
                if(this._cityCode == value)
                    return;
                OnCityCodeSet(ref value);
                this._cityCode = value;
                OnCityCodeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_CityCode);
            }
        }
        /// <summary>
        /// 所在区县编码
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _districtCode;

        partial void OnDistrictCodeGet();

        partial void OnDistrictCodeSet(ref string value);

        partial void OnDistrictCodeSeted();

        
        /// <summary>
        /// 所在区县编码
        /// </summary>
        /// <remarks>
        /// 允许更新
        /// </remarks>
        /// <value>
        /// 可存储6个字符.合理长度应不大于6.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("district_code", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"所在区县编码")]
        public  string DistrictCode
        {
            get
            {
                OnDistrictCodeGet();
                return this._districtCode;
            }
            set
            {
                if(this._districtCode == value)
                    return;
                OnDistrictCodeSet(ref value);
                this._districtCode = value;
                OnDistrictCodeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_DistrictCode);
            }
        }
        /// <summary>
        /// 机构详细地址
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _orgAddress;

        partial void OnOrgAddressGet();

        partial void OnOrgAddressSet(ref string value);

        partial void OnOrgAddressSeted();

        
        /// <summary>
        /// 机构详细地址
        /// </summary>
        /// <remarks>
        /// 允许更新
        /// </remarks>
        /// <value>
        /// 可存储128个字符.合理长度应不大于128.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("org_address", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"机构详细地址")]
        public  string OrgAddress
        {
            get
            {
                OnOrgAddressGet();
                return this._orgAddress;
            }
            set
            {
                if(this._orgAddress == value)
                    return;
                OnOrgAddressSet(ref value);
                this._orgAddress = value;
                OnOrgAddressSeted();
                this.OnPropertyChanged(_DataStruct_.Real_OrgAddress);
            }
        }
        /// <summary>
        /// 机构负责人
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _lawPersonname;

        partial void OnLawPersonnameGet();

        partial void OnLawPersonnameSet(ref string value);

        partial void OnLawPersonnameSeted();

        
        /// <summary>
        /// 机构负责人
        /// </summary>
        /// <remarks>
        /// 允许更新
        /// </remarks>
        /// <value>
        /// 可存储50个字符.合理长度应不大于50.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("law_personname", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"机构负责人")]
        public  string LawPersonname
        {
            get
            {
                OnLawPersonnameGet();
                return this._lawPersonname;
            }
            set
            {
                if(this._lawPersonname == value)
                    return;
                OnLawPersonnameSet(ref value);
                this._lawPersonname = value;
                OnLawPersonnameSeted();
                this.OnPropertyChanged(_DataStruct_.Real_LawPersonname);
            }
        }
        /// <summary>
        /// 机构负责人电话
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _lawPersontel;

        partial void OnLawPersontelGet();

        partial void OnLawPersontelSet(ref string value);

        partial void OnLawPersontelSeted();

        
        /// <summary>
        /// 机构负责人电话
        /// </summary>
        /// <remarks>
        /// 允许更新
        /// </remarks>
        /// <value>
        /// 可存储20个字符.合理长度应不大于20.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("law_persontel", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"机构负责人电话")]
        public  string LawPersontel
        {
            get
            {
                OnLawPersontelGet();
                return this._lawPersontel;
            }
            set
            {
                if(this._lawPersontel == value)
                    return;
                OnLawPersontelSet(ref value);
                this._lawPersontel = value;
                OnLawPersontelSeted();
                this.OnPropertyChanged(_DataStruct_.Real_LawPersontel);
            }
        }
        /// <summary>
        /// 机构联系人
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _contactName;

        partial void OnContactNameGet();

        partial void OnContactNameSet(ref string value);

        partial void OnContactNameSeted();

        
        /// <summary>
        /// 机构联系人
        /// </summary>
        /// <remarks>
        /// 允许更新
        /// </remarks>
        /// <value>
        /// 可存储50个字符.合理长度应不大于50.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("contact_name", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"机构联系人")]
        public  string ContactName
        {
            get
            {
                OnContactNameGet();
                return this._contactName;
            }
            set
            {
                if(this._contactName == value)
                    return;
                OnContactNameSet(ref value);
                this._contactName = value;
                OnContactNameSeted();
                this.OnPropertyChanged(_DataStruct_.Real_ContactName);
            }
        }
        /// <summary>
        /// 机构联系人电话
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _contactTel;

        partial void OnContactTelGet();

        partial void OnContactTelSet(ref string value);

        partial void OnContactTelSeted();

        
        /// <summary>
        /// 机构联系人电话
        /// </summary>
        /// <remarks>
        /// 允许更新
        /// </remarks>
        /// <value>
        /// 可存储20个字符.合理长度应不大于20.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("contact_tel", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"机构联系人电话")]
        public  string ContactTel
        {
            get
            {
                OnContactTelGet();
                return this._contactTel;
            }
            set
            {
                if(this._contactTel == value)
                    return;
                OnContactTelSet(ref value);
                this._contactTel = value;
                OnContactTelSeted();
                this.OnPropertyChanged(_DataStruct_.Real_ContactTel);
            }
        }
        /// <summary>
        /// 行政区域外键
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _areaId;

        partial void OnAreaIdGet();

        partial void OnAreaIdSet(ref long value);

        partial void OnAreaIdSeted();

        
        /// <summary>
        /// 行政区域外键
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("areaId", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"行政区域外键")]
        public  long AreaId
        {
            get
            {
                OnAreaIdGet();
                return this._areaId;
            }
            set
            {
                if(this._areaId == value)
                    return;
                OnAreaIdSet(ref value);
                this._areaId = value;
                OnAreaIdSeted();
                this.OnPropertyChanged(_DataStruct_.Real_AreaId);
            }
        }
        /// <summary>
        /// 行政区域
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _area;

        
        /// <summary>
        /// 行政区域
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("area", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"行政区域")]
        public  string Area
        {
            get
            {
                return this._area;
            }
            set
            {
                this._area = value;
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
            case "type":
                if (value != null)
                {
                    if(value is int)
                    {
                        this.Type = (OrganizationType)(int)value;
                    }
                    else if(value is OrganizationType)
                    {
                        this.Type = (OrganizationType)value;
                    }
                    else
                    {
                        var str = value.ToString();
                        OrganizationType val;
                        if (OrganizationType.TryParse(str, out val))
                        {
                            this.Type = val;
                        }
                        else
                        {
                            int vl;
                            if (int.TryParse(str, out vl))
                            {
                                this.Type = (OrganizationType)vl;
                            }
                        }
                    }
                }
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
            case "boundaryid":
                this.BoundaryId = (long)Convert.ToDecimal(value);
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
            case "auditorid":
                this.AuditorId = (long)Convert.ToDecimal(value);
                return;
            case "auditdate":
                this.AuditDate = Convert.ToDateTime(value);
                return;
            case "auditstate":
                if (value != null)
                {
                    if(value is int)
                    {
                        this.AuditState = (AuditStateType)(int)value;
                    }
                    else if(value is AuditStateType)
                    {
                        this.AuditState = (AuditStateType)value;
                    }
                    else
                    {
                        var str = value.ToString();
                        AuditStateType val;
                        if (AuditStateType.TryParse(str, out val))
                        {
                            this.AuditState = val;
                        }
                        else
                        {
                            int vl;
                            if (int.TryParse(str, out vl))
                            {
                                this.AuditState = (AuditStateType)vl;
                            }
                        }
                    }
                }
                return;
            case "superorgcode":
                this.SuperOrgcode = value == null ? null : value.ToString();
                return;
            case "managorgcode":
                this.ManagOrgcode = value == null ? null : value.ToString();
                return;
            case "managorgname":
                this.ManagOrgname = value == null ? null : value.ToString();
                return;
            case "citycode":
                this.CityCode = value == null ? null : value.ToString();
                return;
            case "districtcode":
                this.DistrictCode = value == null ? null : value.ToString();
                return;
            case "orgaddress":
                this.OrgAddress = value == null ? null : value.ToString();
                return;
            case "lawpersonname":
                this.LawPersonname = value == null ? null : value.ToString();
                return;
            case "lawpersontel":
                this.LawPersontel = value == null ? null : value.ToString();
                return;
            case "contactname":
                this.ContactName = value == null ? null : value.ToString();
                return;
            case "contacttel":
                this.ContactTel = value == null ? null : value.ToString();
                return;
            case "areaid":
                this.AreaId = (long)Convert.ToDecimal(value);
                return;
            case "area":
                this.Area = value == null ? null : value.ToString();
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
            case _DataStruct_.Type:
                this.Type = (OrganizationType)value;
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
            case _DataStruct_.BoundaryId:
                this.BoundaryId = Convert.ToInt64(value);
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
            case _DataStruct_.AuditorId:
                this.AuditorId = Convert.ToInt64(value);
                return;
            case _DataStruct_.AuditDate:
                this.AuditDate = Convert.ToDateTime(value);
                return;
            case _DataStruct_.AuditState:
                this.AuditState = (AuditStateType)value;
                return;
            case _DataStruct_.SuperOrgcode:
                this.SuperOrgcode = value == null ? null : value.ToString();
                return;
            case _DataStruct_.ManagOrgcode:
                this.ManagOrgcode = value == null ? null : value.ToString();
                return;
            case _DataStruct_.ManagOrgname:
                this.ManagOrgname = value == null ? null : value.ToString();
                return;
            case _DataStruct_.CityCode:
                this.CityCode = value == null ? null : value.ToString();
                return;
            case _DataStruct_.DistrictCode:
                this.DistrictCode = value == null ? null : value.ToString();
                return;
            case _DataStruct_.OrgAddress:
                this.OrgAddress = value == null ? null : value.ToString();
                return;
            case _DataStruct_.LawPersonname:
                this.LawPersonname = value == null ? null : value.ToString();
                return;
            case _DataStruct_.LawPersontel:
                this.LawPersontel = value == null ? null : value.ToString();
                return;
            case _DataStruct_.ContactName:
                this.ContactName = value == null ? null : value.ToString();
                return;
            case _DataStruct_.ContactTel:
                this.ContactTel = value == null ? null : value.ToString();
                return;
            case _DataStruct_.AreaId:
                this.AreaId = Convert.ToInt64(value);
                return;
            case _DataStruct_.Area:
                this.Area = value == null ? null : value.ToString();
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
            case "type":
                return this.Type.ToCaption();
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
            case "boundaryid":
                return this.BoundaryId;
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
            case "auditorid":
                return this.AuditorId;
            case "auditdate":
                return this.AuditDate;
            case "auditstate":
                return this.AuditState.ToCaption();
            case "superorgcode":
                return this.SuperOrgcode;
            case "managorgcode":
                return this.ManagOrgcode;
            case "managorgname":
                return this.ManagOrgname;
            case "citycode":
                return this.CityCode;
            case "districtcode":
                return this.DistrictCode;
            case "orgaddress":
                return this.OrgAddress;
            case "lawpersonname":
                return this.LawPersonname;
            case "lawpersontel":
                return this.LawPersontel;
            case "contactname":
                return this.ContactName;
            case "contacttel":
                return this.ContactTel;
            case "areaid":
                return this.AreaId;
            case "area":
                return this.Area;
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
                case _DataStruct_.Type:
                    return this.Type;
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
                case _DataStruct_.BoundaryId:
                    return this.BoundaryId;
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
                case _DataStruct_.AuditorId:
                    return this.AuditorId;
                case _DataStruct_.AuditDate:
                    return this.AuditDate;
                case _DataStruct_.AuditState:
                    return this.AuditState;
                case _DataStruct_.SuperOrgcode:
                    return this.SuperOrgcode;
                case _DataStruct_.ManagOrgcode:
                    return this.ManagOrgcode;
                case _DataStruct_.ManagOrgname:
                    return this.ManagOrgname;
                case _DataStruct_.CityCode:
                    return this.CityCode;
                case _DataStruct_.DistrictCode:
                    return this.DistrictCode;
                case _DataStruct_.OrgAddress:
                    return this.OrgAddress;
                case _DataStruct_.LawPersonname:
                    return this.LawPersonname;
                case _DataStruct_.LawPersontel:
                    return this.LawPersontel;
                case _DataStruct_.ContactName:
                    return this.ContactName;
                case _DataStruct_.ContactTel:
                    return this.ContactTel;
                case _DataStruct_.AreaId:
                    return this.AreaId;
                case _DataStruct_.Area:
                    return this.Area;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(OrganizationData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as OrganizationData;
            if(sourceEntity == null)
                return;
            this._id = sourceEntity._id;
            this._type = sourceEntity._type;
            this._code = sourceEntity._code;
            this._fullName = sourceEntity._fullName;
            this._shortName = sourceEntity._shortName;
            this._treeName = sourceEntity._treeName;
            this._orgLevel = sourceEntity._orgLevel;
            this._levelIndex = sourceEntity._levelIndex;
            this._parentId = sourceEntity._parentId;
            this._boundaryId = sourceEntity._boundaryId;
            this._memo = sourceEntity._memo;
            this._isFreeze = sourceEntity._isFreeze;
            this._dataState = sourceEntity._dataState;
            this._addDate = sourceEntity._addDate;
            this._lastReviserId = sourceEntity._lastReviserId;
            this._lastModifyDate = sourceEntity._lastModifyDate;
            this._authorId = sourceEntity._authorId;
            this._auditorId = sourceEntity._auditorId;
            this._auditDate = sourceEntity._auditDate;
            this._auditState = sourceEntity._auditState;
            this._superOrgcode = sourceEntity._superOrgcode;
            this._managOrgcode = sourceEntity._managOrgcode;
            this._managOrgname = sourceEntity._managOrgname;
            this._cityCode = sourceEntity._cityCode;
            this._districtCode = sourceEntity._districtCode;
            this._orgAddress = sourceEntity._orgAddress;
            this._lawPersonname = sourceEntity._lawPersonname;
            this._lawPersontel = sourceEntity._lawPersontel;
            this._contactName = sourceEntity._contactName;
            this._contactTel = sourceEntity._contactTel;
            this._areaId = sourceEntity._areaId;
            this._area = sourceEntity._area;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(OrganizationData source)
        {
                this.Id = source.Id;
                this.Type = source.Type;
                this.Code = source.Code;
                this.FullName = source.FullName;
                this.ShortName = source.ShortName;
                this.TreeName = source.TreeName;
                this.OrgLevel = source.OrgLevel;
                this.LevelIndex = source.LevelIndex;
                this.ParentId = source.ParentId;
                this.BoundaryId = source.BoundaryId;
                this.Memo = source.Memo;
                this.IsFreeze = source.IsFreeze;
                this.DataState = source.DataState;
                this.AddDate = source.AddDate;
                this.LastReviserId = source.LastReviserId;
                this.LastModifyDate = source.LastModifyDate;
                this.AuthorId = source.AuthorId;
                this.AuditorId = source.AuditorId;
                this.AuditDate = source.AuditDate;
                this.AuditState = source.AuditState;
                this.SuperOrgcode = source.SuperOrgcode;
                this.ManagOrgcode = source.ManagOrgcode;
                this.ManagOrgname = source.ManagOrgname;
                this.CityCode = source.CityCode;
                this.DistrictCode = source.DistrictCode;
                this.OrgAddress = source.OrgAddress;
                this.LawPersonname = source.LawPersonname;
                this.LawPersontel = source.LawPersontel;
                this.ContactName = source.ContactName;
                this.ContactTel = source.ContactTel;
                this.AreaId = source.AreaId;
                this.Area = source.Area;
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
                OnTypeModified(subsist,false);
                OnCodeModified(subsist,false);
                OnFullNameModified(subsist,false);
                OnShortNameModified(subsist,false);
                OnTreeNameModified(subsist,false);
                OnOrgLevelModified(subsist,false);
                OnLevelIndexModified(subsist,false);
                OnParentIdModified(subsist,false);
                OnBoundaryIdModified(subsist,false);
                OnMemoModified(subsist,false);
                OnIsFreezeModified(subsist,false);
                OnDataStateModified(subsist,false);
                OnAddDateModified(subsist,false);
                OnLastReviserIdModified(subsist,false);
                OnLastModifyDateModified(subsist,false);
                OnAuthorIdModified(subsist,false);
                OnAuditorIdModified(subsist,false);
                OnAuditDateModified(subsist,false);
                OnAuditStateModified(subsist,false);
                OnSuperOrgcodeModified(subsist,false);
                OnManagOrgcodeModified(subsist,false);
                OnManagOrgnameModified(subsist,false);
                OnCityCodeModified(subsist,false);
                OnDistrictCodeModified(subsist,false);
                OnOrgAddressModified(subsist,false);
                OnLawPersonnameModified(subsist,false);
                OnLawPersontelModified(subsist,false);
                OnContactNameModified(subsist,false);
                OnContactTelModified(subsist,false);
                OnAreaIdModified(subsist,false);
                OnAreaModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnIdModified(subsist,true);
                OnTypeModified(subsist,true);
                OnCodeModified(subsist,true);
                OnFullNameModified(subsist,true);
                OnShortNameModified(subsist,true);
                OnTreeNameModified(subsist,true);
                OnOrgLevelModified(subsist,true);
                OnLevelIndexModified(subsist,true);
                OnParentIdModified(subsist,true);
                OnBoundaryIdModified(subsist,true);
                OnMemoModified(subsist,true);
                OnIsFreezeModified(subsist,true);
                OnDataStateModified(subsist,true);
                OnAddDateModified(subsist,true);
                OnLastReviserIdModified(subsist,true);
                OnLastModifyDateModified(subsist,true);
                OnAuthorIdModified(subsist,true);
                OnAuditorIdModified(subsist,true);
                OnAuditDateModified(subsist,true);
                OnAuditStateModified(subsist,true);
                OnSuperOrgcodeModified(subsist,true);
                OnManagOrgcodeModified(subsist,true);
                OnManagOrgnameModified(subsist,true);
                OnCityCodeModified(subsist,true);
                OnDistrictCodeModified(subsist,true);
                OnOrgAddressModified(subsist,true);
                OnLawPersonnameModified(subsist,true);
                OnLawPersontelModified(subsist,true);
                OnContactNameModified(subsist,true);
                OnContactTelModified(subsist,true);
                OnAreaIdModified(subsist,true);
                OnAreaModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[32] > 0)
            {
                OnIdModified(subsist,modifieds[_DataStruct_.Real_Id] == 1);
                OnTypeModified(subsist,modifieds[_DataStruct_.Real_Type] == 1);
                OnCodeModified(subsist,modifieds[_DataStruct_.Real_Code] == 1);
                OnFullNameModified(subsist,modifieds[_DataStruct_.Real_FullName] == 1);
                OnShortNameModified(subsist,modifieds[_DataStruct_.Real_ShortName] == 1);
                OnTreeNameModified(subsist,modifieds[_DataStruct_.Real_TreeName] == 1);
                OnOrgLevelModified(subsist,modifieds[_DataStruct_.Real_OrgLevel] == 1);
                OnLevelIndexModified(subsist,modifieds[_DataStruct_.Real_LevelIndex] == 1);
                OnParentIdModified(subsist,modifieds[_DataStruct_.Real_ParentId] == 1);
                OnBoundaryIdModified(subsist,modifieds[_DataStruct_.Real_BoundaryId] == 1);
                OnMemoModified(subsist,modifieds[_DataStruct_.Real_Memo] == 1);
                OnIsFreezeModified(subsist,modifieds[_DataStruct_.Real_IsFreeze] == 1);
                OnDataStateModified(subsist,modifieds[_DataStruct_.Real_DataState] == 1);
                OnAddDateModified(subsist,modifieds[_DataStruct_.Real_AddDate] == 1);
                OnLastReviserIdModified(subsist,modifieds[_DataStruct_.Real_LastReviserId] == 1);
                OnLastModifyDateModified(subsist,modifieds[_DataStruct_.Real_LastModifyDate] == 1);
                OnAuthorIdModified(subsist,modifieds[_DataStruct_.Real_AuthorId] == 1);
                OnAuditorIdModified(subsist,modifieds[_DataStruct_.Real_AuditorId] == 1);
                OnAuditDateModified(subsist,modifieds[_DataStruct_.Real_AuditDate] == 1);
                OnAuditStateModified(subsist,modifieds[_DataStruct_.Real_AuditState] == 1);
                OnSuperOrgcodeModified(subsist,modifieds[_DataStruct_.Real_SuperOrgcode] == 1);
                OnManagOrgcodeModified(subsist,modifieds[_DataStruct_.Real_ManagOrgcode] == 1);
                OnManagOrgnameModified(subsist,modifieds[_DataStruct_.Real_ManagOrgname] == 1);
                OnCityCodeModified(subsist,modifieds[_DataStruct_.Real_CityCode] == 1);
                OnDistrictCodeModified(subsist,modifieds[_DataStruct_.Real_DistrictCode] == 1);
                OnOrgAddressModified(subsist,modifieds[_DataStruct_.Real_OrgAddress] == 1);
                OnLawPersonnameModified(subsist,modifieds[_DataStruct_.Real_LawPersonname] == 1);
                OnLawPersontelModified(subsist,modifieds[_DataStruct_.Real_LawPersontel] == 1);
                OnContactNameModified(subsist,modifieds[_DataStruct_.Real_ContactName] == 1);
                OnContactTelModified(subsist,modifieds[_DataStruct_.Real_ContactTel] == 1);
                OnAreaIdModified(subsist,modifieds[_DataStruct_.Real_AreaId] == 1);
                OnAreaModified(subsist,modifieds[_DataStruct_.Real_Area] == 1);
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
        /// 机构类型修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnTypeModified(EntitySubsist subsist,bool isModified);

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
        /// 边界机构标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnBoundaryIdModified(EntitySubsist subsist,bool isModified);

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

        /// <summary>
        /// 审核人修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAuditorIdModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 审核时间修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAuditDateModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 审核状态修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAuditStateModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 上级机构代码修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnSuperOrgcodeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 注册管理机构代码修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnManagOrgcodeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 注册管理机构名称修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnManagOrgnameModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 所在市级编码修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnCityCodeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 所在区县编码修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnDistrictCodeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 机构详细地址修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOrgAddressModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 机构负责人修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnLawPersonnameModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 机构负责人电话修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnLawPersontelModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 机构联系人修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnContactNameModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 机构联系人电话修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnContactTelModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 行政区域外键修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAreaIdModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 行政区域修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAreaModified(EntitySubsist subsist,bool isModified);
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
            public const string EntityName = @"Organization";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"机构";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"机构";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0x20002;
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
            /// 机构类型的数字标识
            /// </summary>
            public const byte Type = 2;
            
            /// <summary>
            /// 机构类型的实时记录顺序
            /// </summary>
            public const int Real_Type = 1;

            /// <summary>
            /// 编码的数字标识
            /// </summary>
            public const byte Code = 3;
            
            /// <summary>
            /// 编码的实时记录顺序
            /// </summary>
            public const int Real_Code = 2;

            /// <summary>
            /// 全称的数字标识
            /// </summary>
            public const byte FullName = 4;
            
            /// <summary>
            /// 全称的实时记录顺序
            /// </summary>
            public const int Real_FullName = 3;

            /// <summary>
            /// 简称的数字标识
            /// </summary>
            public const byte ShortName = 5;
            
            /// <summary>
            /// 简称的实时记录顺序
            /// </summary>
            public const int Real_ShortName = 4;

            /// <summary>
            /// 树形名称的数字标识
            /// </summary>
            public const byte TreeName = 6;
            
            /// <summary>
            /// 树形名称的实时记录顺序
            /// </summary>
            public const int Real_TreeName = 5;

            /// <summary>
            /// 级别的数字标识
            /// </summary>
            public const byte OrgLevel = 7;
            
            /// <summary>
            /// 级别的实时记录顺序
            /// </summary>
            public const int Real_OrgLevel = 6;

            /// <summary>
            /// 层级的序号的数字标识
            /// </summary>
            public const byte LevelIndex = 20;
            
            /// <summary>
            /// 层级的序号的实时记录顺序
            /// </summary>
            public const int Real_LevelIndex = 7;

            /// <summary>
            /// 上级标识的数字标识
            /// </summary>
            public const byte ParentId = 8;
            
            /// <summary>
            /// 上级标识的实时记录顺序
            /// </summary>
            public const int Real_ParentId = 8;

            /// <summary>
            /// 边界机构标识的数字标识
            /// </summary>
            public const byte BoundaryId = 21;
            
            /// <summary>
            /// 边界机构标识的实时记录顺序
            /// </summary>
            public const int Real_BoundaryId = 9;

            /// <summary>
            /// 备注的数字标识
            /// </summary>
            public const byte Memo = 10;
            
            /// <summary>
            /// 备注的实时记录顺序
            /// </summary>
            public const int Real_Memo = 10;

            /// <summary>
            /// 冻结更新的数字标识
            /// </summary>
            public const byte IsFreeze = 11;
            
            /// <summary>
            /// 冻结更新的实时记录顺序
            /// </summary>
            public const int Real_IsFreeze = 11;

            /// <summary>
            /// 数据状态的数字标识
            /// </summary>
            public const byte DataState = 12;
            
            /// <summary>
            /// 数据状态的实时记录顺序
            /// </summary>
            public const int Real_DataState = 12;

            /// <summary>
            /// 制作时间的数字标识
            /// </summary>
            public const byte AddDate = 13;
            
            /// <summary>
            /// 制作时间的实时记录顺序
            /// </summary>
            public const int Real_AddDate = 13;

            /// <summary>
            /// 最后修改者的数字标识
            /// </summary>
            public const byte LastReviserId = 14;
            
            /// <summary>
            /// 最后修改者的实时记录顺序
            /// </summary>
            public const int Real_LastReviserId = 14;

            /// <summary>
            /// 最后修改日期的数字标识
            /// </summary>
            public const byte LastModifyDate = 15;
            
            /// <summary>
            /// 最后修改日期的实时记录顺序
            /// </summary>
            public const int Real_LastModifyDate = 15;

            /// <summary>
            /// 制作人的数字标识
            /// </summary>
            public const byte AuthorId = 16;
            
            /// <summary>
            /// 制作人的实时记录顺序
            /// </summary>
            public const int Real_AuthorId = 16;

            /// <summary>
            /// 审核人的数字标识
            /// </summary>
            public const byte AuditorId = 17;
            
            /// <summary>
            /// 审核人的实时记录顺序
            /// </summary>
            public const int Real_AuditorId = 17;

            /// <summary>
            /// 审核时间的数字标识
            /// </summary>
            public const byte AuditDate = 18;
            
            /// <summary>
            /// 审核时间的实时记录顺序
            /// </summary>
            public const int Real_AuditDate = 18;

            /// <summary>
            /// 审核状态的数字标识
            /// </summary>
            public const byte AuditState = 19;
            
            /// <summary>
            /// 审核状态的实时记录顺序
            /// </summary>
            public const int Real_AuditState = 19;

            /// <summary>
            /// 上级机构代码的数字标识
            /// </summary>
            public const byte SuperOrgcode = 22;
            
            /// <summary>
            /// 上级机构代码的实时记录顺序
            /// </summary>
            public const int Real_SuperOrgcode = 20;

            /// <summary>
            /// 注册管理机构代码的数字标识
            /// </summary>
            public const byte ManagOrgcode = 23;
            
            /// <summary>
            /// 注册管理机构代码的实时记录顺序
            /// </summary>
            public const int Real_ManagOrgcode = 21;

            /// <summary>
            /// 注册管理机构名称的数字标识
            /// </summary>
            public const byte ManagOrgname = 24;
            
            /// <summary>
            /// 注册管理机构名称的实时记录顺序
            /// </summary>
            public const int Real_ManagOrgname = 22;

            /// <summary>
            /// 所在市级编码的数字标识
            /// </summary>
            public const byte CityCode = 25;
            
            /// <summary>
            /// 所在市级编码的实时记录顺序
            /// </summary>
            public const int Real_CityCode = 23;

            /// <summary>
            /// 所在区县编码的数字标识
            /// </summary>
            public const byte DistrictCode = 26;
            
            /// <summary>
            /// 所在区县编码的实时记录顺序
            /// </summary>
            public const int Real_DistrictCode = 24;

            /// <summary>
            /// 机构详细地址的数字标识
            /// </summary>
            public const byte OrgAddress = 27;
            
            /// <summary>
            /// 机构详细地址的实时记录顺序
            /// </summary>
            public const int Real_OrgAddress = 25;

            /// <summary>
            /// 机构负责人的数字标识
            /// </summary>
            public const byte LawPersonname = 28;
            
            /// <summary>
            /// 机构负责人的实时记录顺序
            /// </summary>
            public const int Real_LawPersonname = 26;

            /// <summary>
            /// 机构负责人电话的数字标识
            /// </summary>
            public const byte LawPersontel = 29;
            
            /// <summary>
            /// 机构负责人电话的实时记录顺序
            /// </summary>
            public const int Real_LawPersontel = 27;

            /// <summary>
            /// 机构联系人的数字标识
            /// </summary>
            public const byte ContactName = 30;
            
            /// <summary>
            /// 机构联系人的实时记录顺序
            /// </summary>
            public const int Real_ContactName = 28;

            /// <summary>
            /// 机构联系人电话的数字标识
            /// </summary>
            public const byte ContactTel = 31;
            
            /// <summary>
            /// 机构联系人电话的实时记录顺序
            /// </summary>
            public const int Real_ContactTel = 29;

            /// <summary>
            /// 行政区域外键的数字标识
            /// </summary>
            public const byte AreaId = 32;
            
            /// <summary>
            /// 行政区域外键的实时记录顺序
            /// </summary>
            public const int Real_AreaId = 30;

            /// <summary>
            /// 行政区域的数字标识
            /// </summary>
            public const byte Area = 33;
            
            /// <summary>
            /// 行政区域的实时记录顺序
            /// </summary>
            public const int Real_Area = 31;

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
                        Real_Type,
                        new PropertySturct
                        {
                            Index        = Type,
                            Name         = "Type",
                            Title        = "机构类型",
                            Caption      = @"机构类型",
                            Description  = @"类型",
                            ColumnName   = "type",
                            PropertyType = typeof(OrganizationType),
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
                        Real_BoundaryId,
                        new PropertySturct
                        {
                            Index        = BoundaryId,
                            Name         = "BoundaryId",
                            Title        = "边界机构标识",
                            Caption      = @"边界机构标识",
                            Description  = @"边界机构标识：不互相访问的锁定标识。如公司独立管理，公司间不互相访问则设置为公司的标识，部门不独立管理则不可设置为部门标识。",
                            ColumnName   = "boundary_id",
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
                    },
                    {
                        Real_AuditorId,
                        new PropertySturct
                        {
                            Index        = AuditorId,
                            Name         = "AuditorId",
                            Title        = "审核人",
                            Caption      = @"审核人",
                            Description  = @"审核人",
                            ColumnName   = "auditor_id",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_AuditDate,
                        new PropertySturct
                        {
                            Index        = AuditDate,
                            Name         = "AuditDate",
                            Title        = "审核时间",
                            Caption      = @"审核时间",
                            Description  = @"审核时间",
                            ColumnName   = "audit_date",
                            PropertyType = typeof(DateTime),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_AuditState,
                        new PropertySturct
                        {
                            Index        = AuditState,
                            Name         = "AuditState",
                            Title        = "审核状态",
                            Caption      = @"审核状态",
                            Description  = @"审核状态",
                            ColumnName   = "audit_state",
                            PropertyType = typeof(AuditStateType),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_SuperOrgcode,
                        new PropertySturct
                        {
                            Index        = SuperOrgcode,
                            Name         = "SuperOrgcode",
                            Title        = "上级机构代码",
                            Caption      = @"上级机构代码",
                            Description  = @"无上级机构时,传当前注册管理机构代码,允许更新",
                            ColumnName   = "super_orgcode",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_ManagOrgcode,
                        new PropertySturct
                        {
                            Index        = ManagOrgcode,
                            Name         = "ManagOrgcode",
                            Title        = "注册管理机构代码",
                            Caption      = @"注册管理机构代码",
                            Description  = @"注册管理机构代码",
                            ColumnName   = "manag_orgcode",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_ManagOrgname,
                        new PropertySturct
                        {
                            Index        = ManagOrgname,
                            Name         = "ManagOrgname",
                            Title        = "注册管理机构名称",
                            Caption      = @"注册管理机构名称",
                            Description  = @"注册管理机构名称",
                            ColumnName   = "manag_orgname",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_CityCode,
                        new PropertySturct
                        {
                            Index        = CityCode,
                            Name         = "CityCode",
                            Title        = "所在市级编码",
                            Caption      = @"所在市级编码",
                            Description  = @"参照数据字典【行政区划】,允许更新",
                            ColumnName   = "city_code",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_DistrictCode,
                        new PropertySturct
                        {
                            Index        = DistrictCode,
                            Name         = "DistrictCode",
                            Title        = "所在区县编码",
                            Caption      = @"所在区县编码",
                            Description  = @"允许更新",
                            ColumnName   = "district_code",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_OrgAddress,
                        new PropertySturct
                        {
                            Index        = OrgAddress,
                            Name         = "OrgAddress",
                            Title        = "机构详细地址",
                            Caption      = @"机构详细地址",
                            Description  = @"允许更新",
                            ColumnName   = "org_address",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_LawPersonname,
                        new PropertySturct
                        {
                            Index        = LawPersonname,
                            Name         = "LawPersonname",
                            Title        = "机构负责人",
                            Caption      = @"机构负责人",
                            Description  = @"允许更新",
                            ColumnName   = "law_personname",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_LawPersontel,
                        new PropertySturct
                        {
                            Index        = LawPersontel,
                            Name         = "LawPersontel",
                            Title        = "机构负责人电话",
                            Caption      = @"机构负责人电话",
                            Description  = @"允许更新",
                            ColumnName   = "law_persontel",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_ContactName,
                        new PropertySturct
                        {
                            Index        = ContactName,
                            Name         = "ContactName",
                            Title        = "机构联系人",
                            Caption      = @"机构联系人",
                            Description  = @"允许更新",
                            ColumnName   = "contact_name",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_ContactTel,
                        new PropertySturct
                        {
                            Index        = ContactTel,
                            Name         = "ContactTel",
                            Title        = "机构联系人电话",
                            Caption      = @"机构联系人电话",
                            Description  = @"允许更新",
                            ColumnName   = "contact_tel",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_AreaId,
                        new PropertySturct
                        {
                            Index        = AreaId,
                            Name         = "AreaId",
                            Title        = "行政区域外键",
                            Caption      = @"行政区域外键",
                            Description  = @"行政区域外键",
                            ColumnName   = "area_id",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Area,
                        new PropertySturct
                        {
                            Index        = Area,
                            Name         = "Area",
                            Title        = "行政区域",
                            Caption      = @"行政区域",
                            Description  = @"行政区域",
                            ColumnName   = "area",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
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