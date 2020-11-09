namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 属性配置
    /// </summary>
    public interface IFieldRuleConfig : IConfig
    {
        #region 数据规则

        /// <summary>
        /// 规则说明
        /// </summary>
        string DataRuleDesc { get; set; }// Field.DataRuleDesc;

        /// <summary>
        /// 规则说明
        /// </summary>
        string AutoDataRuleDesc { get; }// Field.AutoDataRuleDesc;

        /// <summary>
        /// 校验代码
        /// </summary>
        /// <remark>
        /// 校验代码,本字段用{0}代替
        /// </remark>
        string ValidateCode { get; set; }// Field.ValidateCode;

        /// <summary>
        /// 能否为空
        /// </summary>
        /// <remark>
        /// 这是数据相关的逻辑,表示在存储时必须写入数据,否则逻辑不正确
        /// </remark>
        bool CanEmpty { get; set; }// Field.CanEmpty;

        /// <summary>
        /// 必填字段
        /// </summary>
        bool IsRequired { get; set; }// Field.IsRequired;

        /// <summary>
        /// 最大值
        /// </summary>
        string Max { get; set; }// Field.Max;

        /// <summary>
        /// 最小值
        /// </summary>
        string Min { get; set; }// Field.Min;
        #endregion
    }
}