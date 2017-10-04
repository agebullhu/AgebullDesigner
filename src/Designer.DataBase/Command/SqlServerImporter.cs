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
    /// ��ϵ���Ӽ��
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class SqlServerImporter : ConfigCommandBase<ProjectConfig>, IAutoRegister
    {
        /// <summary>
        /// ע�����
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
                Name = "����SQLSERVER���ݿ�",
                Image = Application.Current.Resources["img_open"] as ImageSource
            };
        }
        
        /// <summary>
        /// �������ݿ�
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
        /// �������ݿ�
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
        /// �������ݿ����
        /// </summary>
        /// <param name="status">״̬</param>
        /// <param name="ex">�쳣</param>
        /// <param name="cnt">����ı������</param>
        internal void EndImportSqlServer(CommandStatus status, Exception ex, int cnt)
        {
            if (status == CommandStatus.Succeed)
            {
                MessageBox.Show($"�ɹ�����{cnt}����");
            }
            else
            {
                MessageBox.Show($"����ʧ��");
            }

        }
    }
}