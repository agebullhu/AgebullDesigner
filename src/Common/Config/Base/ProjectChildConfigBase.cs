using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    ///     配置基础
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public abstract partial class ProjectChildConfigBase : IndependenceConfigBase, IChildrenConfig
    {
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
        public ProjectConfig Project
        {
            get => _parent;
            set
            {
                if (_parent == value)
                    return;
                BeforePropertyChanged(nameof(Project), _parent, value);
                _parent = value;
                RaisePropertyChanged(nameof(Project));
                RaisePropertyChanged("Parent");
            }
        }

        ConfigBase IChildrenConfig.Parent { get => _parent; set => _parent = value as ProjectConfig; }
    }
}