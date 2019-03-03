using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agebull.Common.Configuration;
using Agebull.Common.DataModel.BusinessLogic;
using Agebull.Common.DataModel.Redis;
using Agebull.Common.DataModel.WebUI;
using Agebull.Common.Logging;
using Agebull.Common.OAuth.DataAccess;
using Agebull.Common.Rpc;
using Gboxt.Common.DataModel;
using Newtonsoft.Json;

namespace Agebull.Common.OAuth.BusinessLogic
{
    /// <summary>
    ///     组织机构
    /// </summary>
    public sealed partial class OrganizationBusinessLogic : BusinessLogicByStateData<OrganizationData, OrganizationDataAccess, UserCenterDb>
    {
        #region 编辑

        #region 基础继承

        /// <summary>
        ///     构造
        /// </summary>
        public OrganizationBusinessLogic()
        {
            unityStateChanged = true;
        }

        /// <summary>
        ///     保存前的操作
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="isAdd">是否为新增</param>
        /// <returns>如果为否将阻止后续操作</returns>
        protected override bool OnSaving(OrganizationData data, bool isAdd)
        {
            if (!isAdd)
                return true;
            if (data.ParentId <= 0)
                return true;
            var par = Access.LoadByPrimaryKey(data.ParentId);
            if(par == null)
                return false;
            data.AreaId = par.AreaId;
            return true;
        }

        /// <summary>
        ///     内部命令执行完成后的处理
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="cmd">命令</param>
        protected override void OnInnerCommand(OrganizationData data, BusinessCommandType cmd)
        {
            SyncTreeInfo(data);
            Cache();
        }

        #endregion


        #region 编辑树

        /// <summary>
        ///     载入完整的组织结构树(用于编辑)
        /// </summary>
        /// <returns></returns>
        public List<OrganizationData> LoadEditTree()
        {
            var root = new OrganizationData
            {
                ShortName = ConfigurationManager.GetAppSetting("orgShortName", "根"),
                FullName = ConfigurationManager.GetAppSetting("orgFullName", "根"),
                Type = OrganizationType.None
            };
            var lists = Access.All(p => p.DataState < DataStateType.Delete);
            var results = new List<OrganizationData>();
            foreach (var level in lists.GroupBy(p => p.OrgLevel)) results.AddRange(level.OrderBy(p => p.LevelIndex));
            LoadChildren(root, 0, results);
            return new List<OrganizationData> { root };
        }

        /// <summary>
        ///     设置子级以构成树
        /// </summary>
        /// b
        /// <param name="par"></param>
        /// <param name="pid"></param>
        /// <param name="lists"></param>
        private void LoadChildren(OrganizationData par, long pid, List<OrganizationData> lists)
        {
            var childs = lists.Where(p => p.ParentId == pid).ToArray();
            if (childs.Length == 0)
                return;
            foreach (var level in childs.GroupBy(p => p.OrgLevel))
                foreach (var child in level.OrderBy(p => p.LevelIndex))
                    LoadChildren(child, child.Id, lists);
            par.Children = childs;
        }

        /// <summary>
        ///     载入完整的组织结构树
        /// </summary>
        /// <returns></returns>
        public List<OrganizationData> LoadTree(long pid)
        {
            var aAccess = new GovernmentAreaDataAccess();
            var area = aAccess.LoadByPrimaryKey(pid);
            if (area == null)
                return null;
            var root = new OrganizationData
            {
                ShortName = area.ShortName,
                FullName = area.FullName,
                TreeName = area.TreeName,
                Type = OrganizationType.Region
            };
            var lists = Access.All(p => p.AreaId == pid && p.Type == OrganizationType.Organization && p.DataState < DataStateType.Delete);

            foreach (var child in lists.OrderBy(p => p.LevelIndex))
            {
                LoadChildren(child, child.Id);
            }
            root.Children = lists.ToArray();
            return new List<OrganizationData> { root };
        }
        /// <summary>
        ///     设置子级以构成树
        /// </summary>
        /// b
        /// <param name="par"></param>
        /// <param name="pid"></param>
        private void LoadChildren(OrganizationData par, long pid)
        {
            var childs = Access.All(p => p.ParentId == pid && p.DataState < DataStateType.Delete).ToArray();

            if (childs.Length == 0)
                return;
            foreach (var level in childs.GroupBy(p => p.OrgLevel))
                foreach (var child in level.OrderBy(p => p.LevelIndex))
                    LoadChildren(child, child.Id);

            par.Children = childs;
        }

