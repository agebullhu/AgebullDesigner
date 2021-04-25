using Agebull.Common.Mvvm;
using Agebull.EntityModel;
using Agebull.EntityModel.Config;
using System.Windows;
using System.Windows.Media;

namespace Agebull.CodeRefactor.CodeRefactor
{
    /// <summary>
    /// ��Ϣ����ģ��
    /// </summary>
    public sealed class TraceModel : ModelBase
    {

        private CommandItemBase _clearTraceCommand;

        private CommandItemBase _copyTraceCommand;

        private TraceMessage _traceMessage;



        /// <summary>
        /// ��ǰ������Ϣ
        /// </summary>
        public TraceMessage TraceMessage
        {
            get => _traceMessage ?? TraceMessage.DefaultTrace;
            set
            {
                if (_traceMessage == value)
                {
                    return;
                }
                _traceMessage = value;
                RaisePropertyChanged(() => TraceMessage);
                RaisePropertyChanged(() => MessageToTrace);
            }
        }


        /// <summary>
        /// ��Ϣ�Ƿ�д�������Ϣ��
        /// </summary>
        public bool MessageToTrace
        {
            get => TraceMessage.MessageToTrace;
            set
            {
                if (Equals(TraceMessage.MessageToTrace, value))
                {
                    return;
                }
                TraceMessage.MessageToTrace = value;
                RaisePropertyChanged(() => MessageToTrace);
            }
        }
        /// <summary>
        /// ��Ϣ�Ƿ�д�������Ϣ��
        /// </summary>
        public bool DetailTrace
        {
            get => SolutionConfig.Current.DetailTrace;
            set
            {
                if (Equals(SolutionConfig.Current.DetailTrace, value))
                {
                    return;
                }
                SolutionConfig.Current.DetailTrace = value;
                RaisePropertyChanged(() => DetailTrace);
            }
        }
        
        public CommandItemBase ClearTraceCommand => _clearTraceCommand ??= new CommandItem
        {
            Action = ClearTrace,
            Caption = "���������Ϣ",
            IconName = "���"
        };


        private void ClearTrace(object arg)
        {
            TraceMessage.Clear();
        }

        public CommandItemBase CopyTraceCommand => _copyTraceCommand ??= new CommandItem
        {
            Action = CopyTrace,
            Caption = "���Ƹ�����Ϣ",
            IconName = "����"
        };

        public CommandItemBase ShowDefaultMessageCommand => new CommandItem
        {
            Action = (arg) =>
            {
                TraceMessage = TraceMessage == TraceMessage.DefaultTrace
                    ? TreeRoot.Root?.Extend.DependencyObjects.AutoDependency<TraceMessage>()
                    : TraceMessage.DefaultTrace;
            },
            Caption = "�л���ȫ����Ϣ",
            IconName = "�л�"
        };

        private void CopyTrace(object arg)
        {
            if (!string.IsNullOrWhiteSpace(TraceMessage.Track))
                Clipboard.SetText(TraceMessage.Track);
        }
    }
}