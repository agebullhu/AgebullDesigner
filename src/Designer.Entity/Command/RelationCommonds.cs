using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 数据关联命令
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal class RelationCommonds : DesignCommondBase<EntityConfig>
    {

        #region 操作命令

        /// <summary>
        /// 生成命令对象
        /// </summary>
        /// <returns></returns>
        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Catalog = "数据关联",
                SignleSoruce = false,
                IsButton = false,
                Caption = "所有非自增主键设置为雪花码",
                Action = AutoSnowFlakeId,
                SoruceView = "argument",
                IconName = "tree_Open"
            });

            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                TargetType = typeof(EntityConfig),
                Name = "自动关联对象",
                Caption = "自动关联对象",
                Action = RelationChecker.DoCheck,
                Catalog = "数据关联"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                TargetType = typeof(EntityConfig),
                Name = "还原关联对象数据类型",
                Caption = "还原关联对象数据类型",
                Action = RelationChecker.CheckLinkType,
                Catalog = "数据关联"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                TargetType = typeof(EntityConfig),
                Name = "自动外联标题",
                Caption = "自动外联标题",
                Action = RelationChecker.DoLink,
                Catalog = "数据关联"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                TargetType = typeof(EntityConfig),
                Name = "清除所有外联",
                Caption = "清除所有外联",
                Action = RelationChecker.ClearLink,
                Catalog = "数据关联"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Name = "规范实体主键",
                Caption = "规范实体主键",
                Catalog = "数据关联",
                WorkView = "database",
                Action = CheckPrimary,
                TargetType = typeof(EntityConfig)
            });
        }

        #endregion

        /// <summary>
        /// 规范实体主键
        /// </summary>
        public void CheckPrimary(EntityConfig entity)
        {
            if (entity.PrimaryColumn == null)
                return;
            entity.PrimaryColumn.KeepStorageScreen = StorageScreenType.Update;
            entity.PrimaryColumn.CsType = "long";
            entity.PrimaryColumn.DataType = "Int64";
            entity.PrimaryColumn.DbType = "BIGINT";
            Trace.WriteLine($@"ALTER TABLE {entity.SaveTableName} ALTER COLUMN {entity.PrimaryColumn.DbFieldName } BIGINT NOT NULL;");
        }

        /// <summary>
        ///     所有非自增主键设置为雪花码
        /// </summary>
        /// <param name="entity"></param>
        public void AutoSnowFlakeId(EntityConfig entity)
        {
            if (entity.PrimaryColumn == null || entity.PrimaryColumn.IsIdentity)
                return;
            if (string.IsNullOrWhiteSpace(entity.Interfaces))
                entity.Interfaces = "ISnowFlakeId";
            else if (!entity.Interfaces.Contains("ISnowFlakeId"))
                entity.Interfaces += ",ISnowFlakeId";
        }


    }
}