using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    public class CppAccessCoder : CoderWithEntity
    {
        #region sql����

        private string FullLoadSql()
        {
            var sql = new StringBuilder();

            sql.Append(@"SELECT ");
            var isFirst = true;
            foreach (var field in Entity.CppProperty)
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
    `{0}` AS `{1}`", field.DbFieldName, field.PropertyName);
            }
            sql.Append($@"
    FROM `{Entity.ReadTableName}`
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
            return $@"DELETE FROM `{Entity.ReadTableName}` WHERE %s;";
        }

        #endregion
        #region д���SQL

        /// <summary>
        /// �ֶ�Ψһ����(��������ϼ�)
        /// </summary>
        /// <returns></returns>
        private string UniqueCondition()
        {
            if (!Entity.CppProperty.Any(p => p.UniqueIndex > 0))
                return $@"`{Entity.PrimaryColumn.DbFieldName}` = %{Entity.PrimaryColumn.Index}%";

            var code = new StringBuilder();
            var uniqueFields = Entity.CppProperty.Where(p => p.UniqueIndex > 0).OrderBy(p => p.UniqueIndex).ToArray();
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
SELECT ?__myId = `{0}` FROM `{1}` WHERE {2}", Entity.PrimaryColumn.DbFieldName, Entity.SaveTable, UniqueCondition());
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
                , Entity.PrimaryColumn.PropertyName
                , Entity.PrimaryColumn.IsIdentity ? "@@IDENTITY" : $"%{Entity.PrimaryColumn.Index}%");
            sql.Replace("\r\n", "\"\\\r\n\"");
            return sql.ToString();
        }

        private string IdInsertSql(bool isInner = false)
        {
            var sql = new StringBuilder();
            var columns = Entity.CppProperty.Where(p => !p.IsIdentity && !p.IsCompute && !p.CustomWrite && !p.KeepStorageScreen.HasFlag(StorageScreenType.Insert)).ToArray();
            sql.AppendFormat(@"INSERT INTO `{0}`
(", Entity.SaveTable);
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
            if (Entity.PrimaryColumn.IsIdentity)
            {
                sql.Append(@"
SELECT @@IDENTITY;");
            }
            sql.Replace("\r\n", "\"\\\r\n\"");
            return sql.ToString();
        }
        private string InsertSql()
        {
            return !Entity.CppProperty.Any(p => p.UniqueIndex > 0)
                ? IdInsertSql()
                : UniqueInsertSql();
        }

        private string UpdateSql(bool isInner = false)
        {
            var sql = new StringBuilder();
            IEnumerable<PropertyConfig> columns = Entity.CppProperty.Where(p => !p.IsIdentity && !p.IsCompute && !p.CustomWrite && !p.KeepStorageScreen.HasFlag(StorageScreenType.Update)).ToArray();
            sql.AppendFormat(@"UPDATE `{0}` SET", Entity.SaveTable);
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
            var columns = Entity.CppProperty.Where(p => !p.IsIdentity && !p.IsCompute && !p.CustomWrite && !p.KeepStorageScreen.HasFlag(StorageScreenType.Insert)).ToArray();

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
        #region ��ʵ�ִ���

        private string ClassCode(int space)
        {{

            var code = new StringBuilder();
            code.Append($@"

/**
* @brief ���ͻ��ʽ���Ϣ������д�뵽REDIS
* @param {{{Entity.Name}*}} field {Entity.Caption}����ָ��
* @return ��
*/
void {Entity.Name}SqlAccess::WriteToRedis(const {Entity.Name}* field)
{{

}}

/**
* @brief ���ͻ��ʽ���Ϣ������д�뵽REDIS
* @param {{{Entity.PrimaryColumn.CppLastType}}} id ����
* @return {{{Entity.Name}*}} field {Entity.Caption}����ָ��
*/
std::shared_ptr<{Entity.Name}> {Entity.Name}SqlAccess::ReadFromRedis({Entity.PrimaryColumn.CppLastType} id)
{{
    return std::shared_ptr<{Entity.Name}>();
}}
/**
* @brief ����SQL����ʽ����
*/
const boost::format sql_insert_fmt(
    ""{InsertSql()}"");

/**
* @brief ����SQL�������
* @param {{TData}} data ����
* @return {{string}} SQL���
*/
string {Entity.Name}SqlAccess::InsertSql({Entity.Name} data)
{{{SqlCode("sql_insert_fmt")}
}}

/**
* @brief ����SQL����ʽ����
*/
const boost::format sql_update_fmt(
    ""{UpdateSql()}"");

/**
* @brief ����SQL�������
* @param {{TData}} data ����
* @return {{string}} SQL���
*/
string {Entity.Name}SqlAccess::UpdateSql({Entity.Name} data)
{{{SqlCode("sql_update_fmt")}
}}


/**
* @brief ��ѯSQL����ʽ����
*/
const boost::format sql_query_fmt(
    ""{FullLoadSql()}"");

/**
* @brief ��ѯSQL�������
* @param {{string}} condition ��ѯ����
* @return {{string}} SQL���
*/
string {Entity.Name}SqlAccess::QuerySql(string condition)
{{
    auto fmt(sql_query_fmt);
    fmt % condition;
    return fmt.str();
}}

/**
* @brief ɾ��SQL����ʽ����
*/
const boost::format sql_delete_fmt(""{DeleteSql()}"");
/**
* @brief ɾ��SQL�������
* @param {{string}} condition ɾ������
* @return {{string}} SQL���
*/
string {Entity.Name}SqlAccess::DeleteSql(string condition)
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

        #region ����������

        private string ClassDef(int space)
        {

            var code = new StringBuilder();
            code.Append($@"
/**
* @brief {Entity.Name}����ģ�ͷ�װ��
*/
class {Entity.Name}SqlAccess : public MySqlAccessBase<{Entity.Name}, 3>
{{
public:
	/**
	* @brief ����
	*/
	virtual ~{Entity.Name}SqlAccess()
	{{
	}}

	/**
	* @brief Ĭ�Ϲ���
	*/
	{Entity.Name}SqlAccess()
	{{

    }}


    /**
    * @brief ���ͻ��ʽ���Ϣ������д�뵽REDIS
    * @param {{{Entity.Name}*}} field {Entity.Caption}����ָ��
    * @return ��
    */
    void WriteToRedis(const {Entity.Name}* field);

    /**
    * @brief ���ͻ��ʽ���Ϣ������д�뵽REDIS
    * @param {{{Entity.PrimaryColumn.CppLastType}}} id ����
    * @return {{{Entity.Name}*}} field {Entity.Caption}����ָ��
    */
    std::shared_ptr<{Entity.Name}> ReadFromRedis({Entity.PrimaryColumn.CppLastType} id);

	/**
	* @brief ���ƹ���
	* @param {{shared_ptr<CDBMySQL>}} mysql ���ݿ����
	*/
	{Entity.Name}SqlAccess(shared_ptr<CDBMySQL> mysql)
		: MySqlAccessBase(mysql)
	{{

    }}

	/**
	* @brief ����SQL�������
	* @param {{{Entity.Name}}} data ����
	* @return {{string}} SQL���
	*/
	virtual string InsertSql({Entity.Name} data) override;

	/**
	* @brief ����SQL�������
	* @param {{{Entity.Name}}} data ����
	* @return {{string}} SQL���
	*/
	virtual string UpdateSql({Entity.Name} data) override;

	/**
	* @brief ��ѯSQL�������
	* @param {{string}} condition ��ѯ����
	* @return {{string}} SQL���
	*/
	virtual string QuerySql(string condition) override;

	/**
	* @brief ɾ��SQL�������
	* @param {{string}} condition ɾ������
	* @return {{string}} SQL���
	*/
	virtual string DeleteSql(string condition) override;
}};");
            
            StringBuilder sb = new StringBuilder();
            sb.Append(' ', space);
            return code.Replace("\n", "\n" + sb).ToString();
        }

        #endregion

        #region �������


        /// <summary>
        /// ����
        /// </summary>
        protected override string FileSaveConfigName => "File_Model_Access_cpp";

        /// <summary>
        ///     ����ʵ�����
        /// </summary>
        protected override void CreateBaCode(string path)
        {
            var code = new StringBuilder();
            code.Append($@"
#ifndef _{Entity.Name.ToUpper()}_SQLACCESSL_H
#define _{Entity.Name.ToUpper()}_SQLACCESSL_H
#pragma once

#include ""{Entity.Name}.h""
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


            SaveCode(Path.Combine(path, Entity.Name + "SqlAccess.h"), code.ToString());
        }


        /// <summary>
        ///     ������չ����
        /// </summary>
        protected override void CreateExCode(string path)
        {
            string file = Path.Combine(path, Entity.Name + "SqlAccess.cpp");

            var code = new StringBuilder();
            code.Append($@"#include <stdafx.h>
#include ""{Entity.Name}SqlAccess.h""
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

