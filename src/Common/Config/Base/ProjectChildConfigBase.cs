using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    ///     ���û���
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public abstract partial class ProjectChildConfigBase : IndependenceConfigBase, IChildrenConfig
    {
        /// <summary>
        /// �ϼ���Ŀ
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        internal ProjectConfig _parent;

        /// <summary>
        /// �ϼ���Ŀ
        /// </summary>
        /// <remark>
        /// �ϼ���Ŀ
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"��Ŀ����"), DisplayName(@"�ϼ���Ŀ"), Description("�ϼ���Ŀ")]
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