using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 分类配置
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public class EntityClassify : ParentConfigBase
    {
        /// <summary>
        /// 上级
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public ProjectConfig Project { get; set; }

        /// <summary>
        /// 子级
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        private ConfigCollection<EntityConfig> _items = new ConfigCollection<EntityConfig>();

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