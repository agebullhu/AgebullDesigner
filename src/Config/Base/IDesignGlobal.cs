namespace Agebull.EntityModel.Config
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
        /// 全局公用解决方案
        /// </summary>
        SolutionConfig LocalSolution { get; set; }

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
        NotificationList<SolutionConfig> Solutions { get; }

        /// <summary>
        ///     枚举集合
        /// </summary>
        NotificationList<EnumConfig> Enums { get; }

        /// <summary>
        ///     实体集合
        /// </summary>
        NotificationList<ModelConfig> Models { get; }

        /// <summary>
        ///     实体集合
        /// </summary>
        NotificationList<EntityConfig> Entities { get; }

        /// <summary>
        ///     项目集合
        /// </summary>
        NotificationList<ProjectConfig> Projects { get; }

        /// <summary>
        ///     API集合
        /// </summary>
        NotificationList<ApiItem> ApiItems { get; }

    }
}