﻿/*design by:agebull designer date:2017/7/12 22:06:40*/
/*****************************************************
©2008-2017 Copy right by agebull.hu(胡天水)
作者:agebull.hu(胡天水)
工程:Agebull.Common.Config
建立:2014-12-03
修改:2017-07-12
*****************************************************/

using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 属性配置
    /// </summary>
    partial class PropertyConfig
    {
        #region 环境

        /// <summary>
        /// 上级
        /// </summary>
        IEntityConfig IDesignField.Parent => Model;

        /// <summary>
        /// 上级
        /// </summary>
        /// <remark>
        /// 上级
        /// </remark>
        [JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"上级"), Description("上级")]
        public EntityConfig Entity
        {
            get => Field.Entity; set => Field.Entity = value;
        }

        #endregion
        #region 模型设计(C#)


        /// <summary>
        /// 是否扩展数组
        /// </summary>
        /// <remark>
        /// 是否扩展数组
        /// </remark>
        [JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"是否数组"), Description("是否数组")]
        public bool IsArray
        {
            get => Field.IsArray; set => Field.IsArray = value;
        }

        /// <summary>
        /// 数组长度
        /// </summary>
        /// <remark>
        /// 数组长度
        /// </remark>
        [JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"数组长度"), Description("数组长度")]
        public string ArrayLen
        {
            get => Field.ArrayLen;
            set => Field.ArrayLen = value;
        }

        /// <summary>
        /// 是否字典
        /// </summary>
        /// <remark>
        /// 是否扩展数组
        /// </remark>
        [JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"是否字典"), Description("是否字典")]
        public bool IsDictionary
        {
            get => Field.IsDictionary; set => Field.IsDictionary = value;
        }

        /// <summary>
        /// 枚举类型(C#)
        /// </summary>
        /// <remark>
        /// 字段类型
        /// </remark>
        [JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"是否枚举类型)"), Description("字段类型")]
        public bool IsEnum
        {
            get => Field.IsEnum; set => Field.IsEnum = value;
        }


        /// <summary>
        /// 非基本类型名称(C#)
        /// </summary>
        /// <remark>
        /// 字段类型
        /// </remark>
        [JsonIgnore]
        [Category(@"模型设计(C#)"), DisplayName(@"非基本类型名称(C#)"), Description("字段类型")]
        public string CustomType
        {
            get => Field.CustomType; set => Field.CustomType = value;
        }

        /// <summary>
        /// 参考类型
        /// </summary>
        /// <remark>
        /// 字段类型
        /// </remark>
        [JsonIgnore]
        [Category(@"模型设计(C#)"), DisplayName(@"参考类型(C#)"), Description("字段类型")]
        public string ReferenceType
        {
            get => Field.ReferenceType; set => Field.ReferenceType = value;
        }

        /// <summary>
        /// 结果类型(C#)
        /// </summary>
        /// <remark>
        /// 最终生成C#代码时的属性类型
        /// </remark>
        [JsonIgnore]
        [Category(@"模型设计(C#)"), DisplayName(@"结果类型(C#)"), Description("最终生成C#代码时的属性类型")]
        public string LastCsType => ToLastCsType();

        string ToLastCsType()
        {
            if (_csType == null)
                return Field.ToLastCsType();
            if (!string.IsNullOrWhiteSpace(ReferenceType))
                return ReferenceType;
            if (!string.IsNullOrWhiteSpace(CustomType))
                return CustomType;

            if (_csType.Contains("["))
            {
                CsType = _csType.Split('[')[0];
                IsArray = true;
            }
            if (IsArray)
                return $"{_csType}[]";
            if (string.Equals(_csType, "string", StringComparison.OrdinalIgnoreCase))
                return _csType;
            return Nullable ? $"{_csType}?" : _csType;
        }

        /// <summary>
        /// 可空类型(C#)
        /// </summary>
        /// <remark>
        /// 即生成的C#代码,类型为空类型Nullable〈T〉 ,如int?
        /// </remark>
        [JsonIgnore]
        [Category(@"模型设计(C#)"), DisplayName(@"可空类型(C#)")]
        public bool Nullable
        {
            get => Field.Nullable; set => Field.Nullable = value;
        }
        #endregion
        #region 模型设计

        /// <summary>
        /// 是否扩展值
        /// </summary>
        /// <remark>
        /// 是否扩展值
        /// </remark>
        [JsonIgnore]
        [Category(@""), DisplayName(@"是否扩展值"), Description("是否扩展值")]
        public bool IsExtendValue
        {
            get => Field.Nullable; set => Field.Nullable = value;
        }

        /// <summary>
        /// 对应枚举
        /// </summary>
        /// <remark>
        /// 当使用自定义类型时的枚举对象
        /// </remark>
        [JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"对应枚举"), Description("当使用自定义类型时的枚举对象")]
        public string EnumKey
        {
            get => Field.EnumKey; set => Field.EnumKey = value;
        }

        /// <summary>
        /// 对应枚举
        /// </summary>
        /// <remark>
        /// 当使用自定义类型时的枚举对象
        /// </remark>
        [JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"对应枚举"), Description("当使用自定义类型时的枚举对象")]
        public EnumConfig EnumConfig
        {
            get => Field.EnumConfig; set => Field.EnumConfig = value;
        }


        /// <summary>
        /// 系统字段
        /// </summary>
        /// <remark>
        /// 系统字段
        /// </remark>
        [JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"系统字段"), Description("系统字段")]
        public bool IsSystemField
        {
            get => Field.IsSystemField; set => Field.IsSystemField = value;
        }

        /// <summary>
        /// 接口字段
        /// </summary>
        /// <remark>
        /// 是否接口字段
        /// </remark>
        [JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"接口字段"), Description("是否接口字段")]
        public bool IsInterfaceField
        {
            get => Field.IsInterfaceField;
            set => Field.IsInterfaceField = value;
        }

        #endregion
        #region 模型设计(C++)

        /// <summary>
        /// 私有字段
        /// </summary>
        /// <remark>
        /// 私有字段,不应该复制
        /// </remark>
        [JsonIgnore]
        [Category(@"模型设计(C++)"), DisplayName(@"私有字段"), Description("私有字段,不应该复制")]
        public bool IsPrivateField
        {
            get => Field.IsPrivateField; set => Field.IsPrivateField = value;
        }

        /// <summary>
        /// 设计时字段
        /// </summary>
        /// <remark>
        /// 设计时使用的中间过程字段,即最终使用时不需要的字段
        /// </remark>
        [JsonIgnore]
        [Category(@"模型设计(C++)"), DisplayName(@"设计时字段")]
        public bool IsMiddleField
        {
            get => Field.IsMiddleField; set => Field.IsMiddleField = value;
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
        [JsonIgnore]
        [Category(@"模型设计(C++)"), DisplayName(@"6位小数的整数"), Description(IsIntDecimal_Description)]
        public bool IsIntDecimal => Field.IsIntDecimal;

        #endregion
        #region 模型设计(计算列)

        /// <summary>
        /// 可读
        /// </summary>
        /// <remark>
        /// 可读,即可以生成Get代码
        /// </remark>
        [JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"可读"), Description("可读,可以生成Get代码")]
        public bool CanGet
        {
            get => Field.CanGet; set => Field.CanGet = value;
        }

        /// <summary>
        /// 可写
        /// </summary>
        /// <remark>
        /// 可写,即生成SET代码
        /// </remark>
        [JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"可写"), Description("可写,即生成SET代码")]
        public bool CanSet
        {
            get => Field.CanSet; set => Field.CanSet = value;
        }
        /// <summary>
        /// 计算列
        /// </summary>
        /// <remark>
        /// 是否计算列，即数据源于其它字段.如关系引用字段
        /// </remark>
        [JsonIgnore]
        [Category(@"模型设计(计算列)"), DisplayName(@"计算列")]
        public bool IsCompute
        {
            get => Field.IsCompute;
            set => Field.IsCompute = value;
        }


        #endregion

        #region 数据标识

        /// <summary>
        /// 标题字段
        /// </summary>
        /// <remark>
        /// 标题字段
        /// </remark>
        [JsonIgnore]
        [Category(@"数据标识"), DisplayName(@"标题字段"), Description("标题字段")]
        public bool IsCaption
        {
            get => Field.IsCaption; set => Field.IsCaption = value;
        }

        /// <summary>
        /// 上级外键
        /// </summary>
        [JsonIgnore]
        [Category(@"数据标识"), DisplayName(@"上级外键"), Description("上级外键")]
        public bool IsParent
        {
            get => Field.IsParent; set => Field.IsParent = value;
        }

        /// <summary>
        /// 主键字段
        /// </summary>
        /// <remark>
        /// 主键
        /// </remark>
        [JsonIgnore]
        [Category(@"数据标识"), DisplayName(@"主键字段"), Description("主键")]
        public bool IsPrimaryKey
        {
            get => Field.IsPrimaryKey; set => Field.IsPrimaryKey = value;
        }

        /// <summary>
        /// 唯一值字段
        /// </summary>
        /// <remark>
        /// 即它也是唯一标识符,如用户的身份证号
        /// </remark>
        [JsonIgnore]
        [Category(@"数据标识"), DisplayName(@"唯一值字段"), Description("即它也是唯一标识符,如用户的身份证号")]
        public bool IsExtendKey
        {
            get => Field.IsExtendKey; set => Field.IsExtendKey = value;
        }

        /// <summary>
        /// 全局标识
        /// </summary>
        /// <remark>
        /// 是否使用GUID的全局KEY
        /// </remark>
        [JsonIgnore]
        [Category(@"数据标识"), DisplayName(@"全局标识"), Description("是否使用GUID的全局KEY")]
        public bool IsGlobalKey
        {
            get => Field.IsGlobalKey; set => Field.IsGlobalKey = value;
        }

        /// <summary>
        /// 唯一属性组合顺序
        /// </summary>
        /// <remark>
        /// 参与组合成唯一属性的顺序,大于0有效
        /// </remark>
        [JsonIgnore]
        [Category(@"数据标识"), DisplayName(@"唯一属性组合顺序"), Description("参与组合成唯一属性的顺序,大于0有效")]
        public bool UniqueIndex
        {
            get => Field.UniqueIndex; set => Field.UniqueIndex = value;
        }
        /// <summary>
        /// 唯一文本
        /// </summary>
        /// <remark>
        /// 5是否唯一文本
        /// </remark>
        [JsonIgnore]
        [Category(@"数据标识"), DisplayName(@"唯一文本"), Description("5是否唯一文本")]
        public bool UniqueString
        {
            get => Field.UniqueString; set => Field.UniqueString = value;
        }
        #endregion
        #region 用户界面

        /// <summary>
        /// 用户可见
        /// </summary>
        /// <remark>
        /// 用户可见
        /// </remark>
        [JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"用户可见"), Description("用户可见")]
        public bool UserSee => !InnerField && !NoProperty;

        /// <summary>
        /// 不可编辑
        /// </summary>
        /// <remark>
        /// 是否用户可编辑
        /// </remark>
        [JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"不可编辑"), Description("是否用户可编辑")]
        public bool IsUserReadOnly
        {
            get => Field.IsUserReadOnly;
            set => Field.IsUserReadOnly = value;
        }

        /// <summary>
        /// 用户是否可查询
        /// </summary>
        public bool CanUserQuery
        {
            get => Field.CanUserQuery;
            set => Field.CanUserQuery = value;
        }
        /// <summary>
        /// 多行文本
        /// </summary>
        /// <remark>
        /// 多行文本
        /// </remark>
        [JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"多行文本"), Description("多行文本")]
        public bool MulitLine
        {
            get => Field.MulitLine; set => Field.MulitLine = value;
        }

        /// <summary>
        /// 多行文本的行数
        /// </summary>
        [DataMember, JsonProperty("rows", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal int rows;

        /// <summary>
        /// 多行文本的行数
        /// </summary>
        /// <remark>
        /// 多行文本的行数，默认为3行
        /// </remark>
        [JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"行数"), Description("行数")]
        public int Rows
        {
            get => rows <= 0 ? 3 : rows;
            set
            {
                if (rows == value)
                    return;
                BeforePropertyChange(nameof(Rows), rows, value);
                rows = value;
                OnPropertyChanged(nameof(Rows));
            }
        }

        /// <summary>
        /// 前缀
        /// </summary>
        /// <remark>
        /// 前缀
        /// </remark>
        [JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"前缀"), Description("前缀")]
        public string Prefix
        {
            get => Field.Prefix;
            set => Field.Prefix = value;
        }

        /// <summary>
        /// 后缀
        /// </summary>
        /// <remark>
        /// 后缀
        /// </remark>
        [JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"后缀"), Description("后缀")]
        public string Suffix
        {
            get => Field.Suffix; set => Field.Suffix = value;
        }

        /// <summary>
        /// 等同于空值的文本
        /// </summary>
        /// <remark>
        /// 等同于空值的文本,多个用#号分开
        /// </remark>
        [JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"等同于空值的文本"), Description("等同于空值的文本,多个用#号分开")]
        public string EmptyValue
        {
            get => Field.EmptyValue; set => Field.EmptyValue = value;
        }


        /// <summary>
        /// 界面必填字段
        /// </summary>
        /// <remark>
        /// 是否必填字段
        /// </remark>
        [JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"界面必填字段"), Description("界面必填字段")]
        public bool UiRequired
        {
            get => Field.UiRequired; set => Field.UiRequired = value;
        }

        /// <summary>
        /// 输入类型
        /// </summary>
        /// <remark>
        /// 输入类型
        /// </remark>
        [JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"输入类型"), Description("输入类型")]
        public string InputType
        {
            get => Field.InputType; set => Field.InputType = value;
        }

        /// <summary>
        /// Form中占几列宽度
        /// </summary>
        /// <remark>
        /// Form中占几列宽度
        /// </remark>
        [JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"Form中占几列宽度"), Description("Form中占几列宽度")]
        public int FormCloumnSapn
        {
            get => Field.FormCloumnSapn; set => Field.FormCloumnSapn = value;
        }

        /// <summary>
        /// Form中的EasyUi设置
        /// </summary>
        /// <remark>
        /// Form中的EasyUi设置
        /// </remark>
        [JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"Form中的EasyUi设置"), Description("Form中的EasyUi设置")]
        public string FormOption
        {
            get => Field.FormOption; set => Field.FormOption = value;
        }

        /// <summary>
        /// 用户排序
        /// </summary>
        [JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"用户排序"), Description("用户排序")]
        public bool UserOrder
        {
            get => Field.UserOrder; set => Field.UserOrder = value;
        }

        /// <summary>
        /// 下拉列表的地址
        /// </summary>
        /// <remark>
        /// 下拉列表的地址
        /// </remark>
        [JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"下拉列表的地址"), Description("下拉列表的地址")]
        public string ComboBoxUrl
        {
            get => Field.ComboBoxUrl; set => Field.ComboBoxUrl = value;
        }


        /// <summary>
        /// 是否时间
        /// </summary>
        [JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"是否时间"), Description("是否时间")]
        public bool IsTime
        {
            get => Field.IsTime;
            set => Field.IsTime = value;
        }

        /// <summary>
        /// 是否图片
        /// </summary>
        /// <remark>
        /// 是否图片
        /// </remark>
        [JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"是否图片"), Description("是否图片")]
        public bool IsImage
        {
            get => Field.IsImage; set => Field.IsImage = value;
        }

        /// <summary>
        /// 货币类型
        /// </summary>
        /// <remark>
        /// 是否货币
        /// </remark>
        [JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"货币类型"), Description("是否货币")]
        public bool IsMoney
        {
            get => Field.IsMoney; set => Field.IsMoney = value;
        }

        /// <summary>
        /// 表格对齐
        /// </summary>
        /// <remark>
        /// 对齐
        /// </remark>
        [JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"表格对齐"), Description("对齐")]
        public string GridAlign
        {
            get => Field.GridAlign; set => Field.GridAlign = value;
        }

        /// <summary>
        /// 占表格宽度比例
        /// </summary>
        /// <remark>
        /// 数据格式器
        /// </remark>
        [JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"占表格宽度比例"), Description("数据格式器")]
        public int GridWidth
        {
            get => Field.GridWidth; set => Field.GridWidth = value;
        }

        /// <summary>
        /// 数据格式器
        /// </summary>
        /// <remark>
        /// 数据格式器
        /// </remark>
        [JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"数据格式器"), Description("数据格式器")]
        public string DataFormater
        {
            get => Field.DataFormater; set => Field.DataFormater = value;
        }

        /// <summary>
        /// 是否用户内容
        /// </summary>
        [JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"用户内容"), Description("用户内容")]
        public bool IsUserContent
        {
            get => Field.IsUserContent; set => Field.IsUserContent = value;
        }

        /// <summary>
        /// 显示在列表详细页中
        /// </summary>
        /// <remark>
        /// 显示在列表详细页中
        /// </remark>
        [JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"显示在列表详细页中"), Description("显示在列表详细页中")]
        public bool GridDetails
        {
            get => Field.GridDetails; set => Field.GridDetails = value;
        }

        /// <summary>
        /// 列表不显示
        /// </summary>
        /// <remark>
        /// 列表不显示
        /// </remark>
        [JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"列表不显示"), Description("列表不显示")]
        public bool NoneGrid
        {
            get => Field.NoneGrid; set => Field.NoneGrid = value;
        }

        /// <summary>
        /// 详细不显示
        /// </summary>
        /// <remark>
        /// 详细不显示
        /// </remark>
        [JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"详细不显示"), Description("详细不显示")]
        public bool NoneDetails
        {
            get => Field.NoneDetails; set => Field.NoneDetails = value;
        }

        /// <summary>
        /// 列表详细页代码
        /// </summary>
        /// <remark>
        /// 详细界面代码
        /// </remark>
        [JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"列表详细页代码"), Description("详细界面代码")]
        public string GridDetailsCode
        {
            get => Field.GridDetailsCode;
            set => Field.GridDetailsCode = value;
        }
        #endregion
        #region 数据规则

        /// <summary>
        /// 值说明
        /// </summary>
        [JsonIgnore]
        [Category(@"数据规则"), DisplayName(@"数据说明"), Description("对于值数据规则的描述")]
        public string DataRuleDesc
        {
            get => Field.DataRuleDesc; set => Field.DataRuleDesc = value;
        }

        /// <summary>
        /// 值说明
        /// </summary>
        [JsonIgnore]
        public string AutoDataRuleDesc => Field.AutoDataRuleDesc;
        /// <summary>
        /// 校验代码
        /// </summary>
        /// <remark>
        /// 校验代码,本字段用{0}代替
        /// </remark>
        [JsonIgnore]
        [Category(@"数据规则"), DisplayName(@"校验代码"), Description("校验代码,本字段用{0}代替")]
        public string ValidateCode
        {
            get => Field.ValidateCode; set => Field.ValidateCode = value;
        }

        /// <summary>
        /// 能否为空的说明文字
        /// </summary>
        const string CanEmpty_Description = @"这是数据相关的逻辑,表示在存储时必须写入数据,否则逻辑不正确";

        /// <summary>
        /// 能否为空
        /// </summary>
        /// <remark>
        /// 这是数据相关的逻辑,表示在存储时必须写入数据,否则逻辑不正确
        /// </remark>
        [JsonIgnore]
        [Category(@"数据规则"), DisplayName(@"能否为空"), Description(CanEmpty_Description)]
        public bool CanEmpty
        {
            get => Field.CanEmpty;
            set => Field.CanEmpty = value;
        }

        /// <summary>
        /// 必填字段
        /// </summary>
        /// <remark>
        /// 是否必填字段
        /// </remark>
        [JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"必填字段"), Description("是否必填字段")]
        public bool IsRequired
        {
            get => Field.IsRequired;
            set => Field.IsRequired = value;
        }

        /// <summary>
        /// 最大值
        /// </summary>
        /// <remark>
        /// 最大
        /// </remark>
        [JsonIgnore]
        [Category(@"数据规则"), DisplayName(@"最大值"), Description("最大")]
        public string Max
        {
            get => Field.Max; set => Field.Max = value;
        }

        /// <summary>
        /// 最大值
        /// </summary>
        /// <remark>
        /// 最小
        /// </remark>
        [JsonIgnore]
        [Category(@"数据规则"), DisplayName(@"最大值"), Description("最小")]
        public string Min
        {
            get => Field.Min; set => Field.Min = value;
        }
        #endregion
        #region Upgrade

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        /// <returns></returns>
        public void Copy(IPropertyConfig dest, bool full = true)
        {
            if (full)
            {
                Entity = dest.Entity;
                IsCaption = dest.IsCaption;
                IsPrimaryKey = dest.IsPrimaryKey;
                IsExtendKey = dest.IsExtendKey;
                IsGlobalKey = dest.IsGlobalKey;
                UniqueIndex = dest.UniqueIndex;
                UniqueString = dest.UniqueString;
            }
            Group = dest.Group;
            JsonName = dest.JsonName;
            DataType = dest.DataType;
            CsType = dest.CsType;
            Initialization = dest.Initialization;
            CppType = dest.CppType;
            CppName = dest.CppName;
            CppLastType = dest.CppLastType;
            CppTypeObject = dest.CppTypeObject;
            ComputeGetCode = dest.ComputeGetCode;
            ComputeSetCode = dest.ComputeSetCode;
            IsCustomCompute = dest.IsCustomCompute;
            NoneApiArgument = dest.NoneApiArgument;
            ApiArgumentName = dest.ApiArgumentName;
            NoneJson = dest.NoneJson;
            HelloCode = dest.HelloCode;

            IsTime = dest.IsTime;
            IsArray = dest.IsArray;
            IsDictionary = dest.IsDictionary;
            IsEnum = dest.IsEnum;
            CustomType = dest.CustomType;
            ReferenceType = dest.ReferenceType;
            Nullable = dest.Nullable;
            IsExtendValue = dest.IsExtendValue;
            EnumKey = dest.EnumKey;
            EnumConfig = dest.EnumConfig;
            InnerField = dest.InnerField;
            IsSystemField = dest.IsSystemField;
            IsInterfaceField = dest.IsInterfaceField;
            IsPrivateField = dest.IsPrivateField;
            IsMiddleField = dest.IsMiddleField;
            CanGet = dest.CanGet;
            CanSet = dest.CanSet;
            IsCompute = dest.IsCompute;
            /*
            IsIdentity = dest.IsIdentity;
            Function = dest.Function;
            Having = dest.Having;
            DbFieldName = dest.DbFieldName;
            FieldType = dest.FieldType;
            KeepUpdate = dest.KeepUpdate;
            DbNullable = dest.DbNullable;
            Datalen = dest.Datalen;
            ArrayLen = dest.ArrayLen;
            Scale = dest.Scale;
            FixedLength = dest.FixedLength;
            IsMemo = dest.IsMemo;
            IsBlob = dest.IsBlob;
            DbInnerField = dest.DbInnerField;
            NoProperty = dest.NoProperty;
            NoStorage = dest.NoStorage;
            KeepStorageScreen = dest.KeepStorageScreen;
            CustomWrite = dest.CustomWrite;
            StorageProperty = dest.StorageProperty;
            IsLinkField = dest.IsLinkField;
            LinkTable = dest.LinkTable;
            IsLinkKey = dest.IsLinkKey;
            IsLinkCaption = dest.IsLinkCaption;
            LinkField = dest.LinkField;
            */
            IsUserReadOnly = dest.IsUserReadOnly;
            MulitLine = dest.MulitLine;
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
            DataRuleDesc = dest.DataRuleDesc;
            ValidateCode = dest.ValidateCode;
            CanEmpty = dest.CanEmpty;
            IsRequired = dest.IsRequired;
            Max = dest.Max;
            Min = dest.Min;
        }
        #endregion
    }
}