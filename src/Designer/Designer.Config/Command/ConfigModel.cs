﻿using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using System.Collections.Generic;
using System.ComponentModel.Composition;

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

                IconName = "清除"
            });
            commands.Add(new CommandItemBuilder<ConfigBase>
            {
                Action = ClearFileConfig2,
                Caption = "清除所有扩展信息",
                Catalog = "工具",
                SignleSoruce = false,

                IconName = "清除"
            });
            commands.Add(new CommandItemBuilder<ConfigBase>
            {
                Action = Lock,
                Caption = "锁定",
                Catalog = "设计",
                IconName = "锁定"
            });
            commands.Add(new CommandItemBuilder<ConfigBase>
            {
                Action = UnLock,
                Caption = "解锁",
                Catalog = "设计",
                IconName = "解锁"
            });
            commands.Add(new CommandItemBuilder<ConfigBase>
            {
                Action = ToModify,
                Caption = "强制已修改",
                Catalog = "设计",
                IconName = "修改"
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
            config?.Option.ExtendConfigList.ClearFileConfig();
        }

        public void ClearFileConfig2(ConfigBase config)
        {
            config?.Option.ExtendConfigList.Clear();
        }
    }
}