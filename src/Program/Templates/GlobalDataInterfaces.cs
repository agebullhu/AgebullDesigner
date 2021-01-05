using System;
using Agebull.EntityModel.Common;

namespace Agebull.EntityModel.Interfaces
{
    /// <summary>
    /// 数据接口定义
    /// </summary>
    public static class GlobalDataInterfaces
    {
        /// <summary>
        /// 接口IAuditData的数据结构
        /// </summary>
        public static class IAuditData
        {


            /// <summary>
            /// 审核状态
            /// </summary>
            public static PropertyDefault AuditState = new PropertyDefault
            {
                Name = "AuditState",
                ValueType = PropertyValueType.NumberEnum,
                CanNull = false,
                PropertyType = typeof(AuditStateType),
                PropertyFeatrue = PropertyFeatrue.Property | PropertyFeatrue.Field | PropertyFeatrue.Property,
                DbType = (int)MySqlConnector.MySqlDbType.Int32,
                FieldName = "audit_state",
                DbReadWrite = ReadWriteFeatrue.Read,
                JsonName = "auditState",
                CanImport = false,
                CanExport = false,
                Entity = "IAuditData",
                Caption = @"审核状态",
                Description = @"审核状态"
            };

            /// <summary>
            /// 审核时间
            /// </summary>
            public static PropertyDefault AuditDate = new PropertyDefault
            {
                Name = "AuditDate",
                ValueType = PropertyValueType.Value,
                CanNull = false,
                PropertyType = typeof(DateTime),
                PropertyFeatrue = PropertyFeatrue.Property | PropertyFeatrue.Field | PropertyFeatrue.Property,
                DbType = (int)MySqlConnector.MySqlDbType.DateTime,
                FieldName = "audit_date",
                DbReadWrite = ReadWriteFeatrue.Read,
                JsonName = "auditDate",
                CanImport = false,
                CanExport = false,
                Entity = "IAuditData",
                Caption = @"审核时间",
                Description = @"审核时间"
            };

            /// <summary>
            /// 审核人标识
            /// </summary>
            public static PropertyDefault AuditorId = new PropertyDefault
            {
                Name = "AuditorId",
                ValueType = PropertyValueType.Value,
                CanNull = false,
                PropertyType = typeof(long),
                PropertyFeatrue = PropertyFeatrue.Property | PropertyFeatrue.Field | PropertyFeatrue.Property,
                DbType = (int)MySqlConnector.MySqlDbType.Int64,
                FieldName = "auditor_id",
                DbReadWrite = ReadWriteFeatrue.Read,
                JsonName = "auditorId",
                CanImport = false,
                CanExport = false,
                Entity = "IAuditData",
                Caption = @"审核人标识",
                Description = @"审核人标识"
            };

            /// <summary>
            /// 审核人
            /// </summary>
            public static PropertyDefault Auditor = new PropertyDefault
            {
                Name = "Auditor",
                ValueType = PropertyValueType.String,
                CanNull = false,
                PropertyType = typeof(string),
                PropertyFeatrue = PropertyFeatrue.Property | PropertyFeatrue.Field | PropertyFeatrue.Property,
                DbType = (int)MySqlConnector.MySqlDbType.VarString,
                FieldName = "auditor",
                DbReadWrite = ReadWriteFeatrue.Read,
                JsonName = "auditor",
                CanImport = false,
                CanExport = false,
                Entity = "IAuditData",
                Caption = @"审核人",
                Description = @"审核人"
            };
        }

        /// <summary>
        /// 接口IAuthorData的数据结构
        /// </summary>
        public static class IAuthorData
        {


            /// <summary>
            /// 制作人标识
            /// </summary>
            public static PropertyDefault AuthorId = new PropertyDefault
            {
                Name = "AuthorId",
                ValueType = PropertyValueType.String,
                CanNull = false,
                PropertyType = typeof(string),
                PropertyFeatrue = PropertyFeatrue.Property | PropertyFeatrue.Field | PropertyFeatrue.Property,
                DbType = (int)MySqlConnector.MySqlDbType.VarString,
                FieldName = "created_user_id",
                DbReadWrite = ReadWriteFeatrue.Read | ReadWriteFeatrue.Insert,
                JsonName = "authorId",
                CanImport = false,
                CanExport = false,
                Entity = "IAuthorData",
                Caption = @"制作人标识",
                Description = @"制作人标识"
            };

