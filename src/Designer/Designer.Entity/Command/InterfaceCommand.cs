using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    ///     实体实现接口的命令
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class InterfaceCommand : DesignCommondBase<EntityConfig>
    {
        /// <summary>
        /// 生成命令对象
        /// </summary>
        /// <returns></returns>
        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = InterfaceHelper.ClearInterface,
                Caption = "清理字段",
                SoruceView = "entity",
                SignleSoruce = false,
                Catalog = "接口",
                IconName = "img_link"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = InterfaceHelper.FindInterface,
                Caption = "自动识别",
                SoruceView = "entity",
                SignleSoruce = false,
                Catalog = "接口",
                IconName = "img_link"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = InterfaceHelper.ClearInterface,
                Caption = "自动识别",
                SoruceView = "entity",
                SignleSoruce = false,
                Catalog = "接口",
                IconName = "img_link"
            });

            foreach (var iEntity in GlobalConfig.Global.Entities.Where(p => p.IsInterface))
            {
                commands.Add(new CommandItemBuilder<EntityConfig>
                {
                    Action = entity => InterfaceHelper.AddInterface(entity, iEntity.Name),
                    Caption = $"实现{iEntity.Caption}({iEntity.Name})",
                    SoruceView = "entity",
                    SignleSoruce = false,
                    Catalog = "接口",
                    IconName = "img_link"
                });

                commands.Add(new CommandItemBuilder<EntityConfig>
                {
                    Action = entity => InterfaceHelper.RemoveInterface(entity, iEntity.Name),
                    Caption = $"清除{iEntity.Caption}({iEntity.Name})",
                    SoruceView = "entity",
                    SignleSoruce = false,
                    Catalog = "接口",
                    IconName = "img_link"
                });
            }

            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = ToMemo,
                Caption = "添加备注(Memo)字段",
                SoruceView = "entity",
                SignleSoruce = false,
                Catalog = "接口",
                IconName = "img_link"
            });
        }

        public void ToMemo(EntityConfig entity)
        {
            if (entity.Exist("memo"))
                return;
            entity.Add(new FieldConfig
            {
                Name = "Memo",
                Caption = "备注",
                DbFieldName = "memo",
                JsonName = "memo",
                DbNullable=true,
                DbType = "TEXT"
            });
        }

    }
}
