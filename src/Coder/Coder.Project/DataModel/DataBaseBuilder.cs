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
using System.Threading.Tasks;
using System.Linq.Expressions;
using Agebull.Common;
using Agebull.Common.Configuration;
using Agebull.Common.Ioc;
using Agebull.EntityModel.Common;
using ZeroTeam.MessageMVC.ZeroApis;
using Agebull.EntityModel.{Project.DbType};
{Project.UsingNameSpaces}
#endregion

namespace {Project.NameSpace}.DataAccess
{{
    partial class {Project.DataBaseObjectName}
    {{
        #region 数据结构
        {EntityStruct()}
        #endregion
        #region 数据访问对象

        #region 基础

        /// <summary>
        /// 自动构建数据库对象
        /// </summary>
        internal static readonly Func<IDataBase> CreateDataBase = () => DependencyHelper.GetService<{Project.DataBaseObjectName}>();

        /// <summary>
        /// 提供器字典
        /// </summary>
        internal static readonly Dictionary<Type, object> providers = new Dictionary<Type, object>();

        {ProviderHelper()}

        /// <summary>
        /// 构造数据访问对象
        /// </summary>
        /// <returns></returns>
        internal static DataAccessProvider<TEntity> GetProvider<TEntity>(IServiceProvider serviceProvider, bool isDynamic)
              where TEntity : class, new()
        {{
            var type = typeof(TEntity);
            if (!isDynamic && providers.TryGetValue(type, out var pro))
                return (DataAccessProvider<TEntity>)pro;
            var option = {Project.DataBaseObjectName}.GetOption(type.Name);
#if DEBUG
            if (option == null)
                throw new NotSupportedException($""{{typeof(TEntity).FullName}}没有对应配置项，请通过设计器生成"");
#endif
            var opt = {Project.DataBaseObjectName}.GetOperator(type.Name);
            var provider = new DataAccessProvider<TEntity>
            {{
                Option = option,
                ServiceProvider = serviceProvider,
                CreateDataBase = CreateDataBase,
                SqlBuilder = new MySqlSqlBuilder<TEntity>(),
                EntityOperator = (IEntityOperator<TEntity>)opt,
                DataOperator = (IDataOperator<TEntity>)opt,
                Injection = DependencyHelper.GetService<IOperatorInjection<TEntity>>(),
            }};
            provider.DataOperator.Provider = provider;
            provider.SqlBuilder.Provider = provider;
            provider.Injection.Provider = provider;
            if (!isDynamic)
                providers.TryAdd(type, provider);
            return provider;
        }}
        #endregion
{AccessCreate()}
        #endregion{FastDo()}
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
using Agebull.Common.Ioc;
using Agebull.EntityModel.Common;
using Agebull.EntityModel.{Project.DbType};
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
#endregion

namespace {Project.NameSpace}.DataAccess
{{
    /// <summary>
    /// {Project.Caption}数据库对象
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
#if DEBUG
            Logger = DependencyHelper.LoggerFactory.CreateLogger<{Project.DataBaseObjectName}>();
#endif
        }}
    }}

    /// <summary>
    /// 数据库扩展
    /// </summary>
    public static class {Project.DataBaseObjectName}Ex
    {{
        /// <summary>
        /// 构造数据访问对象
        /// </summary>
        /// <returns></returns>
        public static DataAccess<TEntity> CreateDataAccess<TEntity>(this IServiceProvider serviceProvider, bool isDynamic = true)
            where TEntity : class, new()
        {{
#if DEBUG
            var provider = {Project.DataBaseObjectName}.GetProvider<TEntity>(serviceProvider, isDynamic);
            if (provider.Option.IsQuery)
                throw new NotSupportedException($""{{ typeof(TEntity).FullName}}是一个查询，请使用CreateDataQuery方法"");
            return new DataAccess<TEntity>(provider);
#else
            return new DataAccess<TEntity>({Project.DataBaseObjectName}.GetProvider<TEntity>(serviceProvider, isDynamic));
#endif
        }}

        /// <summary>
        /// 构造数据访问对象
        /// </summary>
        /// <returns></returns>
        public static DataQuery<TEntity> CreateDataQuery<TEntity>(this IServiceProvider serviceProvider, bool isDynamic = true)
            where TEntity : class, new()
        {{
            return new DataQuery<TEntity>({Project.DataBaseObjectName}.GetProvider<TEntity>(serviceProvider, isDynamic));
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
        private string AccessCreate()
        {
            StringBuilder code = new StringBuilder();

            code.Append(@"
        /// <summary>
        /// 实体
        /// </summary>
        public static class Entities
        {");
            foreach (var entity in Project.Entities)
            {
                CreateeAccess(code, entity);
            }
            code.Append(@"
        }");
            if (Project.Models.Count == 0)
                return code.ToString();
            code.Append(@"
        
        /// <summary>
        /// 模型
        /// </summary>
        public static class Models
        {");
            foreach (var model in Project.Models)
            {
                CreateeAccess(code, model);
            }
            code.Append(@"
        }");
            return code.ToString();
        }

        private void CreateeAccess(StringBuilder code, IEntityConfig entity)
        {
            if (!entity.EnableDataBase)
                return;
            var name = entity.IsQuery ? "DataQuery" : "DataAccess";

            code.Append($@"
            /// <summary>
            /// 构造数据访问对象
            /// </summary>
            /// <param name=""isDynamic"">是否支持动态字段与排序功能</param>
            /// <returns></returns>
            public static {name}<{entity.EntityName}> {entity.Name}{name}(bool isDynamic = true)
            {{
                if (!isDynamic)
                {{
                    if (providers.TryGetValue(typeof({entity.EntityName}), out var pro))
                        return new {name}<{entity.EntityName}>((DataAccessProvider<{entity.EntityName}>)pro);
                }}
                var opt = {entity.EntityName}DataOperator.GetOption();
                var ope = new {entity.EntityName}DataOperator();
                var provider = new DataAccessProvider<{entity.EntityName}>
                {{
                    Option = opt,
                    DataOperator = ope,
                    EntityOperator = ope,
                    CreateDataBase = CreateDataBase,
                    ServiceProvider = DependencyHelper.ServiceProvider,
                    Injection = DependencyHelper.GetService<IOperatorInjection<{entity.EntityName}>>(),
                    SqlBuilder = new MySqlSqlBuilder<{entity.EntityName}> {{ Option = opt }}
                }};
                provider.SqlBuilder.Provider = provider;
                if(provider.Injection != null)
                    provider.Injection.Provider = provider;
                if (!isDynamic)
                {{
                    providers[typeof({entity.EntityName})] = provider;
                }}
                return new {name}<{entity.EntityName}>(provider);
            }}
");
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

        static string EntityStruct(IEntityConfig entity)
        {
            var codeStruct = new StringBuilder();
            int idx = 0;
            var primary = entity.PrimaryColumn;
            var last = entity.LastProperties.Where(p => p != primary && !p.NoStorage);
            EntityProperty(codeStruct, entity, primary, ref idx, false);
            foreach (var property in last.Where(p => !p.IsInterfaceField).OrderBy(p => p.Index))
            {
                EntityProperty(codeStruct, entity, property, ref idx, true);
            }
            foreach (var property in last.Where(p => p.IsInterfaceField))
            {
                EntityProperty(codeStruct, entity, property, ref idx, true);
            }

            return $@"
        #region {entity.Name}({entity.Caption})

        /// <summary>
        /// 实体{entity.Name}的数据结构
        /// </summary>
        public static class {entity.Name}_Struct_
        {{
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
            public const string primaryProperty = ""{entity.PrimaryColumn.Name}"";
            #endregion
            #region 字段
{codeStruct}
            #endregion

        }}
        #endregion
";
        }

        internal static string EntityProperty(StringBuilder codeStruct, IEntityConfig model, IFieldConfig property, ref int idx, bool pro)
        {
            var str = new StringBuilder("new EntityProperty(");
            bool isOutField = false;
            if (property.Entity != model.Entity)
            {
                if (pro)
                    return null;
                isOutField = true;
                str.Append($"{property.Entity.Name}_Struct_.{property.Field.Name}");
            }
            else if (property.IsReference && property.Option.Reference is IFieldConfig rf && rf.IsInterfaceField)
            {
                if (pro)
                    return null;
                isOutField = true;
                var friend = property.Option.Reference as IFieldConfig;
                str.Append($"GlobalDataInterfaces.{friend.Entity.Name}.{friend.Name}");
            }
            else if (property.IsLinkField)
            {
                var entity = GlobalConfig.GetEntity(property.LinkTable);
                if (entity != null)
                {
                    if (pro)
                        return null;
                    var friend = entity.Find(property.LinkField);
                    isOutField = true;
                    str.Append($"{entity.Name}_Struct_.{friend.Name}");
                }
            }
            if (!isOutField)
            {
                PropertyStruct(codeStruct, property);
                if (pro)
                    return null;
                str.Append($"{property.Entity.Name}_Struct_.{property.Field.Name}");
            }
            str.Append($", {idx++}");
            if (isOutField)
                str.Append($", \"{property.Name}\", \"{property.Entity.SaveTableName}\", \"{property.DbFieldName}\", {ReadWrite(property)}, {PropertyFeature(property)}");
            str.Append(")");
            return str.ToString();
        }

        public static string ReadWrite(IFieldConfig property)
        {
            if (property.NoStorage || property.DbInnerField || property.NoProperty)
            {
                return "ReadWriteFeatrue.None";
            }
            bool read = !property.KeepStorageScreen.HasFlag(StorageScreenType.Read);
            var insert = property.IsLinkKey || !property.IsLinkField && !property.IsIdentity && !property.KeepStorageScreen.HasFlag(StorageScreenType.Insert);
            var update = !property.IsLinkField && !property.IsIdentity && !property.KeepStorageScreen.HasFlag(StorageScreenType.Update);

            if (read && insert && update)
            {
                return "ReadWriteFeatrue.All";
            }
            if (read && insert)
            {
                return "ReadWriteFeatrue.ReadInsert";
            }
            if (read && update)
            {
                return "ReadWriteFeatrue.ReadUpdate";
            }
            if (insert && update)
            {
                return "ReadWriteFeatrue.Write";
            }
            if (insert)
            {
                return "ReadWriteFeatrue.Insert";
            }
            if (update)
            {
                return "ReadWriteFeatrue.Update";
            }
            if (read)
            {
                return "ReadWriteFeatrue.Read";
            }
            return "ReadWriteFeatrue.None";
        }

        static string PropertyFeature(IFieldConfig property)
        {
            var features = new List<string>();
            if (property.IsInterfaceField)
                features.Add("PropertyFeatrue.Interface");

            if (property.NoProperty)
            {
                if (!property.NoStorage)
                {
                    features.Add("PropertyFeatrue.Field");
                }
                return string.Join(" | ", features);
            }
            var head = property.IsInterfaceField ? "PropertyFeatrue.Interface | " : "";
            if (property.IsPrimaryKey)
            {
                return head + "PropertyFeatrue.PrimaryProperty";
            }
            if (property.NoStorage)
            {
                return head + "PropertyFeatrue.Property";
            }
            if (property.DbInnerField)
            {
                return head + "PropertyFeatrue.Field";
            }
            if (property.IsLinkKey)
            {
                return head + "PropertyFeatrue.ForeignKey";
            }
            if (property.IsLinkField)
            {
                return head + "PropertyFeatrue.OutProperty";
            }

            return head + "PropertyFeatrue.General";
        }

        static void PropertyStruct(StringBuilder codeStruct, IFieldConfig property)
        {
            string featrue;
            if (property.IsPrimaryKey)
            {
                featrue = "PropertyFeatrue.PrimaryProperty";
            }
            else if (property.NoStorage)
            {
                featrue = "PropertyFeatrue.Property";
            }
            else if (!property.DbInnerField && !property.NoProperty)
            {
                featrue = "PropertyFeatrue.General";
            }
            else
            {
                featrue = "PropertyFeatrue.Field";
            }

            codeStruct.Append($@"

            /// <summary>
            /// {property.Caption.ToRemString()}
            /// </summary>
            public static PropertyDefault {property.Name} = new PropertyDefault
            {{
                Name           = ""{property.Name}"",
                Caption        = @""{property.Caption}"",
                PropertyType   = typeof({property.CustomType ?? property.CsType}),
                ValueType      = PropertyValueType.{CsharpHelper.PropertyValueType(property)},
                CanNull        = {(property.Nullable ? "true" : "false")},
                DbType         = {DbType(property)},
                FieldName      = ""{property.DbFieldName}"",
                JsonName       = ""{property.JsonName}"",
                Entity         = entityName,
                PropertyFeatrue= {featrue},
                DbReadWrite    = {ReadWrite(property)},
                CanImport      = {(property.ExtendConfigListBool["easyui", "CanImport"] ? "true" : "false")},
                CanExport      = {(property.ExtendConfigListBool["easyui", "CanExport"] ? "true" : "false")},
                Description    = @""{property.Description}""
            }};");
        }

        static string DbType(IFieldConfig field)
        {
            return field.Entity.Parent.DbType == DataBaseType.SqlServer
                ? $"(int)System.Data.SqlDbType.{SqlServerHelper.ToSqlDbType(field.DbType, field.CsType)}"
                : $"(int)MySqlConnector.MySqlDbType.{MySqlDataBaseHelper.ToSqlDbType(field.DbType, field.CsType)}";
        }

        #endregion

        #region 数据访问对象构造
        private string ProviderHelper()
        {
            StringBuilder code = new StringBuilder();
            code.Append(@"
        internal static DataAccessOption GetOption(string name)
        {
            return name switch
            {");

            foreach (var entity in Project.Entities)
            {
                code.Append($@"
                nameof({entity.EntityName}) => {entity.EntityName}DataOperator.GetOption(),");
            }
            foreach (var entity in Project.Models)
            {
                code.Append($@"
                nameof({entity.EntityName}) => {entity.EntityName}DataOperator.GetOption(),");
            }
            code.Append(@"
                _ => null,
            };
        }

        internal static object GetOperator(string name)
        {
            return name switch
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
        #endregion

        #region 快捷访问

        string FastDo()
        {
            return null;
            var code = new StringBuilder();
            code.Append($@"
        #region 快捷访问");

            List<IEntityConfig> noEntities = new List<IEntityConfig>();
            foreach (var model in Project.Models)
            {
                FastDo(model, code);
                noEntities.Add(model.Entity);
            }
            foreach (var entity in Project.Entities)
            {
                if (!noEntities.Contains(entity))
                    FastDo(entity, code);
            }
            code.Append($@"
        #endregion");
            return code.ToString();
        }

        void FastDo(IEntityConfig entity, StringBuilder code)
        {
            if (!entity.EnableDataBase)
                return;
            var head = entity is ModelConfig ? "Models" : "Entities";
            code.Append($@"
        #region {entity.Caption}
        /// <summary>
        /// 插入{entity.Caption}
        /// </summary>
        /// <param name=""entity"">{entity.Caption}实体</param>
        /// <param name=""reload"">是否读取最新数据</param>
        /// <returns>插入状态</returns>
        public static Task<bool> Insert({entity.EntityName} entity, bool reload = false)
        {{
            return {head}.{entity.Entity.Name}DataAccess().InsertAsync(entity, reload);
        }}
        /// <summary>
        /// 更新{entity.Caption}
        /// </summary>
        /// <param name=""entity"">{entity.Caption}实体</param>
        /// <param name=""reload"">是否读取最新数据</param>
        /// <returns>更新状态</returns>
        public static Task<bool> Update({entity.EntityName} entity, bool reload = false)
        {{
            return {head}.{entity.Entity.Name}DataAccess().UpdateAsync(entity, reload);
        }}
        /// <summary>
        /// 按主键载入{entity.Caption}数据
        /// </summary>
        /// <param name=""id"">主键</param>
        /// <returns>查询结果</returns>
        public static Task<{entity.EntityName}> {entity.Entity.Name}First(long id)
        {{
            return {head}.{entity.Entity.Name}DataAccess().LoadByPrimaryKeyAsync(id);
        }}
        /// <summary>
        /// 按条件查询首行{entity.Caption}数据
        /// </summary>
        /// <param name=""lambda""></param>
        /// <returns>查询结果</returns>
        public static Task<{entity.EntityName}> First{entity.Entity.Name}(LambdaItem<{entity.EntityName}> lambda)
        {{
            return {head}.{entity.Entity.Name}DataAccess().FirstAsync(lambda);
        }}
        /// <summary>
        /// 按条件查询首行{entity.Caption}数据
        /// </summary>
        /// <param name=""lambda""></param>
        /// <returns>查询结果</returns>
        public static Task<{entity.EntityName}> First{entity.Entity.Name}(Expression<Func<{entity.EntityName}, bool>> lambda)
        {{
            return {head}.{entity.Entity.Name}DataAccess().FirstAsync(lambda);
        }}
        /// <summary>
        /// 按条件查询所有{entity.Caption}数据
        /// </summary>
        /// <param name=""lambda"">查询条件</param>
        /// <returns>{entity.Caption}数据列表</returns>
        public static Task<List<{entity.EntityName}>> All{entity.Entity.Name}(LambdaItem<{entity.EntityName}> lambda)
        {{
            return {head}.{entity.Entity.Name}DataAccess().AllAsync(lambda);
        }}
        /// <summary>
        /// 按条件查询所有{entity.Caption}数据
        /// </summary>
        /// <param name=""lambda"">查询条件</param>
        /// <returns>{entity.Caption}数据列表</returns>
        public static Task<List<{entity.EntityName}>> All{entity.Entity.Name}(Expression<Func<{entity.EntityName}, bool>> lambda)
        {{
            return {head}.{entity.Entity.Name}DataAccess().AllAsync(lambda);
        }}
        /// <summary>
        /// 按条件读取分页{entity.Caption}数据
        /// </summary>
        /// <param name=""page""></param>
        /// <param name=""limit""></param>
        /// <param name=""lambda""></param>
        /// <returns>{entity.Caption}分页数据</returns>
        public static Task<ApiPageData<{entity.EntityName}>> Load{entity.Entity.Name}Page(int page,int limit, LambdaItem<{entity.EntityName}> lambda=null)
        {{
            return {head}.{entity.Entity.Name}DataAccess().PageAsync(page,limit,lambda);
        }}
        /// <summary>
        /// 按条件读取分页{entity.Caption}数据
        /// </summary>
        /// <param name=""page""></param>
        /// <param name=""limit""></param>
        /// <param name=""lambda""></param>
        /// <returns>{entity.Caption}分页数据</returns>
        public static Task<ApiPageData<{entity.EntityName}>> Load{entity.Entity.Name}Page(int page,int limit, Expression<Func<{entity.EntityName}, bool>> lambda=null)
        {{
            return {head}.{entity.Entity.Name}DataAccess().PageAsync(page,limit,lambda);
        }}
        #endregion");
        }
        #endregion
    }
}
