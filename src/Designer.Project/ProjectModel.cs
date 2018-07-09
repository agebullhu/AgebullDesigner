using System.Collections.Generic;
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
    public sealed class ProjectModel : DesignCommondBase<ProjectConfig>
    {
        /// <summary>
        /// 生成命令对象
        /// </summary>
        /// <returns></returns>
        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            commands.Add(new CommandItemBuilder
            {
                Catalog="编辑",
                Signle = true,
                Caption = "增加表",
                Command = new DelegateCommand(AddEntity),
                IconName = "tree_Open"
            });
            commands.Add(new CommandItemBuilder
            {
                Catalog = "编辑",
                Signle = true,
                NoButton = true,
                Caption = "粘贴表",
                Command = new DelegateCommand(PasteTable),
                IconName = "tree_item"
            });

            commands.Add(new CommandItemBuilder
            {
                Signle = true,
                NoButton = true,
                Catalog = "编辑",
                Caption = "复制到其它项目",
                Command = new DelegateCommand(CopyToProject),
                IconName = "tree_item"
            });
        }

        public void PasteTable()
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


        public void AddEntity()
        {
            if (Context.SelectProject == null)
            {
                MessageBox.Show("请选择一个项目");
                return;
            }
            EntityConfig entity;
            if (!Model.CreateNew("新增实体",out entity))
            {
                return;
            }
            Context.SelectProject.Add(entity);
            Model.Tree.SetSelectEntity(entity);
            Context.SelectColumns = null;
            var nentity = CommandIoc.AddFieldsCommand();
            if (nentity == null)
            {
                return;
            }
            entity.Properties.AddRange(nentity.Properties);
        }
        /// <summary>
        /// 复制项目
        /// </summary>
        public void CopyToProject()
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
                project.PagePath = Context.SelectProject.PagePath;
                //if (string.IsNullOrWhiteSpace(project.BusinessPath))
                project.BusinessPath = Context.SelectProject.BusinessPath;
                //if (string.IsNullOrWhiteSpace(project.ModelPath))
                project.ModelPath = Context.SelectProject.ModelPath;
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
