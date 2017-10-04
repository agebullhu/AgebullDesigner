using System.IO;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    public sealed class DataBaseBuilder : CoderWithProject
    {
        /// <summary>
        /// ����
        /// </summary>
        protected override string FileSaveConfigName => "File_Model_DataBase_cs";
        /// <summary>
        ///     ����ʵ�����
        /// </summary>
        protected override void CreateBaCode(string path)
        {

            string code = $@"
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Diagnostics;

using Gboxt.Common.DataModel;
using Gboxt.Common.DataModel.MySql;

namespace {Project.NameSpace}.DataAccess
{{
    /// <summary>
    /// �������ݿ�
    /// </summary>
    public partial class {Project.DataBaseObjectName}
    {{
        /// <summary>
        /// ����
        /// </summary>
        public {Project.DataBaseObjectName}()
        {{
            /*tableSql = new Dictionary<string, TableSql>(StringComparer.OrdinalIgnoreCase)
            {{{Project.DataBaseObjectName}
            }};*/
            Initialize();
            //RegistToEntityPool();
        }}
        
        /// <summary>
        /// ��ʼ��
        /// </summary>
        partial void Initialize();
    }}
}}
";
            string file = Path.Combine(path, Project.DataBaseObjectName + ".Designer.cs");
            SaveCode(file, code);
        }

        private string CreateDalCode()
        {
            StringBuilder code = new StringBuilder();
            code.Append(@"

namespace DALFactory
{
    /// <summary>
    /// ���ݷ���������
    /// </summary>
    public sealed class DataAccess
    {");

            foreach (var entity in SolutionConfig.Current.Entities)
            {

                string name = entity.EntityName.Substring(0, entity.EntityName.Length - 2);
                code.Append($@"

        /// <summary>
        /// ����{entity.EntityName}ʵ��
        /// </summary>
        public static IDAL.I{name} CreatGet{name}()
        {{
            return new TAG.SQLserver.{entity.EntityName}();
        }}");
            }

            code.Append(@"
    }
}");
            return code.ToString();
        }

        /// <summary>
        ///     ������չ����
        /// </summary>
        protected override void CreateExCode(string path)
        {
            string file = Path.Combine(path, Project.DataBaseObjectName + ".cs");
            string code = $@"
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

using Gboxt.Common.DataModel;
using Gboxt.Common.DataModel.MySql;

namespace {Project.NameSpace}.DataAccess
{{
    /// <summary>
    /// �������ݿ�
    /// </summary>
    sealed partial class {Project.DataBaseObjectName} : MySqlDataBase
    {{
        
        /// <summary>
        /// ��ʼ��
        /// </summary>
        partial void Initialize()
        {{
        }}
        
        /// <summary>
        /// ����ȱʡ���ݿ�
        /// </summary>
        public static void CreateDefault()
        {{
            Default = new {Project.DataBaseObjectName}();
        }}
        static {Project.DataBaseObjectName} _default;
        /// <summary>
        /// ȱʡǿ�������ݿ�
        /// </summary>
        public static {Project.DataBaseObjectName} Default
        {{
            get{{ return _default;}}
            set
            {{ 
                 DefaultDataBase =  _default = value;
            }}
        }}

        /// <summary>
        /// ��ȡ�����ַ���
        /// </summary>
        /// <returns></returns>
        protected override string LoadConnectionStringSetting()
        {{
            return ConfigurationManager.ConnectionStrings[""{Project.NameSpace}""].ConnectionString;
        }}
    }}
}}";
            if (SolutionConfig.Current.ExtendConfigListBool["IsLinming"])
                code += CreateDalCode();
            SaveCode(file, code);
        }
    }
}
//private string CtorCode()
//{
//    var sql = new StringBuilder();
//    bool isFirst = true;
//    foreach (TableSchema schema in DataBase.Schemas)
//    {
//        if (isFirst)
//        {
//            isFirst = false;
//        }
//        else
//        {
//            sql.Append(",");
//        }
//        sql.AppendFormat(@"
//        {{""{0}"",_{1}Sql}}", schema.EntityName, schema.TableName);
//    }
//    return sql.ToString();
//}

//private string TableSqlCode()
//{
//    var sql = new StringBuilder();
//    foreach (TableSchema schema in DataBase.Schemas)
//    {
//        sql.AppendLine(TableSql(schema));
//    }
//    return sql.ToString();
//}

//private string RegistCode()
//{
//    var sql = new StringBuilder();
//    foreach (TableSchema schema in DataBase.Schemas)
//    {
//        sql.AppendFormat(@"
//    EntityPool<{0}>.Table = this.{1};", schema.EntityName, schema.EntityName.ToPluralism());
//    }
//    return sql.ToString();
//}

//private string TablesCode()
//{
//    var code = new StringBuilder();
//    foreach (TableSchema schema in DataBase.Schemas)
//    {
//        string name = schema.EntityName.ToPluralism();
//        code.AppendFormat(@"

///// <summary>
///// {0}���ݷ��ʶ���
///// </summary>
//private {2}BusinessEntity _{1};

///// <summary>
///// {0}���ݷ��ʶ���
///// </summary>
//{4} {2}BusinessEntity {3}
//{{
//    get
//    {{
//        return this._{1} ?? ( this._{1} = new {2}BusinessEntity{{ DataBase = this}});
//    }}
//}}"
//            , schema.Description
//            , name.ToLWord()
//            , schema.EntityName
//            , name
//            , schema.IsInternal ? "internal" : "public");
//    }
//    return code.ToString();
//}


//private string StoreToRedis()
//{
//    var code = new StringBuilder();
//    foreach (TableSchema schema in DataBase.Schemas.Where(p => !p.IsInternal))
//    {
//        if (DataBase.ReadOnly)
//            code.AppendFormat(@"
//    Trace.WriteLine(@""{1}{2}"");
//    ReadOnlyEntityPool<{1}Entity>.LoadCache({0});"
//                , schema.EntityName.ToPluralism()
//                , schema.EntityName
//                , schema.Caption);
//        else
//            code.AppendFormat(@"
//    Trace.WriteLine(@""{1}{2}"");
//    EntityPool<{1}Entity>.Clear();
//    {0}.FeachAll(EntityPool<{1}Entity>.Current.Save, EntityPool<{1}Entity>.Current.SyncEnd);"
//                , schema.EntityName.ToPluralism()
//                , schema.EntityName
//                , schema.Caption);
//    }
//    return code.ToString();
//}

//private string TableSql(TableSchema schema)
//{
//    return string.Format(@"

///// <summary>
///// {0}�Ľṹ���
///// </summary>
//private TableSql _{1}Sql = new TableSql
//{{
//    TableName = ""{1}"",
//    PimaryKey = ""{2}""
//}};", schema.Description, schema.TableName, schema.PrimaryColumn.Name);
//}
