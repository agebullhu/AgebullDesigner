using System.Windows;
using System.Windows.Media;
using Agebull.EntityModel;
using Agebull.Common.Mvvm;

namespace Agebull.CodeRefactor.CodeRefactor
{
    /// <summary>
    /// ��Ϣ����ģ��
    /// </summary>
    public sealed class TraceModel : ModelBase
    {

        private CommandItem _clearTraceCommand;

        private CommandItem _copyTraceCommand;

        private TraceMessage _traceMessage;

        

        /// <summary>
        /// ��ǰ������Ϣ
        /// </summary>
        public TraceMessage TraceMessage
        {
            get
            {
                return _traceMessage ?? TraceMessage.DefaultTrace;
            }
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
            get
            {
                return TraceMessage.MessageToTrace;
            }
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

        public CommandItem ClearTraceCommand => _clearTraceCommand ?? (_clearTraceCommand = new CommandItem
        {
            Command = new DelegateCommand(ClearTrace),
            Name = "���������Ϣ",
            Image = Application.Current.Resources["tree_Close"] as ImageSource
        });


        private void ClearTrace()
        {
            TraceMessage.Clear();
        }

        public CommandItem CopyTraceCommand => _copyTraceCommand ?? (_copyTraceCommand = new CommandItem
        {
            Command = new DelegateCommand(CopyTrace),
            Name = "���Ƹ�����Ϣ",
            Image = Application.Current.Resources["tree_Close"] as ImageSource
        });

        public CommandItem ShowDefaultMessageCommand
        {
            get
            {
                return (new CommandItem
                {
                    Command = new DelegateCommand(() => TraceMessage = TraceMessage.DefaultTrace),
                    Name = "�л���ȫ����Ϣ",
                    Image = Application.Current.Resources["tree_default"] as ImageSource
                });
            }
        }

        private void CopyTrace()
        {
            if (!string.IsNullOrWhiteSpace(TraceMessage.Track))
                Clipboard.SetText(TraceMessage.Track);
        }
    }
}