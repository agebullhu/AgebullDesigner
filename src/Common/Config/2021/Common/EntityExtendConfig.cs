using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config.V2021
{

    /// <summary>
    /// 表示实体的扩展设置
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class EntityExtendConfig : FileConfigBase, IChildrenConfig
    {
        ConfigBase IChildrenConfig.Parent { get => Entity as ConfigBase; set => Entity = value as IEntityConfig; }

        /// <summary>
        /// 对应实体
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        internal IEntityConfig _entity;

        /// <summary>
        /// 对应实体
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"对应实体"), Description(@"对应实体")]
        public IEntityConfig Entity
        {
            get => _entity;
            set
            {
                if (_entity == value)
                    return;
                BeforePropertyChanged(nameof(Entity), _entity, value);
                _entity = value;
                OnPropertyChanged(nameof(Entity));
                OnPropertyChanged("IChildrenConfig.Parent");
            }
        }


        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        protected override void CopyFrom(SimpleConfig dest)
        {
            base.CopyFrom(dest);
            if (dest is EntityExtendConfig cfg)
                CopyProperty(cfg);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        public void Copy(EntityExtendConfig dest)
        {
            CopyFrom(dest);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        /// <returns></returns>
        public void CopyProperty(EntityExtendConfig dest)
        {
            Entity = dest.Entity;
        }
    }
}