using Agebull.EntityModel.Config;

namespace Agebull.Common.Mvvm
{
    /// <summary>
    /// 表示一个命令生成器
    /// </summary>
    public class CommandConfig : SimpleConfig, ICommandItem
    {
        /// <summary>
        ///     不显示为按钮
        /// </summary>
        public bool NoButton
        {
            get;
            set;
        }

        /// <summary>
        ///     图标
        /// </summary>
        public string IconName
        {
            get;
            set;
        }

        /// <summary>
        ///     只能单个操作
        /// </summary>
        public bool Signle
        {
            get;
            set;
        }

        /// <summary>
        ///     分类
        /// </summary>
        public string Catalog
        {
            get;
            set;
        }
        /// <summary>
        ///     目标类型
        /// </summary>
        public string SourceType
        {
            get;
            set;
        }

    }
}