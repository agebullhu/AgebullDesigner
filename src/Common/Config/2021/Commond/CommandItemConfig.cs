/*design by:agebull designer date:2017/7/12 22:06:40*/
/*****************************************************
©2008-2017 Copy right by agebull.hu(胡天水)
作者:agebull.hu(胡天水)
工程:Agebull.Common.Config
建立:2014-12-03
修改:2017-07-12
*****************************************************/

using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace Agebull.EntityModel.Config.V2021
{
    /// <summary>
    /// 用户命令配置
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class CommandItemConfig : ModelChildConfig, ICommandItem
    {
        #region 数据模型

        /// <summary>
        /// 按钮名称
        /// </summary>
        [DataMember, JsonProperty("_button", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _button;

        /// <summary>
        /// 按钮名称
        /// </summary>
        /// <remark>
        /// 按钮名称
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据模型"), DisplayName(@"按钮名称"), Description("按钮名称")]
        public string Button
        {
            get => _button;
            set
            {
                if (_button == value)
                    return;
                BeforePropertyChanged(nameof(Button), _button, value);
                _button = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(Button));
            }
        }

        /// <summary>
        /// 按钮图标
        /// </summary>
        [DataMember, JsonProperty("Icon", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _icon;

        /// <summary>
        /// 按钮图标
        /// </summary>
        /// <remark>
        /// 按钮图标
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据模型"), DisplayName(@"按钮图标"), Description("按钮图标")]
        public string Icon
        {
            get => _icon;
            set
            {
                if (_icon == value)
                    return;
                BeforePropertyChanged(nameof(Icon), _icon, value);
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
        [DataMember, JsonProperty("IsLocalAction", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isLocalAction;

        /// <summary>
        /// 本地操作
        /// </summary>
        /// <remark>
        /// 是否本地操作,即在客户端的操作,服务器无接口
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据模型"), DisplayName(@"本地操作"), Description(IsLocalAction_Description)]
        public bool IsLocalAction
        {
            get => _isLocalAction;
            set
            {
                if (_isLocalAction == value)
                    return;
                BeforePropertyChanged(nameof(IsLocalAction), _isLocalAction, value);
                _isLocalAction = value;
                OnPropertyChanged(nameof(IsLocalAction));
            }
        }

        /// <summary>
        /// 多选操作
        /// </summary>
        [DataMember, JsonProperty("isMulitOperator", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool isMulitOperator;

        /// <summary>
        /// 多选操作
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据模型"), DisplayName(@"多选")]
        public bool IsMulitOperator
        {
            get => isMulitOperator;
            set
            {
                if (isMulitOperator == value)
                    return;
                BeforePropertyChanged(nameof(IsMulitOperator), isMulitOperator, value);
                isMulitOperator = value;
                OnPropertyChanged(nameof(IsMulitOperator));
            }
        }
        /// <summary>
        /// 单选
        /// </summary>
        [DataMember, JsonProperty("IsSingleObject", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isSingleObject;

        /// <summary>
        /// 单选
        /// </summary>
        /// <remark>
        /// 是否单对象操作,即操作对象只能是一行数据
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据模型"), DisplayName(@"单选"), Description("是否单对象操作,即操作对象只能是一行数据")]
        public bool IsSingleObject
        {
            get => _isSingleObject;
            set
            {
                if (_isSingleObject == value)
                    return;
                BeforePropertyChanged(nameof(IsSingleObject), _isSingleObject, value);
                _isSingleObject = value;
                OnPropertyChanged(nameof(IsSingleObject));
            }
        }

        /// <summary>
        /// Js方法
        /// </summary>
        [DataMember, JsonProperty("jsMethod", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string jsMethod;

        /// <summary>
        /// Js方法
        /// </summary>
        /// <remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据模型"), DisplayName(@"Js方法"), Description("Js方法")]
        public string JsMethod
        {
            get => jsMethod ?? Name.ToLWord();
            set
            {
                if (jsMethod == value)
                    return;
                BeforePropertyChanged(nameof(JsMethod), jsMethod, value);
                jsMethod = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(JsMethod));
            }
        }


        /// <summary>
        /// 接口名称
        /// </summary>
        [DataMember, JsonProperty("api", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string api;

        /// <summary>
        /// 接口名称
        /// </summary>
        /// <remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据模型"), DisplayName(@"接口名称"), Description("接口名称")]
        public string Api
        {
            get => api ?? $"edit/{Name.ToLWord()}";
            set
            {
                if (api == value)
                    return;
                BeforePropertyChanged(nameof(Api), api, value);
                api = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(Api));
            }
        }

        /// <summary>
        /// 服务器调用代码
        /// </summary>
        [DataMember, JsonProperty("serviceCommand", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string serviceCommand;

        /// <summary>
        /// 服务器调用代码
        /// </summary>
        /// <remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据模型"), DisplayName(@"服务器调用代码"), Description("服务器调用代码")]
        public string ServiceCommand
        {
            get => serviceCommand;
            set
            {
                if (serviceCommand == value)
                    return;
                BeforePropertyChanged(nameof(ServiceCommand), serviceCommand, value);
                serviceCommand = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(ServiceCommand));
            }
        }

        #endregion

    }
}