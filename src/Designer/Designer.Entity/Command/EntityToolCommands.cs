using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder;

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
                SoruceView = "entity",
                IconName = "tree_item"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = PrimaryKeyName,
                Caption = "主键标题为[表名]ID",
                Catalog = "字段",
                IconName = "tree_Open",
                SoruceView = "entity"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = ForeignKeyName,
                Caption = "外键名称规范(Caption相同,Name为[主表]+Id)",
                Catalog = "字段",
                IconName = "tree_Open",
                SoruceView = "entity"
            });

            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = ForeignKeyDbTypeSync,
                Caption = "外键数据类型一致",
                Catalog = "字段",
                IconName = "tree_Open",
                SoruceView = "entity"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = ForeignKeyName,
                Caption = "外键信息同主键",
                Catalog = "字段",
                IconName = "tree_Open",
                SoruceView = "entity"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = KeyNoClient,
                SignleSoruce = true,
                Caption = "主外键客户端不可见",
                Catalog = "字段",
                IconName = "tree_Open",
                SoruceView = "entity"
            }); 
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = DateShowTime,
                Caption = "日期字段精确到时间",
                Catalog = "字段",
                SoruceView = "entity",
                IconName = "tree_item"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                TargetType = typeof(EntityConfig),
                Name = "规范实体名",
                SoruceView = "entity,model",
                Caption = "规范实体名([Name]Data)",
                Action = CheckDataName,
                Catalog = "工具"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Catalog = "实体",
                SignleSoruce = false,
                IsButton = false,
                SoruceView = "entity",
                Caption = "自动分类",
                Action = AutoClassify,
                IconName = "tree_Open"
            });

            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Catalog = "实体",
                SignleSoruce = false,
                IsButton = false,
                SoruceView = "entity",
                Caption = "接口识别",
                Action = AutoInterface,
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
        ///     接口识别
        /// </summary>
        /// <param name="entity"></param>
        public void AutoInterface(EntityConfig entity)
        {
            foreach (var i in GlobalConfig.GetEntities(p => p.IsInterface))
            {
                bool all = true;
                foreach (var pro in i.Properties.Where(p => !p.IsDiscard && !p.IsDelete))
                {
                    if (entity.Properties.Any(p => p.ReferenceKey == pro.Key || string.Equals(p.DbFieldName, pro.DbFieldName, StringComparison.OrdinalIgnoreCase)))
                        continue;
                    all = false;
                    break;
                }
                if (!all)
                    continue;
                if (!entity.DataInterfaces.Contains(i.Name))
                    entity.DataInterfaces.Add(i.Name);
                foreach (var pro in i.Properties.Where(p => !p.IsDiscard && !p.IsDelete))
                {
                    var link = entity.Properties.First(p => p.ReferenceKey == pro.Key || string.Equals(p.DbFieldName, pro.DbFieldName, StringComparison.OrdinalIgnoreCase));
                    entity.Properties.Remove(link);
                }
            }
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
        /// 主外键客户端不可见
        /// </summary>
        /// <param name="entity"></param>
        public void KeyNoClient(EntityConfig entity)
        {
            foreach (var field in entity.Properties.Where(p => p.IsPrimaryKey || p.IsLinkKey))
                field.InnerField = true;
        }

        /// <summary>
        /// 主键名称规范
        /// </summary>
        /// <param name="entity"></param>
        public void PrimaryKeyName(EntityConfig entity)
        {
            entity.PrimaryColumn.Name = "Id";
            entity.PrimaryColumn.DbFieldName = "id";
            entity.PrimaryColumn.Caption = entity.Caption + "ID";
            if(entity.PrimaryColumn.CsType == "string")
            {
                entity.PrimaryColumn.Datalen = 36;
            }
        }

        /// <summary>
        /// 外键名称规范
        /// </summary>
        /// <param name="entity"></param>
        public void ForeignKeyName(EntityConfig entity)
        {
            foreach (var field in entity.Properties.Where(p => p.IsLinkKey))
            {
                var foreign = entity.Parent.Find(field.LinkTable);
                field.Name = foreign.Name + "Id";
                field.DbFieldName = foreign.Name.ToLinkWordName("_", false) + "_id";
                field.JsonName = field.Name.ToLWord();
                field.Caption = foreign.PrimaryColumn.Caption;
                field.CsType = foreign.PrimaryColumn.CsType;
                field.DbType = foreign.PrimaryColumn.DbType;
                field.Datalen = foreign.PrimaryColumn.Datalen;
            }
        }

        /// <summary>
        /// 外键字段类型信息同步
        /// </summary>
        /// <param name="entity"></param>
        public void ForeignKeyDbTypeSync(EntityConfig entity)
        {
            foreach (var field in entity.Properties.Where(p => p.IsLinkKey))
            {
                var foreign = entity.Parent.Find(field.LinkTable);
                field.CsType = foreign.PrimaryColumn.CsType;
                field.DbType = foreign.PrimaryColumn.DbType;
                field.Datalen = foreign.PrimaryColumn.Datalen;
            }
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