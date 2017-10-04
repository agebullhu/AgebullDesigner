using System;
using System.Collections;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;
using Agebull.Common.SimpleDesign;
using Gboxt.Common.DataAccess.Schemas;
using Gboxt.Common.SimpleDataAccess.Design.Schemas;
using Gboxt.Common.WpfMvvmBase.Commands;
using WpfMvvmBase.Coefficient;

namespace Agebull.Common.Config.Designer
{
    /// <summary>
    /// 关系连接检查
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class SqlServerImporter : ConfigCommandBase<ProjectConfig>, IAutoRegister
    {
        /// <summary>
        /// 注册代码
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            NoButton = true;
            Signle = true;
            CommandCoefficient.RegisterCommand<ProjectConfig, SqlServerImporter>();
        }

        
        public override CommandItem ToCommand(object arg, Func<object, IEnumerator> enumerator = null)
        {
            return new CommandItem
            {
                NoButton = true,
                Signle = true,
                SourceType = typeof(SolutionConfig).Name,
                Command = new AsyncCommand<ProjectConfig, int>(BeginImportSqlServer, DoImportSqlServer, EndImportSqlServer),
                Name = "导入SQLSERVER数据库",
                Image = Application.Current.Resources["img_open"] as ImageSource
            };
        }
        
        /// <summary>
        /// 导入数据库
        /// </summary>
        /// <param name="p"></param>
        /// <param name="setArgument"></param>
        /// <returns></returns>
        public bool BeginImportSqlServer(ProjectConfig p, Action<ProjectConfig> setArgument)
        {
            var project = GlobalConfig.CurrentConfig as ProjectConfig;
            if (project == null)
                return false;
            setArgument(project);
            return true;
        }

        /// <summary>
        /// 导入数据库
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public int DoImportSqlServer(ProjectConfig project)
        {
            var checker = new SqlSchemaChecker
            {
                Project = project
            };
            checker.ImportProject();
            return project.Entities.Count;
        }
        /// <summary>
        /// 导入数据库完成
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="ex">异常</param>
        /// <param name="cnt">导入的表的数量</param>
        internal void EndImportSqlServer(CommandStatus status, Exception ex, int cnt)
        {
            if (status == CommandStatus.Succeed)
            {
                MessageBox.Show($"成功导入{cnt}个表");
            }
            else
            {
                MessageBox.Show($"导入失败");
            }

        }
    }
}