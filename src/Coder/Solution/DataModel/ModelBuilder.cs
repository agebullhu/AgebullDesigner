using System.IO;
using System.Linq;
using System.Text;
using Gboxt.Common.DataAccess.Schemas;

namespace Agebull.Common.SimpleDesign
{
    public sealed class ModelBuilder : SchemaCodeBuilder
    {
        #region 属性

        private string Properties()
        {
            int index = 1;
            StringBuilder code = new StringBuilder();
            foreach (var table in Table.ModelInclude.Split(','))
            {
                Properties(this.Project.Schemas.FirstOrDefault(p => p.EntityName == table), code, index++);
            }
            return code.ToString();
        }

        private void Properties(EntityConfig table, StringBuilder code, int index)
        {
            if (table == null)
                return;
            code.AppendFormat(@"

        #region {0}

        /// <summary>
        /// 载入{0}
        /// </summary>
        public void Load{2}()
        {{
            this._{3} = {2}Entity.GetById(Id);
        }}

        /// <summary>
        /// 生成{0}
        /// </summary>
        public void Create{2}()
        {{
            if(this._{3} == null)
                this._{3} = new {2}Entity{{ Id = (int)this.Id }};
            this.SetEntityStatus({4}, 1);
        }}

        partial void On{2}Seted();

        /// <summary>
        /// {0}
        /// </summary>
        internal {2}Entity _{3};

        /// <summary>
        /// {0}
        /// </summary>
        /// <remarks>
        /// {1}
        /// </remarks>
        public {2}Entity v{2}
        {{
            get
            {{
                return this._{3} ?? (EntityIsNull({4}) ? null : (this._{3} = {2}Entity.GetById(Id)));
            }}
            set
            {{
                if(this._{3} == value)
                    return;
                this._{3} = value;
                On{2}Seted();
            }}
        }}"
                        , ToRemString(table.Caption)
                        , ToRemString(table.Description)
                        , table.EntityName
                        , table.EntityName.ToLower()
                        , index);

            foreach (PropertyConfig field in table.PublishProperty)
            {
                if (field.IsPrimaryKey)
                    continue;
                code.AppendFormat(@"

        /// <summary>
        /// {0}
        /// </summary>
        /// <remarks>
        /// {1}
        /// </remarks>
        public {2} {3}
        {{
            get
            {{
                return _{6} == null && EntityIsNull({5}) ? default({2}) : this.v{4}.{3};
            }}
            set
            {{
                if(default({2}) == value)
                {{
                    return;
                }}
                if(_{6} == null || EntityIsNull({5}))
                {{
                    Create{4}();
                }}
                this.v{4}.{3} = value;
            }}
        }}"
                    , ToRemString(field.Caption)
                    , ToRemString(field.Description)
                    , field.LastCsType
                    , field.PropertyName
                    , table.EntityName
                    , index
                    , table.EntityName.ToLower());
            }
            code.Append(@"

        #endregion");
        }


        private string Interfaces()
        {
            StringBuilder code = new StringBuilder();
            code.AppendFormat(@"{0} , IPropertyJson",Table.ModelBase ?? "ModelBase");
            foreach (var table in Table.ModelInclude.Split(','))
            {
                code.AppendFormat(@" , I{0}", table);
            }
            return code.ToString();
        }
        public string JsonCode()
        {
            var code = new StringBuilder();

            foreach (var table in Table.ModelInclude.Split(','))
            {
                EntityJson(code, this.Project.Schemas.First(p => p.EntityName == table));
            }
            return code.ToString();
        }
        void EntityJson(StringBuilder code, EntityConfig table)
        {
            code.AppendFormat(@"
            if(_{0} != null)
            {{", table.EntityName.ToLower());
            foreach (PropertyConfig field in table.PublishProperty)
            {
                if (field.IsPrimaryKey)
                    continue;
                code.AppendFormat(@"
                jsonBuilder.AppendValueProperty(""{0}"",{1});"
                    , string.IsNullOrWhiteSpace(field.ClientName) ? field.PropertyName : field.ClientName
                    , PropertyName2(field));
            }
            code.Append(@"
            }");
        }
        string PropertyName2(PropertyConfig col, string pre = null, string end = null)
        {
            return col.CustomType == null
                ? string.Format("{0}{1}{2}", pre, col.PropertyName, end)
                : string.Format("({0}){1}{2}{3}", col.CsType, pre, col.PropertyName, end);
        }

        #endregion

        #region 主体代码

        /// <summary>
        ///     生成实体代码
        /// </summary>
        public override void CreateBaCode(string path)
        {
            string code = string.Format(@"using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Runtime.Serialization;
using Agebull.Common;

using HY.Model;
using HY.GameApi.Model;
using HY.Web.Apis.Context;
using HY.Web.RedisDAL;


namespace {0}
{{
    /// <summary>
    /// {1}
    /// </summary>
    /// <remarks>
    /// {2}
    /// </remarks>
    partial class {3} : {4}
    {{
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public {3}()
        {{
            Initialize();
        }}

        /// <summary>
        /// 初始化
        /// </summary>
        partial void Initialize();

        #endregion
{5}

        #region JSON序列化

        partial void OnToPropertyJson(StringBuilder jsonBuilder);

        /// <summary>
        /// 使用将目标对象序列化为以属性为基础的JSON
        /// </summary>
        protected override void ToPropertyJson(StringBuilder jsonBuilder)
        {{
            base.ToPropertyJson(jsonBuilder);
{6}
            OnToPropertyJson(jsonBuilder);
        }}

        #endregion
    }}
}}"
                , NameSpace
                , this.ToRemString(Table.Caption)
                , this.ToRemString(Table.Description)
                , Table.EntityName
                , Interfaces()
                , Properties()
                , JsonCode());
            string file = Path.Combine(path, Table.EntityName + ".Designer.cs");
            this.SaveCode(file, code);
        }

        string ExtendFunc()
        {
            StringBuilder code = new StringBuilder();
            code.AppendFormat(@"partial void On{0}Seted();", Table.EntityName);
            if (!string.IsNullOrWhiteSpace(Table.ModelInclude))
            {
                foreach (var table in Table.ModelInclude.Split(','))
                {
                    code.AppendFormat(@"
        partial void On{0}Seted();", table.Trim());
                }
            }
            return code.ToString();
        }
        /// <summary>
        ///     生成扩展代码
        /// </summary>
        public override void CreateExCode(string path)
        {
            string file = Path.Combine(path, Table.EntityName + ".cs");
            if (File.Exists(file))
                return;
            string code = string.Format(@"
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using HY.GameApi.Model;

namespace {0}
{{
    /// <summary>
    /// {1}
    /// </summary>
    public partial class {2} : ModelBase
    {{
        
        /// <summary>
        /// 初始化
        /// </summary>
        partial void Initialize()
        {{
        }}
        /*
        
        {3}       

        /// <summary>
        /// 使用将目标对象序列化为以属性为基础的JSON的扩展操作
        /// </summary>
        partial void OnToPropertyJson(StringBuilder jsonBuilder);
        */
    }}
}}"
                , NameSpace
                , Table.Description
                , Table.EntityName
                , ExtendFunc());
            this.SaveCode(file, code);
        }

        #endregion
    }
}