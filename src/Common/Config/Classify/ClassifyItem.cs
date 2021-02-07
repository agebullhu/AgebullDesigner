using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 分类配置
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class EntityClassify : IndependenceConfigBase, IChildrenConfig
    {
        ConfigBase IChildrenConfig.Parent { get => Project; set => Project = value as ProjectConfig; }
        /// <summary>
        /// 上级
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public ProjectConfig Project
        {
            get => project; set
            {
                project = value;
                OnPropertyChanged(nameof(Project));
                OnPropertyChanged("IChildrenConfig.Parent");
            }
        }

        /// <summary>
        /// 子级
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        private ConfigCollection<EntityConfig> _items = new ConfigCollection<EntityConfig>();
        private ProjectConfig project;

        /// <summary>
        /// 子级
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Browsable(false)]
        public ConfigCollection<EntityConfig> Items
        {
            get => _items;
            set
            {
                if (_items == value)
                {
                    return;
                }
                _items = value;
                RaisePropertyChanged(() => Items);
            }
        }

    }
}