        #endregion

        #region 导入

        /// <summary>
        ///     导入
        /// </summary>
        /// <param name="texts"></param>
        /// <param name="areaId"></param>
        public void Import(string texts, long areaId)
        {
            var lines = texts.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var preSp = 0;
            var stack = new FixStack<OrganizationData>();
            stack.SetFix(new OrganizationData());
            foreach (var line in lines)
            {
                var words = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length < 2)
                    continue;

                var sp = FirstSpace(line);
                if (sp == preSp)
                    stack.Pop();
                else if (sp < preSp)
                    for (var i = preSp; i >= sp; i--)
                        stack.Pop();
                preSp = sp;
                OrganizationData data;
                Access.Insert(data = new OrganizationData
                {
                    ParentId = stack.Current.Id,
                    OrgLevel = stack.StackCount,
                    FullName = words[0],
                    ShortName = words[0],
                    Type = OrganizationType.Region,
                    AreaId = areaId,
                    Code = words[1]
                });
                stack.Push(data);
            }

            SyncTreeInfo();
            Cache();
        }

        private int FirstSpace(string s)
        {
            var cnt = 0;
            foreach (var ch in s)
                if (ch == '\t' || ch == ' ')
                    cnt++;
                else
                    break;

            return cnt;
        }

        #endregion

        #region 上下级同步

        /// <summary>
        ///     设置一个对象的隶属关系
        /// </summary>
        private void SyncTreeInfo()
        {
            try
            {
                var datas = Access.All(p => p.ParentId == 0);
                foreach (var entity in datas)
                {
                    entity.BoundaryId = entity.Id;
                    entity.TreeName = entity.ShortName;
                    SyncChild(entity);
                }
            }
            catch (Exception e)
            {
                LogRecorder.Exception(e);
            }
        }

        /// <summary>
        ///     设置一个对象的隶属关系
        /// </summary>
        /// <param name="entity">数据</param>
        private void SyncTreeInfo(OrganizationData entity)
        {
            if (entity == null || entity.Id == 0)
                return;
            try
            {
                if (entity.ParentId == 0)
                {
                    entity.BoundaryId = entity.Id;
                    entity.TreeName = entity.ShortName;
                    SyncChild(entity);
                    return;
                }

                var parent = Access.LoadByPrimaryKey(entity.ParentId);
                if (parent == null)
                {
                    entity.ParentId = 0;
                    entity.BoundaryId = entity.Id;
                    entity.TreeName = entity.ShortName;
                    SyncChild(entity);
                    return;
                }
                SyncTreeInfo(parent, entity);
            }
            catch (Exception e)
            {
                LogRecorder.Exception(e);
            }
        }

        /// <summary>
        ///     同步树关系
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="entity">数据</param>
        private void SyncTreeInfo(OrganizationData parent, OrganizationData entity)
        {
            if (parent.DataState >= DataStateType.Disable)
                entity.DataState = parent.DataState;
            //if (entity.Type == OrganizationType.Region)
            //{
            //    entity.BoundaryId = entity.Id;
            //    entity.TreeName = entity.ShortName;
            //}
            //else
            {
                entity.BoundaryId = parent.BoundaryId;
                entity.TreeName = parent.TreeName + ">" + entity.ShortName;
            }
            SyncChild(entity);
        }


