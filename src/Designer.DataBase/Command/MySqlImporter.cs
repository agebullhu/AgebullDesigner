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
    internal sealed class MySqlImporter : IAutoRegister
    {
        /// <summary>
        /// ע�����
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            CommandCoefficient.RegisterCommand(new CommandItemBuilder<ProjectConfig, string>(ImportStructParpare, ImportStruct, ImportStructEnd)
            {
                Caption = "����MySql���ݿ�",
                Catalog = "���ݿ�",
                WorkView="database",
                IconName = "tree_Assembly"
            });
        }

        public bool ImportStructParpare(ProjectConfig arg)
        {
            if (DataModelDesignModel.Current.Context.SelectProject == null)
            {
                MessageBox.Show("��ѡ��һ����Ŀ����ȷ����������Ϣ�����");
                return false;
            }
            if(MessageBox.Show($"ȷ���ڡ�{DataModelDesignModel.Current.Context.SelectProject.Caption}����ִ�С�����MySql���ݿ⡿������?",
                           "����༭", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return false;
            DataModelDesignModel.Current.Editor.ShowTrace();
            return true;
        }


        /// <summary>
        ///     ����MySql���ݿ�
        /// </summary>
        /// <returns></returns>
        internal string ImportStruct(ProjectConfig arg)
        {
            var ctx = DataModelDesignModel.Current.Context;
            new MySqlImport().Import(ctx.CurrentTrace.TraceMessage, ctx.SelectProject, DataModelDesignModel.Current.Dispatcher);
            return string.Empty;
        }

        internal void ImportStructEnd(CommandStatus status, Exception ex, string code)
        {
            DataModelDesignModel.Current.Editor.ShowTrace();
            if (ex != null)
                DataModelDesignModel.Current.Context.CurrentTrace.TraceMessage.Track = ex.ToString();
        }
    }
}