using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    ///     配置基础
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public abstract class ProjectChildConfigBase : ParentConfigBase
    {
        /// <summary>
        /// 项目的说明文字
        /// </summary>
        const string Project_Description = @"存在于哪个项目,用于键盘录入来改变项目归属";

        /// <summary>
        /// 项目
        /// </summary>
        [DataMember, JsonProperty("_project", NullValueHandling = NullValueHandling.Ignore)]
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
    }
}