        /// <summary>
        ///     同步更改到下级
        /// </summary>
        /// <param name="entity">数据</param>
        private void SyncChild(OrganizationData entity)
        {
            Access.Update(entity);
            var children = Access.All(p => p.ParentId == entity.Id);
            foreach (var child in children) SyncTreeInfo(entity, child);
        }

        #endregion

        #endregion

        #region 缓存

        #region Redis缓存

        /// <summary>
        ///     生成完整的组织结构树(UI相关）
        /// </summary>
        /// <returns></returns>
        public static void CacheTask()
        {
            Task.Factory.StartNew(Cache);
        }

        /// <summary>
        ///     生成完整的组织结构树(UI相关）
        /// </summary>
        /// <returns></returns>
        public static void Cache()
        {
            try
            {
                var sl = new SubjectionBusinessLogic();
                sl.SyncSubjection();
                var bl = new OrganizationBusinessLogic();
                using (var proxy = new RedisProxy(RedisProxy.DbSystem))
                {
                    bl.CreateAreaTree(proxy);
                    bl.CreateFullOrgTree(proxy);
                    bl.CreateOrgPosTree(proxy);
                    proxy.CacheData<OrganizationData, OrganizationDataAccess, UserCenterDb>();
                    proxy.CacheData<OrganizePositionData, OrganizePositionDataAccess, UserCenterDb>();
                    proxy.CacheData<PositionPersonnelData, PositionPersonnelDataAccess, UserCenterDb>(p =>
                        $"e:pp:{p.UserId}");
                }
            }
            catch (Exception e)
            {
                LogRecorder.Exception(e);
            }
        }

        #endregion
        #region 读取

        /// <summary>
        ///     载入完整的地区树(UI相关）
        /// </summary>
        /// <returns></returns>
        public List<EasyUiTreeNodeBase> LoadComboTreeForUi()
        {
            var datas = Access.All(p => p.DataState <= DataStateType.Disable);
            var results = new List<EasyUiTreeNodeBase>
            {
                new EasyUiTreeNode
                {
                    ID = 0,
                    Text = "-"
                }
            };
            foreach (var data in datas)
                results.Add(new EasyUiTreeNode<OrganizationData>
                {
                    ID = data.Id,
                    Icon = Icon(data.Type),
                    IsOpen = true,
                    IsFolder = true,
                    Title = data.FullName,
                    Text = data.ShortName,
                    Data = data
                });

            return results;
        }

        /// <summary>
        ///     载入完整的组织结构树(UI相关）
        /// </summary>
        /// <returns></returns>
        public static OrganizationData Get(long oid)
        {
            if (oid < 0)
                oid = 0;
            using (var proxy = new RedisProxy(RedisProxy.DbSystem))
            {
                return proxy.GetEntity<OrganizationData>(oid);
            }
        }

        /// <summary>
        ///     载入完整的地区树(UI相关）
        /// </summary>
        /// <returns></returns>
        public static List<EasyUiTreeNode> LoadAreaTreeForUi(long oid)
        {
            if (oid < 0)
                oid = 0;
            using (var proxy = new RedisProxy(RedisProxy.DbSystem))
            {
                return new List<EasyUiTreeNode> { proxy.Get<EasyUiTreeNode>("ui:org:areaTree:" + oid) };
            }
        }

        /// <summary>
        ///     载入完整的组织结构树(UI相关）
        /// </summary>
        /// <returns></returns>
        public static List<EasyUiTreeNode> LoadTreeForUi(long oid)
        {
            if (oid < 0)
                oid = 0;
            using (var proxy = new RedisProxy(RedisProxy.DbSystem))
            {
                return new List<EasyUiTreeNode> { proxy.Get<EasyUiTreeNode>("ui:org:OrgTree:" + oid) };
            }
        }

        /// <summary>
        ///     载入完整的组织结构树(包含职位）
        /// </summary>
        /// <returns></returns>
        public static List<EasyUiTreeNode> LoadPostTreeForUi()
        {
            return LoadPostTreeForUi(GlobalContext.Current.Organizational.OrgId);
        }

