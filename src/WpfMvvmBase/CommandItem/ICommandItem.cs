namespace Agebull.Common.Mvvm
{
    /// <summary>
    /// 表示一个命令生成器
    /// </summary>
    public interface ICommandItem
    {
        /// <summary>
        ///     不显示为按钮
        /// </summary>
        bool NoButton
        {
            get;
        }

        /// <summary>
        ///     分类
        /// </summary>
        string Catalog
        {
            get;
            set;
        }
        /// <summary>
        ///     只能单个操作
        /// </summary>
        bool Signle
        {
            get;
        }
        /// <summary>
        ///     目标类型
        /// </summary>
        string SourceType
        {
            get;
            set;
        }
        /// <summary>
        ///     图标
        /// </summary>
        string IconName
        {
            get;
        }
    }
}