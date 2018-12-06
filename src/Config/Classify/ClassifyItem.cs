using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// ��������
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public class EntityClassify : ParentConfigBase
    {
        /// <summary>
        /// �ϼ�
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public ProjectConfig Project { get; set; }

        /// <summary>
        /// �Ӽ�
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        private ConfigCollection<EntityConfig> _items = new ConfigCollection<EntityConfig>();

        /// <summary>
        /// �Ӽ�
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        public override void ForeachChild(Action<ConfigBase> action)
        {
            foreach (var item in _items)
                action(item);
        }
    }

}