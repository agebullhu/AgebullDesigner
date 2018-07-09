using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    ///     ���û���
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public abstract class ProjectChildConfigBase : ParentConfigBase
    {
        /// <summary>
        /// ��Ŀ��˵������
        /// </summary>
        const string Project_Description = @"�������ĸ���Ŀ,���ڼ���¼�����ı���Ŀ����";

        /// <summary>
        /// ��Ŀ
        /// </summary>
        [DataMember, JsonProperty("_project", NullValueHandling = NullValueHandling.Ignore)]
        internal string _project;

        /// <summary>
        /// ��Ŀ
        /// </summary>
        /// <remark>
        /// �������ĸ���Ŀ,���ڼ���¼�����ı���Ŀ����
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"�����֧��"), DisplayName(@"��Ŀ"), Description(Project_Description)]
        public string Project
        {
            get => _project;
            set
            {
                if (_project == value)
                    return;
                BeforePropertyChanged(nameof(Project), _project, value);
                _project = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(Project));
            }
        }
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
        public ProjectConfig Parent
        {
            get => _parent;
            set
            {
                if (_parent == value)
                    return;
                BeforePropertyChanged(nameof(Parent), _parent, value);
                _parent = value;
                _project = value?.Name;
                OnPropertyChanged(nameof(Parent));
            }
        }
    }
}