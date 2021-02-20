using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;

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
            commands.Add(new CommandItemBuilder<IEntityConfig>
            {
                Catalog = "数据关联",
                SignleSoruce = false,
                IsButton = false,
                SoruceView = "entity",
                Caption = "所有非自增主键设置为雪花码",
                Action = AutoSnowFlakeId,
                IconName = "字段"
            });

            commands.Add(new CommandItemBuilder<IEntityConfig>
            {
                TargetType = typeof(IEntityConfig),
                Name = "自动关联对象",
                Caption = "自动关联对象",
                SoruceView = "entity",
                Action = RelationChecker.DoCheck,
                Catalog = "数据关联",
                IconName = "关联"
            });
            commands.Add(new CommandItemBuilder<IEntityConfig>
            {
                TargetType = typeof(IEntityConfig),
                Name = "还原关联对象数据类型",
                SoruceView = "entity",
                Caption = "还原关联对象数据类型",
                Action = RelationChecker.CheckLinkType,
                Catalog = "数据关联",
                IconName = "类型"
            });
            commands.Add(new CommandItemBuilder<IEntityConfig>
            {
                TargetType = typeof(IEntityConfig),
                Name = "自动外联标题",
                Caption = "自动外联标题",
                SoruceView = "entity",
                Action = RelationChecker.DoLinkCaption,
                Catalog = "数据关联",
                IconName = "关联"
            });
            commands.Add(new CommandItemBuilder<IEntityConfig>
            {
                TargetType = typeof(IEntityConfig),
                Name = "清除所有外联",
                Caption = "清除所有外联",
                SoruceView = "entity",
                Action = RelationChecker.ClearLink,
                Catalog = "数据关联",
                IconName = "清除"
            });
            commands.Add(new CommandItemBuilder<IEntityConfig>
            {
                Name = "规范实体主键",
                Caption = "规范实体主键",
                Catalog = "数据关联",
                SoruceView = "entity",
                WorkView = "database",
                IconName = "规范",
                Action = CheckPrimary,
                TargetType = typeof(IEntityConfig)
            });
        }

        #endregion

        /// <summary>
        /// 规范实体主键
        /// </summary>
        public void CheckPrimary(IEntityConfig entity)
        {
            if (entity.PrimaryColumn == null)
                return;
            entity.PrimaryColumn.IsPrimaryKey = true;
            entity.PrimaryColumn.CsType = "long";
            entity.PrimaryColumn.DataType = "Int64";
            if (entity.PrimaryColumn.DataBaseField != null)
                entity.PrimaryColumn.DataBaseField.FieldType = "BIGINT";
        }

        /// <summary>
        ///     所有非自增主键设置为雪花码
        /// </summary>
        /// <param name="entity"></param>
        public void AutoSnowFlakeId(IEntityConfig entity)
        {
            if (entity.PrimaryColumn == null || (!entity.PrimaryColumn.NoStorage && !entity.PrimaryColumn.DataBaseField.IsIdentity))
            {
                entity.Interfaces = entity.Interfaces?.Replace("ISnowFlakeId", "");
            }
            else if (string.IsNullOrWhiteSpace(entity.Interfaces))
                entity.Interfaces = "ISnowFlakeId";
            else if (!entity.Interfaces.Contains("ISnowFlakeId"))
                entity.Interfaces += ",ISnowFlakeId";
        }


    }
}