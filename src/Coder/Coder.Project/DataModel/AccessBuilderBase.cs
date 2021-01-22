using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder.DataBase.MySql;

namespace Agebull.EntityModel.RobotCoder
{
    public abstract class AccessBuilderBase : CoderWithModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_Model_DataOperator";


        ///<inheritdoc/>
        protected override void CreateDesignerCode(string path)
        {
            var file = Path.Combine(path, $"{Model.EntityName}DataOperator.Designer.cs");
            SaveCode(file, Code());
        }
        ///<inheritdoc/>
        protected override void CreateCustomCode(string path)
        {
            var file = Path.Combine(path, $"{Model.EntityName}DataOperator.cs");
            SaveCode(file, $@"#region
using System;
using System.Collections.Generic;
{Project.UsingNameSpaces}
#endregion

namespace {Project.NameSpace}.DataAccess
{{
    /// <summary>
    /// {Model.Description}
    /// </summary>
    partial class {Model.EntityName}DataOperator
    {{
        /*// <summary>
        ///     设置属性值
        /// </summary>
        /// <param name=""entity"">实体</param>
        /// <param name=""property"">属性，设置为空可以阻止后续操作</param>
        /// <param name=""value"">值</param>
        partial void SetValue({Model.EntityName} entity, ref string property, object value);

        /// <summary>
        ///     读取属性值
        /// </summary>
        /// <param name=""entity"">实体</param>
        /// <param name=""property"">属性</param>
        /// <param name=""value"">返回值</param>
        partial void GetValue({Model.EntityName} entity, string property, ref object value);
        */
    }}
}}");
        }

        #region 主代码

        protected string Code()
        {
            return $@"#region
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

using Agebull.EntityModel.Common;
using Agebull.EntityModel.Interfaces;
using Agebull.EntityModel.{Project.DbType};
{Project.UsingNameSpaces}
using static {Project.NameSpace}.DataAccess.{Project.DataBaseObjectName};

#endregion
namespace {Project.NameSpace}.DataAccess
{{
    /// <summary>
    /// {Model.Description}
    /// </summary>
    public sealed partial class {Model.EntityName}DataOperator : IDataOperator<{Model.EntityName}> , IEntityOperator<{Model.EntityName}>
    {{
        #region 基本信息

        /// <summary>
        /// 驱动提供者信息
        /// </summary>
        public IDataAccessProvider<{Model.EntityName}> Provider {{ get; set; }}

        #endregion

        #region 配置信息

        /// <summary>
        /// 配置信息
        /// </summary>
        public static DataAccessOption GetOption() => new DataAccessOption(TableOption);

        /// <summary>
        /// 实体结构
        /// </summary>
        public readonly static EntityStruct Struct;

        /// <summary>
        /// 配置信息
        /// </summary>
        readonly static DataTableOption TableOption;

        static {Model.EntityName}DataOperator()
        {{
            Struct = new EntityStruct
            {{
                Name             = {Model.Entity.Name}_Struct_.name,
                Caption          = {Model.Entity.Name}_Struct_.caption,
                Description      = {Model.Entity.Name}_Struct_.description,
                ProjectName      = ""{Project.Name}"",
                EntityName       = ""{Model.EntityName}"",
                ReadTableName    = {Model.Entity.Name}_Struct_.tableName,
                WriteTableName   = {Model.Entity.Name}_Struct_.tableName,
                PrimaryProperty  = {Model.Entity.Name}_Struct_.primaryProperty,
                IsIdentity       = {(Model.PrimaryColumn?.IsIdentity ?? false ? "true" : "false")},{Interfaces(Model)}
                Properties       = new List<EntityProperty>
                {{
                    {EntityStruct()}
                }}
            }};

            TableOption = new DataTableOption
            {{
                IsQuery          = false,
                UpdateByMidified = true,
                EventLevel       = EventEventLevel.{(Model.EnableDataEvent ? "Details" : "None")},
                InjectionLevel   = InjectionLevel.All,
                SqlBuilder       = new MySqlSqlBuilder<{Model.EntityName}>(),
                DataStruct       = Struct,
                ReadTableName    = FromSqlCode,
                WriteTableName   = {Model.Entity.Name}_Struct_.tableName,
                LoadFields       = LoadFields,
                OrderbyFields    = OrderbyFields,
                Having           = Having,
                GroupFields      = GroupFields,
                UpdateFields     = UpdateFields,
                InsertFieldCode  = InsertFieldCode,
                InsertValueCode  = InsertValueCode,
                DeleteSqlCode    = DeleteSqlCode
            }};
            TableOption.Initiate();
        }}

        #endregion

        #region SQL

        /// <summary>
        /// 读取的字段
        /// </summary>
        public const string LoadFields = @""{LoadFields()}"";


        /// <summary>
        /// 排序的字段
        /// </summary>
        public const string OrderbyFields = @""{OrderbyFields()}"";

        /// <summary>
        /// 汇总条件
        /// </summary>
        public const string Having = {Having()};

        /// <summary>
        /// 分组字段
        /// </summary>
        public const string GroupFields = {GroupFields()};

        /// <summary>
        /// 读取的字段
        /// </summary>
        public const string FromSqlCode = @""{ReadTableName()}"";

        /// <summary>
        /// 更新的字段
        /// </summary>
        public const string UpdateFields = @""{UpdateFields()}"";

        /// <summary>
        /// 写入的Sql
        /// </summary>
        public const string InsertFieldCode = @""{InsertFieldCode()}"";

        /// <summary>
        /// 写入的Sql
        /// </summary>
        public const string InsertValueCode = @""{InsertValuesCode()}"";

        /// <summary>
        /// 删除的Sql
        /// </summary>
        public const string DeleteSqlCode = @""{DeleteSql()}"";

        #endregion

        #region IDataOperator
{GetDbTypeCode()}

{LoadEntityCode()}

{CreateFullSqlParameter()}

        #endregion

        #region IEntityOperator
{GetSetValues()}
        #endregion
    }}
}}";
        }


        #endregion

        #region SQL构造

        /// <summary>
        /// 读取的字段
        /// </summary>
        protected abstract string LoadFields();

        /// <summary>
        /// 汇总条件
        /// </summary>
        protected abstract string Having();

        /// <summary>
        /// 分组字段
        /// </summary>
        protected abstract string GroupFields();
        protected abstract string OrderbyFields();

        protected abstract string ReadTableName();

        protected abstract string DeleteSql();

        protected abstract string InsertFieldCode();
        protected abstract string InsertValuesCode();
        protected abstract string UpdateFields();

        #endregion

        #region 数据结构

        static string Interfaces(IEntityConfig entity)
        {
            var it = new StringBuilder();
            bool first = true;
            foreach (var i in entity.Interfaces?.Split(',', System.StringSplitOptions.RemoveEmptyEntries))
            {
                if (first)
                {
                    first = false;
                    it.Append(@"
                InterfaceFeature = new HashSet<string>{");
                }
                else
                    it.Append(',');
                it.Append($"\"{i}\"");
            }
            if (first)
                return null;
            it.Append("},");
            return it.ToString(); ;
        }


        protected string EntityStruct()
        {
            var properties = new List<string>();
            var idx = 0;
            EntityStruct(Model, properties, ref idx);
            return string.Join(@",
                    ", properties);
        }

        protected void EntityStruct(IEntityConfig model, List<string> properties, ref int idx)
        {
            //if (!string.IsNullOrWhiteSpace(Model.ModelBase))
            //{
            //    var modelBase = GlobalConfig.GetModel(p => p.Name == Model.ModelBase);
            //    EntityStruct(modelBase, properties, ref idx);
            //}
            var code = new StringBuilder();
            var primary = model.PrimaryColumn;
            var last = model.LastProperties.Where(p => p != primary && !p.NoStorage);
            properties.Add(DataBaseBuilder.EntityProperty(code, model, primary, ref idx, false));
            foreach (var property in last.Where(p => !p.IsInterfaceField).OrderBy(p => p.Index))
            {
                properties.Add(DataBaseBuilder.EntityProperty(code, model, property, ref idx, false));
            }
            foreach (var property in last.Where(p => p.IsInterfaceField))
            {
                properties.Add(DataBaseBuilder.EntityProperty(code, model, property, ref idx, false));
            }
        }

        #endregion

        #region 数据读取

        protected abstract string CreateFullSqlParameter();

        protected abstract string GetDbTypeCode();

        protected abstract string LoadEntityCode();

        #endregion

        #region 名称值取置

        /// <summary>
        /// </summary>
        /// <remarks></remarks>
        /// <returns></returns>
        protected string GetSetValues()
        {
            var code = new StringBuilder();

            code.Append($@"

        partial void GetValue({Model.EntityName} entity, string property, ref object value);

        /// <summary>
        ///     读取属性值
        /// </summary>
        /// <param name=""entity""></param>
        /// <param name=""property""></param>
        object IEntityOperator<{Model.EntityName}>.GetValue({Model.EntityName} entity, string property)
        {{
            if (property == null) return null;
            switch (property.ToLower()) 
            {{");

            foreach (var property in Model.PublishProperty.Where(p => p.CanGet))
            {
                var names = property.GetAliasPropertys().Select(p => p.ToLower()).ToList();
                var name = property.Name.ToLower();
                if (!names.Contains(name))
                    names.Add(name);
                name = property.DbFieldName.ToLower();
                if (!names.Contains(name))
                    names.Add(name);
                foreach (var alias in names)
                    code.AppendLine($@"
                case ""{alias}"" :");
                foreach (var alias in names)
                    code.AppendLine($@"
                    return entity.{property.Name};");
            }
            code.AppendLine(@"
            }
            object value = null;
            GetValue(entity, property, ref value);
            return value;
        }");

            code.Append($@"    

        partial void SetValue({Model.EntityName} entity, ref string property, object value);

        /// <summary>
        ///     设置属性值
        /// </summary>
        /// <param name=""entity""></param>
        /// <param name=""property""></param>
        /// <param name=""value""></param>
        void IEntityOperator<{Model.EntityName}>.SetValue({Model.EntityName} entity, string property, object value)
        {{
            SetValue(entity, ref property, value);
            if(property == null) return;
            switch(property.Trim().ToLower())
            {{");

            foreach (var property in Model.PublishProperty.Where(p => p.CanSet))
            {
                var names = property.GetAliasPropertys().Select(p => p.ToLower()).ToList();

                var varName = $"tmp{property.Name}_";

                var name = property.Name.ToLower();
                if (!names.Contains(name))
                    names.Add(name);
                name = property.DbFieldName.ToLower();
                if (!names.Contains(name))
                    names.Add(name);
                foreach (var alia in names)
                    code.Append($@"
            case ""{alia}"":");

                code.Append($@"
                if (value == null)
                     entity.{property.Name} = default;
                else if(value is {property.LastCsType} {varName})
                    entity.{property.Name} = {varName};");

                switch (property.CsType)
                {
                    case "string":
                    case "String":
                        code.Append($@"
                else
                    entity.{property.Name} = value.ToString();
                return;");
                        continue;
                    case "bool":
                    case "Boolean":
                        code.Append($@"
                else if(value is int i{property.Name})
                    entity.{property.Name} = i{property.Name} != 0;
                else if (int.TryParse(value.ToString(), out var { name}_vl))
                    entity.{ property.Name} = { name}_vl != 0; ");
                        break;
                }

                if (!string.IsNullOrWhiteSpace(property.CustomType))
                {
                    code.Append($@"
                else if(value is int i{property.Name})
                    entity.{property.Name} = ({property.CustomType})i{property.Name};
                else if (int.TryParse(value.ToString(), out i{property.Name}))
                    entity.{property.Name} = ({property.CustomType})i{property.Name};");
                }
                code.AppendLine($@"
                else if ({property.LastCsType}.TryParse(value.ToString(), out {varName}))
                    entity.{property.Name} = {varName};
                else
                    entity.{property.Name} = default;
                return;");
            }
            code.AppendLine(@"
            }
        }");

            return code.ToString();
        }

        #endregion
    }
}