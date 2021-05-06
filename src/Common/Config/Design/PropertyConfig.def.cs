// // /*****************************************************
// // (c)2016-2016 Copy right Agebull.hu
// // 作者:
// // 工程:CodeRefactor
// // 建立:2016-09-18
// // 修改:2016-09-18
// // *****************************************************/

namespace Agebull.EntityModel.Config
{
    partial class PropertyConfig
    {

        #region 预定义

        #endregion

        #region 扩展
        /*
       /// <summary>
       /// 是否关系字段
       /// </summary>
       [DataMember, JsonProperty("isRelationField",  DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
       internal bool _isRelationField;

       /// <summary>
       /// 是否关系字段
       /// </summary>
       /// <remark>
       /// 是否关系字段
       /// </remark>
       [JsonIgnore]
       [Category(@""), DisplayName(@"是否关系字段"), Description("是否关系字段")]
       public bool IsRelationField
       {
           get => _isRelationField;
           set
           {
               if (_isRelationField == value)
                   return;
               BeforePropertyChange(nameof(IsRelationField), _isRelationField, value);
               _isRelationField = value;
               OnPropertyChanged(nameof(IsRelationField));
           }
       }

       /// <summary>
       /// 是否关系值
       /// </summary>
       [DataMember, JsonProperty("isRelationValue",  DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
       internal bool _isRelationValue;

       /// <summary>
       /// 是否关系值
       /// </summary>
       /// <remark>
       /// 是否关系值
       /// </remark>
       [JsonIgnore]
       [Category(@""), DisplayName(@"是否关系值"), Description("是否关系值")]
       public bool IsRelationValue
       {
           get => _isRelationValue;
           set
           {
               if (_isRelationValue == value)
                   return;
               BeforePropertyChange(nameof(IsRelationValue), _isRelationValue, value);
               _isRelationValue = value;
               OnPropertyChanged(nameof(IsRelationValue));
           }
       }

       /// <summary>
       /// 是否关系数组
       /// </summary>
       [DataMember, JsonProperty("isRelationArray",  DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
       internal bool _isRelationArray;

       /// <summary>
       /// 是否关系数组
       /// </summary>
       /// <remark>
       /// 是否关系数组
       /// </remark>
       [JsonIgnore]
       [Category(@""), DisplayName(@"是否关系数组"), Description("是否关系数组")]
       public bool IsRelationArray
       {
           get => _isRelationArray;
           set
           {
               if (_isRelationArray == value)
                   return;
               BeforePropertyChange(nameof(IsRelationArray), _isRelationArray, value);
               _isRelationArray = value;
               OnPropertyChanged(nameof(IsRelationArray));
           }
       }

       /// <summary>
       /// 是否扩展数组
       /// </summary>
       [DataMember, JsonProperty("isExtendArray",  DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
       internal bool _isExtendArray;

       /// <summary>
       /// 是否扩展数组
       /// </summary>
       /// <remark>
       /// 是否扩展数组
       /// </remark>
       [JsonIgnore]
       [Category(@""), DisplayName(@"是否扩展数组"), Description("是否扩展数组")]
       public bool IsExtendArray
       {
           get => _isExtendArray;
           set
           {
               if (_isExtendArray == value)
                   return;
               BeforePropertyChange(nameof(IsExtendArray), _isExtendArray, value);
               _isExtendArray = value;
               OnPropertyChanged(nameof(IsExtendArray));
           }
       }
       */
        /*// <summary>
        ///     是否关系字段
        /// </summary>
        [Browsable(false)]
        public bool IsRelationField => IsRelation && !string.IsNullOrWhiteSpace(ExtendRole);

        /// <summary>
        ///     是否关系值
        /// </summary>
        [Browsable(false)]
        public bool IsRelationValue => IsRelation && !ExtendArray && !string.IsNullOrWhiteSpace(ExtendRole);

        /// <summary>
        ///     是否关系数组
        /// </summary>
        [Browsable(false)]
        public bool IsRelationArray => IsRelation && ExtendArray && !string.IsNullOrWhiteSpace(ExtendRole);
        /// <summary>
        /// 是否扩展数组
        /// </summary>
        [Browsable(false)]
        public bool IsExtendArray => !IsRelation && ExtendArray && !string.IsNullOrWhiteSpace(ExtendRole);

        /// <summary>
        /// 是否扩展值
        /// </summary>
        [Browsable(false)]
        public bool IsExtendValue => !IsRelation && !ExtendArray && !string.IsNullOrWhiteSpace(ExtendRole);
        */
        #endregion

