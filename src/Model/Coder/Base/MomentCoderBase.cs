using Agebull.EntityModel.Config;
using System;
using System.Text;

namespace Agebull.EntityModel.RobotCoder
{
    /// <summary>
    /// ����Ƭ�ϻ���
    /// </summary>
    public class MomentCoderBase : CoderBase
    {
        #region �������ɴ���ӿ�

        /// <summary>
        /// ִ��Ŀ��Ķ���
        /// </summary>
        /// <param name="config"></param>
        /// <param name="coder"></param>
        /// <returns></returns>
        public static string CreateCode(ConfigBase config, Func<IEntityConfig, string> coder)
        {
            StringBuilder code = new StringBuilder();

            GlobalTrigger.Regularize(GlobalConfig.CurrentSolution);
            config.Foreach<IEntityConfig>(arg =>
            {
                using var scope = CodeGeneratorScope.CreateScope(config,false);
                code.AppendLine(coder(arg));
            });
            return code.ToString();
        }

        /// <summary>
        /// ִ��Ŀ��Ķ���
        /// </summary>
        /// <param name="config"></param>
        /// <param name="coder"></param>
        /// <returns></returns>
        public static string CreateCode<TConfig>(ConfigBase config, Func<TConfig, string> coder)
            where TConfig : ConfigBase
        {
            GlobalTrigger.Regularize(GlobalConfig.CurrentSolution);
            StringBuilder code = new StringBuilder();
            config.Foreach<TConfig>(arg =>
            {
                using var scope = CodeGeneratorScope.CreateScope(config, false);
                code.AppendLine(coder(arg));
            });
            return code.ToString();
        }

        /// <summary>
        /// ִ��Ŀ��Ķ���
        /// </summary>
        /// <param name="config"></param>
        /// <param name="condition"></param>
        /// <param name="coder"></param>
        /// <returns></returns>
        public static string CreateCode<TConfig>(ConfigBase config, Func<TConfig, bool> condition, Func<TConfig, string> coder)
            where TConfig : ConfigBase
        {
            GlobalTrigger.Regularize(GlobalConfig.CurrentSolution);
            StringBuilder code = new StringBuilder();
            config.Foreach<TConfig>(arg =>
            {
                using var scope = CodeGeneratorScope.CreateScope(config, false);
                code.AppendLine(coder(arg));
            });
            return code.ToString();
        }
        #endregion

    }
}