        /// <summary>
        ///     载入完整的组织结构树(包含职位）
        /// </summary>
        /// <returns></returns>
        public static List<EasyUiTreeNode> LoadPostTreeForUi(long oid)
        {
            if (oid < 0)
                oid = 0;
            using (var proxy = new RedisProxy(RedisProxy.DbSystem))
            {
                return new List<EasyUiTreeNode> { proxy.Get<EasyUiTreeNode>("ui:org:PostTree:" + oid) };
            }
        }

        #endregion

        #region 载入完整的组织结构树(自定义回调）

        /*
        /// <summary>
        ///     载入完整的组织结构树(自定义回调）
        /// </summary>
        /// <returns></returns>
        public EasyUiTreeNode LoadTreeForCustom(long oid, string name, Func<OrganizationData, EasyUiTreeNode, bool> func)
        {
            var key = "tr:org:" + oid;
            using (var proxy = new RedisProxy(RedisProxy.DbSystem))
            {
                var root = new EasyUiTreeNode
                {
                    ID = 0,
                    Icon = "icon-global",
                    IsOpen = true,
                    Attributes = "root",
                    Text = name,
                    Children = new List<EasyUiTreeNode>()
                };
                LoadTreeForCustomInner(root, func);
                proxy.Set(key, root);
                return root;
            }
        }

        /// <summary>
        ///     载入完整的组织结构树
        /// </summary>
        /// <returns></returns>
        private void LoadTreeForCustomInner(EasyUiTreeNode root, Func<OrganizationData, EasyUiTreeNode, bool> func)
        {
            var lists = Access.All(p => p.DataState < DataStateType.Delete);
            LoadTreeForCustomInner(root, lists, func);
        }

        /// <summary>
        ///     设置子级以构成树
        /// </summary>
        /// <param name="par"></param>
        /// <param name="lists"></param>
        /// <param name="func"></param>
        private int LoadTreeForCustomInner(EasyUiTreeNode par, List<OrganizationData> lists,
            Func<OrganizationData, EasyUiTreeNode, bool> func)
        {
            var childs = lists.Where(p => p.ParentId == par.ID).ToArray();
            if (childs.Length == 0)
                return 0;
            foreach (var child in childs)
            {
                var node = new EasyUiTreeNode
                {
                    ID = child.Id,
                    Icon = child.Type == OrganizationType.Organization ? "icon-com" : "icon-bm",
                    IsOpen = true,
                    Text = child.FullName,
                    Children = new List<EasyUiTreeNode>()
                };
                if (func != null && !func(child, node))
                    continue;
                if (LoadTreeForCustomInner(node, lists, func) > 0)
                    par.Children.Add(node);
            }
            return childs.Length;
        }
        */

        #endregion


        #region 两个组织是否存在隶属关系

        /*
    /// <summary>
    ///     两个组织是否存在隶属关系
    /// </summary>
    /// <param name="parOid">上级组织</param>
    /// <param name="chdOid">下级组织</param>
    /// <returns>是否存在隶属关系</returns>
    public static bool IsSubjection(int parOid, int chdOid)
    {
        if (parOid == 0)
            return true;
        if (parOid == chdOid)
            return true;
        using (var proxy = new RedisProxy(RedisProxy.DbSystem))
        {
            return proxy.Client.SIsMember("org:sub:" + parOid, chdOid.ToByte()) == 1;
        }
    }*/

        #endregion


        #region Helper

        private EasyUiTreeNode CreateRootNode()
        {
            return CreateNode(new OrganizationData
            {
                ShortName = ConfigurationManager.GetAppSetting("orgShortName", "根"),
                FullName = ConfigurationManager.GetAppSetting("orgFullName", "根"),
                Type = OrganizationType.None
            },"root");
        }


