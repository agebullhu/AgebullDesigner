using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config.V2021
{
    /// <summary>
    /// 用户界面的字段设置
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, ItemNullValueHandling = NullValueHandling.Ignore)]
    public partial class UserInterfaceField : FieldExtendConfig, IUIFieldConfig
    {
        #region 字段

        /// <summary>
        /// 用户可见
        /// </summary>
        [DataMember, JsonProperty("userSee", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _userSee;

        /// <summary>
        /// 用户可见
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"用户可见"), Description(@"用户可见")]
        public bool UserSee
        {
            get => _userSee;
            set
            {
                if (_userSee == value)
                    return;
                BeforePropertyChanged(nameof(UserSee), _userSee, value);
                _userSee = value;
                OnPropertyChanged(nameof(UserSee));
            }
        }

        /// <summary>
        /// 用户是否可查询
        /// </summary>
        [DataMember, JsonProperty("canUserQuery", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _canUserQuery;

        /// <summary>
        /// 用户是否可查询
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"用户是否可查询"), Description(@"用户是否可查询")]
        public bool CanUserQuery
        {
            get => _canUserQuery;
            set
            {
                if (_canUserQuery == value)
                    return;
                BeforePropertyChanged(nameof(CanUserQuery), _canUserQuery, value);
                _canUserQuery = value;
                OnPropertyChanged(nameof(CanUserQuery));
            }
        }

        /// <summary>
        /// 不可编辑
        /// </summary>/// <remarks>
        /// 是否用户可编辑
        /// </remarks>
        [DataMember, JsonProperty("isUserReadOnly", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isUserReadOnly;

        /// <summary>
        /// 不可编辑
        /// </summary>/// <remarks>
        /// 是否用户可编辑
        /// </remarks>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"不可编辑"), Description(@"是否用户可编辑")]
        public bool IsUserReadOnly
        {
            get => _isUserReadOnly;
            set
            {
                if (_isUserReadOnly == value)
                    return;
                BeforePropertyChanged(nameof(IsUserReadOnly), _isUserReadOnly, value);
                _isUserReadOnly = value;
                OnPropertyChanged(nameof(IsUserReadOnly));
            }
        }

        /// <summary>
        /// 多行文本
        /// </summary>
        [DataMember, JsonProperty("mulitLine", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _mulitLine;

        /// <summary>
        /// 多行文本
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"多行文本"), Description(@"多行文本")]
        public bool MulitLine
        {
            get => _mulitLine;
            set
            {
                if (_mulitLine == value)
                    return;
                BeforePropertyChanged(nameof(MulitLine), _mulitLine, value);
                _mulitLine = value;
                OnPropertyChanged(nameof(MulitLine));
            }
        }

        /// <summary>
        /// 多行文本的行数
        /// </summary>/// <remarks>
        /// 默认为3行
        /// </remarks>
        [DataMember, JsonProperty("rows", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal int _rows;

        /// <summary>
        /// 多行文本的行数
        /// </summary>/// <remarks>
        /// 默认为3行
        /// </remarks>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"多行文本的行数"), Description(@"默认为3行")]
        public int Rows
        {
            get => _rows;
            set
            {
                if (_rows == value)
                    return;
                BeforePropertyChanged(nameof(Rows), _rows, value);
                _rows = value;
                OnPropertyChanged(nameof(Rows));
            }
        }

        /// <summary>
        /// 前缀
        /// </summary>
        [DataMember, JsonProperty("prefix", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _prefix;

        /// <summary>
        /// 前缀
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"前缀"), Description(@"前缀")]
        public string Prefix
        {
            get => _prefix;
            set
            {
                if (_prefix == value)
                    return;
                BeforePropertyChanged(nameof(Prefix), _prefix, value);
                _prefix = value;
                OnPropertyChanged(nameof(Prefix));
            }
        }

        /// <summary>
        /// 后缀
        /// </summary>
        [DataMember, JsonProperty("suffix", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _suffix;

        /// <summary>
        /// 后缀
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"后缀"), Description(@"后缀")]
        public string Suffix
        {
            get => _suffix;
            set
            {
                if (_suffix == value)
                    return;
                BeforePropertyChanged(nameof(Suffix), _suffix, value);
                _suffix = value;
                OnPropertyChanged(nameof(Suffix));
            }
        }

        /// <summary>
        /// 等同于空值的文本
        /// </summary>/// <remarks>
        /// 等同于空值的文本,多个用#号分开
        /// </remarks>
        [DataMember, JsonProperty("emptyValue", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _emptyValue;

        /// <summary>
        /// 等同于空值的文本
        /// </summary>/// <remarks>
        /// 等同于空值的文本,多个用#号分开
        /// </remarks>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"等同于空值的文本"), Description(@"等同于空值的文本,多个用#号分开")]
        public string EmptyValue
        {
            get => _emptyValue;
            set
            {
                if (_emptyValue == value)
                    return;
                BeforePropertyChanged(nameof(EmptyValue), _emptyValue, value);
                _emptyValue = value;
                OnPropertyChanged(nameof(EmptyValue));
            }
        }

        /// <summary>
        /// 界面必填字段
        /// </summary>
        [DataMember, JsonProperty("uiRequired", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _uiRequired;

        /// <summary>
        /// 界面必填字段
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"界面必填字段"), Description(@"界面必填字段")]
        public bool UiRequired
        {
            get => _uiRequired;
            set
            {
                if (_uiRequired == value)
                    return;
                BeforePropertyChanged(nameof(UiRequired), _uiRequired, value);
                _uiRequired = value;
                OnPropertyChanged(nameof(UiRequired));
            }
        }

        /// <summary>
        /// 输入类型
        /// </summary>
        [DataMember, JsonProperty("inputType", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _inputType;

        /// <summary>
        /// 输入类型
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"输入类型"), Description(@"输入类型")]
        public string InputType
        {
            get => _inputType;
            set
            {
                if (_inputType == value)
                    return;
                BeforePropertyChanged(nameof(InputType), _inputType, value);
                _inputType = value;
                OnPropertyChanged(nameof(InputType));
            }
        }

        /// <summary>
        /// Form中占几列宽度
        /// </summary>
        [DataMember, JsonProperty("formCloumnSapn", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal int _formCloumnSapn;

        /// <summary>
        /// Form中占几列宽度
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"Form中占几列宽度"), Description(@"Form中占几列宽度")]
        public int FormCloumnSapn
        {
            get => _formCloumnSapn;
            set
            {
                if (_formCloumnSapn == value)
                    return;
                BeforePropertyChanged(nameof(FormCloumnSapn), _formCloumnSapn, value);
                _formCloumnSapn = value;
                OnPropertyChanged(nameof(FormCloumnSapn));
            }
        }

        /// <summary>
        /// Form中的设置
        /// </summary>
        [DataMember, JsonProperty("formOption", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _formOption;

        /// <summary>
        /// Form中的设置
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"Form中的设置"), Description(@"Form中的设置")]
        public string FormOption
        {
            get => _formOption;
            set
            {
                if (_formOption == value)
                    return;
                BeforePropertyChanged(nameof(FormOption), _formOption, value);
                _formOption = value;
                OnPropertyChanged(nameof(FormOption));
            }
        }

        /// <summary>
        /// 用户排序
        /// </summary>
        [DataMember, JsonProperty("userOrder", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _userOrder;

        /// <summary>
        /// 用户排序
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"用户排序"), Description(@"用户排序")]
        public bool UserOrder
        {
            get => _userOrder;
            set
            {
                if (_userOrder == value)
                    return;
                BeforePropertyChanged(nameof(UserOrder), _userOrder, value);
                _userOrder = value;
                OnPropertyChanged(nameof(UserOrder));
            }
        }

        /// <summary>
        /// 下拉列表的地址
        /// </summary>
        [DataMember, JsonProperty("comboBoxUrl", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _comboBoxUrl;

        /// <summary>
        /// 下拉列表的地址
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"下拉列表的地址"), Description(@"下拉列表的地址")]
        public string ComboBoxUrl
        {
            get => _comboBoxUrl;
            set
            {
                if (_comboBoxUrl == value)
                    return;
                BeforePropertyChanged(nameof(ComboBoxUrl), _comboBoxUrl, value);
                _comboBoxUrl = value;
                OnPropertyChanged(nameof(ComboBoxUrl));
            }
        }

        /// <summary>
        /// 是否图片
        /// </summary>
        [DataMember, JsonProperty("isImage", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isImage;

        /// <summary>
        /// 是否图片
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"是否图片"), Description(@"是否图片")]
        public bool IsImage
        {
            get => _isImage;
            set
            {
                if (_isImage == value)
                    return;
                BeforePropertyChanged(nameof(IsImage), _isImage, value);
                _isImage = value;
                OnPropertyChanged(nameof(IsImage));
            }
        }

        /// <summary>
        /// 货币类型
        /// </summary>
        [DataMember, JsonProperty("isMoney", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isMoney;

        /// <summary>
        /// 货币类型
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"货币类型"), Description(@"货币类型")]
        public bool IsMoney
        {
            get => _isMoney;
            set
            {
                if (_isMoney == value)
                    return;
                BeforePropertyChanged(nameof(IsMoney), _isMoney, value);
                _isMoney = value;
                OnPropertyChanged(nameof(IsMoney));
            }
        }

        /// <summary>
        /// 表格对齐
        /// </summary>
        [DataMember, JsonProperty("gridAlign", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _gridAlign;

        /// <summary>
        /// 表格对齐
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"表格对齐"), Description(@"表格对齐")]
        public string GridAlign
        {
            get => _gridAlign;
            set
            {
                if (_gridAlign == value)
                    return;
                BeforePropertyChanged(nameof(GridAlign), _gridAlign, value);
                _gridAlign = value;
                OnPropertyChanged(nameof(GridAlign));
            }
        }

        /// <summary>
        /// 占表格宽度比例
        /// </summary>
        [DataMember, JsonProperty("gridWidth", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal int _gridWidth;

        /// <summary>
        /// 占表格宽度比例
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"占表格宽度比例"), Description(@"占表格宽度比例")]
        public int GridWidth
        {
            get => _gridWidth;
            set
            {
                if (_gridWidth == value)
                    return;
                BeforePropertyChanged(nameof(GridWidth), _gridWidth, value);
                _gridWidth = value;
                OnPropertyChanged(nameof(GridWidth));
            }
        }

        /// <summary>
        /// 数据格式器
        /// </summary>
        [DataMember, JsonProperty("dataFormater", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _dataFormater;

        /// <summary>
        /// 数据格式器
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"数据格式器"), Description(@"数据格式器")]
        public string DataFormater
        {
            get => _dataFormater;
            set
            {
                if (_dataFormater == value)
                    return;
                BeforePropertyChanged(nameof(DataFormater), _dataFormater, value);
                _dataFormater = value;
                OnPropertyChanged(nameof(DataFormater));
            }
        }

        /// <summary>
        /// 显示在列表详细页中
        /// </summary>
        [DataMember, JsonProperty("gridDetails", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _gridDetails;

        /// <summary>
        /// 显示在列表详细页中
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"显示在列表详细页中"), Description(@"显示在列表详细页中")]
        public bool GridDetails
        {
            get => _gridDetails;
            set
            {
                if (_gridDetails == value)
                    return;
                BeforePropertyChanged(nameof(GridDetails), _gridDetails, value);
                _gridDetails = value;
                OnPropertyChanged(nameof(GridDetails));
            }
        }

        /// <summary>
        /// 列表不显示
        /// </summary>
        [DataMember, JsonProperty("noneGrid", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _noneGrid;

        /// <summary>
        /// 列表不显示
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"列表不显示"), Description(@"列表不显示")]
        public bool NoneGrid
        {
            get => _noneGrid;
            set
            {
                if (_noneGrid == value)
                    return;
                BeforePropertyChanged(nameof(NoneGrid), _noneGrid, value);
                _noneGrid = value;
                OnPropertyChanged(nameof(NoneGrid));
            }
        }

        /// <summary>
        /// 详细不显示
        /// </summary>
        [DataMember, JsonProperty("noneDetails", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _noneDetails;

        /// <summary>
        /// 详细不显示
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"详细不显示"), Description(@"详细不显示")]
        public bool NoneDetails
        {
            get => _noneDetails;
            set
            {
                if (_noneDetails == value)
                    return;
                BeforePropertyChanged(nameof(NoneDetails), _noneDetails, value);
                _noneDetails = value;
                OnPropertyChanged(nameof(NoneDetails));
            }
        }

        /// <summary>
        /// 列表详细页代码
        /// </summary>
        [DataMember, JsonProperty("gridDetailsCode", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _gridDetailsCode;

        /// <summary>
        /// 列表详细页代码
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"列表详细页代码"), Description(@"列表详细页代码")]
        public string GridDetailsCode
        {
            get => _gridDetailsCode;
            set
            {
                if (_gridDetailsCode == value)
                    return;
                BeforePropertyChanged(nameof(GridDetailsCode), _gridDetailsCode, value);
                _gridDetailsCode = value;
                OnPropertyChanged(nameof(GridDetailsCode));
            }
        }

        /// <summary>
        /// 可导入
        /// </summary>
        [DataMember, JsonProperty("canImport", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _canImport;

        /// <summary>
        /// 可导入
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"可导入"), Description(@"可导入")]
        public bool CanImport
        {
            get => _canImport;
            set
            {
                if (_canImport == value)
                    return;
                BeforePropertyChanged(nameof(CanImport), _canImport, value);
                _canImport = value;
                OnPropertyChanged(nameof(CanImport));
            }
        }

        /// <summary>
        /// 可导出
        /// </summary>
        [DataMember, JsonProperty("canExport", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _canExport;

        /// <summary>
        /// 可导出
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"可导出"), Description(@"可导出")]
        public bool CanExport
        {
            get => _canExport;
            set
            {
                if (_canExport == value)
                    return;
                BeforePropertyChanged(nameof(CanExport), _canExport, value);
                _canExport = value;
                OnPropertyChanged(nameof(CanExport));
            }
        }

        /// <summary>
        /// 用户提示文本
        /// </summary>
        [DataMember, JsonProperty("userHint", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _userHint;

        /// <summary>
        /// 用户提示文本
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"用户提示文本"), Description(@"用户提示文本")]
        public bool UserHint
        {
            get => _userHint;
            set
            {
                if (_userHint == value)
                    return;
                BeforePropertyChanged(nameof(UserHint), _userHint, value);
                _userHint = value;
                OnPropertyChanged(nameof(UserHint));
            }
        }

        /// <summary>
        /// 是否时间
        /// </summary>
        [DataMember, JsonProperty("isTime", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isTime;

        /// <summary>
        /// 是否时间
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"是否时间"), Description(@"是否时间")]
        public bool IsTime
        {
            get => _isTime;
            set
            {
                if (_isTime == value)
                    return;
                BeforePropertyChanged(nameof(IsTime), _isTime, value);
                _isTime = value;
                OnPropertyChanged(nameof(IsTime));
            }
        }
        #endregion 字段

        #region 兼容性升级

        /// <summary>
        /// 兼容性升级
        /// </summary>
        public void Upgrade()
        {
            //Copy(Entity);
        }

        #endregion

        #region 字段复制

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        public void Copy(IUIFieldConfig dest)
        {
            CopyFrom((SimpleConfig)dest);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        protected override void CopyFrom(SimpleConfig dest)
        {
            base.CopyFrom(dest);
            if (dest is IUIFieldConfig cfg)
                CopyProperty(cfg);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        /// <returns></returns>
        public void CopyProperty(IUIFieldConfig dest)
        {
            UserSee = dest.UserSee;
            CanUserQuery = dest.CanUserQuery;
            IsUserReadOnly = dest.IsUserReadOnly;
            MulitLine = dest.MulitLine;
            Rows = dest.Rows;
            Prefix = dest.Prefix;
            Suffix = dest.Suffix;
            EmptyValue = dest.EmptyValue;
            UiRequired = dest.UiRequired;
            InputType = dest.InputType;
            FormCloumnSapn = dest.FormCloumnSapn;
            FormOption = dest.FormOption;
            UserOrder = dest.UserOrder;
            ComboBoxUrl = dest.ComboBoxUrl;
            IsImage = dest.IsImage;
            IsMoney = dest.IsMoney;
            GridAlign = dest.GridAlign;
            GridWidth = dest.GridWidth;
            DataFormater = dest.DataFormater;
            GridDetails = dest.GridDetails;
            NoneGrid = dest.NoneGrid;
            NoneDetails = dest.NoneDetails;
            GridDetailsCode = dest.GridDetailsCode;
            CanImport = dest.CanImport;
            CanExport = dest.CanExport;
            UserHint = dest.UserHint;

            IsTime = dest.IsTime;
        }
        #endregion 字段复制
    }
}
