using Agebull.Common.Mvvm;
using Agebull.EntityModel;
using Agebull.EntityModel.Config;
using System.Windows;
using System.Windows.Media;

namespace Agebull.CodeRefactor.CodeRefactor
{
    /// <summary>
    /// 消息跟踪模型
    /// </summary>
    public sealed class TraceModel : ModelBase
    {

        private CommandItemBase _clearTraceCommand;

        private CommandItemBase _copyTraceCommand;

        private TraceMessage _traceMessage;



        /// <summary>
        /// 当前跟踪消息
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
        /// 消息是否写入跟踪信息中
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
        /// 消息是否写入跟踪信息中
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
            Caption = "清除跟踪信息",
            IconName = "清除"
        };


        private void ClearTrace(object arg)
        {
            TraceMessage.Clear();
        }

        public CommandItemBase CopyTraceCommand => _copyTraceCommand ??= new CommandItem
        {
            Action = CopyTrace,
            Caption = "复制跟踪信息",
            IconName = "复制"
        };

        public CommandItemBase ShowDefaultMessageCommand => new CommandItem
        {
            Action = (arg) =>
            {
                TraceMessage = TraceMessage == TraceMessage.DefaultTrace
                    ? TreeRoot.Root?.Extend.DependencyObjects.AutoDependency<TraceMessage>()
                    : TraceMessage.DefaultTrace;
            },
            Caption = "切换到全局消息",
            IconName = "切换"
        };

        private void CopyTrace(object arg)
        {
            if (!string.IsNullOrWhiteSpace(TraceMessage.Track))
                Clipboard.SetText(TraceMessage.Track);
        }
    }
}