/*design by:agebull designer date:2017/11/1 16:13:52*/

using Gboxt.Common.DataModel.MySql;

namespace Agebull.Common.OAuth.DataAccess
{
    /// <summary>
    /// 本地数据库
    /// </summary>
    public sealed partial class UserCenterDb : MySqlDataBase
    {

        /// <summary>
        /// 生成缺省数据库
        /// </summary>
        public static void CreateDefault()
        {
            Default = new UserCenterDb();
        }

        /// <summary>
        /// 缺省强类型数据库
        /// </summary>
        public static UserCenterDb Default { get; private set; }

        /// <summary>
        /// 读取连接字符串
        /// </summary>
        /// <returns></returns>
        protected override string LoadConnectionStringSetting()
        {
            return Agebull.Common.Configuration.ConfigurationManager.ConnectionStrings["UserCenter"];
        }
    }
}