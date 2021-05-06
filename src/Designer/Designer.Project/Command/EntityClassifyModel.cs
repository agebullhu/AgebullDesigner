using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;

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
            commands.Add(new CommandItemBuilder<EntityClassify>
            {
                Catalog = "编辑",
                SignleSoruce = true,
                IsButton = true,
                Caption = "增加实体",
                SoruceView = "entity",
                Action = AddEntity,
                IconName = "增加"
            });
            commands.Add(new CommandItemBuilder<EntityClassify>
            {
                Catalog = "编辑",
                SignleSoruce = true,
                SoruceView = "entity",
                IsButton = true,
                Caption = "粘贴实体",
                Action = PasteTable,
                IconName = "粘贴"
            });
        }

        public void PasteTable(object arg)
        {
            if (Context.CopiedTables.Count == 0)
            {
                MessageBox.Show("剪贴板为空");
                return;
            }
            if (arg is not EntityClassify cls)
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
            if (arg is not EntityClassify cls)
            {
                MessageBox.Show("请选择一个项目或类目");
                return;
            }

            var project = cls.Project;
            var entity = new EntityConfig
            {
                Project = project,
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