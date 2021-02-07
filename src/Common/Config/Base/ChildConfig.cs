using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config
{

    /// <summary>
    /// 属性配置
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class ChildConfig<T> : ConfigBase, IChildrenConfig
        where T : ConfigBase
    {

        ConfigBase IChildrenConfig.Parent { get => _parent; set => Parent = value as T; }

        /// <summary>
        /// 上级
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        internal T _parent;

        /// <summary>
        /// 上级
        /// </summary>
        /// <remark>
        /// 上级
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"上级"), Description("上级")]
        public T Parent
        {
            get => _parent;
            set
            {
                if (_parent == value)
                    return;
                BeforePropertyChanged(nameof(Parent), _parent, value);
                _parent = value;
                OnPropertyChanged(nameof(Parent));
                OnPropertyChanged("IChildrenConfig.Parent");
            }
        }
    }

}