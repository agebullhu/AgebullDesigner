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
        /// ��ʾ��Ϣ
        /// </summary>
        /// <param name="level">��Ϣ�ȼ�</param>
        /// <param name="message">��Ϣ</param>
        public void ShowMessage(int level, string message)
        {
            if (Messager != null)
                Messager.ShowMessage(level, message);
        }
    }
}