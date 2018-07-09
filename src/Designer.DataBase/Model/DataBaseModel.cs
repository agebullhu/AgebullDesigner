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
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

#endregion

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 数据库相关命令
    /// </summary>
    internal class DataBaseModel: EntityDesignModel
    {
        #region 基础
        

        /// <summary>
        /// 生成命令对象
        /// </summary>
        /// <param name="commands"></param>
        protected override void CreateCommands(ObservableCollection<CommandItem> commands)
        {
            commands.Add(NormalDb);
        }

        #endregion


        #region 数据库设计检查

        private CommandItem _normalDb;
        /// <summary>
        /// 数据库设计检查
        /// </summary>
        public CommandItem NormalDb => _normalDb ?? (_normalDb = new CommandItem
        {
            NoButton = true,
            Command = new DelegateCommand(RepairByDb),
            Caption = "数据库设计检查",
            Image = Application.Current.Resources["tree_item"] as ImageSource
        });

        /// <summary>
        /// 数据库设计检查
        /// </summary>
        public void RepairByDb()
        {
            var result = MessageBox.Show("是重构基础数据库设计,否仅检查并修改不正确的设置项", "数据库设计检查", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Cancel)
            {
                return;
            }
            var tables = Context.GetSelectEntities();
            foreach (var entity in tables)
            {
                EntityDatabaseBusiness business = new EntityDatabaseBusiness
                {
                    Entity = entity
                };
                business.CheckDbConfig(result == MessageBoxResult.Yes);
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
