using Agebull.EntityModel.Config;
using System;
using System.Text;

namespace Agebull.EntityModel.RobotCoder
{
    /// <summary>
    /// 代码片断基类
    /// </summary>
    public class MomentCoderBase : CoderBase
    {
        #region 代码生成代理接口

        /// <summary>
        /// 执行目标的动作
        /// </summary>
        /// <param name="config"></param>
        /// <param name="coder"></param>
        /// <returns></returns>
        public static string CreateCode(ConfigBase config, Func<IEntityConfig, string> coder)
        {
            StringBuilder code = new();

            GlobalTrigger.Regularize(GlobalConfig.CurrentSolution);
            config.Preorder<IEntityConfig>(arg =>
            {
                try
                {
                    using var scope = CodeGeneratorScope.CreateScope(arg, true);
                    code.AppendLine(coder(arg));
                }
                catch (Exception)
                {

                    throw;
                }
            });
            return code.ToString();
        }

        /// <summary>
        /// 执行目标的动作
        /// </summary>
        /// <param name="config"></param>
        /// <param name="coder"></param>
        /// <returns></returns>
        public static string CreateCode<TConfig>(ConfigBase config, Func<TConfig, string> coder)
            where TConfig : ConfigBase
        {
            GlobalTrigger.Regularize(GlobalConfig.CurrentSolution);
            StringBuilder code = new();
            config.Preorder<TConfig>(arg =>
            {
                using var scope = CodeGeneratorScope.CreateScope(arg, true);
                code.AppendLine(coder(arg));
            });
            return code.ToString();
        }

        /// <summary>
        /// 执行目标的动作
        /// </summary>
        /// <param name="config"></param>
        /// <param name="condition"></param>
        /// <param name="coder"></param>
        /// <returns></returns>
        public static string CreateCode<TConfig>(ConfigBase config, Func<TConfig, bool> condition, Func<TConfig, string> coder)
            where TConfig : ConfigBase
        {
            GlobalTrigger.Regularize(GlobalConfig.CurrentSolution);
            StringBuilder code = new();
            config.Preorder<TConfig>(arg =>
            {
                using var scope = CodeGeneratorScope.CreateScope(arg, true);
                code.AppendLine(coder(arg));
            });
            return code.ToString();
        }
        #endregion

    }
}