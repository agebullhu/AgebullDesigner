/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/4/10 10:44:52*/
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
    /// 用户
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class UserData : IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public UserData()
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
        public void ChangePrimaryKey(long uID)
        {
            _uID = uID;
        }
        /// <summary>
        /// 主键
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _uID;

        partial void OnUIDGet();

        partial void OnUIDSet(ref long value);

        partial void OnUIDLoad(ref long value);

        partial void OnUIDSeted();

        
        /// <summary>
        /// 主键
        /// </summary>
        /// <remarks>
        /// 逻辑ID
        /// </remarks>
        [DataMember , JsonProperty("UID", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"主键")]
        public long UID
        {
            get
            {
                OnUIDGet();
                return this._uID;
            }
            set
            {
                if(this._uID == value)
                    return;
                //if(this._uID > 0)
                //    throw new Exception("主键一旦设置就不可以修改");
                OnUIDSet(ref value);
                this._uID = value;
                this.OnPropertyChanged(_DataStruct_.Real_UID);
                OnUIDSeted();
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
        /// referee用户标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _refereeUID;

        partial void OnRefereeUIDGet();

        partial void OnRefereeUIDSet(ref long value);

        partial void OnRefereeUIDSeted();

        
        /// <summary>
        /// referee用户标识
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("RefereeUID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"referee用户标识")]
        public  long RefereeUID
        {
            get
            {
                OnRefereeUIDGet();
                return this._refereeUID;
            }
            set
            {
                if(this._refereeUID == value)
                    return;
                OnRefereeUIDSet(ref value);
                this._refereeUID = value;
                OnRefereeUIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_RefereeUID);
            }
        }
        /// <summary>
        /// 用户端
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _userID;

        partial void OnUserIDGet();

        partial void OnUserIDSet(ref string value);

        partial void OnUserIDSeted();

        
        /// <summary>
        /// 用户端
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("UserID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"用户端")]
        public  string UserID
        {
            get
            {
                OnUserIDGet();
                return this._userID;
            }
            set
            {
                if(this._userID == value)
                    return;
                OnUserIDSet(ref value);
                this._userID = value;
                OnUserIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_UserID);
            }
        }
        /// <summary>
        /// wx应用标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _wx_appid;

        partial void Onwx_appidGet();

        partial void Onwx_appidSet(ref string value);

        partial void Onwx_appidSeted();

        
        /// <summary>
        /// wx应用标识
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("wx_appid", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"wx应用标识")]
        public  string wx_appid
        {
            get
            {
                Onwx_appidGet();
                return this._wx_appid;
            }
            set
            {
                if(this._wx_appid == value)
                    return;
                Onwx_appidSet(ref value);
                this._wx_appid = value;
                Onwx_appidSeted();
                this.OnPropertyChanged(_DataStruct_.Real_wx_appid);
            }
        }
        /// <summary>
        /// WXOpenID
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _wx_openid;

        partial void Onwx_openidGet();

        partial void Onwx_openidSet(ref string value);

        partial void Onwx_openidSeted();

        
        /// <summary>
        /// WXOpenID
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("wx_openid", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"WXOpenID")]
        public  string wx_openid
        {
            get
            {
                Onwx_openidGet();
                return this._wx_openid;
            }
            set
            {
                if(this._wx_openid == value)
                    return;
                Onwx_openidSet(ref value);
                this._wx_openid = value;
                Onwx_openidSeted();
                this.OnPropertyChanged(_DataStruct_.Real_wx_openid);
            }
        }
        /// <summary>
        /// WX工会会员
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _wx_unionid;

        partial void Onwx_unionidGet();

        partial void Onwx_unionidSet(ref string value);

        partial void Onwx_unionidSeted();

        
        /// <summary>
        /// WX工会会员
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("wx_unionid", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"WX工会会员")]
        public  string wx_unionid
        {
            get
            {
                Onwx_unionidGet();
                return this._wx_unionid;
            }
            set
            {
                if(this._wx_unionid == value)
                    return;
                Onwx_unionidSet(ref value);
                this._wx_unionid = value;
                Onwx_unionidSeted();
                this.OnPropertyChanged(_DataStruct_.Real_wx_unionid);
            }
        }
        /// <summary>
        /// WX群
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public float _wx_groupid;

        partial void Onwx_groupidGet();

        partial void Onwx_groupidSet(ref float value);

        partial void Onwx_groupidSeted();

        
        /// <summary>
        /// WX群
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("wx_groupid", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"WX群")]
        public  float wx_groupid
        {
            get
            {
                Onwx_groupidGet();
                return this._wx_groupid;
            }
            set
            {
                if(this._wx_groupid == value)
                    return;
                Onwx_groupidSet(ref value);
                this._wx_groupid = value;
                Onwx_groupidSeted();
                this.OnPropertyChanged(_DataStruct_.Real_wx_groupid);
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
        /// 会话全局标识MP
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _mPSessionKey;

        partial void OnMPSessionKeyGet();

        partial void OnMPSessionKeySet(ref string value);

        partial void OnMPSessionKeySeted();

        
        /// <summary>
        /// 会话全局标识MP
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("MPSessionKey", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"会话全局标识MP")]
        public  string MPSessionKey
        {
            get
            {
                OnMPSessionKeyGet();
                return this._mPSessionKey;
            }
            set
            {
                if(this._mPSessionKey == value)
                    return;
                OnMPSessionKeySet(ref value);
                this._mPSessionKey = value;
                OnMPSessionKeySeted();
                this.OnPropertyChanged(_DataStruct_.Real_MPSessionKey);
            }
        }
        /// <summary>
        /// 标签：法典
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _mPQRCodeImgUrl;

        partial void OnMPQRCodeImgUrlGet();

        partial void OnMPQRCodeImgUrlSet(ref string value);

        partial void OnMPQRCodeImgUrlSeted();

        
        /// <summary>
        /// 标签：法典
        /// </summary>
        /// <value>
        /// 可存储1000个字符.合理长度应不大于1000.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("MPQRCodeImgUrl", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"标签：法典")]
        public  string MPQRCodeImgUrl
        {
            get
            {
                OnMPQRCodeImgUrlGet();
                return this._mPQRCodeImgUrl;
            }
            set
            {
                if(this._mPQRCodeImgUrl == value)
                    return;
                OnMPQRCodeImgUrlSet(ref value);
                this._mPQRCodeImgUrl = value;
                OnMPQRCodeImgUrlSeted();
                this.OnPropertyChanged(_DataStruct_.Real_MPQRCodeImgUrl);
            }
        }
        /// <summary>
        /// 用户口令
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _userPassword;

        partial void OnUserPasswordGet();

        partial void OnUserPasswordSet(ref string value);

        partial void OnUserPasswordSeted();

        
        /// <summary>
        /// 用户口令
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("UserPassword", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"用户口令")]
        public  string UserPassword
        {
            get
            {
                OnUserPasswordGet();
                return this._userPassword;
            }
            set
            {
                if(this._userPassword == value)
                    return;
                OnUserPasswordSet(ref value);
                this._userPassword = value;
                OnUserPasswordSeted();
                this.OnPropertyChanged(_DataStruct_.Real_UserPassword);
            }
        }
        /// <summary>
        /// 移动电话
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _mobilePhone;

        partial void OnMobilePhoneGet();

        partial void OnMobilePhoneSet(ref string value);

        partial void OnMobilePhoneSeted();

        
        /// <summary>
        /// 移动电话
        /// </summary>
        /// <value>
        /// 可存储16个字符.合理长度应不大于16.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("MobilePhone", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"移动电话")]
        public  string MobilePhone
        {
            get
            {
                OnMobilePhoneGet();
                return this._mobilePhone;
            }
            set
            {
                if(this._mobilePhone == value)
                    return;
                OnMobilePhoneSet(ref value);
                this._mobilePhone = value;
                OnMobilePhoneSeted();
                this.OnPropertyChanged(_DataStruct_.Real_MobilePhone);
            }
        }
        /// <summary>
        /// 纯手机
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _mobilePhonePure;

        partial void OnMobilePhonePureGet();

        partial void OnMobilePhonePureSet(ref string value);

        partial void OnMobilePhonePureSeted();

        
        /// <summary>
        /// 纯手机
        /// </summary>
        /// <value>
        /// 可存储16个字符.合理长度应不大于16.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("MobilePhonePure", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"纯手机")]
        public  string MobilePhonePure
        {
            get
            {
                OnMobilePhonePureGet();
                return this._mobilePhonePure;
            }
            set
            {
                if(this._mobilePhonePure == value)
                    return;
                OnMobilePhonePureSet(ref value);
                this._mobilePhonePure = value;
                OnMobilePhonePureSeted();
                this.OnPropertyChanged(_DataStruct_.Real_MobilePhonePure);
            }
        }
        /// <summary>
        /// 手机国家代码
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _mobilePhoneCountryCode;

        partial void OnMobilePhoneCountryCodeGet();

        partial void OnMobilePhoneCountryCodeSet(ref string value);

        partial void OnMobilePhoneCountryCodeSeted();

        
        /// <summary>
        /// 手机国家代码
        /// </summary>
        /// <value>
        /// 可存储16个字符.合理长度应不大于16.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("MobilePhoneCountryCode", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"手机国家代码")]
        public  string MobilePhoneCountryCode
        {
            get
            {
                OnMobilePhoneCountryCodeGet();
                return this._mobilePhoneCountryCode;
            }
            set
            {
                if(this._mobilePhoneCountryCode == value)
                    return;
                OnMobilePhoneCountryCodeSet(ref value);
                this._mobilePhoneCountryCode = value;
                OnMobilePhoneCountryCodeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_MobilePhoneCountryCode);
            }
        }
        /// <summary>
        /// 昵称
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _nickName;

        partial void OnnickNameGet();

        partial void OnnickNameSet(ref string value);

        partial void OnnickNameSeted();

        
        /// <summary>
        /// 昵称
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("nickName", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"昵称")]
        public  string nickName
        {
            get
            {
                OnnickNameGet();
                return this._nickName;
            }
            set
            {
                if(this._nickName == value)
                    return;
                OnnickNameSet(ref value);
                this._nickName = value;
                OnnickNameSeted();
                this.OnPropertyChanged(_DataStruct_.Real_nickName);
            }
        }
        /// <summary>
        /// 性别
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public bool _gender;

        partial void OngenderGet();

        partial void OngenderSet(ref bool value);

        partial void OngenderSeted();

        
        /// <summary>
        /// 性别
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("gender", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"性别")]
        public  bool gender
        {
            get
            {
                OngenderGet();
                return this._gender;
            }
            set
            {
                if(this._gender == value)
                    return;
                OngenderSet(ref value);
                this._gender = value;
                OngenderSeted();
                this.OnPropertyChanged(_DataStruct_.Real_gender);
            }
        }
        /// <summary>
        /// 2.Avatar第38142号；
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _avatarUrl;

        partial void OnavatarUrlGet();

        partial void OnavatarUrlSet(ref string value);

        partial void OnavatarUrlSeted();

        
        /// <summary>
        /// 2.Avatar第38142号；
        /// </summary>
        /// <value>
        /// 可存储1000个字符.合理长度应不大于1000.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("avatarUrl", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"2.Avatar第38142号；")]
        public  string avatarUrl
        {
            get
            {
                OnavatarUrlGet();
                return this._avatarUrl;
            }
            set
            {
                if(this._avatarUrl == value)
                    return;
                OnavatarUrlSet(ref value);
                this._avatarUrl = value;
                OnavatarUrlSeted();
                this.OnPropertyChanged(_DataStruct_.Real_avatarUrl);
            }
        }
        /// <summary>
        /// 语言
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _language;

        partial void OnlanguageGet();

        partial void OnlanguageSet(ref string value);

        partial void OnlanguageSeted();

        
        /// <summary>
        /// 语言
        /// </summary>
        /// <value>
        /// 可存储20个字符.合理长度应不大于20.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("language", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"语言")]
        public  string language
        {
            get
            {
                OnlanguageGet();
                return this._language;
            }
            set
            {
                if(this._language == value)
                    return;
                OnlanguageSet(ref value);
                this._language = value;
                OnlanguageSeted();
                this.OnPropertyChanged(_DataStruct_.Real_language);
            }
        }
        /// <summary>
        /// 城市
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _city;

        partial void OncityGet();

        partial void OncitySet(ref string value);

        partial void OncitySeted();

        
        /// <summary>
        /// 城市
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("city", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"城市")]
        public  string city
        {
            get
            {
                OncityGet();
                return this._city;
            }
            set
            {
                if(this._city == value)
                    return;
                OncitySet(ref value);
                this._city = value;
                OncitySeted();
                this.OnPropertyChanged(_DataStruct_.Real_city);
            }
        }
        /// <summary>
        /// 省份
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _province;

        partial void OnprovinceGet();

        partial void OnprovinceSet(ref string value);

        partial void OnprovinceSeted();

        
        /// <summary>
        /// 省份
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("province", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"省份")]
        public  string province
        {
            get
            {
                OnprovinceGet();
                return this._province;
            }
            set
            {
                if(this._province == value)
                    return;
                OnprovinceSet(ref value);
                this._province = value;
                OnprovinceSeted();
                this.OnPropertyChanged(_DataStruct_.Real_province);
            }
        }
        /// <summary>
        /// 国家
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _country;

        partial void OncountryGet();

        partial void OncountrySet(ref string value);

        partial void OncountrySeted();

        
        /// <summary>
        /// 国家
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("country", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"国家")]
        public  string country
        {
            get
            {
                OncountryGet();
                return this._country;
            }
            set
            {
                if(this._country == value)
                    return;
                OncountrySet(ref value);
                this._country = value;
                OncountrySeted();
                this.OnPropertyChanged(_DataStruct_.Real_country);
            }
        }
        /// <summary>
        /// 备注
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _remark;

        partial void OnremarkGet();

        partial void OnremarkSet(ref string value);

        partial void OnremarkSeted();

        
        /// <summary>
        /// 备注
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("remark", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"备注")]
        public  string remark
        {
            get
            {
                OnremarkGet();
                return this._remark;
            }
            set
            {
                if(this._remark == value)
                    return;
                OnremarkSet(ref value);
                this._remark = value;
                OnremarkSeted();
                this.OnPropertyChanged(_DataStruct_.Real_remark);
            }
        }
        /// <summary>
        /// 场景叠加
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public int _sceneID;

        partial void OnSceneIDGet();

        partial void OnSceneIDSet(ref int value);

        partial void OnSceneIDSeted();

        
        /// <summary>
        /// 场景叠加
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("SceneID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"场景叠加")]
        public  int SceneID
        {
            get
            {
                OnSceneIDGet();
                return this._sceneID;
            }
            set
            {
                if(this._sceneID == value)
                    return;
                OnSceneIDSet(ref value);
                this._sceneID = value;
                OnSceneIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_SceneID);
            }
        }
        /// <summary>
        /// 从注册
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _registerFrom;

        partial void OnRegisterFromGet();

        partial void OnRegisterFromSet(ref string value);

        partial void OnRegisterFromSeted();

        
        /// <summary>
        /// 从注册
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("RegisterFrom", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"从注册")]
        public  string RegisterFrom
        {
            get
            {
                OnRegisterFromGet();
                return this._registerFrom;
            }
            set
            {
                if(this._registerFrom == value)
                    return;
                OnRegisterFromSet(ref value);
                this._registerFrom = value;
                OnRegisterFromSeted();
                this.OnPropertyChanged(_DataStruct_.Real_RegisterFrom);
            }
        }
        /// <summary>
        /// 注册时间
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public DateTime _registerTime;

        partial void OnRegisterTimeGet();

        partial void OnRegisterTimeSet(ref DateTime value);

        partial void OnRegisterTimeSeted();

        
        /// <summary>
        /// 注册时间
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("RegisterTime", NullValueHandling = NullValueHandling.Ignore) , JsonConverter(typeof(MyDateTimeConverter)) , DisplayName(@"注册时间")]
        public  DateTime RegisterTime
        {
            get
            {
                OnRegisterTimeGet();
                return this._registerTime;
            }
            set
            {
                if(this._registerTime == value)
                    return;
                OnRegisterTimeSet(ref value);
                this._registerTime = value;
                OnRegisterTimeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_RegisterTime);
            }
        }
        /// <summary>
        /// 订阅
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public bool _subscribe;

        partial void OnsubscribeGet();

        partial void OnsubscribeSet(ref bool value);

        partial void OnsubscribeSeted();

        
        /// <summary>
        /// 订阅
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("subscribe", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"订阅")]
        public  bool subscribe
        {
            get
            {
                OnsubscribeGet();
                return this._subscribe;
            }
            set
            {
                if(this._subscribe == value)
                    return;
                OnsubscribeSet(ref value);
                this._subscribe = value;
                OnsubscribeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_subscribe);
            }
        }
        /// <summary>
        /// 订阅时间
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _subscribe_time;

        partial void Onsubscribe_timeGet();

        partial void Onsubscribe_timeSet(ref string value);

        partial void Onsubscribe_timeSeted();

        
        /// <summary>
        /// 订阅时间
        /// </summary>
        /// <value>
        /// 可存储50个字符.合理长度应不大于50.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("subscribe_time", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"订阅时间")]
        public  string subscribe_time
        {
            get
            {
                Onsubscribe_timeGet();
                return this._subscribe_time;
            }
            set
            {
                if(this._subscribe_time == value)
                    return;
                Onsubscribe_timeSet(ref value);
                this._subscribe_time = value;
                Onsubscribe_timeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_subscribe_time);
            }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _userName;

        partial void OnUserNameGet();

        partial void OnUserNameSet(ref string value);

        partial void OnUserNameSeted();

        
        /// <summary>
        /// 用户名
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("UserName", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"用户名")]
        public  string UserName
        {
            get
            {
                OnUserNameGet();
                return this._userName;
            }
            set
            {
                if(this._userName == value)
                    return;
                OnUserNameSet(ref value);
                this._userName = value;
                OnUserNameSeted();
                this.OnPropertyChanged(_DataStruct_.Real_UserName);
            }
        }
        /// <summary>
        /// 用户面
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _userFace;

        partial void OnUserFaceGet();

        partial void OnUserFaceSet(ref string value);

        partial void OnUserFaceSeted();

        
        /// <summary>
        /// 用户面
        /// </summary>
        /// <value>
        /// 可存储256个字符.合理长度应不大于256.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("UserFace", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"用户面")]
        public  string UserFace
        {
            get
            {
                OnUserFaceGet();
                return this._userFace;
            }
            set
            {
                if(this._userFace == value)
                    return;
                OnUserFaceSet(ref value);
                this._userFace = value;
                OnUserFaceSeted();
                this.OnPropertyChanged(_DataStruct_.Real_UserFace);
            }
        }
        /// <summary>
        /// 年龄
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public int _age;

        partial void OnAgeGet();

        partial void OnAgeSet(ref int value);

        partial void OnAgeSeted();

        
        /// <summary>
        /// 年龄
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Age", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"年龄")]
        public  int Age
        {
            get
            {
                OnAgeGet();
                return this._age;
            }
            set
            {
                if(this._age == value)
                    return;
                OnAgeSet(ref value);
                this._age = value;
                OnAgeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Age);
            }
        }
        /// <summary>
        /// 生日
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public DateTime _birthday;

        partial void OnBirthdayGet();

        partial void OnBirthdaySet(ref DateTime value);

        partial void OnBirthdaySeted();

        
        /// <summary>
        /// 生日
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Birthday", NullValueHandling = NullValueHandling.Ignore) , JsonConverter(typeof(MyDateTimeConverter)) , DisplayName(@"生日")]
        public  DateTime Birthday
        {
            get
            {
                OnBirthdayGet();
                return this._birthday;
            }
            set
            {
                if(this._birthday == value)
                    return;
                OnBirthdaySet(ref value);
                this._birthday = value;
                OnBirthdaySeted();
                this.OnPropertyChanged(_DataStruct_.Real_Birthday);
            }
        }
        /// <summary>
        /// 住宅信息
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _houseInfo;

        partial void OnHouseInfoGet();

        partial void OnHouseInfoSet(ref string value);

        partial void OnHouseInfoSeted();

        
        /// <summary>
        /// 住宅信息
        /// </summary>
        /// <value>
        /// 可存储20个字符.合理长度应不大于20.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("HouseInfo", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"住宅信息")]
        public  string HouseInfo
        {
            get
            {
                OnHouseInfoGet();
                return this._houseInfo;
            }
            set
            {
                if(this._houseInfo == value)
                    return;
                OnHouseInfoSet(ref value);
                this._houseInfo = value;
                OnHouseInfoSeted();
                this.OnPropertyChanged(_DataStruct_.Real_HouseInfo);
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
        /// 可存储400个字符.合理长度应不大于400.
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
        /// 海特
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public int _hight;

        partial void OnHightGet();

        partial void OnHightSet(ref int value);

        partial void OnHightSeted();

        
        /// <summary>
        /// 海特
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Hight", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"海特")]
        public  int Hight
        {
            get
            {
                OnHightGet();
                return this._hight;
            }
            set
            {
                if(this._hight == value)
                    return;
                OnHightSet(ref value);
                this._hight = value;
                OnHightSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Hight);
            }
        }
        /// <summary>
        /// 重量
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public float _weight;

        partial void OnWeightGet();

        partial void OnWeightSet(ref float value);

        partial void OnWeightSeted();

        
        /// <summary>
        /// 重量
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Weight", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"重量")]
        public  float Weight
        {
            get
            {
                OnWeightGet();
                return this._weight;
            }
            set
            {
                if(this._weight == value)
                    return;
                OnWeightSet(ref value);
                this._weight = value;
                OnWeightSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Weight);
            }
        }
        /// <summary>
        /// QQID
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _qQID;

        partial void OnQQIDGet();

        partial void OnQQIDSet(ref string value);

        partial void OnQQIDSeted();

        
        /// <summary>
        /// QQID
        /// </summary>
        /// <value>
        /// 可存储40个字符.合理长度应不大于40.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("QQID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"QQID")]
        public  string QQID
        {
            get
            {
                OnQQIDGet();
                return this._qQID;
            }
            set
            {
                if(this._qQID == value)
                    return;
                OnQQIDSet(ref value);
                this._qQID = value;
                OnQQIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_QQID);
            }
        }
        /// <summary>
        /// WXID
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _wXID;

        partial void OnWXIDGet();

        partial void OnWXIDSet(ref string value);

        partial void OnWXIDSeted();

        
        /// <summary>
        /// WXID
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("WXID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"WXID")]
        public  string WXID
        {
            get
            {
                OnWXIDGet();
                return this._wXID;
            }
            set
            {
                if(this._wXID == value)
                    return;
                OnWXIDSet(ref value);
                this._wXID = value;
                OnWXIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_WXID);
            }
        }
        /// <summary>
        /// 用户状态
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public bool _userState;

        partial void OnUserStateGet();

        partial void OnUserStateSet(ref bool value);

        partial void OnUserStateSeted();

        
        /// <summary>
        /// 用户状态
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("UserState", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"用户状态")]
        public  bool UserState
        {
            get
            {
                OnUserStateGet();
                return this._userState;
            }
            set
            {
                if(this._userState == value)
                    return;
                OnUserStateSet(ref value);
                this._userState = value;
                OnUserStateSeted();
                this.OnPropertyChanged(_DataStruct_.Real_UserState);
            }
        }
        /// <summary>
        /// 上次登录时间
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public DateTime _lastLoginTime;

        partial void OnLastLoginTimeGet();

        partial void OnLastLoginTimeSet(ref DateTime value);

        partial void OnLastLoginTimeSeted();

        
        /// <summary>
        /// 上次登录时间
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("LastLoginTime", NullValueHandling = NullValueHandling.Ignore) , JsonConverter(typeof(MyDateTimeConverter)) , DisplayName(@"上次登录时间")]
        public  DateTime LastLoginTime
        {
            get
            {
                OnLastLoginTimeGet();
                return this._lastLoginTime;
            }
            set
            {
                if(this._lastLoginTime == value)
                    return;
                OnLastLoginTimeSet(ref value);
                this._lastLoginTime = value;
                OnLastLoginTimeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_LastLoginTime);
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
        /// 推荐代码
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _recommendCode;

        partial void OnRecommendCodeGet();

        partial void OnRecommendCodeSet(ref string value);

        partial void OnRecommendCodeSeted();

        
        /// <summary>
        /// 推荐代码
        /// </summary>
        /// <value>
        /// 可存储50个字符.合理长度应不大于50.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("RecommendCode", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"推荐代码")]
        public  string RecommendCode
        {
            get
            {
                OnRecommendCodeGet();
                return this._recommendCode;
            }
            set
            {
                if(this._recommendCode == value)
                    return;
                OnRecommendCodeSet(ref value);
                this._recommendCode = value;
                OnRecommendCodeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_RecommendCode);
            }
        }
        /// <summary>
        /// 推荐使用标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _recommendUseID;

        partial void OnRecommendUseIDGet();

        partial void OnRecommendUseIDSet(ref long value);

        partial void OnRecommendUseIDSeted();

        
        /// <summary>
        /// 推荐使用标识
        /// </summary>
        /// <remarks>
        /// 推荐者
        /// </remarks>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("RecommendUseID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"推荐使用标识")]
        public  long RecommendUseID
        {
            get
            {
                OnRecommendUseIDGet();
                return this._recommendUseID;
            }
            set
            {
                if(this._recommendUseID == value)
                    return;
                OnRecommendUseIDSet(ref value);
                this._recommendUseID = value;
                OnRecommendUseIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_RecommendUseID);
            }
        }
        /// <summary>
        /// 加分
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public int _bonusPoints;

        partial void OnBonusPointsGet();

        partial void OnBonusPointsSet(ref int value);

        partial void OnBonusPointsSeted();

        
        /// <summary>
        /// 加分
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("BonusPoints", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"加分")]
        public  int BonusPoints
        {
            get
            {
                OnBonusPointsGet();
                return this._bonusPoints;
            }
            set
            {
                if(this._bonusPoints == value)
                    return;
                OnBonusPointsSet(ref value);
                this._bonusPoints = value;
                OnBonusPointsSeted();
                this.OnPropertyChanged(_DataStruct_.Real_BonusPoints);
            }
        }
        /// <summary>
        /// 代码票
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _codeTicket;

        partial void OnCodeTicketGet();

        partial void OnCodeTicketSet(ref string value);

        partial void OnCodeTicketSeted();

        
        /// <summary>
        /// 代码票
        /// </summary>
        /// <value>
        /// 可存储500个字符.合理长度应不大于500.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("CodeTicket", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"代码票")]
        public  string CodeTicket
        {
            get
            {
                OnCodeTicketGet();
                return this._codeTicket;
            }
            set
            {
                if(this._codeTicket == value)
                    return;
                OnCodeTicketSet(ref value);
                this._codeTicket = value;
                OnCodeTicketSeted();
                this.OnPropertyChanged(_DataStruct_.Real_CodeTicket);
            }
        }
        /// <summary>
        /// 快速语音信号
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public int _messagePromptType;

        partial void OnMessagePromptTypeGet();

        partial void OnMessagePromptTypeSet(ref int value);

        partial void OnMessagePromptTypeSeted();

        
        /// <summary>
        /// 快速语音信号
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("MessagePromptType", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"快速语音信号")]
        public  int MessagePromptType
        {
            get
            {
                OnMessagePromptTypeGet();
                return this._messagePromptType;
            }
            set
            {
                if(this._messagePromptType == value)
                    return;
                OnMessagePromptTypeSet(ref value);
                this._messagePromptType = value;
                OnMessagePromptTypeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_MessagePromptType);
            }
        }
        /// <summary>
        /// 上次消息提示
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public DateTime _messagePromptLastTime;

        partial void OnMessagePromptLastTimeGet();

        partial void OnMessagePromptLastTimeSet(ref DateTime value);

        partial void OnMessagePromptLastTimeSeted();

        
        /// <summary>
        /// 上次消息提示
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("MessagePromptLastTime", NullValueHandling = NullValueHandling.Ignore) , JsonConverter(typeof(MyDateTimeConverter)) , DisplayName(@"上次消息提示")]
        public  DateTime MessagePromptLastTime
        {
            get
            {
                OnMessagePromptLastTimeGet();
                return this._messagePromptLastTime;
            }
            set
            {
                if(this._messagePromptLastTime == value)
                    return;
                OnMessagePromptLastTimeSet(ref value);
                this._messagePromptLastTime = value;
                OnMessagePromptLastTimeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_MessagePromptLastTime);
            }
        }
        /// <summary>
        /// 定制区
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _customArea;

        partial void OnCustomAreaGet();

        partial void OnCustomAreaSet(ref string value);

        partial void OnCustomAreaSeted();

        
        /// <summary>
        /// 定制区
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("CustomArea", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"定制区")]
        public  string CustomArea
        {
            get
            {
                OnCustomAreaGet();
                return this._customArea;
            }
            set
            {
                if(this._customArea == value)
                    return;
                OnCustomAreaSet(ref value);
                this._customArea = value;
                OnCustomAreaSeted();
                this.OnPropertyChanged(_DataStruct_.Real_CustomArea);
            }
        }
        /// <summary>
        /// 本地省
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _nativePlaceProvince;

        partial void OnNativePlaceProvinceGet();

        partial void OnNativePlaceProvinceSet(ref string value);

        partial void OnNativePlaceProvinceSeted();

        
        /// <summary>
        /// 本地省
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("NativePlaceProvince", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"本地省")]
        public  string NativePlaceProvince
        {
            get
            {
                OnNativePlaceProvinceGet();
                return this._nativePlaceProvince;
            }
            set
            {
                if(this._nativePlaceProvince == value)
                    return;
                OnNativePlaceProvinceSet(ref value);
                this._nativePlaceProvince = value;
                OnNativePlaceProvinceSeted();
                this.OnPropertyChanged(_DataStruct_.Real_NativePlaceProvince);
            }
        }
        /// <summary>
        /// 本地城市
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _nativePlaceCity;

        partial void OnNativePlaceCityGet();

        partial void OnNativePlaceCitySet(ref string value);

        partial void OnNativePlaceCitySeted();

        
        /// <summary>
        /// 本地城市
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("NativePlaceCity", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"本地城市")]
        public  string NativePlaceCity
        {
            get
            {
                OnNativePlaceCityGet();
                return this._nativePlaceCity;
            }
            set
            {
                if(this._nativePlaceCity == value)
                    return;
                OnNativePlaceCitySet(ref value);
                this._nativePlaceCity = value;
                OnNativePlaceCitySeted();
                this.OnPropertyChanged(_DataStruct_.Real_NativePlaceCity);
            }
        }
        /// <summary>
        /// 本地区
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _nativePlaceArea;

        partial void OnNativePlaceAreaGet();

        partial void OnNativePlaceAreaSet(ref string value);

        partial void OnNativePlaceAreaSeted();

        
        /// <summary>
        /// 本地区
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("NativePlaceArea", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"本地区")]
        public  string NativePlaceArea
        {
            get
            {
                OnNativePlaceAreaGet();
                return this._nativePlaceArea;
            }
            set
            {
                if(this._nativePlaceArea == value)
                    return;
                OnNativePlaceAreaSet(ref value);
                this._nativePlaceArea = value;
                OnNativePlaceAreaSeted();
                this.OnPropertyChanged(_DataStruct_.Real_NativePlaceArea);
            }
        }
        /// <summary>
        /// 职业
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _profession;

        partial void OnProfessionGet();

        partial void OnProfessionSet(ref string value);

        partial void OnProfessionSeted();

        
        /// <summary>
        /// 职业
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Profession", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"职业")]
        public  string Profession
        {
            get
            {
                OnProfessionGet();
                return this._profession;
            }
            set
            {
                if(this._profession == value)
                    return;
                OnProfessionSet(ref value);
                this._profession = value;
                OnProfessionSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Profession);
            }
        }
        /// <summary>
        /// 上次离线访问时间
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public DateTime _timeVisitOfflineLast;

        partial void OnTimeVisitOfflineLastGet();

        partial void OnTimeVisitOfflineLastSet(ref DateTime value);

        partial void OnTimeVisitOfflineLastSeted();

        
        /// <summary>
        /// 上次离线访问时间
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("TimeVisitOfflineLast", NullValueHandling = NullValueHandling.Ignore) , JsonConverter(typeof(MyDateTimeConverter)) , DisplayName(@"上次离线访问时间")]
        public  DateTime TimeVisitOfflineLast
        {
            get
            {
                OnTimeVisitOfflineLastGet();
                return this._timeVisitOfflineLast;
            }
            set
            {
                if(this._timeVisitOfflineLast == value)
                    return;
                OnTimeVisitOfflineLastSet(ref value);
                this._timeVisitOfflineLast = value;
                OnTimeVisitOfflineLastSeted();
                this.OnPropertyChanged(_DataStruct_.Real_TimeVisitOfflineLast);
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
                return this.UID;
            }
            set
            {
                this.UID = value;
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
            case "uid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.UID = vl;
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
            case "refereeuid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.RefereeUID = vl;
                        return true;
                    }
                }
                return false;
            case "userid":
                this.UserID = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "wx_appid":
                this.wx_appid = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "wx_openid":
                this.wx_openid = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "wx_unionid":
                this.wx_unionid = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "wx_groupid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (float.TryParse(value, out var vl))
                    {
                        this.wx_groupid = vl;
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
            case "token":
                this.Token = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "mpsessionkey":
                this.MPSessionKey = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "mpqrcodeimgurl":
                this.MPQRCodeImgUrl = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "userpassword":
                this.UserPassword = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "mobilephone":
                this.MobilePhone = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "mobilephonepure":
                this.MobilePhonePure = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "mobilephonecountrycode":
                this.MobilePhoneCountryCode = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "nickname":
                this.nickName = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "gender":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (bool.TryParse(value, out var vl))
                    {
                        this.gender = vl;
                        return true;
                    }
                }
                return false;
            case "avatarurl":
                this.avatarUrl = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "language":
                this.language = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "city":
                this.city = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "province":
                this.province = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "country":
                this.country = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "remark":
                this.remark = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "sceneid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (int.TryParse(value, out var vl))
                    {
                        this.SceneID = vl;
                        return true;
                    }
                }
                return false;
            case "registerfrom":
                this.RegisterFrom = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "registertime":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (DateTime.TryParse(value, out var vl))
                    {
                        this.RegisterTime = vl;
                        return true;
                    }
                }
                return false;
            case "subscribe":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (bool.TryParse(value, out var vl))
                    {
                        this.subscribe = vl;
                        return true;
                    }
                }
                return false;
            case "subscribe_time":
                this.subscribe_time = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "username":
                this.UserName = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "userface":
                this.UserFace = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "age":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (int.TryParse(value, out var vl))
                    {
                        this.Age = vl;
                        return true;
                    }
                }
                return false;
            case "birthday":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (DateTime.TryParse(value, out var vl))
                    {
                        this.Birthday = vl;
                        return true;
                    }
                }
                return false;
            case "houseinfo":
                this.HouseInfo = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "email":
                this.Email = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "hight":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (int.TryParse(value, out var vl))
                    {
                        this.Hight = vl;
                        return true;
                    }
                }
                return false;
            case "weight":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (float.TryParse(value, out var vl))
                    {
                        this.Weight = vl;
                        return true;
                    }
                }
                return false;
            case "qqid":
                this.QQID = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "wxid":
                this.WXID = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "userstate":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (bool.TryParse(value, out var vl))
                    {
                        this.UserState = vl;
                        return true;
                    }
                }
                return false;
            case "lastlogintime":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (DateTime.TryParse(value, out var vl))
                    {
                        this.LastLoginTime = vl;
                        return true;
                    }
                }
                return false;
            case "lastloginip":
                this.LastLoginIP = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "recommendcode":
                this.RecommendCode = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "recommenduseid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.RecommendUseID = vl;
                        return true;
                    }
                }
                return false;
            case "bonuspoints":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (int.TryParse(value, out var vl))
                    {
                        this.BonusPoints = vl;
                        return true;
                    }
                }
                return false;
            case "codeticket":
                this.CodeTicket = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "messageprompttype":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (int.TryParse(value, out var vl))
                    {
                        this.MessagePromptType = vl;
                        return true;
                    }
                }
                return false;
            case "messagepromptlasttime":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (DateTime.TryParse(value, out var vl))
                    {
                        this.MessagePromptLastTime = vl;
                        return true;
                    }
                }
                return false;
            case "customarea":
                this.CustomArea = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "nativeplaceprovince":
                this.NativePlaceProvince = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "nativeplacecity":
                this.NativePlaceCity = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "nativeplacearea":
                this.NativePlaceArea = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "profession":
                this.Profession = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "timevisitofflinelast":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (DateTime.TryParse(value, out var vl))
                    {
                        this.TimeVisitOfflineLast = vl;
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
            case "uid":
                this.UID = (long)Convert.ToDecimal(value);
                return;
            case "sitesid":
                this.SiteSID = (long)Convert.ToDecimal(value);
                return;
            case "refereeuid":
                this.RefereeUID = (long)Convert.ToDecimal(value);
                return;
            case "userid":
                this.UserID = value == null ? null : value.ToString();
                return;
            case "wx_appid":
                this.wx_appid = value == null ? null : value.ToString();
                return;
            case "wx_openid":
                this.wx_openid = value == null ? null : value.ToString();
                return;
            case "wx_unionid":
                this.wx_unionid = value == null ? null : value.ToString();
                return;
            case "wx_groupid":
                this.wx_groupid = Convert.ToSingle(value);
                return;
            case "rolerid":
                this.RoleRID = (long)Convert.ToDecimal(value);
                return;
            case "token":
                this.Token = value == null ? null : value.ToString();
                return;
            case "mpsessionkey":
                this.MPSessionKey = value == null ? null : value.ToString();
                return;
            case "mpqrcodeimgurl":
                this.MPQRCodeImgUrl = value == null ? null : value.ToString();
                return;
            case "userpassword":
                this.UserPassword = value == null ? null : value.ToString();
                return;
            case "mobilephone":
                this.MobilePhone = value == null ? null : value.ToString();
                return;
            case "mobilephonepure":
                this.MobilePhonePure = value == null ? null : value.ToString();
                return;
            case "mobilephonecountrycode":
                this.MobilePhoneCountryCode = value == null ? null : value.ToString();
                return;
            case "nickname":
                this.nickName = value == null ? null : value.ToString();
                return;
            case "gender":
                if (value != null)
                {
                    int vl;
                    if (int.TryParse(value.ToString(), out vl))
                    {
                        this.gender = vl != 0;
                    }
                    else
                    {
                        this.gender = Convert.ToBoolean(value);
                    }
                }
                return;
            case "avatarurl":
                this.avatarUrl = value == null ? null : value.ToString();
                return;
            case "language":
                this.language = value == null ? null : value.ToString();
                return;
            case "city":
                this.city = value == null ? null : value.ToString();
                return;
            case "province":
                this.province = value == null ? null : value.ToString();
                return;
            case "country":
                this.country = value == null ? null : value.ToString();
                return;
            case "remark":
                this.remark = value == null ? null : value.ToString();
                return;
            case "sceneid":
                this.SceneID = (int)Convert.ToDecimal(value);
                return;
            case "registerfrom":
                this.RegisterFrom = value == null ? null : value.ToString();
                return;
            case "registertime":
                this.RegisterTime = Convert.ToDateTime(value);
                return;
            case "subscribe":
                if (value != null)
                {
                    int vl;
                    if (int.TryParse(value.ToString(), out vl))
                    {
                        this.subscribe = vl != 0;
                    }
                    else
                    {
                        this.subscribe = Convert.ToBoolean(value);
                    }
                }
                return;
            case "subscribe_time":
                this.subscribe_time = value == null ? null : value.ToString();
                return;
            case "username":
                this.UserName = value == null ? null : value.ToString();
                return;
            case "userface":
                this.UserFace = value == null ? null : value.ToString();
                return;
            case "age":
                this.Age = (int)Convert.ToDecimal(value);
                return;
            case "birthday":
                this.Birthday = Convert.ToDateTime(value);
                return;
            case "houseinfo":
                this.HouseInfo = value == null ? null : value.ToString();
                return;
            case "email":
                this.Email = value == null ? null : value.ToString();
                return;
            case "hight":
                this.Hight = (int)Convert.ToDecimal(value);
                return;
            case "weight":
                this.Weight = Convert.ToSingle(value);
                return;
            case "qqid":
                this.QQID = value == null ? null : value.ToString();
                return;
            case "wxid":
                this.WXID = value == null ? null : value.ToString();
                return;
            case "userstate":
                if (value != null)
                {
                    int vl;
                    if (int.TryParse(value.ToString(), out vl))
                    {
                        this.UserState = vl != 0;
                    }
                    else
                    {
                        this.UserState = Convert.ToBoolean(value);
                    }
                }
                return;
            case "lastlogintime":
                this.LastLoginTime = Convert.ToDateTime(value);
                return;
            case "lastloginip":
                this.LastLoginIP = value == null ? null : value.ToString();
                return;
            case "recommendcode":
                this.RecommendCode = value == null ? null : value.ToString();
                return;
            case "recommenduseid":
                this.RecommendUseID = (long)Convert.ToDecimal(value);
                return;
            case "bonuspoints":
                this.BonusPoints = (int)Convert.ToDecimal(value);
                return;
            case "codeticket":
                this.CodeTicket = value == null ? null : value.ToString();
                return;
            case "messageprompttype":
                this.MessagePromptType = (int)Convert.ToDecimal(value);
                return;
            case "messagepromptlasttime":
                this.MessagePromptLastTime = Convert.ToDateTime(value);
                return;
            case "customarea":
                this.CustomArea = value == null ? null : value.ToString();
                return;
            case "nativeplaceprovince":
                this.NativePlaceProvince = value == null ? null : value.ToString();
                return;
            case "nativeplacecity":
                this.NativePlaceCity = value == null ? null : value.ToString();
                return;
            case "nativeplacearea":
                this.NativePlaceArea = value == null ? null : value.ToString();
                return;
            case "profession":
                this.Profession = value == null ? null : value.ToString();
                return;
            case "timevisitofflinelast":
                this.TimeVisitOfflineLast = Convert.ToDateTime(value);
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
            case _DataStruct_.UID:
                this.UID = Convert.ToInt64(value);
                return;
            case _DataStruct_.SiteSID:
                this.SiteSID = Convert.ToInt64(value);
                return;
            case _DataStruct_.RefereeUID:
                this.RefereeUID = Convert.ToInt64(value);
                return;
            case _DataStruct_.UserID:
                this.UserID = value == null ? null : value.ToString();
                return;
            case _DataStruct_.wx_appid:
                this.wx_appid = value == null ? null : value.ToString();
                return;
            case _DataStruct_.wx_openid:
                this.wx_openid = value == null ? null : value.ToString();
                return;
            case _DataStruct_.wx_unionid:
                this.wx_unionid = value == null ? null : value.ToString();
                return;
            case _DataStruct_.wx_groupid:
                this.wx_groupid = Convert.ToSingle(value);
                return;
            case _DataStruct_.RoleRID:
                this.RoleRID = Convert.ToInt64(value);
                return;
            case _DataStruct_.Token:
                this.Token = value == null ? null : value.ToString();
                return;
            case _DataStruct_.MPSessionKey:
                this.MPSessionKey = value == null ? null : value.ToString();
                return;
            case _DataStruct_.MPQRCodeImgUrl:
                this.MPQRCodeImgUrl = value == null ? null : value.ToString();
                return;
            case _DataStruct_.UserPassword:
                this.UserPassword = value == null ? null : value.ToString();
                return;
            case _DataStruct_.MobilePhone:
                this.MobilePhone = value == null ? null : value.ToString();
                return;
            case _DataStruct_.MobilePhonePure:
                this.MobilePhonePure = value == null ? null : value.ToString();
                return;
            case _DataStruct_.MobilePhoneCountryCode:
                this.MobilePhoneCountryCode = value == null ? null : value.ToString();
                return;
            case _DataStruct_.nickName:
                this.nickName = value == null ? null : value.ToString();
                return;
            case _DataStruct_.gender:
                this.gender = Convert.ToBoolean(value);
                return;
            case _DataStruct_.avatarUrl:
                this.avatarUrl = value == null ? null : value.ToString();
                return;
            case _DataStruct_.language:
                this.language = value == null ? null : value.ToString();
                return;
            case _DataStruct_.city:
                this.city = value == null ? null : value.ToString();
                return;
            case _DataStruct_.province:
                this.province = value == null ? null : value.ToString();
                return;
            case _DataStruct_.country:
                this.country = value == null ? null : value.ToString();
                return;
            case _DataStruct_.remark:
                this.remark = value == null ? null : value.ToString();
                return;
            case _DataStruct_.SceneID:
                this.SceneID = Convert.ToInt32(value);
                return;
            case _DataStruct_.RegisterFrom:
                this.RegisterFrom = value == null ? null : value.ToString();
                return;
            case _DataStruct_.RegisterTime:
                this.RegisterTime = Convert.ToDateTime(value);
                return;
            case _DataStruct_.subscribe:
                this.subscribe = Convert.ToBoolean(value);
                return;
            case _DataStruct_.subscribe_time:
                this.subscribe_time = value == null ? null : value.ToString();
                return;
            case _DataStruct_.UserName:
                this.UserName = value == null ? null : value.ToString();
                return;
            case _DataStruct_.UserFace:
                this.UserFace = value == null ? null : value.ToString();
                return;
            case _DataStruct_.Age:
                this.Age = Convert.ToInt32(value);
                return;
            case _DataStruct_.Birthday:
                this.Birthday = Convert.ToDateTime(value);
                return;
            case _DataStruct_.HouseInfo:
                this.HouseInfo = value == null ? null : value.ToString();
                return;
            case _DataStruct_.Email:
                this.Email = value == null ? null : value.ToString();
                return;
            case _DataStruct_.Hight:
                this.Hight = Convert.ToInt32(value);
                return;
            case _DataStruct_.Weight:
                this.Weight = Convert.ToSingle(value);
                return;
            case _DataStruct_.QQID:
                this.QQID = value == null ? null : value.ToString();
                return;
            case _DataStruct_.WXID:
                this.WXID = value == null ? null : value.ToString();
                return;
            case _DataStruct_.UserState:
                this.UserState = Convert.ToBoolean(value);
                return;
            case _DataStruct_.LastLoginTime:
                this.LastLoginTime = Convert.ToDateTime(value);
                return;
            case _DataStruct_.LastLoginIP:
                this.LastLoginIP = value == null ? null : value.ToString();
                return;
            case _DataStruct_.RecommendCode:
                this.RecommendCode = value == null ? null : value.ToString();
                return;
            case _DataStruct_.RecommendUseID:
                this.RecommendUseID = Convert.ToInt64(value);
                return;
            case _DataStruct_.BonusPoints:
                this.BonusPoints = Convert.ToInt32(value);
                return;
            case _DataStruct_.CodeTicket:
                this.CodeTicket = value == null ? null : value.ToString();
                return;
            case _DataStruct_.MessagePromptType:
                this.MessagePromptType = Convert.ToInt32(value);
                return;
            case _DataStruct_.MessagePromptLastTime:
                this.MessagePromptLastTime = Convert.ToDateTime(value);
                return;
            case _DataStruct_.CustomArea:
                this.CustomArea = value == null ? null : value.ToString();
                return;
            case _DataStruct_.NativePlaceProvince:
                this.NativePlaceProvince = value == null ? null : value.ToString();
                return;
            case _DataStruct_.NativePlaceCity:
                this.NativePlaceCity = value == null ? null : value.ToString();
                return;
            case _DataStruct_.NativePlaceArea:
                this.NativePlaceArea = value == null ? null : value.ToString();
                return;
            case _DataStruct_.Profession:
                this.Profession = value == null ? null : value.ToString();
                return;
            case _DataStruct_.TimeVisitOfflineLast:
                this.TimeVisitOfflineLast = Convert.ToDateTime(value);
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
            case "uid":
                return this.UID;
            case "sitesid":
                return this.SiteSID;
            case "refereeuid":
                return this.RefereeUID;
            case "userid":
                return this.UserID;
            case "wx_appid":
                return this.wx_appid;
            case "wx_openid":
                return this.wx_openid;
            case "wx_unionid":
                return this.wx_unionid;
            case "wx_groupid":
                return this.wx_groupid;
            case "rolerid":
                return this.RoleRID;
            case "token":
                return this.Token;
            case "mpsessionkey":
                return this.MPSessionKey;
            case "mpqrcodeimgurl":
                return this.MPQRCodeImgUrl;
            case "userpassword":
                return this.UserPassword;
            case "mobilephone":
                return this.MobilePhone;
            case "mobilephonepure":
                return this.MobilePhonePure;
            case "mobilephonecountrycode":
                return this.MobilePhoneCountryCode;
            case "nickname":
                return this.nickName;
            case "gender":
                return this.gender;
            case "avatarurl":
                return this.avatarUrl;
            case "language":
                return this.language;
            case "city":
                return this.city;
            case "province":
                return this.province;
            case "country":
                return this.country;
            case "remark":
                return this.remark;
            case "sceneid":
                return this.SceneID;
            case "registerfrom":
                return this.RegisterFrom;
            case "registertime":
                return this.RegisterTime;
            case "subscribe":
                return this.subscribe;
            case "subscribe_time":
                return this.subscribe_time;
            case "username":
                return this.UserName;
            case "userface":
                return this.UserFace;
            case "age":
                return this.Age;
            case "birthday":
                return this.Birthday;
            case "houseinfo":
                return this.HouseInfo;
            case "email":
                return this.Email;
            case "hight":
                return this.Hight;
            case "weight":
                return this.Weight;
            case "qqid":
                return this.QQID;
            case "wxid":
                return this.WXID;
            case "userstate":
                return this.UserState;
            case "lastlogintime":
                return this.LastLoginTime;
            case "lastloginip":
                return this.LastLoginIP;
            case "recommendcode":
                return this.RecommendCode;
            case "recommenduseid":
                return this.RecommendUseID;
            case "bonuspoints":
                return this.BonusPoints;
            case "codeticket":
                return this.CodeTicket;
            case "messageprompttype":
                return this.MessagePromptType;
            case "messagepromptlasttime":
                return this.MessagePromptLastTime;
            case "customarea":
                return this.CustomArea;
            case "nativeplaceprovince":
                return this.NativePlaceProvince;
            case "nativeplacecity":
                return this.NativePlaceCity;
            case "nativeplacearea":
                return this.NativePlaceArea;
            case "profession":
                return this.Profession;
            case "timevisitofflinelast":
                return this.TimeVisitOfflineLast;
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
                case _DataStruct_.UID:
                    return this.UID;
                case _DataStruct_.SiteSID:
                    return this.SiteSID;
                case _DataStruct_.RefereeUID:
                    return this.RefereeUID;
                case _DataStruct_.UserID:
                    return this.UserID;
                case _DataStruct_.wx_appid:
                    return this.wx_appid;
                case _DataStruct_.wx_openid:
                    return this.wx_openid;
                case _DataStruct_.wx_unionid:
                    return this.wx_unionid;
                case _DataStruct_.wx_groupid:
                    return this.wx_groupid;
                case _DataStruct_.RoleRID:
                    return this.RoleRID;
                case _DataStruct_.Token:
                    return this.Token;
                case _DataStruct_.MPSessionKey:
                    return this.MPSessionKey;
                case _DataStruct_.MPQRCodeImgUrl:
                    return this.MPQRCodeImgUrl;
                case _DataStruct_.UserPassword:
                    return this.UserPassword;
                case _DataStruct_.MobilePhone:
                    return this.MobilePhone;
                case _DataStruct_.MobilePhonePure:
                    return this.MobilePhonePure;
                case _DataStruct_.MobilePhoneCountryCode:
                    return this.MobilePhoneCountryCode;
                case _DataStruct_.nickName:
                    return this.nickName;
                case _DataStruct_.gender:
                    return this.gender;
                case _DataStruct_.avatarUrl:
                    return this.avatarUrl;
                case _DataStruct_.language:
                    return this.language;
                case _DataStruct_.city:
                    return this.city;
                case _DataStruct_.province:
                    return this.province;
                case _DataStruct_.country:
                    return this.country;
                case _DataStruct_.remark:
                    return this.remark;
                case _DataStruct_.SceneID:
                    return this.SceneID;
                case _DataStruct_.RegisterFrom:
                    return this.RegisterFrom;
                case _DataStruct_.RegisterTime:
                    return this.RegisterTime;
                case _DataStruct_.subscribe:
                    return this.subscribe;
                case _DataStruct_.subscribe_time:
                    return this.subscribe_time;
                case _DataStruct_.UserName:
                    return this.UserName;
                case _DataStruct_.UserFace:
                    return this.UserFace;
                case _DataStruct_.Age:
                    return this.Age;
                case _DataStruct_.Birthday:
                    return this.Birthday;
                case _DataStruct_.HouseInfo:
                    return this.HouseInfo;
                case _DataStruct_.Email:
                    return this.Email;
                case _DataStruct_.Hight:
                    return this.Hight;
                case _DataStruct_.Weight:
                    return this.Weight;
                case _DataStruct_.QQID:
                    return this.QQID;
                case _DataStruct_.WXID:
                    return this.WXID;
                case _DataStruct_.UserState:
                    return this.UserState;
                case _DataStruct_.LastLoginTime:
                    return this.LastLoginTime;
                case _DataStruct_.LastLoginIP:
                    return this.LastLoginIP;
                case _DataStruct_.RecommendCode:
                    return this.RecommendCode;
                case _DataStruct_.RecommendUseID:
                    return this.RecommendUseID;
                case _DataStruct_.BonusPoints:
                    return this.BonusPoints;
                case _DataStruct_.CodeTicket:
                    return this.CodeTicket;
                case _DataStruct_.MessagePromptType:
                    return this.MessagePromptType;
                case _DataStruct_.MessagePromptLastTime:
                    return this.MessagePromptLastTime;
                case _DataStruct_.CustomArea:
                    return this.CustomArea;
                case _DataStruct_.NativePlaceProvince:
                    return this.NativePlaceProvince;
                case _DataStruct_.NativePlaceCity:
                    return this.NativePlaceCity;
                case _DataStruct_.NativePlaceArea:
                    return this.NativePlaceArea;
                case _DataStruct_.Profession:
                    return this.Profession;
                case _DataStruct_.TimeVisitOfflineLast:
                    return this.TimeVisitOfflineLast;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(UserData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as UserData;
            if(sourceEntity == null)
                return;
            this._uID = sourceEntity._uID;
            this._siteSID = sourceEntity._siteSID;
            this._refereeUID = sourceEntity._refereeUID;
            this._userID = sourceEntity._userID;
            this._wx_appid = sourceEntity._wx_appid;
            this._wx_openid = sourceEntity._wx_openid;
            this._wx_unionid = sourceEntity._wx_unionid;
            this._wx_groupid = sourceEntity._wx_groupid;
            this._roleRID = sourceEntity._roleRID;
            this._token = sourceEntity._token;
            this._mPSessionKey = sourceEntity._mPSessionKey;
            this._mPQRCodeImgUrl = sourceEntity._mPQRCodeImgUrl;
            this._userPassword = sourceEntity._userPassword;
            this._mobilePhone = sourceEntity._mobilePhone;
            this._mobilePhonePure = sourceEntity._mobilePhonePure;
            this._mobilePhoneCountryCode = sourceEntity._mobilePhoneCountryCode;
            this._nickName = sourceEntity._nickName;
            this._gender = sourceEntity._gender;
            this._avatarUrl = sourceEntity._avatarUrl;
            this._language = sourceEntity._language;
            this._city = sourceEntity._city;
            this._province = sourceEntity._province;
            this._country = sourceEntity._country;
            this._remark = sourceEntity._remark;
            this._sceneID = sourceEntity._sceneID;
            this._registerFrom = sourceEntity._registerFrom;
            this._registerTime = sourceEntity._registerTime;
            this._subscribe = sourceEntity._subscribe;
            this._subscribe_time = sourceEntity._subscribe_time;
            this._userName = sourceEntity._userName;
            this._userFace = sourceEntity._userFace;
            this._age = sourceEntity._age;
            this._birthday = sourceEntity._birthday;
            this._houseInfo = sourceEntity._houseInfo;
            this._email = sourceEntity._email;
            this._hight = sourceEntity._hight;
            this._weight = sourceEntity._weight;
            this._qQID = sourceEntity._qQID;
            this._wXID = sourceEntity._wXID;
            this._userState = sourceEntity._userState;
            this._lastLoginTime = sourceEntity._lastLoginTime;
            this._lastLoginIP = sourceEntity._lastLoginIP;
            this._recommendCode = sourceEntity._recommendCode;
            this._recommendUseID = sourceEntity._recommendUseID;
            this._bonusPoints = sourceEntity._bonusPoints;
            this._codeTicket = sourceEntity._codeTicket;
            this._messagePromptType = sourceEntity._messagePromptType;
            this._messagePromptLastTime = sourceEntity._messagePromptLastTime;
            this._customArea = sourceEntity._customArea;
            this._nativePlaceProvince = sourceEntity._nativePlaceProvince;
            this._nativePlaceCity = sourceEntity._nativePlaceCity;
            this._nativePlaceArea = sourceEntity._nativePlaceArea;
            this._profession = sourceEntity._profession;
            this._timeVisitOfflineLast = sourceEntity._timeVisitOfflineLast;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(UserData source)
        {
                this.UID = source.UID;
                this.SiteSID = source.SiteSID;
                this.RefereeUID = source.RefereeUID;
                this.UserID = source.UserID;
                this.wx_appid = source.wx_appid;
                this.wx_openid = source.wx_openid;
                this.wx_unionid = source.wx_unionid;
                this.wx_groupid = source.wx_groupid;
                this.RoleRID = source.RoleRID;
                this.Token = source.Token;
                this.MPSessionKey = source.MPSessionKey;
                this.MPQRCodeImgUrl = source.MPQRCodeImgUrl;
                this.UserPassword = source.UserPassword;
                this.MobilePhone = source.MobilePhone;
                this.MobilePhonePure = source.MobilePhonePure;
                this.MobilePhoneCountryCode = source.MobilePhoneCountryCode;
                this.nickName = source.nickName;
                this.gender = source.gender;
                this.avatarUrl = source.avatarUrl;
                this.language = source.language;
                this.city = source.city;
                this.province = source.province;
                this.country = source.country;
                this.remark = source.remark;
                this.SceneID = source.SceneID;
                this.RegisterFrom = source.RegisterFrom;
                this.RegisterTime = source.RegisterTime;
                this.subscribe = source.subscribe;
                this.subscribe_time = source.subscribe_time;
                this.UserName = source.UserName;
                this.UserFace = source.UserFace;
                this.Age = source.Age;
                this.Birthday = source.Birthday;
                this.HouseInfo = source.HouseInfo;
                this.Email = source.Email;
                this.Hight = source.Hight;
                this.Weight = source.Weight;
                this.QQID = source.QQID;
                this.WXID = source.WXID;
                this.UserState = source.UserState;
                this.LastLoginTime = source.LastLoginTime;
                this.LastLoginIP = source.LastLoginIP;
                this.RecommendCode = source.RecommendCode;
                this.RecommendUseID = source.RecommendUseID;
                this.BonusPoints = source.BonusPoints;
                this.CodeTicket = source.CodeTicket;
                this.MessagePromptType = source.MessagePromptType;
                this.MessagePromptLastTime = source.MessagePromptLastTime;
                this.CustomArea = source.CustomArea;
                this.NativePlaceProvince = source.NativePlaceProvince;
                this.NativePlaceCity = source.NativePlaceCity;
                this.NativePlaceArea = source.NativePlaceArea;
                this.Profession = source.Profession;
                this.TimeVisitOfflineLast = source.TimeVisitOfflineLast;
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
                OnUIDModified(subsist,false);
                OnSiteSIDModified(subsist,false);
                OnRefereeUIDModified(subsist,false);
                OnUserIDModified(subsist,false);
                Onwx_appidModified(subsist,false);
                Onwx_openidModified(subsist,false);
                Onwx_unionidModified(subsist,false);
                Onwx_groupidModified(subsist,false);
                OnRoleRIDModified(subsist,false);
                OnTokenModified(subsist,false);
                OnMPSessionKeyModified(subsist,false);
                OnMPQRCodeImgUrlModified(subsist,false);
                OnUserPasswordModified(subsist,false);
                OnMobilePhoneModified(subsist,false);
                OnMobilePhonePureModified(subsist,false);
                OnMobilePhoneCountryCodeModified(subsist,false);
                OnnickNameModified(subsist,false);
                OngenderModified(subsist,false);
                OnavatarUrlModified(subsist,false);
                OnlanguageModified(subsist,false);
                OncityModified(subsist,false);
                OnprovinceModified(subsist,false);
                OncountryModified(subsist,false);
                OnremarkModified(subsist,false);
                OnSceneIDModified(subsist,false);
                OnRegisterFromModified(subsist,false);
                OnRegisterTimeModified(subsist,false);
                OnsubscribeModified(subsist,false);
                Onsubscribe_timeModified(subsist,false);
                OnUserNameModified(subsist,false);
                OnUserFaceModified(subsist,false);
                OnAgeModified(subsist,false);
                OnBirthdayModified(subsist,false);
                OnHouseInfoModified(subsist,false);
                OnEmailModified(subsist,false);
                OnHightModified(subsist,false);
                OnWeightModified(subsist,false);
                OnQQIDModified(subsist,false);
                OnWXIDModified(subsist,false);
                OnUserStateModified(subsist,false);
                OnLastLoginTimeModified(subsist,false);
                OnLastLoginIPModified(subsist,false);
                OnRecommendCodeModified(subsist,false);
                OnRecommendUseIDModified(subsist,false);
                OnBonusPointsModified(subsist,false);
                OnCodeTicketModified(subsist,false);
                OnMessagePromptTypeModified(subsist,false);
                OnMessagePromptLastTimeModified(subsist,false);
                OnCustomAreaModified(subsist,false);
                OnNativePlaceProvinceModified(subsist,false);
                OnNativePlaceCityModified(subsist,false);
                OnNativePlaceAreaModified(subsist,false);
                OnProfessionModified(subsist,false);
                OnTimeVisitOfflineLastModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnUIDModified(subsist,true);
                OnSiteSIDModified(subsist,true);
                OnRefereeUIDModified(subsist,true);
                OnUserIDModified(subsist,true);
                Onwx_appidModified(subsist,true);
                Onwx_openidModified(subsist,true);
                Onwx_unionidModified(subsist,true);
                Onwx_groupidModified(subsist,true);
                OnRoleRIDModified(subsist,true);
                OnTokenModified(subsist,true);
                OnMPSessionKeyModified(subsist,true);
                OnMPQRCodeImgUrlModified(subsist,true);
                OnUserPasswordModified(subsist,true);
                OnMobilePhoneModified(subsist,true);
                OnMobilePhonePureModified(subsist,true);
                OnMobilePhoneCountryCodeModified(subsist,true);
                OnnickNameModified(subsist,true);
                OngenderModified(subsist,true);
                OnavatarUrlModified(subsist,true);
                OnlanguageModified(subsist,true);
                OncityModified(subsist,true);
                OnprovinceModified(subsist,true);
                OncountryModified(subsist,true);
                OnremarkModified(subsist,true);
                OnSceneIDModified(subsist,true);
                OnRegisterFromModified(subsist,true);
                OnRegisterTimeModified(subsist,true);
                OnsubscribeModified(subsist,true);
                Onsubscribe_timeModified(subsist,true);
                OnUserNameModified(subsist,true);
                OnUserFaceModified(subsist,true);
                OnAgeModified(subsist,true);
                OnBirthdayModified(subsist,true);
                OnHouseInfoModified(subsist,true);
                OnEmailModified(subsist,true);
                OnHightModified(subsist,true);
                OnWeightModified(subsist,true);
                OnQQIDModified(subsist,true);
                OnWXIDModified(subsist,true);
                OnUserStateModified(subsist,true);
                OnLastLoginTimeModified(subsist,true);
                OnLastLoginIPModified(subsist,true);
                OnRecommendCodeModified(subsist,true);
                OnRecommendUseIDModified(subsist,true);
                OnBonusPointsModified(subsist,true);
                OnCodeTicketModified(subsist,true);
                OnMessagePromptTypeModified(subsist,true);
                OnMessagePromptLastTimeModified(subsist,true);
                OnCustomAreaModified(subsist,true);
                OnNativePlaceProvinceModified(subsist,true);
                OnNativePlaceCityModified(subsist,true);
                OnNativePlaceAreaModified(subsist,true);
                OnProfessionModified(subsist,true);
                OnTimeVisitOfflineLastModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[54] > 0)
            {
                OnUIDModified(subsist,modifieds[_DataStruct_.Real_UID] == 1);
                OnSiteSIDModified(subsist,modifieds[_DataStruct_.Real_SiteSID] == 1);
                OnRefereeUIDModified(subsist,modifieds[_DataStruct_.Real_RefereeUID] == 1);
                OnUserIDModified(subsist,modifieds[_DataStruct_.Real_UserID] == 1);
                Onwx_appidModified(subsist,modifieds[_DataStruct_.Real_wx_appid] == 1);
                Onwx_openidModified(subsist,modifieds[_DataStruct_.Real_wx_openid] == 1);
                Onwx_unionidModified(subsist,modifieds[_DataStruct_.Real_wx_unionid] == 1);
                Onwx_groupidModified(subsist,modifieds[_DataStruct_.Real_wx_groupid] == 1);
                OnRoleRIDModified(subsist,modifieds[_DataStruct_.Real_RoleRID] == 1);
                OnTokenModified(subsist,modifieds[_DataStruct_.Real_Token] == 1);
                OnMPSessionKeyModified(subsist,modifieds[_DataStruct_.Real_MPSessionKey] == 1);
                OnMPQRCodeImgUrlModified(subsist,modifieds[_DataStruct_.Real_MPQRCodeImgUrl] == 1);
                OnUserPasswordModified(subsist,modifieds[_DataStruct_.Real_UserPassword] == 1);
                OnMobilePhoneModified(subsist,modifieds[_DataStruct_.Real_MobilePhone] == 1);
                OnMobilePhonePureModified(subsist,modifieds[_DataStruct_.Real_MobilePhonePure] == 1);
                OnMobilePhoneCountryCodeModified(subsist,modifieds[_DataStruct_.Real_MobilePhoneCountryCode] == 1);
                OnnickNameModified(subsist,modifieds[_DataStruct_.Real_nickName] == 1);
                OngenderModified(subsist,modifieds[_DataStruct_.Real_gender] == 1);
                OnavatarUrlModified(subsist,modifieds[_DataStruct_.Real_avatarUrl] == 1);
                OnlanguageModified(subsist,modifieds[_DataStruct_.Real_language] == 1);
                OncityModified(subsist,modifieds[_DataStruct_.Real_city] == 1);
                OnprovinceModified(subsist,modifieds[_DataStruct_.Real_province] == 1);
                OncountryModified(subsist,modifieds[_DataStruct_.Real_country] == 1);
                OnremarkModified(subsist,modifieds[_DataStruct_.Real_remark] == 1);
                OnSceneIDModified(subsist,modifieds[_DataStruct_.Real_SceneID] == 1);
                OnRegisterFromModified(subsist,modifieds[_DataStruct_.Real_RegisterFrom] == 1);
                OnRegisterTimeModified(subsist,modifieds[_DataStruct_.Real_RegisterTime] == 1);
                OnsubscribeModified(subsist,modifieds[_DataStruct_.Real_subscribe] == 1);
                Onsubscribe_timeModified(subsist,modifieds[_DataStruct_.Real_subscribe_time] == 1);
                OnUserNameModified(subsist,modifieds[_DataStruct_.Real_UserName] == 1);
                OnUserFaceModified(subsist,modifieds[_DataStruct_.Real_UserFace] == 1);
                OnAgeModified(subsist,modifieds[_DataStruct_.Real_Age] == 1);
                OnBirthdayModified(subsist,modifieds[_DataStruct_.Real_Birthday] == 1);
                OnHouseInfoModified(subsist,modifieds[_DataStruct_.Real_HouseInfo] == 1);
                OnEmailModified(subsist,modifieds[_DataStruct_.Real_Email] == 1);
                OnHightModified(subsist,modifieds[_DataStruct_.Real_Hight] == 1);
                OnWeightModified(subsist,modifieds[_DataStruct_.Real_Weight] == 1);
                OnQQIDModified(subsist,modifieds[_DataStruct_.Real_QQID] == 1);
                OnWXIDModified(subsist,modifieds[_DataStruct_.Real_WXID] == 1);
                OnUserStateModified(subsist,modifieds[_DataStruct_.Real_UserState] == 1);
                OnLastLoginTimeModified(subsist,modifieds[_DataStruct_.Real_LastLoginTime] == 1);
                OnLastLoginIPModified(subsist,modifieds[_DataStruct_.Real_LastLoginIP] == 1);
                OnRecommendCodeModified(subsist,modifieds[_DataStruct_.Real_RecommendCode] == 1);
                OnRecommendUseIDModified(subsist,modifieds[_DataStruct_.Real_RecommendUseID] == 1);
                OnBonusPointsModified(subsist,modifieds[_DataStruct_.Real_BonusPoints] == 1);
                OnCodeTicketModified(subsist,modifieds[_DataStruct_.Real_CodeTicket] == 1);
                OnMessagePromptTypeModified(subsist,modifieds[_DataStruct_.Real_MessagePromptType] == 1);
                OnMessagePromptLastTimeModified(subsist,modifieds[_DataStruct_.Real_MessagePromptLastTime] == 1);
                OnCustomAreaModified(subsist,modifieds[_DataStruct_.Real_CustomArea] == 1);
                OnNativePlaceProvinceModified(subsist,modifieds[_DataStruct_.Real_NativePlaceProvince] == 1);
                OnNativePlaceCityModified(subsist,modifieds[_DataStruct_.Real_NativePlaceCity] == 1);
                OnNativePlaceAreaModified(subsist,modifieds[_DataStruct_.Real_NativePlaceArea] == 1);
                OnProfessionModified(subsist,modifieds[_DataStruct_.Real_Profession] == 1);
                OnTimeVisitOfflineLastModified(subsist,modifieds[_DataStruct_.Real_TimeVisitOfflineLast] == 1);
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
        partial void OnUIDModified(EntitySubsist subsist,bool isModified);

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
        /// referee用户标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnRefereeUIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 用户端修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnUserIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// wx应用标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void Onwx_appidModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// WXOpenID修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void Onwx_openidModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// WX工会会员修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void Onwx_unionidModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// WX群修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void Onwx_groupidModified(EntitySubsist subsist,bool isModified);

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
        /// 令牌修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnTokenModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 会话全局标识MP修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnMPSessionKeyModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 标签：法典修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnMPQRCodeImgUrlModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 用户口令修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnUserPasswordModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 移动电话修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnMobilePhoneModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 纯手机修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnMobilePhonePureModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 手机国家代码修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnMobilePhoneCountryCodeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 昵称修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnnickNameModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 性别修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OngenderModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 2.Avatar第38142号；修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnavatarUrlModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 语言修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnlanguageModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 城市修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OncityModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 省份修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnprovinceModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 国家修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OncountryModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 备注修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnremarkModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 场景叠加修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnSceneIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 从注册修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnRegisterFromModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 注册时间修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnRegisterTimeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 订阅修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnsubscribeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 订阅时间修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void Onsubscribe_timeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 用户名修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnUserNameModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 用户面修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnUserFaceModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 年龄修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAgeModified(EntitySubsist subsist,bool isModified);

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
        /// 住宅信息修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnHouseInfoModified(EntitySubsist subsist,bool isModified);

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
        /// 海特修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnHightModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 重量修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnWeightModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// QQID修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnQQIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// WXID修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnWXIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 用户状态修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnUserStateModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 上次登录时间修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnLastLoginTimeModified(EntitySubsist subsist,bool isModified);

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
        /// 推荐代码修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnRecommendCodeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 推荐使用标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnRecommendUseIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 加分修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnBonusPointsModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 代码票修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnCodeTicketModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 快速语音信号修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnMessagePromptTypeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 上次消息提示修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnMessagePromptLastTimeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 定制区修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnCustomAreaModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 本地省修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnNativePlaceProvinceModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 本地城市修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnNativePlaceCityModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 本地区修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnNativePlaceAreaModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 职业修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnProfessionModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 上次离线访问时间修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnTimeVisitOfflineLastModified(EntitySubsist subsist,bool isModified);
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
            public const string EntityName = @"User";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"用户";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"用户";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0x0;
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityPrimaryKey = "UID";
            
            
            /// <summary>
            /// 主键的数字标识
            /// </summary>
            public const byte UID = 1;
            
            /// <summary>
            /// 主键的实时记录顺序
            /// </summary>
            public const int Real_UID = 0;

            /// <summary>
            /// 站点标识的数字标识
            /// </summary>
            public const byte SiteSID = 2;
            
            /// <summary>
            /// 站点标识的实时记录顺序
            /// </summary>
            public const int Real_SiteSID = 1;

            /// <summary>
            /// referee用户标识的数字标识
            /// </summary>
            public const byte RefereeUID = 3;
            
            /// <summary>
            /// referee用户标识的实时记录顺序
            /// </summary>
            public const int Real_RefereeUID = 2;

            /// <summary>
            /// 用户端的数字标识
            /// </summary>
            public const byte UserID = 4;
            
            /// <summary>
            /// 用户端的实时记录顺序
            /// </summary>
            public const int Real_UserID = 3;

            /// <summary>
            /// wx应用标识的数字标识
            /// </summary>
            public const byte wx_appid = 5;
            
            /// <summary>
            /// wx应用标识的实时记录顺序
            /// </summary>
            public const int Real_wx_appid = 4;

            /// <summary>
            /// WXOpenID的数字标识
            /// </summary>
            public const byte wx_openid = 6;
            
            /// <summary>
            /// WXOpenID的实时记录顺序
            /// </summary>
            public const int Real_wx_openid = 5;

            /// <summary>
            /// WX工会会员的数字标识
            /// </summary>
            public const byte wx_unionid = 7;
            
            /// <summary>
            /// WX工会会员的实时记录顺序
            /// </summary>
            public const int Real_wx_unionid = 6;

            /// <summary>
            /// WX群的数字标识
            /// </summary>
            public const byte wx_groupid = 8;
            
            /// <summary>
            /// WX群的实时记录顺序
            /// </summary>
            public const int Real_wx_groupid = 7;

            /// <summary>
            /// 角色扮演的数字标识
            /// </summary>
            public const byte RoleRID = 9;
            
            /// <summary>
            /// 角色扮演的实时记录顺序
            /// </summary>
            public const int Real_RoleRID = 8;

            /// <summary>
            /// 令牌的数字标识
            /// </summary>
            public const byte Token = 10;
            
            /// <summary>
            /// 令牌的实时记录顺序
            /// </summary>
            public const int Real_Token = 9;

            /// <summary>
            /// 会话全局标识MP的数字标识
            /// </summary>
            public const byte MPSessionKey = 11;
            
            /// <summary>
            /// 会话全局标识MP的实时记录顺序
            /// </summary>
            public const int Real_MPSessionKey = 10;

            /// <summary>
            /// 标签：法典的数字标识
            /// </summary>
            public const byte MPQRCodeImgUrl = 12;
            
            /// <summary>
            /// 标签：法典的实时记录顺序
            /// </summary>
            public const int Real_MPQRCodeImgUrl = 11;

            /// <summary>
            /// 用户口令的数字标识
            /// </summary>
            public const byte UserPassword = 13;
            
            /// <summary>
            /// 用户口令的实时记录顺序
            /// </summary>
            public const int Real_UserPassword = 12;

            /// <summary>
            /// 移动电话的数字标识
            /// </summary>
            public const byte MobilePhone = 14;
            
            /// <summary>
            /// 移动电话的实时记录顺序
            /// </summary>
            public const int Real_MobilePhone = 13;

            /// <summary>
            /// 纯手机的数字标识
            /// </summary>
            public const byte MobilePhonePure = 15;
            
            /// <summary>
            /// 纯手机的实时记录顺序
            /// </summary>
            public const int Real_MobilePhonePure = 14;

            /// <summary>
            /// 手机国家代码的数字标识
            /// </summary>
            public const byte MobilePhoneCountryCode = 16;
            
            /// <summary>
            /// 手机国家代码的实时记录顺序
            /// </summary>
            public const int Real_MobilePhoneCountryCode = 15;

            /// <summary>
            /// 昵称的数字标识
            /// </summary>
            public const byte nickName = 17;
            
            /// <summary>
            /// 昵称的实时记录顺序
            /// </summary>
            public const int Real_nickName = 16;

            /// <summary>
            /// 性别的数字标识
            /// </summary>
            public const byte gender = 18;
            
            /// <summary>
            /// 性别的实时记录顺序
            /// </summary>
            public const int Real_gender = 17;

            /// <summary>
            /// 2.Avatar第38142号；的数字标识
            /// </summary>
            public const byte avatarUrl = 19;
            
            /// <summary>
            /// 2.Avatar第38142号；的实时记录顺序
            /// </summary>
            public const int Real_avatarUrl = 18;

            /// <summary>
            /// 语言的数字标识
            /// </summary>
            public const byte language = 20;
            
            /// <summary>
            /// 语言的实时记录顺序
            /// </summary>
            public const int Real_language = 19;

            /// <summary>
            /// 城市的数字标识
            /// </summary>
            public const byte city = 21;
            
            /// <summary>
            /// 城市的实时记录顺序
            /// </summary>
            public const int Real_city = 20;

            /// <summary>
            /// 省份的数字标识
            /// </summary>
            public const byte province = 22;
            
            /// <summary>
            /// 省份的实时记录顺序
            /// </summary>
            public const int Real_province = 21;

            /// <summary>
            /// 国家的数字标识
            /// </summary>
            public const byte country = 23;
            
            /// <summary>
            /// 国家的实时记录顺序
            /// </summary>
            public const int Real_country = 22;

            /// <summary>
            /// 备注的数字标识
            /// </summary>
            public const byte remark = 24;
            
            /// <summary>
            /// 备注的实时记录顺序
            /// </summary>
            public const int Real_remark = 23;

            /// <summary>
            /// 场景叠加的数字标识
            /// </summary>
            public const byte SceneID = 25;
            
            /// <summary>
            /// 场景叠加的实时记录顺序
            /// </summary>
            public const int Real_SceneID = 24;

            /// <summary>
            /// 从注册的数字标识
            /// </summary>
            public const byte RegisterFrom = 26;
            
            /// <summary>
            /// 从注册的实时记录顺序
            /// </summary>
            public const int Real_RegisterFrom = 25;

            /// <summary>
            /// 注册时间的数字标识
            /// </summary>
            public const byte RegisterTime = 27;
            
            /// <summary>
            /// 注册时间的实时记录顺序
            /// </summary>
            public const int Real_RegisterTime = 26;

            /// <summary>
            /// 订阅的数字标识
            /// </summary>
            public const byte subscribe = 28;
            
            /// <summary>
            /// 订阅的实时记录顺序
            /// </summary>
            public const int Real_subscribe = 27;

            /// <summary>
            /// 订阅时间的数字标识
            /// </summary>
            public const byte subscribe_time = 29;
            
            /// <summary>
            /// 订阅时间的实时记录顺序
            /// </summary>
            public const int Real_subscribe_time = 28;

            /// <summary>
            /// 用户名的数字标识
            /// </summary>
            public const byte UserName = 30;
            
            /// <summary>
            /// 用户名的实时记录顺序
            /// </summary>
            public const int Real_UserName = 29;

            /// <summary>
            /// 用户面的数字标识
            /// </summary>
            public const byte UserFace = 31;
            
            /// <summary>
            /// 用户面的实时记录顺序
            /// </summary>
            public const int Real_UserFace = 30;

            /// <summary>
            /// 年龄的数字标识
            /// </summary>
            public const byte Age = 32;
            
            /// <summary>
            /// 年龄的实时记录顺序
            /// </summary>
            public const int Real_Age = 31;

            /// <summary>
            /// 生日的数字标识
            /// </summary>
            public const byte Birthday = 33;
            
            /// <summary>
            /// 生日的实时记录顺序
            /// </summary>
            public const int Real_Birthday = 32;

            /// <summary>
            /// 住宅信息的数字标识
            /// </summary>
            public const byte HouseInfo = 34;
            
            /// <summary>
            /// 住宅信息的实时记录顺序
            /// </summary>
            public const int Real_HouseInfo = 33;

            /// <summary>
            /// 电子邮件的数字标识
            /// </summary>
            public const byte Email = 35;
            
            /// <summary>
            /// 电子邮件的实时记录顺序
            /// </summary>
            public const int Real_Email = 34;

            /// <summary>
            /// 海特的数字标识
            /// </summary>
            public const byte Hight = 36;
            
            /// <summary>
            /// 海特的实时记录顺序
            /// </summary>
            public const int Real_Hight = 35;

            /// <summary>
            /// 重量的数字标识
            /// </summary>
            public const byte Weight = 37;
            
            /// <summary>
            /// 重量的实时记录顺序
            /// </summary>
            public const int Real_Weight = 36;

            /// <summary>
            /// QQID的数字标识
            /// </summary>
            public const byte QQID = 38;
            
            /// <summary>
            /// QQID的实时记录顺序
            /// </summary>
            public const int Real_QQID = 37;

            /// <summary>
            /// WXID的数字标识
            /// </summary>
            public const byte WXID = 39;
            
            /// <summary>
            /// WXID的实时记录顺序
            /// </summary>
            public const int Real_WXID = 38;

            /// <summary>
            /// 用户状态的数字标识
            /// </summary>
            public const byte UserState = 40;
            
            /// <summary>
            /// 用户状态的实时记录顺序
            /// </summary>
            public const int Real_UserState = 39;

            /// <summary>
            /// 上次登录时间的数字标识
            /// </summary>
            public const byte LastLoginTime = 41;
            
            /// <summary>
            /// 上次登录时间的实时记录顺序
            /// </summary>
            public const int Real_LastLoginTime = 40;

            /// <summary>
            /// 最后登录IP的数字标识
            /// </summary>
            public const byte LastLoginIP = 42;
            
            /// <summary>
            /// 最后登录IP的实时记录顺序
            /// </summary>
            public const int Real_LastLoginIP = 41;

            /// <summary>
            /// 推荐代码的数字标识
            /// </summary>
            public const byte RecommendCode = 43;
            
            /// <summary>
            /// 推荐代码的实时记录顺序
            /// </summary>
            public const int Real_RecommendCode = 42;

            /// <summary>
            /// 推荐使用标识的数字标识
            /// </summary>
            public const byte RecommendUseID = 44;
            
            /// <summary>
            /// 推荐使用标识的实时记录顺序
            /// </summary>
            public const int Real_RecommendUseID = 43;

            /// <summary>
            /// 加分的数字标识
            /// </summary>
            public const byte BonusPoints = 45;
            
            /// <summary>
            /// 加分的实时记录顺序
            /// </summary>
            public const int Real_BonusPoints = 44;

            /// <summary>
            /// 代码票的数字标识
            /// </summary>
            public const byte CodeTicket = 46;
            
            /// <summary>
            /// 代码票的实时记录顺序
            /// </summary>
            public const int Real_CodeTicket = 45;

            /// <summary>
            /// 快速语音信号的数字标识
            /// </summary>
            public const byte MessagePromptType = 47;
            
            /// <summary>
            /// 快速语音信号的实时记录顺序
            /// </summary>
            public const int Real_MessagePromptType = 46;

            /// <summary>
            /// 上次消息提示的数字标识
            /// </summary>
            public const byte MessagePromptLastTime = 48;
            
            /// <summary>
            /// 上次消息提示的实时记录顺序
            /// </summary>
            public const int Real_MessagePromptLastTime = 47;

            /// <summary>
            /// 定制区的数字标识
            /// </summary>
            public const byte CustomArea = 49;
            
            /// <summary>
            /// 定制区的实时记录顺序
            /// </summary>
            public const int Real_CustomArea = 48;

            /// <summary>
            /// 本地省的数字标识
            /// </summary>
            public const byte NativePlaceProvince = 50;
            
            /// <summary>
            /// 本地省的实时记录顺序
            /// </summary>
            public const int Real_NativePlaceProvince = 49;

            /// <summary>
            /// 本地城市的数字标识
            /// </summary>
            public const byte NativePlaceCity = 51;
            
            /// <summary>
            /// 本地城市的实时记录顺序
            /// </summary>
            public const int Real_NativePlaceCity = 50;

            /// <summary>
            /// 本地区的数字标识
            /// </summary>
            public const byte NativePlaceArea = 52;
            
            /// <summary>
            /// 本地区的实时记录顺序
            /// </summary>
            public const int Real_NativePlaceArea = 51;

            /// <summary>
            /// 职业的数字标识
            /// </summary>
            public const byte Profession = 53;
            
            /// <summary>
            /// 职业的实时记录顺序
            /// </summary>
            public const int Real_Profession = 52;

            /// <summary>
            /// 上次离线访问时间的数字标识
            /// </summary>
            public const byte TimeVisitOfflineLast = 54;
            
            /// <summary>
            /// 上次离线访问时间的实时记录顺序
            /// </summary>
            public const int Real_TimeVisitOfflineLast = 53;

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
                        Real_UID,
                        new PropertySturct
                        {
                            Index        = UID,
                            Name         = "UID",
                            Title        = "主键",
                            Caption      = @"主键",
                            Description  = @"逻辑ID",
                            ColumnName   = "UID",
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
                        Real_RefereeUID,
                        new PropertySturct
                        {
                            Index        = RefereeUID,
                            Name         = "RefereeUID",
                            Title        = "referee用户标识",
                            Caption      = @"referee用户标识",
                            Description  = @"referee用户标识",
                            ColumnName   = "RefereeUID",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_UserID,
                        new PropertySturct
                        {
                            Index        = UserID,
                            Name         = "UserID",
                            Title        = "用户端",
                            Caption      = @"用户端",
                            Description  = @"用户端",
                            ColumnName   = "UserID",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_wx_appid,
                        new PropertySturct
                        {
                            Index        = wx_appid,
                            Name         = "wx_appid",
                            Title        = "wx应用标识",
                            Caption      = @"wx应用标识",
                            Description  = @"wx应用标识",
                            ColumnName   = "wx_appid",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_wx_openid,
                        new PropertySturct
                        {
                            Index        = wx_openid,
                            Name         = "wx_openid",
                            Title        = "WXOpenID",
                            Caption      = @"WXOpenID",
                            Description  = @"WXOpenID",
                            ColumnName   = "wx_openid",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_wx_unionid,
                        new PropertySturct
                        {
                            Index        = wx_unionid,
                            Name         = "wx_unionid",
                            Title        = "WX工会会员",
                            Caption      = @"WX工会会员",
                            Description  = @"WX工会会员",
                            ColumnName   = "wx_unionid",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_wx_groupid,
                        new PropertySturct
                        {
                            Index        = wx_groupid,
                            Name         = "wx_groupid",
                            Title        = "WX群",
                            Caption      = @"WX群",
                            Description  = @"WX群",
                            ColumnName   = "wx_groupid",
                            PropertyType = typeof(float),
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
                        Real_MPSessionKey,
                        new PropertySturct
                        {
                            Index        = MPSessionKey,
                            Name         = "MPSessionKey",
                            Title        = "会话全局标识MP",
                            Caption      = @"会话全局标识MP",
                            Description  = @"会话全局标识MP",
                            ColumnName   = "MPSessionKey",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_MPQRCodeImgUrl,
                        new PropertySturct
                        {
                            Index        = MPQRCodeImgUrl,
                            Name         = "MPQRCodeImgUrl",
                            Title        = "标签：法典",
                            Caption      = @"标签：法典",
                            Description  = @"标签：法典",
                            ColumnName   = "MPQRCodeImgUrl",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_UserPassword,
                        new PropertySturct
                        {
                            Index        = UserPassword,
                            Name         = "UserPassword",
                            Title        = "用户口令",
                            Caption      = @"用户口令",
                            Description  = @"用户口令",
                            ColumnName   = "UserPassword",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_MobilePhone,
                        new PropertySturct
                        {
                            Index        = MobilePhone,
                            Name         = "MobilePhone",
                            Title        = "移动电话",
                            Caption      = @"移动电话",
                            Description  = @"移动电话",
                            ColumnName   = "MobilePhone",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_MobilePhonePure,
                        new PropertySturct
                        {
                            Index        = MobilePhonePure,
                            Name         = "MobilePhonePure",
                            Title        = "纯手机",
                            Caption      = @"纯手机",
                            Description  = @"纯手机",
                            ColumnName   = "MobilePhonePure",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_MobilePhoneCountryCode,
                        new PropertySturct
                        {
                            Index        = MobilePhoneCountryCode,
                            Name         = "MobilePhoneCountryCode",
                            Title        = "手机国家代码",
                            Caption      = @"手机国家代码",
                            Description  = @"手机国家代码",
                            ColumnName   = "MobilePhoneCountryCode",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_nickName,
                        new PropertySturct
                        {
                            Index        = nickName,
                            Name         = "nickName",
                            Title        = "昵称",
                            Caption      = @"昵称",
                            Description  = @"昵称",
                            ColumnName   = "nickName",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_gender,
                        new PropertySturct
                        {
                            Index        = gender,
                            Name         = "gender",
                            Title        = "性别",
                            Caption      = @"性别",
                            Description  = @"性别",
                            ColumnName   = "gender",
                            PropertyType = typeof(bool),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_avatarUrl,
                        new PropertySturct
                        {
                            Index        = avatarUrl,
                            Name         = "avatarUrl",
                            Title        = "2.Avatar第38142号；",
                            Caption      = @"2.Avatar第38142号；",
                            Description  = @"2.Avatar第38142号；",
                            ColumnName   = "avatarUrl",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_language,
                        new PropertySturct
                        {
                            Index        = language,
                            Name         = "language",
                            Title        = "语言",
                            Caption      = @"语言",
                            Description  = @"语言",
                            ColumnName   = "language",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_city,
                        new PropertySturct
                        {
                            Index        = city,
                            Name         = "city",
                            Title        = "城市",
                            Caption      = @"城市",
                            Description  = @"城市",
                            ColumnName   = "city",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_province,
                        new PropertySturct
                        {
                            Index        = province,
                            Name         = "province",
                            Title        = "省份",
                            Caption      = @"省份",
                            Description  = @"省份",
                            ColumnName   = "province",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_country,
                        new PropertySturct
                        {
                            Index        = country,
                            Name         = "country",
                            Title        = "国家",
                            Caption      = @"国家",
                            Description  = @"国家",
                            ColumnName   = "country",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_remark,
                        new PropertySturct
                        {
                            Index        = remark,
                            Name         = "remark",
                            Title        = "备注",
                            Caption      = @"备注",
                            Description  = @"备注",
                            ColumnName   = "remark",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_SceneID,
                        new PropertySturct
                        {
                            Index        = SceneID,
                            Name         = "SceneID",
                            Title        = "场景叠加",
                            Caption      = @"场景叠加",
                            Description  = @"场景叠加",
                            ColumnName   = "SceneID",
                            PropertyType = typeof(int),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_RegisterFrom,
                        new PropertySturct
                        {
                            Index        = RegisterFrom,
                            Name         = "RegisterFrom",
                            Title        = "从注册",
                            Caption      = @"从注册",
                            Description  = @"从注册",
                            ColumnName   = "RegisterFrom",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_RegisterTime,
                        new PropertySturct
                        {
                            Index        = RegisterTime,
                            Name         = "RegisterTime",
                            Title        = "注册时间",
                            Caption      = @"注册时间",
                            Description  = @"注册时间",
                            ColumnName   = "RegisterTime",
                            PropertyType = typeof(DateTime),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_subscribe,
                        new PropertySturct
                        {
                            Index        = subscribe,
                            Name         = "subscribe",
                            Title        = "订阅",
                            Caption      = @"订阅",
                            Description  = @"订阅",
                            ColumnName   = "subscribe",
                            PropertyType = typeof(bool),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_subscribe_time,
                        new PropertySturct
                        {
                            Index        = subscribe_time,
                            Name         = "subscribe_time",
                            Title        = "订阅时间",
                            Caption      = @"订阅时间",
                            Description  = @"订阅时间",
                            ColumnName   = "subscribe_time",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_UserName,
                        new PropertySturct
                        {
                            Index        = UserName,
                            Name         = "UserName",
                            Title        = "用户名",
                            Caption      = @"用户名",
                            Description  = @"用户名",
                            ColumnName   = "UserName",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_UserFace,
                        new PropertySturct
                        {
                            Index        = UserFace,
                            Name         = "UserFace",
                            Title        = "用户面",
                            Caption      = @"用户面",
                            Description  = @"用户面",
                            ColumnName   = "UserFace",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Age,
                        new PropertySturct
                        {
                            Index        = Age,
                            Name         = "Age",
                            Title        = "年龄",
                            Caption      = @"年龄",
                            Description  = @"年龄",
                            ColumnName   = "Age",
                            PropertyType = typeof(int),
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
                            ColumnName   = "Birthday",
                            PropertyType = typeof(DateTime),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_HouseInfo,
                        new PropertySturct
                        {
                            Index        = HouseInfo,
                            Name         = "HouseInfo",
                            Title        = "住宅信息",
                            Caption      = @"住宅信息",
                            Description  = @"住宅信息",
                            ColumnName   = "HouseInfo",
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
                        Real_Hight,
                        new PropertySturct
                        {
                            Index        = Hight,
                            Name         = "Hight",
                            Title        = "海特",
                            Caption      = @"海特",
                            Description  = @"海特",
                            ColumnName   = "Hight",
                            PropertyType = typeof(int),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Weight,
                        new PropertySturct
                        {
                            Index        = Weight,
                            Name         = "Weight",
                            Title        = "重量",
                            Caption      = @"重量",
                            Description  = @"重量",
                            ColumnName   = "Weight",
                            PropertyType = typeof(float),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_QQID,
                        new PropertySturct
                        {
                            Index        = QQID,
                            Name         = "QQID",
                            Title        = "QQID",
                            Caption      = @"QQID",
                            Description  = @"QQID",
                            ColumnName   = "QQID",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_WXID,
                        new PropertySturct
                        {
                            Index        = WXID,
                            Name         = "WXID",
                            Title        = "WXID",
                            Caption      = @"WXID",
                            Description  = @"WXID",
                            ColumnName   = "WXID",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_UserState,
                        new PropertySturct
                        {
                            Index        = UserState,
                            Name         = "UserState",
                            Title        = "用户状态",
                            Caption      = @"用户状态",
                            Description  = @"用户状态",
                            ColumnName   = "UserState",
                            PropertyType = typeof(bool),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_LastLoginTime,
                        new PropertySturct
                        {
                            Index        = LastLoginTime,
                            Name         = "LastLoginTime",
                            Title        = "上次登录时间",
                            Caption      = @"上次登录时间",
                            Description  = @"上次登录时间",
                            ColumnName   = "LastLoginTime",
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
                        Real_RecommendCode,
                        new PropertySturct
                        {
                            Index        = RecommendCode,
                            Name         = "RecommendCode",
                            Title        = "推荐代码",
                            Caption      = @"推荐代码",
                            Description  = @"推荐代码",
                            ColumnName   = "RecommendCode",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_RecommendUseID,
                        new PropertySturct
                        {
                            Index        = RecommendUseID,
                            Name         = "RecommendUseID",
                            Title        = "推荐使用标识",
                            Caption      = @"推荐使用标识",
                            Description  = @"推荐者",
                            ColumnName   = "RecommendUseID",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_BonusPoints,
                        new PropertySturct
                        {
                            Index        = BonusPoints,
                            Name         = "BonusPoints",
                            Title        = "加分",
                            Caption      = @"加分",
                            Description  = @"加分",
                            ColumnName   = "BonusPoints",
                            PropertyType = typeof(int),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_CodeTicket,
                        new PropertySturct
                        {
                            Index        = CodeTicket,
                            Name         = "CodeTicket",
                            Title        = "代码票",
                            Caption      = @"代码票",
                            Description  = @"代码票",
                            ColumnName   = "CodeTicket",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_MessagePromptType,
                        new PropertySturct
                        {
                            Index        = MessagePromptType,
                            Name         = "MessagePromptType",
                            Title        = "快速语音信号",
                            Caption      = @"快速语音信号",
                            Description  = @"快速语音信号",
                            ColumnName   = "MessagePromptType",
                            PropertyType = typeof(int),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_MessagePromptLastTime,
                        new PropertySturct
                        {
                            Index        = MessagePromptLastTime,
                            Name         = "MessagePromptLastTime",
                            Title        = "上次消息提示",
                            Caption      = @"上次消息提示",
                            Description  = @"上次消息提示",
                            ColumnName   = "MessagePromptLastTime",
                            PropertyType = typeof(DateTime),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_CustomArea,
                        new PropertySturct
                        {
                            Index        = CustomArea,
                            Name         = "CustomArea",
                            Title        = "定制区",
                            Caption      = @"定制区",
                            Description  = @"定制区",
                            ColumnName   = "CustomArea",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_NativePlaceProvince,
                        new PropertySturct
                        {
                            Index        = NativePlaceProvince,
                            Name         = "NativePlaceProvince",
                            Title        = "本地省",
                            Caption      = @"本地省",
                            Description  = @"本地省",
                            ColumnName   = "NativePlaceProvince",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_NativePlaceCity,
                        new PropertySturct
                        {
                            Index        = NativePlaceCity,
                            Name         = "NativePlaceCity",
                            Title        = "本地城市",
                            Caption      = @"本地城市",
                            Description  = @"本地城市",
                            ColumnName   = "NativePlaceCity",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_NativePlaceArea,
                        new PropertySturct
                        {
                            Index        = NativePlaceArea,
                            Name         = "NativePlaceArea",
                            Title        = "本地区",
                            Caption      = @"本地区",
                            Description  = @"本地区",
                            ColumnName   = "NativePlaceArea",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Profession,
                        new PropertySturct
                        {
                            Index        = Profession,
                            Name         = "Profession",
                            Title        = "职业",
                            Caption      = @"职业",
                            Description  = @"职业",
                            ColumnName   = "Profession",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_TimeVisitOfflineLast,
                        new PropertySturct
                        {
                            Index        = TimeVisitOfflineLast,
                            Name         = "TimeVisitOfflineLast",
                            Title        = "上次离线访问时间",
                            Caption      = @"上次离线访问时间",
                            Description  = @"上次离线访问时间",
                            ColumnName   = "TimeVisitOfflineLast",
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