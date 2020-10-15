namespace Agebull.EntityModel.RobotCoder.CodeTemplate
{
    /// <summary>
    /// 模板内容节点类型
    /// </summary>
    public enum TemplateUnitType
    {
        None,
        /// <summary>
        /// 文档注释
        /// </summary>
        Rem,
        /// <summary>
        /// 文档配置
        /// </summary>
        Config,
        /// <summary>
        /// 转义字符
        /// </summary>
        Sharp,
        /// <summary>
        /// 内容文本
        /// </summary>
        Content,
        /// <summary>
        /// 简单内容值(无括号)
        /// </summary>
        SimpleValue,
        /// <summary>
        /// 内容值(括号内)
        /// </summary>
        Value,
        /// <summary>
        /// 普通代码
        /// </summary>
        Code
    }
}