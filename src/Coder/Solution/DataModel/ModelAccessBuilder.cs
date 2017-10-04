using System;
using System.IO;
using System.Linq;
using System.Text;
using Gboxt.Common.DataAccess.Schemas;

namespace Agebull.Common.SimpleDesign
{
    public sealed class ModelAccessBuilder : SchemaCodeBuilder
    {
        #region New

        string NewCode()
        {
            StringBuilder code = new StringBuilder();
            //code.Append(NewCode(Table));

            if (!string.IsNullOrWhiteSpace(Table.ModelInclude))
            {
                foreach (var table in Table.ModelInclude.Split(','))
                {
                    code.Append(NewCode(this.Project.Schemas.First(p => p.EntityName == table)));
                }
            }
            return code.ToString();
        }
        string NewCode(EntityConfig table)
        {
            return string.Format(@"
            model.Create{0}();", table.EntityName);
        }
        #endregion
        #region Delete

        string DeleteCode()
        {
            StringBuilder code = new StringBuilder();
            //code.Append(DeleteCode(Table));

            if (!string.IsNullOrWhiteSpace(Table.ModelInclude))
            {
                foreach (var table in Table.ModelInclude.Split(','))
                {
                    code.Append(DeleteCode(this.Project.Schemas.First(p => p.EntityName == table)));
                }
            }
            return code.ToString();
        }
        string DeleteCode(EntityConfig table)
        {
            return string.Format(@"
                scope.DataBase.{0}.DeletePrimaryKey(id);", table.EntityName.ToPluralism());
        }
        #endregion
        #region Insert

        string InsertCode()
        {
            StringBuilder code = new StringBuilder();
            //code.Append(InsertCode(Table));

            if (!string.IsNullOrWhiteSpace(Table.ModelInclude))
            {
                foreach (var table in Table.ModelInclude.Split(','))
                {
                    code.Append(InsertCode(this.Project.Schemas.First(p => p.EntityName == table)));
                }
            }
            return code.ToString();
        }
        string InsertCode(EntityConfig table)
        {
            return string.Format(@"
            if (model._{0} != null)
            {{
                cnt++;
                model._{0}.Id = (int)model.Id;
                {1}Entity.Add(model._{0});
            }}", table.EntityName.ToLower(), table.EntityName);
        }
        #endregion
        #region Load

        string LoadCode()
        {
            StringBuilder code = new StringBuilder();
            //code.Append(LoadCode(Table));

            if (!string.IsNullOrWhiteSpace(Table.ModelInclude))
            {
                foreach (var table in Table.ModelInclude.Split(','))
                {
                    code.Append(LoadCode(this.Project.Schemas.First(p => p.EntityName == table)));
                }
            }
            return code.ToString();
        }
        string LoadCode(EntityConfig table)
        {
            return string.Format(@"
            if (model.GetEntityStatus(1) > 0)
            {{
                model._{0} = {1}Entity.GetById(id);
            }}", table.EntityName.ToLower(), table.EntityName);
        }
        #endregion
        #region Save

        string SaveCode()
        {
            StringBuilder code = new StringBuilder();
            int index = 1;
            //code.Append(SaveCode(Table, index++));

            if (!string.IsNullOrWhiteSpace(Table.ModelInclude))
            {
                foreach (var table in Table.ModelInclude.Split(','))
                {
                    code.Append(SaveCode(this.Project.Schemas.First(p => p.EntityName == table), index++));
                }
            }
            return code.ToString();
        }
        string SaveCode(EntityConfig table, int index)
        {
            return string.Format(@"
            if (model._{0} != null && model._{0}.__EntityStatus.Subsist != EntitySubsist.None)
            {{
                cnt++;
                model.SetEntityStatus({1});
                model._{0}.Id = (int)model.Id;
                model._{0}.Save();
            }}", table.EntityName.ToLower(), index);
        }
        #endregion
        #region 主体代码

        /// <summary>
        /// 生成实体代码
        /// </summary>
        /// <param name="path"></param>
        public override void CreateBaCode(string path)
        {
            string code = string.Format(@"using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Agebull.Common.Projects;
using Gboxt.Common.DataAccess.Schemas.DataAccess;
using Gboxt.Common.SimpleDataAccess.SqlServer;
using HY.GameApi.Model;
using HY.Model;

namespace {0}
{{
    /// <summary>
    /// {1}模型访问对象
    /// </summary>
    public partial class {2}Access : IModelDataAccess<{2}>
    {{
        /// <summary>
        /// 生成新对象
        /// </summary>
        /// <param name=""doInsert"">直接插入数据库,即预生成</param>
        public {2} CreateNew(bool doInsert = true)
        {{
            var model = new {2}();
            if (doInsert)
            {{
                model.__Status.Status = new byte[] {{ 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};
                model.__Status.CreateTime = DateTime.Now.Ticks;
                ModelStatusEntity.Add(model.__Status);
            }}
            model.CreateDesignElement();{3}
            return model;
        }}

        /// <summary>
        /// 载入一条数据
        /// </summary>
        /// <param name=""id"">数据ID</param>
        public {2} Load(int id)
        {{
            var model = new {2}
            {{
                Id = id
            }};
            if (model.GetEntityStatus(0) > 0)
            {{
                model._designelement = DesignElementEntity.GetById(id);
            }}{4}
            return model;
        }}

        /// <summary>
        /// 载入分页数据
        /// </summary>
        /// <param name=""type"">数据类型</param>
        public PageData<{2}> Load(ElementType type)
        {{
            var result = new PageData<{2}>();
            using (var scope = EntityPool<DesignElementEntity>.CreateTableFunc())
            {{
                string condition = string.Format(""ElementType = {{0}}"", (int)type);
                result.Total = scope.Table.Count(condition);
                var entities = scope.Table.LoadData(condition);
                foreach (var entity in entities)
                {{
                    var model = new {2}
                    {{
                        Id = entity.Id,
                        _designelement = entity
                    }};
                    result.Data.Add(model);
                }}
            }}
            return result;
        }}

        /// <summary>
        /// 载入分页数据
        /// </summary>
        /// <param name=""page"">页号</param>
        /// <param name=""limit"">每页行数</param>
        /// <param name=""order"">排序字段</param>
        /// <param name=""condition"">数据条件</param>
        public PageData<{2}> Page(int page, int limit, string order,string condition)
        {{
            var result = new PageData<{2}>();
            using (var scope = EntityPool<DesignElementEntity>.CreateTableFunc())
            {{
                result.Total = scope.Table.Count(condition);
                var entities = scope.Table.LoadData(page, limit, order, condition);
                foreach (var entity in entities)
                {{
                    var model = new {2}
                    {{
                        Id = entity.Id,
                        _designelement = entity
                    }};
                    result.Data.Add(model);
                }}
            }}
            return result;
        }}

        /// <summary>
        /// 保存数据
        /// </summary>
        public int Save({2} model)
        {{
            int cnt = 0;
            if (model._designelement != null && model._designelement.__EntityStatus.Subsist != EntitySubsist.None)
            {{
                cnt++;
                model.SetEntityStatus(0);
                model._designelement.Id = (int)model.Id;
                model._designelement.Save();
            }}{5}
            model.__Status.ModifyTime = DateTime.Now.Ticks;
            model.__Status.Save();
            return cnt;
        }}


        /// <summary>
        /// 新增数据
        /// </summary>
        public int Insert({2} model)
        {{
            model.__Status.Status = new byte[] {{ 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};
            model.__Status.CreateTime = DateTime.Now.Ticks;
            ModelStatusEntity.Add(model.__Status);

            int cnt = 1;
            if (model._designelement != null)
            {{
                cnt++;
                model._designelement.Id = (int)model.Id;
                DesignElementEntity.Add(model._designelement);
            }}{6}
            return cnt;
        }}

        public void Delete(int id)
        {{
            using (var scope = SqlServerDataBaseScope<SchemaDesign>.CreateScope())
            {{
                scope.DataBase.ModelStatuses.DeletePrimaryKey(id);
                scope.DataBase.DesignElements.DeletePrimaryKey(id);{7}
            }}
        }}
    }}
}}"
                , this.NameSpace
                , this.ToRemString(Table.Caption)
                , Table.EntityName
                , NewCode()
                , LoadCode()
                , SaveCode()
                , InsertCode()
                , DeleteCode());
            string file = Path.Combine(path, Table.EntityName + ".Accress.cs");
            this.SaveCode(file, code);
        }


        /// <summary>
        ///     生成扩展代码
        /// </summary>
        public override void CreateExCode(string path)
        {

        }

        #endregion
    }
}