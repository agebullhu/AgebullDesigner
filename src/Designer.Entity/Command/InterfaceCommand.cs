using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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
                Action = InterfaceHelper.CheckInterface,
                Caption = "刷新接口引用",
                SignleSoruce = false,
                Catalog = "接口",
                IconName = "img_link"
            });/*
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = (ToIDataState),
                Caption = "实现状态数据(IStateData)接口",

                SignleSoruce = false,
                Catalog = "接口",
                IconName = "img_link"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = (ToIHistory),
                Caption = "实现历史数据(IHistoryData)接口",

                SignleSoruce = false,
                Catalog = "接口",
                IconName = "img_link"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = (ToIAudit),
                Caption = "实现审核(IAudit)接口",

                SignleSoruce = false,
                Catalog = "接口",
                IconName = "img_link"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = (ToMemo),
                Caption = "添加备注(Memo)字段",

                SignleSoruce = false,
                Catalog = "接口",
                IconName = "img_link"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = (ToSelfRelation),
                Caption = "添加上级关联(ParentId)字段",
                Catalog = "接口",

                SignleSoruce = false,
                IconName = "img_link"
            });
        }
        //public void ToSelfRelation(EntityConfig entity)
        //{
        //    ToInterface(entity, "ISelfRelation");
        //    var cmd = new EntityInterfaceSeter();
        //    cmd.ToSelfRelation(entity);
        //}

        public void ToMemo(EntityConfig entity)
        {
            ToInterface(entity, "IMemo");
        }

        public void ToIDataState(EntityConfig entity)
        {
            ToInterface(entity, "IDataState");
        }
        public void ToIHistory(EntityConfig entity)
        {
            ToInterface(entity, "IHistoryData");
        }

        public void ToIAudit(EntityConfig entity)
        {
            ToInterface(entity, "IAudit");
        */
        }

        public void ToInterface(EntityConfig entity, string it)
        {
            if (string.IsNullOrWhiteSpace(entity.Interfaces))
                entity.Interfaces = it;
            else if (!entity.Interfaces.Contains(it))
                entity.Interfaces += "," + it;
            InterfaceHelper.CheckInterface(entity);
        }
    }
}
