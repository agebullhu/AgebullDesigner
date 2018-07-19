using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

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
            commands.Add(new CommandItemBuilder<ConfigBase>
            {
                Action = ClearFileConfig,
                Caption = "清除文件相关的扩展信息",
                Catalog = "工具",
                SignleSoruce = false,

                IconName = "tree_item"
            });
            commands.Add(new CommandItemBuilder<ConfigBase>
            {
                Action = (ClearFileConfig2),
                Caption = "清除所有扩展信息",
                Catalog = "工具",
                SignleSoruce = false,

                IconName = "tree_item"
            });
            commands.Add(new CommandItemBuilder<ConfigBase>
            {
                Action = Lock,
                Caption = "锁定",
                Catalog = "设计",
                IconName = "img_lock"
            });
            commands.Add(new CommandItemBuilder<ConfigBase>
            {
                Action = UnLock,
                Caption = "解锁",
                Catalog = "设计",
                IconName = "img_no_modify"
            });
            commands.Add(new CommandItemBuilder<ConfigBase>
            {
                Action = ToModify,
                Caption = "强制已修改",
                Catalog = "设计",
                IconName = "img_modify"
            });
        }

        void UnLock(ConfigBase project)
        {
            project.Option.IsFreeze = false;
        }

        void Lock(ConfigBase project)
        {
            project.Option.IsFreeze = true;
        }

        public void ToModify(ConfigBase entity)
        {
            entity.IsModify = true;
        }

        /// <summary>
        ///     自动修复
        /// </summary>
        public void ClearFileConfig(ConfigBase config)
        {
            foreach (var kv in config.ExtendConfig.Where(p => p.Name != null && p.Name.IndexOf("File", StringComparison.OrdinalIgnoreCase) == 0).ToArray())
            {
                config.ExtendConfig.Remove(kv);
            }
            foreach (var kv in config.ExtendDictionary.Keys.Where(p => p != null && p.IndexOf("File", StringComparison.OrdinalIgnoreCase) == 0).ToArray())
            {
                config.ExtendDictionary.Remove(kv);
            }
        }

        public void ClearFileConfig2(ConfigBase config)
        {
            config.ExtendConfig.Clear();
            config.ExtendDictionary.Clear();
        }
    }
}