namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 属性配置
    /// </summary>
    public interface IChildrenConfig
    {
        /// <summary>
        /// 上级
        /// </summary>
        ConfigBase Parent { get; set; }
    }

}