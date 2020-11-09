namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 属性配置
    /// </summary>
    public interface ICppFieldConfig : IConfig
    {
        #region CPP

        /// <summary>
        /// 语言类型(C++)
        /// </summary>
        string CppType
        {
            get;
            set;
        }
        /// <summary>
        /// 字段名称(C++)
        /// </summary>
        string CppName
        {
            get;
            set;
        }
        /// <summary>
        /// 结果类型(C++)
        /// </summary>
        /// <remark>
        /// 最终生成C++代码时的字段类型
        /// </remark>
        string CppLastType
        {
            get;
            set;
        }
        /// <summary>
        /// C++字段类型
        /// </summary>
        object CppTypeObject
        {
            get;
            set;
        }

        #endregion

        #region 模型设计(C++)

        /// <summary>
        /// 私有字段
        /// </summary>
        /// <remark>
        /// 私有字段,不应该复制
        /// </remark>
        bool IsPrivateField { get; set; }// Field.IsPrivateField;

        /// <summary>
        /// 设计时字段
        /// </summary>
        /// <remark>
        /// 设计时使用的中间过程字段,即最终使用时不需要的字段
        /// </remark>
        bool IsMiddleField { get; set; }// Field.IsMiddleField;

        /// <summary>
        /// 6位小数的整数
        /// </summary>
        /// <remark>
        /// 是否转为整数的小数,即使用扩大100成倍的整数
        /// </remark>
        bool IsIntDecimal { get; }// Field.IsIntDecimal;

        #endregion
    }
}