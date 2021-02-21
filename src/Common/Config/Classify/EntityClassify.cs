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
        ISimpleConfig IChildrenConfig.Parent { get => Project; set => Project = value as ProjectConfig; }

        private ProjectConfig project;

        /// <summary>
        /// �ϼ�
        /// </summary>
        [JsonIgnore]
        public ProjectConfig Project
        {
            get => project;
            set
            {
                project = value;
                RaisePropertyChanged(nameof(Project));
                RaisePropertyChanged("IChildrenConfig.Parent");
            }
        }

        /// <summary>
        /// �Ӽ�
        /// </summary>
        [JsonIgnore]
        private ConfigCollection<EntityConfigBase> _items;

        /// <summary>
        /// �Ӽ�
        /// </summary>
        [JsonIgnore, Browsable(false)]
        public ConfigCollection<EntityConfigBase> Items
        {
            get
            {
                if (_items != null)
                    return _items;
                _items = new ConfigCollection<EntityConfigBase>(this, nameof(Items));
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
                {
                    value.Name = nameof(Items);
                    value.Parent = this;
                }
                RaisePropertyChanged(() => Items);
            }
        }

    }
}