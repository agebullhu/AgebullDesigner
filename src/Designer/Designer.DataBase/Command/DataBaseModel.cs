// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 配置:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// 建立:2014-11-20
// 修改:2014-11-29
// *****************************************************/

#region 引用

using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;

#endregion

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 数据库相关命令
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal class DataBaseModel : DesignCommondBase<IEntityConfig>
    {
        /// <summary>
        /// 生成命令对象
        /// </summary>
        /// <param name="commands"></param>
        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            commands.Add(new CommandItemBuilder<IEntityConfig>()
            {
                Action = Upgrade,
                Catalog = "数据库",
                CanButton=true,
                SoruceView = "entity",
                Caption = "重新升级",
                Editor = "DataBase",
                WorkView = "database",
                IconName = "分析"
            });
            commands.Add(new CommandItemBuilder<IEntityConfig>()
            {
                Action = StandardName,
                Catalog = "数据库",
                SoruceView = "entity",
                Caption = "标准化字段名称",
                Editor = "DataBase",
                WorkView = "database",
                IconName = "格式",
                ConfirmMessage = "是否执行【标准化字段名称】操作"
            });
            commands.Add(new CommandItemBuilder<IEntityConfig>()
            {
                Name = "Link Field Check",
                Caption = "关系连接修复",
                IconName = "修复",
                Catalog = "工具",
                Action = ent => DataBaseHelper.CheckFieldLink(ent.DataTable.Fields),
                Editor = "DataRelation",
                WorkView = "database,model"
            });

            commands.Add(new CommandItemBuilder<IEntityConfig>()
            {
                Action = RepairByDb,
                Catalog = "数据库",
                SoruceView = "entity",
                Caption = "重构数据库设计",
                Editor = "DataBase",
                WorkView = "database",
                IconName = "修复",
                ConfirmMessage = "是否执行【重构数据库设计】操作"
            });
            commands.Add(new CommandItemBuilder<IEntityConfig>()
            {
                Action = CheckByDb,
                Catalog = "数据库",
                Caption = "修复数据库设计",
                SoruceView = "entity",
                Editor = "DataBase",
                WorkView = "database",
                IconName = "修复",
                ConfirmMessage = "是否执行【修复数据库设计】操作"
            });
            commands.Add(new CommandItemBuilder<IEntityConfig>
            {
                Action = CheckRelation,
                Catalog = "数据库",
                SoruceView = "entity",
                Caption = "修复数据关联",
                Editor = "DataBase",
                WorkView = "database",
                IconName = "修复",
                ConfirmMessage = "是否执行【修复数据库设计】操作"
            });

            commands.Add(new CommandItemBuilder<IEntityConfig>
            {
                Action = CheckIndex,
                Catalog = "数据库",
                SoruceView = "entity",
                Caption = "索引构建",
                Editor = "DataBase",
                WorkView = "database",
                IconName = "索引",
                ConfirmMessage = "是否执行【修复数据库设计】操作"
            });
        }

        #region 数据库设计检查

        /// <summary>
        /// 数据库设计检查
        /// </summary>
        public void CheckRelation(IEntityConfig entity)
        {
            DataBaseHelper.CheckFieldLink(entity.DataTable.Fields);
        }

        /// <summary>
        /// 数据库设计检查
        /// </summary>
        public void CheckIndex(IEntityConfig entity)
        {
            var business = new DataTableApplication
            {
                DataTable = entity.DataTable
            };
            business.CheckIndex();
        }

        /// <summary>
        /// 数据库设计检查
        /// </summary>
        public void StandardName(IEntityConfig entity)
        {
            var business = new DataTableApplication
            {
                DataTable = entity.DataTable
            };
            business.StandardName(false);
        }

        /// <summary>
        /// 数据库设计检查
        /// </summary>
        public void CheckByDb(IEntityConfig entity)
        {
            var business = new DataTableApplication
            {
                DataTable = entity.DataTable
            };
            business.CheckDbConfig(false);
        }

        /// <summary>
        /// 数据库设计检查
        /// </summary>
        public void Upgrade(IEntityConfig entity)
        {
            using (RepairScope.CreateScope(entity))
            {
                entity.DataTable = new Config.V2021.DataTableConfig
                {
                    Entity = entity
                };
                entity.DataTable.Upgrade();
                entity.DataTable.OnLoad();
            }
        }

        /// <summary>
        /// 数据库设计检查
        /// </summary>
        public void RepairByDb(IEntityConfig entity)
        {
            using (RepairScope.CreateScope(entity))
            {
                DataTableApplication business = new DataTableApplication
                {
                    DataTable = entity.DataTable
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
                Model.ConfigIo.SaveProject();
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
                Model.ConfigIo.SaveProject();
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
