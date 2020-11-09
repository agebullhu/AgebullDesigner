namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 属性配置
    /// </summary>
    public interface IRelationFieldConfig : IConfig
    {
        #region 数据关联

        /// <summary>
        /// 连接字段
        /// </summary>
        bool IsLinkField { get; set; }// Field.IsLinkField;

        /// <summary>
        /// 关联表名
        /// </summary>
        string LinkTable { get; set; }// Field.LinkTable;

        /// <summary>
        /// 关联表主键
        /// </summary>
        /// <remark>
        /// 关联表主键,即与另一个实体关联的外键
        /// </remark>
        bool IsLinkKey { get; set; }// Field.IsLinkKey;

        /// <summary>
        /// 关联表标题
        /// </summary>
        /// <remark>
        /// 关联表标题,即此字段为关联表的标题内容
        /// </remark>
        bool IsLinkCaption { get; set; }// Field.IsLinkCaption;

        /// <summary>
        /// 对应客户ID
        /// </summary>
        /// <remark>
        /// 是对应的UID,已过时,原来用于龙之战鼓
        /// </remark>
        bool IsUserId { get; set; }// Field.IsUserId;

        /// <summary>
        /// 关联字段名称
        /// </summary>
        /// <remark>
        /// 关联字段名称,即在关联表中的字段名称
        /// </remark>
        string LinkField { get; set; }// Field.LinkField;
        #endregion
    }
}