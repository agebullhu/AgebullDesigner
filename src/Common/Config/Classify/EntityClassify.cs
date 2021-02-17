using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// ��������
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class EntityClassify : IndependenceConfigBase, IChildrenConfig
    {
        ConfigBase IChildrenConfig.Parent { get => Project; set => Project = value as ProjectConfig; }

        private ProjectConfig project;

        /// <summary>
        /// �ϼ�
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
        /// �Ӽ�
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        private ConfigCollection<EntityConfigBase> _items;

        /// <summary>
        /// �Ӽ�
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Browsable(false)]
        public ConfigCollection<EntityConfigBase> Items
        {
            get
            {
                if (_items != null)
                    return _items;
                _items = new ConfigCollection<EntityConfigBase>(this);
                RaisePropertyChanged(nameof(Items));
                return _items;
            }
            set
            {
                if (_items == value)
                {
                    return;
                }
                _items = value;
                if (value != null)
                    value.Parent = this;
                RaisePropertyChanged(() => Items);
            }
        }

    }
}