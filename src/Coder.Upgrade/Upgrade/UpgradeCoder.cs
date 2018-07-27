using System.ComponentModel.Composition;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.Upgrade
{
    /// <summary>
    /// 自升级代码
    /// </summary>

    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class UpgradeCoder : MomentCoderBase, IAutoRegister
    {
        #region 注册

        /// <summary>
        /// 注册代码
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            MomentCoder.RegisteCoder("自升级代码","配置类升级", "cs", cfg => NewConfigDefault());
        }
        #endregion

        /// <summary>
        /// 升级配置类
        /// </summary>
        /// <returns></returns>
        public string NewConfigDefault()
        {
            var up = new AssemblyUpgrader();
            var types = up.GetConfig();

            foreach (var type in types.Values)
            {
                var builder = new UpgradeEntityBuilder
                {
                    Config = type
                };
                builder.CreateBaseCode(@"C:\Projects\AgebullDesigner\Defaults\Config");
            }


            return "在文件里看";
        }
    }

}