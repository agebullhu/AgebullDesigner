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
    /// ��ϵ���Ӽ��
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class MySqlImporter : ConfigCommandBase<SolutionConfig>, IAutoRegister
    {
        /// <summary>
        /// ע�����
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            NoButton = true;
            Signle = true;
            CommandCoefficient.RegisterCommand<SolutionConfig, MySqlImporter>();
        }


        public override CommandItem ToCommand(object arg, Func<object, IEnumerator> enumerator = null)
        {
            return new CommandItem
            {
                NoButton = true,
                Signle = true,
                SourceType = typeof(SolutionConfig).Name,
                Command = new AsyncCommand<string, string>(ImportStructParpare, ImportStruct, ImportStructEnd),
                Name = "����MySql���ݿ�",
                Image = Application.Current.Resources["tree_Assembly"] as ImageSource
            };
        }

        public bool ImportStructParpare(string arg, Action<string> setAction)
        {
            var ctx = DataModelDesignModel.Current.Context;
            ctx.NowJob = DesignContext.JobTrace;
            return MessageBox.Show("ȷ��ִ�С�����MySql���ݿ⡿������?", "����༭", MessageBoxButton.YesNo) == MessageBoxResult.Yes;
        }


        /// <summary>
        ///     ����MySql���ݿ�
        /// </summary>
        /// <returns></returns>
        internal string ImportStruct(string arg)
        {
            var ctx = DataModelDesignModel.Current.Context;
            new MySqlImport().Import(ctx.CurrentTrace.TraceMessage, ctx.Solution, DataModelDesignModel.Current.Dispatcher);
            return string.Empty;
        }

        internal void ImportStructEnd(CommandStatus status, Exception ex, string code)
        {
            DataModelDesignModel.Current.Context.NowJob = DesignContext.JobTrace;
            if (ex != null)
                DataModelDesignModel.Current.Context.CurrentTrace.TraceMessage.Track = ex.ToString();
        }
    }
}