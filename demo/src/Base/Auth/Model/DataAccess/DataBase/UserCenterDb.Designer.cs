/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/2 18:25:27*/
#region

using Agebull.EntityModel.Common;
using Agebull.EntityModel.Redis;
using Agebull.EntityModel.Events;


#endregion

namespace Agebull.Common.OAuth.DataAccess
{
    /// <summary>
    /// 本地数据库
    /// </summary>
    public partial class UserCenterDb
    {
        /// <summary>
        /// 构造
        /// </summary>
        static UserCenterDb()
        {
            /*tableSql = new Dictionary<string, TableSql>(StringComparer.OrdinalIgnoreCase)
            {UserCenterDb
            };*/
            DataUpdateHandler.RegisterUpdateHandler(new MySqlDataTrigger());
            DataUpdateHandler.RegisterUpdateHandler(new RedisDataTrigger());
        }

        /// <summary>
        /// 构造
        /// </summary>
        public UserCenterDb()
        {
            Name = @"Auth";
            Caption = @"认证中心";
            Description = @"认证中心";
            Initialize();
            //RegistToEntityPool();
        }
        
        /// <summary>
        /// 初始化
        /// </summary>
        partial void Initialize();
    }
}