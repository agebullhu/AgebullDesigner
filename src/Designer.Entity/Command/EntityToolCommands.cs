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
                Caption = "组织单元只读",
                Action = SiteReadOnly,
                SoruceView = "argument",
                IconName = "tree_Open"
            });
        }

        /// <summary>
        ///     组织单元只读
        /// </summary>
        /// <param name="entity"></param>
        public void SiteReadOnly(EntityConfig entity)
        {
            foreach (var field in entity.Properties.Where(p => p.LinkTable == "Site" || p.LinkTable == "Organization"))
            {
                field.IsUserReadOnly = true;
            }
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