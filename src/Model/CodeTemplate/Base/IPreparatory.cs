namespace Agebull.CodeRefactor
{
    /// <summary>
    /// 预处理对象
    /// </summary>
    public interface IPreparatory
    {
        /// <summary>
        /// 键
        /// </summary>
        bool IsReady
        {
            get;
            set;
        }
    }
}