using Agebull.Common.Configuration;
using Agebull.EntityModel.MySql;

namespace Agebull.ZeroNet.ManageApplication.DataAccess
{
    /// <summary>
    ///     本地数据库
    /// </summary>
    public  partial class AnnexDb : MySqlDataBase
    {
        /// <summary>
        ///     读取连接字符串
        /// </summary>
        /// <returns></returns>
        protected override string LoadConnectionStringSetting()
        {
            return ConfigurationManager.ConnectionStrings["AnnexDb"];
        }
    }
}