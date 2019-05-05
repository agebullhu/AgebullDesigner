/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/4/10 10:44:36*/
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
using Agebull.EntityModel.Interfaces;


#endregion

namespace HPC.Projects
{
    /// <summary>
    /// 员工
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class EmployeeData : IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public EmployeeData()
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
        public void ChangePrimaryKey(long eID)
        {
            _eID = eID;
        }
        /// <summary>
        /// 主键
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _eID;

        partial void OnEIDGet();

        partial void OnEIDSet(ref long value);

        partial void OnEIDLoad(ref long value);

        partial void OnEIDSeted();

        
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember , JsonProperty("EID", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"主键")]
        public long EID
        {
            get
            {
                OnEIDGet();
                return this._eID;
            }
            set
            {
                if(this._eID == value)
                    return;
                //if(this._eID > 0)
                //    throw new Exception("主键一旦设置就不可以修改");
                OnEIDSet(ref value);
                this._eID = value;
                this.OnPropertyChanged(_DataStruct_.Real_EID);
                OnEIDSeted();
            }
        }
        /// <summary>
        /// 站点标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _siteSID;

        partial void OnSiteSIDGet();

        partial void OnSiteSIDSet(ref long value);

        partial void OnSiteSIDSeted();

        
        /// <summary>
        /// 站点标识
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("SiteSID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"站点标识")]
        public  long SiteSID
        {
            get
            {
                OnSiteSIDGet();
                return this._siteSID;
            }
            set
            {
                if(this._siteSID == value)
                    return;
                OnSiteSIDSet(ref value);
                this._siteSID = value;
                OnSiteSIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_SiteSID);
            }
        }
        /// <summary>
        /// 组织标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _orgOID;

        partial void OnOrgOIDGet();

        partial void OnOrgOIDSet(ref long value);

        partial void OnOrgOIDSeted();

        
        /// <summary>
        /// 组织标识
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("OrgOID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"组织标识")]
        public  long OrgOID
        {
            get
            {
                OnOrgOIDGet();
                return this._orgOID;
            }
            set
            {
                if(this._orgOID == value)
                    return;
                OnOrgOIDSet(ref value);
                this._orgOID = value;
                OnOrgOIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_OrgOID);
            }
        }
        /// <summary>
        /// 角色扮演
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _roleRID;

        partial void OnRoleRIDGet();

        partial void OnRoleRIDSet(ref long value);

        partial void OnRoleRIDSeted();

        
        /// <summary>
        /// 角色扮演
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("RoleRID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"角色扮演")]
        public  long RoleRID
        {
            get
            {
                OnRoleRIDGet();
                return this._roleRID;
            }
            set
            {
                if(this._roleRID == value)
                    return;
                OnRoleRIDSet(ref value);
                this._roleRID = value;
                OnRoleRIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_RoleRID);
            }
        }
        /// <summary>
        /// EMP=35782；
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _empID;

        partial void OnEmpIDGet();

        partial void OnEmpIDSet(ref string value);

        partial void OnEmpIDSeted();

        
        /// <summary>
        /// EMP=35782；
        /// </summary>
        /// <value>
        /// 可存储50个字符.合理长度应不大于50.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("EmpID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"EMP=35782；")]
        public  string EmpID
        {
            get
            {
                OnEmpIDGet();
                return this._empID;
            }
            set
            {
                if(this._empID == value)
                    return;
                OnEmpIDSet(ref value);
                this._empID = value;
                OnEmpIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_EmpID);
            }
        }
        /// <summary>
        /// 状态登录
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public bool _stateLogin;

        partial void OnStateLoginGet();

        partial void OnStateLoginSet(ref bool value);

        partial void OnStateLoginSeted();

        
        /// <summary>
        /// 状态登录
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("StateLogin", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"状态登录")]
        public  bool StateLogin
        {
            get
            {
                OnStateLoginGet();
                return this._stateLogin;
            }
            set
            {
                if(this._stateLogin == value)
                    return;
                OnStateLoginSet(ref value);
                this._stateLogin = value;
                OnStateLoginSeted();
                this.OnPropertyChanged(_DataStruct_.Real_StateLogin);
            }
        }
        /// <summary>
        /// 状态删除
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public bool _stateDelete;

        partial void OnStateDeleteGet();

        partial void OnStateDeleteSet(ref bool value);

        partial void OnStateDeleteSeted();

        
        /// <summary>
        /// 状态删除
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("StateDelete", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"状态删除")]
        public  bool StateDelete
        {
            get
            {
                OnStateDeleteGet();
                return this._stateDelete;
            }
            set
            {
                if(this._stateDelete == value)
                    return;
                OnStateDeleteSet(ref value);
                this._stateDelete = value;
                OnStateDeleteSeted();
                this.OnPropertyChanged(_DataStruct_.Real_StateDelete);
            }
        }
        /// <summary>
        /// 员工姓名
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _employeeName;

        partial void OnEmployeeNameGet();

        partial void OnEmployeeNameSet(ref string value);

        partial void OnEmployeeNameSeted();

        
        /// <summary>
        /// 员工姓名
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("EmployeeName", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"员工姓名")]
        public  string EmployeeName
        {
            get
            {
                OnEmployeeNameGet();
                return this._employeeName;
            }
            set
            {
                if(this._employeeName == value)
                    return;
                OnEmployeeNameSet(ref value);
                this._employeeName = value;
                OnEmployeeNameSeted();
                this.OnPropertyChanged(_DataStruct_.Real_EmployeeName);
            }
        }
        /// <summary>
        /// 密码
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _password;

        partial void OnPasswordGet();

        partial void OnPasswordSet(ref string value);

        partial void OnPasswordSeted();

        
        /// <summary>
        /// 密码
        /// </summary>
        /// <value>
        /// 可存储50个字符.合理长度应不大于50.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Password", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"密码")]
        public  string Password
        {
            get
            {
                OnPasswordGet();
                return this._password;
            }
            set
            {
                if(this._password == value)
                    return;
                OnPasswordSet(ref value);
                this._password = value;
                OnPasswordSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Password);
            }
        }
        /// <summary>
        /// 令牌
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _token;

        partial void OnTokenGet();

        partial void OnTokenSet(ref string value);

        partial void OnTokenSeted();

        
        /// <summary>
        /// 令牌
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Token", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"令牌")]
        public  string Token
        {
            get
            {
                OnTokenGet();
                return this._token;
            }
            set
            {
                if(this._token == value)
                    return;
                OnTokenSet(ref value);
                this._token = value;
                OnTokenSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Token);
            }
        }
        /// <summary>
        /// 性别
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _gender;

        partial void OnGenderGet();

        partial void OnGenderSet(ref string value);

        partial void OnGenderSeted();

        
        /// <summary>
        /// 性别
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Gender", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"性别")]
        public  string Gender
        {
            get
            {
                OnGenderGet();
                return this._gender;
            }
            set
            {
                if(this._gender == value)
                    return;
                OnGenderSet(ref value);
                this._gender = value;
                OnGenderSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Gender);
            }
        }
        /// <summary>
        /// 电话
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _phone;

        partial void OnPhoneGet();

        partial void OnPhoneSet(ref string value);

        partial void OnPhoneSeted();

        
        /// <summary>
        /// 电话
        /// </summary>
        /// <value>
        /// 可存储50个字符.合理长度应不大于50.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Phone", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"电话")]
        public  string Phone
        {
            get
            {
                OnPhoneGet();
                return this._phone;
            }
            set
            {
                if(this._phone == value)
                    return;
                OnPhoneSet(ref value);
                this._phone = value;
                OnPhoneSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Phone);
            }
        }
        /// <summary>
        /// 电子邮件
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _email;

        partial void OnEmailGet();

        partial void OnEmailSet(ref string value);

        partial void OnEmailSeted();

        
        /// <summary>
        /// 电子邮件
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Email", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"电子邮件")]
        public  string Email
        {
            get
            {
                OnEmailGet();
                return this._email;
            }
            set
            {
                if(this._email == value)
                    return;
                OnEmailSet(ref value);
                this._email = value;
                OnEmailSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Email);
            }
        }
        /// <summary>
        /// 备注
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _remark;

        partial void OnRemarkGet();

        partial void OnRemarkSet(ref string value);

        partial void OnRemarkSeted();

        
        /// <summary>
        /// 备注
        /// </summary>
        /// <value>
        /// 可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Remark", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"备注")]
        public  string Remark
        {
            get
            {
                OnRemarkGet();
                return this._remark;
            }
            set
            {
                if(this._remark == value)
                    return;
                OnRemarkSet(ref value);
                this._remark = value;
                OnRemarkSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Remark);
            }
        }
        /// <summary>
        /// 误差时间
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public int _errorTimes;

        partial void OnErrorTimesGet();

        partial void OnErrorTimesSet(ref int value);

        partial void OnErrorTimesSeted();

        
        /// <summary>
        /// 误差时间
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("ErrorTimes", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"误差时间")]
        public  int ErrorTimes
        {
            get
            {
                OnErrorTimesGet();
                return this._errorTimes;
            }
            set
            {
                if(this._errorTimes == value)
                    return;
                OnErrorTimesSet(ref value);
                this._errorTimes = value;
                OnErrorTimesSeted();
                this.OnPropertyChanged(_DataStruct_.Real_ErrorTimes);
            }
        }
        /// <summary>
        /// 上次登录日期
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public DateTime _lastLoginDate;

        partial void OnLastLoginDateGet();

        partial void OnLastLoginDateSet(ref DateTime value);

        partial void OnLastLoginDateSeted();

        
        /// <summary>
        /// 上次登录日期
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("LastLoginDate", NullValueHandling = NullValueHandling.Ignore) , JsonConverter(typeof(MyDateTimeConverter)) , DisplayName(@"上次登录日期")]
        public  DateTime LastLoginDate
        {
            get
            {
                OnLastLoginDateGet();
                return this._lastLoginDate;
            }
            set
            {
                if(this._lastLoginDate == value)
                    return;
                OnLastLoginDateSet(ref value);
                this._lastLoginDate = value;
                OnLastLoginDateSeted();
                this.OnPropertyChanged(_DataStruct_.Real_LastLoginDate);
            }
        }
        /// <summary>
        /// 最后登录IP
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _lastLoginIP;

        partial void OnLastLoginIPGet();

        partial void OnLastLoginIPSet(ref string value);

        partial void OnLastLoginIPSeted();

        
        /// <summary>
        /// 最后登录IP
        /// </summary>
        /// <value>
        /// 可存储50个字符.合理长度应不大于50.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("LastLoginIP", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"最后登录IP")]
        public  string LastLoginIP
        {
            get
            {
                OnLastLoginIPGet();
                return this._lastLoginIP;
            }
            set
            {
                if(this._lastLoginIP == value)
                    return;
                OnLastLoginIPSet(ref value);
                this._lastLoginIP = value;
                OnLastLoginIPSeted();
                this.OnPropertyChanged(_DataStruct_.Real_LastLoginIP);
            }
        }
        /// <summary>
        /// 添加日期
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public DateTime _addDate;

        partial void OnAddDateGet();

        partial void OnAddDateSet(ref DateTime value);

        partial void OnAddDateSeted();

        
        /// <summary>
        /// 添加日期
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("AddDate", NullValueHandling = NullValueHandling.Ignore) , JsonConverter(typeof(MyDateTimeConverter)) , DisplayName(@"添加日期")]
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

        #region 接口属性


        /// <summary>
        /// 对象标识
        /// </summary>
        [IgnoreDataMember,Browsable(false)]
        public long Id
        {
            get
            {
                return this.EID;
            }
            set
            {
                this.EID = value;
            }
        }
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
        protected override bool SetValueInner(string property, string value)
        {
            if(property == null) return false;
            switch(property.Trim().ToLower())
            {
            case "eid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.EID = vl;
                        return true;
                    }
                }
                return false;
            case "sitesid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.SiteSID = vl;
                        return true;
                    }
                }
                return false;
            case "orgoid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.OrgOID = vl;
                        return true;
                    }
                }
                return false;
            case "rolerid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.RoleRID = vl;
                        return true;
                    }
                }
                return false;
            case "empid":
                this.EmpID = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "statelogin":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (bool.TryParse(value, out var vl))
                    {
                        this.StateLogin = vl;
                        return true;
                    }
                }
                return false;
            case "statedelete":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (bool.TryParse(value, out var vl))
                    {
                        this.StateDelete = vl;
                        return true;
                    }
                }
                return false;
            case "employeename":
                this.EmployeeName = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "password":
                this.Password = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "token":
                this.Token = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "gender":
                this.Gender = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "phone":
                this.Phone = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "email":
                this.Email = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "remark":
                this.Remark = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "errortimes":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (int.TryParse(value, out var vl))
                    {
                        this.ErrorTimes = vl;
                        return true;
                    }
                }
                return false;
            case "lastlogindate":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (DateTime.TryParse(value, out var vl))
                    {
                        this.LastLoginDate = vl;
                        return true;
                    }
                }
                return false;
            case "lastloginip":
                this.LastLoginIP = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "adddate":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (DateTime.TryParse(value, out var vl))
                    {
                        this.AddDate = vl;
                        return true;
                    }
                }
                return false;
            }
            return false;
        }

    

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
            case "eid":
                this.EID = (long)Convert.ToDecimal(value);
                return;
            case "sitesid":
                this.SiteSID = (long)Convert.ToDecimal(value);
                return;
            case "orgoid":
                this.OrgOID = (long)Convert.ToDecimal(value);
                return;
            case "rolerid":
                this.RoleRID = (long)Convert.ToDecimal(value);
                return;
            case "empid":
                this.EmpID = value == null ? null : value.ToString();
                return;
            case "statelogin":
                if (value != null)
                {
                    int vl;
                    if (int.TryParse(value.ToString(), out vl))
                    {
                        this.StateLogin = vl != 0;
                    }
                    else
                    {
                        this.StateLogin = Convert.ToBoolean(value);
                    }
                }
                return;
            case "statedelete":
                if (value != null)
                {
                    int vl;
                    if (int.TryParse(value.ToString(), out vl))
                    {
                        this.StateDelete = vl != 0;
                    }
                    else
                    {
                        this.StateDelete = Convert.ToBoolean(value);
                    }
                }
                return;
            case "employeename":
                this.EmployeeName = value == null ? null : value.ToString();
                return;
            case "password":
                this.Password = value == null ? null : value.ToString();
                return;
            case "token":
                this.Token = value == null ? null : value.ToString();
                return;
            case "gender":
                this.Gender = value == null ? null : value.ToString();
                return;
            case "phone":
                this.Phone = value == null ? null : value.ToString();
                return;
            case "email":
                this.Email = value == null ? null : value.ToString();
                return;
            case "remark":
                this.Remark = value == null ? null : value.ToString();
                return;
            case "errortimes":
                this.ErrorTimes = (int)Convert.ToDecimal(value);
                return;
            case "lastlogindate":
                this.LastLoginDate = Convert.ToDateTime(value);
                return;
            case "lastloginip":
                this.LastLoginIP = value == null ? null : value.ToString();
                return;
            case "adddate":
                this.AddDate = Convert.ToDateTime(value);
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
            case _DataStruct_.EID:
                this.EID = Convert.ToInt64(value);
                return;
            case _DataStruct_.SiteSID:
                this.SiteSID = Convert.ToInt64(value);
                return;
            case _DataStruct_.OrgOID:
                this.OrgOID = Convert.ToInt64(value);
                return;
            case _DataStruct_.RoleRID:
                this.RoleRID = Convert.ToInt64(value);
                return;
            case _DataStruct_.EmpID:
                this.EmpID = value == null ? null : value.ToString();
                return;
            case _DataStruct_.StateLogin:
                this.StateLogin = Convert.ToBoolean(value);
                return;
            case _DataStruct_.StateDelete:
                this.StateDelete = Convert.ToBoolean(value);
                return;
            case _DataStruct_.EmployeeName:
                this.EmployeeName = value == null ? null : value.ToString();
                return;
            case _DataStruct_.Password:
                this.Password = value == null ? null : value.ToString();
                return;
            case _DataStruct_.Token:
                this.Token = value == null ? null : value.ToString();
                return;
            case _DataStruct_.Gender:
                this.Gender = value == null ? null : value.ToString();
                return;
            case _DataStruct_.Phone:
                this.Phone = value == null ? null : value.ToString();
                return;
            case _DataStruct_.Email:
                this.Email = value == null ? null : value.ToString();
                return;
            case _DataStruct_.Remark:
                this.Remark = value == null ? null : value.ToString();
                return;
            case _DataStruct_.ErrorTimes:
                this.ErrorTimes = Convert.ToInt32(value);
                return;
            case _DataStruct_.LastLoginDate:
                this.LastLoginDate = Convert.ToDateTime(value);
                return;
            case _DataStruct_.LastLoginIP:
                this.LastLoginIP = value == null ? null : value.ToString();
                return;
            case _DataStruct_.AddDate:
                this.AddDate = Convert.ToDateTime(value);
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
            case "eid":
                return this.EID;
            case "sitesid":
                return this.SiteSID;
            case "orgoid":
                return this.OrgOID;
            case "rolerid":
                return this.RoleRID;
            case "empid":
                return this.EmpID;
            case "statelogin":
                return this.StateLogin;
            case "statedelete":
                return this.StateDelete;
            case "employeename":
                return this.EmployeeName;
            case "password":
                return this.Password;
            case "token":
                return this.Token;
            case "gender":
                return this.Gender;
            case "phone":
                return this.Phone;
            case "email":
                return this.Email;
            case "remark":
                return this.Remark;
            case "errortimes":
                return this.ErrorTimes;
            case "lastlogindate":
                return this.LastLoginDate;
            case "lastloginip":
                return this.LastLoginIP;
            case "adddate":
                return this.AddDate;
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
                case _DataStruct_.EID:
                    return this.EID;
                case _DataStruct_.SiteSID:
                    return this.SiteSID;
                case _DataStruct_.OrgOID:
                    return this.OrgOID;
                case _DataStruct_.RoleRID:
                    return this.RoleRID;
                case _DataStruct_.EmpID:
                    return this.EmpID;
                case _DataStruct_.StateLogin:
                    return this.StateLogin;
                case _DataStruct_.StateDelete:
                    return this.StateDelete;
                case _DataStruct_.EmployeeName:
                    return this.EmployeeName;
                case _DataStruct_.Password:
                    return this.Password;
                case _DataStruct_.Token:
                    return this.Token;
                case _DataStruct_.Gender:
                    return this.Gender;
                case _DataStruct_.Phone:
                    return this.Phone;
                case _DataStruct_.Email:
                    return this.Email;
                case _DataStruct_.Remark:
                    return this.Remark;
                case _DataStruct_.ErrorTimes:
                    return this.ErrorTimes;
                case _DataStruct_.LastLoginDate:
                    return this.LastLoginDate;
                case _DataStruct_.LastLoginIP:
                    return this.LastLoginIP;
                case _DataStruct_.AddDate:
                    return this.AddDate;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(EmployeeData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as EmployeeData;
            if(sourceEntity == null)
                return;
            this._eID = sourceEntity._eID;
            this._siteSID = sourceEntity._siteSID;
            this._orgOID = sourceEntity._orgOID;
            this._roleRID = sourceEntity._roleRID;
            this._empID = sourceEntity._empID;
            this._stateLogin = sourceEntity._stateLogin;
            this._stateDelete = sourceEntity._stateDelete;
            this._employeeName = sourceEntity._employeeName;
            this._password = sourceEntity._password;
            this._token = sourceEntity._token;
            this._gender = sourceEntity._gender;
            this._phone = sourceEntity._phone;
            this._email = sourceEntity._email;
            this._remark = sourceEntity._remark;
            this._errorTimes = sourceEntity._errorTimes;
            this._lastLoginDate = sourceEntity._lastLoginDate;
            this._lastLoginIP = sourceEntity._lastLoginIP;
            this._addDate = sourceEntity._addDate;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(EmployeeData source)
        {
                this.EID = source.EID;
                this.SiteSID = source.SiteSID;
                this.OrgOID = source.OrgOID;
                this.RoleRID = source.RoleRID;
                this.EmpID = source.EmpID;
                this.StateLogin = source.StateLogin;
                this.StateDelete = source.StateDelete;
                this.EmployeeName = source.EmployeeName;
                this.Password = source.Password;
                this.Token = source.Token;
                this.Gender = source.Gender;
                this.Phone = source.Phone;
                this.Email = source.Email;
                this.Remark = source.Remark;
                this.ErrorTimes = source.ErrorTimes;
                this.LastLoginDate = source.LastLoginDate;
                this.LastLoginIP = source.LastLoginIP;
                this.AddDate = source.AddDate;
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
                OnEIDModified(subsist,false);
                OnSiteSIDModified(subsist,false);
                OnOrgOIDModified(subsist,false);
                OnRoleRIDModified(subsist,false);
                OnEmpIDModified(subsist,false);
                OnStateLoginModified(subsist,false);
                OnStateDeleteModified(subsist,false);
                OnEmployeeNameModified(subsist,false);
                OnPasswordModified(subsist,false);
                OnTokenModified(subsist,false);
                OnGenderModified(subsist,false);
                OnPhoneModified(subsist,false);
                OnEmailModified(subsist,false);
                OnRemarkModified(subsist,false);
                OnErrorTimesModified(subsist,false);
                OnLastLoginDateModified(subsist,false);
                OnLastLoginIPModified(subsist,false);
                OnAddDateModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnEIDModified(subsist,true);
                OnSiteSIDModified(subsist,true);
                OnOrgOIDModified(subsist,true);
                OnRoleRIDModified(subsist,true);
                OnEmpIDModified(subsist,true);
                OnStateLoginModified(subsist,true);
                OnStateDeleteModified(subsist,true);
                OnEmployeeNameModified(subsist,true);
                OnPasswordModified(subsist,true);
                OnTokenModified(subsist,true);
                OnGenderModified(subsist,true);
                OnPhoneModified(subsist,true);
                OnEmailModified(subsist,true);
                OnRemarkModified(subsist,true);
                OnErrorTimesModified(subsist,true);
                OnLastLoginDateModified(subsist,true);
                OnLastLoginIPModified(subsist,true);
                OnAddDateModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[18] > 0)
            {
                OnEIDModified(subsist,modifieds[_DataStruct_.Real_EID] == 1);
                OnSiteSIDModified(subsist,modifieds[_DataStruct_.Real_SiteSID] == 1);
                OnOrgOIDModified(subsist,modifieds[_DataStruct_.Real_OrgOID] == 1);
                OnRoleRIDModified(subsist,modifieds[_DataStruct_.Real_RoleRID] == 1);
                OnEmpIDModified(subsist,modifieds[_DataStruct_.Real_EmpID] == 1);
                OnStateLoginModified(subsist,modifieds[_DataStruct_.Real_StateLogin] == 1);
                OnStateDeleteModified(subsist,modifieds[_DataStruct_.Real_StateDelete] == 1);
                OnEmployeeNameModified(subsist,modifieds[_DataStruct_.Real_EmployeeName] == 1);
                OnPasswordModified(subsist,modifieds[_DataStruct_.Real_Password] == 1);
                OnTokenModified(subsist,modifieds[_DataStruct_.Real_Token] == 1);
                OnGenderModified(subsist,modifieds[_DataStruct_.Real_Gender] == 1);
                OnPhoneModified(subsist,modifieds[_DataStruct_.Real_Phone] == 1);
                OnEmailModified(subsist,modifieds[_DataStruct_.Real_Email] == 1);
                OnRemarkModified(subsist,modifieds[_DataStruct_.Real_Remark] == 1);
                OnErrorTimesModified(subsist,modifieds[_DataStruct_.Real_ErrorTimes] == 1);
                OnLastLoginDateModified(subsist,modifieds[_DataStruct_.Real_LastLoginDate] == 1);
                OnLastLoginIPModified(subsist,modifieds[_DataStruct_.Real_LastLoginIP] == 1);
                OnAddDateModified(subsist,modifieds[_DataStruct_.Real_AddDate] == 1);
            }
        }

        /// <summary>
        /// 主键修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnEIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 站点标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnSiteSIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 组织标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOrgOIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 角色扮演修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnRoleRIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// EMP=35782；修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnEmpIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 状态登录修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnStateLoginModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 状态删除修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnStateDeleteModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 员工姓名修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnEmployeeNameModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 密码修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnPasswordModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 令牌修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnTokenModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 性别修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnGenderModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 电话修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnPhoneModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 电子邮件修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnEmailModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 备注修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnRemarkModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 误差时间修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnErrorTimesModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 上次登录日期修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnLastLoginDateModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 最后登录IP修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnLastLoginIPModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 添加日期修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAddDateModified(EntitySubsist subsist,bool isModified);
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
            public const string EntityName = @"Employee";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"员工";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"员工";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0x0;
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityPrimaryKey = "EID";
            
            
            /// <summary>
            /// 主键的数字标识
            /// </summary>
            public const byte EID = 1;
            
            /// <summary>
            /// 主键的实时记录顺序
            /// </summary>
            public const int Real_EID = 0;

            /// <summary>
            /// 站点标识的数字标识
            /// </summary>
            public const byte SiteSID = 2;
            
            /// <summary>
            /// 站点标识的实时记录顺序
            /// </summary>
            public const int Real_SiteSID = 1;

            /// <summary>
            /// 组织标识的数字标识
            /// </summary>
            public const byte OrgOID = 3;
            
            /// <summary>
            /// 组织标识的实时记录顺序
            /// </summary>
            public const int Real_OrgOID = 2;

            /// <summary>
            /// 角色扮演的数字标识
            /// </summary>
            public const byte RoleRID = 4;
            
            /// <summary>
            /// 角色扮演的实时记录顺序
            /// </summary>
            public const int Real_RoleRID = 3;

            /// <summary>
            /// EMP=35782；的数字标识
            /// </summary>
            public const byte EmpID = 5;
            
            /// <summary>
            /// EMP=35782；的实时记录顺序
            /// </summary>
            public const int Real_EmpID = 4;

            /// <summary>
            /// 状态登录的数字标识
            /// </summary>
            public const byte StateLogin = 6;
            
            /// <summary>
            /// 状态登录的实时记录顺序
            /// </summary>
            public const int Real_StateLogin = 5;

            /// <summary>
            /// 状态删除的数字标识
            /// </summary>
            public const byte StateDelete = 7;
            
            /// <summary>
            /// 状态删除的实时记录顺序
            /// </summary>
            public const int Real_StateDelete = 6;

            /// <summary>
            /// 员工姓名的数字标识
            /// </summary>
            public const byte EmployeeName = 8;
            
            /// <summary>
            /// 员工姓名的实时记录顺序
            /// </summary>
            public const int Real_EmployeeName = 7;

            /// <summary>
            /// 密码的数字标识
            /// </summary>
            public const byte Password = 9;
            
            /// <summary>
            /// 密码的实时记录顺序
            /// </summary>
            public const int Real_Password = 8;

            /// <summary>
            /// 令牌的数字标识
            /// </summary>
            public const byte Token = 10;
            
            /// <summary>
            /// 令牌的实时记录顺序
            /// </summary>
            public const int Real_Token = 9;

            /// <summary>
            /// 性别的数字标识
            /// </summary>
            public const byte Gender = 11;
            
            /// <summary>
            /// 性别的实时记录顺序
            /// </summary>
            public const int Real_Gender = 10;

            /// <summary>
            /// 电话的数字标识
            /// </summary>
            public const byte Phone = 12;
            
            /// <summary>
            /// 电话的实时记录顺序
            /// </summary>
            public const int Real_Phone = 11;

            /// <summary>
            /// 电子邮件的数字标识
            /// </summary>
            public const byte Email = 13;
            
            /// <summary>
            /// 电子邮件的实时记录顺序
            /// </summary>
            public const int Real_Email = 12;

            /// <summary>
            /// 备注的数字标识
            /// </summary>
            public const byte Remark = 14;
            
            /// <summary>
            /// 备注的实时记录顺序
            /// </summary>
            public const int Real_Remark = 13;

            /// <summary>
            /// 误差时间的数字标识
            /// </summary>
            public const byte ErrorTimes = 15;
            
            /// <summary>
            /// 误差时间的实时记录顺序
            /// </summary>
            public const int Real_ErrorTimes = 14;

            /// <summary>
            /// 上次登录日期的数字标识
            /// </summary>
            public const byte LastLoginDate = 16;
            
            /// <summary>
            /// 上次登录日期的实时记录顺序
            /// </summary>
            public const int Real_LastLoginDate = 15;

            /// <summary>
            /// 最后登录IP的数字标识
            /// </summary>
            public const byte LastLoginIP = 17;
            
            /// <summary>
            /// 最后登录IP的实时记录顺序
            /// </summary>
            public const int Real_LastLoginIP = 16;

            /// <summary>
            /// 添加日期的数字标识
            /// </summary>
            public const byte AddDate = 18;
            
            /// <summary>
            /// 添加日期的实时记录顺序
            /// </summary>
            public const int Real_AddDate = 17;

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
                        Real_EID,
                        new PropertySturct
                        {
                            Index        = EID,
                            Name         = "EID",
                            Title        = "主键",
                            Caption      = @"主键",
                            Description  = @"主键",
                            ColumnName   = "EID",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_SiteSID,
                        new PropertySturct
                        {
                            Index        = SiteSID,
                            Name         = "SiteSID",
                            Title        = "站点标识",
                            Caption      = @"站点标识",
                            Description  = @"站点标识",
                            ColumnName   = "SiteSID",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_OrgOID,
                        new PropertySturct
                        {
                            Index        = OrgOID,
                            Name         = "OrgOID",
                            Title        = "组织标识",
                            Caption      = @"组织标识",
                            Description  = @"组织标识",
                            ColumnName   = "OrgOID",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_RoleRID,
                        new PropertySturct
                        {
                            Index        = RoleRID,
                            Name         = "RoleRID",
                            Title        = "角色扮演",
                            Caption      = @"角色扮演",
                            Description  = @"角色扮演",
                            ColumnName   = "RoleRID",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_EmpID,
                        new PropertySturct
                        {
                            Index        = EmpID,
                            Name         = "EmpID",
                            Title        = "EMP=35782；",
                            Caption      = @"EMP=35782；",
                            Description  = @"EMP=35782；",
                            ColumnName   = "EmpID",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_StateLogin,
                        new PropertySturct
                        {
                            Index        = StateLogin,
                            Name         = "StateLogin",
                            Title        = "状态登录",
                            Caption      = @"状态登录",
                            Description  = @"状态登录",
                            ColumnName   = "StateLogin",
                            PropertyType = typeof(bool),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_StateDelete,
                        new PropertySturct
                        {
                            Index        = StateDelete,
                            Name         = "StateDelete",
                            Title        = "状态删除",
                            Caption      = @"状态删除",
                            Description  = @"状态删除",
                            ColumnName   = "StateDelete",
                            PropertyType = typeof(bool),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_EmployeeName,
                        new PropertySturct
                        {
                            Index        = EmployeeName,
                            Name         = "EmployeeName",
                            Title        = "员工姓名",
                            Caption      = @"员工姓名",
                            Description  = @"员工姓名",
                            ColumnName   = "EmployeeName",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Password,
                        new PropertySturct
                        {
                            Index        = Password,
                            Name         = "Password",
                            Title        = "密码",
                            Caption      = @"密码",
                            Description  = @"密码",
                            ColumnName   = "Password",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Token,
                        new PropertySturct
                        {
                            Index        = Token,
                            Name         = "Token",
                            Title        = "令牌",
                            Caption      = @"令牌",
                            Description  = @"令牌",
                            ColumnName   = "Token",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Gender,
                        new PropertySturct
                        {
                            Index        = Gender,
                            Name         = "Gender",
                            Title        = "性别",
                            Caption      = @"性别",
                            Description  = @"性别",
                            ColumnName   = "Gender",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Phone,
                        new PropertySturct
                        {
                            Index        = Phone,
                            Name         = "Phone",
                            Title        = "电话",
                            Caption      = @"电话",
                            Description  = @"电话",
                            ColumnName   = "Phone",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Email,
                        new PropertySturct
                        {
                            Index        = Email,
                            Name         = "Email",
                            Title        = "电子邮件",
                            Caption      = @"电子邮件",
                            Description  = @"电子邮件",
                            ColumnName   = "Email",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Remark,
                        new PropertySturct
                        {
                            Index        = Remark,
                            Name         = "Remark",
                            Title        = "备注",
                            Caption      = @"备注",
                            Description  = @"备注",
                            ColumnName   = "Remark",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_ErrorTimes,
                        new PropertySturct
                        {
                            Index        = ErrorTimes,
                            Name         = "ErrorTimes",
                            Title        = "误差时间",
                            Caption      = @"误差时间",
                            Description  = @"误差时间",
                            ColumnName   = "ErrorTimes",
                            PropertyType = typeof(int),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_LastLoginDate,
                        new PropertySturct
                        {
                            Index        = LastLoginDate,
                            Name         = "LastLoginDate",
                            Title        = "上次登录日期",
                            Caption      = @"上次登录日期",
                            Description  = @"上次登录日期",
                            ColumnName   = "LastLoginDate",
                            PropertyType = typeof(DateTime),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_LastLoginIP,
                        new PropertySturct
                        {
                            Index        = LastLoginIP,
                            Name         = "LastLoginIP",
                            Title        = "最后登录IP",
                            Caption      = @"最后登录IP",
                            Description  = @"最后登录IP",
                            ColumnName   = "LastLoginIP",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
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
                            Title        = "添加日期",
                            Caption      = @"添加日期",
                            Description  = @"添加日期",
                            ColumnName   = "AddDate",
                            PropertyType = typeof(DateTime),
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