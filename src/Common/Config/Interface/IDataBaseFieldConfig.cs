namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 属性配置
    /// </summary>
    public interface IDataBaseFieldConfig : IConfig
    {
        #region 数据库

        /// <summary>
        /// 存储类型
        /// </summary>
        string DbType
        {
            get;
            set;
        }
        /// <summary>
        /// 数据库字段名称
        /// </summary>
        string DbFieldName
        {
            get;
            set;
        }

        /// <summary>
        /// 不更新
        /// </summary>
        bool KeepUpdate { get; set; }// Field.KeepUpdate;

        /// <summary>
        /// 能否存储空值
        /// </summary>
        /// <remark>
        /// 如为真,在存储空值读取时使用语言类型的默认值
        /// </remark>
        bool DbNullable { get; set; }// Field.DbNullable;

        /// <summary>
        /// 需要构建索引
        /// </summary>
        bool NeedDbIndex { get; }

        /// <summary>
        /// 数据长度
        /// </summary>
        /// <remark>
        /// 文本或二进制存储的最大长度
        /// </remark>
        int Datalen { get; set; }// Field.Datalen;

        /// <summary>
        /// 存储精度
        /// </summary>
        int Scale { get; set; }// Field.Scale;

        /// <summary>
        /// 是否数据库索引
        /// </summary>
        bool IsDbIndex
        {
            get; set;
        }

        /// <summary>
        /// 固定长度
        /// </summary>
        /// <remark>
        /// 是否固定长度字符串
        /// </remark>
        bool FixedLength { get; set; }// Field.FixedLength;

        /// <summary>
        /// 备注字段
        /// </summary>
        bool IsMemo { get; set; }// Field.IsMemo;

        /// <summary>
        /// 大数据
        /// </summary>
        bool IsBlob { get; set; }// Field.IsBlob;

        /// <summary>
        /// 内部字段(数据库)
        /// </summary>
        /// <remark>
        /// 数据库内部字段,如果为真,仅支持在SQL的语句中出现此字段，不支持外部的读写
        /// </remark>
        bool DbInnerField { get; set; }// Field.DbInnerField;

        /// <summary>
        /// 存储数据时跳过的场景
        /// </summary>
        StorageScreenType KeepStorageScreen { get; set; }// Field.KeepStorageScreen;

        /// <summary>
        /// 自定义保存
        /// </summary>
        /// <remark>
        /// 自定义保存,如果为真,数据库的写入忽略这个字段,数据的写入由代码自行维护
        /// </remark>
        bool CustomWrite { get; set; }// Field.CustomWrite;

        /// <summary>
        /// 存储值读写字段
        /// </summary>
        /// <remark>
        /// 存储值读写字段(internal),即使用非基础类型时,当发生读写数据库操作时使用的字段,字段为文本(JSON或XML)类型,使用序列化方法读写
        /// </remark>
        string StorageProperty { get; set; }// Field.StorageProperty;
        #endregion
    }
}