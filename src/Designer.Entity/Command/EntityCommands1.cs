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
using System.IO;
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
    internal class EntityCommands : DesignCommondBase<EntityConfig>
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

            commands.Add(new CommandItemBuilder<string, string>(ValidatePrepare, Validate, ValidateEnd)
            {
                TargetType = typeof(EntityConfig),
                Caption = "检查设计",
                Catalog = "工具",
                IconName = "tree_item"
            });

            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                SignleSoruce=true,
                Action = AddCommand,
                Caption = "新增命令",
                Catalog = "编辑",
                IconName = "tree_Open",
                SoruceView = "command",
                WorkView = "Model"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                SignleSoruce = true,
                Action = AddAuditCommand,
                Caption = "新增审核命令",
                Catalog = "编辑",
                IconName = "tree_Open",
                SoruceView = "command",
                WorkView = "Model"
            });
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

        #region 客户端检测

        public bool CheckClientPrepare(string arg, Action<string> setAction)
        {
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
        /// 新增命令
        /// </summary>
        /// <param name="entity"></param>
        public void AddCommand(EntityConfig entity)
        {
            if (Model.CreateNew("新增命令", out UserCommandConfig config))
                entity.Add(config);
        }
        /// <summary>
        /// 新增审核命令
        /// </summary>
        /// <param name="entity"></param>
        public void AddAuditCommand(EntityConfig entity)
        {
            if (entity.Commands.Count != 0 && entity.Commands.Any(p => p.Name == "Pass"))
                return;
            entity.Add(new UserCommandConfig
            {
                Name = "Pass",
                Button = "btnPass",
                Caption = "审核通过",
                Description = "审核通过"
            });
            entity.Add(new UserCommandConfig
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
                        cls.Project = project;
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
