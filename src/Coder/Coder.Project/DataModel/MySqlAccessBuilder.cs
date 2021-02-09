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
        #region SQL构造

        protected override string LoadFields() => SqlMomentCoder.LoadSql(Model.DataTable);

        protected override string Having() => SqlMomentCoder.HavingSql(Model.DataTable);

        protected override string GroupFields() => SqlMomentCoder.GroupSql(Model.DataTable);

        protected override string OrderbyFields()
        {
            var field = Model.DataTable.Fields.FirstOrDefault(p => p.Name == Model.OrderField);
            if (field == null)
                return null;
            return Model is ModelConfig model
                ? $"{model.DataTable.ReadTableName}.{field.DbFieldName} {(Model.OrderDesc ? " DESC" : "")}"
                : $"{field.DbFieldName} {(Model.OrderDesc ? " DESC" : "")}";
        }
        protected override string ReadTableName()
        {
            var table = Model.DataTable.ReadTableName;
            var code = new StringBuilder();
            code.Append(table);
            if (Model is ModelConfig model)
            {
                foreach (var releation in model.Releations.Where(p => p.ModelType == ReleationModelType.ExtensionProperty))
                {
                    var entity = releation.PrimaryEntity == model.Entity
                        ? releation.ForeignEntity
                        : releation.PrimaryEntity;

                    var outField = releation.PrimaryEntity == model.Entity
                        ? entity.Properties.FirstOrDefault(p => p.Name == releation.ForeignKey)
                        : entity.PrimaryColumn;

                    var inField = releation.PrimaryEntity == model.Entity
                        ? model.DataTable.Fields.FirstOrDefault(p=>p == Model.PrimaryColumn)
                        : model.DataTable.Fields.FirstOrDefault(p => p.IsLinkKey && p.LinkTable == entity.Name);


                    code.AppendLine();
                    code.Append(releation.JoinType == EntityJoinType.Inner ? "INNER JOIN" : "LEFT JOIN");
                    code.Append($"`{entity.ReadTableName}` ON `{table}`.`{inField.DbFieldName}` = `{entity.ReadTableName}`.`{outField.DbFieldName}` {releation.Condition}");
                }
            }
            return code.ToString();
        }

        protected override string DeleteSql()
        {
            if (Model.Interfaces != null)
            {
                if (Model.Interfaces.Contains("ILogicDeleteData"))
                {
                    var entity = GlobalConfig.GetEntity("ILogicDeleteData");
                    return $"UPDATE `{Model.DataTable.SaveTableName}` SET `{entity.Properties[0].DbFieldName}`=1 ";
                }
                if (Model.Interfaces.Contains("IStateData"))
                {
                    var entity = GlobalConfig.GetEntity("IStateData");
                    var field = entity.Properties.FirstOrDefault(p => p.Name == "DataState");
                    return $"UPDATE `{Model.DataTable.SaveTableName}` SET `{field.DbFieldName}`=255 ";
                }
            }
            return $"DELETE FROM `{Model.DataTable.SaveTableName}`";
        }

        protected override string InsertFieldCode()
        {
            if (Model.DataTable.IsQuery)
                return null;
            var columns = Model.DataTable.Fields.Where(p => p.Entity == Model.Entity &&
                    !p.IsIdentity && !p.IsReadonly && !p.CustomWrite &&
                    !p.DbInnerField &&
                    !p.KeepStorageScreen.HasFlag(StorageScreenType.Insert)
                ).ToArray();

            var fields = new StringBuilder();

            var isFirst = true;
            foreach (var property in columns)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    fields.Append(" , ");
                }
                fields.Append($@"`{property.DbFieldName}`");
            }
            return fields.ToString();
        }
        protected override string InsertValuesCode()
        {
            if (Model.DataTable.IsQuery)
                return null;
            var columns = Model.DataTable.Fields.Where(p => p.Entity == Model.Entity &&
                    !p.IsIdentity && !p.IsReadonly && !p.CustomWrite &&
                    !p.DbInnerField &&
                    !p.KeepStorageScreen.HasFlag(StorageScreenType.Insert)
                ).ToArray();
            var values = new StringBuilder();
            var isFirst = true;
            foreach (var property in columns)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    values.Append(" , ");
                }
                values.Append($@"?{property.Name}");
            }
            return values.ToString();
        }

        protected override string UpdateFields()
        {
            if (Model.DataTable.IsQuery)
                return null;
            var isFirst = true;
            var sql = new StringBuilder();
            var columns = Model.DataTable.Fields.Where(p => p.Entity == Model.Entity &&
                    !p.IsIdentity && !p.IsReadonly && !p.CustomWrite &&
                    !p.DbInnerField &&
                    !p.KeepStorageScreen.HasFlag(StorageScreenType.Update)
                ).ToArray();

            foreach (var property in columns)
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
       `{property.DbFieldName}` = ?{property.Name}");
            }
            return sql.ToString();
        }

        #endregion

        #region 数据读取

        protected override string CreateFullSqlParameter()
        {
            var code = new StringBuilder();
            code.Append($@"

        /// <summary>
        /// 设置插入数据的命令
        /// </summary>
        /// <param name=""entity"">实体对象</param>
        /// <param name=""cmd"">命令</param>
        /// <returns>返回真说明要取主键</returns>
        public void SetEntityParameter(DbCommand cmd,{Model.EntityName} entity)
        {{");

            foreach (var field in Model.DataTable.Fields.Where(p => !p.DbInnerField).OrderBy(p => p.Index))
            {
                if (!string.IsNullOrWhiteSpace(field.Property.CustomType))
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

        protected override string GetDbTypeCode()
        {
            var code = new StringBuilder();
            code.Append(@"
        /// <summary>
        /// 得到字段的DbType类型
        /// </summary>
        /// <param name=""property"">字段名称</param>
        /// <returns>参数</returns>
        public int GetDbType(string property)
        {
            if(property == null) 
               return (int)MySqlDbType.VarChar;
            switch (property)
            {");

            foreach (var field in Model.DataTable.Fields)
            {
                if (field.DbFieldName.ToLower() != field.Name.ToLower())
                    code.Append($@"
                case ""{field.DbFieldName.ToLower()}"":");

                code.Append($@"
                case ""{field.Name.ToLower()}"":
                    return (int)MySqlDbType.{MySqlDataBaseHelper.ToSqlDbType(field.FieldType, field.CsType)};");
            }

            code.Append(@"
                default:
                    return (int)MySqlDbType.VarChar;
            }
        }");
            return code.ToString();
        }

        protected override string LoadEntityCode()
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
            foreach (var property in Model.DataTable.Fields.Where(p => !p.DbInnerField && !p.NoStorage && !p.KeepStorageScreen.HasFlag(StorageScreenType.Read)))
            {
                SqlMomentCoder.FieldReadCode(property, code, idx++);
            }
            code.Append(@"
        }");
            if (!(Model is ModelConfig model))
            {
                return code.ToString();
            }
            var array = model.Releations.Where(p => p.ModelType != ReleationModelType.ExtensionProperty
                        && p.ModelType != ReleationModelType.Custom).ToArray();
            if (array.Length != 0)
            {
                code.Append($@"

        /// <summary>
        /// 载入后
        /// </summary>
        /// <param name=""entity"">读取数据的实体</param>
        public async Task AfterLoad({Model.EntityName} entity)
        {{");

                foreach (var re in array)
                {
                    var e = GlobalConfig.GetEntity(re.ForeignTable);
                    code.Append($@"
            var access = Provider.ServiceProvider.CreateDataQuery<{e.EntityName}>();");
                    if (re.ModelType == ReleationModelType.Children)
                        code.Append($@"
            entity.{re.Name} = await access.AllAsync(p=>p.{re.ForeignKey} == entity.{re.PrimaryKey});");
                    else
                        code.Append($@"
            entity.{re.Name} = await access.FirstAsync(p=>p.{re.ForeignKey} == entity.{re.PrimaryKey});");
                }
                code.Append(@"
        }");
            }
            if (Model.DataTable.IsQuery || model.Releations.Count == 0)
                return code.ToString();
            var releations = model.Releations.Where(p => p.CanWrite).ToArray();
            if (releations.Length == 0)
                return code.ToString();
            code.Append($@"

        /// <summary>
        ///     实体保存完成后期处理(Insert/Update/Delete)
        /// </summary>
        /// <param name=""entityAccess"">当前数据访问对象</param>
        /// <param name=""entity"">实体</param>
        /// <param name=""operatorType"">操作类型</param>
        /// <remarks>
        ///     对当前对象的属性的更改,请自行保存,否则将丢失
        /// </remarks>
        public async Task AfterSave(DataAccess<{Model.EntityName}> entityAccess, {Model.EntityName} entity, DataOperatorType operatorType)
        {{");
            foreach (var re in releations)
            {
                var friend = re.PrimaryEntity == model.Entity
                    ? re.ForeignEntity
                    : re.PrimaryEntity;

                var outField = re.PrimaryEntity == model.Entity
                    ? friend.Properties.FirstOrDefault(p => p.Name == re.ForeignKey)
                    : friend.PrimaryColumn;

                var inField = re.PrimaryEntity == model.Entity
                    ? Model.DataTable.Fields.FirstOrDefault(p => p == Model.PrimaryColumn)
                    : Model.DataTable.Fields.FirstOrDefault(p => p.IsLinkKey && p.LinkTable == friend.Name);

                code.Append($@"
            {{
                var access = Provider.ServiceProvider.CreateDataAccess<{friend.EntityName}>();");

                if (re.ModelType == ReleationModelType.EntityProperty)
                    code.Append($@"
                if (entity.{friend.Name} == null || operatorType == DataOperatorType.Delete)
                {{
                    await access.DeleteAsync(p => p.{outField.Name} == entity.{inField.Name});
                }}
                else
                {{
                    if (entity.{inField.Name} == 0 || await access.AnyAsync(p => p.{outField.Name} == entity.{inField.Name}))
                    {{
                        entity.{friend.Name}.{outField.Name} = entity.{inField.Name};
                        await access.UpdateAsync(entity.entity.{friend.Name});
                    }}
                    else
                    {{
                        await access.InsertAsync(entity.entity.{friend.Name});
                        entity.{inField.Name} = entity.{friend.Name}.{outField.Name};
                        await entityAccess.SetValueAsync(p=>p.{inField.Name},entity.{inField.Name},entity.{model.PrimaryColumn.Name});
                    }}
                }}");
                else if (re.ModelType == ReleationModelType.Children)
                    code.Append($@"
                if (entity.{friend.Name} == null || entity.{friend.Name}.Count == 0 || operatorType == DataOperatorType.Delete)
                {{
                    await access.DeleteAsync(p => p.{re.ForeignKey} == entity.{re.PrimaryKey});
                }}
                else
                {{
                    foreach(var ch in entity.{friend.Name})
                    {{
                        ch.{outField.Name} = entity.{inField.Name};
                        if (ch.{friend.PrimaryColumn.Name} == 0 || await access.ExistPrimaryKeyAsync(ch.{friend.PrimaryColumn.Name}))
                            await access.UpdateAsync(ch);
                        else
                            await access.InsertAsync(ch);
                    }}
                }}");
                else
                {
                    code.Append($@"
                var ch = new {friend.EntityName}
                {{");
                    bool first = true;
                    foreach (var pro in model.PublishProperty.Where(p => p.Entity == friend))
                    {
                        if (first)
                            first = false;
                        else
                            code.Append(',');
                        code.Append($@"
                    {pro.Field.Name} = entity.{pro.Name}");
                    }
                    code.Append($@"
                }};");
                    if (re.PrimaryEntity == model.Entity)
                        code.Append($@"
                ch.{outField.Name} = entity.{inField.Name};
                var (hase,id) = await access.LoadValueAsync(p=> p.{friend.PrimaryColumn.Name} , p=>  p.{outField.Name} == entity.{inField.Name});
                ch.{outField.Name} = id;
                if (hase)
                    await access.UpdateAsync(ch);
                else
                    await access.InsertAsync(ch);");
                    else
                        code.Append($@"
                if (entity.{inField.Name} > 0)
                {{
                    ch.{outField.Name} = entity.{inField.Name};
                    await access.UpdateAsync(ch);
                }}
                else
                {{
                    await access.InsertAsync(ch);
                    entity.{inField.Name} = ch.{outField.Name};
                    await entityAccess.SetValueAsync(p=>p.{inField.Name},entity.{inField.Name},entity.{model.PrimaryColumn.Name});
                }}");
                }
                code.Append(@"
            }");
            }
            code.Append(@"
        }");
            return code.ToString();
        }

        #endregion

    }
}