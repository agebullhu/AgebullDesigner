using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows;
using Agebull.Common;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 实体配置相关模型
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal class ConfigModel : DesignCommondBase<ConfigBase>
    {

        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            commands.Add(new CommandItemBuilder
            {
                Command = new DelegateCommand(ClearFileConfig),
                Name = "清除文件相关的扩展信息",
                Signle = false,
                NoButton = true,
                IconName = "tree_item"
            });
            commands.Add(new CommandItemBuilder
            {
                Command = new DelegateCommand(ClearFileConfig2),
                Name = "清除所有扩展信息",
                Signle = false,
                NoButton = true,
                IconName = "tree_item"
            });
        }

        /// <summary>
        ///     自动修复
        /// </summary>
        public void ClearFileConfig()
        {
            Foreach(ClearFileConfig);
        }


        public void ClearFileConfig(ConfigBase config)
        {
            foreach (var kv in config.ExtendConfig.Where(p => p.Name != null && p.Name.IndexOf("File", StringComparison.OrdinalIgnoreCase) == 0).ToArray())
            {
                config.ExtendConfig.Remove(kv);
            }
            foreach (var kv in config.ExtendDictionary.Keys.Where(p => p != null && p.IndexOf("File",StringComparison.OrdinalIgnoreCase) == 0).ToArray())
            {
                config.ExtendDictionary.Remove(kv);
            }
        }



        /// <summary>
        ///     自动修复
        /// </summary>
        public void ClearFileConfig2()
        {
            Foreach(ClearFileConfig2);
        }
        public void ClearFileConfig2(ConfigBase config)
        {
            config.ExtendConfig.Clear();
            config.ExtendDictionary.Clear();
        }
    }
}