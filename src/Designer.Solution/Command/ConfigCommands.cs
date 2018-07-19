// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 配置:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// 建立:2014-11-20
// 修改:2014-11-29
// *****************************************************/

#region 引用

using System;
using System.Linq;
using System.Text;
using System.Windows;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

#endregion

namespace Agebull.EntityModel.Designer
{
    public abstract class ConfigCommands<TConfig> : DesignCommondBase<TConfig>
    {
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
        /// 新增项目
        /// </summary>
        public void AddProject(object arg)
        {
            if (Model.CreateNew("新增项目",out ProjectConfig config))
                Context.Solution.Add(config);
        }
        /// <summary>
        /// 新增命令
        /// </summary>
        /// <param name="entity"></param>
        public void AddCommand(EntityConfig entity)
        {
            if (Model.CreateNew("新增命令",out UserCommandConfig config))
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