        private static EasyUiTreeNode CreateNode(OrganizationData data, string attribute="org")
        {
            return new EasyUiTreeNode
            {
                ID = data.Id,
                Icon = Icon(data.Type),
                IsOpen = true,
                IsFolder = true,
                Attributes = attribute,
                Title = data.FullName,
                Text = data.ShortName
            };
        }
        private static EasyUiTreeNode CreateNode(GovernmentAreaData data)
        {
            return new EasyUiTreeNode
            {
                ID = data.Id,
                Icon = "icon-area",
                IsOpen = true,
                IsFolder = true,
                Attributes = "area",
                Title = data.FullName,
                Text = data.ShortName
            };
        }


        private static EasyUiTreeNode CreateNode(OrganizePositionData data)
        {
            return new EasyUiTreeNode
            {
                ID = data.Id,
                Icon = "icon-post",
                IsOpen = true,
                IsFolder = true,
                Attributes = "post",
                Text = data.Position,
                Title = data.Department + data.Position,
                Tag = data.DepartmentId.ToString()
            };
        }

        private static string Icon(OrganizationType type)
        {
            switch (type)
            {
                case OrganizationType.Region:
                    return "icon-area";
                case OrganizationType.Organization:
                    return "icon-com";
                default:
                    return "icon-bm";
            }
        }

        #endregion

        #region 生成职位结构树

        /// <summary>
        ///     载入完整的组织结构树(包含职位）
        /// </summary>
        /// <returns></returns>
        internal void CreateOrgPosTree(RedisProxy proxy)
        {
            var root = CreateRootNode();
            var aAccess = new GovernmentAreaDataAccess();
            var areas = aAccess.All(p => p.DataState < DataStateType.Delete);
            CreateOrgPosTree(root, areas, proxy);
        }

        /// <summary>
        ///     设置子级以构成树
        /// </summary>
        /// <param name="par"></param>
        /// <param name="areas"></param>
        /// <param name="proxy"></param>
        private bool CreateOrgPosTree(EasyUiTreeNode par, List<GovernmentAreaData> areas, RedisProxy proxy)
        {
            bool hase = false;
            var orgs = Access.All(p =>
                p.AreaId == par.ID && p.Type == OrganizationType.Organization && p.DataState < DataStateType.Delete);
            if (orgs.Count > 0)
            {
                par.IsFolder = true;
                foreach (var child in orgs.OrderBy(p => p.LevelIndex))
                {
                    var node = CreateNode(child);
                    par.Children.Add(node);
                    if (CreateOrgPosTree(node, proxy))
                        hase = true;
                }
            }

            var childs = areas.Where(p => p.ParentId == par.ID).ToArray();
            if (childs.Length == 0)
                return hase;
            par.IsFolder = true;
            foreach (var child in childs.OrderBy(p => p.LevelIndex))
            {
                var node = CreateNode(child);
                if (CreateOrgTree(node, areas, proxy))
                {
                    par.Children.Add(node);
                    hase = true;
                }
            }
            proxy.Set("ui:org:PostTree:" + par.ID, par);
            return hase;
        }

        /// <summary>
        ///     设置子级以构成树
        /// </summary>
        /// <param name="par"></param>
        /// <param name="proxy"></param>
        private bool CreateOrgPosTree(EasyUiTreeNode par, RedisProxy proxy)
        {
            bool hase = false;
            var opAccess = new OrganizePositionDataAccess();

            var posts = opAccess.All(p => p.DepartmentId == par.ID && p.DataState < DataStateType.Delete);
            if (posts.Count > 0)
            {
                hase = true;
                foreach (var post in posts)
                {
                    par.Children.Add(CreateNode(post));
                }
            }
            var orgs = Access.All(p => p.ParentId == par.ID && p.Type == OrganizationType.Department && p.DataState < DataStateType.Delete);
            if (orgs.Count > 0)
            {
                par.IsFolder = true;
                foreach (var child in orgs.OrderBy(p => p.LevelIndex))
                {
                    var node = CreateNode(child);
                    par.Children.Add(node);
                    CreateOrgPosTree(node, proxy);
                }
            }
            proxy.Set("ui:org:PostTree:" + par.ID, par);
            return hase;
        }
        #endregion

