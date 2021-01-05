using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    ///     ���û���
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public abstract partial class ProjectChildConfigBase : ParentConfigBase
    {
        /// <summary>
        /// ��Ŀ��˵������
        /// </summary>
        const string Project_Description = @"�������ĸ���Ŀ,���ڼ���¼�����ı���Ŀ����";

        /// <summary>
        /// ��Ŀ
        /// </summary>
        [DataMember, JsonProperty("_project", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
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
        #region �ӽǿ���

        /// <summary>
        /// �ϼ���Ŀ
        /// </summary>
        [DataMember, JsonProperty("desingSwitch")]
        internal int _desingSwitch;

        void SetDesingSwitch(int value, bool enable) => _desingSwitch = enable ? _desingSwitch | value : _desingSwitch & ~value;

        /// <summary>
        /// ��������У��
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"�ӽǿ���"), DisplayName(@"��������У��"), Description("��������У��")]
        public bool EnableValidate
        {
            get => (_desingSwitch & 0x1) == 0x1;
            set
            {
                BeforePropertyChanged(nameof(EnableValidate), _parent, value);
                SetDesingSwitch(0x1, value);
                OnPropertyChanged(nameof(EnableValidate));
            }
        }

        /// <summary>
        /// ���������¼�
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"�ӽǿ���"), DisplayName(@"���������¼�"), Description("���������¼�")]
        public bool EnableDataEvent
        {
            get => (_desingSwitch & 0x2) == 0x2;
            set
            {
                BeforePropertyChanged(nameof(EnableDataEvent), _parent, value);
                SetDesingSwitch(0x2, value);
                OnPropertyChanged(nameof(EnableDataEvent));
            }
        }

        /// <summary>
        /// �������ݿ�
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"�ӽǿ���"), DisplayName(@"�������ݿ�"), Description("�������ݿ�")]
        public bool EnableDataBase
        {
            get => (_desingSwitch & 0x4) == 0x4;
            set
            {
                BeforePropertyChanged(nameof(EnableDataBase), _parent, value);
                SetDesingSwitch(0x4, value);
                OnPropertyChanged(nameof(EnableDataBase));
            }
        }
        /// <summary>
        /// ���ñ༭API
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"�ӽǿ���"), DisplayName(@"���ñ༭�ӿ�"), Description("���ñ༭�ӿ�")]
        public bool EnableEditApi
        {
            get => (_desingSwitch & 0x8) == 0x8;
            set
            {
                BeforePropertyChanged(nameof(EnableEditApi), _parent, value);
                SetDesingSwitch(0x8, value);
                OnPropertyChanged(nameof(EnableEditApi));
            }
        }

        /// <summary>
        /// �����û�����
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"�ӽǿ���"), DisplayName(@"�����û�����"), Description("�����û�����")]
        public bool EnableUI
        {
            get => (_desingSwitch & 0x10) == 0x10;
            set
            {
                BeforePropertyChanged(nameof(EnableUI), _parent, value);
                SetDesingSwitch(0x10, value);
                OnPropertyChanged(nameof(EnableUI));
            }
        }

        #endregion
    }
}