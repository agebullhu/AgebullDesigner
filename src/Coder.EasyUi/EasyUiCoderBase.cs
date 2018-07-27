using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.EasyUi
{
    /// <summary>
    /// EasyUi���࣬��ʵ���Զ�ע��
    /// </summary>
    public abstract class EasyUiCoderBase : CoderWithEntity, IAutoRegister
    {
        /// <summary>
        /// ִ���Զ�ע��
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