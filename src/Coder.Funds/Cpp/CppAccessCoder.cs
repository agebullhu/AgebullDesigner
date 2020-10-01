using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    public class CppAccessCoder : CoderWithModel
    {
        #region sql生成

        private string FullLoadSql()
        {
            var sql = new StringBuilder();

            sql.Append(@"SELECT ");
            var isFirst = true;
            foreach (var field in Model.UserProperty)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    sql.Append(",");
                }
                sql.AppendFormat(@"
    `{0}` AS `{1}`", field.DbFieldName, field.Name);
            }
            sql.Append($@"
    FROM `{Model.ReadTableName}`
    WHERE");
            sql.Replace("\r\n", "\"\\\r\n\"");
            return sql.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string DeleteSql()
        {
            return $@"DELETE FROM `{Model.ReadTableName}` WHERE %s;";
        }

        #endregion
        #region 写相关SQL

        /// <summary>
        /// 字段唯一条件(主键或组合键)
        /// </summary>
        /// <returns></returns>
        private string UniqueCondition()
        {
            if (!Model.UserProperty.Any(p => p.UniqueIndex > 0))
                return $@"`{Model.PrimaryColumn.DbFieldName}` = %{Model.PrimaryColumn.Index}%";

            var code = new StringBuilder();
            var uniqueFields = Model.UserProperty.Where(p => p.UniqueIndex > 0).OrderBy(p => p.UniqueIndex).ToArray();
            var isFirst = true;
            foreach (var col in uniqueFields)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    code.Append(" AND ");
                }
                code.AppendFormat("`{0}`=%{1}%", col.DbFieldName, col.Index);
            }
            return code.ToString();
        }

        private string UniqueInsertSql()
        {
            var sql = new StringBuilder();
            sql.AppendFormat(@"DECLARE ?__myId INT(4);
SELECT ?__myId = `{0}` FROM `{1}` WHERE {2}", Model.PrimaryColumn.DbFieldName, Model.SaveTable, UniqueCondition());
            sql.AppendFormat(@"
IF ?__myId IS NULL
BEGIN
    {0}
    SET ?__myId = {3};
END
ELSE
BEGIN
    SET ?{2}=?__myId;
    {1}
END
SELECT ?__myId;"
                , IdInsertSql(true)
                , UpdateSql(true)
                , Model.PrimaryColumn.Name
                , Model.PrimaryColumn.IsIdentity ? "@@IDENTITY" : $"%{Model.PrimaryColumn.Index}%");
            sql.Replace("\r\n", "\"\\\r\n\"");
            return sql.ToString();
        }

        private string IdInsertSql(bool isInner = false)
        {
            var sql = new StringBuilder();
            var columns = Model.UserProperty.Where(p => !p.IsIdentity && !p.IsCompute && !p.CustomWrite && !p.KeepStorageScreen.HasFlag(StorageScreenType.Insert)).ToArray();
            sql.AppendFormat(@"INSERT INTO `{0}`
(", Model.SaveTable);
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
                sql.AppendFormat(@"
    `{0}`", field.DbFieldName);
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
                sql.AppendFormat(@"
    '%{0}%'", field.Index);
            }
            sql.Append(@"
);");

            if (isInner)
            {
                sql.Replace("\n", "\n    ");
                return sql.ToString();
            }
            if (Model.PrimaryColumn.IsIdentity)
            {
                sql.Append(@"
SELECT @@IDENTITY;");
            }
            sql.Replace("\r\n", "\"\\\r\n\"");
            return sql.ToString();
        }
        private string InsertSql()
        {
            return !Model.UserProperty.Any(p => p.UniqueIndex > 0)
                ? IdInsertSql()
                : UniqueInsertSql();
        }

        private string UpdateSql(bool isInner = false)
        {
            var sql = new StringBuilder();
            var columns = Model.UserProperty.Where(p => !p.IsIdentity && !p.IsCompute && !p.CustomWrite && !p.KeepStorageScreen.HasFlag(StorageScreenType.Update)).ToArray();
            sql.AppendFormat(@"UPDATE `{0}` SET", Model.SaveTable);
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
                sql.AppendFormat(@"
       `{0}` = '%{1}%'", field.DbFieldName, field.Index);
            }
            sql.AppendFormat(@"
 WHERE {0};", UniqueCondition());
            if (isInner)
            {
                sql.Replace("\n", "\n    ");
                return sql.ToString();
            }
            sql.Replace("\r\n", "\"\\\r\n\"");

            return sql.ToString();
        }

        private string SqlCode(string name)
        {
            var columns = Model.UserProperty.Where(p => !p.IsIdentity && !p.IsCompute && !p.CustomWrite && !p.KeepStorageScreen.HasFlag(StorageScreenType.Insert)).ToArray();

            var code = new StringBuilder();
            code.Append($@"
    auto fmt({name});
    fmt ");
            bool isfirst = true;
            foreach (var field in columns)
            {
                if (isfirst)
                    isfirst = false;
                else
                    code.Append(@"
        ");
                code.Append($@" % data.{field.Name}");
            }
            code.Append(@";
    return fmt.str();");
            return code.ToString();
        }
        #endregion
        #region 类实现代码

        private string ClassCode(int space)
        {{

            var code = new StringBuilder();
            code.Append($@"

/**
* @brief 〖客户资金信息〗本机写入到REDIS
* @param {{{Model.Name}*}} field {Model.Caption}对象指针
* @return 无
*/
void {Model.Name}SqlAccess::WriteToRedis(const {Model.Name}* field)
{{

}}

/**
* @brief 〖客户资金信息〗本机写入到REDIS
* @param {{{Model.PrimaryColumn.CppLastType}}} id 主键
* @return {{{Model.Name}*}} field {Model.Caption}对象指针
*/
std::shared_ptr<{Model.Name}> {Model.Name}SqlAccess::ReadFromRedis({Model.PrimaryColumn.CppLastType} id)
{{
    return std::shared_ptr<{Model.Name}>();
}}
/**
* @brief 新增SQL语句格式代码
*/
const boost::format sql_insert_fmt(
    ""{InsertSql()}"");

/**
* @brief 新增SQL语句生成
* @param {{TData}} data 数据
* @return {{string}} SQL语句
*/
string {Model.Name}SqlAccess::InsertSql({Model.Name} data)
{{{SqlCode("sql_insert_fmt")}
}}

/**
* @brief 更新SQL语句格式代码
*/
const boost::format sql_update_fmt(
    ""{UpdateSql()}"");

/**
* @brief 更新SQL语句生成
* @param {{TData}} data 数据
* @return {{string}} SQL语句
*/
string {Model.Name}SqlAccess::UpdateSql({Model.Name} data)
{{{SqlCode("sql_update_fmt")}
}}


/**
* @brief 查询SQL语句格式代码
*/
const boost::format sql_query_fmt(
    ""{FullLoadSql()}"");

/**
* @brief 查询SQL语句生成
* @param {{string}} condition 查询条件
* @return {{string}} SQL语句
*/
string {Model.Name}SqlAccess::QuerySql(string condition)
{{
    auto fmt(sql_query_fmt);
    fmt % condition;
    return fmt.str();
}}

/**
* @brief 删除SQL语句格式代码
*/
const boost::format sql_delete_fmt(""{DeleteSql()}"");
/**
* @brief 删除SQL语句生成
* @param {{string}} condition 删除条件
* @return {{string}} SQL语句
*/
string {Model.Name}SqlAccess::DeleteSql(string condition)
{{
    auto fmt(sql_delete_fmt);
    fmt % condition;
    return fmt.str();
}}");
            StringBuilder sb = new StringBuilder();
            sb.Append(' ', space);
            return code.Replace("\n", "\n" + sb).ToString();
        }}

        #endregion

        #region 类声明代码

        private string ClassDef(int space)
        {

            var code = new StringBuilder();
            code.Append($@"
/**
* @brief {Model.Name}数据模型封装类
*/
class {Model.Name}SqlAccess : public MySqlAccessBase<{Model.Name}, 3>
{{
public:
	/**
	* @brief 析构
	*/
	virtual ~{Model.Name}SqlAccess()
	{{
	}}

	/**
	* @brief 默认构造
	*/
	{Model.Name}SqlAccess()
	{{

    }}


    /**
    * @brief 〖客户资金信息〗本机写入到REDIS
    * @param {{{Model.Name}*}} field {Model.Caption}对象指针
    * @return 无
    */
    void WriteToRedis(const {Model.Name}* field);

    /**
    * @brief 〖客户资金信息〗本机写入到REDIS
    * @param {{{Model.PrimaryColumn.CppLastType}}} id 主键
    * @return {{{Model.Name}*}} field {Model.Caption}对象指针
    */
    std::shared_ptr<{Model.Name}> ReadFromRedis({Model.PrimaryColumn.CppLastType} id);

	/**
	* @brief 复制构造
	* @param {{shared_ptr<CDBMySQL>}} mysql 数据库对象
	*/
	{Model.Name}SqlAccess(shared_ptr<CDBMySQL> mysql)
		: MySqlAccessBase(mysql)
	{{

    }}

	/**
	* @brief 新增SQL语句生成
	* @param {{{Model.Name}}} data 数据
	* @return {{string}} SQL语句
	*/
	virtual string InsertSql({Model.Name} data) override;

	/**
	* @brief 更新SQL语句生成
	* @param {{{Model.Name}}} data 数据
	* @return {{string}} SQL语句
	*/
	virtual string UpdateSql({Model.Name} data) override;

	/**
	* @brief 查询SQL语句生成
	* @param {{string}} condition 查询条件
	* @return {{string}} SQL语句
	*/
	virtual string QuerySql(string condition) override;

	/**
	* @brief 删除SQL语句生成
	* @param {{string}} condition 删除条件
	* @return {{string}} SQL语句
	*/
	virtual string DeleteSql(string condition) override;
}};");
            
            StringBuilder sb = new StringBuilder();
            sb.Append(' ', space);
            return code.Replace("\n", "\n" + sb).ToString();
        }

        #endregion

        #region 主体代码


        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_Model_Access_cpp";

        /// <summary>
        ///     生成实体代码
        /// </summary>
        protected override void CreateDesignerCode(string path)
        {
            var code = new StringBuilder();
            code.Append($@"
#ifndef _{Model.Name.ToUpper()}_SQLACCESSL_H
#define _{Model.Name.ToUpper()}_SQLACCESSL_H
#pragma once

#include ""{Model.Name}.h""
#include ""../mysql/MySqlAccessBase.h""
using namespace GBS::Futures::DataModel;

");
            int sapce = 0;
            var ns = Project.NameSpace.Split('.');
            foreach (var name in ns)
            {
                code.AppendLine();
                code.Append(' ', sapce);
                code.Append($@"namespace {name}");
                code.AppendLine();
                code.Append(' ', sapce);
                code.Append('{');
                sapce += 4;
            }

            code.Append(ClassDef(sapce));

            for (int index = 0; index < ns.Length; index++)
            {
                sapce -= 4;
                code.AppendLine();
                code.Append(' ', sapce);
                code.Append('}');
            }

            code.Append(@"
#endif");


            SaveCode(Path.Combine(path, Model.Name + "SqlAccess.h"), code.ToString());
        }


        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected override void CreateCustomCode(string path)
        {
            string file = Path.Combine(path, Model.Name + "SqlAccess.cpp");

            var code = new StringBuilder();
            code.Append($@"#include <stdafx.h>
#include ""{Model.Name}SqlAccess.h""
using namespace GBS::Futures::DataModel;

");
            int sapce = 0;
            var ns = Project.NameSpace.Split('.');
            foreach (var name in ns)
            {
                code.AppendLine();
                code.Append(' ', sapce);
                code.Append($@"namespace {name}");
                code.AppendLine();
                code.Append(' ', sapce);
                code.Append('{');
                sapce += 4;
            }

            code.Append(ClassCode(sapce));

            for (int index = 0; index < ns.Length; index++)
            {
                sapce -= 4;
                code.AppendLine();
                code.Append(' ', sapce);
                code.Append('}');
            }


            SaveCode(file, code.ToString());
        }

        #endregion

    }
}

