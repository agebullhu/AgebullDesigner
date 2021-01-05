using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    ///     配置基础
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public abstract partial class ProjectChildConfigBase : ParentConfigBase
    {
        /// <summary>
        /// 项目的说明文字
        /// </summary>
        const string Project_Description = @"存在于哪个项目,用于键盘录入来改变项目归属";

        /// <summary>
        /// 项目
        /// </summary>
        [DataMember, JsonProperty("_project", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _project;

        /// <summary>
        /// 项目
        /// </summary>
        /// <remark>
        /// 存在于哪个项目,用于键盘录入来改变项目归属
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"项目"), Description(Project_Description)]
        public string Project
        {
            get => _project;
            set
            {
                if (_project == value)
                    return;
                BeforePropertyChanged(nameof(Project), _project, value);
                _project = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(Project));
            }
        }
        /// <summary>
        /// 上级项目
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        internal ProjectConfig _parent;

        /// <summary>
        /// 上级项目
        /// </summary>
        /// <remark>
        /// 上级项目
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"项目管理"), DisplayName(@"上级项目"), Description("上级项目")]
        public ProjectConfig Parent
        {
            get => _parent;
            set
            {
                if (_parent == value)
                    return;
                BeforePropertyChanged(nameof(Parent), _parent, value);
                _parent = value;
                _project = value?.Name;
                OnPropertyChanged(nameof(Parent));
            }
        }
        #region 视角开关

        /// <summary>
        /// 上级项目
        /// </summary>
        [DataMember, JsonProperty("desingSwitch")]
        internal int _desingSwitch;

        void SetDesingSwitch(int value, bool enable) => _desingSwitch = enable ? _desingSwitch | value : _desingSwitch & ~value;

        /// <summary>
        /// 启用数据校验
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"视角开关"), DisplayName(@"启用数据校验"), Description("启用数据校验")]
        public bool EnableValidate
        {
            get => (_desingSwitch & 0x1) == 0x1;
            set
            {
                BeforePropertyChanged(nameof(EnableValidate), _parent, value);
                SetDesingSwitch(0x1, value);
                OnPropertyChanged(nameof(EnableValidate));
            }
        }

        /// <summary>
        /// 启用数据事件
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"视角开关"), DisplayName(@"启用数据事件"), Description("启用数据事件")]
        public bool EnableDataEvent
        {
            get => (_desingSwitch & 0x2) == 0x2;
            set
            {
                BeforePropertyChanged(nameof(EnableDataEvent), _parent, value);
                SetDesingSwitch(0x2, value);
                OnPropertyChanged(nameof(EnableDataEvent));
            }
        }

        /// <summary>
        /// 启用数据库
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"视角开关"), DisplayName(@"启用数据库"), Description("启用数据库")]
        public bool EnableDataBase
        {
            get => (_desingSwitch & 0x4) == 0x4;
            set
            {
                BeforePropertyChanged(nameof(EnableDataBase), _parent, value);
                SetDesingSwitch(0x4, value);
                OnPropertyChanged(nameof(EnableDataBase));
            }
        }
        /// <summary>
        /// 启用编辑API
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"视角开关"), DisplayName(@"启用编辑接口"), Description("启用编辑接口")]
        public bool EnableEditApi
        {
            get => (_desingSwitch & 0x8) == 0x8;
            set
            {
                BeforePropertyChanged(nameof(EnableEditApi), _parent, value);
                SetDesingSwitch(0x8, value);
                OnPropertyChanged(nameof(EnableEditApi));
            }
        }

        /// <summary>
        /// 启用用户界面
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"视角开关"), DisplayName(@"启用用户界面"), Description("启用用户界面")]
        public bool EnableUI
        {
            get => (_desingSwitch & 0x10) == 0x10;
            set
            {
                BeforePropertyChanged(nameof(EnableUI), _parent, value);
                SetDesingSwitch(0x10, value);
                OnPropertyChanged(nameof(EnableUI));
            }
        }

        #endregion
    }
}