            /// <summary>
            /// 制作人
            /// </summary>
            public static PropertyDefault Author = new PropertyDefault
            {
                Name = "Author",
                ValueType = PropertyValueType.String,
                CanNull = false,
                PropertyType = typeof(string),
                PropertyFeatrue = PropertyFeatrue.Property | PropertyFeatrue.Field | PropertyFeatrue.Property,
                DbType = (int)MySqlConnector.MySqlDbType.VarString,
                FieldName = "created_user",
                DbReadWrite = ReadWriteFeatrue.Read | ReadWriteFeatrue.Insert,
                JsonName = "author",
                CanImport = false,
                CanExport = false,
                Entity = "IAuthorData",
                Caption = @"制作人",
                Description = @"制作人"
            };

            /// <summary>
            /// 制作时间
            /// </summary>
            public static PropertyDefault AddDate = new PropertyDefault
            {
                Name = "AddDate",
                ValueType = PropertyValueType.Value,
                CanNull = false,
                PropertyType = typeof(DateTime),
                PropertyFeatrue = PropertyFeatrue.Property | PropertyFeatrue.Field | PropertyFeatrue.Property,
                DbType = (int)MySqlConnector.MySqlDbType.DateTime,
                FieldName = "created_date",
                DbReadWrite = ReadWriteFeatrue.Read | ReadWriteFeatrue.Insert,
                JsonName = "addDate",
                CanImport = false,
                CanExport = false,
                Entity = "IAuthorData",
                Caption = @"制作时间",
                Description = @"制作时间"
            };
        }

        /// <summary>
        /// 接口IDepartmentData的数据结构
        /// </summary>
        public static class IDepartmentData
        {


            /// <summary>
            /// 部门数据边界
            /// </summary>
            public static PropertyDefault DepartmentId = new PropertyDefault
            {
                Name = "DepartmentId",
                ValueType = PropertyValueType.String,
                CanNull = false,
                PropertyType = typeof(string),
                PropertyFeatrue = PropertyFeatrue.Property | PropertyFeatrue.Field | PropertyFeatrue.Property,
                DbType = (int)MySqlConnector.MySqlDbType.VarString,
                FieldName = "department_id",
                DbReadWrite = ReadWriteFeatrue.Read | ReadWriteFeatrue.Insert,
                JsonName = "departmentId",
                CanImport = false,
                CanExport = false,
                Entity = "IDepartmentData",
                Caption = @"部门数据边界",
                Description = @"部门数据边界"
            };

            /// <summary>
            /// 部门查找代码
            /// </summary>
            public static PropertyDefault DepartmentCode = new PropertyDefault
            {
                Name = "DepartmentCode",
                ValueType = PropertyValueType.String,
                CanNull = false,
                PropertyType = typeof(string),
                PropertyFeatrue = PropertyFeatrue.Property | PropertyFeatrue.Field | PropertyFeatrue.Property,
                DbType = (int)MySqlConnector.MySqlDbType.VarString,
                FieldName = "department_code",
                DbReadWrite = ReadWriteFeatrue.Read | ReadWriteFeatrue.Insert,
                JsonName = "departmentCode",
                CanImport = false,
                CanExport = false,
                Entity = "IDepartmentData",
                Caption = @"部门查找代码",
                Description = @"部门查找代码"
            };
        }

        /// <summary>
        /// 接口IEntityLink的数据结构
        /// </summary>
        public static class IEntityLink
        {


            /// <summary>
            /// 连接类型
            /// </summary>
            public static PropertyDefault LinkEntityType = new PropertyDefault
            {
                Name = "LinkEntityType",
                ValueType = PropertyValueType.Value,
                CanNull = false,
                PropertyType = typeof(int),
                PropertyFeatrue = PropertyFeatrue.Property | PropertyFeatrue.Field | PropertyFeatrue.Property,
                DbType = (int)MySqlConnector.MySqlDbType.Int32,
                FieldName = "link_entity_type",
                DbReadWrite = ReadWriteFeatrue.Read | ReadWriteFeatrue.Insert,
                JsonName = "linkEntityType",
                CanImport = false,
                CanExport = false,
                Entity = "IEntityLink",
                Caption = @"连接类型",
                Description = @"连接类型"
            };