        #region 生成组织结构树

        /// <summary>
        ///     生成完整的组织结构树(UI相关）
        /// </summary>
        /// <returns></returns>
        private void CreateFullOrgTree(RedisProxy proxy)
        {
            var root = CreateRootNode();
            var aAccess = new GovernmentAreaDataAccess();
            var areas = aAccess.All(p => p.DataState < DataStateType.Delete);
            CreateOrgTree(root, areas, proxy);
        }

        /// <summary>
        ///     设置子级以构成树
        /// </summary>
        /// <param name="par"></param>
        /// <param name="areas"></param>
        /// <param name="proxy"></param>
        private bool CreateOrgTree(EasyUiTreeNode par, List<GovernmentAreaData> areas, RedisProxy proxy)
        {
            bool hase = false;
            var orgs = Access.All(p => p.AreaId == par.ID && p.Type == OrganizationType.Organization && p.DataState < DataStateType.Delete);
            if (orgs.Count > 0)
            {
                par.IsFolder = true;
                hase = true;
                foreach (var child in orgs.OrderBy(p => p.LevelIndex))
                {
                    var node = CreateNode(child);
                    par.Children.Add(node);
                    CreateOrgTree(node, proxy);
                }
            }
            var childs = areas.Where(p => p.ParentId == par.ID).ToArray();
            if (childs.Length != 0)
            {
                par.IsFolder = true;
                foreach (var child in childs.OrderBy(p => p.LevelIndex))
                {
                    var node = CreateNode(child);
                    if (CreateOrgTree(node, areas, proxy))
                    {
                        par.Children.Add(node);
                        hase = true;
                    }
                }
            }
            if (!hase)
                return false;
            proxy.Set("ui:org:OrgTree:" + par.ID, par);
            return true;
        }

        /// <summary>
        ///     设置子级以构成树
        /// </summary>
        /// <param name="par"></param>
        /// <param name="proxy"></param>
        private void CreateOrgTree(EasyUiTreeNode par, RedisProxy proxy)
        {
            var orgs = Access.All(p => p.ParentId == par.ID && p.Type == OrganizationType.Department && p.DataState < DataStateType.Delete);
            if (orgs.Count > 0)
            {
                par.IsFolder = true;
                foreach (var child in orgs.OrderBy(p => p.LevelIndex))
                {
                    var node = CreateNode(child);
                    par.Children.Add(node);
                    CreateOrgTree(node, proxy);
                }
            }
            proxy.Set("ui:org:OrgTree:" + par.ID, par);
        }
        #endregion

        #region 生成区域树

        /// <summary>
        ///     生成完整的组织结构树(UI相关）
        /// </summary>
        /// <returns></returns>
        public EasyUiTreeNode CreateAreaTree(RedisProxy proxy)
        {
            var root = CreateRootNode();
            var aAccess = new GovernmentAreaDataAccess();
            var lists = aAccess.All(p => p.DataState < DataStateType.Delete);
            CreateAreaTree(root, lists, proxy);
            return root;
        }

        /// <summary>
        ///     设置子级以构成树
        /// </summary>
        /// <param name="par"></param>
        /// <param name="lists"></param>
        /// <param name="proxy"></param>
        private void CreateAreaTree(EasyUiTreeNode par, List<GovernmentAreaData> lists, RedisProxy proxy)
        {
            var childs = lists.Where(p => p.ParentId == par.ID).ToArray();
            if (childs.Length != 0)
            {
                par.IsFolder = true;
                foreach (var child in childs.OrderBy(p => p.LevelIndex))
                {
                    var node = CreateNode(child);
                    node.Attributes = "area";
                    CreateAreaTree(node, lists, proxy);
                    par.Children.Add(node);
                }
            }

            proxy.Set("ui:org:areaTree:" + par.ID, par);
        }

        #endregion

        #endregion
    }
}