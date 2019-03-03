/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/3 1:24:20*/
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
using Agebull.Common.DataModel;
using Gboxt.Common.DataModel;
using Gboxt.Common.DataModel.Extends;
using Agebull.Common.WebApi;


#endregion

namespace Agebull.Common.OAuth
{
    /// <summary>
    /// 人员职位设置
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class PositionPersonnelData : IStateData , IHistoryData , IAuditData , IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public PositionPersonnelData()
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
        /// 称谓
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _appellation;

        partial void OnAppellationGet();

        partial void OnAppellationSet(ref string value);

        partial void OnAppellationSeted();

        
        /// <summary>
        /// 称谓
        /// </summary>
        /// <value>
        /// 可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Appellation", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"称谓")]
        public  string Appellation
        {
            get
            {
                OnAppellationGet();
                return this._appellation;
            }
            set
            {
                if(this._appellation == value)
                    return;
                OnAppellationSet(ref value);
                this._appellation = value;
                OnAppellationSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Appellation);
            }
        }
        /// <summary>
        /// 姓名
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _realName;

        
        /// <summary>
        /// 姓名
        /// </summary>
        /// <value>
        /// 不能为空.可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataMember , JsonProperty("RealName", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"姓名")]
        public  string RealName
        {
            get
            {
                return this._realName;
            }
            set
            {
                this._realName = value;
            }
        }
        /// <summary>
        /// 员工标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _userId;

        partial void OnUserIdGet();

        partial void OnUserIdSet(ref long value);

        partial void OnUserIdSeted();

        
        /// <summary>
        /// 员工标识
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("UserId", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"员工标识")]
        public  long UserId
        {
            get
            {
                OnUserIdGet();
                return this._userId;
            }
            set
            {
                if(this._userId == value)
                    return;
                OnUserIdSet(ref value);
                this._userId = value;
                OnUserIdSeted();
                this.OnPropertyChanged(_DataStruct_.Real_UserId);
            }
        }
        /// <summary>
        /// 性别
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public SexType _sex;

        
        /// <summary>
        /// 性别
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Sex", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"性别")]
        public  SexType Sex
        {
            get
            {
                return this._sex;
            }
            set
            {
                this._sex = value;
            }
        }
        /// <summary>
        /// 性别的可读内容
        /// </summary>
        [IgnoreDataMember,JsonIgnore,DisplayName("性别")]
        public string Sex_Content => Sex.ToCaption();

        /// <summary>
        /// 性别的数字属性
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public  int Sex_Number
        {
            get => (int)this.Sex;
            set => this.Sex = (SexType)value;
        }
        /// <summary>
        /// 生日
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public DateTime _birthday;

        
        /// <summary>
        /// 生日
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Birthday", NullValueHandling = NullValueHandling.Ignore) , JsonConverter(typeof(MyDateTimeConverter)) , ReadOnly(true) , DisplayName(@"生日")]
        public  DateTime Birthday
        {
            get
            {
                return this._birthday;
            }
            set
            {
                this._birthday = value;
            }
        }
        /// <summary>
        /// 电话
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _tel;

        
        /// <summary>
        /// 电话
        /// </summary>
        /// <value>
        /// 可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Tel", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"电话")]
        public  string Tel
        {
            get
            {
                return this._tel;
            }
            set
            {
                this._tel = value;
            }
        }
        /// <summary>
        /// 手机
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _phoneNumber;

        
        /// <summary>
        /// 手机
        /// </summary>
        /// <value>
        /// 可存储20个字符.合理长度应不大于20.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Mobile", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"手机")]
        public  string PhoneNumber
        {
            get
            {
                return this._phoneNumber;
            }
            set
            {
                this._phoneNumber = value;
            }
        }
        /// <summary>
        /// 角色
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _role;

        
        /// <summary>
        /// 角色
        /// </summary>
        /// <value>
        /// 不能为空.可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataMember , JsonProperty("Role", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"角色")]
        public  string Role
        {
            get
            {
                return this._role;
            }
            set
            {
                this._role = value;
            }
        }
        /// <summary>
        /// 角色外键
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _roleId;

        partial void OnRoleIdGet();

        partial void OnRoleIdSet(ref long value);

        partial void OnRoleIdSeted();

        
        /// <summary>
        /// 角色外键
        /// </summary>
        [DataMember , JsonProperty("RoleId", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"角色外键")]
        public  long RoleId
        {
            get
            {
                OnRoleIdGet();
                return this._roleId;
            }
            set
            {
                if(this._roleId == value)
                    return;
                OnRoleIdSet(ref value);
                this._roleId = value;
                OnRoleIdSeted();
                this.OnPropertyChanged(_DataStruct_.Real_RoleId);
            }
        }
        /// <summary>
        /// 职位标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _organizePositionId;

        partial void OnOrganizePositionIdGet();

        partial void OnOrganizePositionIdSet(ref long value);

        partial void OnOrganizePositionIdSeted();

        
        /// <summary>
        /// 职位标识
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("OrganizePositionId", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"职位标识")]
        public  long OrganizePositionId
        {
            get
            {
                OnOrganizePositionIdGet();
                return this._organizePositionId;
            }
            set
            {
                if(this._organizePositionId == value)
                    return;
                OnOrganizePositionIdSet(ref value);
                this._organizePositionId = value;
                OnOrganizePositionIdSeted();
                this.OnPropertyChanged(_DataStruct_.Real_OrganizePositionId);
            }
        }
        /// <summary>
        /// 职位
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _position;

        
        /// <summary>
        /// 职位
        /// </summary>
        /// <remarks>
        /// 称谓
        /// </remarks>
        /// <value>
        /// 不能为空.可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataMember , JsonProperty("Position", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"职位")]
        public  string Position
        {
            get
            {
                return this._position;
            }
            set
            {
                this._position = value;
            }
        }
        /// <summary>
        /// 部门外键
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _departmentId;

        
        /// <summary>
        /// 部门外键
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("DepartmentId", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"部门外键")]
        public  long DepartmentId
        {
            get
            {
                return this._departmentId;
            }
            set
            {
                this._departmentId = value;
            }
        }
        /// <summary>
        /// 部门
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _department;

        
        /// <summary>
        /// 部门
        /// </summary>
        /// <value>
        /// 可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Department", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"部门")]
        public  string Department
        {
            get
            {
                return this._department;
            }
            set
            {
                this._department = value;
            }
        }
        /// <summary>
        /// 机构标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _organizationId;

        
        /// <summary>
        /// 机构标识
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("OrganizationId", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"机构标识")]
        public  long OrganizationId
        {
            get
            {
                return this._organizationId;
            }
            set
            {
                this._organizationId = value;
            }
        }
        /// <summary>
        /// 所在机构
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _organization;

        
        /// <summary>
        /// 所在机构
        /// </summary>
        /// <value>
        /// 可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Organization", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"所在机构")]
        public  string Organization
        {
            get
            {
                return this._organization;
            }
            set
            {
                this._organization = value;
            }
        }
        /// <summary>
        /// 级别
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public int _orgLevel;

        
        /// <summary>
        /// 级别
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("OrgLevel", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"级别")]
        public  int OrgLevel
        {
            get
            {
                return this._orgLevel;
            }
            set
            {
                this._orgLevel = value;
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
        /// 关联标识
        /// </summary>
        /// <remarks>
        /// 仅限用于查询的Lambda表达式使用
        /// </remarks>
        [IgnoreDataMember , Browsable(false),JsonIgnore]
        public long MasterId => throw new Exception("关联标识属性仅限用于查询的Lambda表达式使用");

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
            case "appellation":
                this.Appellation = value == null ? null : value.ToString();
                return;
            case "realname":
                this.RealName = value == null ? null : value.ToString();
                return;
            case "userid":
                this.UserId = (long)Convert.ToDecimal(value);
                return;
            case "sex":
                if (value != null)
                {
                    if(value is int)
                    {
                        this.Sex = (SexType)(int)value;
                    }
                    else if(value is SexType)
                    {
                        this.Sex = (SexType)value;
                    }
                    else
                    {
                        var str = value.ToString();
                        SexType val;
                        if (SexType.TryParse(str, out val))
                        {
                            this.Sex = val;
                        }
                        else
                        {
                            int vl;
                            if (int.TryParse(str, out vl))
                            {
                                this.Sex = (SexType)vl;
                            }
                        }
                    }
                }
                return;
            case "birthday":
                this.Birthday = Convert.ToDateTime(value);
                return;
            case "tel":
                this.Tel = value == null ? null : value.ToString();
                return;
            case "phonenumber":
                this.PhoneNumber = value == null ? null : value.ToString();
                return;
            case "role":
                this.Role = value == null ? null : value.ToString();
                return;
            case "roleid":
                this.RoleId = (long)Convert.ToDecimal(value);
                return;
            case "organizepositionid":
                this.OrganizePositionId = (long)Convert.ToDecimal(value);
                return;
            case "position":
                this.Position = value == null ? null : value.ToString();
                return;
            case "departmentid":
                this.DepartmentId = (long)Convert.ToDecimal(value);
                return;
            case "department":
                this.Department = value == null ? null : value.ToString();
                return;
            case "organizationid":
                this.OrganizationId = (long)Convert.ToDecimal(value);
                return;
            case "organization":
                this.Organization = value == null ? null : value.ToString();
                return;
            case "orglevel":
                this.OrgLevel = (int)Convert.ToDecimal(value);
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
            case "areaid":
                this.AreaId = (long)Convert.ToDecimal(value);
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
            case _DataStruct_.Appellation:
                this.Appellation = value == null ? null : value.ToString();
                return;
            case _DataStruct_.RealName:
                this.RealName = value == null ? null : value.ToString();
                return;
            case _DataStruct_.UserId:
                this.UserId = Convert.ToInt64(value);
                return;
            case _DataStruct_.Sex:
                this.Sex = (SexType)value;
                return;
            case _DataStruct_.Birthday:
                this.Birthday = Convert.ToDateTime(value);
                return;
            case _DataStruct_.Tel:
                this.Tel = value == null ? null : value.ToString();
                return;
            case _DataStruct_.PhoneNumber:
                this.PhoneNumber = value == null ? null : value.ToString();
                return;
            case _DataStruct_.Role:
                this.Role = value == null ? null : value.ToString();
                return;
            case _DataStruct_.RoleId:
                this.RoleId = Convert.ToInt64(value);
                return;
            case _DataStruct_.OrganizePositionId:
                this.OrganizePositionId = Convert.ToInt64(value);
                return;
            case _DataStruct_.Position:
                this.Position = value == null ? null : value.ToString();
                return;
            case _DataStruct_.DepartmentId:
                this.DepartmentId = Convert.ToInt64(value);
                return;
            case _DataStruct_.Department:
                this.Department = value == null ? null : value.ToString();
                return;
            case _DataStruct_.OrganizationId:
                this.OrganizationId = Convert.ToInt64(value);
                return;
            case _DataStruct_.Organization:
                this.Organization = value == null ? null : value.ToString();
                return;
            case _DataStruct_.OrgLevel:
                this.OrgLevel = Convert.ToInt32(value);
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
            case _DataStruct_.AreaId:
                this.AreaId = Convert.ToInt64(value);
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
            case "appellation":
                return this.Appellation;
            case "realname":
                return this.RealName;
            case "userid":
                return this.UserId;
            case "sex":
                return this.Sex.ToCaption();
            case "birthday":
                return this.Birthday;
            case "tel":
                return this.Tel;
            case "phonenumber":
                return this.PhoneNumber;
            case "role":
                return this.Role;
            case "roleid":
                return this.RoleId;
            case "organizepositionid":
                return this.OrganizePositionId;
            case "position":
                return this.Position;
            case "departmentid":
                return this.DepartmentId;
            case "department":
                return this.Department;
            case "organizationid":
                return this.OrganizationId;
            case "organization":
                return this.Organization;
            case "orglevel":
                return this.OrgLevel;
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
            case "areaid":
                return this.AreaId;
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
                case _DataStruct_.Appellation:
                    return this.Appellation;
                case _DataStruct_.RealName:
                    return this.RealName;
                case _DataStruct_.UserId:
                    return this.UserId;
                case _DataStruct_.Sex:
                    return this.Sex;
                case _DataStruct_.Birthday:
                    return this.Birthday;
                case _DataStruct_.Tel:
                    return this.Tel;
                case _DataStruct_.PhoneNumber:
                    return this.PhoneNumber;
                case _DataStruct_.Role:
                    return this.Role;
                case _DataStruct_.RoleId:
                    return this.RoleId;
                case _DataStruct_.OrganizePositionId:
                    return this.OrganizePositionId;
                case _DataStruct_.Position:
                    return this.Position;
                case _DataStruct_.DepartmentId:
                    return this.DepartmentId;
                case _DataStruct_.Department:
                    return this.Department;
                case _DataStruct_.OrganizationId:
                    return this.OrganizationId;
                case _DataStruct_.Organization:
                    return this.Organization;
                case _DataStruct_.OrgLevel:
                    return this.OrgLevel;
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
                case _DataStruct_.AreaId:
                    return this.AreaId;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(PositionPersonnelData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as PositionPersonnelData;
            if(sourceEntity == null)
                return;
            this._id = sourceEntity._id;
            this._appellation = sourceEntity._appellation;
            this._realName = sourceEntity._realName;
            this._userId = sourceEntity._userId;
            this._sex = sourceEntity._sex;
            this._birthday = sourceEntity._birthday;
            this._tel = sourceEntity._tel;
            this._phoneNumber = sourceEntity._phoneNumber;
            this._role = sourceEntity._role;
            this._roleId = sourceEntity._roleId;
            this._organizePositionId = sourceEntity._organizePositionId;
            this._position = sourceEntity._position;
            this._departmentId = sourceEntity._departmentId;
            this._department = sourceEntity._department;
            this._organizationId = sourceEntity._organizationId;
            this._organization = sourceEntity._organization;
            this._orgLevel = sourceEntity._orgLevel;
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
            this._areaId = sourceEntity._areaId;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(PositionPersonnelData source)
        {
                this.Id = source.Id;
                this.Appellation = source.Appellation;
                this.RealName = source.RealName;
                this.UserId = source.UserId;
                this.Sex = source.Sex;
                this.Birthday = source.Birthday;
                this.Tel = source.Tel;
                this.PhoneNumber = source.PhoneNumber;
                this.Role = source.Role;
                this.RoleId = source.RoleId;
                this.OrganizePositionId = source.OrganizePositionId;
                this.Position = source.Position;
                this.DepartmentId = source.DepartmentId;
                this.Department = source.Department;
                this.OrganizationId = source.OrganizationId;
                this.Organization = source.Organization;
                this.OrgLevel = source.OrgLevel;
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
                this.AreaId = source.AreaId;
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
                OnAppellationModified(subsist,false);
                OnRealNameModified(subsist,false);
                OnUserIdModified(subsist,false);
                OnSexModified(subsist,false);
                OnBirthdayModified(subsist,false);
                OnTelModified(subsist,false);
                OnPhoneNumberModified(subsist,false);
                OnRoleModified(subsist,false);
                OnRoleIdModified(subsist,false);
                OnOrganizePositionIdModified(subsist,false);
                OnPositionModified(subsist,false);
                OnDepartmentIdModified(subsist,false);
                OnDepartmentModified(subsist,false);
                OnOrganizationIdModified(subsist,false);
                OnOrganizationModified(subsist,false);
                OnOrgLevelModified(subsist,false);
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
                OnAreaIdModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnIdModified(subsist,true);
                OnAppellationModified(subsist,true);
                OnRealNameModified(subsist,true);
                OnUserIdModified(subsist,true);
                OnSexModified(subsist,true);
                OnBirthdayModified(subsist,true);
                OnTelModified(subsist,true);
                OnPhoneNumberModified(subsist,true);
                OnRoleModified(subsist,true);
                OnRoleIdModified(subsist,true);
                OnOrganizePositionIdModified(subsist,true);
                OnPositionModified(subsist,true);
                OnDepartmentIdModified(subsist,true);
                OnDepartmentModified(subsist,true);
                OnOrganizationIdModified(subsist,true);
                OnOrganizationModified(subsist,true);
                OnOrgLevelModified(subsist,true);
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
                OnAreaIdModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[28] > 0)
            {
                OnIdModified(subsist,modifieds[_DataStruct_.Real_Id] == 1);
                OnAppellationModified(subsist,modifieds[_DataStruct_.Real_Appellation] == 1);
                OnRealNameModified(subsist,modifieds[_DataStruct_.Real_RealName] == 1);
                OnUserIdModified(subsist,modifieds[_DataStruct_.Real_UserId] == 1);
                OnSexModified(subsist,modifieds[_DataStruct_.Real_Sex] == 1);
                OnBirthdayModified(subsist,modifieds[_DataStruct_.Real_Birthday] == 1);
                OnTelModified(subsist,modifieds[_DataStruct_.Real_Tel] == 1);
                OnPhoneNumberModified(subsist,modifieds[_DataStruct_.Real_PhoneNumber] == 1);
                OnRoleModified(subsist,modifieds[_DataStruct_.Real_Role] == 1);
                OnRoleIdModified(subsist,modifieds[_DataStruct_.Real_RoleId] == 1);
                OnOrganizePositionIdModified(subsist,modifieds[_DataStruct_.Real_OrganizePositionId] == 1);
                OnPositionModified(subsist,modifieds[_DataStruct_.Real_Position] == 1);
                OnDepartmentIdModified(subsist,modifieds[_DataStruct_.Real_DepartmentId] == 1);
                OnDepartmentModified(subsist,modifieds[_DataStruct_.Real_Department] == 1);
                OnOrganizationIdModified(subsist,modifieds[_DataStruct_.Real_OrganizationId] == 1);
                OnOrganizationModified(subsist,modifieds[_DataStruct_.Real_Organization] == 1);
                OnOrgLevelModified(subsist,modifieds[_DataStruct_.Real_OrgLevel] == 1);
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
                OnAreaIdModified(subsist,modifieds[_DataStruct_.Real_AreaId] == 1);
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
        /// 称谓修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAppellationModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 姓名修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnRealNameModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 员工标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnUserIdModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 性别修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnSexModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 生日修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnBirthdayModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 电话修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnTelModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 手机修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnPhoneNumberModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 角色修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnRoleModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 角色外键修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnRoleIdModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 职位标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOrganizePositionIdModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 职位修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnPositionModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 部门外键修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnDepartmentIdModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 部门修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnDepartmentModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 机构标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOrganizationIdModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 所在机构修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOrganizationModified(EntitySubsist subsist,bool isModified);

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
        /// 行政区域外键修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAreaIdModified(EntitySubsist subsist,bool isModified);
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
            public const string EntityName = @"PositionPersonnel";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"人员职位设置";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"人员职位设置";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0x20006;
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityPrimaryKey = "Id";
            
            
            /// <summary>
            /// 标识的数字标识
            /// </summary>
            public const byte Id = 59;
            
            /// <summary>
            /// 标识的实时记录顺序
            /// </summary>
            public const int Real_Id = 0;

            /// <summary>
            /// 称谓的数字标识
            /// </summary>
            public const byte Appellation = 60;
            
            /// <summary>
            /// 称谓的实时记录顺序
            /// </summary>
            public const int Real_Appellation = 1;

            /// <summary>
            /// 姓名的数字标识
            /// </summary>
            public const byte RealName = 61;
            
            /// <summary>
            /// 姓名的实时记录顺序
            /// </summary>
            public const int Real_RealName = 2;

            /// <summary>
            /// 员工标识的数字标识
            /// </summary>
            public const byte UserId = 62;
            
            /// <summary>
            /// 员工标识的实时记录顺序
            /// </summary>
            public const int Real_UserId = 3;

            /// <summary>
            /// 性别的数字标识
            /// </summary>
            public const byte Sex = 63;
            
            /// <summary>
            /// 性别的实时记录顺序
            /// </summary>
            public const int Real_Sex = 4;

            /// <summary>
            /// 生日的数字标识
            /// </summary>
            public const byte Birthday = 64;
            
            /// <summary>
            /// 生日的实时记录顺序
            /// </summary>
            public const int Real_Birthday = 5;

            /// <summary>
            /// 电话的数字标识
            /// </summary>
            public const byte Tel = 65;
            
            /// <summary>
            /// 电话的实时记录顺序
            /// </summary>
            public const int Real_Tel = 6;

            /// <summary>
            /// 手机的数字标识
            /// </summary>
            public const byte PhoneNumber = 66;
            
            /// <summary>
            /// 手机的实时记录顺序
            /// </summary>
            public const int Real_PhoneNumber = 7;

            /// <summary>
            /// 角色的数字标识
            /// </summary>
            public const byte Role = 67;
            
            /// <summary>
            /// 角色的实时记录顺序
            /// </summary>
            public const int Real_Role = 8;

            /// <summary>
            /// 角色外键的数字标识
            /// </summary>
            public const byte RoleId = 68;
            
            /// <summary>
            /// 角色外键的实时记录顺序
            /// </summary>
            public const int Real_RoleId = 9;

            /// <summary>
            /// 职位标识的数字标识
            /// </summary>
            public const byte OrganizePositionId = 69;
            
            /// <summary>
            /// 职位标识的实时记录顺序
            /// </summary>
            public const int Real_OrganizePositionId = 10;

            /// <summary>
            /// 职位的数字标识
            /// </summary>
            public const byte Position = 70;
            
            /// <summary>
            /// 职位的实时记录顺序
            /// </summary>
            public const int Real_Position = 11;

            /// <summary>
            /// 部门外键的数字标识
            /// </summary>
            public const byte DepartmentId = 71;
            
            /// <summary>
            /// 部门外键的实时记录顺序
            /// </summary>
            public const int Real_DepartmentId = 12;

            /// <summary>
            /// 部门的数字标识
            /// </summary>
            public const byte Department = 72;
            
            /// <summary>
            /// 部门的实时记录顺序
            /// </summary>
            public const int Real_Department = 13;

            /// <summary>
            /// 机构标识的数字标识
            /// </summary>
            public const byte OrganizationId = 73;
            
            /// <summary>
            /// 机构标识的实时记录顺序
            /// </summary>
            public const int Real_OrganizationId = 14;

            /// <summary>
            /// 所在机构的数字标识
            /// </summary>
            public const byte Organization = 74;
            
            /// <summary>
            /// 所在机构的实时记录顺序
            /// </summary>
            public const int Real_Organization = 15;

            /// <summary>
            /// 级别的数字标识
            /// </summary>
            public const byte OrgLevel = 75;
            
            /// <summary>
            /// 级别的实时记录顺序
            /// </summary>
            public const int Real_OrgLevel = 16;

            /// <summary>
            /// 备注的数字标识
            /// </summary>
            public const byte Memo = 76;
            
            /// <summary>
            /// 备注的实时记录顺序
            /// </summary>
            public const int Real_Memo = 17;

            /// <summary>
            /// 冻结更新的数字标识
            /// </summary>
            public const byte IsFreeze = 77;
            
            /// <summary>
            /// 冻结更新的实时记录顺序
            /// </summary>
            public const int Real_IsFreeze = 18;

            /// <summary>
            /// 数据状态的数字标识
            /// </summary>
            public const byte DataState = 78;
            
            /// <summary>
            /// 数据状态的实时记录顺序
            /// </summary>
            public const int Real_DataState = 19;

            /// <summary>
            /// 制作时间的数字标识
            /// </summary>
            public const byte AddDate = 79;
            
            /// <summary>
            /// 制作时间的实时记录顺序
            /// </summary>
            public const int Real_AddDate = 20;

            /// <summary>
            /// 最后修改者的数字标识
            /// </summary>
            public const byte LastReviserId = 80;
            
            /// <summary>
            /// 最后修改者的实时记录顺序
            /// </summary>
            public const int Real_LastReviserId = 21;

            /// <summary>
            /// 最后修改日期的数字标识
            /// </summary>
            public const byte LastModifyDate = 81;
            
            /// <summary>
            /// 最后修改日期的实时记录顺序
            /// </summary>
            public const int Real_LastModifyDate = 22;

            /// <summary>
            /// 制作人的数字标识
            /// </summary>
            public const byte AuthorId = 82;
            
            /// <summary>
            /// 制作人的实时记录顺序
            /// </summary>
            public const int Real_AuthorId = 23;

            /// <summary>
            /// 审核人的数字标识
            /// </summary>
            public const byte AuditorId = 83;
            
            /// <summary>
            /// 审核人的实时记录顺序
            /// </summary>
            public const int Real_AuditorId = 24;

            /// <summary>
            /// 审核时间的数字标识
            /// </summary>
            public const byte AuditDate = 84;
            
            /// <summary>
            /// 审核时间的实时记录顺序
            /// </summary>
            public const int Real_AuditDate = 25;

            /// <summary>
            /// 审核状态的数字标识
            /// </summary>
            public const byte AuditState = 85;
            
            /// <summary>
            /// 审核状态的实时记录顺序
            /// </summary>
            public const int Real_AuditState = 26;

            /// <summary>
            /// 行政区域外键的数字标识
            /// </summary>
            public const byte AreaId = 87;
            
            /// <summary>
            /// 行政区域外键的实时记录顺序
            /// </summary>
            public const int Real_AreaId = 27;

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
                            Description  = @"标识",
                            ColumnName   = "id",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Appellation,
                        new PropertySturct
                        {
                            Index        = Appellation,
                            Name         = "Appellation",
                            Title        = "称谓",
                            Caption      = @"称谓",
                            Description  = @"称谓",
                            ColumnName   = "appellation",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_RealName,
                        new PropertySturct
                        {
                            Index        = RealName,
                            Name         = "RealName",
                            Title        = "姓名",
                            Caption      = @"姓名",
                            Description  = @"姓名",
                            ColumnName   = "real_name",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_UserId,
                        new PropertySturct
                        {
                            Index        = UserId,
                            Name         = "UserId",
                            Title        = "员工标识",
                            Caption      = @"员工标识",
                            Description  = @"员工标识",
                            ColumnName   = "user_id",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Sex,
                        new PropertySturct
                        {
                            Index        = Sex,
                            Name         = "Sex",
                            Title        = "性别",
                            Caption      = @"性别",
                            Description  = @"性别",
                            ColumnName   = "sex",
                            PropertyType = typeof(SexType),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Birthday,
                        new PropertySturct
                        {
                            Index        = Birthday,
                            Name         = "Birthday",
                            Title        = "生日",
                            Caption      = @"生日",
                            Description  = @"生日",
                            ColumnName   = "birthday",
                            PropertyType = typeof(DateTime),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Tel,
                        new PropertySturct
                        {
                            Index        = Tel,
                            Name         = "Tel",
                            Title        = "电话",
                            Caption      = @"电话",
                            Description  = @"电话",
                            ColumnName   = "tel",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_PhoneNumber,
                        new PropertySturct
                        {
                            Index        = PhoneNumber,
                            Name         = "PhoneNumber",
                            Title        = "手机",
                            Caption      = @"手机",
                            Description  = @"手机",
                            ColumnName   = "phone_number",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Role,
                        new PropertySturct
                        {
                            Index        = Role,
                            Name         = "Role",
                            Title        = "角色",
                            Caption      = @"角色",
                            Description  = @"角色",
                            ColumnName   = "role",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_RoleId,
                        new PropertySturct
                        {
                            Index        = RoleId,
                            Name         = "RoleId",
                            Title        = "角色外键",
                            Caption      = @"角色外键",
                            Description  = @"角色外键",
                            ColumnName   = "role_id",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_OrganizePositionId,
                        new PropertySturct
                        {
                            Index        = OrganizePositionId,
                            Name         = "OrganizePositionId",
                            Title        = "职位标识",
                            Caption      = @"职位标识",
                            Description  = @"职位标识",
                            ColumnName   = "organize_position_id",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Position,
                        new PropertySturct
                        {
                            Index        = Position,
                            Name         = "Position",
                            Title        = "职位",
                            Caption      = @"职位",
                            Description  = @"称谓",
                            ColumnName   = "position",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_DepartmentId,
                        new PropertySturct
                        {
                            Index        = DepartmentId,
                            Name         = "DepartmentId",
                            Title        = "部门外键",
                            Caption      = @"部门外键",
                            Description  = @"部门外键",
                            ColumnName   = "department_id",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Department,
                        new PropertySturct
                        {
                            Index        = Department,
                            Name         = "Department",
                            Title        = "部门",
                            Caption      = @"部门",
                            Description  = @"部门",
                            ColumnName   = "department",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_OrganizationId,
                        new PropertySturct
                        {
                            Index        = OrganizationId,
                            Name         = "OrganizationId",
                            Title        = "机构标识",
                            Caption      = @"机构标识",
                            Description  = @"机构标识",
                            ColumnName   = "organization_id",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Organization,
                        new PropertySturct
                        {
                            Index        = Organization,
                            Name         = "Organization",
                            Title        = "所在机构",
                            Caption      = @"所在机构",
                            Description  = @"所在机构",
                            ColumnName   = "organization",
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
                        Real_Memo,
                        new PropertySturct
                        {
                            Index        = Memo,
                            Name         = "Memo",
                            Title        = "备注",
                            Caption      = @"备注",
                            Description  = @"备注",
                            ColumnName   = "memo",
                            PropertyType = typeof(string),
                            CanNull      = true,
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
                    }
                }
            };
        }
        #endregion

    }
}