using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    public abstract class AccessBuilderBase<TModel> : CoderWithModel<TModel>
        where TModel : ProjectChildConfigBase, IEntityConfig
    {
        protected string DataBaseExtend()
        {
            return $@"
    /*
    partial class {Project.DataBaseObjectName}
    {{
{TableSql()}
{TableObject()}
{TablesEnum()}
    }}*/";
        }

        protected string TableObject()
        {
            var name = Model.Name.ToPluralism();
            return $@"

        /// <summary>
        /// {Model.Description}数据访问对象
        /// </summary>
        private {Model.Name}DataAccess _{name.ToLWord()};

        /// <summary>
        /// {Model.Description}数据访问对象
        /// </summary>
        {(Model.IsInternal ? "internal" : "public")} {Model.Name}DataAccess {name}
        {{
            get
            {{
                return this._{name.ToLWord()} ?? ( this._{name.ToLWord()} = new {Model.Name}DataAccess{{ DataBase = this}});
            }}
        }}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected string TablesEnum()
        {
            return $@"

        /// <summary>
        /// {Model.Caption}({Model.ReadTableName}):{Model.Caption}
        /// </summary>
        public const int Table_{Model.Name} = 0x{Model.Index:x};";
        }

        protected string TableSql()
        {
            return $@"

        /// <summary>
        /// {Model.Description}的结构语句
        /// </summary>
        private TableSql _{Model.Name}Sql = new TableSql
        {{
            TableName = ""{Model.ReadTableName}"",
            PimaryKey = ""{Model.PrimaryColumn.Name}""
        }};";
        }

        protected IFieldConfig[] dbFields;
        /// <summary>
        ///     公开的数据库字段
        /// </summary>
        protected IFieldConfig[] PublishDbFields => dbFields ??= Model.DbFields.Where(p => !p.DbInnerField && !string.Equals(p.DbType, "EMPTY", StringComparison.OrdinalIgnoreCase)).ToArray();

        protected string FieldCode()
        {
            return $@"
        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{{ {Fields()} }};

        /// <summary>
        ///  所有字段
        /// </summary>
        public sealed override string[] Fields
        {{
            get
            {{
                return _fields;
            }}
        }}

        /// <summary>
        ///  字段字典
        /// </summary>
        public static Dictionary<string, string> fieldMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {{{FieldMap()}
        }};

        /// <summary>
        ///  字段字典
        /// </summary>
        public sealed override Dictionary<string, string> FieldMap
        {{
            get {{ return fieldMap ; }}
        }}
        #endregion";
        }

        protected string Fields()
        {
            var sql = new StringBuilder();
            var isFirst = true;
            foreach (var field in Model.DbFields)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    sql.Append(",");
                }
                sql.Append($@"""{field.Name}""");
            }
            return sql.ToString();
        }

        protected string FieldMap()
        {
            var sql = new StringBuilder();
            var names = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            FieldMap(Model, names);
            var isFirst = true;
            foreach (var field in names)
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
            {{ ""{field.Key}"" , ""{field.Value}"" }}");
            }
            return sql.ToString();
        }


        protected void FieldMap(TModel entity, Dictionary<string, string> names)
        {
            if (entity == null)
            {
                return;
            }
            if (!string.IsNullOrWhiteSpace(entity.ModelBase))
            {
                if (typeof(TModel) == typeof(ModelConfig))
                    FieldMap(Project.Models.FirstOrDefault(p => p.Name == entity.ModelBase) as TModel, names);
                else
                    FieldMap(Project.Entities.FirstOrDefault(p => p.Name == entity.ModelBase) as TModel, names);
            }
            foreach (var field in entity.DbFields)
            {
                if (!names.ContainsKey(field.Name))
                {
                    names.Add(field.Name, field.DbFieldName);
                }
                if (!names.ContainsKey(field.DbFieldName))
                {
                    names.Add(field.DbFieldName, field.DbFieldName);
                }
                foreach (var alia in field.GetAliasPropertys())
                {
                    if (!names.ContainsKey(alia))
                    {
                        names.Add(alia, field.DbFieldName);
                    }
                }
            }
            if (!names.ContainsKey("Id"))
            {
                names.Add("Id", entity.PrimaryColumn.DbFieldName);
            }
        }



    }
}