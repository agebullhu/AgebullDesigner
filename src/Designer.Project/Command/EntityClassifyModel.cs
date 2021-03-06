﻿using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 扩展节点
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public sealed class EntityClassifyModel : DesignCommondBase<EntityClassify>
    {
        /// <summary>
        /// 生成命令对象
        /// </summary>
        /// <returns></returns>
        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            commands.Add(new CommandItemBuilder
            {
                Catalog = "编辑",
                SignleSoruce = true,
                IsButton = true,
                Caption = "增加实体",
                Action = AddEntity,
                IconName = "tree_Open"
            });
            commands.Add(new CommandItemBuilder
            {
                Catalog = "编辑",
                SignleSoruce = true,
                IsButton = true,
                Caption = "粘贴实体",
                Action = PasteTable,
                IconName = "tree_item"
            });
        }

        public void PasteTable(object arg)
        {
            if (Context.CopiedTables.Count == 0)
            {
                MessageBox.Show("剪贴板为空");
                return;
            }
            var cls = arg as EntityClassify;
            if (cls == null)
            {
                MessageBox.Show("请选择一个项目或类目");
                return;
            }
            var project = cls.Project;

            foreach (var entity in Context.CopiedTables)
            {
                entity.Classify = cls.Name;
                foreach (var pro in entity.Properties)
                {
                    pro.Tag = $"{entity.Tag},{pro.CppType},{pro.Name}";
                }

                project.Add(entity);
            }

            //this.Context.CopiedTable = null;
            //this.Context.CopiedTables.Clear();
            //Context.SelectColumns = null;
            //this.RaisePropertyChanged(() => this.Context.CopiedTableCounts);
        }

        public void AddEntity(object arg)
        {
            var cls = arg as EntityClassify;
            if (cls == null)
            {
                MessageBox.Show("请选择一个项目或类目");
                return;
            }

            var project = cls.Project;
            var entity = new EntityConfig
            {
                Parent = project,
                Classify = cls.Name
            };
            if (CommandIoc.EditEntityCommand(entity))
            {
                project.Add(entity);
                Model.Tree.SetSelectEntity(entity);
                return;
            }
            project.Remove(entity);
        }
    }
}