        #region 扩展配置(过时)
        /*
    /// <summary>
    /// 扩展组合规划的说明文字
    /// </summary>
    const string ExtendRole_Description = @"扩展组合规划,
类JSON方式即[名称:类型]多个用逗号分开,
类型说明:#表名,%小数,*整数,@日期,!文本(默认,可不填)
例  ID:*,颜色,EID:#Equipments 解析结果: 两个属性的对象,ID为整数,颜色为文本,EID为关联到Equipments表的主键字段
特殊说明:如果是否为关系表为真,只按 表名 解析成表间一对多关系";

    /// <summary>
    /// 扩展组合规划
    /// </summary>
    [DataMember, JsonProperty("ExtendRole",  DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
    internal string _extendRole;

    /// <summary>
    /// 扩展组合规划
    /// </summary>
    /// <remark>
    /// 扩展组合规划,
    /// 类JSON方式即[名称:类型]多个用逗号分开,
    /// 类型说明:#表名,%小数,*整数,@日期,!文本(默认,可不填)
    /// 例  ID:*,颜色,EID:#Equipments 解析结果: 两个属性的对象,ID为整数,颜色为文本,EID为关联到Equipments表的主键字段
    /// 特殊说明:如果是否为关系表为真,只按 表名 解析成表间一对多关系
    /// </remark>
    [JsonIgnore]
    [Category(@"扩展配置(过时)"), DisplayName(@"扩展组合规划"), Description(ExtendRole_Description)]
    public string ExtendRole
    {
        get => _extendRole;
        set
        {
            if (_extendRole == value)
                return;
            BeforePropertyChange(nameof(ExtendRole), _extendRole, value);
            _extendRole = value.SafeTrim();
            OnPropertyChanged(nameof(ExtendRole));
        }
    }

    /// <summary>
    /// 值分隔符
    /// </summary>
    [DataMember, JsonProperty("ValueSeparate",  DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
    internal string _valueSeparate;

    /// <summary>
    /// 值分隔符
    /// </summary>
    /// <remark>
    /// 值分隔符
    /// </remark>
    [JsonIgnore]
    [Category(@"扩展配置(过时)"), DisplayName(@"值分隔符"), Description("值分隔符")]
    public string ValueSeparate
    {
        get => _valueSeparate;
        set
        {
            if (_valueSeparate == value)
                return;
            BeforePropertyChange(nameof(ValueSeparate), _valueSeparate, value);
            _valueSeparate = value.SafeTrim();
            OnPropertyChanged(nameof(ValueSeparate));
        }
    }

    /// <summary>
    /// 数组分隔符
    /// </summary>
    [DataMember, JsonProperty("ArraySeparate",  DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
    internal string _arraySeparate;

    /// <summary>
    /// 数组分隔符
    /// </summary>
    /// <remark>
    /// 数组分隔符
    /// </remark>
    [JsonIgnore]
    [Category(@"扩展配置(过时)"), DisplayName(@"数组分隔符"), Description("数组分隔符")]
    public string ArraySeparate
    {
        get => _arraySeparate;
        set
        {
            if (_arraySeparate == value)
                return;
            BeforePropertyChange(nameof(ArraySeparate), _arraySeparate, value);
            _arraySeparate = value.SafeTrim();
            OnPropertyChanged(nameof(ArraySeparate));
        }
    }

    /// <summary>
    /// 是否扩展数组的说明文字
    /// </summary>
    const string ExtendArray_Description = @"是否扩展数组,是则解析为二维数组,否解析为一维数组";

    /// <summary>
    /// 是否扩展数组
    /// </summary>
    [DataMember, JsonProperty("ExtendArray",  DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
    internal bool _extendArray;

    /// <summary>
    /// 是否扩展数组
    /// </summary>
    /// <remark>
    /// 是否扩展数组,是则解析为二维数组,否解析为一维数组
    /// </remark>
    [JsonIgnore]
    [Category(@"扩展配置(过时)"), DisplayName(@"是否扩展数组"), Description(ExtendArray_Description)]
    public bool ExtendArray
    {
        get => _extendArray;
        set
        {
            if (_extendArray == value)
                return;
            BeforePropertyChange(nameof(ExtendArray), _extendArray, value);
            _extendArray = value;
            OnPropertyChanged(nameof(ExtendArray));
        }
    }

    /// <summary>
    /// 是否值对分隔方式的说明文字
    /// </summary>
    const string IsKeyValueArray_Description = @"是否值对分隔方式,
如果是每组为一种类型的对象,否则一组是单个对象,举例如下:
对象实际JSON表示方式为[{ID:1,颜色:黄},{ID:2,颜色:红},{ID:3,颜色:绿}]
是的情况:1,2,3#红,黄,绿,第一组是ID属性,第二组是颜色属性,解析后为
否的情况:1,红#2,黄#3,绿,分别对应三组";

    /// <summary>
    /// 是否值对分隔方式
    /// </summary>
    [DataMember, JsonProperty("IsKeyValueArray",  DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
    internal bool _isKeyValueArray;

    /// <summary>
    /// 是否值对分隔方式
    /// </summary>
    /// <remark>
    /// 是否值对分隔方式,
    /// 如果是每组为一种类型的对象,否则一组是单个对象,举例如下:
    /// 对象实际JSON表示方式为[{ID:1,颜色:黄},{ID:2,颜色:红},{ID:3,颜色:绿}]
    /// 是的情况:1,2,3#红,黄,绿,第一组是ID属性,第二组是颜色属性,解析后为
    /// 否的情况:1,红#2,黄#3,绿,分别对应三组
    /// </remark>
    [JsonIgnore]
    [Category(@"扩展配置(过时)"), DisplayName(@"是否值对分隔方式"), Description(IsKeyValueArray_Description)]
    public bool IsKeyValueArray
    {
        get => _isKeyValueArray;
        set
        {
            if (_isKeyValueArray == value)
                return;
            BeforePropertyChange(nameof(IsKeyValueArray), _isKeyValueArray, value);
            _isKeyValueArray = value;
            OnPropertyChanged(nameof(IsKeyValueArray));
        }
    }

    /// <summary>
    /// 是否为关系表的说明文字
    /// </summary>
    const string IsRelation_Description = @"是否为关系表,是则扩展组合规划 按 表名 解析成表间一对多关系";

    /// <summary>
    /// 是否为关系表
    /// </summary>
    [DataMember, JsonProperty("IsRelation",  DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
    internal bool _isRelation;

    /// <summary>
    /// 是否为关系表
    /// </summary>
    /// <remark>
    /// 是否为关系表,是则扩展组合规划 按 表名 解析成表间一对多关系
    /// </remark>
    [JsonIgnore]
    [Category(@"扩展配置(过时)"), DisplayName(@"是否为关系表"), Description(IsRelation_Description)]
    public bool IsRelation
    {
        get => _isRelation;
        set
        {
            if (_isRelation == value)
                return;
            BeforePropertyChange(nameof(IsRelation), _isRelation, value);
            _isRelation = value;
            OnPropertyChanged(nameof(IsRelation));
        }
    }

    /// <summary>
    /// 扩展对象属性名称
    /// </summary>
    [DataMember, JsonProperty("ExtendPropertyName",  DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
    internal string _extendPropertyName;

    /// <summary>
    /// 扩展对象属性名称
    /// </summary>
    /// <remark>
    /// 扩展对象属性名称
    /// </remark>
    [JsonIgnore]
    [Category(@"扩展配置(过时)"), DisplayName(@"扩展对象属性名称"), Description("扩展对象属性名称")]
    public string ExtendPropertyName
    {
        get => _extendPropertyName;
        set
        {
            if (_extendPropertyName == value)
                return;
            BeforePropertyChange(nameof(ExtendPropertyName), _extendPropertyName, value);
            _extendPropertyName = value.SafeTrim();
            OnPropertyChanged(nameof(ExtendPropertyName));
        }
    }

    /// <summary>
    /// 扩展对象对象名称
    /// </summary>
    [DataMember, JsonProperty("ExtendClassName",  DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
    internal string _extendClassName;

    /// <summary>
    /// 扩展对象对象名称
    /// </summary>
    /// <remark>
    /// 扩展对象对象名称
    /// </remark>
    [JsonIgnore]
    [Category(@"扩展配置(过时)"), DisplayName(@"扩展对象对象名称"), Description("扩展对象对象名称")]
    public string ExtendClassName
    {
        get => _extendClassName;
        set
        {
            if (_extendClassName == value)
                return;
            BeforePropertyChange(nameof(ExtendClassName), _extendClassName, value);
            _extendClassName = value.SafeTrim();
            OnPropertyChanged(nameof(ExtendClassName));
        }
    }

    /// <summary>
    /// 扩展对象对象已定义
    /// </summary>
    [DataMember, JsonProperty("ExtendClassIsPredestinate",  DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
    internal bool _extendClassIsPredestinate;

    /// <summary>
    /// 扩展对象对象已定义
    /// </summary>
    /// <remark>
    /// 扩展对象对象已定义
    /// </remark>
    [JsonIgnore]
    [Category(@"扩展配置(过时)"), DisplayName(@"扩展对象对象已定义"), Description("扩展对象对象已定义")]
    public bool ExtendClassIsPredestinate
    {
        get => _extendClassIsPredestinate;
        set
        {
            if (_extendClassIsPredestinate == value)
                return;
            BeforePropertyChange(nameof(ExtendClassIsPredestinate), _extendClassIsPredestinate, value);
            _extendClassIsPredestinate = value;
            OnPropertyChanged(nameof(ExtendClassIsPredestinate));
        }
    }*/
        #endregion
    }
}