using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.EasyUi
{
    /// <summary>
    /// EasyUi基类，以实现自动注册
    /// </summary>
    public abstract class EasyUiCoderBase : CoderWithEntity, IAutoRegister
    {
        /// <summary>
        /// 执行自动注册
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            MomentCoder.RegisteCoder("EasyUi",FileSaveConfigName, "js", BaseCode);
            MomentCoder.RegisteCoder("EasyUi", FileSaveConfigName, "js", ExtendCode);
        }

        public static string BaseCode(ConfigBase config)
        {
            var entity = config as EntityConfig;
            if (entity == null)
                return null;
            var coder = new ApiActionCoder
            {
                Entity = entity,
                Project = entity.Parent
            };
            return coder.BaseCode();
        }
        public static string ExtendCode(ConfigBase config)
        {
            var entity = config as EntityConfig;
            if (entity == null)
                return null;
            var coder = new ApiActionCoder
            {
                Entity = entity,
                Project = entity.Parent
            };
            return coder.Code();
        }

        public virtual string BaseCode()
        {
            return null;
        }

        public virtual string Code()
        {
            return null;
        }
    }
}