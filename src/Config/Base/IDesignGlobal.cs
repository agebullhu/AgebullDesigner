using System.Collections.ObjectModel;

namespace Gboxt.Common.DataAccess.Schemas
{
    /// <summary>
    /// 表示设计器的全局对象
    /// </summary>
    public interface IDesignGlobal
    {
        /// <summary>
        ///     状态消息
        /// </summary>
        string StateMessage
        {
            get;
            set;
        }

        /// <summary>
        /// 应用根目录
        /// </summary>
        string ProgramRoot { get; }

        /// <summary>
        /// 全局公用解决方案
        /// </summary>
        SolutionConfig GlobalSolution { get; set; }

        /// <summary>
        /// 当前解决方案
        /// </summary>
        SolutionConfig CurrentSolution { get; set; }

        /// <summary>
        /// 当前选择
        /// </summary>
        ConfigBase CurrentConfig { get; set; }

        /// <summary>
        ///     解决方案集合
        /// </summary>
        ObservableCollection<SolutionConfig> Solutions { get; }

        /// <summary>
        ///     枚举集合
        /// </summary>
        ObservableCollection<EnumConfig> Enums { get; }

        /// <summary>
        ///     类型(C++)集合
        /// </summary>
        ObservableCollection<TypedefItem> TypedefItems { get; }

        /// <summary>
        ///     实体集合
        /// </summary>
        ObservableCollection<EntityConfig> Entities { get; }

        /// <summary>
        ///     项目集合
        /// </summary>
        ObservableCollection<ProjectConfig> Projects { get; }

        /// <summary>
        ///     API集合
        /// </summary>
        ObservableCollection<ApiItem> ApiItems { get; }

        /// <summary>
        ///     通知集合
        /// </summary>
        ObservableCollection<NotifyItem> NotifyItems { get; }
    }
}