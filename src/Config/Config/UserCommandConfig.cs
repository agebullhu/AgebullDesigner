/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2017/7/12 22:06:40*/
/*****************************************************
©2008-2017 Copy right by agebull.hu(胡天水)
作者:agebull.hu(胡天水)
工程:Agebull.Common.Config
建立:2014-12-03
修改:2017-07-12
*****************************************************/

using System.ComponentModel;
using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 用户命令配置
    /// </summary>
    [DataContract,JsonObject(MemberSerialization.OptIn)]
    public partial class UserCommandConfig : EntityChildConfig
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public UserCommandConfig()
        {
        }

        #endregion

 
        #region 数据模型

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
        [Category(@"数据模型"),DisplayName(@"按钮名称"),Description("按钮名称")]
        public string Button
        {
            get => _button;
            set
            {
                if(_button == value)
                    return;
                BeforePropertyChanged(nameof(Button), _button,value);
                _button = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(Button));
            }
        }

        /// <summary>
        /// 按钮图标
        /// </summary>
        [DataMember,JsonProperty("Icon", NullValueHandling = NullValueHandling.Ignore)]
        internal string _icon;

        /// <summary>
        /// 按钮图标
        /// </summary>
        /// <remark>
        /// 按钮图标
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"数据模型"),DisplayName(@"按钮图标"),Description("按钮图标")]
        public string Icon
        {
            get => _icon;
            set
            {
                if(_icon == value)
                    return;
                BeforePropertyChanged(nameof(Icon), _icon,value);
                _icon = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(Icon));
            }
        }

        /// <summary>
        /// 本地操作的说明文字
        /// </summary>
        const string IsLocalAction_Description = @"是否本地操作,即在客户端的操作,服务器无接口";

        /// <summary>
        /// 本地操作
        /// </summary>
        [DataMember,JsonProperty("IsLocalAction", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isLocalAction;

        /// <summary>
        /// 本地操作
        /// </summary>
        /// <remark>
        /// 是否本地操作,即在客户端的操作,服务器无接口
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"数据模型"),DisplayName(@"本地操作"),Description(IsLocalAction_Description)]
        public bool IsLocalAction
        {
            get => _isLocalAction;
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
        /// 单对象操作
        /// </summary>
        [DataMember,JsonProperty("IsSingleObject", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isSingleObject;

        /// <summary>
        /// 单对象操作
        /// </summary>
        /// <remark>
        /// 是否单对象操作,即操作对象只能是一行数据
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"数据模型"),DisplayName(@"单对象操作"),Description("是否单对象操作,即操作对象只能是一行数据")]
        public bool IsSingleObject
        {
            get => _isSingleObject;
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
        /// 在本地操作时打开的链接
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"数据模型"),DisplayName(@"打开链接"),Description("在本地操作时打开的链接")]
        public string Url
        {
            get => _url;
            set
            {
                if(_url == value)
                    return;
                BeforePropertyChanged(nameof(Url), _url,value);
                _url = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(Url));
            }
        } 
        #endregion

    }
}