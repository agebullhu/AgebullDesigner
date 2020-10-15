using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.EasyUi
{
    /// <summary>
    /// EasyUi基类，以实现自动注册
    /// </summary>
    public abstract class EasyUiCoderBase : CoderBase, IAutoRegister
    {
        /// <summary>
        /// 当前对象
        /// </summary>
        public ConfigBase CurrentConfig => Entity;

        /// <summary>
        /// 项目对象
        /// </summary>
        public ProjectConfig Project => Entity?.Parent;

        /// <summary>
        /// 当前表对象
        /// </summary>
        public EntityConfig Entity { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        protected string NameSpace => Project?.NameSpace;

        /// <summary>
        /// 名称
        /// </summary>
        protected abstract string FileName { get; }

        /// <summary>
        /// 名称
        /// </summary>
        protected virtual string ExFileName => null;

        /// <summary>
        /// 语言类型
        /// </summary>
        protected virtual string LangName => "js";

        /// <summary>
        /// 执行自动注册
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