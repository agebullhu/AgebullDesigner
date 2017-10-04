using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Gboxt.Common.DataAccess.Schemas
{
    /// <summary>
    /// 用户命令配置
    /// </summary>
    [DataContract,JsonObject(MemberSerialization.OptIn)]
    public partial class UserCommandConfig : ConfigBase
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public UserCommandConfig()
        {
        }

        #endregion

 
        #region *设计

        /// <summary>
        /// 按钮名称
        /// </summary>
        [DataMember,JsonProperty("_button", NullValueHandling = NullValueHandling.Ignore)]
        internal string _button;

        /// <summary>
        /// 按钮名称
        /// </summary>
        /// <remark>
        /// 按钮名称
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category("*设计"),DisplayName("按钮名称"),Description("按钮名称")]
        public string Button
        {
            get
            {
                return _button;
            }
            set
            {
                if(_button == value)
                    return;
                BeforePropertyChanged(nameof(Button), _button,value);
                _button = value;
                OnPropertyChanged(nameof(Button));
            }
        }

        /// <summary>
        /// 图标
        /// </summary>
        [DataMember,JsonProperty("Icon", NullValueHandling = NullValueHandling.Ignore)]
        internal string _icon;

        /// <summary>
        /// 图标
        /// </summary>
        /// <remark>
        /// 图标
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category("*设计"),DisplayName("图标"),Description("图标")]
        public string Icon
        {
            get
            {
                return _icon;
            }
            set
            {
                if(_icon == value)
                    return;
                BeforePropertyChanged(nameof(Icon), _icon,value);
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        /// <summary>
        /// 是否本地操作
        /// </summary>
        [DataMember,JsonProperty("IsLocalAction", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isLocalAction;

        /// <summary>
        /// 是否本地操作
        /// </summary>
        /// <remark>
        /// 是否本地操作
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category("*设计"),DisplayName("是否本地操作"),Description("是否本地操作")]
        public bool IsLocalAction
        {
            get
            {
                return _isLocalAction;
            }
            set
            {
                if(_isLocalAction == value)
                    return;
                BeforePropertyChanged(nameof(IsLocalAction), _isLocalAction,value);
                _isLocalAction = value;
                OnPropertyChanged(nameof(IsLocalAction));
            }
        }

        /// <summary>
        /// 是否单对象操作
        /// </summary>
        [DataMember,JsonProperty("IsSingleObject", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isSingleObject;

        /// <summary>
        /// 是否单对象操作
        /// </summary>
        /// <remark>
        /// 是否单对象操作
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category("*设计"),DisplayName("是否单对象操作"),Description("是否单对象操作")]
        public bool IsSingleObject
        {
            get
            {
                return _isSingleObject;
            }
            set
            {
                if(_isSingleObject == value)
                    return;
                BeforePropertyChanged(nameof(IsSingleObject), _isSingleObject,value);
                _isSingleObject = value;
                OnPropertyChanged(nameof(IsSingleObject));
            }
        }

        /// <summary>
        /// 打开链接
        /// </summary>
        [DataMember,JsonProperty("Url", NullValueHandling = NullValueHandling.Ignore)]
        internal string _url;

        /// <summary>
        /// 打开链接
        /// </summary>
        /// <remark>
        /// 打开链接
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category("*设计"),DisplayName("打开链接"),Description("打开链接")]
        public string Url
        {
            get
            {
                return _url;
            }
            set
            {
                if(_url == value)
                    return;
                BeforePropertyChanged(nameof(Url), _url,value);
                _url = value;
                OnPropertyChanged(nameof(Url));
            }
        } 
        #endregion *设计

    }
}