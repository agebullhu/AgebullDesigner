
using Microsoft.Xaml.Behaviors.Media;
using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 属性配置
    /// </summary>
    public interface IFieldConfig : IConfig
    {
        /// <summary>
        ///     原始字段
        /// </summary>
        FieldConfig Field
        {
            get;
        }

        #region 扩散信息

        /// <summary>
        /// 数据库字段名称
        /// </summary>
        string DbFieldName
        {
            get;
            set;
        }

        /// <summary>
        /// 字段名称(json)
        /// </summary>
        string JsonName
        {
            get;
            set;
        }

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
        /// 存储类型
        /// </summary>
        string DbType
        {
            get;
            set;
        }
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

        #region 计算列
        List<string> GetAliasPropertys();

        /// <summary>
        /// 自定义代码(get)
        /// </summary>
        /// <remark>
        /// 自定义代码Get部分代码,仅用于C#
        /// </remark>
        string ComputeGetCode
        {
            get;
            set;
        }
        /// <summary>
        /// 自定义代码(set)
        /// </summary>
        /// <remark>
        /// 自定义代码Set部分代码,仅用于C#
        /// </remark>
        string ComputeSetCode
        {
            get;
            set;
        }
        /// <summary>
        /// 自定义读写代码
        /// </summary>
        /// <remark>
        /// 自定义读写代码,即不使用代码生成,而使用录入的代码
        /// </remark>
        bool IsCustomCompute
        {
            get;
            set;
        }
        #endregion

        #region API支持

        /// <summary>
        /// 不参与ApiArgument序列化
        /// </summary>
        bool NoneApiArgument
        {
            get;
            set;
        }

        /// <summary>
        /// 字段名称(ApiArgument)
        /// </summary>
        string ApiArgumentName
        {
            get;
            set;
        }
        /// <summary>
        /// 不参与Json序列化
        /// </summary>
        bool NoneJson
        {
            get;
            set;
        }
        /// <summary>
        /// 示例内容
        /// </summary>
        string HelloCode
        {
            get;
            set;
        }
        #endregion

        #region 设计器支持

        /// <summary>
        /// 分组
        /// </summary>
        string Group
        {
            get;
            set;
        }
        #endregion
        #region 环境

        /// <summary>
        /// 上级
        /// </summary>
        EntityConfig Entity { get; }// Field.Entity;


        #endregion
        #region 系统

        /// <summary>
        /// 阻止编辑
        /// </summary>
        /// <remark>
        /// 阻止使用的范围
        /// </remark>
        AccessScopeType DenyScope { get; }// Field.DenyScope;

        #endregion
        #region 模型设计(C#)


        /// <summary>
        /// 是否时间
        /// </summary>
        bool IsTime { get; }// Field.IsTime;

        /// <summary>
        /// 是否扩展数组
        /// </summary>
        /// <remark>
        /// 是否扩展数组
        /// </remark>
        bool IsArray { get; }// Field.IsArray;

        /// <summary>
        /// 是否字典
        /// </summary>
        bool IsDictionary { get; }// Field.IsDictionary;

        /// <summary>
        /// 枚举类型(C#)
        /// </summary>
        bool IsEnum { get; }// Field.IsEnum;


        /// <summary>
        /// 非基本类型名称(C#)
        /// </summary>
        /// <remark>
        string CustomType { get; }// Field.CustomType;

        /// <summary>
        /// 参考类型
        /// </summary>
        string ReferenceType { get; }// Field.ReferenceType;

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
        bool Nullable { get; }// Field.Nullable;
        #endregion
        #region 模型设计

        /// <summary>
        /// 是否扩展值
        /// </summary>
        /// <remark>
        /// 是否扩展值
        /// </remark>
        bool IsExtendValue { get; }// Field.Nullable;

        /// <summary>
        /// 对应枚举
        /// </summary>
        /// <remark>
        /// 当使用自定义类型时的枚举对象
        /// </remark>
        Guid EnumKey { get; }// Field.EnumKey;

        /// <summary>
        /// 对应枚举
        /// </summary>
        /// <remark>
        /// 当使用自定义类型时的枚举对象
        /// </remark>
        EnumConfig EnumConfig { get; }// Field.EnumConfig;

        /// <summary>
        /// 内部字段
        /// </summary>
        bool InnerField { get; }// Field.InnerField;

        /// <summary>
        /// 系统字段
        /// </summary>
        bool IsSystemField { get; }// Field.IsSystemField;

        /// <summary>
        /// 接口字段
        /// </summary>
        bool IsInterfaceField { get; }// Field.IsInterfaceField;

        /// <summary>
        /// 代码访问范围
        /// </summary>
        /// <remark>
        /// 代码访问范围,即面向对象的三大范围(,private,protected)
        /// </remark>
        string AccessType { get; }// Field.AccessType;

        #endregion
        #region 模型设计(C++)

        /// <summary>
        /// 私有字段
        /// </summary>
        /// <remark>
        /// 私有字段,不应该复制
        /// </remark>
        bool IsPrivateField { get; }// Field.IsPrivateField;

        /// <summary>
        /// 设计时字段
        /// </summary>
        /// <remark>
        /// 设计时使用的中间过程字段,即最终使用时不需要的字段
        /// </remark>
        bool IsMiddleField { get; }// Field.IsMiddleField;

        /// <summary>
        /// 6位小数的整数
        /// </summary>
        /// <remark>
        /// 是否转为整数的小数,即使用扩大100成倍的整数
        /// </remark>
        bool IsIntDecimal { get; }// Field.IsIntDecimal;

        #endregion
        #region 模型设计(计算列)

        /// <summary>
        /// 可读
        /// </summary>
        /// <remark>
        /// 可读,即可以生成Get代码
        /// </remark>
        bool CanGet { get; }// Field.CanGet;

        /// <summary>
        /// 可写
        /// </summary>
        /// <remark>
        /// 可写,即生成SET代码
        /// </remark>
        bool CanSet { get; }// Field.CanSet;
        /// <summary>
        /// 计算列
        /// </summary>
        /// <remark>
        /// 是否计算列，即数据源于其它字段.如关系引用字段
        /// </remark>
        bool IsCompute { get; }// Field.IsCompute;


        #endregion

        #region 数据标识

        /// <summary>
        /// 标题字段
        /// </summary>
        bool IsCaption { get; }// Field.IsCaption;

        /// <summary>
        /// 主键字段
        /// </summary>
        bool IsPrimaryKey { get; }// Field.IsPrimaryKey;

        /// <summary>
        /// 唯一值字段
        /// </summary>
        /// <remark>
        /// 即它也是唯一标识符,如用户的身份证号
        /// </remark>
        bool IsExtendKey { get; }// Field.IsExtendKey;

        /// <summary>
        /// 自增字段
        /// </summary>
        /// <remark>
        /// 自增列,通过数据库(或REDIS)自动增加
        /// </remark>
        bool IsIdentity { get; }// Field.IsIdentity;

        /// <summary>
        /// 全局标识
        /// </summary>
        /// <remark>
        /// 是否使用GUID的全局KEY
        /// </remark>
        bool IsGlobalKey { get; }// Field.IsGlobalKey;

        /// <summary>
        /// 唯一属性组合顺序
        /// </summary>
        /// <remark>
        /// 参与组合成唯一属性的顺序,大于0有效
        /// </remark>
        int UniqueIndex { get; }// Field.UniqueIndex;
        /// <summary>
        /// 唯一文本
        /// </summary>
        bool UniqueString { get; }// Field.UniqueString;
        #endregion
        #region 数据库

        /// <summary>
        /// 不更新
        /// </summary>
        bool KeepUpdate { get; }// Field.KeepUpdate;

        /// <summary>
        /// 能否存储空值
        /// </summary>
        /// <remark>
        /// 如为真,在存储空值读取时使用语言类型的默认值
        /// </remark>
        bool DbNullable { get; }// Field.DbNullable;

        bool CreateDbIndex { get; }

        /// <summary>
        /// 数据长度
        /// </summary>
        /// <remark>
        /// 文本或二进制存储的最大长度
        /// </remark>
        int Datalen { get; set; }// Field.Datalen;

        /// <summary>
        /// 数组长度
        /// </summary>
        string ArrayLen { get; set; }// Field.ArrayLen;

        /// <summary>
        /// 存储精度
        /// </summary>
        int Scale { get; set; }// Field.Scale;

        /// <summary>
        /// 存储列ID
        /// </summary>
        /// <remark>
        /// 存储列ID,即在数据库内部对应的列ID
        /// </remark>
        int DbIndex { get; }// Field.DbIndex;

        /// <summary>
        /// 是否数据库索引
        /// </summary>
        bool IsDbIndex
        {
            get;
        }

        /// <summary>
        /// 固定长度
        /// </summary>
        /// <remark>
        /// 是否固定长度字符串
        /// </remark>
        bool FixedLength { get; }// Field.FixedLength;

        /// <summary>
        /// 备注字段
        /// </summary>
        bool IsMemo { get; }// Field.IsMemo;

        /// <summary>
        /// 大数据
        /// </summary>
        bool IsBlob { get; }// Field.IsBlob;

        /// <summary>
        /// 内部字段(数据库)
        /// </summary>
        /// <remark>
        /// 数据库内部字段,如果为真,仅支持在SQL的语句中出现此字段，不支持外部的读写
        /// </remark>
        bool DbInnerField { get; }// Field.DbInnerField;
        /// <summary>
        /// 非数据库字段
        /// </summary>
        /// <remark>
        /// 是否非数据库字段,如果为真,数据库的读写均忽略这个字段
        /// </remark>
        bool NoStorage { get; }// Field.NoStorage;
        /// <summary>
        /// 跳过保存的场景
        /// </summary>
        StorageScreenType KeepStorageScreen { get; }// Field.KeepStorageScreen;

        /// <summary>
        /// 自定义保存
        /// </summary>
        /// <remark>
        /// 自定义保存,如果为真,数据库的写入忽略这个字段,数据的写入由代码自行维护
        /// </remark>
        bool CustomWrite { get; }// Field.CustomWrite;

        /// <summary>
        /// 存储值读写字段
        /// </summary>
        /// <remark>
        /// 存储值读写字段(internal),即使用非基础类型时,当发生读写数据库操作时使用的字段,字段为文本(JSON或XML)类型,使用序列化方法读写
        /// </remark>
        string StorageProperty { get; }// Field.StorageProperty;
        #endregion
        #region 用户界面

        /// <summary>
        /// 客户端不可见
        /// </summary>
        bool DenyClient { get; }// Field.DenyClient;

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
        bool IsUserReadOnly { get; }// Field.IsUserReadOnly;

        /// <summary>
        /// 多行文本
        /// </summary>
        bool MulitLine { get; }// Field.MulitLine;

        /// <summary>
        /// 前缀
        /// </summary>
        string Prefix { get; }// Field.Prefix;

        /// <summary>
        /// 后缀
        /// </summary>
        string Suffix { get; }// Field.Suffix;

        /// <summary>
        /// 等同于空值的文本
        /// </summary>
        /// <remark>
        /// 等同于空值的文本,多个用#号分开
        /// </remark>
        string EmptyValue { get; }// Field.EmptyValue;


        /// <summary>
        /// 界面必填字段
        /// </summary>
        bool UiRequired { get; }// Field.UiRequired;

        /// <summary>
        /// 输入类型
        /// </summary>
        string InputType { get; }// Field.InputType;

        /// <summary>
        /// Form中占几列宽度
        /// </summary>
        int FormCloumnSapn { get; }// Field.FormCloumnSapn;

        /// <summary>
        /// Form中的设置
        /// </summary>
        string FormOption { get; }// Field.FormOption;

        /// <summary>
        /// 用户排序
        /// </summary>
        bool UserOrder { get; }// Field.UserOrder;

        /// <summary>
        /// 下拉列表的地址
        /// </summary>
        string ComboBoxUrl { get; }// Field.ComboBoxUrl;

        /// <summary>
        /// 是否图片
        /// </summary>
        bool IsImage { get; }// Field.IsImage;

        /// <summary>
        /// 货币类型
        /// </summary>
        bool IsMoney { get; }// Field.IsMoney;

        /// <summary>
        /// 表格对齐
        /// </summary>
        string GridAlign { get; }// Field.GridAlign;

        /// <summary>
        /// 占表格宽度比例
        /// </summary>
        int GridWidth { get; }// Field.GridWidth;

        /// <summary>
        /// 数据格式器
        /// </summary>
        string DataFormater { get; }// Field.DataFormater;

        /// <summary>
        /// 显示在列表详细页中
        /// </summary>
        bool GridDetails { get; }// Field.GridDetails;

        /// <summary>
        /// 列表不显示
        /// </summary>
        bool NoneGrid { get; }// Field.NoneGrid;

        /// <summary>
        /// 详细不显示
        /// </summary>
        bool NoneDetails { get; }// Field.NoneDetails;

        /// <summary>
        /// 列表详细页代码
        /// </summary>
        string GridDetailsCode { get; }// Field.GridDetailsCode;
        #endregion
        #region 数据规则

        /// <summary>
        /// 规则说明
        /// </summary>
        string DataRuleDesc { get; }// Field.DataRuleDesc;

        /// <summary>
        /// 规则说明
        /// </summary>
        string AutoDataRuleDesc { get; }// Field.AutoDataRuleDesc;

        /// <summary>
        /// 校验代码
        /// </summary>
        /// <remark>
        /// 校验代码,本字段用{0}代替
        /// </remark>
        string ValidateCode { get; }// Field.ValidateCode;

        /// <summary>
        /// 能否为空
        /// </summary>
        /// <remark>
        /// 这是数据相关的逻辑,表示在存储时必须写入数据,否则逻辑不正确
        /// </remark>
        bool CanEmpty { get; }// Field.CanEmpty;

        /// <summary>
        /// 必填字段
        /// </summary>
        bool IsRequired { get; }// Field.IsRequired;

        /// <summary>
        /// 最大值
        /// </summary>
        string Max { get; }// Field.Max;

        /// <summary>
        /// 最小值
        /// </summary>
        string Min { get; }// Field.Min;
        #endregion
        #region 数据关联

        /// <summary>
        /// 连接字段
        /// </summary>
        bool IsLinkField { get; }// Field.IsLinkField;

        /// <summary>
        /// 关联表名
        /// </summary>
        string LinkTable { get; }// Field.LinkTable;

        /// <summary>
        /// 关联表主键
        /// </summary>
        /// <remark>
        /// 关联表主键,即与另一个实体关联的外键
        /// </remark>
        bool IsLinkKey { get; }// Field.IsLinkKey;

        /// <summary>
        /// 关联表标题
        /// </summary>
        /// <remark>
        /// 关联表标题,即此字段为关联表的标题内容
        /// </remark>
        bool IsLinkCaption { get; }// Field.IsLinkCaption;

        /// <summary>
        /// 对应客户ID
        /// </summary>
        /// <remark>
        /// 是对应的UID,已过时,原来用于龙之战鼓
        /// </remark>
        bool IsUserId { get; }// Field.IsUserId;

        /// <summary>
        /// 关联字段名称
        /// </summary>
        /// <remark>
        /// 关联字段名称,即在关联表中的字段名称
        /// </remark>
        string LinkField { get; }// Field.LinkField;
        #endregion
    }
}