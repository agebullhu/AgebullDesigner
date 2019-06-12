using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.EasyUi
{
    /// <summary>
    /// EasyUi���࣬��ʵ���Զ�ע��
    /// </summary>
    public abstract class EasyUiCoderBase : CoderBase, IAutoRegister
    {
        /// <summary>
        /// ��ǰ����
        /// </summary>
        public ConfigBase CurrentConfig => Entity;

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public ProjectConfig Project => Entity?.Parent;

        /// <summary>
        /// ��ǰ�����
        /// </summary>
        public EntityConfig Entity { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        protected string NameSpace => Project?.NameSpace;

        /// <summary>
        /// ����
        /// </summary>
        protected abstract string FileName { get; }

        /// <summary>
        /// ����
        /// </summary>
        protected virtual string ExFileName => null;

        /// <summary>
        /// ��������
        /// </summary>
        protected virtual string LangName => "js";

        /// <summary>
        /// ִ���Զ�ע��
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            MomentCoder.RegisteCoder("Web-EasyUi", FileName, LangName, BaseCode);
            if (!string.IsNullOrWhiteSpace(ExFileName))
                MomentCoder.RegisteCoder("Web-EasyUi", ExFileName, LangName, ExtendCode);
        }

        public string BaseCode(EntityConfig entity)
        {
            Entity = entity;
            return BaseCode();
        }
        public string ExtendCode(EntityConfig config)
        {
            Entity = config as EntityConfig;
            if (Entity == null)
                return null;
            return ExtendCode();
        }

        protected virtual string BaseCode()
        {
            return null;
        }

        protected virtual string ExtendCode()
        {
            return null;
        }
    }
}