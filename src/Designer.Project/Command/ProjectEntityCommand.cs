﻿using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 扩展节点
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public sealed class ProjectEntityCommand : DesignCommondBase<ProjectConfig>
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
                SoruceView = "entity",
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
                SoruceView = "entity",
                Caption = "粘贴实体",
                Action = PasteTable,
                IconName = "tree_item"
            });

            commands.Add(new CommandItemBuilder
            {
                SignleSoruce = true,
                IsButton = true,
                SoruceView = "entity",
                Catalog = "编辑",
                Caption = "复制基础配置到其它项目",
                Action = CopyToProject,
                IconName = "tree_item"
            });
        }

        public void PasteTable(object arg)
        {
            if (Context.SelectProject == null)
            {
                MessageBox.Show("请选择一个项目先");
                return;
            }
            if (Context.CopiedTables.Count == 0)
            {
                MessageBox.Show("剪贴板为空");
                return;
            }

            foreach (var entity in Context.CopiedTables)
            {
                entity.Parent = Context.SelectProject;
                entity.Project = Context.SelectProject.Name;
                foreach (var pro in entity.Properties)
                {
                    pro.Tag = $"{entity.Tag},{pro.CppType},{pro.Name}";
                }

                Context.SelectProject.Add(entity);
            }

            //this.Context.CopiedTable = null;
            //this.Context.CopiedTables.Clear();
            //Context.SelectColumns = null;
            //this.RaisePropertyChanged(() => this.Context.CopiedTableCounts);
        }

        public void AddEntity(object arg)
        {
            if (Context.SelectProject == null)
            {
                MessageBox.Show("请选择一个项目");
                return;
            }

            var project = Context.SelectProject;
            var entity = new EntityConfig{ Parent = project };
            if (CommandIoc.EditEntityCommand(entity))
            {
                project.Add(entity);

                Model.Tree.SetSelectEntity(entity);
                return;
            }
            project.Remove(entity);
        }
        /// <summary>
        /// 复制项目
        /// </summary>
        public void CopyToProject(object arg)
        {
            if (Context.SelectProject == null)
            {
                return;
            }
            foreach (var project in Context.Solution.Projects.Where(p => p != Context.SelectProject))
            {
                project.DataBaseObjectName = Context.SelectProject.DataBaseObjectName;
                //if (string.IsNullOrWhiteSpace(project.NameSpace))
                project.NameSpace = Context.SelectProject.NameSpace;
                //if (string.IsNullOrWhiteSpace(project.PagePath))
                project.ModelFolder = Context.SelectProject.ModelFolder;
                project.ApiFolder = Context.SelectProject.ApiFolder;
                project.PageFolder = Context.SelectProject.PageFolder;
                //if (string.IsNullOrWhiteSpace(project.BusinessPath))
                project.BusinessPath = Context.SelectProject.BusinessPath;
                //if (string.IsNullOrWhiteSpace(project.ModelPath))
                //if (string.IsNullOrWhiteSpace(project.CodePath))
                project.CppCodePath = Context.SelectProject.CppCodePath;
                //if (string.IsNullOrWhiteSpace(project.DataBaseObjectName))
                project.DataBaseObjectName = Context.SelectProject.DataBaseObjectName;
                //if (string.IsNullOrWhiteSpace(project.DataBaseObjectName))
                project.DataBaseObjectName = Context.SelectProject.DataBaseObjectName;
            }
        }

    }
}
