using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 属性配置
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class ModelChildConfig : ConfigBase, IChildrenConfig
    {
        /// <summary>
        /// 上级
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        internal IEntityConfig _parent;

        /// <summary>
        /// 上级
        /// </summary>
        /// <remark>
        /// 上级
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"上级"), Description("上级")]
        public IEntityConfig Parent
        {
            get => _parent;
            set
            {
                if (_parent == value)
                    return;
                BeforePropertyChanged(nameof(Parent), _parent, value);
                _parent = value;
                RaisePropertyChanged(nameof(Parent));
            }
        }
        ConfigBase IChildrenConfig.Parent { get => _parent as ModelConfig; set => _parent = value as ModelConfig; }
    }
}