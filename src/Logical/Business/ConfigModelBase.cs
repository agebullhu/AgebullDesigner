using System;
using System.Text;
using Agebull.Common;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 实体配置业务模型
    /// </summary>
    public class ConfigModelBase : GlobalConfig
    {
        /// <summary>
        /// 设置当前解决方案
        /// </summary>
        /// <param name="solution"></param>
        public static void SetCurrentSolution(SolutionConfig solution)
        {
            SolutionConfig.SetCurrentSolution(solution);
            GlobalTrigger.Reset();
            GlobalTrigger.OnLoad(solution);
        }
        /// <summary>
        /// 修复配置名称
        /// </summary>
        /// <param name="config"></param>
        /// <param name="noName"></param>

        public static void RepairConfigName(ConfigBase config, bool noName = false)
        {
            if (config == null)
                return;
            config.Caption = ReplceWord(string.Equals(config.Caption, config.Name, StringComparison.OrdinalIgnoreCase) ? config.Name : config.Caption, string.Equals(config.Caption, config.Name, StringComparison.OrdinalIgnoreCase));
            config.Description = ReplceWord(string.Equals(config.Description, config.Name, StringComparison.OrdinalIgnoreCase) ? config.Name : config.Description, string.Equals(config.Description, config.Name, StringComparison.OrdinalIgnoreCase));
            if (!noName)
                config.Name = config.Name?.Trim(NoneLanguageChar).MulitReplace2('_', NoneLanguageChar);
        }


        private static string ReplceWord(string caption, bool isEn)
        {
            if (caption == null)
                return null;
            caption = caption.Trim(NoneLanguageChar);
            if (isEn)
                caption = BaiduFanYi.FanYi(caption);
            return caption;
        }
    }
}