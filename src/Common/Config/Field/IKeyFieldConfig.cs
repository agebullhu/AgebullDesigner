namespace Agebull.EntityModel.Config
{
    public interface IKeyFieldConfig : IConfig
    {
        /// <summary>
        /// 标题字段
        /// </summary>
        bool IsCaption { get; set; }// Field.IsCaption;

        /// <summary>
        /// 主键字段
        /// </summary>
        bool IsPrimaryKey { get; set; }// Field.IsPrimaryKey;

        /// <summary>
        /// 唯一值字段
        /// </summary>
        /// <remark>
        /// 即它也是唯一标识符,如用户的身份证号
        /// </remark>
        bool IsExtendKey { get; set; }// Field.IsExtendKey;

        /// <summary>
        /// 全局标识
        /// </summary>
        /// <remark>
        /// 是否使用GUID的全局KEY
        /// </remark>
        bool IsGlobalKey { get; set; }// Field.IsGlobalKey;

        /// <summary>
        /// 唯一属性组合顺序
        /// </summary>
        /// <remark>
        /// 参与组合成唯一属性的顺序,大于0有效
        /// </remark>
        bool UniqueIndex { get; set; }// Field.UniqueIndex;

        /// <summary>
        /// 唯一文本
        /// </summary>
        bool UniqueString { get; set; }// Field.UniqueString;

    }
}