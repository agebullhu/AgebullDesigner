using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.VUE
{
    /// <summary>
    /// Vue基类，以实现自动注册
    /// </summary>
    public abstract class VueCoderBase : CoderWithEntity, IAutoRegister
    {
        /// <summary>
        /// 执行自动注册
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            MomentCoder.RegisteCoder("Vue", FileSaveConfigName, BaseCode);
            MomentCoder.RegisteCoder("Vue", FileSaveConfigName, ExtendCode);
        }

        public static string BaseCode(ConfigBase config)
        {
            var entity = config as EntityConfig;
            if (entity == null)
                return null;
            var coder = new VueCoder
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
            var coder = new VueCoder
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