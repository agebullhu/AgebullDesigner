using System.Runtime.Serialization;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder.CodeTemplate
{
    /// <summary>
    /// ģ������
    /// </summary>
    [DataContract]
    public class TemplateClassify : SimpleConfig
    {
        public ConfigCollection<TemplateConfig> Templates { get; } = new ConfigCollection<TemplateConfig>();
    }

    /// <summary>
    /// ģ������
    /// </summary>
    [DataContract]
    public class TemplateConfig : NotificationObject
    {
        [IgnoreDataMember]
        private string _name;

        /// <summary>
        ///     ����
        /// </summary>
        [DataMember]
        public string Name
        {
            get { return _name; }
            set
            {
                var now = !string.IsNullOrWhiteSpace(value) ? value.Trim() : null;
                if (_name == now)
                {
                    return;
                }
                _name = now;
                RaisePropertyChanged(nameof(Name));
            }
        }

        [IgnoreDataMember]
        private string _caption;

        /// <summary>
        ///     ����
        /// </summary>
        [DataMember]
        public string Caption
        {
            get { return _caption ?? _name; }
            set
            {
                if (_caption == value)
                {
                    return;
                }
                _caption = !string.IsNullOrWhiteSpace(value) ? value.Trim() : null;
                RaisePropertyChanged(nameof(Caption));
            }
        }

        [IgnoreDataMember]
        private string _description;

        /// <summary>
        ///     ˵��
        /// </summary>
        [DataMember]
        public string Description
        {
            get { return _description ?? _caption ?? _name; }
            set
            {
                if (_description == value)
                {
                    return;
                }
                _description = !string.IsNullOrWhiteSpace(value) ? value.Trim() : null; ;
                RaisePropertyChanged(nameof(Description));
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        [DataMember]
        public string Classify { get; set; }
        /// <summary>
        /// ����ģ�͵�����
        /// </summary>
        [DataMember]
        public string ModelType { get; set; }
        /// <summary>
        /// ����ִ�е�����(LUA�ű�)
        /// </summary>
        [DataMember]
        public string Condition { get; set; }
        /// <summary>
        /// ���ɴ����Ŀ������
        /// </summary>
        [DataMember]
        public string Language { get; set; }
        /// <summary>
        /// ���ɴ���󱣴��·��
        /// </summary>
        [DataMember]
        public string CodeSavePath { get; set; }

        /// <summary>
        /// ģ�屣���·��
        /// </summary>
        public string TemplatePath { get; set; }

        /// <summary>
        /// ģ������
        /// </summary>
        [IgnoreDataMember]
        public string Template { get; set; }

        /// <summary>
        /// ģ�巭���Ĵ���
        /// </summary>
        [IgnoreDataMember]
        public string Code { get; set; }
    }
}