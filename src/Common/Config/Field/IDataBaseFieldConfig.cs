namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 数据库字段
    /// </summary>
    public interface IDataBaseFieldConfig
    {
        /// <summary>
        /// 存储类型
        /// </summary>
        string FieldType { get; }
        /// <summary>
        /// 数据库字段名称
        /// </summary>
        string DbFieldName { get; }
        /// <summary>
        /// 能否存储空值
        /// </summary>
        /// <remarks>
        /// 如为真,在存储空值读取时使用语言类型的默认值
        /// </remarks>
        bool DbNullable { get; }
        /// <summary>
        /// 需要构建索引
        /// </summary>
        bool NeedDbIndex { get; }
        /// <summary>
        /// 数据长度
        /// </summary>/// <remarks>
        /// 文本或二进制存储的最大长度
        /// </remarks>
        int Datalen { get; }
        /// <summary>
        /// 存储精度
        /// </summary>
        int Scale { get; }
        /// <summary>
        /// 是否数据库索引
        /// </summary>
        bool IsDbIndex { get; }
        /// <summary>
        /// 固定长度
        /// </summary>/// <remarks>
        /// 是否固定长度字符串
        /// </remarks>
        bool FixedLength { get; }
        /// <summary>
        /// 备注字段
        /// </summary>
        bool IsText { get; }
        /// <summary>
        /// 大数据
        /// </summary>
        bool IsBlob { get; }
        /// <summary>
        /// 内部字段(数据库)
        /// </summary>/// <remarks>
        /// 数据库内部字段,如果为真,仅支持在SQL的语句中出现此字段,不支持外部的读写
        /// </remarks>
        bool DbInnerField { get; }
        /// <summary>
        /// 存储数据时跳过的场景
        /// </summary>
        StorageScreenType KeepStorageScreen { get; }
        /// <summary>
        /// 自定义保存
        /// </summary>/// <remarks>
        /// 自定义保存,如果为真,数据库的写入忽略这个字段,数据的写入由代码自行维护
        /// </remarks>
        bool CustomWrite { get; }
        /// <summary>
        /// 存储值读写字段
        /// </summary>/// <remarks>
        /// 存储值读写字段(internal),即使用非基础类型时,当发生读写数据库操作时使用的字段,字段为文本(JSON或XML)类型,使用序列化方法读写
        /// </remarks>
        string StorageProperty { get; }
        /// <summary>
        /// 非数据库字段
        /// </summary>/// <remarks>
        /// 是否非数据库字段,如果为真,数据库的读写均忽略这个字段
        /// </remarks>
        bool NoStorage { get; }
        /// <summary>
        /// 自增字段
        /// </summary>/// <remarks>
        /// 自增列,通过数据库(或REDIS)自动增加
        /// </remarks>
        bool IsIdentity { get; }
        /// <summary>
        /// 汇总方法
        /// </summary>
        string Function { get; }
        /// <summary>
        /// 汇总条件
        /// </summary>
        string Having { get; }
        /// <summary>
        /// 值类型
        /// </summary>
        string ValueType { get; }
        /// <summary>
        /// 上级外键
        /// </summary>
        bool IsParent { get; }
        /// <summary>
        /// 连接字段
        /// </summary>
        bool IsLinkField { get; }
        /// <summary>
        /// 关联表名
        /// </summary>
        string LinkTable { get; }
        /// <summary>
        /// 关联表主键
        /// </summary>/// <remarks>
        /// 关联表主键,即与另一个实体关联的外键
        /// </remarks>
        bool IsLinkKey { get; }
        /// <summary>
        /// 关联表标题
        /// </summary>/// <remarks>
        /// 关联表标题,即此字段为关联表的标题内容
        /// </remarks>
        bool IsLinkCaption { get; }
        /// <summary>
        /// 关联字段名称
        /// </summary>/// <remarks>
        /// 关联字段名称,即在关联表中的字段名称
        /// </remarks>
        string LinkField { get; }
        /// <summary>
        /// 是否只读
        /// </summary>/// <remarks>
        /// 指数据只可读,无法写入的场景,如此字段为汇总字段
        /// </remarks>
        bool IsReadonly { get; }
    }

}