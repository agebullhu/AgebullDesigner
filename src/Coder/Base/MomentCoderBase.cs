using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

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
        public static string CreateCode<TConfig>(ConfigBase config, Func<TConfig, string> coder)
            where TConfig : ConfigBase
        {
            StringBuilder code = new StringBuilder();

            using (CodeGeneratorScope.CreateScope(config))
            {
                config.Foreach<TConfig>(arg =>
                {
                    code.AppendLine(coder(arg));
                });
            }

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
            StringBuilder code = new StringBuilder();

            using (CodeGeneratorScope.CreateScope(config))
            {
                config.Foreach(condition, arg =>
                {
                    code.AppendLine(coder(arg));
                });
            }

            return code.ToString();
        }
        #endregion

    }
}