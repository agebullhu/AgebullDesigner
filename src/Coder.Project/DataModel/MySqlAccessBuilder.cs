using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config.Mysql;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder.DataBase.MySql;

namespace Agebull.EntityModel.RobotCoder
{
    public sealed class MySqlAccessBuilder : AccessBuilderBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_Model_MySqlAccess";

        private string Code()
        {
            return $@"#region
using System;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

using Agebull.EntityModel.Common;
using Agebull.EntityModel.Interfaces;
{Project.UsingNameSpaces}

#endregion
namespace {SolutionConfig.Current.NameSpace}.DataAccess;
{{
    /// <summary>
    /// {Model.Description}
    /// </summary>
    public sealed class {Model.Name}DataOperator : IDataOperator<{Model.EntityName}> , IEntityOperator<{Model.EntityName}>
    {{
        /// <summary>
        /// 驱动提供者信息
        /// </summary>
        public DataAccessProvider<{Model.EntityName}> Provider {{ get; set; }}

        /// <summary>
        /// 配置信息
        /// </summary>
        internal static DataAccessOption Option = new DataAccessOption
        {{
            NoInjection = true,
            UpdateByMidified = false,
            LoadFields = LoadFields,
            UpdateFields = UpdateFields,
            InsertSqlCode = InsertSqlCode,
            DataSturct = {Project.DataBaseObjectName}.{Model.Name}_Struct_.Struct
        }};

        #region 基本SQL语句

        /// <summary>
        /// 读取的字段
        /// </summary>
        public const string LoadFields = @""{SqlMomentCoder.LoadSql(PublishDbFields)}"";

        /// <summary>
        /// 更新的字段
        /// </summary>
        public static string UpdateFields = $@""{UpdateFields()}"";

        /// <summary>
        /// 写入的Sql
        /// </summary>
        public static string InsertSqlCode => $@""{InsertSql()}"";

        #endregion

        #region 操作代码
{GetDbTypeCode()}

{LoadEntityCode()}

{CreateFullSqlParameter()}

{GetSetValues()}
        #endregion
    }}
}}";
        }

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected override void CreateCustomCode(string path)
        {
            var file = Path.Combine(path, $"{Model.Name}DataAccess.cs");
            SaveCode(file, Code());
        }

        private string InsertSql()
        {
            var sql = new StringBuilder();
            var columns = PublishDbFields.Where(p => !p.IsIdentity && !p.IsCompute && !p.CustomWrite && !p.KeepStorageScreen.HasFlag(StorageScreenType.Insert)).ToArray();
            sql.Append($@"
INSERT INTO `{Model.SaveTableName}`
(");
            var isFirst = true;
            foreach (var field in columns)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    sql.Append(",");
                }
                sql.Append($@"
    `{field.DbFieldName}`");
            }
            sql.Append(@"
)
VALUES
(");
            isFirst = true;
            foreach (var field in columns)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    sql.Append(",");
                }
                sql.Append($@"
    ?{field.Name}");
            }
            sql.Append(@"
);");
            return sql.ToString();
        }

        private string UpdateFields()
        {
            var sql = new StringBuilder();
            IEnumerable<FieldConfig> columns = PublishDbFields.Where(p => !p.IsIdentity && !p.IsCompute && !p.CustomWrite && !p.KeepStorageScreen.HasFlag(StorageScreenType.Update)).ToArray();
            var isFirst = true;
            foreach (var field in columns)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    sql.Append(",");
                }
                sql.Append($@"
       `{field.DbFieldName}` = ?{field.Name}");
            }
            return sql.ToString();
        }

