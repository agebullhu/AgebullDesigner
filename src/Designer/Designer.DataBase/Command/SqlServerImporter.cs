using System;
using System.Collections;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;
using Agebull.EntityModel.Designer;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

namespace Agebull.Common.Config.Designer
{
    /// <summary>
    /// 命令注册器
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class SqlServerImporter : IAutoRegister
    {
        /// <summary>
        /// 注册代码
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            CommandCoefficient.RegisterItem<ProjectConfig>(new CommandItemBuilder<ProjectConfig, int>(BeginImportSqlServer, DoImportSqlServer, EndImportSqlServer)
            {
                Caption = "导入SqlServer数据库",
                Catalog = "数据库",
                SoruceView = "entity",
                WorkView = "database",
                IconName = "tree_Assembly"
            });
        }

        /// <summary>
        /// 导入数据库
        /// </summary>
        /// <returns></returns>
        public bool BeginImportSqlServer(ProjectConfig _)
        {
            if (DataModelDesignModel.Current.Context.SelectProject == null)
            {
                MessageBox.Show("请选择一个项目并正确设置连接信息后继续");
                return false;
            }
            var name = DataModelDesignModel.Current.Context.SelectProject.Caption ??
                DataModelDesignModel.Current.Context.SelectProject.Name;
            if (MessageBox.Show($"确认在【{name}】中执行【导入SQLSERVER数据库】操作吗?",
                          "对象编辑", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return false;

            DataModelDesignModel.Current.Editor.ShowTrace();
            return true;
        }

        /// <summary>
        /// 导入数据库
        /// </summary>
        /// <returns></returns>
        public int DoImportSqlServer(ProjectConfig _)
        {
            var checker = new SqlSchemaChecker
            {
                Project = DataModelDesignModel.Current.Context.SelectProject
            };
            checker.ImportProject();
            return checker.Project.Entities.Count;
        }
        /// <summary>
        /// 导入数据库完成
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="ex">异常</param>
        /// <param name="cnt">导入的表的数量</param>
        internal void EndImportSqlServer(CommandStatus status, Exception ex, int cnt)
        {
            MessageBox.Show(status == CommandStatus.Succeed ? $"成功导入{cnt}个表" : $"导入失败{ex?.Message }");
        }
    }
}