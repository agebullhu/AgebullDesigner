namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 用户界面的字段设置
    /// </summary>
    public interface IUIFieldConfig
    {
        /// <summary>
        /// 用户可见
        /// </summary>
        bool UserSee { get;  }
        /// <summary>
        /// 用户是否可查询
        /// </summary>
        bool CanUserQuery { get; set; }
        /// <summary>
        /// 不可编辑
        /// </summary>/// <remarks>
        /// 是否用户可编辑
        /// </remarks>
        bool IsUserReadOnly { get; set; }
        /// <summary>
        /// 多行文本
        /// </summary>
        bool MulitLine { get; set; }
        /// <summary>
        /// 多行文本的行数
        /// </summary>/// <remarks>
        /// 默认为3行
        /// </remarks>
        int Rows { get; set; }
        /// <summary>
        /// 前缀
        /// </summary>
        string Prefix { get; set; }
        /// <summary>
        /// 后缀
        /// </summary>
        string Suffix { get; set; }
        /// <summary>
        /// 等同于空值的文本
        /// </summary>/// <remarks>
        /// 等同于空值的文本,多个用#号分开
        /// </remarks>
        string EmptyValue { get; set; }
        /// <summary>
        /// 界面必填字段
        /// </summary>
        bool UiRequired { get; set; }
        /// <summary>
        /// 输入类型
        /// </summary>
        string InputType { get; set; }
        /// <summary>
        /// Form中占几列宽度
        /// </summary>
        int FormCloumnSapn { get; set; }
        /// <summary>
        /// Form中的设置
        /// </summary>
        string FormOption { get; set; }
        /// <summary>
        /// 用户排序
        /// </summary>
        bool UserOrder { get; set; }
        /// <summary>
        /// 下拉列表的地址
        /// </summary>
        string ComboBoxUrl { get; set; }
        /// <summary>
        /// 是否图片
        /// </summary>
        bool IsImage { get; set; }
        /// <summary>
        /// 货币类型
        /// </summary>
        bool IsMoney { get; set; }
        /// <summary>
        /// 表格对齐
        /// </summary>
        string GridAlign { get; set; }
        /// <summary>
        /// 占表格宽度比例
        /// </summary>
        int GridWidth { get; set; }
        /// <summary>
        /// 数据格式器
        /// </summary>
        string DataFormater { get; set; }
        /// <summary>
        /// 显示在列表详细页中
        /// </summary>
        bool GridDetails { get; set; }
        /// <summary>
        /// 列表不显示
        /// </summary>
        bool NoneGrid { get; set; }
        /// <summary>
        /// 详细不显示
        /// </summary>
        bool NoneDetails { get; set; }
        /// <summary>
        /// 列表详细页代码
        /// </summary>
        string GridDetailsCode { get; set; }
        /// <summary>
        /// 可导入
        /// </summary>
        bool CanImport { get; set; }
        /// <summary>
        /// 可导出
        /// </summary>
        bool CanExport { get; set; }
        /// <summary>
        /// 用户提示文本
        /// </summary>
        bool UserHint { get; set; }

        /// <summary>
        /// 是否时间
        /// </summary>
        bool IsTime { get; set; }
    }

}