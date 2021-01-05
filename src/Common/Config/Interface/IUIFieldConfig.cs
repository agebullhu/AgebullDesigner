namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 属性配置
    /// </summary>
    public interface IUIFieldConfig: IConfig
    {
        #region 用户界面

        /// <summary>
        /// 用户是否可输入
        /// </summary>
        bool CanUserInput { get; }// !IsCompute && !DenyClient && !IsUserReadOnly && !IsSystemField && !IsIdentity;

        /// <summary>
        /// 不可编辑
        /// </summary>
        /// <remark>
        /// 是否用户可编辑
        /// </remark>
        bool IsUserReadOnly { get; set; }// Field.IsUserReadOnly;

        /// <summary>
        /// 多行文本
        /// </summary>
        bool MulitLine { get; set; }// Field.MulitLine;

        /// <summary>
        /// 前缀
        /// </summary>
        string Prefix { get; set; }// Field.Prefix;

        /// <summary>
        /// 后缀
        /// </summary>
        string Suffix { get; set; }// Field.Suffix;

        /// <summary>
        /// 等同于空值的文本
        /// </summary>
        /// <remark>
        /// 等同于空值的文本,多个用#号分开
        /// </remark>
        string EmptyValue { get; set; }// Field.EmptyValue;


        /// <summary>
        /// 界面必填字段
        /// </summary>
        bool UiRequired { get; set; }// Field.UiRequired;

        /// <summary>
        /// 输入类型
        /// </summary>
        string InputType { get; set; }// Field.InputType;

        /// <summary>
        /// Form中占几列宽度
        /// </summary>
        int FormCloumnSapn { get; set; }// Field.FormCloumnSapn;

        /// <summary>
        /// Form中的设置
        /// </summary>
        string FormOption { get; set; }// Field.FormOption;

        /// <summary>
        /// 用户排序
        /// </summary>
        bool UserOrder { get; set; }// Field.UserOrder;

        /// <summary>
        /// 下拉列表的地址
        /// </summary>
        string ComboBoxUrl { get; set; }// Field.ComboBoxUrl;

        /// <summary>
        /// 是否图片
        /// </summary>
        bool IsImage { get; set; }// Field.IsImage;

        /// <summary>
        /// 货币类型
        /// </summary>
        bool IsMoney { get; set; }// Field.IsMoney;

        /// <summary>
        /// 表格对齐
        /// </summary>
        string GridAlign { get; set; }// Field.GridAlign;

        /// <summary>
        /// 占表格宽度比例
        /// </summary>
        int GridWidth { get; set; }// Field.GridWidth;

        /// <summary>
        /// 数据格式器
        /// </summary>
        string DataFormater { get; set; }// Field.DataFormater;

        /// <summary>
        /// 显示在列表详细页中
        /// </summary>
        bool GridDetails { get; set; }// Field.GridDetails;

        /// <summary>
        /// 列表不显示
        /// </summary>
        bool NoneGrid { get; set; }// Field.NoneGrid;

        /// <summary>
        /// 详细不显示
        /// </summary>
        bool NoneDetails { get; set; }// Field.NoneDetails;

        /// <summary>
        /// 列表详细页代码
        /// </summary>
        string GridDetailsCode { get; set; }// Field.GridDetailsCode;
        #endregion
    }
}