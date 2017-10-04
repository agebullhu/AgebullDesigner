using Agebull.Common.DataModel;

namespace Agebull.CodeRefactor
{
    public class AnalyzerBase
    {
        private TraceMessage _messager;

        public TraceMessage Messager
        {
            set
            {
                this._messager = value;
            }
            get
            {
                return this._messager;
            }
        }
        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="level">消息等级</param>
        /// <param name="message">消息</param>
        public void ShowMessage(int level, string message)
        {
            if (Messager != null)
                Messager.ShowMessage(level, message);
        }
    }
}