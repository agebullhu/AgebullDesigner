// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Gboxt.Common.WpfMvvmBase
// 建立:2014-11-27
// 修改:2014-11-29
// *****************************************************/

#region 引用

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;

#endregion

namespace Agebull.Common.DataModel
{
    /// <summary>
    /// 一个跟踪消息的类
    /// </summary>
    public sealed class TraceMessage : NotificationObject
    {

        private Dictionary<int, int> threadIndex = new Dictionary<int, int>();

        private readonly ObservableCollection<string> _trace = new ObservableCollection<string>();

        private bool _doTrace = true;

        private string _message1;

        private string _message2;

        private string _message3;

        private string _message4;

        private string _status;

        private static TraceMessage _message;

        /// <summary>
        ///     缺省跟踪消息
        /// </summary>
        public static TraceMessage DefaultTrace => _message ?? (_message = new TraceMessage());

        /// <summary>
        /// 消息是否写入跟踪信息中
        /// </summary>
        public bool MessageToTrace
        {
            get
            {
                return _doTrace;
            }
            set
            {
                if (_doTrace == value)
                {
                    return;
                }
                _doTrace = value;
                RaisePropertyChanged(() => MessageToTrace);
            }
        }
        /// <summary>
        /// 1级消息
        /// </summary>
        public string Message1
        {
            get
            {
                return _message1;
            }
            set
            {
                if (MessageToTrace) WriteTrace($"【{value}】");
                if (Equals(_message1, value))
                {
                    return;
                }
                _message1 = value;
                RaisePropertyChanged(() => Message1);
            }
        }
        /// <summary>
        /// 2级消息
        /// </summary>
        public string Message2
        {
            get
            {
                return _message2;
            }
            set
            {
                if (MessageToTrace) WriteTrace($"〖{value}〗");
                if (Equals(_message2, value))
                {
                    return;
                }
                _message2 = value;
                RaisePropertyChanged(() => Message2);
            }
        }
        /// <summary>
        /// 3级消息
        /// </summary>
        public string Message3
        {
            get
            {
                return _message3;
            }
            set
            {
                if (MessageToTrace) WriteTrace($"［{value}］");
                if (Equals(_message3, value))
                {
                    return;
                }
                _message3 = value;
                RaisePropertyChanged(() => Message3);
            }
        }

        /// <summary>
        /// 4级消息
        /// </summary>
        public string Message4
        {
            get
            {
                return _message4;
            }
            set
            {
                if (MessageToTrace) WriteTrace($"〈{value}〉");
                if (Equals(_message4, value))
                {
                    return;
                }
                _message4 = value;
                RaisePropertyChanged(() => Message4);
            }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                WriteTrace(
                    $@"『详细』
“{value
                        }”
---------------------------------------------------------------------------------------------", true);
                if (Equals(_status, value))
                {
                    return;
                }
                _status = value;
                RaisePropertyChanged(() => Status);
            }
        }

        /// <summary>
        ///  跟踪消息
        /// </summary>
        public IEnumerable<string> Tracks => _trace;

        /// <summary>
        ///  跟踪消息(它是直接写入跟踪内容的,即写入时是一个消息,返回的是所有跟踪消息)
        /// </summary>
        public string Track
        {
            get
            {
                lock (this)
                {
                    return _trace.LinkToString("\r\n");
                }
            }
            set
            {
                lock (this)
                    WriteTrace(value, ShowTime);
            }
        }

        /// <summary>
        ///  跟踪消息(它是直接写入跟踪内容的,即写入时是一个消息,返回的是所有跟踪消息)
        /// </summary>
        public bool ShowTime
        {
            get;
            set;
        }

        private string _selectLine;
        /// <summary>
        /// 状态
        /// </summary>
        public string SelectLine
        {
            get
            {
                return _selectLine;
            }
            set
            {
                if (Equals(_selectLine, value))
                {
                    return;
                }
                _selectLine = value;
                RaisePropertyChanged(() => SelectLine);
            }
        }
        private void WriteTrace(string message, bool time = false)
        {
#if CLIENT
            BeginInvokeInUiThread(p =>
            {
                if (threadIndex.ContainsKey(Thread.CurrentThread.ManagedThreadId))
                {
                    var idx = threadIndex[Thread.CurrentThread.ManagedThreadId] + 1;
                    if (idx >= _trace.Count)
                    {
                        _trace.Add(time ? $"{DateTime.Now}:{p}" : p);
                        threadIndex[Thread.CurrentThread.ManagedThreadId] = _trace.Count;
                    }
                    else
                    {
                        _trace.Insert(idx, time ? $"{DateTime.Now}:{p}" : p);
                        threadIndex[Thread.CurrentThread.ManagedThreadId] = idx;
                    }
                }
                else
                {
                    _trace.Add(time ? $"{DateTime.Now}:{p}" : p);
                    threadIndex.Add(Thread.CurrentThread.ManagedThreadId, _trace.Count - 1);
                }
                RaisePropertyChanged(() => Track);
                LastMessageIndex = _trace.Count - 1;
            }, message);
#else
            if (threadIndex.ContainsKey(Thread.CurrentThread.ManagedThreadId))
            {
                var idx = threadIndex[Thread.CurrentThread.ManagedThreadId] + 1;
                if (idx >= this._trace.Count)
                {
                    this._trace.Add(time ? string.Format("{0}:{1}", DateTime.Now, p) : p);
                    threadIndex[Thread.CurrentThread.ManagedThreadId] = this._trace.Count;
                }
                else
                {
                    this._trace.Insert(idx, time ? string.Format("{0}:{1}", DateTime.Now, p) : p);
                    threadIndex[Thread.CurrentThread.ManagedThreadId] = idx;
                }
            }
            else
            {
                this._trace.Add(time ? string.Format("{0}:{1}", DateTime.Now, p) : p);
                threadIndex.Add(Thread.CurrentThread.ManagedThreadId, this._trace.Count - 1);
            }
            this.RaisePropertyChanged(() => this.Track); ;
#endif
        }

        private int _lastMessageIndex;
        /// <summary>
        /// 最后一条消息ID
        /// </summary>
        public int LastMessageIndex
        {
            get
            {
                return _lastMessageIndex;
            }
            set
            {
                if (Equals(_lastMessageIndex, value))
                {
                    return;
                }
                _lastMessageIndex = value;
                RaisePropertyChanged(() => LastMessageIndex);
            }
        }
        /// <summary>
        /// 清理
        /// </summary>
        public void Clear()
        {
            Message1 = null;
            Message2 = null;
            Message3 = null;
            Message4 = null;
            Status = null;
            _trace.Clear();
            RaisePropertyChanged(() => Track);
        }

        #region 扩展方法

        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="level">消息等级</param>
        /// <param name="message">消息</param>
        public void ShowMessage(int level, object message)
        {
            ShowMessage(level, message == null ? null : message.ToString());
        }

        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="level">消息等级</param>
        /// <param name="message">消息</param>
        public void ShowMessage(int level, string message)
        {
            switch (level)
            {
                case 1:
                    Message1 = message;
                    break;
                case 2:
                    Message2 = message;
                    break;
                case 3:
                    Message3 = message;
                    break;
                case 4:
                    Message4 = message;
                    break;
                default:
                    Status = message;
                    break;
            }
        }
        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="level">消息等级</param>
        /// <param name="message">消息</param>
        /// <param name="args">消息格式化参数</param>
        public void ShowMessage(int level, string message, params object[] args)
        {
            ShowMessage(level, string.Format(message, args));
        }

        #endregion
    }
}
