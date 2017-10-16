// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 配置:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// 建立:2014-11-20
// 修改:2014-11-29
// *****************************************************/

#region 引用

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
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
    internal class SolutionCommands : ConfigCommands<SolutionConfig>
    {
        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            commands.Add(new CommandItemBuilder
            {
                Signle = true,
                Name = "新增项目",
                SourceType = typeof(SolutionConfig).FullName,
                Command = new DelegateCommand(AddProject),
                IconName = "img_add"
            });
        }

    }
    /// <summary>
    /// 导入导出相关模型
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal class EntityCommands1 : ConfigCommands<EntityConfig>
    {
        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            commands.Add(new CommandItemBuilder
            {
                Command = new DelegateCommand(() => Model.ConfigIo.SaveEntity()),
                SourceType = $"{typeof(EntityConfig).Name}",
                Name = "强制保存实体",
                Signle = true,
                NoButton = true,
                IconName = "tree_item"
            });

            commands.Add(new CommandItemBuilder
            {
                SourceType = $"{typeof(EntityConfig).Name},{typeof(PropertyConfig).Name}",
                Command = new AsyncCommand<string, string>(ValidatePrepare, Validate, ValidateEnd),
                Name = "检查设计",
                IconName = "tree_item"
            });

            commands.Add(new CommandItemBuilder
            {
                Command = new DelegateCommand<EntityConfig>(AddCommand),
                Name = "新增命令",
                Catalog = "命令",
                IconName = "tree_Open"
            });
            commands.Add(new CommandItemBuilder
            {
                Command = new DelegateCommand<EntityConfig>(AddAuditCommand),
                Name = "新增审核命令",
                Catalog = "命令",
                IconName = "tree_Open"
            });
        }
    }

    /// <summary>
    /// 导入导出相关模型
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal class UserCommandConfigCommands : ConfigCommands<UserCommandConfig>
    {
        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            commands.Add(new CommandItemBuilder
            {
                Command = new DelegateCommand<ConfigTreeItem<UserCommandConfig>>(p =>
                {
                    var par = p.Parent as ConfigTreeItem<EntityConfig>;
                    if (par == null)
                        return;
                    par.Model.Commands.Remove(p.Model);
                    par.Items.Remove(p);
                }),
                Signle = true,
                SourceType = $"{typeof(UserCommandConfig).Name}",
                Name = "删除命令",
                IconName = "img_del"
            });
        }
    }
    /// <summary>
    /// 导入导出相关模型
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal class PropertyCommands : ConfigCommands<PropertyConfig>
    {
        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            commands.Add(new CommandItemBuilder
            {
                Command = new DelegateCommand<ConfigTreeItem<PropertyConfig>>(p =>
                {
                    var par = p.Parent as ConfigTreeItem<EntityConfig>;
                    if (par == null)
                    {
                        return;
                    }
                    par.Model.Properties.Remove(p.Model);
                    par.Items.Remove(p);
                }),
                Name = "删除字段",
                Signle = true,
                NoButton = true,
                IconName = "img_del"
            });
        }
    }

    /// <summary>
    /// 导入导出相关模型
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal class ProjectCommands : ConfigCommands<ProjectConfig>
    {
        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            string type = $"{typeof(ProjectConfig).Name}";
            commands.Add(new CommandItemBuilder
            {
                NoButton = true,
                Signle = true,
                Command = new DelegateCommand<ProjectConfig>(ProjectBusinessModel.Lock),
                Name = "锁定",
                SourceType = type,
                IconName = "img_lock"
            });
            commands.Add(new CommandItemBuilder
            {
                NoButton = true,
                Signle = true,
                Command = new DelegateCommand<ProjectConfig>(ProjectBusinessModel.UnLock),
                Name = "解锁",
                SourceType = type,
                IconName = "img_no_modify"
            });
            commands.Add(new CommandItemBuilder
            {
                NoButton = true,
                Signle = true,
                Command = new DelegateCommand<ProjectConfig>(ProjectBusinessModel.ToClass),
                Name = "全部设置为类",
                SourceType = type,
                IconName = "tree_Type"
            });
            commands.Add(new CommandItemBuilder
            {
                NoButton = true,
                Signle = true,
                Command = new DelegateCommand<ProjectConfig>(ProjectBusinessModel.ToReference),
                Name = "全部设置为引用",
                SourceType = type,
                IconName = "img_ref"
            });
            commands.Add(new CommandItemBuilder
            {
                NoButton = true,
                Signle = true,
                Command = new DelegateCommand<ProjectConfig>(ProjectBusinessModel.ToModify),
                Name = "强制已修改",
                SourceType = type,
                IconName = "img_modify"
            });
        }
    }
    public abstract class ConfigCommands<TConfig> : DesignCommondBase<TConfig>
    {
        #region 客户端检测

        public bool CheckClientPrepare(string arg, Action<string> setAction)
        {
            return true;
        }


        public bool DoCheckClientFieldes(string path)
        {
            foreach (var entity in Context.Solution.Entities)
            {
                if (entity.Properties.Any(p => p.IsUserId))
                {
                    entity.PrimaryColumn.CsType = "long";
                    entity.PrimaryColumn.DbType = "BIGINT";
                }
            }
            return true;
        }

        public void CheckClientEnd(CommandStatus status, Exception ex, bool result)
        {
            if (status == CommandStatus.Succeed)
            {
                Model.ConfigIo.SaveSolution();
                MessageBox.Show("完成");
            }
        }

        #endregion


        #region 关联设置

        /// <summary>
        /// 新增项目
        /// </summary>
        public void AddProject()
        {
            ProjectConfig config;
            if (Model.CreateNew("新增项目",out config))
                Context.Solution.Projects.Add(config);
        }
        /// <summary>
        /// 新增命令
        /// </summary>
        /// <param name="entity"></param>
        public void AddCommand(EntityConfig entity)
        {
            UserCommandConfig config;
            if (Model.CreateNew("新增命令",out config))
                entity.Commands.Add(config);
        }
        /// <summary>
        /// 新增审核命令
        /// </summary>
        /// <param name="entity"></param>
        public void AddAuditCommand(EntityConfig entity)
        {
            if (entity.Commands.Count != 0 && entity.Commands.Any(p => p.Name == "Pass"))
                return;
            entity.Commands.Add(new UserCommandConfig
            {
                Name = "Pass",
                Button = "btnPass",
                Caption = "审核通过",
                Description = "审核通过"
            });
            entity.Commands.Add(new UserCommandConfig
            {
                Name = "Deny",
                Button = "btnDeny",
                Caption = "拒绝通过",
                Description = "拒绝通过"
            });
        }

        #endregion


        #region 检查与修复
        public void ToLinName()
        {
            try
            {
                foreach (var project in Context.Solution.Projects)
                {
                    foreach (var cls in project.Classifies)
                    {
                        foreach (var entity in cls.Items)
                        {
                            entity.Name = entity.CppName;
                            entity.EntityName = $"{cls.Abbreviation}_{entity.Name}_M";
                            foreach (var property in entity.Properties)
                            {
                                var name = property.Name.ToLWord();
                                if (string.IsNullOrWhiteSpace(property.Alias))
                                {
                                    property.Alias = name;
                                }
                                else if (!property.Alias.Contains(name))
                                {
                                    property.Alias += "," + name;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Context.CurrentTrace.TraceMessage.Status = ex.ToString();
            }
        }


        public bool ValidatePrepare(string arg, Action<string> setAction)
        {
            Context.NowJob = DesignContext.JobTrace;
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
            Context.NowJob = DesignContext.JobTrace;
        }

        #endregion
    }
}
