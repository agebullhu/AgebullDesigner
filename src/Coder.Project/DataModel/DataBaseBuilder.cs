using System.IO;
using Agebull.EntityModel.Config;

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
        /// <summary>
        ///     生成实体代码
        /// </summary>
        protected override void CreateBaCode(string path)
        {
            string file = Path.Combine(path, Project.DataBaseObjectName + ".Designer.cs");
            if (File.Exists(file))
                File.Delete(file);
        }

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected override void CreateExCode(string path)
        {
            string dbNameSpace;
            switch (Project.DbType)
            {
                case DataBaseType.SqlServer:
                    dbNameSpace = "System.Data.Sql";
                    break;
                case DataBaseType.Sqlite:
                    dbNameSpace = "Microsoft.Data.Sqlite";
                    break;
                default:
                    //case DataBaseType.MySql:
                    dbNameSpace = "MySql.Data.MySqlClient";
                    break;
            }
            string file = Path.Combine(path, Project.DataBaseObjectName + ".cs");
            string code = $@"#region
using System;
using System.Collections.Generic;
using System.Data;
using {dbNameSpace};
using Agebull.Common;
using Agebull.Common.Configuration;
using Agebull.EntityModel.{Project.DbType};
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

            ConnectionStringName = ""{Project.DataBaseObjectName}"";
        }}{SqliteCode()}
    }}
}}";
            SaveCode(file, code);
        }
        string SqliteCode()
        {
            if (Project.DbType != DataBaseType.Sqlite)
                return null;

            return $@"
        /// <summary>
        /// 读取连接字符串
        /// </summary>
        /// <returns></returns>
        protected override string LoadConnectionStringSetting()
        {{
            var b = new SqliteConnectionStringBuilder
            {{
                DataSource = ZeroAppOption.Instance.IsLinux
                    ? $""{{ ZeroAppOption.Instance.DataFolder}}/{Project.DbSoruce}""
                    : $""{{ZeroAppOption.Instance.DataFolder}}\\{Project.DbSoruce}"",
                Mode = SqliteOpenMode.ReadWriteCreate,
                Cache = SqliteCacheMode.Shared
            }};
            return b.ConnectionString;
        }}";
        }
    }
}
