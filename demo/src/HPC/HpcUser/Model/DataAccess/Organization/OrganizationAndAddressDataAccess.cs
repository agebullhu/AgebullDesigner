/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/4/10 16:30:30*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using Agebull.EntityModel.SqlServer;

namespace HPC.Projects.DataAccess
{
    /// <summary>
    /// 组织和地址
    /// </summary>
    sealed partial class OrganizationAndAddressDataAccess : SqlServerTable<OrganizationAndAddressData,HpcSqlServerDb>
    {
        /// <summary>
        /// 构造单个读取命令
        /// </summary>
        /// <summary>编译查询条件</summary>
        /// <param name="lambda">条件</param>
        /// <returns>命令对象</returns>
        public SqlCommand CreateCommand(Expression<Func<OrganizationAndAddressData, bool>> lambda)
        {
            var convert = Compile(lambda);
            return CreateOnceCommand(convert.ConditionSql, KeyField, false, convert.Parameters);
        }
    }
}