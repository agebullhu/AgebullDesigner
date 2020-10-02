﻿/*design by:agebull designer date:2017/7/12 22:06:40*/
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
using Newtonsoft.Json;

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
        /// <remark>
        /// 上级
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"上级"), Description("上级")]
        public EntityConfig Entity => Field.Entity;

        #endregion
        #region 系统

        /// <summary>
        /// 阻止编辑
        /// </summary>
        /// <remark>
        /// 阻止使用的范围
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"系统"), DisplayName(@"阻止编辑"), Description("阻止使用的范围")]
        public AccessScopeType DenyScope => Field.DenyScope;

        #endregion
        #region 模型设计(C#)


        /// <summary>
        /// 是否时间
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"是否时间"), Description("是否时间")]
        public bool IsTime => Field.IsTime;

        /// <summary>
        /// 是否扩展数组
        /// </summary>
        /// <remark>
        /// 是否扩展数组
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"是否数组"), Description("是否数组")]
        public bool IsArray => Field.IsArray;

        /// <summary>
        /// 是否字典
        /// </summary>
        /// <remark>
        /// 是否扩展数组
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"是否字典"), Description("是否字典")]
        public bool IsDictionary => Field.IsDictionary;

        /// <summary>
        /// 枚举类型(C#)
        /// </summary>
        /// <remark>
        /// 字段类型
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"是否枚举类型)"), Description("字段类型")]
        public bool IsEnum => Field.IsEnum;


        /// <summary>
        /// 非基本类型名称(C#)
        /// </summary>
        /// <remark>
        /// 字段类型
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计(C#)"), DisplayName(@"非基本类型名称(C#)"), Description("字段类型")]
        public string CustomType => Field.CustomType;

        /// <summary>
        /// 参考类型
        /// </summary>
        /// <remark>
        /// 字段类型
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计(C#)"), DisplayName(@"参考类型(C#)"), Description("字段类型")]
        public string ReferenceType => Field.ReferenceType;

        /// <summary>
        /// 结果类型(C#)
        /// </summary>
        /// <remark>
        /// 最终生成C#代码时的属性类型
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计(C#)"), DisplayName(@"结果类型(C#)"), Description("最终生成C#代码时的属性类型")]
        public string LastCsType => Field.ToLastCsType();

        /// <summary>
        /// 可空类型(C#)
        /// </summary>
        /// <remark>
        /// 即生成的C#代码,类型为空类型Nullable〈T〉 ,如int?
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计(C#)"), DisplayName(@"可空类型(C#)")]
        public bool Nullable => Field.Nullable;
        #endregion
        #region 模型设计

        /// <summary>
        /// 是否扩展值
        /// </summary>
        /// <remark>
        /// 是否扩展值
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@""), DisplayName(@"是否扩展值"), Description("是否扩展值")]
        public bool IsExtendValue => Field.Nullable;

        /// <summary>
        /// 对应枚举
        /// </summary>
        /// <remark>
        /// 当使用自定义类型时的枚举对象
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"对应枚举"), Description("当使用自定义类型时的枚举对象")]
        public Guid EnumKey => Field.EnumKey;

        /// <summary>
        /// 对应枚举
        /// </summary>
        /// <remark>
        /// 当使用自定义类型时的枚举对象
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"对应枚举"), Description("当使用自定义类型时的枚举对象")]
        public EnumConfig EnumConfig => Field.EnumConfig;

        /// <summary>
        /// 内部字段
        /// </summary>
        /// <remark>
        /// 是否内部字段,即非用户字段,不呈现给用户
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"内部字段"), Description("是否内部字段,即非用户字段,不呈现给用户")]
        public bool InnerField => Field.InnerField;

        /// <summary>
        /// 系统字段
        /// </summary>
        /// <remark>
        /// 系统字段
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"系统字段"), Description("系统字段")]
        public bool IsSystemField => Field.IsSystemField;

        /// <summary>
        /// 接口字段
        /// </summary>
        /// <remark>
        /// 是否接口字段
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"接口字段"), Description("是否接口字段")]
        public bool IsInterfaceField => Field.IsInterfaceField;

        /// <summary>
        /// 代码访问范围
        /// </summary>
        /// <remark>
        /// 代码访问范围,即面向对象的三大范围(public,private,protected)
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"代码访问范围")]
        public string AccessType => Field.AccessType;

        #endregion
        #region 模型设计(C++)

        /// <summary>
        /// 私有字段
        /// </summary>
        /// <remark>
        /// 私有字段,不应该复制
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计(C++)"), DisplayName(@"私有字段"), Description("私有字段,不应该复制")]
        public bool IsPrivateField => Field.IsPrivateField;

        /// <summary>
        /// 设计时字段
        /// </summary>
        /// <remark>
        /// 设计时使用的中间过程字段,即最终使用时不需要的字段
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计(C++)"), DisplayName(@"设计时字段")]
        public bool IsMiddleField => Field.IsMiddleField;

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
        public bool IsIntDecimal => Field.IsIntDecimal;

        #endregion
        #region 模型设计(计算列)

        /// <summary>
        /// 可读
        /// </summary>
        /// <remark>
        /// 可读,即可以生成Get代码
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"可读"), Description("可读,可以生成Get代码")]
        public bool CanGet => Field.CanGet;

        /// <summary>
        /// 可写
        /// </summary>
        /// <remark>
        /// 可写,即生成SET代码
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"可写"), Description("可写,即生成SET代码")]
        public bool CanSet => Field.CanSet;
        /// <summary>
        /// 计算列
        /// </summary>
        /// <remark>
        /// 是否计算列，即数据源于其它字段.如关系引用字段
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计(计算列)"), DisplayName(@"计算列")]
        public bool IsCompute => Field.IsCompute;


        #endregion

        #region 数据标识

        /// <summary>
        /// 标题字段
        /// </summary>
        /// <remark>
        /// 标题字段
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据标识"), DisplayName(@"标题字段"), Description("标题字段")]
        public bool IsCaption => Field.IsCaption;

        /// <summary>
        /// 主键字段
        /// </summary>
        /// <remark>
        /// 主键
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据标识"), DisplayName(@"主键字段"), Description("主键")]
        public bool IsPrimaryKey => Field.IsPrimaryKey;

        /// <summary>
        /// 唯一值字段
        /// </summary>
        /// <remark>
        /// 即它也是唯一标识符,如用户的身份证号
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据标识"), DisplayName(@"唯一值字段"), Description("即它也是唯一标识符,如用户的身份证号")]
        public bool IsExtendKey => Field.IsExtendKey;

        /// <summary>
        /// 自增字段
        /// </summary>
        /// <remark>
        /// 自增列,通过数据库(或REDIS)自动增加
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据标识"), DisplayName(@"自增字段")]
        public bool IsIdentity => Field.IsIdentity;

        /// <summary>
        /// 全局标识
        /// </summary>
        /// <remark>
        /// 是否使用GUID的全局KEY
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据标识"), DisplayName(@"全局标识"), Description("是否使用GUID的全局KEY")]
        public bool IsGlobalKey => Field.IsGlobalKey;

        /// <summary>
        /// 唯一属性组合顺序
        /// </summary>
        /// <remark>
        /// 参与组合成唯一属性的顺序,大于0有效
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据标识"), DisplayName(@"唯一属性组合顺序"), Description("参与组合成唯一属性的顺序,大于0有效")]
        public int UniqueIndex => Field.UniqueIndex;
        /// <summary>
        /// 唯一文本
        /// </summary>
        /// <remark>
        /// 5是否唯一文本
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据标识"), DisplayName(@"唯一文本"), Description("5是否唯一文本")]
        public bool UniqueString => Field.UniqueString;
        #endregion
        #region 数据库

        /// <summary>
        /// 不更新
        /// </summary>
        /// <remark>
        /// 不更新
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据库"), DisplayName(@"不更新"), Description("不更新")]
        public bool KeepUpdate => Field.KeepUpdate;

        /// <summary>
        /// 能否存储空值
        /// </summary>
        /// <remark>
        /// 如为真,在存储空值读取时使用语言类型的默认值
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据库"), DisplayName(@"能否存储空值")]
        public bool DbNullable => Field.DbNullable;

        /// <summary>
        /// 构建数据库索引
        /// </summary>
        /// <remark>
        /// 构建数据库索引的优化选项
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据库"), DisplayName(@"构建数据库索引"), Description("构建数据库索引的优化选项")]
        public bool CreateDbIndex => Field.CreateDbIndex;

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
            get => Field.Datalen;
            set => Field.Datalen = value;
        }

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
            get => Field.ArrayLen;
            set => Field.ArrayLen = value;
        }

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
            get => Field.Scale;
            set => Field.Scale = value;
        }

        /// <summary>
        /// 存储列ID
        /// </summary>
        /// <remark>
        /// 存储列ID,即在数据库内部对应的列ID
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据库"), DisplayName(@"存储列ID"), Description("存储列ID,即在数据库内部对应的列ID")]
        public int DbIndex => Field.DbIndex;

        /// <summary>
        /// 固定长度
        /// </summary>
        /// <remark>
        /// 是否固定长度字符串
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据库"), DisplayName(@"固定长度"), Description("是否固定长度字符串")]
        public bool FixedLength => Field.FixedLength;

        /// <summary>
        /// 备注字段
        /// </summary>
        /// <remark>
        /// 是否备注字段
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据库"), DisplayName(@"备注字段"), Description("是否备注字段")]
        public bool IsMemo => Field.IsMemo;

        /// <summary>
        /// 大数据
        /// </summary>
        /// <remark>
        /// 是否大数据
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据库"), DisplayName(@"大数据"), Description("是否大数据")]
        public bool IsBlob => Field.IsBlob;

        /// <summary>
        /// 内部字段(数据库)
        /// </summary>
        /// <remark>
        /// 数据库内部字段,如果为真,仅支持在SQL的语句中出现此字段，不支持外部的读写
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据库"), DisplayName(@"内部字段(数据库)")]
        public bool DbInnerField => Field.DbInnerField;
        /// <summary>
        /// 非数据库字段
        /// </summary>
        /// <remark>
        /// 是否非数据库字段,如果为真,数据库的读写均忽略这个字段
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据库"), DisplayName(@"非数据库字段")]
        public bool NoStorage => Field.NoStorage;

        /// <summary>
        /// 自定义保存
        /// </summary>
        /// <remark>
        /// 自定义保存,如果为真,数据库的写入忽略这个字段,数据的写入由代码自行维护
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据库"), DisplayName(@"自定义保存")]
        public bool CustomWrite => Field.CustomWrite;

        /// <summary>
        /// 存储值读写字段
        /// </summary>
        /// <remark>
        /// 存储值读写字段(internal),即使用非基础类型时,当发生读写数据库操作时使用的字段,字段为文本(JSON或XML)类型,使用序列化方法读写
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据库"), DisplayName(@"存储值读写字段")]
        public string StorageProperty => Field.StorageProperty;
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
        public bool DenyClient => Field.DenyClient;

        /// <summary>
        /// 用户是否可输入
        /// </summary>
        /// <remark>
        /// 用户是否可输入
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"用户是否可输入"), Description("用户是否可输入")]
        public bool CanUserInput => !IsCompute && !DenyClient && !IsUserReadOnly && !IsSystemField && !IsIdentity;

        /// <summary>
        /// 不可编辑
        /// </summary>
        /// <remark>
        /// 是否用户可编辑
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"不可编辑"), Description("是否用户可编辑")]
        public bool IsUserReadOnly => Field.IsUserReadOnly;

        /// <summary>
        /// 多行文本
        /// </summary>
        /// <remark>
        /// 多行文本
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"多行文本"), Description("多行文本")]
        public bool MulitLine => Field.MulitLine;

        /// <summary>
        /// 前缀
        /// </summary>
        /// <remark>
        /// 前缀
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"前缀"), Description("前缀")]
        public string Prefix => Field.Prefix;

        /// <summary>
        /// 后缀
        /// </summary>
        /// <remark>
        /// 后缀
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"后缀"), Description("后缀")]
        public string Suffix => Field.Suffix;

        /// <summary>
        /// 等同于空值的文本
        /// </summary>
        /// <remark>
        /// 等同于空值的文本,多个用#号分开
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"等同于空值的文本"), Description("等同于空值的文本,多个用#号分开")]
        public string EmptyValue => Field.EmptyValue;


        /// <summary>
        /// 界面必填字段
        /// </summary>
        /// <remark>
        /// 是否必填字段
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"界面必填字段"), Description("界面必填字段")]
        public bool UiRequired => Field.UiRequired;

        /// <summary>
        /// 输入类型
        /// </summary>
        /// <remark>
        /// 输入类型
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"输入类型"), Description("输入类型")]
        public string InputType => Field.InputType;

        /// <summary>
        /// Form中占几列宽度
        /// </summary>
        /// <remark>
        /// Form中占几列宽度
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"Form中占几列宽度"), Description("Form中占几列宽度")]
        public int FormCloumnSapn => Field.FormCloumnSapn;

        /// <summary>
        /// Form中的EasyUi设置
        /// </summary>
        /// <remark>
        /// Form中的EasyUi设置
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"Form中的EasyUi设置"), Description("Form中的EasyUi设置")]
        public string FormOption => Field.FormOption;

        /// <summary>
        /// 用户排序
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"用户排序"), Description("用户排序")]
        public bool UserOrder => Field.UserOrder;

        /// <summary>
        /// 下拉列表的地址
        /// </summary>
        /// <remark>
        /// 下拉列表的地址
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"下拉列表的地址"), Description("下拉列表的地址")]
        public string ComboBoxUrl => Field.ComboBoxUrl;

        /// <summary>
        /// 是否图片
        /// </summary>
        /// <remark>
        /// 是否图片
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"是否图片"), Description("是否图片")]
        public bool IsImage => Field.IsImage;

        /// <summary>
        /// 货币类型
        /// </summary>
        /// <remark>
        /// 是否货币
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"货币类型"), Description("是否货币")]
        public bool IsMoney => Field.IsMoney;

        /// <summary>
        /// 表格对齐
        /// </summary>
        /// <remark>
        /// 对齐
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"表格对齐"), Description("对齐")]
        public string GridAlign => Field.GridAlign;

        /// <summary>
        /// 占表格宽度比例
        /// </summary>
        /// <remark>
        /// 数据格式器
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"占表格宽度比例"), Description("数据格式器")]
        public int GridWidth => Field.GridWidth;

        /// <summary>
        /// 数据格式器
        /// </summary>
        /// <remark>
        /// 数据格式器
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"数据格式器"), Description("数据格式器")]
        public string DataFormater => Field.DataFormater;

        /// <summary>
        /// 显示在列表详细页中
        /// </summary>
        /// <remark>
        /// 显示在列表详细页中
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"显示在列表详细页中"), Description("显示在列表详细页中")]
        public bool GridDetails => Field.GridDetails;

        /// <summary>
        /// 列表不显示
        /// </summary>
        /// <remark>
        /// 列表不显示
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"列表不显示"), Description("列表不显示")]
        public bool NoneGrid => Field.NoneGrid;

        /// <summary>
        /// 详细不显示
        /// </summary>
        /// <remark>
        /// 详细不显示
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"详细不显示"), Description("详细不显示")]
        public bool NoneDetails => Field.NoneDetails;

        /// <summary>
        /// 列表详细页代码
        /// </summary>
        /// <remark>
        /// 详细界面代码
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"列表详细页代码"), Description("详细界面代码")]
        public string GridDetailsCode => Field.GridDetailsCode;
        #endregion
        #region 数据规则

        /// <summary>
        /// 值说明
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据规则"), DisplayName(@"数据说明"), Description("对于值数据规则的描述")]
        public string DataRuleDesc => Field.DataRuleDesc;

        /// <summary>
        /// 值说明
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public string AutoDataRuleDesc => Field.AutoDataRuleDesc;
        /// <summary>
        /// 校验代码
        /// </summary>
        /// <remark>
        /// 校验代码,本字段用{0}代替
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据规则"), DisplayName(@"校验代码"), Description("校验代码,本字段用{0}代替")]
        public string ValidateCode => Field.ValidateCode;

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
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据规则"), DisplayName(@"能否为空"), Description(CanEmpty_Description)]
        public bool CanEmpty => Field.CanEmpty;

        /// <summary>
        /// 必填字段
        /// </summary>
        /// <remark>
        /// 是否必填字段
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"必填字段"), Description("是否必填字段")]
        public bool IsRequired => Field.IsRequired;

        /// <summary>
        /// 最大值
        /// </summary>
        /// <remark>
        /// 最大
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据规则"), DisplayName(@"最大值"), Description("最大")]
        public string Max => Field.Max;

        /// <summary>
        /// 最大值
        /// </summary>
        /// <remark>
        /// 最小
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据规则"), DisplayName(@"最大值"), Description("最小")]
        public string Min => Field.Min;
        #endregion
        #region 数据关联

        /// <summary>
        /// 连接字段
        /// </summary>
        /// <remark>
        /// 连接字段
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据关联"), DisplayName(@"连接字段"), Description("连接字段")]
        public bool IsLinkField => Field.IsLinkField;

        /// <summary>
        /// 关联表名
        /// </summary>
        /// <remark>
        /// 关联表名
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据关联"), DisplayName(@"关联表名"), Description("关联表名")]
        public string LinkTable => Field.LinkTable;

        /// <summary>
        /// 关联表主键
        /// </summary>
        /// <remark>
        /// 关联表主键,即与另一个实体关联的外键
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据关联"), DisplayName(@"关联表主键"), Description("关联表主键,即与另一个实体关联的外键")]
        public bool IsLinkKey => Field.IsLinkKey;

        /// <summary>
        /// 关联表标题
        /// </summary>
        /// <remark>
        /// 关联表标题,即此字段为关联表的标题内容
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据关联"), DisplayName(@"关联表标题"), Description("关联表标题,即此字段为关联表的标题内容")]
        public bool IsLinkCaption => Field.IsLinkCaption;

        /// <summary>
        /// 对应客户ID
        /// </summary>
        /// <remark>
        /// 是对应的UID,已过时,原来用于龙之战鼓
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据关联"), DisplayName(@"对应客户ID"), Description("是对应的UID,已过时,原来用于龙之战鼓")]
        public bool IsUserId => Field.IsUserId;

        /// <summary>
        /// 关联字段名称
        /// </summary>
        /// <remark>
        /// 关联字段名称,即在关联表中的字段名称
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据关联"), DisplayName(@"关联字段名称"), Description("关联字段名称,即在关联表中的字段名称")]
        public string LinkField => Field.LinkField;
        #endregion
    }
}