            /// <summary>
            /// 关联标识
            /// </summary>
            public static PropertyDefault LinkEntityId = new PropertyDefault
            {
                Name = "LinkEntityId",
                ValueType = PropertyValueType.Value,
                CanNull = false,
                PropertyType = typeof(long),
                PropertyFeatrue = PropertyFeatrue.Property | PropertyFeatrue.Field | PropertyFeatrue.Property,
                DbType = (int)MySqlConnector.MySqlDbType.Int64,
                FieldName = "link_entity_id",
                DbReadWrite = ReadWriteFeatrue.Read | ReadWriteFeatrue.Insert,
                JsonName = "linkEntityId",
                CanImport = false,
                CanExport = false,
                Entity = "IEntityLink",
                Caption = @"关联标识",
                Description = @"关联标识"
            };
        }

        /// <summary>
        /// 接口IHistoryData的数据结构
        /// </summary>
        public static class IHistoryData
        {


            /// <summary>
            /// 最后修改日期
            /// </summary>
            public static PropertyDefault LastModifyDate = new PropertyDefault
            {
                Name = "LastModifyDate",
                ValueType = PropertyValueType.Value,
                CanNull = false,
                PropertyType = typeof(DateTime),
                PropertyFeatrue = PropertyFeatrue.Property | PropertyFeatrue.Field | PropertyFeatrue.Property,
                DbType = (int)MySqlConnector.MySqlDbType.DateTime,
                FieldName = "latest_updated_date",
                DbReadWrite = ReadWriteFeatrue.Read | ReadWriteFeatrue.Insert,
                JsonName = "lastModifyDate",
                CanImport = false,
                CanExport = false,
                Entity = "IHistoryData",
                Caption = @"最后修改日期",
                Description = @"最后修改日期"
            };

            /// <summary>
            /// 最后修改者标识
            /// </summary>
            public static PropertyDefault LastReviserId = new PropertyDefault
            {
                Name = "LastReviserId",
                ValueType = PropertyValueType.String,
                CanNull = false,
                PropertyType = typeof(string),
                PropertyFeatrue = PropertyFeatrue.Property | PropertyFeatrue.Field | PropertyFeatrue.Property,
                DbType = (int)MySqlConnector.MySqlDbType.VarString,
                FieldName = "latest_updated_user_id",
                DbReadWrite = ReadWriteFeatrue.Read | ReadWriteFeatrue.Insert,
                JsonName = "lastReviserId",
                CanImport = false,
                CanExport = false,
                Entity = "IHistoryData",
                Caption = @"最后修改者标识",
                Description = @"最后修改者标识"
            };

            /// <summary>
            /// 最后修改者
            /// </summary>
            public static PropertyDefault LastReviser = new PropertyDefault
            {
                Name = "LastReviser",
                ValueType = PropertyValueType.String,
                CanNull = false,
                PropertyType = typeof(string),
                PropertyFeatrue = PropertyFeatrue.Property | PropertyFeatrue.Field | PropertyFeatrue.Property,
                DbType = (int)MySqlConnector.MySqlDbType.VarString,
                FieldName = "latest_updated_user",
                DbReadWrite = ReadWriteFeatrue.Read | ReadWriteFeatrue.Insert,
                JsonName = "lastReviser",
                CanImport = false,
                CanExport = false,
                Entity = "IHistoryData",
                Caption = @"最后修改者",
                Description = @"最后修改者"
            };
        }

        /// <summary>
        /// 接口IInnerTree的数据结构
        /// </summary>
        public static class IInnerTree
        {


            /// <summary>
            /// 上级标识
            /// </summary>
            public static PropertyDefault ParentId = new PropertyDefault
            {
                Name = "ParentId",
                ValueType = PropertyValueType.Value,
                CanNull = false,
                PropertyType = typeof(long),
                PropertyFeatrue = PropertyFeatrue.Property | PropertyFeatrue.Field | PropertyFeatrue.Property,
                DbType = (int)MySqlConnector.MySqlDbType.Int64,
                FieldName = "PID",
                DbReadWrite = ReadWriteFeatrue.Read | ReadWriteFeatrue.Insert,
                JsonName = "parentId",
                CanImport = false,
                CanExport = false,
                Entity = "IInnerTree",
                Caption = @"上级标识",
                Description = @"上级标识"
            };
        }

        /// <summary>
        /// 接口ILogicDeleteData的数据结构
        /// </summary>
        public static class ILogicDeleteData
        {


