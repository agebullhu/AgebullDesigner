﻿// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 配置:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// 建立:2014-11-20
// 修改:2014-11-29
// *****************************************************/

#region 引用

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Windows;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

#endregion

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 导入导出相关模型
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal class EntityEditCommands : DesignCommondBase<EntityConfig>
    { 
        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            commands.Add(new CommandItemBuilder
            {
                Action = SaveEntity,
                Caption = "保存所选对象",
                SignleSoruce = true,
                IsButton = true,
                Catalog = "编辑",
                ConfirmMessage= "确认强制保存吗?\n要知道这存在一定破坏性!",
                IconName = "tree_item"
            });

            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = AddNewProperty,
                Caption = "导入字段",
                SignleSoruce = true,
                IsButton = true,
                Catalog = "编辑",
                IconName = "tree_Open",
                SoruceView = "property"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = CopyTable,
                Caption = "复制实体",
                IsButton = true,
                SignleSoruce = true,
                Catalog = "编辑",
                IconName = "tree_Child1"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = DeleteEntity,
                Caption = "删除实体",
                IsButton = false,
                SignleSoruce = true,
                Catalog = "编辑",
                IconName = "img_del"
            });
        }


        public void DeleteEntity(EntityConfig entity)
        {
            if (entity == null ||
                MessageBox.Show($"确认删除{entity.Name}({entity.Caption})吗?", "对象编辑", MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes)
            {
                return;
            }
            entity.Parent.Remove(entity);
        }


        public void CopyTable(EntityConfig entity)
        {
            Context.CopiedTable = new EntityConfig();
            Context.CopiedTable.CopyValue(entity);
            Context.CopiedTables.Add(Context.CopiedTable);
            RaisePropertyChanged(() => Context.CopiedTableCounts);
        }

        /// <summary>
        /// 新增属性
        /// </summary>
        /// <param name="entity"></param>
        public void AddNewProperty(EntityConfig entity)
        {
            CommandIoc.AddFieldsCommand(entity);
        }

        /// <summary>
        /// 强制保存
        /// </summary>
        public void SaveEntity(object arg)
        {
            ConfigWriter writer = new ConfigWriter
            {
                Solution = Context.Solution,
            };
            if (Context.SelectProject != null)
            {
                writer.SaveProject(Context.SelectProject, Path.GetDirectoryName(Context.Solution.SaveFileName));
                return;
            }
            var tables = Context.GetSelectEntities();
            foreach (var entity in tables)
            {
                writer.SaveEntity(entity, Path.GetDirectoryName(Context.Solution.SaveFileName),true);
            }
        }

    }
}
