using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;
using Agebull.Common;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    ///     百度翻译的命令
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class BaiduFanYiCommand : IAutoRegister
    {
        #region 命令注入

        /// <summary>
        /// 注册代码
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            CommandCoefficient.RegisterCommand<ConfigBase, ActionItem>(new ActionItem
            {
                Action = ToChiness2,
                Caption = "翻译成中文(名称到标题)",
                Catalog = "编辑",
                NoButton = true,
                ViewModel = "Entity",
                SourceType = typeof(ConfigBase),
                Image = Application.Current.Resources["imgBaidu"] as ImageSource
            });
            CommandCoefficient.RegisterCommand<ConfigBase, ActionItem>(new ActionItem
            {
                Action = ToChiness,
                Caption = "翻译成中文(标题到注释)",
                Catalog = "编辑",
                NoButton = true,
                ViewModel = "Entity",
                SourceType = typeof(ConfigBase),
                Image = Application.Current.Resources["imgBaidu"] as ImageSource
            });
        }
        public static void ToChiness(RuntimeActionItem item, object arg)
        {
            ToChiness(arg as ConfigBase);
        }

        public static void ToChiness2(RuntimeActionItem item, object arg)
        {
            ToChiness2(arg as ConfigBase);
        }
        #endregion


        #region 实现

        private static void ToChiness(ConfigBase config)
        {
            config.Caption = BaiduFanYi.FanYi(config.Caption);
            config.Description = BaiduFanYi.FanYi(config.Description);
            var parent = config as ParentConfigBase;
            if (parent == null)
                return;
            foreach (var child in parent.MyChilds)
            {
                ToChiness(child);
            }
        }


        private static void ToChiness2(ConfigBase config)
        {
            config.Caption = BaiduFanYi.FanYi(config.Name);
            var parent = config as ParentConfigBase;
            if (parent == null)
                return;
            foreach (var child in parent.MyChilds)
            {
                ToChiness2(child);
            }
        }
        #endregion
    }
}