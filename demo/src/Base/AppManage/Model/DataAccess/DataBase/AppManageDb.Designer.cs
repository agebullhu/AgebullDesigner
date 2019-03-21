/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/1 15:31:53*/
#region

using Agebull.EntityModel.Common;
using Agebull.EntityModel.Events;
using Agebull.EntityModel.Redis;
#endregion

namespace Agebull.Common.AppManage.DataAccess
{
    /// <summary>
    /// 本地数据库
    /// </summary>
    public partial class AppManageDb
    {
        /// <summary>
        /// 构造
        /// </summary>
        static AppManageDb()
        {
            /*tableSql = new Dictionary<string, TableSql>(StringComparer.OrdinalIgnoreCase)
            {AppManageDb
            };*/
            DataUpdateHandler.RegisterUpdateHandler(new MySqlDataTrigger());
            DataUpdateHandler.RegisterUpdateHandler(new RedisDataTrigger());
        }

        /// <summary>
        /// 构造
        /// </summary>
        public AppManageDb()
        {
            Name = @"AppManage";
            Caption = @"应用管理";
            Description = @"应用管理";
            Initialize();
            //RegistToEntityPool();
        }
        
        /// <summary>
        /// 初始化
        /// </summary>
        partial void Initialize();
    }
}