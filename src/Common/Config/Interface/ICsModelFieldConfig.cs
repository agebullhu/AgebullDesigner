namespace Agebull.EntityModel.Config
{
    public interface ICsModelFieldConfig : IConfig
    {
        #region 扩散信息


        /// <summary>
        /// 初始值
        /// </summary>
        /// <remark>
        /// 3初始值,原样写入代码,如果是文本,需要加引号
        /// </remark>
        string Initialization
        {
            get;
            set;
        }
        #endregion
        #region 数据类型


        /// <summary>
        /// 数据类型
        /// </summary>
        string DataType
        {
            get;
            set;
        }

        /// <summary>
        /// 语言类型(C#)
        /// </summary>
        string CsType
        {
            get;
            set;
        }

        /// <summary>
        /// 参考类型
        /// </summary>
        string ReferenceType { get; set; }// Field.ReferenceType;

        /// <summary>
        /// 结果类型(C#)
        /// </summary>
        /// <remark>
        /// 最终生成C#代码时的属性类型
        /// </remark>
        string LastCsType { get; }// Field.ToLastCsType();

        /// <summary>
        /// 可空类型(C#)
        /// </summary>
        /// <remark>
        /// 即生成的C#代码,类型为空类型Nullable〈T〉 ,如int?
        /// </remark>
        bool Nullable { get; set; }// Field.Nullable;
        #endregion
    }
}