using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config.V2021
{
    /// <summary>
    /// 表示实体的扩展设置
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public class UserInterfaceConfig : ConfigBase
    {
        /// <summary>
        /// 对应实体
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        internal IEntityConfig _entityConfig;

        /// <summary>
        /// 对应实体
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"对应实体"), Description(@"对应实体")]
        public IEntityConfig Entity
        {
            get => _entityConfig;
            set
            {
                if (_entityConfig == value)
                    return;
                BeforePropertyChanged(nameof(Entity), _entityConfig, value);
                _entityConfig = value;
                OnPropertyChanged(nameof(Entity));
            }
        }

    }
}