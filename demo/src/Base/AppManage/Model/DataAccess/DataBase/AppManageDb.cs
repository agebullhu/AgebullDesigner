/*design by:agebull designer date:2018/9/2 13:00:45*/
using Agebull.Common.Configuration;
using Gboxt.Common.DataModel.MySql;

namespace Agebull.Common.AppManage.DataAccess
{
    /// <summary>
    /// 本地数据库
    /// </summary>
    sealed partial class AppManageDb : MySqlDataBase
    {
        
        /// <summary>
        /// 初始化
        /// </summary>
        partial void Initialize()
        {
        }
        
        /// <summary>
        /// 生成缺省数据库
        /// </summary>
        public static void CreateDefault()
        {
            Default = new AppManageDb();
        }

        /// <summary>
        /// 缺省强类型数据库
        /// </summary>
        public static AppManageDb Default
        {
            get;
            set;
        }

        /// <summary>
        /// 读取连接字符串
        /// </summary>
        /// <returns></returns>
        protected override string LoadConnectionStringSetting()
        {
            return ConfigurationManager.ConnectionStrings["UserCenter"];
        }
    }
}