        private string CreateFullSqlParameter()
        {
            var code = new StringBuilder();
            code.Append($@"

        /// <summary>
        /// 设置插入数据的命令
        /// </summary>
        /// <param name=""entity"">实体对象</param>
        /// <param name=""cmd"">命令</param>
        /// <returns>返回真说明要取主键</returns>
        public void SetEntityParameter({Model.EntityName} entity, MySqlCommand cmd)
        {{");

            foreach (var field in PublishDbFields.OrderBy(p => p.Index))
            {
                if (!string.IsNullOrWhiteSpace(field.CustomType))
                {
                    code.Append($@"
            cmd.Parameters.Add(new MySqlParameter(""{field.Name}"", ({field.CsType})entity.{field.Name}));");
                }
                else if (field.CsType.Equals("bool", StringComparison.OrdinalIgnoreCase))
                {
                    code.Append($@"
            cmd.Parameters.Add(new MySqlParameter(""{field.Name}"", entity.{field.Name} ? (byte)1 : (byte)0));");
                }
                else
                {
                    code.Append($@"
            cmd.Parameters.Add(new MySqlParameter(""{field.Name}"", entity.{field.Name}));");
                }
            }
            code.Append(@"
        }");
            return code.ToString();
        }

        private string GetDbTypeCode()
        {
            var code = new StringBuilder();
            code.Append(@"
        /// <summary>
        /// 得到字段的DbType类型
        /// </summary>
        /// <param name=""field"">字段名称</param>
        /// <returns>参数</returns>
        public int GetDbType(string field)
        {
            if(field == null) 
               return (int)MySqlDbType.VarChar;
            switch (field)
            {");
            foreach (var field in PublishDbFields)
            {
                if (field.DbFieldName.ToLower() != field.Name.ToLower())
                    code.Append($@"
                case ""{field.DbFieldName.ToLower()}"":");

                code.Append($@"
                case ""{field.Name.ToLower()}"":
                    return (int)MySqlDbType.{MySqlHelper.ToSqlDbType(field)};");
            }

            code.Append(@"
                default:
                    return (int)MySqlDbType.VarChar;
            }
        }");
            return code.ToString();
        }

        private string LoadEntityCode()
        {
            var code = new StringBuilder();
            code.Append($@"

        /// <summary>
        /// 载入数据
        /// </summary>
        /// <param name=""r"">数据读取器</param>
        /// <param name=""entity"">读取数据的实体</param>
        public async Task LoadEntity(DbDataReader r,{Model.EntityName} entity)
        {{
            var reader = r as MySqlDataReader;");
            int idx = 0;
            foreach (var field in PublishDbFields)
            {
                SqlMomentCoder.FieldReadCode(field, code, idx++);
            }
            code.Append(@"
        }");
            return code.ToString();
        }


        #region 名称值取置

        /// <summary>
        /// </summary>
        /// <remarks></remarks>
        /// <returns></returns>
        private string GetSetValues()
        {
            var code = new StringBuilder();

            code.Append($@"

        /// <summary>
        ///     读取属性值
        /// </summary>
        /// <param name=""entity""></param>
        /// <param name=""property""></param>
        object IEntityOperator<{Model.EntityName}>.GetValue({Model.EntityName} entity, string property)
        {{
            if (property == null) return null;
            return (property.Trim().ToLower()) switch
            {{");

            foreach (var field in Model.LastProperties.Where(p => p.CanGet))
            {
                var names = field.GetAliasPropertys().Select(p => p.ToLower()).ToList();
                var name = field.Name.ToLower();
                if (!names.Contains(name))
                    names.Add(name);
                foreach (var alias in names)
                    code.Append($@"
                ""{alias}"" => entity.{field.PropertyName},");

            }
            code.AppendLine(@"
                _ => null
            };
        }");

            code.Append($@"    

        /// <summary>
        ///     设置属性值
        /// </summary>
        /// <param name=""entity""></param>
        /// <param name=""property""></param>
        /// <param name=""value""></param>
        void IEntityOperator<{Model.EntityName}>.SetValue({Model.EntityName} entity, string property, object value)
        {{
            if(property == null)
                return;
            switch(property.Trim().ToLower())
            {{");

            foreach (var field in Model.LastProperties.Where(p => p.CanSet))
            {
                var names = field.GetAliasPropertys().Select(p => p.ToLower()).ToList();
                var name = field.Name.ToLower();
                if (!names.Contains(name))
                    names.Add(name);
                foreach (var alia in names)
                    code.Append($@"
            case ""{alia}"":");

                if (!string.IsNullOrWhiteSpace(field.CustomType))
                {
                    code.Append($@"
                if (value != null)
                {{
                    if(value is int)
                    {{
                        entity.{field.Name} = ({field.CustomType})(int)value;
                    }}
                    else if(value is {field.CustomType})
                    {{
                        entity.{field.Name} = ({field.CustomType})value;
                    }}
                    else
                    {{
                        var str = value.ToString();
                        {field.CustomType} val;
                        if ({field.CustomType}.TryParse(str, out val))
                        {{
                            entity.{field.Name} = val;
                        }}
                        else
                        {{
                            int vl;
                            if (int.TryParse(str, out vl))
                            {{
                                entity.{field.Name} = ({field.CustomType})vl;
                            }}
                        }}
                    }}
                }}
                return;");
                    continue;
                }

                switch (field.CsType)
                {
                    case "bool":
                    case "Boolean":
                        code.Append($@"
                if (value != null)
                {{
                    int vl;
                    if (int.TryParse(value.ToString(), out vl))
                    {{
                        entity.{field.Name} = vl != 0;
                    }}
                    else
                    {{
                        entity.{field.Name} = Convert.ToBoolean(value);
                    }}
                }}
                return;");
                        continue;
                    case "int":
                    case "long":
                        code.Append($@"
                entity.{field.Name} = ({field.CsType})Convert.ToDecimal(value);
                return;");
                        break;
                    default:
                        code.Append($@"
                entity.{field.Name} = {ConvertCode(field, "value")};
                return;");
                        break;
                }
            }
            code.AppendLine(@"
            }
        }");

            return code.ToString();
        }


        private string ConvertCode(FieldConfig column, string arg)
        {
            switch (column.CsType)
            {
                case "string":
                case "String":
                    return $"{arg} == null ? null : {arg}.ToString()";
                case "long":
                case "Int64":
                    if (column.Nullable)
                        return $"{arg} == null ? null : (long?)Convert.ToInt64({arg})";
                    return $"Convert.ToInt64({arg})";
                case "int":
                case "Int32":
                    if (column.Nullable)
                        return $"{arg} == null ? null : (int?)Convert.ToInt32({arg})";
                    return $"Convert.ToInt32({arg})";
                case "decimal":
                case "Decimal":
                    if (column.Nullable)
                        return $"{arg} == null ? null : (decimal?)Convert.ToDecimal({arg})";
                    return $"Convert.ToDecimal({arg})";
                case "float":
                case "Float":
                    if (column.Nullable)
                        return $"{arg} == null ? null : (float?)Convert.ToSingle({arg})";
                    return $"Convert.ToSingle({arg})";
                case "bool":
                case "Boolean":
                    if (column.Nullable)
                        return $"{arg} == null ? null : (bool?)Convert.ToBoolean({arg})";
                    return $"Convert.ToBoolean({arg})";
                case "DateTime":
                    if (column.Nullable)
                        return $"{arg} == null ? null : (DateTime?)Convert.ToDateTime({arg})";
                    return $"Convert.ToDateTime({arg})";
            }
            return $"({column.LastCsType}){arg}";
        }

        #endregion
    }
}