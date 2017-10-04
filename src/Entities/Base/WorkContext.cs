#if CLIENT
namespace Agebull.Common.DataModel
{
    /// <summary>
    /// 使用的上下文汇总
    /// </summary>
    public static class WorkContext
    {
        /// <summary>
        /// 同步上下文
        /// </summary>
        public static ISynchronousContext SynchronousContext
        {
            get; set;
        }
    }
}
#endif