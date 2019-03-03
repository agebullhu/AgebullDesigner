using System.Collections.Generic;
using System.Linq;
using Agebull.Common.DataModel.Redis;
using Agebull.Common.DataModel.WebUI;
using Gboxt.Common.DataModel.MySql;


namespace Agebull.Common.OAuth.DataAccess
{
    /// <summary>
    /// 职位组织关联
    /// </summary>
    sealed partial class OrganizePositionDataAccess : DataStateTable<OrganizePositionData, UserCenterDb>
    {
        /// <summary>
        /// 取机构职位设置的下拉列表数据
        /// </summary>
        public static List<EasyComboValues> GetOrganizePosition(long oid)
        {
            using (var proxy = new RedisProxy(RedisProxy.DbComboCache))
            {
                var result = proxy.Get<List<EasyComboValues>>(comboKey);
                if (result != null)
                    return result;
                var access = new OrganizePositionDataAccess();
                var list = oid == 0 ? access.All() : access.All(p => p.BoundaryId == oid);
                result = list.Select(p => new EasyComboValues(p.Id, p.Department + p.Position)).ToList();
                result.Insert(0, EasyComboValues.Empty);
                proxy.Set(comboKey, result);
                return result;
            }
        }
    }
}
