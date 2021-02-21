namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 关系模型类型
    /// </summary>
    public enum ReleationModelType
    {
        /// <summary>
        /// 扩展属性（1：1）
        /// </summary>
        ExtensionProperty,
        /// <summary>
        /// 实体属性（1：1）
        /// </summary>
        EntityProperty,
        /// <summary>
        /// 子集列表（1：n）
        /// </summary>
        Children,
        /// <summary>
        /// 自定义
        /// </summary>
        Custom
    }
}