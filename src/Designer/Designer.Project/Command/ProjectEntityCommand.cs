using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer.NewConfig;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;

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
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                IsButton = true,
                Caption = "生成模型",
                Catalog = "模型",
                SoruceView = "entity",
                IconName = "tree_Open",
                Action = ToModel
            });
            commands.Add(new CommandItemBuilder<ProjectConfig>
            {
                Catalog = "枚举",
                Action = NewEnum,
                NoConfirm = true,
                IsButton = true,
                SignleSoruce = true,
                SoruceView = "enum",
                Caption = "新增枚举",
                IconName = "tree_Open"
            });
            commands.Add(new CommandItemBuilder<ProjectConfig>
            {
                Catalog = "编辑",
                SoruceView = "entity",
                SignleSoruce = true,
                IsButton = true,
                Caption = "增加实体",
                Action = AddEntity,
                IconName = "tree_Open"
            });
            commands.Add(new CommandItemBuilder<ProjectConfig>
            {
                Catalog = "编辑",
                SignleSoruce = true,
                IsButton = true,
                SoruceView = "entity",
                Caption = "粘贴实体",
                Action = PasteTable,
                IconName = "tree_item"
            });

            commands.Add(new CommandItemBuilder<ProjectConfig>
            {
                SignleSoruce = true,
                SoruceView = "entity",
                Catalog = "编辑",
                Caption = "复制基础配置到其它项目",
                Action = CopyToProject,
                IconName = "tree_item"
            });


            commands.Add(new CommandItemBuilder<EntityClassify>
            {
                Catalog = "编辑",
                SoruceView = "entity",
                Caption = "升级为项目",
                Action = ToProject,
                IconName = "tree_item"
            });
        }

        /// <summary>
        /// 数据对象名称检查
        /// </summary>
        public void ToModel(EntityConfig entity)
        {
            //var model = GlobalConfig.GetModel(p=>p.Entity == entity);
            //if (model != null)
            //    return;
            var model = new ModelConfig
            {
                Key = Guid.NewGuid().ToString("N"),
                Entity = entity
            };
            model.CopyConfig(entity);
            ((IEntityConfig)model).Copy(entity);
            foreach (var field in entity.Properties)
            {
                var property = new PropertyConfig
                {
                    Key = Guid.NewGuid().ToString("N"),
                    Field = field
                };
                property.CopyConfig(field);
                ((IPropertyConfig)property).Copy(field);
                model.Add(property);
                property.Field = field;
            }
            entity.Project.Add(model);
            model.Entity = entity;
        }

        public void NewEnum(object arg)
        {
            if (Context.SelectProject == null)
            {
                MessageBox.Show("请选择一个项目先");
                return;
            }
            var em = new EnumConfig();
            var window = new NewConfigWindow
            {
                Title = "新增枚举"
            };
            var vm = (NewConfigViewModel)window.DataContext;
            vm.Config = em;
            if (window.ShowDialog() == true)
                Context.SelectProject.Add(em);
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
                entity.Project = Context.SelectProject;
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
            var entity = new EntityConfig { Project = project };
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


        /// <summary>
        /// 复制项目
        /// </summary>
        public void ToProject(EntityClassify classify)
        {
            var project = new ProjectConfig
            {
                NoClassify = true
            };
            project.Copy(classify);

            var entities = classify.Items.ToArray();
            foreach(var e in entities)
            {
                if (e is EntityConfig entity)
                {
                    classify.Project.Remove(entity);
                }
                else if (e is ModelConfig model)
                {
                    classify.Project.Remove(model);
                }
            }

            classify.Project.Remove(classify);

            project.Add(classify);
            GlobalConfig.CurrentSolution.Add(project);
            foreach (var e in entities)
            {
                if (e is EntityConfig entity)
                {
                    project.Add(entity);
                }
                else if (e is ModelConfig model)
                {
                    project.Add(model);
                }
            }
        }
    }
}
