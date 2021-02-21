namespace Agebull.EntityModel.Config
{
    /// <summary>
    ///     数据库代码风格
    /// </summary>
    public interface IDatabaseCodeStyle : ICodeStyle
    {
        /// <summary>
        /// 数据库
        /// </summary>
        DataBaseType DataBase { get; }

        /// <summary>
        /// 格式化表名
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        string FormatTableName(IEntityConfig entity);

        /// <summary>
        /// 格式化视图名称
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        string FormatViewName(IEntityConfig entity);

        /// <summary>
        /// 格式化字段名
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        string FormatFieldName(IPropertyConfig field);
    }
}
