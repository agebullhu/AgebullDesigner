namespace Agebull.EntityModel.Config
{
    public interface IEnumFieldConfig : IConfig
    {
        /// <summary>
        /// 非基本类型名称(C#)
        /// </summary>
        /// <remark>
        string CustomType { get; set; }// Field.CustomType;

        /// <summary>
        /// 对应枚举
        /// </summary>
        /// <remark>
        /// 当使用自定义类型时的枚举对象
        /// </remark>
        string EnumKey { get; set; }// Field.EnumKey;

        /// <summary>
        /// 对应枚举
        /// </summary>
        /// <remark>
        /// 当使用自定义类型时的枚举对象
        /// </remark>
        EnumConfig EnumConfig { get; set; }// Field.EnumConfig;

    }
}