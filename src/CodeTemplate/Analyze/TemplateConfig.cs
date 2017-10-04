using System.Runtime.Serialization;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder.CodeTemplate
{
    /// <summary>
    /// 模板配置
    /// </summary>
    [DataContract]
    public class TemplateClassify : SimpleConfig
    {
        public ConfigCollection<TemplateConfig> Templates { get; } = new ConfigCollection<TemplateConfig>();
    }

    /// <summary>
    /// 模板配置
    /// </summary>
    [DataContract]
    public class TemplateConfig : NotificationObject
    {
        [IgnoreDataMember]
        private string _name;

        /// <summary>
        ///     名称
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
        ///     标题
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
        ///     说明
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
        /// 分类
        /// </summary>
        [DataMember]
        public string Classify { get; set; }
        /// <summary>
        /// 数据模型的类型
        /// </summary>
        [DataMember]
        public string ModelType { get; set; }
        /// <summary>
        /// 代码执行的条件(LUA脚本)
        /// </summary>
        [DataMember]
        public string Condition { get; set; }
        /// <summary>
        /// 生成代码的目标语言
        /// </summary>
        [DataMember]
        public string Language { get; set; }
        /// <summary>
        /// 生成代码后保存的路径
        /// </summary>
        [DataMember]
        public string CodeSavePath { get; set; }

        /// <summary>
        /// 模板保存的路径
        /// </summary>
        public string TemplatePath { get; set; }

        /// <summary>
        /// 模板内容
        /// </summary>
        [IgnoreDataMember]
        public string Template { get; set; }

        /// <summary>
        /// 模板翻译后的代码
        /// </summary>
        [IgnoreDataMember]
        public string Code { get; set; }
    }
}