/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2017/9/16 22:47:59*/
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
using Gboxt.Common.DataModel;

using Newtonsoft.Json;
using  Agebull.ZeroNet.ManageApplication;
using Gboxt.Common.DataModel.Extends;

namespace Agebull.ZeroNet.ManageApplication
{
    /// <summary>
    /// 附件
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class AnnexData : IStateData , IHistoryData ,  IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public AnnexData()
        {
            Initialize();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        partial void Initialize();
        #endregion


        #region 属性字义


        /// <summary>
        /// 修改主键
        /// </summary>
        public void ChangePrimaryKey(int id)
        {
            _id = id;
        }
        
        /// <summary>
        /// 标识:标识的实时记录顺序
        /// </summary>
        internal const int Real_ID = 0;

        /// <summary>
        /// 标识:标识
        /// </summary>
        [DataMember,JsonIgnore]
        internal long _id;

        partial void OnIDGet();

        partial void OnIDSet(ref long value);

        partial void OnIDLoad(ref long value);

        partial void OnIDSeted();

        /// <summary>
        /// 标识:标识
        /// </summary>
        /// <remarks>
        /// 标识
        /// </remarks>
        [IgnoreDataMember , JsonProperty("ID", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"标识")]
        public long ID
        {
            get
            {
                OnIDGet();
                return this._id;
            }
            set
            {
                if(this._id == value)
                    return;
                //if(this._id > 0)
                //    throw new Exception("主键一旦设置就不可以修改");
                OnIDSet(ref value);
                this._id = value;
                this.OnPropertyChanged(Real_ID);
                OnIDSeted();
            }
        }
        /// <summary>
        /// 附件标题:附件标题的实时记录顺序
        /// </summary>
        internal const int Real_Title = 1;

        /// <summary>
        /// 附件标题:附件标题
        /// </summary>
        [DataMember,JsonIgnore]
        internal string _title;

        partial void OnTitleGet();

        partial void OnTitleSet(ref string value);

        partial void OnTitleSeted();

        /// <summary>
        /// 附件标题:附件标题
        /// </summary>
        /// <remarks>
        /// 附件标题
        /// </remarks>
        [IgnoreDataMember , JsonProperty("Title", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"附件标题")]
        public  string Title
        {
            get
            {
                OnTitleGet();
                return this._title;
            }
            set
            {
                if(this._title == value)
                    return;
                OnTitleSet(ref value);
                this._title = value;
                OnTitleSeted();
                this.OnPropertyChanged(Real_Title);
            }
        }
        /// <summary>
        /// 附件类型:附件类型的实时记录顺序
        /// </summary>
        internal const int Real_AnnexType = 2;

        /// <summary>
        /// 附件类型:附件类型
        /// </summary>
        [DataMember,JsonIgnore]
        internal AnnexType _annextype;

        partial void OnAnnexTypeGet();

        partial void OnAnnexTypeSet(ref AnnexType value);

        partial void OnAnnexTypeSeted();

        /// <summary>
        /// 附件类型:附件类型
        /// </summary>
        /// <remarks>
        /// 附件类型
        /// </remarks>
        [IgnoreDataMember , JsonProperty("AnnexType", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"附件类型")]
        public  AnnexType AnnexType
        {
            get
            {
                OnAnnexTypeGet();
                return this._annextype;
            }
            set
            {
                if(this._annextype == value)
                    return;
                OnAnnexTypeSet(ref value);
                this._annextype = value;
                OnAnnexTypeSeted();
                this.OnPropertyChanged(Real_AnnexType);
            }
        }
        /// <summary>
        /// 附件类型的可读内容
        /// </summary>
        [IgnoreDataMember,JsonIgnore,DisplayName("附件类型")]
        public string AnnexType_Content
        {
            get
            {
                switch(AnnexType)
                {
                case AnnexType.None:
                    return @"未知";
                case AnnexType.Wrod:
                    return @"Word文档";
                case AnnexType.Excel:
                    return @"Excel文档";
                case AnnexType.Pdf:
                    return @"PDF文档";
                case AnnexType.Audio:
                    return @"声音文件";
                case AnnexType.Video:
                    return @"视频文件";
                case AnnexType.Image:
                    return @"图片文件";
                case AnnexType.Ppt:
                    return @"PPT文件";
                case AnnexType.WPS:
                    return @"WPS文件";
                case AnnexType.Text:
                    return @"文本文件";
                default:
                    return null;
                }
            }
        }
        /// <summary>
        /// 附件类型的数字属性
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public  int AnnexType_Number
        {
            get
            {
                return (int)this.AnnexType;
            }
            set
            {
                this.AnnexType = (AnnexType)value;
            }
        }
        /// <summary>
        /// 连接类型:连接类型的实时记录顺序
        /// </summary>
        internal const int Real_EntityType = 3;

        /// <summary>
        /// 连接类型:连接类型
        /// </summary>
        [DataMember,JsonIgnore]
        internal int _entitytype;

        partial void OnEntityTypeGet();

        partial void OnEntityTypeSet(ref int value);

        partial void OnEntityTypeSeted();

        /// <summary>
        /// 连接类型:连接类型
        /// </summary>
        /// <remarks>
        /// 连接类型
        /// </remarks>
        [IgnoreDataMember , JsonProperty("EntityType", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"连接类型")]
        public  int EntityType
        {
            get
            {
                OnEntityTypeGet();
                return this._entitytype;
            }
            set
            {
                if(this._entitytype == value)
                    return;
                OnEntityTypeSet(ref value);
                this._entitytype = value;
                OnEntityTypeSeted();
                this.OnPropertyChanged(Real_EntityType);
            }
        }
        /// <summary>
        /// 关联标识:关联标识的实时记录顺序
        /// </summary>
        internal const int Real_LinkId = 4;

        /// <summary>
        /// 关联标识:关联标识
        /// </summary>
        [DataMember,JsonIgnore]
        internal int _linkid;

        partial void OnLinkIdGet();

        partial void OnLinkIdSet(ref int value);

        partial void OnLinkIdSeted();

        /// <summary>
        /// 关联标识:关联标识
        /// </summary>
        /// <remarks>
        /// 关联标识
        /// </remarks>
        [IgnoreDataMember , JsonProperty("LinkId", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"关联标识")]
        public  int LinkId
        {
            get
            {
                OnLinkIdGet();
                return this._linkid;
            }
            set
            {
                if(this._linkid == value)
                    return;
                OnLinkIdSet(ref value);
                this._linkid = value;
                OnLinkIdSeted();
                this.OnPropertyChanged(Real_LinkId);
            }
        }
        /// <summary>
        /// 文件名称:如果需要更新文件,请选择一个文件上传的实时记录顺序
        /// </summary>
        internal const int Real_FileName = 5;

        /// <summary>
        /// 文件名称:如果需要更新文件,请选择一个文件上传
        /// </summary>
        [DataMember,JsonIgnore]
        internal string _filename;

        partial void OnFileNameGet();

        partial void OnFileNameSet(ref string value);

        partial void OnFileNameSeted();

        /// <summary>
        /// 文件名称:如果需要更新文件,请选择一个文件上传
        /// </summary>
        /// <remarks>
        /// 如果需要更新文件,请选择一个文件上传
        /// </remarks>
        [IgnoreDataMember , JsonProperty("FileName", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"文件名称")]
        public  string FileName
        {
            get
            {
                OnFileNameGet();
                return this._filename;
            }
            set
            {
                if(this._filename == value)
                    return;
                OnFileNameSet(ref value);
                this._filename = value;
                OnFileNameSeted();
                this.OnPropertyChanged(Real_FileName);
            }
        }
        /// <summary>
        /// 是否公开:是否公开的实时记录顺序
        /// </summary>
        internal const int Real_IsPublic = 6;

        /// <summary>
        /// 是否公开:是否公开
        /// </summary>
        [DataMember,JsonIgnore]
        internal bool _ispublic;

        partial void OnIsPublicGet();

        partial void OnIsPublicSet(ref bool value);

        partial void OnIsPublicSeted();

        /// <summary>
        /// 是否公开:是否公开
        /// </summary>
        /// <remarks>
        /// 是否公开
        /// </remarks>
        [IgnoreDataMember , JsonProperty("IsPublic", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"是否公开")]
        public  bool IsPublic
        {
            get
            {
                OnIsPublicGet();
                return this._ispublic;
            }
            set
            {
                if(this._ispublic == value)
                    return;
                OnIsPublicSet(ref value);
                this._ispublic = value;
                OnIsPublicSeted();
                this.OnPropertyChanged(Real_IsPublic);
            }
        }
        /// <summary>
        /// 连接地址:连接地址的实时记录顺序
        /// </summary>
        internal const int Real_Url = 7;

        /// <summary>
        /// 连接地址:连接地址
        /// </summary>
        [DataMember,JsonIgnore]
        internal string _url;

        partial void OnUrlGet();

        partial void OnUrlSet(ref string value);

        partial void OnUrlSeted();

        /// <summary>
        /// 连接地址:连接地址
        /// </summary>
        /// <remarks>
        /// 连接地址
        /// </remarks>
        [IgnoreDataMember , JsonProperty("Url", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"连接地址")]
        public  string Url
        {
            get
            {
                OnUrlGet();
                return this._url;
            }
            set
            {
                if(this._url == value)
                    return;
                OnUrlSet(ref value);
                this._url = value;
                OnUrlSeted();
                this.OnPropertyChanged(Real_Url);
            }
        }
        /// <summary>
        /// 存储地址:存储地址的实时记录顺序
        /// </summary>
        internal const int Real_Storage = 8;

        /// <summary>
        /// 存储地址:存储地址
        /// </summary>
        [DataMember,JsonIgnore]
        internal string _storage;

        partial void OnStorageGet();

        partial void OnStorageSet(ref string value);

        partial void OnStorageSeted();

        /// <summary>
        /// 存储地址:存储地址
        /// </summary>
        /// <remarks>
        /// 存储地址
        /// </remarks>
        [IgnoreDataMember , JsonIgnore , Browsable(false) , DisplayName(@"存储地址")]
        internal string Storage
        {
            get
            {
                OnStorageGet();
                return this._storage;
            }
            set
            {
                if(this._storage == value)
                    return;
                OnStorageSet(ref value);
                this._storage = value;
                OnStorageSeted();
                this.OnPropertyChanged(Real_Storage);
            }
        }
        /// <summary>
        /// 备注:备注的实时记录顺序
        /// </summary>
        internal const int Real_Memo = 9;

        /// <summary>
        /// 备注:备注
        /// </summary>
        [DataMember,JsonIgnore]
        internal string _memo;

        partial void OnMemoGet();

        partial void OnMemoSet(ref string value);

        partial void OnMemoSeted();

        /// <summary>
        /// 备注:备注
        /// </summary>
        /// <remarks>
        /// 备注
        /// </remarks>
        [IgnoreDataMember , JsonProperty("Memo", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"备注")]
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
                this.OnPropertyChanged(Real_Memo);
            }
        }
        /// <summary>
        /// 数据状态:数据状态的实时记录顺序
        /// </summary>
        internal const int Real_DataState = 10;

        /// <summary>
        /// 数据状态:数据状态
        /// </summary>
        [DataMember,JsonIgnore]
        internal DataStateType _datastate;

        partial void OnDataStateGet();

        partial void OnDataStateSet(ref DataStateType value);

        partial void OnDataStateSeted();

        /// <summary>
        /// 数据状态:数据状态
        /// </summary>
        /// <remarks>
        /// 数据状态
        /// </remarks>
        [IgnoreDataMember , JsonProperty("DataState", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"数据状态")]
        public  DataStateType DataState
        {
            get
            {
                OnDataStateGet();
                return this._datastate;
            }
            set
            {
                if(this._datastate == value)
                    return;
                OnDataStateSet(ref value);
                this._datastate = value;
                OnDataStateSeted();
                this.OnPropertyChanged(Real_DataState);
            }
        }
        /// <summary>
        /// 数据是否已冻结:数据是否已冻结的实时记录顺序
        /// </summary>
        internal const int Real_IsFreeze = 11;

        /// <summary>
        /// 数据是否已冻结:数据是否已冻结
        /// </summary>
        [DataMember,JsonIgnore]
        internal bool _isfreeze;

        partial void OnIsFreezeGet();

        partial void OnIsFreezeSet(ref bool value);

        partial void OnIsFreezeSeted();

        /// <summary>
        /// 数据是否已冻结:数据是否已冻结
        /// </summary>
        /// <remarks>
        /// 数据是否已冻结
        /// </remarks>
        [IgnoreDataMember , JsonProperty("IsFreeze", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"数据是否已冻结")]
        public  bool IsFreeze
        {
            get
            {
                OnIsFreezeGet();
                return this._isfreeze;
            }
            set
            {
                if(this._isfreeze == value)
                    return;
                OnIsFreezeSet(ref value);
                this._isfreeze = value;
                OnIsFreezeSeted();
                this.OnPropertyChanged(Real_IsFreeze);
            }
        }
        /// <summary>
        /// 制作人:制作人的实时记录顺序
        /// </summary>
        internal const int Real_AuthorID = 12;

        /// <summary>
        /// 制作人:制作人
        /// </summary>
        [DataMember,JsonIgnore]
        internal long _authorid;

        partial void OnAuthorIDGet();

        partial void OnAuthorIDSet(ref long value);

        partial void OnAuthorIDSeted();

        /// <summary>
        /// 制作人:制作人
        /// </summary>
        /// <remarks>
        /// 制作人
        /// </remarks>
        [IgnoreDataMember , JsonProperty("AuthorId", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"制作人")]
        public  long AuthorId
        {
            get
            {
                OnAuthorIDGet();
                return this._authorid;
            }
            set
            {
                if(this._authorid == value)
                    return;
                OnAuthorIDSet(ref value);
                this._authorid = value;
                OnAuthorIDSeted();
                this.OnPropertyChanged(Real_AuthorID);
            }
        }
        /// <summary>
        /// 制作时间:制作时间的实时记录顺序
        /// </summary>
        internal const int Real_AddDate = 13;

        /// <summary>
        /// 制作时间:制作时间
        /// </summary>
        [DataMember,JsonIgnore]
        internal DateTime _adddate;

        partial void OnAddDateGet();

        partial void OnAddDateSet(ref DateTime value);

        partial void OnAddDateSeted();

        /// <summary>
        /// 制作时间:制作时间
        /// </summary>
        /// <remarks>
        /// 制作时间
        /// </remarks>
        [IgnoreDataMember , JsonProperty("AddDate", NullValueHandling = NullValueHandling.Ignore) , JsonConverter(typeof(MyDateTimeConverter)) , DisplayName(@"制作时间")]
        public  DateTime AddDate
        {
            get
            {
                OnAddDateGet();
                return this._adddate;
            }
            set
            {
                if(this._adddate == value)
                    return;
                OnAddDateSet(ref value);
                this._adddate = value;
                OnAddDateSeted();
                this.OnPropertyChanged(Real_AddDate);
            }
        }
        /// <summary>
        /// 最后修改者:最后修改者的实时记录顺序
        /// </summary>
        internal const int Real_LastReviserID = 14;

        /// <summary>
        /// 最后修改者:最后修改者
        /// </summary>
        [DataMember,JsonIgnore]
        internal long _lastreviserid;

        partial void OnLastReviserIDGet();

        partial void OnLastReviserIDSet(ref long value);

        partial void OnLastReviserIDSeted();

        /// <summary>
        /// 最后修改者:最后修改者
        /// </summary>
        /// <remarks>
        /// 最后修改者
        /// </remarks>
        [IgnoreDataMember , JsonProperty("LastReviserId", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"最后修改者")]
        public long LastReviserId
        {
            get
            {
                OnLastReviserIDGet();
                return this._lastreviserid;
            }
            set
            {
                if(this._lastreviserid == value)
                    return;
                OnLastReviserIDSet(ref value);
                this._lastreviserid = value;
                OnLastReviserIDSeted();
                this.OnPropertyChanged(Real_LastReviserID);
            }
        }
        /// <summary>
        /// 最后修改日期:最后修改日期的实时记录顺序
        /// </summary>
        internal const int Real_LastModifyDate = 15;

        /// <summary>
        /// 最后修改日期:最后修改日期
        /// </summary>
        [DataMember,JsonIgnore]
        internal DateTime _lastmodifydate;

        partial void OnLastModifyDateGet();

        partial void OnLastModifyDateSet(ref DateTime value);

        partial void OnLastModifyDateSeted();

        /// <summary>
        /// 最后修改日期:最后修改日期
        /// </summary>
        /// <remarks>
        /// 最后修改日期
        /// </remarks>
        [IgnoreDataMember , JsonProperty("LastModifyDate", NullValueHandling = NullValueHandling.Ignore) , JsonConverter(typeof(MyDateTimeConverter)) , DisplayName(@"最后修改日期")]
        public  DateTime LastModifyDate
        {
            get
            {
                OnLastModifyDateGet();
                return this._lastmodifydate;
            }
            set
            {
                if(this._lastmodifydate == value)
                    return;
                OnLastModifyDateSet(ref value);
                this._lastmodifydate = value;
                OnLastModifyDateSeted();
                this.OnPropertyChanged(Real_LastModifyDate);
            }
        }
        /// <summary>
        /// 区域标识:区域标识的实时记录顺序
        /// </summary>
        internal const int Real_AreaId = 16;

        /// <summary>
        /// 区域标识:区域标识
        /// </summary>
        [DataMember,JsonIgnore]
        internal int _areaid;

        partial void OnAreaIdGet();

        partial void OnAreaIdSet(ref int value);

        partial void OnAreaIdSeted();

        /// <summary>
        /// 区域标识:区域标识
        /// </summary>
        /// <remarks>
        /// 区域标识
        /// </remarks>
        [IgnoreDataMember , JsonProperty("AreaId", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"区域标识")]
        public  int AreaId
        {
            get
            {
                OnAreaIdGet();
                return this._areaid;
            }
            set
            {
                if(this._areaid == value)
                    return;
                OnAreaIdSet(ref value);
                this._areaid = value;
                OnAreaIdSeted();
                this.OnPropertyChanged(Real_AreaId);
            }
        }
        /// <summary>
        /// 部门所有者:实现本部门查看的实时记录顺序
        /// </summary>
        internal const int Real_DepartmentId = 17;

        /// <summary>
        /// 部门所有者:实现本部门查看
        /// </summary>
        [DataMember,JsonIgnore]
        internal int _departmentid;

        partial void OnDepartmentIdGet();

        partial void OnDepartmentIdSet(ref int value);

        partial void OnDepartmentIdSeted();

        /// <summary>
        /// 部门所有者:实现本部门查看
        /// </summary>
        /// <remarks>
        /// 实现本部门查看
        /// </remarks>
        [IgnoreDataMember , JsonIgnore , DisplayName(@"部门所有者")]
        public  int DepartmentId
        {
            get
            {
                OnDepartmentIdGet();
                return this._departmentid;
            }
            set
            {
                if(this._departmentid == value)
                    return;
                OnDepartmentIdSet(ref value);
                this._departmentid = value;
                OnDepartmentIdSeted();
                this.OnPropertyChanged(Real_DepartmentId);
            }
        }
        
        /// <summary>
        /// 微信的MediaId:微信的MediaId的实时记录顺序
        /// </summary>
        internal const int Real_WeiXinMediaId = 18;

        /// <summary>
        /// 微信的MediaId:微信的MediaId
        /// </summary>
        [DataMember,JsonIgnore]
        internal string _weixinmediaid;

        partial void OnWeiXinMediaIdGet();

        partial void OnWeiXinMediaIdSet(ref string value);

        partial void OnWeiXinMediaIdSeted();

        /// <summary>
        /// 微信的MediaId:微信的MediaId
        /// </summary>
        /// <remarks>
        /// 微信的MediaId
        /// </remarks>
        [IgnoreDataMember , JsonProperty("WeiXinMediaId", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"微信的MediaId")]
        public  string WeiXinMediaId
        {
            get
            {
                OnWeiXinMediaIdGet();
                return this._weixinmediaid;
            }
            set
            {
                if(this._weixinmediaid == value)
                    return;
                OnWeiXinMediaIdSet(ref value);
                this._weixinmediaid = value;
                OnWeiXinMediaIdSeted();
                this.OnPropertyChanged(Real_WeiXinMediaId);
            }
        }
        /// <summary>
        /// 部门级别:实现按级别查看的实时记录顺序
        /// </summary>
        internal const int Real_DepartmentLevel = 19;

        /// <summary>
        /// 部门级别:实现按级别查看
        /// </summary>
        [DataMember,JsonIgnore]
        internal int _departmentlevel;

        partial void OnDepartmentLevelGet();

        partial void OnDepartmentLevelSet(ref int value);

        partial void OnDepartmentLevelSeted();

        /// <summary>
        /// 部门级别:实现按级别查看
        /// </summary>
        /// <remarks>
        /// 实现按级别查看
        /// </remarks>
        [IgnoreDataMember , JsonIgnore , DisplayName(@"部门级别")]
        public  int DepartmentLevel
        {
            get
            {
                OnDepartmentLevelGet();
                return this._departmentlevel;
            }
            set
            {
                if(this._departmentlevel == value)
                    return;
                OnDepartmentLevelSet(ref value);
                this._departmentlevel = value;
                OnDepartmentLevelSeted();
                this.OnPropertyChanged(Real_DepartmentLevel);
            }
        }
        #endregion

        #region IIdentityData接口


        /// <summary>
        /// 对象标识
        /// </summary>
        [IgnoreDataMember,Browsable(false)]
        public long Id
        {
            get
            {
                return this.ID;
            }
            set
            {
                this.ID = value;
            }
        }

        #endregion
        #region 属性扩展


    

        /// <summary>
        ///     设置属性值
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        protected override void SetValueInner(string property, object value)
        {
            switch(property.Trim().ToLower())
            {
            case "id":
                this.ID = (int)Convert.ToDecimal(value);
                return;
            case "title":
                this.Title = value == null ? null : value.ToString();
                return;
            case "annextype":
                if (value != null)
                {
                    if(value is int)
                    {
                        this.AnnexType = (AnnexType)(int)value;
                    }
                    else if(value is AnnexType)
                    {
                        this.AnnexType = (AnnexType)value;
                    }
                    else
                    {
                        var str = value.ToString();
                        AnnexType val;
                        if (AnnexType.TryParse(str, out val))
                        {
                            this.AnnexType = val;
                        }
                        else
                        {
                            int vl;
                            if (int.TryParse(str, out vl))
                            {
                                this.AnnexType = (AnnexType)vl;
                            }
                        }
                    }
                }
                return;
            case "entitytype":
                this.EntityType = (int)Convert.ToDecimal(value);
                return;
            case "linkid":
                this.LinkId = (int)Convert.ToDecimal(value);
                return;
            case "filename":
                this.FileName = value == null ? null : value.ToString();
                return;
            case "ispublic":
                if (value != null)
                {
                    int vl;
                    if (int.TryParse(value.ToString(), out vl))
                    {
                        this.IsPublic = vl != 0;
                    }
                    else
                    {
                        this.IsPublic = Convert.ToBoolean(value);
                    }
                }
                return;
            case "url":
                this.Url = value == null ? null : value.ToString();
                return;
            case "storage":
                this.Storage = value == null ? null : value.ToString();
                return;
            case "memo":
                this.Memo = value == null ? null : value.ToString();
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
            case "authorid":
                this.AuthorId = (int)Convert.ToDecimal(value);
                return;
            case "adddate":
                this.AddDate = Convert.ToDateTime(value);
                return;
            case "lastreviserid":
                this.LastReviserId = (int)Convert.ToDecimal(value);
                return;
            case "lastmodifydate":
                this.LastModifyDate = Convert.ToDateTime(value);
                return;
            case "areaid":
                this.AreaId = (int)Convert.ToDecimal(value);
                return;
            case "departmentid":
                this.DepartmentId = (int)Convert.ToDecimal(value);
                return;
            case "weixinmediaid":
                this.WeiXinMediaId = value == null ? null : value.ToString();
                return;
            case "departmentlevel":
                this.DepartmentLevel = (int)Convert.ToDecimal(value);
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
            /*switch(index)
            {
            case Index_ID:
                this.ID = Convert.ToInt32(value);
                return;
            case Index_Title:
                this.Title = value == null ? null : value.ToString();
                return;
            case Index_AnnexType:
                this.AnnexType = (AnnexType)value;
                return;
            case Index_EntityType:
                this.EntityType = Convert.ToInt32(value);
                return;
            case Index_LinkId:
                this.LinkId = Convert.ToInt32(value);
                return;
            case Index_FileName:
                this.FileName = value == null ? null : value.ToString();
                return;
            case Index_IsPublic:
                this.IsPublic = Convert.ToBoolean(value);
                return;
            case Index_Url:
                this.Url = value == null ? null : value.ToString();
                return;
            case Index_Storage:
                this.Storage = value == null ? null : value.ToString();
                return;
            case Index_Memo:
                this.Memo = value == null ? null : value.ToString();
                return;
            case Index_DataState:
                this.DataState = (DataStateType)value;
                return;
            case Index_IsFreeze:
                this.IsFreeze = Convert.ToBoolean(value);
                return;
            case Index_AuthorID:
                this.AuthorID = Convert.ToInt32(value);
                return;
            case Index_AddDate:
                this.AddDate = Convert.ToDateTime(value);
                return;
            case Index_LastReviserID:
                this.LastReviserID = Convert.ToInt32(value);
                return;
            case Index_LastModifyDate:
                this.LastModifyDate = Convert.ToDateTime(value);
                return;
            case Index_AreaId:
                this.AreaId = Convert.ToInt32(value);
                return;
            case Index_DepartmentId:
                this.DepartmentId = Convert.ToInt32(value);
                return;
            case Index_ViewScope:
                this.ViewScope = (ViewScopeType)value;
                return;
            case Index_WeiXinMediaId:
                this.WeiXinMediaId = value == null ? null : value.ToString();
                return;
            case Index_DepartmentLevel:
                this.DepartmentLevel = Convert.ToInt32(value);
                return;
            }*/
        }


        /// <summary>
        ///     读取属性值
        /// </summary>
        /// <param name="property"></param>
        protected override object GetValueInner(string property)
        {
            switch(property)
            {
            case "ID":
                return this.ID;
            case "Title":
                return this.Title;
            case "AnnexType":
                return this.AnnexType.ToCaption();
            case "EntityType":
                return this.EntityType;
            case "LinkId":
                return this.LinkId;
            case "FileName":
                return this.FileName;
            case "IsPublic":
                return this.IsPublic;
            case "Url":
                return this.Url;
            case "Storage":
                return this.Storage;
            case "Memo":
                return this.Memo;
            case "DataState":
                return this.DataState;
            case "IsFreeze":
                return this.IsFreeze;
            case "AuthorID":
                return this.AuthorId;
            case "AddDate":
                return this.AddDate;
            case "LastReviserID":
                return this.LastReviserId;
            case "LastModifyDate":
                return this.LastModifyDate;
            case "AreaId":
                return this.AreaId;
            case "DepartmentId":
                return this.DepartmentId;
            case "WeiXinMediaId":
                return this.WeiXinMediaId;
            case "DepartmentLevel":
                return this.DepartmentLevel;
            }

            return null;
        }


        /// <summary>
        ///     读取属性值
        /// </summary>
        /// <param name="index"></param>
        protected override object GetValueInner(int index)
        {
            /*switch(index)
            {
                case Index_ID:
                    return this.ID;
                case Index_Title:
                    return this.Title;
                case Index_AnnexType:
                    return this.AnnexType;
                case Index_EntityType:
                    return this.EntityType;
                case Index_LinkId:
                    return this.LinkId;
                case Index_FileName:
                    return this.FileName;
                case Index_IsPublic:
                    return this.IsPublic;
                case Index_Url:
                    return this.Url;
                case Index_Storage:
                    return this.Storage;
                case Index_Memo:
                    return this.Memo;
                case Index_DataState:
                    return this.DataState;
                case Index_IsFreeze:
                    return this.IsFreeze;
                case Index_AuthorID:
                    return this.AuthorID;
                case Index_AddDate:
                    return this.AddDate;
                case Index_LastReviserID:
                    return this.LastReviserID;
                case Index_LastModifyDate:
                    return this.LastModifyDate;
                case Index_AreaId:
                    return this.AreaId;
                case Index_DepartmentId:
                    return this.DepartmentId;
                case Index_ViewScope:
                    return this.ViewScope;
                case Index_WeiXinMediaId:
                    return this.WeiXinMediaId;
                case Index_DepartmentLevel:
                    return this.DepartmentLevel;
            }*/

            return null;
        }

        #endregion

        #region 关联

        #endregion

        #region 复制


        partial void CopyExtendValue(AnnexData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as AnnexData;
            if(sourceEntity == null)
                return;
            this._id = sourceEntity._id;
            this._title = sourceEntity._title;
            this._annextype = sourceEntity._annextype;
            this._entitytype = sourceEntity._entitytype;
            this._linkid = sourceEntity._linkid;
            this._filename = sourceEntity._filename;
            this._ispublic = sourceEntity._ispublic;
            this._url = sourceEntity._url;
            this._storage = sourceEntity._storage;
            this._memo = sourceEntity._memo;
            this._datastate = sourceEntity._datastate;
            this._isfreeze = sourceEntity._isfreeze;
            this._authorid = sourceEntity._authorid;
            this._adddate = sourceEntity._adddate;
            this._lastreviserid = sourceEntity._lastreviserid;
            this._lastmodifydate = sourceEntity._lastmodifydate;
            this._areaid = sourceEntity._areaid;
            this._departmentid = sourceEntity._departmentid;
            this._weixinmediaid = sourceEntity._weixinmediaid;
            this._departmentlevel = sourceEntity._departmentlevel;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(AnnexData source)
        {
                this.ID = source.ID;
                this.Title = source.Title;
                this.AnnexType = source.AnnexType;
                this.EntityType = source.EntityType;
                this.LinkId = source.LinkId;
                this.FileName = source.FileName;
                this.IsPublic = source.IsPublic;
                this.Url = source.Url;
                this.Storage = source.Storage;
                this.Memo = source.Memo;
                this.DataState = source.DataState;
                this.IsFreeze = source.IsFreeze;
                this.AuthorId = source.AuthorId;
                this.AddDate = source.AddDate;
                this.LastReviserId = source.LastReviserId;
                this.LastModifyDate = source.LastModifyDate;
                this.AreaId = source.AreaId;
                this.DepartmentId = source.DepartmentId;
                this.WeiXinMediaId = source.WeiXinMediaId;
                this.DepartmentLevel = source.DepartmentLevel;
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
                OnIDModified(subsist,false);
                OnTitleModified(subsist,false);
                OnAnnexTypeModified(subsist,false);
                OnEntityTypeModified(subsist,false);
                OnLinkIdModified(subsist,false);
                OnFileNameModified(subsist,false);
                OnIsPublicModified(subsist,false);
                OnUrlModified(subsist,false);
                OnStorageModified(subsist,false);
                OnMemoModified(subsist,false);
                OnDataStateModified(subsist,false);
                OnIsFreezeModified(subsist,false);
                OnAuthorIDModified(subsist,false);
                OnAddDateModified(subsist,false);
                OnLastReviserIDModified(subsist,false);
                OnLastModifyDateModified(subsist,false);
                OnAreaIdModified(subsist,false);
                OnDepartmentIdModified(subsist,false);
                OnViewScopeModified(subsist,false);
                OnWeiXinMediaIdModified(subsist,false);
                OnDepartmentLevelModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnIDModified(subsist,true);
                OnTitleModified(subsist,true);
                OnAnnexTypeModified(subsist,true);
                OnEntityTypeModified(subsist,true);
                OnLinkIdModified(subsist,true);
                OnFileNameModified(subsist,true);
                OnIsPublicModified(subsist,true);
                OnUrlModified(subsist,true);
                OnStorageModified(subsist,true);
                OnMemoModified(subsist,true);
                OnDataStateModified(subsist,true);
                OnIsFreezeModified(subsist,true);
                OnAuthorIDModified(subsist,true);
                OnAddDateModified(subsist,true);
                OnLastReviserIDModified(subsist,true);
                OnLastModifyDateModified(subsist,true);
                OnAreaIdModified(subsist,true);
                OnDepartmentIdModified(subsist,true);
                OnViewScopeModified(subsist,true);
                OnWeiXinMediaIdModified(subsist,true);
                OnDepartmentLevelModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[21] > 0)
            {
                OnIDModified(subsist,modifieds[Real_ID] == 1);
                OnTitleModified(subsist,modifieds[Real_Title] == 1);
                OnAnnexTypeModified(subsist,modifieds[Real_AnnexType] == 1);
                OnEntityTypeModified(subsist,modifieds[Real_EntityType] == 1);
                OnLinkIdModified(subsist,modifieds[Real_LinkId] == 1);
                OnFileNameModified(subsist,modifieds[Real_FileName] == 1);
                OnIsPublicModified(subsist,modifieds[Real_IsPublic] == 1);
                OnUrlModified(subsist,modifieds[Real_Url] == 1);
                OnStorageModified(subsist,modifieds[Real_Storage] == 1);
                OnMemoModified(subsist,modifieds[Real_Memo] == 1);
                OnDataStateModified(subsist,modifieds[Real_DataState] == 1);
                OnIsFreezeModified(subsist,modifieds[Real_IsFreeze] == 1);
                OnAuthorIDModified(subsist,modifieds[Real_AuthorID] == 1);
                OnAddDateModified(subsist,modifieds[Real_AddDate] == 1);
                OnLastReviserIDModified(subsist,modifieds[Real_LastReviserID] == 1);
                OnLastModifyDateModified(subsist,modifieds[Real_LastModifyDate] == 1);
                OnAreaIdModified(subsist,modifieds[Real_AreaId] == 1);
                OnDepartmentIdModified(subsist,modifieds[Real_DepartmentId] == 1);
                OnWeiXinMediaIdModified(subsist,modifieds[Real_WeiXinMediaId] == 1);
                OnDepartmentLevelModified(subsist,modifieds[Real_DepartmentLevel] == 1);
            }
        }

        #region 属性后期修改的分部方法

        /// <summary>
        /// 标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 附件标题修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnTitleModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 附件类型修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAnnexTypeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 连接类型修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnEntityTypeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 关联标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnLinkIdModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 文件名称修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnFileNameModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 是否公开修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnIsPublicModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 连接地址修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnUrlModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 存储地址修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnStorageModified(EntitySubsist subsist,bool isModified);

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
        /// 数据状态修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnDataStateModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 数据是否已冻结修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnIsFreezeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 制作人修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAuthorIDModified(EntitySubsist subsist,bool isModified);

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
        partial void OnLastReviserIDModified(EntitySubsist subsist,bool isModified);

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
        /// 区域标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAreaIdModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 部门所有者修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnDepartmentIdModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 可视范围修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnViewScopeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 微信的MediaId修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnWeiXinMediaIdModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 部门级别修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnDepartmentLevelModified(EntitySubsist subsist,bool isModified);

        #endregion

        #endregion

        #region 实体结构

        
        public const byte Index_ID = 1;
        public const byte Index_Title = 2;
        public const byte Index_AnnexType = 3;
        public const byte Index_EntityType = 4;
        public const byte Index_LinkId = 5;
        public const byte Index_FileName = 6;
        public const byte Index_IsPublic = 7;
        public const byte Index_Url = 8;
        public const byte Index_Storage = 9;
        public const byte Index_Memo = 10;
        public const byte Index_DataState = 163;
        public const byte Index_IsFreeze = 164;
        public const byte Index_AuthorID = 166;
        public const byte Index_AddDate = 168;
        public const byte Index_LastReviserID = 170;
        public const byte Index_LastModifyDate = 172;
        public const byte Index_AreaId = 173;
        public const byte Index_DepartmentId = 174;
        public const byte Index_ViewScope = 175;
        public const byte Index_WeiXinMediaId = 176;
        public const byte Index_DepartmentLevel = 177;

        /// <summary>
        /// 实体结构
        /// </summary>
        [IgnoreDataMember,Browsable (false)]
        public override EntitySturct __Struct
        {
            get
            {
                return __struct;
            }
        }

        /// <summary>
        /// 实体结构
        /// </summary>
        [IgnoreDataMember]
        static readonly EntitySturct __struct = new EntitySturct
        {
            EntityName = "Annex",
            PrimaryKey = "ID",
            EntityType = 0x20008,
            Properties = new Dictionary<int, PropertySturct>
            {
                {
                    Real_ID,
                    new PropertySturct
                    {
                        Index = Index_ID,
                        Name = "ID",
                        Title = "标识",
                        ColumnName = "id",
                        PropertyType = typeof(int),
                        CanNull = false,
                        ValueType = PropertyValueType.Value,
                        CanImport = true,
                        CanExport = true
                    }
                },
                {
                    Real_Title,
                    new PropertySturct
                    {
                        Index = Index_Title,
                        Name = "Title",
                        Title = "附件标题",
                        ColumnName = "title",
                        PropertyType = typeof(string),
                        CanNull = false,
                        ValueType = PropertyValueType.String,
                        CanImport = true,
                        CanExport = true
                    }
                },
                {
                    Real_AnnexType,
                    new PropertySturct
                    {
                        Index = Index_AnnexType,
                        Name = "AnnexType",
                        Title = "附件类型",
                        ColumnName = "annex_type",
                        PropertyType = typeof(AnnexType),
                        CanNull = false,
                        ValueType = PropertyValueType.Value,
                        CanImport = false,
                        CanExport = false
                    }
                },
                {
                    Real_EntityType,
                    new PropertySturct
                    {
                        Index = Index_EntityType,
                        Name = "EntityType",
                        Title = "连接类型",
                        ColumnName = "entity_type",
                        PropertyType = typeof(int),
                        CanNull = false,
                        ValueType = PropertyValueType.Value,
                        CanImport = false,
                        CanExport = false
                    }
                },
                {
                    Real_LinkId,
                    new PropertySturct
                    {
                        Index = Index_LinkId,
                        Name = "LinkId",
                        Title = "关联标识",
                        ColumnName = "link_id",
                        PropertyType = typeof(int),
                        CanNull = false,
                        ValueType = PropertyValueType.Value,
                        CanImport = false,
                        CanExport = false
                    }
                },
                {
                    Real_FileName,
                    new PropertySturct
                    {
                        Index = Index_FileName,
                        Name = "FileName",
                        Title = "文件名称",
                        ColumnName = "file_name",
                        PropertyType = typeof(string),
                        CanNull = false,
                        ValueType = PropertyValueType.String,
                        CanImport = true,
                        CanExport = true
                    }
                },
                {
                    Real_IsPublic,
                    new PropertySturct
                    {
                        Index = Index_IsPublic,
                        Name = "IsPublic",
                        Title = "是否公开",
                        ColumnName = "is_public",
                        PropertyType = typeof(bool),
                        CanNull = false,
                        ValueType = PropertyValueType.Value,
                        CanImport = true,
                        CanExport = true
                    }
                },
                {
                    Real_Url,
                    new PropertySturct
                    {
                        Index = Index_Url,
                        Name = "Url",
                        Title = "连接地址",
                        ColumnName = "url",
                        PropertyType = typeof(string),
                        CanNull = false,
                        ValueType = PropertyValueType.String,
                        CanImport = true,
                        CanExport = true
                    }
                },
                {
                    Real_Storage,
                    new PropertySturct
                    {
                        Index = Index_Storage,
                        Name = "Storage",
                        Title = "存储地址",
                        ColumnName = "storage",
                        PropertyType = typeof(string),
                        CanNull = false,
                        ValueType = PropertyValueType.String,
                        CanImport = false,
                        CanExport = false
                    }
                },
                {
                    Real_Memo,
                    new PropertySturct
                    {
                        Index = Index_Memo,
                        Name = "Memo",
                        Title = "备注",
                        ColumnName = "memo",
                        PropertyType = typeof(string),
                        CanNull = false,
                        ValueType = PropertyValueType.String,
                        CanImport = true,
                        CanExport = true
                    }
                },
                {
                    Real_DataState,
                    new PropertySturct
                    {
                        Index = Index_DataState,
                        Name = "DataState",
                        Title = "数据状态",
                        ColumnName = "data_state",
                        PropertyType = typeof(DataStateType),
                        CanNull = false,
                        ValueType = PropertyValueType.Value,
                        CanImport = false,
                        CanExport = false
                    }
                },
                {
                    Real_IsFreeze,
                    new PropertySturct
                    {
                        Index = Index_IsFreeze,
                        Name = "IsFreeze",
                        Title = "数据是否已冻结",
                        ColumnName = "is_freeze",
                        PropertyType = typeof(bool),
                        CanNull = false,
                        ValueType = PropertyValueType.Value,
                        CanImport = false,
                        CanExport = false
                    }
                },
                {
                    Real_AuthorID,
                    new PropertySturct
                    {
                        Index = Index_AuthorID,
                        Name = "AuthorID",
                        Title = "制作人",
                        ColumnName = "author_id",
                        PropertyType = typeof(int),
                        CanNull = false,
                        ValueType = PropertyValueType.Value,
                        CanImport = false,
                        CanExport = false
                    }
                },
                {
                    Real_AddDate,
                    new PropertySturct
                    {
                        Index = Index_AddDate,
                        Name = "AddDate",
                        Title = "制作时间",
                        ColumnName = "add_date",
                        PropertyType = typeof(DateTime),
                        CanNull = false,
                        ValueType = PropertyValueType.Value,
                        CanImport = false,
                        CanExport = false
                    }
                },
                {
                    Real_LastReviserID,
                    new PropertySturct
                    {
                        Index = Index_LastReviserID,
                        Name = "LastReviserID",
                        Title = "最后修改者",
                        ColumnName = "last_reviser_id",
                        PropertyType = typeof(int),
                        CanNull = false,
                        ValueType = PropertyValueType.Value,
                        CanImport = false,
                        CanExport = false
                    }
                },
                {
                    Real_LastModifyDate,
                    new PropertySturct
                    {
                        Index = Index_LastModifyDate,
                        Name = "LastModifyDate",
                        Title = "最后修改日期",
                        ColumnName = "last_modify_date",
                        PropertyType = typeof(DateTime),
                        CanNull = false,
                        ValueType = PropertyValueType.Value,
                        CanImport = false,
                        CanExport = false
                    }
                },
                {
                    Real_AreaId,
                    new PropertySturct
                    {
                        Index = Index_AreaId,
                        Name = "AreaId",
                        Title = "区域标识",
                        ColumnName = "area_id",
                        PropertyType = typeof(int),
                        CanNull = false,
                        ValueType = PropertyValueType.Value,
                        CanImport = false,
                        CanExport = false
                    }
                },
                {
                    Real_DepartmentId,
                    new PropertySturct
                    {
                        Index = Index_DepartmentId,
                        Name = "DepartmentId",
                        Title = "部门所有者",
                        ColumnName = "department_id",
                        PropertyType = typeof(int),
                        CanNull = false,
                        ValueType = PropertyValueType.Value,
                        CanImport = false,
                        CanExport = false
                    }
                },
                {
                    Real_WeiXinMediaId,
                    new PropertySturct
                    {
                        Index = Index_WeiXinMediaId,
                        Name = "WeiXinMediaId",
                        Title = "微信的MediaId",
                        ColumnName = "wx_media_id",
                        PropertyType = typeof(string),
                        CanNull = false,
                        ValueType = PropertyValueType.String,
                        CanImport = true,
                        CanExport = true
                    }
                },
                {
                    Real_DepartmentLevel,
                    new PropertySturct
                    {
                        Index = Index_DepartmentLevel,
                        Name = "DepartmentLevel",
                        Title = "部门级别",
                        ColumnName = "department_level",
                        PropertyType = typeof(int),
                        CanNull = false,
                        ValueType = PropertyValueType.Value,
                        CanImport = false,
                        CanExport = false
                    }
                }
            }
        };

        #endregion
    }
}