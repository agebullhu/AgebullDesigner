/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/6 17:20:20*/
#region



#endregion

using Agebull.Common.Configuration;
using Agebull.EntityModel.MySql;

namespace Agebull.MicroZero.Demo.DataAccess
{
    /// <summary>
    /// 本地数据库
    /// </summary>
    sealed partial class ProjectDemoDb : MySqlDataBase
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
            Default = new ProjectDemoDb();
        }

        /// <summary>
        /// 缺省强类型数据库
        /// </summary>
        public static ProjectDemoDb Default
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
            return ConfigurationManager.ConnectionStrings["ProjectDemoDb"];
        }
    }
}