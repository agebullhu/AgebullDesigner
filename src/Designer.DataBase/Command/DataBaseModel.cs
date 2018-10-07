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
using System.Windows;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

#endregion

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 数据库相关命令
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal class DataBaseModel : DesignCommondBase<EntityConfig>
    {
        /// <summary>
        /// 生成命令对象
        /// </summary>
        /// <param name="commands"></param>
        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            commands.Add(new CommandItemBuilder<EntityConfig>()
            {
                Action= RepairByDb,
                Catalog = "数据库",
                Caption = "重构数据库设计",
                Editor = "DataBase",
                IconName = "tree_item",
                ConfirmMessage = "是否执行【重构数据库设计】操作"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>()
            {
                Action = CheckByDb,
                Catalog = "数据库",
                Caption = "修复数据库设计",
                Editor = "DataBase",
                IconName = "tree_item",
                ConfirmMessage = "是否执行【修复数据库设计】操作"
            });
        }

        #region 基础

        #endregion


        #region 数据库设计检查

        /// <summary>
        /// 数据库设计检查
        /// </summary>
        public void CheckByDb(EntityConfig entity)
        {
            var business = new EntityDatabaseBusiness
            {
                Entity = entity
            };
            business.CheckDbConfig(false);
        }

        /// <summary>
        /// 数据库设计检查
        /// </summary>
        public void RepairByDb(EntityConfig entity)
        {
            using (CodeGeneratorScope.CreateScope(entity))
            {
                EntityDatabaseBusiness business = new EntityDatabaseBusiness
                {
                    Entity = entity
                };
                business.CheckDbConfig(true);
            }
        }

        #endregion

        #region 校验数据库字段

        /// <summary>
        /// 校验数据库字段
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string DoCheckDbFieldes(string path)
        {
            foreach (var prj in Context.Solution.Projects)
            {
                var builder = new SqlSchemaChecker
                {
                    Project = prj
                };
                builder.CheckColumns();
            }
            return string.Empty;
        }

        internal void CheckDbFieldesEnd(CommandStatus status, Exception ex, string code)
        {
            if (status == CommandStatus.Succeed)
            {
                Model.ConfigIo.Save();
            }
        }

        #endregion

        #region 重构数据库

        public bool AlertTablesPrepare(string arg, Action<string> setAction)
        {
            return MessageBox.Show("确认要重构数据库吗", "对象编辑", MessageBoxButton.YesNo) == MessageBoxResult.Yes;
        }

        /// <summary>
        /// 重构数据库
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool DoAlertTables(string path)
        {
            foreach (var prj in Context.Solution.Projects)
            {
                var checker = new SqlSchemaChecker { Project = prj };
                checker.AlertTables(prj.DbHost, prj.DbUser, prj.DbPassWord, prj.DbSoruce);
            }
            return true;
        }

        internal void AlertTablesEnd(CommandStatus status, Exception ex, bool result)
        {
            if (status == CommandStatus.Succeed)
            {
                Model.ConfigIo.Save();
            }
        }

        #endregion

        #region 数据类型检查

        public bool IntCheckPrepare(string arg, Action<string> setAction)
        {
            return MessageBox.Show("确认要检查可转为INT吗", "对象编辑", MessageBoxButton.YesNo) == MessageBoxResult.Yes;
        }

        public bool DoIntCheck(string path)
        {
            foreach (var prj in Context.Solution.Projects)
            {
                var checker = new SqlSchemaChecker { Project = prj };
                checker.IntCheck();
            }
            return true;
        }

        internal void IntCheckEnd(CommandStatus status, Exception ex, bool result)
        {
            if (status == CommandStatus.Succeed)
            {
                //Save();
            }
        }

        #endregion
    }
}
