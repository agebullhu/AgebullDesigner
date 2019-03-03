/*design by:agebull designer date:2018/9/2 12:32:49*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Agebull.Common.WebApi.Auth;
using Gboxt.Common.DataModel.MySql;
using Agebull.Common.DataModel.BusinessLogic;
using Agebull.Common.AppManage.DataAccess;
using Agebull.Common.DataModel;
using Agebull.Common.DataModel.WebUI;
using Agebull.Common.Ioc;

namespace Agebull.Common.AppManage.BusinessLogic
{
    /// <summary>
    /// 角色权限
    /// </summary>
    public sealed partial class RolePowerBusinessLogic : UiBusinessLogicBase<RolePowerData,RolePowerDataAccess,AppManageDb>
    {
        /// <summary>
        /// 保存角色页面权限设置
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="sels"></param>
        public static void SaveRolePagePower(long roleId, string sels)
        {
            var pAccess = new RolePowerDataAccess();
            pAccess.Delete(p => p.RoleId == roleId);
            if (!string.IsNullOrWhiteSpace(sels))
            {
                var sids = sels.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (sids.Length != 0)
                {
                    foreach (var line in sids.Select(p => p.Split(',')))
                    {
                        pAccess.Insert(new RolePowerData
                        {
                            RoleId = roleId,
                            PageItemId = int.Parse(line[0]),
                            Power = RolePowerType.Allow,
                            DataScope = (SubjectionType)int.Parse(line[1])
                        });
                    }
                }
            }
            Task.Factory.StartNew(() => CacheTask(roleId));
        }

        static void CacheTask(long roleId)
        {
            var cache = IocHelper.Create<IRoleCache>();
            if (cache != null)
            {
                cache.Cache(roleId);
                cache.CachePageAuditUser();
                cache.CacheTypeUser();
            }
        }
        /// <summary>
        /// 载入角色的树形页面权限数据
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>树形页面权限数据</returns>
        public static List<EasyUiTreeNode> LoadPowers(long roleId)
        {
            return new RolePowerBusinessLogic().LoadRolePowers(roleId);
        }

        /// <summary>
        /// 载入角色的树形页面权限数据
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>树形页面权限数据</returns>
        private List<EasyUiTreeNode> LoadRolePowers(long roleId)
        {
            using (MySqlDataBaseScope.CreateScope(Access.DataBase))
            {
                var pAccess = new RolePowerDataAccess
                {
                    DataBase = Access.DataBase
                };
                var powers = pAccess.All(p => p.RoleId == roleId);

                Dictionary<long, RolePowerData> dictionary;
                try
                {
                    dictionary = powers.ToDictionary(p => p.PageItemId);
                }
                catch// (Exception e)
                {
                    dictionary = new Dictionary<long, RolePowerData>();
                    foreach (var g in powers.GroupBy(p => p.PageItemId))
                    {
                        dictionary.Add(g.Key, g.First());
                        foreach (var item in g.Skip(1))
                        {
                            pAccess.DeletePrimaryKey(item.Id);
                        }
                    }
                }
                var cache = IocHelper.Create<IRoleCache>();
                if (cache == null)
                    return new List<EasyUiTreeNode>
                    {
                        
                    };

                var tree = cache.LoadPowerTree();
                SyncPower(tree, dictionary);
                return new List<EasyUiTreeNode>
                {
                    tree
                };
            }
        }
        /// <summary>
        /// 载入角色的树形页面权限数据
        /// </summary>
        /// <param name="node"></param>
        /// <param name="powers"></param>
        /// <returns></returns>
        static void SyncPower(EasyUiTreeNode node, Dictionary<long, RolePowerData> powers)
        {
            if (node.Children == null)
                return;
            if (node.Tag == "page")
                node.IsOpen = false;
            foreach (var item in node.Children)
            {
                SyncPower(item, powers);
                RolePowerData pwoer;
                if (!powers.TryGetValue(item.ID, out pwoer))
                    continue;
                item.Extend = item.Extend;
                item.Attributes = ((int)pwoer.DataScope).ToString();
                item.IsSelect = true;
            }
        }
    }
}