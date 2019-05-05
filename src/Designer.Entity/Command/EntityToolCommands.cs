using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 导入导出相关模型
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal class EntityToolCommands : DesignCommondBase<EntityConfig>
    {
        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            commands.Add(new CommandItemBuilder<string, string>(ValidatePrepare, Validate, ValidateEnd)
            {
                TargetType = typeof(EntityConfig),
                Caption = "检查设计",
                Catalog = "工具",
                IconName = "tree_item"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = CheckName,
                Caption = "名称中的[标识]统一替换为[编号]",
                Catalog = "工具",
                IconName = "tree_item"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = DateShowTime,
                Caption = "日期字段精确到时间",
                Catalog = "工具",
                IconName = "tree_item"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                TargetType = typeof(EntityConfig),
                Name = "规范实体名",
                Caption = "规范实体名([Name]Data)",
                Action = CheckDataName,
                Catalog = "工具"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Name = "规范实体主键",
                Caption = "规范实体主键",
                Catalog = "工具",
                WorkView = "database",
                Action = CheckPrimary,
                TargetType = typeof(EntityConfig)
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                TargetType = typeof(EntityConfig),
                Name = "自动关联对象",
                Caption = "自动关联对象",
                Action = RelationChecker.DoChecke,
                Catalog = "工具"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                TargetType = typeof(EntityConfig),
                Name = "自动外联标题",
                Caption = "自动外联标题",
                Action = RelationChecker.DoLink,
                Catalog = "工具"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Catalog = "工具",
                SignleSoruce = false,
                IsButton = false,
                Caption = "自动分类",
                Action = AutoClassify,
                SoruceView = "argument",
                IconName = "tree_Open"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Catalog = "工具",
                SignleSoruce = false,
                IsButton = false,
                Caption = "所有非自增主键设置为雪花码",
                Action = AutoSnowFlakeId,
                SoruceView = "argument",
                IconName = "tree_Open"
            });

        }

        /// <summary>
        /// 规范实体主键
        /// </summary>
        public void CheckPrimary(EntityConfig entity)
        {
            if (entity.PrimaryColumn == null || entity.PrimaryColumn.CsType == "long")
                return;
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


        /// <summary>
        ///     自动分类
        /// </summary>
        /// <param name="entity"></param>
        public void AutoClassify(EntityConfig entity)
        {
            var word = GlobalConfig.SplitWords(entity.Name).FirstOrDefault() ?? "None";
            var cls = entity.Parent.Classifies.FirstOrDefault(p =>
                string.Equals(p.Name, word, StringComparison.OrdinalIgnoreCase));
            if (cls == null)
            {
                entity.Parent.Classifies.Add(cls = new EntityClassify
                {
                    Name = word
                });
            }
            entity.Classify = cls.Name;
            cls.Items.Add(entity);
        }
        /// <summary>
        /// 数据对象名称检查
        /// </summary>
        public void CheckDataName(EntityConfig entity)
        {
            if (string.IsNullOrWhiteSpace(entity.EntityName))
                entity.EntityName = entity.Name + "Data";
        }

        /// <summary>
        /// 数据对象名称检查
        /// </summary>
        public void DateShowTime(EntityConfig entity)
        {
            foreach (var property in entity.Properties)
            {
                if (property.CsType == nameof(DateTime))
                    property.IsTime = true;
            }
        }

        /// <summary>
        /// 数据对象名称检查
        /// </summary>
        public void CheckName(EntityConfig entity)
        {
            foreach (var property in entity.Properties)
            {
                if (string.IsNullOrWhiteSpace(property.Caption))
                    return;
                if (property.IsPrimaryKey && property.Caption == "主键")
                    property.Caption = $"{entity.Caption}编号(主键)";
                else
                    property.Caption = property.Caption.Replace("标识", "编号");

                if (!property.IsLinkField)
                    continue;
                var link = GlobalConfig.GetEntity(property.LinkTable);
                var fi = link?.Properties.FirstOrDefault(p => p.Name == property.LinkField);
                if (fi == null)
                    continue;
                if(property.IsLinkKey)
                    property.Caption = $"{link.Caption}编号(外键)";
                else if (fi.Caption.Contains(link.Caption))
                    property.Caption = fi.Caption;
            }
        }

        #region 检查与修复

        public bool ValidatePrepare(string arg)
        {
            DataModelDesignModel.Current.Editor.ShowTrace();
            Context.CurrentTrace.TraceMessage = TraceMessage.DefaultTrace;
            Context.CurrentTrace.TraceMessage.Clear();
            return true;
        }


        public string Validate(string path)
        {
            var tables = Context.GetSelectEntities();
            foreach (var entity in tables)
            {
                var model = new EntityValidater { Entity = entity };
                model.Validate(Context.CurrentTrace.TraceMessage);
            }
            return string.Empty;
        }

        public void ValidateEnd(CommandStatus status, Exception ex, string code)
        {
        }

        #endregion
    }
}