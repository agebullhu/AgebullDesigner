/*design by:agebull designer date:2017/7/12 22:06:40*/
/*****************************************************
©2008-2017 Copy right by agebull.hu(胡天水)
作者:agebull.hu(胡天水)
工程:Agebull.Common.Config
建立:2014-12-03
修改:2017-07-12
*****************************************************/

using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 属性配置
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class PropertyConfig : EntityChildConfig
    {
        #region 环境

        /// <summary>
        /// 是否接口引用对象
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category("设计器支持"), DisplayName("是否参照对象"), Description("是否接口引用对象")]
        public bool IsInterface => Option.IsReference;

        /// <summary>
        /// 是否关联对象
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category("设计器支持"), DisplayName("是否关联对象"), Description("是否关联对象")]
        public bool IsLink => Option.IsLink;


        /// <summary>
        /// 是否正在生成代码
        /// </summary>
        private static bool InCoding => WorkContext.InCoderGenerating;

        /// <summary>
        /// 链接的原始对象(字段与之有逻辑上的关联关系)
        /// </summary>
        private PropertyConfig InterfaceOrThis => Option.Reference as PropertyConfig ?? this;

        /// <summary>
        /// 链接的原始对象(字段与之有逻辑上的关联关系)
        /// </summary>
        private PropertyConfig InterfaceProperty => Option.Reference as PropertyConfig;

        /// <summary>
        /// 引用对象(无论接口还是引用)
        /// </summary>
        private PropertyConfig ReferenceOrThis => Option.ReferenceConfig as PropertyConfig ?? this;

        /// <summary>
        /// 引用对象(无论接口还是引用)
        /// </summary>
        private PropertyConfig ReferenceProperty => Option.ReferenceConfig as PropertyConfig;


        #endregion
        #region 设计器支持

        /// <summary>
        /// 构造
        /// </summary>
        public PropertyConfig()
        {
            _canGet = true;
            _canSet = true;
        }
        /// <summary>
        /// 属性名称
        /// </summary>
        /// <remark>
        /// 属性名称
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"属性名称"), Description("属性名称")]
        public string PropertyName => Name;

        /// <summary>
        /// 只读
        /// </summary>
        /// <remark>
        /// 是否只读
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"只读"), Description("是否只读")]
        public bool ReadOnly => IsCompute || IsIdentity || UniqueIndex > 0 || IsPrimaryKey;


        /// <summary>
        /// 分组
        /// </summary>
        [DataMember, JsonProperty("Group", NullValueHandling = NullValueHandling.Ignore)]
        internal string _group;

        /// <summary>
        /// 分组
        /// </summary>
        /// <remark>
        /// 分组
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"分组"), Description("分组")]
        public string Group
        {
            get => _group;
            set
            {
                if (_group == value)
                    return;
                BeforePropertyChanged(nameof(Group), _group, value);
                _group = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(Group));
            }
        }
        #endregion
        #region 系统

        /// <summary>
        /// 阻止编辑
        /// </summary>
        [DataMember, JsonProperty("DenyScope", NullValueHandling = NullValueHandling.Ignore)]
        internal AccessScopeType _denyScope;

        /// <summary>
        /// 阻止编辑
        /// </summary>
        /// <remark>
        /// 阻止使用的范围
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"系统"), DisplayName(@"阻止编辑"), Description("阻止使用的范围")]
        public AccessScopeType DenyScope
        {
            get => InterfaceOrThis._denyScope;
            set
            {
                if (_denyScope == value)
                    return;
                BeforePropertyChanged(nameof(DenyScope), _denyScope, value);
                _denyScope = value;
                OnPropertyChanged(nameof(DenyScope));
            }
        }
        #endregion
        #region 模型设计(C#)

        /// <summary>
        /// 数据类型
        /// </summary>
        [DataMember, JsonProperty("DataType", NullValueHandling = NullValueHandling.Ignore)]
        internal string _dataType;

        /// <summary>
        /// 数据类型
        /// </summary>
        /// <remark>
        /// 数据类型
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"数据类型")]
        public string DataType
        {
            get => ReferenceOrThis._dataType;
            set
            {
                if (_dataType == value)
                    return;
                BeforePropertyChanged(nameof(DataType), _dataType, value);
                _dataType = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(DataType));
            }
        }

        /// <summary>
        /// 语言类型(C#)
        /// </summary>
        [DataMember, JsonProperty("CsType", NullValueHandling = NullValueHandling.Ignore)]
        internal string _csType;

        /// <summary>
        /// 语言类型(C#)
        /// </summary>
        /// <remark>
        /// C#语言类型
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计(C#)"), DisplayName(@"语言类型(C#)"), Description("C#语言类型")]
        public string CsType
        {
            get => ReferenceOrThis.GetCsType();
            set
            {
                if (_csType == value)
                    return;
                BeforePropertyChanged(nameof(CsType), _csType, value);
                _csType = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(CsType));
            }
        }

        string GetCsType() => InCoding ? _csType ?? "string" : _csType;

        /// <summary>
        /// 是否时间
        /// </summary>
        [DataMember, JsonProperty("isTime", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isTime;

        /// <summary>
        /// 是否时间
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"是否时间"), Description("是否时间")]
        public bool IsTime
        {
            get => ReferenceOrThis._isTime;
            set
            {
                if (_isTime == value)
                    return;
                BeforePropertyChanged(nameof(IsTime), _isTime, value);
                _isTime = value;
                OnPropertyChanged(nameof(IsTime));
            }
        }

        /// <summary>
        /// 是否数组
        /// </summary>
        [DataMember, JsonProperty("isArray", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isArray;

        /// <summary>
        /// 是否扩展数组
        /// </summary>
        /// <remark>
        /// 是否扩展数组
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"是否数组"), Description("是否数组")]
        public bool IsArray
        {
            get => ReferenceOrThis._isArray;
            set
            {
                if (_isArray == value)
                    return;
                BeforePropertyChanged(nameof(IsArray), _isArray, value);
                _isArray = value;
                OnPropertyChanged(nameof(IsArray));
            }
        }
        /// <summary>
        /// 是否字典
        /// </summary>
        [DataMember, JsonProperty("IsDictionary", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isDictionary;

        /// <summary>
        /// 是否字典
        /// </summary>
        /// <remark>
        /// 是否扩展数组
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"是否字典"), Description("是否字典")]
        public bool IsDictionary
        {
            get => ReferenceOrThis._isDictionary;
            set
            {
                if (_isDictionary == value)
                    return;
                BeforePropertyChanged(nameof(IsDictionary), _isDictionary, value);
                _isDictionary = value;
                OnPropertyChanged(nameof(IsDictionary));
            }
        }

        /// <summary>
        /// 是否枚举类型
        /// </summary>
        [DataMember, JsonProperty("isEnum", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isEnum;

        /// <summary>
        /// 枚举类型(C#)
        /// </summary>
        /// <remark>
        /// 字段类型
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"是否枚举类型)"), Description("字段类型")]
        public bool IsEnum
        {
            get => ReferenceOrThis._isEnum;
            set
            {
                if (_isEnum == value)
                    return;
                BeforePropertyChanged(nameof(IsEnum), _isEnum, value);
                _isEnum = value;
                OnPropertyChanged(nameof(IsEnum));
            }
        }

        /// <summary>
        /// 非基本类型名称(C#)
        /// </summary>
        [DataMember, JsonProperty("CustomType", NullValueHandling = NullValueHandling.Ignore)]
        internal string _customType;

        /// <summary>
        /// 非基本类型名称(C#)
        /// </summary>
        /// <remark>
        /// 字段类型
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计(C#)"), DisplayName(@"非基本类型名称(C#)"), Description("字段类型")]
        public string CustomType
        {
            get => ReferenceOrThis._customType;
            set
            {
                if (_customType == value)
                    return;
                BeforePropertyChanged(nameof(CustomType), _customType, value);
                if (string.IsNullOrWhiteSpace(value) || value == CsType)
                {
                    _customType = null;
                    EnumConfig = null;
                    IsEnum = false;
                }
                else
                {
                    _customType = value.Trim();
                }
                OnPropertyChanged(nameof(CustomType));
            }
        }

        /// <summary>
        /// 参考类型
        /// </summary>
        [DataMember, JsonProperty("ReferenceType", NullValueHandling = NullValueHandling.Ignore)]
        private string _referenceType;
        /// <summary>
        /// 参考类型
        /// </summary>
        /// <remark>
        /// 字段类型
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计(C#)"), DisplayName(@"参考类型(C#)"), Description("字段类型")]
        public string ReferenceType
        {
            get => _referenceType;
            set
            {
                if (_referenceType == value)
                    return;
                BeforePropertyChanged(nameof(ReferenceType), _referenceType, value);
                _referenceType = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(ReferenceType));
            }
        }

        /// <summary>
        /// 结果类型(C#)
        /// </summary>
        /// <remark>
        /// 最终生成C#代码时的属性类型
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计(C#)"), DisplayName(@"结果类型(C#)"), Description("最终生成C#代码时的属性类型")]
        public string LastCsType => ReferenceOrThis.ToLastCsType();

        private string ToLastCsType()
        {
            if (!string.IsNullOrWhiteSpace(_referenceType))
                return _referenceType;
            if (!string.IsNullOrWhiteSpace(_customType))
                return CustomType;
            _customType = null;
            if (_csType == null)
                return null;
            if (_csType.Contains("["))
            {
                CsType = _csType.Split('[')[0];
                IsArray=true;
            }
            if (IsArray)
                return $"{_csType}[]";
            if (string.Equals(_csType, "string", StringComparison.OrdinalIgnoreCase))
                return _csType;
            return Nullable ? $"{_csType}?" : _csType;
        }
        /// <summary>
        /// 可空类型(C#)的说明文字
        /// </summary>
        const string Nullable_Description = @"即生成的C#代码,类型为空类型Nullable<T> ,如int?";

        /// <summary>
        /// 可空类型(C#)
        /// </summary>
        [DataMember, JsonProperty("Nullable", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _nullable;

        /// <summary>
        /// 可空类型(C#)
        /// </summary>
        /// <remark>
        /// 即生成的C#代码,类型为空类型Nullable〈T〉 ,如int?
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计(C#)"), DisplayName(@"可空类型(C#)"), Description(Nullable_Description)]
        public bool Nullable
        {
            get => ReferenceOrThis._nullable;
            set
            {
                if (_nullable == value)
                    return;
                BeforePropertyChanged(nameof(Nullable), _nullable, value);
                _nullable = value;
                OnPropertyChanged(nameof(Nullable));
            }
        }
        #endregion
        #region 模型设计

        /// <summary>
        /// 是否扩展值
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        internal bool _isExtendValue;

        /// <summary>
        /// 是否扩展值
        /// </summary>
        /// <remark>
        /// 是否扩展值
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@""), DisplayName(@"是否扩展值"), Description("是否扩展值")]
        public bool IsExtendValue
        {
            get => _isExtendValue;
            set
            {
                if (_isExtendValue == value)
                    return;
                BeforePropertyChanged(nameof(IsExtendValue), _isExtendValue, value);
                _isExtendValue = value;
                OnPropertyChanged(nameof(IsExtendValue));
            }
        }

        /// <summary>
        /// 对应枚举
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        internal Guid _enumKey;

        /// <summary>
        /// 对应枚举
        /// </summary>
        /// <remark>
        /// 当使用自定义类型时的枚举对象
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"对应枚举"), Description("当使用自定义类型时的枚举对象")]
        public Guid EnumKey
        {
            get => ReferenceOrThis._enumKey;
            set
            {
                if (_enumKey == value)
                    return;
                BeforePropertyChanged(nameof(EnumKey), _enumConfig, value);
                _enumKey = value;
                OnPropertyChanged(nameof(EnumKey));
            }
        }

        /// <summary>
        /// 对应枚举
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        internal EnumConfig _enumConfig;

        /// <summary>
        /// 对应枚举
        /// </summary>
        /// <remark>
        /// 当使用自定义类型时的枚举对象
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"对应枚举"), Description("当使用自定义类型时的枚举对象")]
        public EnumConfig EnumConfig
        {
            get => CustomType == null ? null : ReferenceOrThis.GetEnumConfig();
            set
            {
                if (_enumConfig == value)
                    return;
                BeforePropertyChanged(nameof(EnumConfig), _enumConfig, value);
                _enumConfig = value;
                EnumKey = value?.Key ?? Guid.Empty;
                IsEnum = value != null;
                if (value != null)
                    CustomType = value.Name;
                OnPropertyChanged(nameof(EnumConfig));
            }
        }

        EnumConfig GetEnumConfig() => _enumConfig ?? (_enumConfig = GlobalConfig.GetEnum(_customType));

        /// <summary>
        /// 内部字段
        /// </summary>
        [DataMember, JsonProperty("_innerField", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _innerField;

        /// <summary>
        /// 内部字段
        /// </summary>
        /// <remark>
        /// 是否内部字段,即非用户字段,不呈现给用户
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"内部字段"), Description("是否内部字段,即非用户字段,不呈现给用户")]
        public bool InnerField
        {
            get => InterfaceOrThis._innerField;
            set
            {
                if (_innerField == value)
                    return;
                BeforePropertyChanged(nameof(InnerField), _innerField, value);
                _innerField = value;
                OnPropertyChanged(nameof(InnerField));
            }
        }

        /// <summary>
        /// 系统字段
        /// </summary>
        [DataMember, JsonProperty("IsSystemField", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isSystemField;

        /// <summary>
        /// 系统字段
        /// </summary>
        /// <remark>
        /// 系统字段
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"系统字段"), Description("系统字段")]
        public bool IsSystemField
        {
            get => InterfaceOrThis._isSystemField;
            set
            {
                if (_isSystemField == value)
                    return;
                BeforePropertyChanged(nameof(IsSystemField), _isSystemField, value);
                _isSystemField = value;
                OnPropertyChanged(nameof(IsSystemField));
            }
        }

        /// <summary>
        /// 接口字段
        /// </summary>
        [DataMember, JsonProperty("IsInterfaceField", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isInterfaceField;

        /// <summary>
        /// 接口字段
        /// </summary>
        /// <remark>
        /// 是否接口字段
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"接口字段"), Description("是否接口字段")]
        public bool IsInterfaceField
        {
            get => _isInterfaceField;
            set
            {
                if (_isInterfaceField == value)
                    return;
                BeforePropertyChanged(nameof(IsInterfaceField), _isInterfaceField, value);
                _isInterfaceField = value;
                OnPropertyChanged(nameof(IsInterfaceField));
            }
        }


        /// <summary>
        /// 代码访问范围的说明文字
        /// </summary>
        const string AccessType_Description = @"代码访问范围,即面向对象的三大范围(public,private,protected)";

        /// <summary>
        /// 代码访问范围
        /// </summary>
        /// <remark>
        /// 代码访问范围,即面向对象的三大范围(public,private,protected)
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"代码访问范围"), Description(AccessType_Description)]
        public string AccessType => InnerField || DenyScope.HasFlag(AccessScopeType.Server)
            //||!IsRelation && !string.IsNullOrWhiteSpace(ExtendRole) &&
            //ExtendRole.Contains(",")
            ? "internal"
            : "public ";

        /// <summary>
        /// 别名
        /// </summary>
        [DataMember, JsonProperty("Alias", NullValueHandling = NullValueHandling.Ignore)]
        internal string _alias;

        /// <summary>
        /// 别名
        /// </summary>
        /// <remark>
        /// 属性别名
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"别名"), Description("属性别名")]
        public string Alias
        {
            get => _alias;
            set
            {
                if (_alias == value)
                    return;
                BeforePropertyChanged(nameof(Alias), _alias, value);
                _alias = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(Alias));
            }
        }

        /// <summary>
        /// 初始值的说明文字
        /// </summary>
        const string Initialization_Description = @"3初始值,原样写入代码,如果是文本,需要加引号";

        /// <summary>
        /// 初始值
        /// </summary>
        [DataMember, JsonProperty("Initialization", NullValueHandling = NullValueHandling.Ignore)]
        internal string _initialization;

        /// <summary>
        /// 初始值
        /// </summary>
        /// <remark>
        /// 3初始值,原样写入代码,如果是文本,需要加引号
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"初始值"), Description(Initialization_Description)]
        public string Initialization
        {
            get => _initialization;
            set
            {
                if (_initialization == value)
                    return;
                BeforePropertyChanged(nameof(Initialization), _initialization, value);
                _initialization = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(Initialization));
            }
        }
        #endregion
        #region 模型设计(C++)
        /// <summary>
        /// 私有字段
        /// </summary>
        [DataMember, JsonProperty("isPrivateField", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isPrivateField;

        /// <summary>
        /// 私有字段
        /// </summary>
        /// <remark>
        /// 私有字段,不应该复制
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计(C++)"), DisplayName(@"私有字段"), Description("私有字段,不应该复制")]
        public bool IsPrivateField
        {
            get => _isMiddleField;
            set
            {
                if (_isPrivateField == value)
                    return;
                BeforePropertyChanged(nameof(IsPrivateField), _isPrivateField, value);
                _isPrivateField = value;
                OnPropertyChanged(nameof(IsPrivateField));
            }
        }

        /// <summary>
        /// 设计时字段的说明文字
        /// </summary>
        const string IsMiddleField_Description = @"设计时使用的中间过程字段,即最终使用时不需要的字段";

        /// <summary>
        /// 设计时字段
        /// </summary>
        [DataMember, JsonProperty("_middleField", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isMiddleField;

        /// <summary>
        /// 设计时字段
        /// </summary>
        /// <remark>
        /// 设计时使用的中间过程字段,即最终使用时不需要的字段
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计(C++)"), DisplayName(@"设计时字段"), Description(IsMiddleField_Description)]
        public bool IsMiddleField
        {
            get => _isMiddleField;
            set
            {
                if (_isMiddleField == value)
                    return;
                BeforePropertyChanged(nameof(IsMiddleField), _isMiddleField, value);
                _isMiddleField = value;
                OnPropertyChanged(nameof(IsMiddleField));
            }
        }

        /// <summary>
        /// 语言类型(C++)
        /// </summary>
        [DataMember, JsonProperty("CppType", NullValueHandling = NullValueHandling.Ignore)]
        internal string _cppType;

        /// <summary>
        /// 语言类型(C++)
        /// </summary>
        /// <remark>
        /// C++字段类型
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计(C++)"), DisplayName(@"语言类型(C++)"), Description("C++字段类型")]
        public string CppType
        {
            get => _cppType;
            set
            {
                if (_cppType == value)
                    return;
                BeforePropertyChanged(nameof(CppType), _cppType, value);
                _cppType = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(CppType));
            }
        }

        /// <summary>
        /// 字段名称(C++)
        /// </summary>
        [DataMember, JsonProperty("CppName", NullValueHandling = NullValueHandling.Ignore)]
        internal string _cppName;

        /// <summary>
        /// 字段名称(C++)
        /// </summary>
        /// <remark>
        /// C++字段名称
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计(C++)"), DisplayName(@"字段名称(C++)"), Description("C++字段名称")]
        public string CppName
        {
            get => InCoding ? _cppName ?? Name : _cppName;
            set
            {
                if (_cppName == value)
                    return;
                if (value == Name)
                    value = null;
                BeforePropertyChanged(nameof(CppName), _cppName, value);
                _cppName = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(CppName));
            }
        }

        /// <summary>
        /// 结果类型(C++)
        /// </summary>
        [DataMember, JsonProperty("_cppLastType", NullValueHandling = NullValueHandling.Ignore)]
        internal string _cppLastType;

        /// <summary>
        /// 结果类型(C++)
        /// </summary>
        /// <remark>
        /// 最终生成C++代码时的字段类型
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计(C++)"), DisplayName(@"结果类型(C++)"), Description("最终生成C++代码时的字段类型")]
        public string CppLastType
        {
            get => _cppLastType;
            set
            {
                if (_cppLastType == value)
                    return;
                BeforePropertyChanged(nameof(CppLastType), _cppLastType, value);
                _cppLastType = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(CppLastType));
            }
        }

        /// <summary>
        /// C++字段类型
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        internal object _cppTypeObject;

        /// <summary>
        /// C++字段类型
        /// </summary>
        /// <remark>
        /// C++字段类型
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计(C++)"), DisplayName(@"C++字段类型"), Description("C++字段类型")]
        public object CppTypeObject
        {
            get => _cppTypeObject;
            set
            {
                if (_cppTypeObject == value)
                    return;
                BeforePropertyChanged(nameof(CppTypeObject), _cppTypeObject, value);
                _cppTypeObject = value;
                OnPropertyChanged(nameof(CppTypeObject));
            }
        }

        /// <summary>
        /// 6位小数的整数的说明文字
        /// </summary>
        const string IsIntDecimal_Description = @"是否转为整数的小数,即使用扩大100成倍的整数";

        /// <summary>
        /// 6位小数的整数
        /// </summary>
        /// <remark>
        /// 是否转为整数的小数,即使用扩大100成倍的整数
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计(C++)"), DisplayName(@"6位小数的整数"), Description(IsIntDecimal_Description)]
        public bool IsIntDecimal => CsType == "decimal" && CppLastType == "__int64";
        #endregion
        #region 模型设计(计算列)

        /// <summary>
        /// 是否有Get属性
        /// </summary>
        [DataMember, JsonProperty("canGet", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _canGet;

        /// <summary>
        /// 可读
        /// </summary>
        /// <remark>
        /// 可读,即可以生成Get代码
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"可读"), Description("可读,可以生成Get代码")]
        public bool CanGet
        {
            get => _canGet && (!IsCustomCompute || !string.IsNullOrWhiteSpace(ComputeGetCode));
            set
            {
                if (_canGet == value)
                    return;
                BeforePropertyChanged(nameof(CanGet), _canGet, value);
                _canGet = value;
                OnPropertyChanged(nameof(CanGet));
            }
        }

        /// <summary>
        /// 是否有Set属性
        /// </summary>
        [DataMember, JsonProperty("canSet", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _canSet;


        /// <summary>
        /// 可写
        /// </summary>
        /// <remark>
        /// 可写,即生成SET代码
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"可写"), Description("可写,即生成SET代码")]
        public bool CanSet
        {
            get => _canSet && (!IsCustomCompute || !string.IsNullOrWhiteSpace(ComputeSetCode));
            set
            {
                if (_canSet == value)
                    return;
                BeforePropertyChanged(nameof(CanSet), _canSet, value);
                _canSet = value;
                OnPropertyChanged(nameof(CanSet));
            }
        }

        /// <summary>
        /// 计算列的说明文字
        /// </summary>
        const string IsCompute_Description = @"是否计算列，即数据源于其它字段.如关系引用字段";

        /// <summary>
        /// 计算列
        /// </summary>
        [DataMember, JsonProperty("IsCompute", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isCompute;

        /// <summary>
        /// 计算列
        /// </summary>
        /// <remark>
        /// 是否计算列，即数据源于其它字段.如关系引用字段
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计(计算列)"), DisplayName(@"计算列"), Description(IsCompute_Description)]
        public bool IsCompute
        {
            get => _isCompute;
            set
            {
                if (_isCompute == value)
                    return;
                BeforePropertyChanged(nameof(IsCompute), _isCompute, value);
                _isCompute = value;
                OnPropertyChanged(nameof(IsCompute));
            }
        }

        /// <summary>
        /// 自定义代码(get)
        /// </summary>
        [DataMember, JsonProperty("ComputeGetCode", NullValueHandling = NullValueHandling.Ignore)]
        internal string _computeGetCode;

        /// <summary>
        /// 自定义代码(get)
        /// </summary>
        /// <remark>
        /// 自定义代码Get部分代码,仅用于C#
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计(计算列)"), DisplayName(@"自定义代码(get)"), Description("自定义代码Get部分代码,仅用于C#")]
        public string ComputeGetCode
        {
            get => _computeGetCode;
            set
            {
                if (_computeGetCode == value)
                    return;
                BeforePropertyChanged(nameof(ComputeGetCode), _computeGetCode, value);
                _computeGetCode = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(ComputeGetCode));
            }
        }

        /// <summary>
        /// 自定义代码(set)
        /// </summary>
        [DataMember, JsonProperty("ComputeSetCode", NullValueHandling = NullValueHandling.Ignore)]
        internal string _computeSetCode;

        /// <summary>
        /// 自定义代码(set)
        /// </summary>
        /// <remark>
        /// 自定义代码Set部分代码,仅用于C#
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计(计算列)"), DisplayName(@"自定义代码(set)"), Description("自定义代码Set部分代码,仅用于C#")]
        public string ComputeSetCode
        {
            get => _computeSetCode;
            set
            {
                if (_computeSetCode == value)
                    return;
                BeforePropertyChanged(nameof(ComputeSetCode), _computeSetCode, value);
                _computeSetCode = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(ComputeSetCode));
            }
        }

        /// <summary>
        /// 自定义读写代码的说明文字
        /// </summary>
        const string IsCustomCompute_Description = @"自定义读写代码,即不使用代码生成,而使用录入的代码";

        /// <summary>
        /// 自定义读写代码
        /// </summary>
        [DataMember, JsonProperty("IsCustomCompute", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isCustomCompute;

        /// <summary>
        /// 自定义读写代码
        /// </summary>
        /// <remark>
        /// 自定义读写代码,即不使用代码生成,而使用录入的代码
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计(计算列)"), DisplayName(@"自定义读写代码"), Description(IsCustomCompute_Description)]
        public bool IsCustomCompute
        {
            get => _isCustomCompute;
            set
            {
                if (_isCustomCompute == value)
                    return;
                BeforePropertyChanged(nameof(IsCustomCompute), _isCustomCompute, value);
                _isCustomCompute = value;
                OnPropertyChanged(nameof(IsCustomCompute));
            }
        }

        #endregion
        #region API支持

        /// <summary>
        /// 不参与ApiArgument序列化
        /// </summary>
        [DataMember, JsonProperty("noneApiArgument", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _noneApiArgument;

        /// <summary>
        /// 不参与ApiArgument序列化
        /// </summary>
        /// <remark>
        /// 客户端不显示
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"API支持"), DisplayName(@"不参与ApiArgument序列化")]
        public bool NoneApiArgument
        {
            get => _noneApiArgument;
            set
            {
                if (_noneApiArgument == value)
                    return;
                BeforePropertyChanged(nameof(NoneApiArgument), _noneApiArgument, value);
                _noneApiArgument = value;
                OnPropertyChanged(nameof(NoneApiArgument));
            }
        }
        /// <summary>
        /// 字段名称(ApiArgument)
        /// </summary>
        [DataMember, JsonProperty("apiArgumentName", NullValueHandling = NullValueHandling.Ignore)]
        internal string _apiArgumentName;

        /// <summary>
        /// 字段名称(ApiArgument)
        /// </summary>
        /// <remark>
        /// ApiArgument字段名称
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"API支持"), DisplayName(@"字段名称(ApiArgument)")]
        public string ApiArgumentName
        {
            get => InCoding ? _apiArgumentName ?? JsonName : _apiArgumentName;
            set
            {
                if (_apiArgumentName == value)
                    return;
                BeforePropertyChanged(nameof(ApiArgumentName), _apiArgumentName, value);
                _apiArgumentName = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(ApiArgumentName));
            }
        }
        /// <summary>
        /// 不参与Json序列化
        /// </summary>
        [DataMember, JsonProperty("NoneJson", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _noneJson;

        /// <summary>
        /// 不参与Json序列化
        /// </summary>
        /// <remark>
        /// 客户端不显示
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"API支持"), DisplayName(@"不参与Json序列化")]
        public bool NoneJson
        {
            get => _noneJson;
            set
            {
                if (_noneJson == value)
                    return;
                BeforePropertyChanged(nameof(NoneJson), _noneJson, value);
                _noneJson = value;
                OnPropertyChanged(nameof(NoneJson));
            }
        }
        /// <summary>
        /// 字段名称(json)
        /// </summary>
        [DataMember, JsonProperty("jsonName", NullValueHandling = NullValueHandling.Ignore)]
        internal string _jsonName;

        /// <summary>
        /// 字段名称(json)
        /// </summary>
        /// <remark>
        /// json字段名称
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"API支持"), DisplayName(@"字段名称(json)")]
        public string JsonName
        {
            get => InCoding ? _jsonName ?? Name : _jsonName;
            set
            {
                if (_jsonName == value)
                    return;
                BeforePropertyChanged(nameof(JsonName), _jsonName, value);
                _jsonName = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(JsonName));
            }
        }
        /// <summary>
        /// 示例内容
        /// </summary>
        [DataMember, JsonProperty("_helloCode", NullValueHandling = NullValueHandling.Ignore)]
        internal string _helloCode;

        /// <summary>
        /// 示例内容
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"API支持"), DisplayName(@"示例内容")]
        public string HelloCode
        {
            get => _helloCode;
            set
            {
                if (_helloCode == value)
                    return;
                BeforePropertyChanged(nameof(HelloCode), _helloCode, value);
                _helloCode = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(HelloCode));
            }
        }
        #endregion
        #region 数据标识

        /// <summary>
        /// 标题字段
        /// </summary>
        [DataMember, JsonProperty("IsCaption", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isCaption;

        /// <summary>
        /// 标题字段
        /// </summary>
        /// <remark>
        /// 标题字段
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据标识"), DisplayName(@"标题字段"), Description("标题字段")]
        public bool IsCaption
        {
            get => _isCaption;
            set
            {
                if (_isCaption == value)
                    return;
                BeforePropertyChanged(nameof(IsCaption), _isCaption, value);
                _isCaption = value;
                OnPropertyChanged(nameof(IsCaption));
            }
        }

        /// <summary>
        /// 主键字段
        /// </summary>
        [DataMember, JsonProperty("IsPrimaryKey", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isPrimaryKey;

        /// <summary>
        /// 主键字段
        /// </summary>
        /// <remark>
        /// 主键
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据标识"), DisplayName(@"主键字段"), Description("主键")]
        public bool IsPrimaryKey
        {
            get => _isPrimaryKey;
            set
            {
                if (_isPrimaryKey == value)
                    return;
                BeforePropertyChanged(nameof(IsPrimaryKey), _isPrimaryKey, value);
                _isPrimaryKey = value;
                OnPropertyChanged(nameof(IsPrimaryKey));
            }
        }

        /// <summary>
        /// 唯一值字段
        /// </summary>
        [DataMember, JsonProperty("IsExtendKey", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isExtendKey;

        /// <summary>
        /// 唯一值字段
        /// </summary>
        /// <remark>
        /// 即它也是唯一标识符,如用户的身份证号
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据标识"), DisplayName(@"唯一值字段"), Description("即它也是唯一标识符,如用户的身份证号")]
        public bool IsExtendKey
        {
            get => _isExtendKey;
            set
            {
                if (_isExtendKey == value)
                    return;
                BeforePropertyChanged(nameof(IsExtendKey), _isExtendKey, value);
                _isExtendKey = value;
                OnPropertyChanged(nameof(IsExtendKey));
            }
        }

        /// <summary>
        /// 自增字段的说明文字
        /// </summary>
        const string IsIdentity_Description = @"自增列,通过数据库(或REDIS)自动增加";

        /// <summary>
        /// 自增字段
        /// </summary>
        [DataMember, JsonProperty("IsIdentity", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isIdentity;

        /// <summary>
        /// 自增字段
        /// </summary>
        /// <remark>
        /// 自增列,通过数据库(或REDIS)自动增加
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据标识"), DisplayName(@"自增字段"), Description(IsIdentity_Description)]
        public bool IsIdentity
        {
            get => _isIdentity;
            set
            {
                if (_isIdentity == value)
                    return;
                BeforePropertyChanged(nameof(IsIdentity), _isIdentity, value);
                _isIdentity = value;
                OnPropertyChanged(nameof(IsIdentity));
            }
        }

        /// <summary>
        /// 全局标识
        /// </summary>
        [DataMember, JsonProperty("IsGlobalKey", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isGlobalKey;

        /// <summary>
        /// 全局标识
        /// </summary>
        /// <remark>
        /// 是否使用GUID的全局KEY
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据标识"), DisplayName(@"全局标识"), Description("是否使用GUID的全局KEY")]
        public bool IsGlobalKey
        {
            get => _isGlobalKey;
            set
            {
                if (_isGlobalKey == value)
                    return;
                BeforePropertyChanged(nameof(IsGlobalKey), _isGlobalKey, value);
                _isGlobalKey = value;
                OnPropertyChanged(nameof(IsGlobalKey));
            }
        }

        /// <summary>
        /// 唯一属性组合顺序
        /// </summary>
        [DataMember, JsonProperty("UniqueIndex", NullValueHandling = NullValueHandling.Ignore)]
        internal int _uniqueIndex;

        /// <summary>
        /// 唯一属性组合顺序
        /// </summary>
        /// <remark>
        /// 参与组合成唯一属性的顺序,大于0有效
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据标识"), DisplayName(@"唯一属性组合顺序"), Description("参与组合成唯一属性的顺序,大于0有效")]
        public int UniqueIndex
        {
            get => _uniqueIndex;
            set
            {
                if (_uniqueIndex == value)
                    return;
                BeforePropertyChanged(nameof(UniqueIndex), _uniqueIndex, value);
                _uniqueIndex = value;
                OnPropertyChanged(nameof(UniqueIndex));
            }
        }

        /// <summary>
        /// 唯一文本
        /// </summary>
        [DataMember, JsonProperty("UniqueString", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _uniqueString;

        /// <summary>
        /// 唯一文本
        /// </summary>
        /// <remark>
        /// 5是否唯一文本
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据标识"), DisplayName(@"唯一文本"), Description("5是否唯一文本")]
        public bool UniqueString
        {
            get => _uniqueString;
            set
            {
                if (_uniqueString == value)
                    return;
                BeforePropertyChanged(nameof(UniqueString), _uniqueString, value);
                _uniqueString = value;
                OnPropertyChanged(nameof(UniqueString));
            }
        }
        #endregion

        #region 数据库

        /// <summary>
        /// 构建数据库索引
        /// </summary>
        /// <remark>
        /// 构建数据库索引的优化选项
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据库"), DisplayName(@"构建数据库索引"), Description("构建数据库索引的优化选项")]
        public bool CreateDbIndex => _isDbIndex || (ReferenceProperty?.CreateDbIndex ?? IsPrimaryKey || IsIdentity || IsLinkKey || IsCaption);


        /// <summary>
        /// 数据库索引
        /// </summary>
        [DataMember, JsonProperty("isDbIndex", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isDbIndex;

        /// <summary>
        /// 是否数据库索引
        /// </summary>
        /// <remark>
        /// 构建数据库索引的优化选项
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据库"), DisplayName(@"是否数据库索引"), Description("构建数据库索引的优化选项")]
        public bool IsDbIndex
        {
            get => _isDbIndex;
            set
            {
                if (_isDbIndex == value)
                    return;
                BeforePropertyChanged(nameof(IsDbIndex), _isDbIndex, value);
                _isDbIndex = value;
                OnPropertyChanged(nameof(IsDbIndex));
            }
        }

        /// <summary>
        /// 不更新
        /// </summary>
        /// <remark>
        /// 不更新
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据库"), DisplayName(@"不更新"), Description("不更新")]
        public bool KeepUpdate
        {
            get => KeepStorageScreen == StorageScreenType.Update;
            set => KeepStorageScreen = value ? StorageScreenType.Update : StorageScreenType.None;
        }

        /// <summary>
        /// 数据库字段名称
        /// </summary>
        [DataMember, JsonProperty("_columnName", NullValueHandling = NullValueHandling.Ignore)]
        internal string _dbFieldName;

        /// <summary>
        /// 数据库字段名称
        /// </summary>
        /// <remark>
        /// 字段名称
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据库"), DisplayName(@"数据库字段名称"), Description("字段名称")]
        public string DbFieldName
        {
            get => InCoding ? _dbFieldName ?? Name : _dbFieldName;
            set
            {
                if (_dbFieldName == value)
                    return;
                if (Name == value)
                    value = null;
                BeforePropertyChanged(nameof(DbFieldName), _dbFieldName, value);
                _dbFieldName = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(DbFieldName));
            }
        }

        /// <summary>
        /// 能否存储空值的说明文字
        /// </summary>
        const string DbNullable_Description = @"如为真,在存储空值读取时使用语言类型的默认值";

        /// <summary>
        /// 能否存储空值
        /// </summary>
        [DataMember, JsonProperty("_dbNullable", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _dbNullable;

        /// <summary>
        /// 能否存储空值
        /// </summary>
        /// <remark>
        /// 如为真,在存储空值读取时使用语言类型的默认值
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据库"), DisplayName(@"能否存储空值"), Description(DbNullable_Description)]
        public bool DbNullable
        {
            get => InterfaceOrThis._dbNullable;
            set
            {
                if (_dbNullable == value)
                    return;
                BeforePropertyChanged(nameof(DbNullable), _dbNullable, value);
                _dbNullable = value;
                OnPropertyChanged(nameof(DbNullable));
            }
        }

        /// <summary>
        /// 存储类型
        /// </summary>
        [DataMember, JsonProperty("DbType", NullValueHandling = NullValueHandling.Ignore)]
        internal string _dbType;

        /// <summary>
        /// 存储类型
        /// </summary>
        /// <remark>
        /// 存储类型
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据库"), DisplayName(@"存储类型"), Description("存储类型")]
        public string DbType
        {
            get => ReferenceOrThis._dbType;
            set
            {
                if (_dbType == value)
                    return;
                BeforePropertyChanged(nameof(DbType), _dbType, value);
                _dbType = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(DbType));
            }
        }

        /// <summary>
        /// 数据长度
        /// </summary>
        [DataMember, JsonProperty("_datalen", NullValueHandling = NullValueHandling.Ignore)]
        internal int _datalen;

        /// <summary>
        /// 数据长度
        /// </summary>
        /// <remark>
        /// 文本或二进制存储的最大长度
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据库"), DisplayName(@"数据长度"), Description("文本或二进制存储的最大长度")]
        public int Datalen
        {
            get => ReferenceOrThis._datalen;
            set
            {
                if (_datalen == value)
                    return;
                BeforePropertyChanged(nameof(Datalen), _datalen, value);
                _datalen = value;
                OnPropertyChanged(nameof(Datalen));
                OnPropertyChanged(nameof(AutoDataRuleDesc));
            }
        }

        /// <summary>
        /// 数组长度
        /// </summary>
        [DataMember, JsonProperty("ArrayLen", NullValueHandling = NullValueHandling.Ignore)]
        internal string _arrayLen;

        /// <summary>
        /// 数组长度
        /// </summary>
        /// <remark>
        /// 数组长度
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据库"), DisplayName(@"数组长度"), Description("数组长度")]
        public string ArrayLen
        {
            get => ReferenceOrThis._arrayLen;
            set
            {
                if (_arrayLen == value)
                    return;
                BeforePropertyChanged(nameof(ArrayLen), _arrayLen, value);
                _arrayLen = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(ArrayLen));
            }
        }

        /// <summary>
        /// 存储精度
        /// </summary>
        [DataMember, JsonProperty("Scale", NullValueHandling = NullValueHandling.Ignore)]
        internal int _scale;

        /// <summary>
        /// 存储精度
        /// </summary>
        /// <remark>
        /// 存储精度
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据库"), DisplayName(@"存储精度"), Description("存储精度")]
        public int Scale
        {
            get => ReferenceOrThis._scale;
            set
            {
                if (_scale == value)
                    return;
                BeforePropertyChanged(nameof(Scale), _scale, value);
                _scale = value;
                OnPropertyChanged(nameof(Scale));
                OnPropertyChanged(nameof(AutoDataRuleDesc));
            }
        }

        /// <summary>
        /// 存储列ID
        /// </summary>
        [DataMember, JsonProperty("DbIndex", NullValueHandling = NullValueHandling.Ignore)]
        internal int _dbIndex;

        /// <summary>
        /// 存储列ID
        /// </summary>
        /// <remark>
        /// 存储列ID,即在数据库内部对应的列ID
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据库"), DisplayName(@"存储列ID"), Description("存储列ID,即在数据库内部对应的列ID")]
        public int DbIndex
        {
            get => _dbIndex;
            set
            {
                if (_dbIndex == value)
                    return;
                BeforePropertyChanged(nameof(DbIndex), _dbIndex, value);
                _dbIndex = value;
                OnPropertyChanged(nameof(DbIndex));
            }
        }

        /// <summary>
        /// 固定长度
        /// </summary>
        [DataMember, JsonProperty("FixedLength", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _fixedLength;

        /// <summary>
        /// 固定长度
        /// </summary>
        /// <remark>
        /// 是否固定长度字符串
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据库"), DisplayName(@"固定长度"), Description("是否固定长度字符串")]
        public bool FixedLength
        {
            get => ReferenceOrThis._fixedLength;
            set
            {
                if (_fixedLength == value)
                    return;
                BeforePropertyChanged(nameof(FixedLength), _fixedLength, value);
                _fixedLength = value;
                OnPropertyChanged(nameof(FixedLength));
            }
        }

        /// <summary>
        /// 备注字段
        /// </summary>
        [DataMember, JsonProperty("IsMemo", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isMemo;

        /// <summary>
        /// 备注字段
        /// </summary>
        /// <remark>
        /// 是否备注字段
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据库"), DisplayName(@"备注字段"), Description("是否备注字段")]
        public bool IsMemo
        {
            get => _isMemo;
            set
            {
                if (_isMemo == value)
                    return;
                BeforePropertyChanged(nameof(IsMemo), _isMemo, value);
                _isMemo = value;
                OnPropertyChanged(nameof(IsMemo));
            }
        }

        /// <summary>
        /// 大数据
        /// </summary>
        [DataMember, JsonProperty("IsBlob", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isBlob;

        /// <summary>
        /// 大数据
        /// </summary>
        /// <remark>
        /// 是否大数据
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据库"), DisplayName(@"大数据"), Description("是否大数据")]
        public bool IsBlob
        {
            get => InterfaceOrThis._isBlob || DataType == "ByteArray";
            set
            {
                value = value || DataType == "ByteArray";
                if (_isBlob == value)
                    return;
                BeforePropertyChanged(nameof(IsBlob), _isBlob, value);
                _isBlob = value;
                OnPropertyChanged(nameof(IsBlob));
            }
        }

        /// <summary>
        /// 内部字段(数据库)的说明文字
        /// </summary>
        const string DbInnerField_Description = @"数据库内部字段,如果为真,仅支持在SQL的语句中出现此字段，不支持外部的读写";

        /// <summary>
        /// 内部字段(数据库)
        /// </summary>
        [DataMember, JsonProperty("DbInnerField", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _dbInnerField;

        /// <summary>
        /// 内部字段(数据库)
        /// </summary>
        /// <remark>
        /// 数据库内部字段,如果为真,仅支持在SQL的语句中出现此字段，不支持外部的读写
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据库"), DisplayName(@"内部字段(数据库)"), Description(DbInnerField_Description)]
        public bool DbInnerField
        {
            get => InterfaceOrThis._dbInnerField;
            set
            {
                if (_dbInnerField == value)
                    return;
                BeforePropertyChanged(nameof(DbInnerField), _dbInnerField, value);
                _dbInnerField = value;
                OnPropertyChanged(nameof(DbInnerField));
            }
        }

        /// <summary>
        /// 非数据库字段的说明文字
        /// </summary>
        const string NoStorage_Description = @"是否非数据库字段,如果为真,数据库的读写均忽略这个字段";

        /// <summary>
        /// 非数据库字段
        /// </summary>
        [DataMember, JsonProperty("NoStorage", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _noStorage;

        /// <summary>
        /// 非数据库字段
        /// </summary>
        /// <remark>
        /// 是否非数据库字段,如果为真,数据库的读写均忽略这个字段
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据库"), DisplayName(@"非数据库字段"), Description(NoStorage_Description)]
        public bool NoStorage
        {
            get => (Parent?.Parent != null && Parent.Parent.NoRelation && IsCompute && IsLinkField) || InterfaceOrThis._noStorage;
            set
            {
                if (_noStorage == value)
                    return;
                BeforePropertyChanged(nameof(NoStorage), _noStorage, value);
                _noStorage = value;
                OnPropertyChanged(nameof(NoStorage));
            }
        }

        /// <summary>
        /// *跳过保存的场景
        /// </summary>
        [DataMember, JsonProperty("KeepStorageScreen", NullValueHandling = NullValueHandling.Ignore)]
        internal StorageScreenType _keepStorageScreen;

        /// <summary>
        /// *跳过保存的场景
        /// </summary>
        /// <remark>
        /// 跳过保存的场景
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据库"), DisplayName(@"*跳过保存的场景"), Description("跳过保存的场景")]
        public StorageScreenType KeepStorageScreen
        {
            get => IsCompute && IsLinkField ? StorageScreenType.All : InterfaceOrThis._keepStorageScreen;
            set
            {
                if (_keepStorageScreen == value)
                    return;
                BeforePropertyChanged(nameof(KeepStorageScreen), _keepStorageScreen, value);
                _keepStorageScreen = value;
                OnPropertyChanged(nameof(KeepStorageScreen));
            }
        }

        /// <summary>
        /// 自定义保存的说明文字
        /// </summary>
        const string CustomWrite_Description = @"自定义保存,如果为真,数据库的写入忽略这个字段,数据的写入由代码自行维护";

        /// <summary>
        /// 自定义保存
        /// </summary>
        [DataMember, JsonProperty("CustomWrite", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _customWrite;

        /// <summary>
        /// 自定义保存
        /// </summary>
        /// <remark>
        /// 自定义保存,如果为真,数据库的写入忽略这个字段,数据的写入由代码自行维护
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据库"), DisplayName(@"自定义保存"), Description(CustomWrite_Description)]
        public bool CustomWrite
        {
            get => _customWrite;
            set
            {
                if (_customWrite == value)
                    return;
                BeforePropertyChanged(nameof(CustomWrite), _customWrite, value);
                _customWrite = value;
                OnPropertyChanged(nameof(CustomWrite));
            }
        }

        /// <summary>
        /// 存储值读写字段的说明文字
        /// </summary>
        const string StorageProperty_Description = @"存储值读写字段(internal),即使用非基础类型时,当发生读写数据库操作时使用的字段,字段为文本(JSON或XML)类型,使用序列化方法读写";

        /// <summary>
        /// 存储值读写字段
        /// </summary>
        [DataMember, JsonProperty("StorageProperty", NullValueHandling = NullValueHandling.Ignore)]
        internal string _storageProperty;

        /// <summary>
        /// 存储值读写字段
        /// </summary>
        /// <remark>
        /// 存储值读写字段(internal),即使用非基础类型时,当发生读写数据库操作时使用的字段,字段为文本(JSON或XML)类型,使用序列化方法读写
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据库"), DisplayName(@"存储值读写字段"), Description(StorageProperty_Description)]
        public string StorageProperty
        {
            get => _storageProperty;
            set
            {
                if (_storageProperty == value)
                    return;
                BeforePropertyChanged(nameof(StorageProperty), _storageProperty, value);
                _storageProperty = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(StorageProperty));
            }
        }
        #endregion
        #region 用户界面

        /// <summary>
        /// 客户端不可见
        /// </summary>
        /// <remark>
        /// 客户端不可见
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"客户端不可见"), Description("客户端不可见")]
        public bool DenyClient
        {
            get => DenyScope.HasFlag(AccessScopeType.Client);
            set
            {
                if (value)
                    DenyScope |= AccessScopeType.Client;
                else
                    DenyScope &= ~AccessScopeType.Client;
            }
        }

        /// <summary>
        /// 用户是否可输入
        /// </summary>
        /// <remark>
        /// 用户是否可输入
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"用户是否可输入"), Description("用户是否可输入")]
        public bool CanUserInput => !DenyClient && !IsUserReadOnly && !IsSystemField && !IsCompute && !(IsIdentity && IsIdentity) && (Parent != null && !Parent.IsUiReadOnly);

        /// <summary>
        /// 不可编辑
        /// </summary>
        [DataMember, JsonProperty("IsUserReadOnly", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isUserReadOnly;

        /// <summary>
        /// 不可编辑
        /// </summary>
        /// <remark>
        /// 是否用户可编辑
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"不可编辑"), Description("是否用户可编辑")]
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
        [DataMember, JsonProperty("MulitLine", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _mulitLine;

        /// <summary>
        /// 多行文本
        /// </summary>
        /// <remark>
        /// 多行文本
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"多行文本"), Description("多行文本")]
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
        /// 前缀
        /// </summary>
        [DataMember, JsonProperty("_prefix", NullValueHandling = NullValueHandling.Ignore)]
        internal string _prefix;

        /// <summary>
        /// 前缀
        /// </summary>
        /// <remark>
        /// 前缀
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"前缀"), Description("前缀")]
        public string Prefix
        {
            get => _prefix;
            set
            {
                if (_prefix == value)
                    return;
                BeforePropertyChanged(nameof(Prefix), _prefix, value);
                _prefix = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(Prefix));
            }
        }

        /// <summary>
        /// 后缀
        /// </summary>
        [DataMember, JsonProperty("_suffix", NullValueHandling = NullValueHandling.Ignore)]
        internal string _suffix;

        /// <summary>
        /// 后缀
        /// </summary>
        /// <remark>
        /// 后缀
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"后缀"), Description("后缀")]
        public string Suffix
        {
            get => _suffix;
            set
            {
                if (_suffix == value)
                    return;
                BeforePropertyChanged(nameof(Suffix), _suffix, value);
                _suffix = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(Suffix));
            }
        }

        /// <summary>
        /// 等同于空值的文本
        /// </summary>
        [DataMember, JsonProperty("EmptyValue", NullValueHandling = NullValueHandling.Ignore)]
        internal string _emptyValue;

        /// <summary>
        /// 等同于空值的文本
        /// </summary>
        /// <remark>
        /// 等同于空值的文本,多个用#号分开
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"等同于空值的文本"), Description("等同于空值的文本,多个用#号分开")]
        public string EmptyValue
        {
            get => _emptyValue;
            set
            {
                if (_emptyValue == value)
                    return;
                BeforePropertyChanged(nameof(EmptyValue), _emptyValue, value);
                _emptyValue = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(EmptyValue));
            }
        }

        /// <summary>
        /// 界面必填字段
        /// </summary>
        [DataMember, JsonProperty("uiRequired", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _uiRequired;

        /// <summary>
        /// 界面必填字段
        /// </summary>
        /// <remark>
        /// 是否必填字段
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"界面必填字段"), Description("界面必填字段")]
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
        [DataMember, JsonProperty("_inputType", NullValueHandling = NullValueHandling.Ignore)]
        internal string _inputType;

        /// <summary>
        /// 输入类型
        /// </summary>
        /// <remark>
        /// 输入类型
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"输入类型"), Description("输入类型")]
        public string InputType
        {
            get => _inputType;
            set
            {
                if (_inputType == value)
                    return;
                BeforePropertyChanged(nameof(InputType), _inputType, value);
                _inputType = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(InputType));
            }
        }

        /// <summary>
        /// Form中占几列宽度
        /// </summary>
        [DataMember, JsonProperty("FormCloumnSapn", NullValueHandling = NullValueHandling.Ignore)]
        internal int _formCloumnSapn;

        /// <summary>
        /// Form中占几列宽度
        /// </summary>
        /// <remark>
        /// Form中占几列宽度
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"Form中占几列宽度"), Description("Form中占几列宽度")]
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
        /// Form中的EasyUi设置
        /// </summary>
        [DataMember, JsonProperty("FormOption", NullValueHandling = NullValueHandling.Ignore)]
        internal string _formOption;

        /// <summary>
        /// Form中的EasyUi设置
        /// </summary>
        /// <remark>
        /// Form中的EasyUi设置
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"Form中的EasyUi设置"), Description("Form中的EasyUi设置")]
        public string FormOption
        {
            get => _formOption;
            set
            {
                if (_formOption == value)
                    return;
                BeforePropertyChanged(nameof(FormOption), _formOption, value);
                _formOption = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(FormOption));
            }
        }

        /// <summary>
        /// 用户排序
        /// </summary>
        [DataMember, JsonProperty("userOrder", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _userOrder;

        /// <summary>
        /// 用户排序
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"用户排序"), Description("用户排序")]
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
        [DataMember, JsonProperty("_comboBoxUrl", NullValueHandling = NullValueHandling.Ignore)]
        internal string _comboBoxUrl;

        /// <summary>
        /// 下拉列表的地址
        /// </summary>
        /// <remark>
        /// 下拉列表的地址
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"下拉列表的地址"), Description("下拉列表的地址")]
        public string ComboBoxUrl
        {
            get => _comboBoxUrl;
            set
            {
                if (_comboBoxUrl == value)
                    return;
                BeforePropertyChanged(nameof(ComboBoxUrl), _comboBoxUrl, value);
                _comboBoxUrl = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(ComboBoxUrl));
            }
        }
        /// <summary>
        /// 是否图片
        /// </summary>
        [DataMember, JsonProperty("isImage", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isImage;

        /// <summary>
        /// 是否图片
        /// </summary>
        /// <remark>
        /// 是否图片
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"是否图片"), Description("是否图片")]
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
        [DataMember, JsonProperty("IsMoney", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isMoney;

        /// <summary>
        /// 货币类型
        /// </summary>
        /// <remark>
        /// 是否货币
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"货币类型"), Description("是否货币")]
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
        [DataMember, JsonProperty("GridAlign", NullValueHandling = NullValueHandling.Ignore)]
        internal string _gridAlign;

        /// <summary>
        /// 表格对齐
        /// </summary>
        /// <remark>
        /// 对齐
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"表格对齐"), Description("对齐")]
        public string GridAlign
        {
            get => _gridAlign;
            set
            {
                if (_gridAlign == value)
                    return;
                BeforePropertyChanged(nameof(GridAlign), _gridAlign, value);
                _gridAlign = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(GridAlign));
            }
        }

        /// <summary>
        /// 占表格宽度比例
        /// </summary>
        [DataMember, JsonProperty("GridWidth", NullValueHandling = NullValueHandling.Ignore)]
        internal int _gridWidth;

        /// <summary>
        /// 占表格宽度比例
        /// </summary>
        /// <remark>
        /// 数据格式器
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"占表格宽度比例"), Description("数据格式器")]
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
        [DataMember, JsonProperty("DataFormater", NullValueHandling = NullValueHandling.Ignore)]
        internal string _dataFormater;

        /// <summary>
        /// 数据格式器
        /// </summary>
        /// <remark>
        /// 数据格式器
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"数据格式器"), Description("数据格式器")]
        public string DataFormater
        {
            get => _dataFormater;
            set
            {
                if (_dataFormater == value)
                    return;
                BeforePropertyChanged(nameof(DataFormater), _dataFormater, value);
                _dataFormater = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(DataFormater));
            }
        }

        /// <summary>
        /// 显示在列表详细页中
        /// </summary>
        [DataMember, JsonProperty("GridDetails", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _gridDetails;

        /// <summary>
        /// 显示在列表详细页中
        /// </summary>
        /// <remark>
        /// 显示在列表详细页中
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"显示在列表详细页中"), Description("显示在列表详细页中")]
        public bool GridDetails
        {
            get => InCoding ? _gridDetails && !NoneJson : _gridDetails;
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
        [DataMember, JsonProperty("NoneGrid", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _noneGrid;

        /// <summary>
        /// 列表不显示
        /// </summary>
        /// <remark>
        /// 列表不显示
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"列表不显示"), Description("列表不显示")]
        public bool NoneGrid
        {
            get => DenyClient || _noneGrid;
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
        [DataMember, JsonProperty("NoneDetails", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _noneDetails;

        /// <summary>
        /// 详细不显示
        /// </summary>
        /// <remark>
        /// 详细不显示
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"详细不显示"), Description("详细不显示")]
        public bool NoneDetails
        {
            get => DenyClient || _noneDetails;
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
        [DataMember, JsonProperty("GridDetailsCode", NullValueHandling = NullValueHandling.Ignore)]
        internal string _gridDetailsCode;

        /// <summary>
        /// 列表详细页代码
        /// </summary>
        /// <remark>
        /// 详细界面代码
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"列表详细页代码"), Description("详细界面代码")]
        public string GridDetailsCode
        {
            get => _gridDetailsCode;
            set
            {
                if (_gridDetailsCode == value)
                    return;
                BeforePropertyChanged(nameof(GridDetailsCode), _gridDetailsCode, value);
                _gridDetailsCode = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(GridDetailsCode));
            }
        }
        #endregion
        #region 数据规则

        /// <summary>
        /// 值说明
        /// </summary>
        [DataMember, JsonProperty("dataRuleDesc", NullValueHandling = NullValueHandling.Ignore)]
        internal string _dataRuleDesc;

        /// <summary>
        /// 值说明
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据规则"), DisplayName(@"数据说明"), Description("对于值数据规则的描述")]
        public string DataRuleDesc
        {
            get => InCoding ? (_dataRuleDesc ?? AutoDataRuleDesc) : _dataRuleDesc;
            set
            {
                if (_dataRuleDesc == value)
                    return;
                BeforePropertyChanged(nameof(DataRuleDesc), _dataRuleDesc, value);
                _dataRuleDesc = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(DataRuleDesc));
            }
        }/// <summary>
         /// 值说明
         /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public string AutoDataRuleDesc
        {
            get
            {
                StringBuilder decs = new StringBuilder();
                bool checkNull;
                switch (CsType?.ToLower() ?? "string")
                {
                    case "string":
                    case "object":
                        checkNull = true;
                        break;
                    default:
                        checkNull = Nullable || IsArray || IsDictionary;
                        break;
                }
                if (checkNull)
                {
                    if (IsRequired)
                    {
                        decs.Append("用户提交时不能为空,");
                    }
                    if (!CanEmpty)
                    {
                        decs.Append("后台保存时不能为空,");
                    }
                }

                if (CsType == "string")
                {
                    string max = Max;
                    if (!IsBlob && !IsMemo && Datalen > 0)
                    {
                        decs.Append($"可存储{Datalen}个字符.");
                        if ((int.TryParse(max, out var len) && len > Datalen))
                        {
                            max = Max = null;
                        }
                        if (string.IsNullOrEmpty(max))
                            max = Datalen.ToString();
                    }

                    if (!string.IsNullOrEmpty(Min) && !string.IsNullOrEmpty(max))
                    {
                        decs.Append($"合理长度在{Min}-{max}之间.");
                    }
                    else if (!string.IsNullOrEmpty(Min))
                    {
                        decs.Append($"合理长度应不小于{Min}.");
                    }
                    else if (!string.IsNullOrEmpty(max))
                    {
                        decs.Append($"合理长度应不大于{max}.");
                    }
                }
                else
                {
                    string max = Max;
                    if (!string.IsNullOrEmpty(Min) && !string.IsNullOrEmpty(max))
                    {
                        decs.Append($"合理值(大等于){Min}且(小于){max}.");
                    }
                    else if (!string.IsNullOrEmpty(Min))
                    {
                        decs.Append($"合理值不应小于{Min}.");
                    }
                    else if (!string.IsNullOrEmpty(max))
                    {
                        decs.Append($"合理值不应大于{max}.");
                    }
                }
                return decs.ToString();
            }
        }

        /// <summary>
        /// 校验代码
        /// </summary>
        [DataMember, JsonProperty("_validateCode", NullValueHandling = NullValueHandling.Ignore)]
        private string _validateCode;
        /// <summary>
        /// 校验代码
        /// </summary>
        /// <remark>
        /// 校验代码,本字段用{0}代替
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据规则"), DisplayName(@"校验代码"), Description("校验代码,本字段用{0}代替")]
        public string ValidateCode
        {
            get => _validateCode;
            set
            {
                if (_validateCode == value)
                    return;
                BeforePropertyChanged(nameof(ValidateCode), _validateCode, value);
                _validateCode = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(ValidateCode));
            }
        }

        /// <summary>
        /// 能否为空的说明文字
        /// </summary>
        const string CanEmpty_Description = @"这是数据相关的逻辑,表示在存储时必须写入数据,否则逻辑不正确";

        /// <summary>
        /// 能否为空
        /// </summary>
        [DataMember, JsonProperty("CanEmpty", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _canEmpty;

        /// <summary>
        /// 能否为空
        /// </summary>
        /// <remark>
        /// 这是数据相关的逻辑,表示在存储时必须写入数据,否则逻辑不正确
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据规则"), DisplayName(@"能否为空"), Description(CanEmpty_Description)]
        public bool CanEmpty
        {
            get => InterfaceOrThis._canEmpty;
            set
            {
                if (_canEmpty == value)
                    return;
                BeforePropertyChanged(nameof(CanEmpty), _canEmpty, value);
                _canEmpty = value;
                OnPropertyChanged(nameof(CanEmpty));
                OnPropertyChanged(nameof(AutoDataRuleDesc));
            }
        }

        /// <summary>
        /// 必填字段
        /// </summary>
        [DataMember, JsonProperty("_isRequired", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isRequired;

        /// <summary>
        /// 必填字段
        /// </summary>
        /// <remark>
        /// 是否必填字段
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"必填字段"), Description("是否必填字段")]
        public bool IsRequired
        {
            get => !CanEmpty || _isRequired;
            set
            {
                if (_isRequired == value)
                    return;
                BeforePropertyChanged(nameof(IsRequired), _isRequired, value);
                _isRequired = value;
                OnPropertyChanged(nameof(IsRequired));
                OnPropertyChanged(nameof(AutoDataRuleDesc));
            }
        }
        /// <summary>
        /// 最大值
        /// </summary>
        [DataMember, JsonProperty("Max", NullValueHandling = NullValueHandling.Ignore)]
        internal string _max;

        /// <summary>
        /// 最大值
        /// </summary>
        /// <remark>
        /// 最大
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据规则"), DisplayName(@"最大值"), Description("最大")]
        public string Max
        {
            get => _max;
            set
            {
                if (_max == value)
                    return;
                BeforePropertyChanged(nameof(Max), _max, value);
                _max = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(Max));
                OnPropertyChanged(nameof(AutoDataRuleDesc));
            }
        }

        /// <summary>
        /// 最大值
        /// </summary>
        [DataMember, JsonProperty("Min", NullValueHandling = NullValueHandling.Ignore)]
        internal string _min;

        /// <summary>
        /// 最大值
        /// </summary>
        /// <remark>
        /// 最小
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据规则"), DisplayName(@"最大值"), Description("最小")]
        public string Min
        {
            get => _min;
            set
            {
                if (_min == value)
                    return;
                BeforePropertyChanged(nameof(Min), _min, value);
                _min = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(Min));
                OnPropertyChanged(nameof(AutoDataRuleDesc));
            }
        }
        #endregion
        #region 数据关联

        /// <summary>
        /// 连接字段
        /// </summary>
        [DataMember, JsonProperty("IsLinkField", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isLinkField;

        /// <summary>
        /// 连接字段
        /// </summary>
        /// <remark>
        /// 连接字段
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据关联"), DisplayName(@"连接字段"), Description("连接字段")]
        public bool IsLinkField
        {
            get => IsLinkKey || IsLinkCaption || _isLinkField;
            set
            {
                if (_isLinkField == value)
                    return;
                BeforePropertyChanged(nameof(IsLinkField), _isLinkField, value);
                _isLinkField = value;
                OnPropertyChanged(nameof(IsLinkField));
            }
        }

        /// <summary>
        /// 关联表名
        /// </summary>
        [DataMember, JsonProperty("LinkTable", NullValueHandling = NullValueHandling.Ignore)]
        internal string _linkTable;

        /// <summary>
        /// 关联表名
        /// </summary>
        /// <remark>
        /// 关联表名
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据关联"), DisplayName(@"关联表名"), Description("关联表名")]
        public string LinkTable
        {
            get => _linkTable;
            set
            {
                if (_linkTable == value)
                    return;
                if (Parent != null && (string.Equals(value, Parent.Name, StringComparison.OrdinalIgnoreCase) ||
                                       string.Equals(value, Parent.SaveTableName, StringComparison.OrdinalIgnoreCase) ||
                                       string.Equals(value, Parent.ReadTableName, StringComparison.OrdinalIgnoreCase)))
                {
                    value = null;
                }
                BeforePropertyChanged(nameof(LinkTable), _linkTable, value);
                _linkTable = string.IsNullOrWhiteSpace(value) ? null : value.Trim();

                OnPropertyChanged(nameof(LinkTable));
            }
        }

        /// <summary>
        /// 关联表主键
        /// </summary>
        [DataMember, JsonProperty("IsLinkKey", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isLinkKey;

        /// <summary>
        /// 关联表主键
        /// </summary>
        /// <remark>
        /// 关联表主键,即与另一个实体关联的外键
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据关联"), DisplayName(@"关联表主键"), Description("关联表主键,即与另一个实体关联的外键")]
        public bool IsLinkKey
        {
            get => _isLinkKey;
            set
            {
                if (_isLinkKey == value)
                    return;
                BeforePropertyChanged(nameof(IsLinkKey), _isLinkKey, value);
                _isLinkKey = value;
                OnPropertyChanged(nameof(IsLinkKey));
            }
        }

        /// <summary>
        /// 关联表标题
        /// </summary>
        [DataMember, JsonProperty("IsLinkCaption", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isLinkCaption;

        /// <summary>
        /// 关联表标题
        /// </summary>
        /// <remark>
        /// 关联表标题,即此字段为关联表的标题内容
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据关联"), DisplayName(@"关联表标题"), Description("关联表标题,即此字段为关联表的标题内容")]
        public bool IsLinkCaption
        {
            get => _isLinkCaption;
            set
            {
                if (_isLinkCaption == value)
                    return;
                BeforePropertyChanged(nameof(IsLinkCaption), _isLinkCaption, value);
                _isLinkCaption = value;
                OnPropertyChanged(nameof(IsLinkCaption));
            }
        }

        /// <summary>
        /// 对应客户ID
        /// </summary>
        [DataMember, JsonProperty("IsUserId", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isUserId;

        /// <summary>
        /// 对应客户ID
        /// </summary>
        /// <remark>
        /// 是对应的UID,已过时,原来用于龙之战鼓
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据关联"), DisplayName(@"对应客户ID"), Description("是对应的UID,已过时,原来用于龙之战鼓")]
        public bool IsUserId
        {
            get => _isUserId;
            set
            {
                if (_isUserId == value)
                    return;
                BeforePropertyChanged(nameof(IsUserId), _isUserId, value);
                _isUserId = value;
                OnPropertyChanged(nameof(IsUserId));
            }
        }

        /// <summary>
        /// 关联字段名称
        /// </summary>
        [DataMember, JsonProperty("LinkField", NullValueHandling = NullValueHandling.Ignore)]
        internal string _linkField;

        /// <summary>
        /// 关联字段名称
        /// </summary>
        /// <remark>
        /// 关联字段名称,即在关联表中的字段名称
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据关联"), DisplayName(@"关联字段名称"), Description("关联字段名称,即在关联表中的字段名称")]
        public string LinkField
        {
            get => _linkField;
            set
            {
                if (_linkField == value)
                    return;
                BeforePropertyChanged(nameof(LinkField), _linkField, value);
                _linkField = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(LinkField));
            }
        }
        #endregion
    }
}