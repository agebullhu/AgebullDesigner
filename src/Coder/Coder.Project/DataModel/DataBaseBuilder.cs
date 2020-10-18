using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Config.Mysql;
using Agebull.EntityModel.Config.SqlServer;

namespace Agebull.EntityModel.RobotCoder
{
    /// <summary>
    /// 数据库对象生成器
    /// </summary>
    public sealed class DataBaseBuilder : CoderWithProject
    {
        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_Model_DataBase_cs";

        #region 基础代码

        /// <summary>
        ///     生成实体代码
        /// </summary>
        protected override void CreateDesignerCode(string path)
        {
            string file = Path.Combine(path, Project.DataBaseObjectName + ".Designer.cs");
            string code = $@"#region
using System;
using System.Collections.Generic;
using System.Data;
using Agebull.Common;
using Agebull.Common.Configuration;
using Agebull.EntityModel.Common;
using Agebull.EntityModel.{Project.DbType};
#endregion

namespace {Project.NameSpace}.DataAccess
{{
    partial class {Project.DataBaseObjectName}
    {{
        {EntityStruct()}
    }}

    partial class DataAccessProviderHelper
    {{
        {ProviderHelper()}
    }}
}}";
            SaveCode(file, code);
        }
        #endregion

        #region 扩展代码

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected override void CreateCustomCode(string path)
        {
            string file = Path.Combine(path, Project.DataBaseObjectName + ".cs");
            string code = $@"#region
using System;
using System.Collections.Generic;
using System.Data;
using Agebull.Common;
using Agebull.Common.Configuration;
using Agebull.EntityModel.Common;
using Agebull.EntityModel.{Project.DbType};
using Microsoft.Extensions.DependencyInjection;
#endregion

namespace {Project.NameSpace}.DataAccess
{{
    /// <summary>
    /// 本地数据库
    /// </summary>
    public sealed partial class {Project.DataBaseObjectName} : {Project.DbType}DataBase
    {{
        /// <summary>
        /// 构造
        /// </summary>
        public {Project.DataBaseObjectName}()
        {{
            Name = @""{Project.Name}"";
            Caption = @""{Project.Caption}"";
            Description = @""{Project.Description.Replace("\"", "\"\"")}"";
            ConnectionString = {ConnectionString()}
        }}
    }}

    /// <summary>
    /// 本地数据库
    /// </summary>
    public static partial class DataAccessProviderHelper
    {{
        /// <summary>
        /// 构造数据访问对象
        /// </summary>
        /// <returns></returns>
        public static DataAccess<TEntity> CreateDataAccess<TEntity>(this IServiceProvider serviceProvider)
            where TEntity : class, new()
        {{
            var option = GetOption<TEntity>();
            if (option == null)
                throw new NotSupportedException($""{{typeof(TEntity).FullName}}没有对应配置项，请通过设计器生成"");
            if (option.IsQuery)
                throw new NotSupportedException($""{{typeof(TEntity).FullName}}是一个查询，请使用CreateDataQuery方法"");
            var provider = new DataAccessProvider<TEntity>
            {{
                ServiceProvider = serviceProvider,
                Option = option,
                SqlBuilder = new MySqlSqlBuilder<TEntity>(),
                Injection = serviceProvider.GetService<IOperatorInjection<TEntity>>(),
                CreateDataBase = () => serviceProvider.GetService<{Project.DataBaseObjectName}>(),
                EntityOperator = (IEntityOperator<TEntity>)GetEntityOperator<TEntity>(),
                DataOperator = (IDataOperator<TEntity>)GetDataOperator<TEntity>()
            }};
            provider.DataOperator.Provider = provider;
            provider.SqlBuilder.Provider = provider;
            if (provider.Injection != null)
                provider.Injection.Provider = provider;
            provider.Option.SqlBuilder = provider.SqlBuilder;
            provider.Option.Initiate();
            return new DataAccess<TEntity>(provider);
        }}

        /// <summary>
        /// 构造数据访问对象
        /// </summary>
        /// <returns></returns>
        public static DataQuery<TEntity> CreateDataQuery<TEntity>(this IServiceProvider serviceProvider)
            where TEntity : class, new()
        {{
            var provider = new DataAccessProvider<TEntity>
            {{
                ServiceProvider = serviceProvider,
                Option = GetOption<TEntity>(),
                SqlBuilder = new MySqlSqlBuilder<TEntity>(),
                Injection = serviceProvider.GetService<IOperatorInjection<TEntity>>(),
                CreateDataBase = () => serviceProvider.GetService<{Project.DataBaseObjectName}>(),
                EntityOperator = (IEntityOperator<TEntity>)GetEntityOperator<TEntity>(),
                DataOperator = (IDataOperator<TEntity>)GetDataOperator<TEntity>()
            }};
            provider.DataOperator.Provider = provider;
            provider.SqlBuilder.Provider = provider;
            if (provider.Injection != null)
                provider.Injection.Provider = provider;
            provider.Option.SqlBuilder = provider.SqlBuilder;
            provider.Option.Initiate();
            return new DataQuery<TEntity>(provider);
        }}
    }}
}}";
            SaveCode(file, code);
        }
        string ConnectionString()
        {
            if (Project.DbType != DataBaseType.Sqlite)
                return $@"ConfigurationHelper.GetConnectionString(""{Project.DataBaseObjectName}"");";

            return $@"new SqliteConnectionStringBuilder
            {{
                DataSource = ZeroAppOption.Instance.IsLinux
                    ? $""{{ZeroAppOption.Instance.DataFolder}}/{Project.DbSoruce}""
                    : $""{{ZeroAppOption.Instance.DataFolder}}\\{Project.DbSoruce}"",
                Mode = SqliteOpenMode.ReadWriteCreate,
                Cache = SqliteCacheMode.Shared
            }}.ConnectionString;
        }}";
        }
        #endregion

        #region 数据结构

        public static string Interfaces(IEntityConfig entity)
        {
            var it = new StringBuilder();
            bool first = true;
            foreach (var i in entity.Interfaces?.Split(',', System.StringSplitOptions.RemoveEmptyEntries))
            {
                if (first)
                {
                    first = false;
                    it.Append(@"
                InterfaceFeature = new[]{");
                }
                else
                    it.Append(',');
                it.Append($"nameof(GlobalDataInterfaces.{i})");
            }
            if (first)
                return null;
            it.Append("},");
            return it.ToString(); ;
        }

        private string EntityStruct()
        {
            StringBuilder code = new StringBuilder();

            foreach (var entity in Project.Entities)
            {
                code.Append(EntityStruct(entity));
            }
            return code.ToString();
        }

        public static string EntityStruct(IEntityConfig entity)
        {
            var properties = new List<string>();
            var codeStruct = new StringBuilder();
            EntityStruct(entity, codeStruct, properties);
            if (entity.IsInterface)
                return $@"
        /// <summary>
        /// 接口{entity.Name}的数据结构
        /// </summary>
        public static class {entity.Name}
        {{
{codeStruct}
        }}
";
            var it = Interfaces(entity);
            return $@"
        #region {entity.Name}({entity.Caption})

        /// <summary>
        /// 实体{entity.Name}的数据结构
        /// </summary>
        public static class {entity.Name}_Struct_
        {{
            /// <summary>
            /// 实体结构
            /// </summary>
            public static EntityStruct Struct => new EntityStruct
            {{
                EntityName      = entityName,
                Caption         = caption,
                Description     = description,
                PrimaryKey      = primaryKey,
                IsIdentity      = {(entity.PrimaryColumn?.IsIdentity ?? false ? "true" : "false")},
                ReadTableName   = tableName,
                WriteTableName  = ""{entity.SaveTableName}"",{it}
                Properties      = new List<EntityProperty>
                {{
                    {string.Join(@",
                    ", properties)}
                }}
            }};

            #region 常量

            /// <summary>
            /// 实体名称
            /// </summary>
            public const string name = ""{entity.Name}"";

            /// <summary>
            /// 实体名称
            /// </summary>
            public const string entityName = ""{entity.Name}"";

            /// <summary>
            /// 实体标题
            /// </summary>
            public const string caption = ""{entity.Caption}"";

            /// <summary>
            /// 实体说明
            /// </summary>
            public const string description = @""{entity.Description}"";

            /// <summary>
            /// 数据表名称
            /// </summary>
            public const string tableName = ""{entity.SaveTableName}"";

            /// <summary>
            /// 实体说明
            /// </summary>
            public const string primaryKey = ""{entity.PrimaryColumn.Name}"";
            #endregion
            #region 字段
{codeStruct}
            #endregion

        }}
        #endregion
";
        }

        public static void EntityStruct(IEntityConfig table, StringBuilder codeStruct, List<string> properties)
        {
            bool isFirst = true;
            int idx = 0;
            EntityStruct(table, codeStruct, properties, ref isFirst, ref idx);
        }
        static void EntityStruct(IEntityConfig table, StringBuilder codeStruct, List<string> properties, ref bool isFirst, ref int idx)
        {
            if (table == null)
                return;

            if (!string.IsNullOrWhiteSpace(table.ModelBase))
            {
                var entity = table.Parent.Entities.FirstOrDefault(p => p.Name == table.ModelBase);
                if (entity != null)
                    EntityStruct(entity, codeStruct, properties, ref isFirst, ref idx);
            }

            if (table.PrimaryColumn != null)
            {
                EntityStruct(codeStruct, properties, ref idx, table.PrimaryColumn);
            }
            foreach (var property in table.Properties.Where(p => p != table.PrimaryColumn).OrderBy(p => p.Index))
            {
                EntityStruct(codeStruct, properties, ref idx, property);
            }
        }

        static void EntityStruct(StringBuilder codeStruct, List<string> properties, ref int idx, IFieldConfig property)
        {
            var str = new StringBuilder("new EntityProperty(");
            IFieldConfig friend;
            bool hase = false;
            if (!property.Entity.IsInterface)
            {
                if (property.IsInterfaceField)
                {
                    hase = true;
                    str.Append($"GlobalDataInterfaces.{property.LinkTable}.{property.LinkField}");
                    friend = GlobalConfig.GetEntity(property.LinkTable).Properties.FirstOrDefault(p => p.Name == property.LinkField);
                }
                else if (property.IsLinkField)
                {
                    var entity = GlobalConfig.GetEntity(property.LinkTable);
                    if (entity != null)
                    {
                        hase = true;
                        str.Append($"{property.Entity.Name}_Struct_.{property.LinkField}");
                        friend = entity.Properties.FirstOrDefault(p => p.Name == property.LinkField);
                    }
                }
            }
            if (!hase)
            {
                PropertyStruct(codeStruct, property);
                str.Append(property.Name);
                friend = property;
            }
            str.Append($",{idx++},\"{property.Name}\",\"{property.Entity.SaveTableName}\",\"{property.DbFieldName}\",{ReadWrite(property)})");
            properties.Add(str.ToString());
        }

        public static string ReadWrite(IFieldConfig property)
        {
            var dbReadWrite = new List<string>();
            if (property.NoStorage)
            {
                return "ReadWriteFeatrue.None";
            }
            else
            {
                if (!property.KeepStorageScreen.HasFlag(StorageScreenType.Read))
                    dbReadWrite.Add("ReadWriteFeatrue.Read");
                if (!property.IsIdentity && !property.KeepStorageScreen.HasFlag(StorageScreenType.Insert))
                    dbReadWrite.Add("ReadWriteFeatrue.Insert");
                if (!property.KeepStorageScreen.HasFlag(StorageScreenType.Update))
                    dbReadWrite.Add("ReadWriteFeatrue.Update");
            }
            return string.Join(" | ", dbReadWrite);
        }

        static void PropertyStruct(StringBuilder codeStruct, IFieldConfig property)
        {
            var featrue = new List<string>();
            if (!property.DbInnerField)

                if (!property.DbInnerField)
                {
                    featrue.Add("PropertyFeatrue.Property");
                }

            if (property.NoStorage)
            {
                featrue.Add("PropertyFeatrue.Property");
            }
            else
            {
                featrue.Add("PropertyFeatrue.Field");
                if (!property.DbInnerField)
                    featrue.Add("PropertyFeatrue.Property");
                else
                    featrue.Add("PropertyFeatrue.None");
            }
            codeStruct.Append($@"

            /// <summary>
            /// {ToRemString(property.Caption)}
            /// </summary>
            public static PropertyDefault {property.Name} = new PropertyDefault
            {{
                Name           = ""{property.Name}"",
                ValueType      = PropertyValueType.{CsharpHelper.PropertyValueType(property)},
                CanNull        = {(property.Nullable ? "true" : "false")},
                PropertyType   = typeof({property.CustomType ?? property.CsType}),
                PropertyFeatrue= {string.Join(" | ", featrue)},
                DbType         = {DbType(property)},
                FieldName      = ""{property.DbFieldName}"",
                TableName      = ""{property.Entity.SaveTableName}"",
                DbReadWrite    = {ReadWrite(property)},
                JsonName       = ""{property.JsonName}"",
                CanImport      = {(property.ExtendConfigListBool["easyui", "CanImport"] ? "true" : "false")},
                CanExport      = {(property.ExtendConfigListBool["easyui", "CanExport"] ? "true" : "false")},
                Entity         = ""{property.Entity.Name}"",
                Caption        = @""{property.Caption}"",
                Description    = @""{property.Description}""
            }};");
        }

        static string DbType(IFieldConfig field)
        {
            return field.Entity.Parent.DbType == DataBaseType.SqlServer
                ? $"(int)System.Data.SqlDbType.{SqlServerHelper.ToSqlDbType(field.DbType, field.CsType)}"
                : $"(int)MySqlConnector.MySqlDbType.{MySqlHelper.ToSqlDbType(field.DbType, field.CsType)}";
        }

        #endregion

        #region 数据访问对象构造
        private string ProviderHelper()
        {
            StringBuilder code = new StringBuilder();
            code.Append(@"
        static DataAccessOption GetOption<TEntity>()
        {
            return typeof(TEntity).Name switch
            {");

            foreach (var entity in Project.Entities)
            {
                code.Append($@"
                nameof({entity.EntityName}) => {entity.EntityName}DataOperator.Option,");
            }
            foreach (var entity in Project.Models)
            {
                code.Append($@"
                nameof({entity.EntityName}) => {entity.EntityName}DataOperator.Option,");
            }
            code.Append(@"
                _ => null,
            };
        }

        static object GetDataOperator<TEntity>()
            where TEntity : class, new()
        {
            return typeof(TEntity).Name switch
            {");
            foreach (var entity in Project.Entities)
            {
                code.Append($@"
                nameof({entity.EntityName}) => new {entity.EntityName}DataOperator(),");
            }
            foreach (var entity in Project.Models)
            {
                code.Append($@"
                nameof({entity.EntityName}) => new {entity.EntityName}DataOperator(),");
            }
            code.Append(@"
                _ => null,
            };
        }

        static object GetEntityOperator<TEntity>()
            where TEntity : class, new()
        {
            return typeof(TEntity).Name switch
            {");
            foreach (var entity in Project.Entities)
            {
                code.Append($@"
                nameof({entity.EntityName}) => new {entity.EntityName}DataOperator(),");
            }
            foreach (var entity in Project.Models)
            {
                code.Append($@"
                nameof({entity.EntityName}) => new {entity.EntityName}DataOperator(),");
            }
            code.Append(@"
                _ => null,
            };
        }");
            return code.ToString();
        }
        /*
         
        static DataAccessOption GetOption<TEntity>()
        {
            return typeof(TEntity).Name switch
            {
                nameof(EventSubscribeData) => EventSubscribeDataOperator.Option,
                _ => null,
            };
        }

        static object GetDataOperator<TEntity>()
            where TEntity : class, new()
        {
            return typeof(TEntity).Name switch
            {
                nameof(EventSubscribeData) => new EventSubscribeDataOperator(),
                _ => new DataOperator<TEntity>(),
            };
        }

        static object GetEntityOperator<TEntity>()
            where TEntity : class, new()
        {
            return typeof(TEntity).Name switch
            {
                nameof(EventSubscribeData) => new EventSubscribeDataOperator(),
                _ => new DataOperator<TEntity>(),
            };
        }
         */
        #endregion
    }
}
