#if CLIENT
namespace Agebull.Common.DataModel
{
    /// <summary>
    /// ʹ�õ������Ļ���
    /// </summary>
    public static class WorkContext
    {
        /// <summary>
        /// ͬ��������
        /// </summary>
        public static ISynchronousContext SynchronousContext
        {
            get; set;
        }
    }
}
#endif