            /// <summary>
            /// 逻辑删除标识
            /// </summary>
            public static PropertyDefault IsDeleted = new PropertyDefault
            {
                Name = "IsDeleted",
                ValueType = PropertyValueType.Value,
                CanNull = false,
                PropertyType = typeof(bool),
                PropertyFeatrue = PropertyFeatrue.Field | PropertyFeatrue.None,
                DbType = (int)MySqlConnector.MySqlDbType.Byte,
                FieldName = "is_deleted",
                DbReadWrite = ReadWriteFeatrue.Read,
                JsonName = "isDeleted",
                CanImport = false,
                CanExport = false,
                Entity = "ILogicDeleteData",
                Caption = @"逻辑删除标识",
                Description = @"逻辑删除标识"
            };
        }

        /// <summary>
        /// 接口IOrganizationData的数据结构
        /// </summary>
        public static class IOrganizationData
        {


            /// <summary>
            /// 组织数据边界
            /// </summary>
            public static PropertyDefault OrganizationId = new PropertyDefault
            {
                Name = "OrganizationId",
                ValueType = PropertyValueType.String,
                CanNull = false,
                PropertyType = typeof(string),
                PropertyFeatrue = PropertyFeatrue.Property | PropertyFeatrue.Field | PropertyFeatrue.Property,
                DbType = (int)MySqlConnector.MySqlDbType.VarString,
                FieldName = "top_org_id",
                DbReadWrite = ReadWriteFeatrue.Read,
                JsonName = "organizationID",
                CanImport = false,
                CanExport = false,
                Entity = "IOrganizationData",
                Caption = @"组织数据边界",
                Description = @"组织数据边界"
            };
        }

        /// <summary>
        /// 接口IStateData的数据结构
        /// </summary>
        public static class IStateData
        {


            /// <summary>
            /// 冻结更新
            /// </summary>
            public static PropertyDefault IsFreeze = new PropertyDefault
            {
                Name = "IsFreeze",
                ValueType = PropertyValueType.Value,
                CanNull = false,
                PropertyType = typeof(bool),
                PropertyFeatrue = PropertyFeatrue.Property | PropertyFeatrue.Field | PropertyFeatrue.Property,
                DbType = (int)MySqlConnector.MySqlDbType.Byte,
                FieldName = "is_freeze",
                DbReadWrite = ReadWriteFeatrue.Read,
                JsonName = "isFreeze",
                CanImport = false,
                CanExport = false,
                Entity = "IStateData",
                Caption = @"冻结更新",
                Description = @"无论在什么数据状态,一旦设置且保存后,数据将不再允许执行Update的操作,作为Update的统一开关.取消的方法是单独设置这个字段的值"
            };

            /// <summary>
            /// 数据状态
            /// </summary>
            public static PropertyDefault DataState = new PropertyDefault
            {
                Name = "DataState",
                ValueType = PropertyValueType.NumberEnum,
                CanNull = false,
                PropertyType = typeof(DataStateType),
                PropertyFeatrue = PropertyFeatrue.Property | PropertyFeatrue.Field | PropertyFeatrue.Property,
                DbType = (int)MySqlConnector.MySqlDbType.Int32,
                FieldName = "data_state",
                DbReadWrite = ReadWriteFeatrue.Read,
                JsonName = "dataState",
                CanImport = false,
                CanExport = false,
                Entity = "IStateData",
                Caption = @"数据状态",
                Description = @"数据状态"
            };
        }

        /// <summary>
        /// 接口IVersionData的数据结构
        /// </summary>
        public static class IVersionData
        {


            /// <summary>
            /// 数据版本号
            /// </summary>
            public static PropertyDefault DataVersion = new PropertyDefault
            {
                Name = "DataVersion",
                ValueType = PropertyValueType.Value,
                CanNull = false,
                PropertyType = typeof(long),
                PropertyFeatrue = PropertyFeatrue.Property | PropertyFeatrue.Field | PropertyFeatrue.Property,
                DbType = (int)MySqlConnector.MySqlDbType.Int64,
                FieldName = "_data_version",
                DbReadWrite = ReadWriteFeatrue.Read | ReadWriteFeatrue.Insert | ReadWriteFeatrue.Update,
                JsonName = "dataVersion",
                CanImport = false,
                CanExport = false,
                Entity = "IVersionData",
                Caption = @"数据版本号",
                Description = @"数据版本号"
            };
        }

    }
}
