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
    /// ����ע����
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class SqlServerImporter : IAutoRegister
    {
        /// <summary>
        /// ע�����
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            CommandCoefficient.RegisterCommand(new CommandItemBuilder<ProjectConfig, int>(BeginImportSqlServer, DoImportSqlServer, EndImportSqlServer)
            {
                Caption = "����SqlServer���ݿ�",
                Catalog = "���ݿ�",
                IconName = "tree_Assembly"
            });
        }
        
        /// <summary>
        /// �������ݿ�
        /// </summary>
        /// <returns></returns>
        public bool BeginImportSqlServer(ProjectConfig _)
        {
            if (DataModelDesignModel.Current.Context.SelectProject == null)
            {
                MessageBox.Show("��ѡ��һ����Ŀ����ȷ����������Ϣ�����");
                return false;
            }
            if (MessageBox.Show($"ȷ���ڡ�{DataModelDesignModel.Current.Context.SelectProject.Caption}����ִ�С�����SQLSERVER���ݿ⡿������?",
                          "����༭", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return false;

            DataModelDesignModel.Current.Editor.ShowTrace();
            return true;
        }

        /// <summary>
        /// �������ݿ�
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
        /// �������ݿ����
        /// </summary>
        /// <param name="status">״̬</param>
        /// <param name="ex">�쳣</param>
        /// <param name="cnt">����ı������</param>
        internal void EndImportSqlServer(CommandStatus status, Exception ex, int cnt)
        {
            MessageBox.Show(status == CommandStatus.Succeed ? $"�ɹ�����{cnt}����" : "����ʧ��");
        }
    }
}