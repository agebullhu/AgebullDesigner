using System;
using System.Collections.Generic;
using Agebull.Common.Configuration;
using System.Linq;
using Agebull.Common.AppManage;
using Agebull.Common.AppManage.BusinessLogic;
using Agebull.Common.AppManage.DataAccess;
using Agebull.EntityModel.Common;
using Agebull.EntityModel.Redis;
using Agebull.Common.Logging;
using Agebull.Common.OAuth.DataAccess;
using Agebull.Common.OAuth;
using Agebull.Common.OAuth;
using Agebull.Common.OAuth.BusinessLogic;
using Agebull.EntityModel.EasyUI;
using Agebull.Common.Context;

namespace Agebull.Common.UserCenter.BusinessLogic
{
    public class RoleCache : IRoleCache
    {
        /// <summary>
        /// ʵ��
        /// </summary>
        public static RoleCache Instance = new RoleCache();
        #region ʵ������


        private readonly PageItemDataAccess _piAccess = new PageItemDataAccess();
        private readonly RolePowerDataAccess _rpAccess = new RolePowerDataAccess();
        private readonly List<PageItemData> _pages;
        private List<RolePowerData> AllPowers { get; set; }
        private readonly Dictionary<long, List<PageItemData>> _actions;

        public RoleCache()
        {
            var items = _piAccess.All();
            _pages = items.Where(p => p.ItemType <= PageItemType.Page && !p.IsHide).ToList();

            _actions = new Dictionary<long, List<PageItemData>>();
            foreach (var page in items.Where(p => p.ItemType >= PageItemType.Button).GroupBy(p => p.ParentId))
                _actions.Add(page.Key, page.ToList());
        }

        private EasyUiTreeNode CreatePageNode(PageItemData page)
        {
            var node = new EasyUiTreeNode
            {
                ID = page.Id,
                IsOpen = page.ItemType <= PageItemType.Folder,
                Tag = page.Url == "Folder" ? "folder" : "page",
                Text = page.Caption,
                Title = page.Name,
                IsFolder = page.ItemType <= PageItemType.Folder
            };
            if (!string.IsNullOrWhiteSpace(page.Icon))
                node.Icon = page.Icon;
            else
                switch (page.ItemType)
                {
                    case PageItemType.Folder:
                        node.Icon = "icon-tree-folder";
                        break;
                    case PageItemType.Page:
                        node.Icon = "icon-tree-page";
                        break;
                }
            return node;
        }


        /// <summary>
        ///     ���ɽ�ɫ��Ӧ��ҳ�水ť��Ϣ�ļ�
        /// </summary>
        /// <param name="roleId">��ɫID</param>
        /// <param name="pageId">ҳ���ʶ</param>
        /// <param name="types">����</param>
        /// <returns></returns>
        public string ToRolePageKey(long roleId, long pageId = -1, params string[] types)
        {
            if (roleId == 1)
                roleId = 0;
            return types.Length == 0
                ? $"role:{roleId}:{(pageId < 0 ? "*" : pageId.ToString())}"
                : $"role:{roleId}:{(pageId < 0 ? "*" : pageId.ToString())}:{string.Join(":", types)}";
        }

        #endregion

        #region �������

        /// <summary>
        /// ����
        /// </summary>
        public RoleData GetRole(long id)
        {
            using (var proxy = new RedisProxy(RedisProxy.DbSystem))
            {
                return proxy.GetEntity<RoleData>(id) ?? new RoleData { Name = "δ���û�" };
            }
        }


        /// <summary>
        ///     �������н�ɫ��ҳ��Ȩ������
        /// </summary>
        public void Cache()
        {
            DoCache();
        }

        /// <summary>
        ///     �������н�ɫ��ҳ��Ȩ������
        /// </summary>
        public void Cache(long id)
        {
            CacheRolePower(id);
        }

        /// <summary>
        ///     �������н�ɫ��ҳ��Ȩ������
        /// </summary>
        public void DoCache()
        {
            using (var proxy = new RedisProxy(RedisProxy.DbSystem))
            {
                // proxy.FindAndRemoveKey("role:*");
            }

            var access = new RoleDataAccess();
            var roles = access.All();
            using (var proxy = new RedisProxy(RedisProxy.DbSystem))
            {
                proxy.CacheData(roles);
            }

            using (var proxy = new RedisProxy(RedisProxy.DbSystem))
            {
                CreatePowerTree(proxy);
            }

            using (var proxy = new RedisProxy(RedisProxy.DbSystem))
            {
                LoadAllPowers(0, proxy);
                foreach (var items in roles)
                    LoadAllPowers(items.Id, proxy);
            }

            CachePageAuditUser();
            CacheTypeUser();
        }

        /// <summary>
        ///     �����ɫ��ҳ��Ȩ������
        /// </summary>
        /// <param name="roleId">��ɫID</param>
        public void CacheRolePower(long roleId)
        {

            {
                using (var proxy = new RedisProxy(RedisProxy.DbSystem))
                {
                    LoadAllPowers(roleId, proxy);
                }
            }
        }
        /// <summary>
        ///     �����ɫ��ҳ��Ȩ������
        /// </summary>
        /// <param name="roleId">��ɫID</param>
        /// <param name="proxy"></param>
        public void LoadAllPowers(long roleId, RedisProxy proxy)
        {
            AllPowers = roleId <= 1 ? null : _rpAccess.All(p => p.RoleId == roleId);

            CacheRolePower(proxy, roleId);
            CreateMenu(roleId, proxy);
        }

        #endregion

        #region ��ɫ�˵�

        /// <summary>
        ///     �����ɫ�˵�
        /// </summary>
        public List<EasyUiTreeNode> LoadRoleMenu(long roleId)
        {
            using (var proxy = new RedisProxy(RedisProxy.DbSystem))
            {
                return proxy.Get<List<EasyUiTreeNode>>(ToRolePageKey(roleId <= 0 ? 0 : roleId, 0, "menu"));
            }
        }

        /// <summary>
        ///     ���ɽ�ɫ�˵�
        /// </summary>
        private void CreateMenu(long roleId, RedisProxy proxy)
        {
            var root = new EasyUiTreeNode
            {
                ID = 0,
                IsOpen = true,
                Text = ConfigurationManager.AppSettings["ProjectName"],
                IsFolder = true
            };
            foreach (var folder in _pages.Where(p => p.ItemType == PageItemType.Root || p.ParentId == 0 && p.ItemType == PageItemType.Page).OrderBy(p => p.Index))
            {
                if (folder.ItemType == PageItemType.Page)
                {
                    CreateRoleMenu(root, roleId, folder);
                    continue;
                }
                var node = new EasyUiTreeNode
                {
                    IsOpen = true,
                    Text = folder.Caption,
                    Icon = folder.Icon ?? "icon-item",
                    Attributes = "home",
                    Tag = folder.ExtendValue,
                    IsFolder = true
                };
                foreach (var page in _pages.Where(p => p.ParentId == folder.Id && p.ItemType <= PageItemType.Page).OrderBy(p => p.Index))
                {
                    CreateRoleMenu(node, roleId, page);
                }
                if (node.HaseChildren)
                {
                    root.Children.Add(node);
                }
            }
            proxy.Set(ToRolePageKey(roleId, 0, "menu"), root.Children);
        }

        /// <summary>
        ///     ���ɽ�ɫ�˵�
        /// </summary>
        private void CreateRoleMenu(EasyUiTreeNode parentNode, long roleId, PageItemData page)
        {
            if (AllPowers != null && !AllPowers.Any(p => p.RoleId == roleId && p.PageItemId == page.Id))
                return;
            var node = CreatePageNode(page);
            if (string.IsNullOrWhiteSpace(page.Url))
                page.Url = "Folder";
            node.Attributes = page.Url.Split('.')[0];
            node.Tag = page.Url == "Folder" ? "folder" : "page";

            var array = _pages.Where(p => p.ParentId == page.Id && !p.Config.Hide).OrderBy(p => p.Index).ToArray();
            if (array.Length > 0)
                node.IsFolder = true;
            foreach (var ch in array)
            {
                CreateRoleMenu(node, roleId, ch);
            }
            if (page.ItemType == PageItemType.Page || node.HaseChildren)
                parentNode.Children.Add(node);
        }

        #endregion

        #region ҳ��Ȩ�����λ�������

        /// <summary>
        ///     ����ҳ��Ȩ�����λ�������
        /// </summary>
        public EasyUiTreeNode LoadPowerTree()
        {
            using (var proxy = new RedisProxy(RedisProxy.DbSystem))
            {
                return proxy.Get<EasyUiTreeNode>("role:power:tree");
            }
        }

        /// <summary>
        ///     ����ҳ��Ȩ�����λ�������
        /// </summary>
        public void CreatePowerTree()
        {
            using (var proxy = new RedisProxy(RedisProxy.DbSystem))
            {
                CreatePowerTree(proxy);
            }
        }

        /// <summary>
        ///     ����ҳ��Ȩ�����λ�������
        /// </summary>
        public void CreatePowerTree(RedisProxy proxy)
        {
            var root = new EasyUiTreeNode
            {
                ID = 0,
                IsOpen = true,
                Text = ConfigurationManager.AppSettings["ProjectName"],
                Title = ConfigurationManager.AppSettings["ProjectName"],
                IsFolder = true
            };
            foreach (var filter in _pages.Where(p => p.ItemType == PageItemType.Root || p.ParentId == 0 && p.ItemType == PageItemType.Page).OrderBy(p => p.Index))
            {
                if (filter.ItemType == PageItemType.Page)
                {
                    CreatePowerTree(root, filter);
                    continue;
                }
                var node = new EasyUiTreeNode
                {
                    IsOpen = true,
                    Icon = filter.Icon ?? "icon-item",
                    Text = filter.Caption,
                    Title = filter.Caption,
                    Tag = filter.ExtendValue,
                    IsFolder = true
                };
                CreatePowerTree(node, filter);
                root.Children.Add(node);
            }
            proxy.Set("role:power:tree", root);
        }

        /// <summary>
        ///     ����ҳ��Ȩ�����λ�������
        /// </summary>
        private void CreatePowerTree(EasyUiTreeNode parentNode, PageItemData page)
        {
            parentNode.IsFolder = true;
            var node = CreatePageNode(page);
            foreach (var ch in _pages.Where(p => p.ParentId == page.Id).OrderBy(p => p.Index))
            {
                CreatePowerTree(node, ch);
            }
            parentNode.Attributes = null;
            parentNode.Children.Add(node);
            if (page.ItemType != PageItemType.Page)
            {
                parentNode.IsOpen = true;
                return;
            }
            node.IsFolder = true;
            var items = _piAccess.All(p => p.ParentId == page.Id && p.ItemType >= PageItemType.Button);
            foreach (var item in items)
            {
                node.Children.Add(new EasyUiTreeNode
                {
                    ID = item.Id,
                    Text = item.Caption,
                    Title = item.Name,
                    Tag = item.ExtendValue,
                    Memo = item.Memo,
                    IsOpen = true,
                    Icon = item.ItemType == PageItemType.Action ? "icon-cmd" : "icon-cus",
                });
            }
        }

        #endregion

        #region ����ҳ��������û�


        /// <summary>
        ///     ����ҳ��������û�
        /// </summary>
        public void CachePageAuditUser()
        {
            //LogRecorder.BeginStepMonitor("CachePageAuditUser");

            {
                AllPowers = _rpAccess.All();
                using (var proxy = new RedisProxy(RedisProxy.DbSystem))
                {
                    CachePageAuditUser(proxy);
                }
            }
            //LogRecorder.EndStepMonitor();
        }

        readonly List<string> _audits = new List<string>
        {
            "submit","pullback","deny","pass","reaudit","back"
        };

        /// <summary>
        ///     ����ҳ��������û�
        /// </summary>
        /// <param name="proxy"></param>
        private void CachePageAuditUser(RedisProxy proxy)
        {
            foreach (var page in _pages.Where(p => p.ItemType == PageItemType.Page))
            {
                if (!_actions.ContainsKey(page.Id))
                {
                    //Trace.WriteLine("����ҳ��", page.Caption + page.Id);
                    continue;
                }

                if (page.Config.AuditPage > 0)
                {
                    var friend = _pages.FirstOrDefault(p => p.Id == page.Config.AuditPage);
                    if (friend == null)
                    {
                        //Trace.WriteLine($"��������({page.Config.AuditPage})", page.Caption + page.Id);
                    }
                    else
                    {
                        proxy.SetValue($"audit:page:link:{page.Id}", page.Config.AuditPage);
                        //Trace.WriteLine($"���ӵ�=>{friend.Caption}", page.Caption + page.Id);
                    }
                    continue;
                }

                var items = _actions[page.Id].Where(p => _audits.Contains(p.ExtendValue)).ToArray();
                if (items.Length == 0)
                {
                    //Trace.WriteLine("û����˲���", page.Caption + page.Id);
                    continue;
                }
                var itemIds = items.Select(p => p.Id).ToList();
                var pwoers = AllPowers.Where(p => itemIds.Contains(p.PageItemId)).ToArray();
                if (pwoers.Length == 0)
                {
                    //Trace.WriteLine("û������û�", page.Caption + page.Id);
                    continue;
                }
                var roles = pwoers.Select(p => p.RoleId).Distinct().ToArray();
                var ponAccess = new PositionPersonnelDataAccess();
                var prAccess = new OrganizePositionDataAccess();

                Dictionary<long, PositionPersonnelData> personnels = new Dictionary<long, PositionPersonnelData>();
                foreach (var role in roles)
                {
                    var posIds = prAccess.LoadValues(p => p.Id, Convert.ToInt64, p => p.RoleId == role);
                    var pons = ponAccess.All(p => posIds.Contains(p.OrganizePositionId) && p.AuditState == AuditStateType.Pass);
                    foreach (var p in pons)
                    {
                        if (personnels.ContainsKey(p.Id))
                            continue;
                        personnels.Add(p.Id, p);
                    }

                    //System.Diagnostics.Trace.WriteLine(role, "CachePageAuditUser");
                    foreach (var pid in posIds)
                    {
                        pons = ponAccess.All(p => p.OrganizePositionId == pid && p.AuditState == AuditStateType.Pass);
                        foreach (var p in pons)
                        {
                            if (personnels.ContainsKey(p.Id))
                                continue;
                            personnels.Add(p.Id, p);
                        }
                    }
                }
                AuditNode(proxy, page.Id, personnels.Values.ToList());
            }
        }

        void AuditNode(RedisProxy proxy, long pageId, List<PositionPersonnelData> users)
        {
            //var page = PageItemBusinessLogic.GetPageItem(pageId);
            //if (!page.LevelAudit)
            {
                //var orgs = OrganizationBusinessLogic.LoadAreaTreeForUi(0);
                //AuditNode(orgs[0], users);
                //proxy.Set($"audit:page:users:nodes:{pageId}:0", orgs);
                proxy.Set($"audit:page:users:ids:{pageId}:0", users.Select(p => p.UserId).LinkToString(","));
                return;
            }
            foreach (var levels in users.GroupBy(p => p.OrgLevel))
            {
                var orgs = OrganizationBusinessLogic.LoadAreaTreeForUi(0);
                AuditNode(orgs[0], levels.ToList());
                if (orgs[0].HaseChildren)
                {
                    foreach (var ch in orgs[0].Children.Where(p => p.Tag != "personnel").ToArray())
                    {
                        if (!ch.HaseChildren)
                            orgs[0].Children.Remove(ch);
                    }
                }
                proxy.Set($"audit:page:users:nodes:{pageId}:{levels.Key}", orgs);
                proxy.Set($"audit:page:users:ids:{pageId}:{levels.Key}", levels.Select(p => p.UserId).LinkToString(","));
            }
        }

        void AuditNode(EasyUiTreeNode orgNode, List<PositionPersonnelData> users)
        {
            if (orgNode.HaseChildren)
            {
                foreach (var ch in orgNode.Children)
                {
                    AuditNode(ch, users);
                }
            }
            var array = users.Where(p => p.DepartmentId == orgNode.ID).ToArray();
            if (array.Length == 0)
            {
                orgNode.IsFolder = false;
                return;
            }
            orgNode.IsFolder = true;
            foreach (var personnel in array)
            {
                orgNode.Children.Add(new EasyUiTreeNode
                {
                    ID = personnel.UserId,
                    IsOpen = true,
                    Text = $"{personnel.RealName}({personnel.Position})",
                    Tag = "personnel"
                });
            }
            orgNode.ID = 0 - orgNode.ID;
        }
        /// <summary>
        /// ҳ���Ӧ������û���
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public List<EasyUiTreeNode> GetPageAuditUsersTree(int pid)
        {
            using (var proxy = new RedisProxy(RedisProxy.DbSystem))
            {
                int friend = proxy.GetValue<int>($"audit:page:link:{pid}");
                if (friend > 0)
                    pid = friend;

                var page = PageItemBusinessLogic.GetPageItem(pid);
                if (page == null)
                    return new List<EasyUiTreeNode>();
                int level=0;
                //if (!page.LevelAudit)
                //    level = 0;//BusinessContext.Current.LoginUser.DepartmentId;
                //else
                //{
                //    level = GlobalContext.Current.User.DepartmentLevel;
                //    if (level < 1)
                //        level = 1;
                //    else if (level > 2)
                //        level = 2;
                //}
                var nodes = proxy.Get<List<EasyUiTreeNode>>($"audit:page:users:nodes:{pid}:{level}");
                if (level <= 1)
                    return nodes;
                if (nodes != null && nodes.Count > 0)
                {
                    nodes[0].IsFolder = true;
                    var arrs = nodes[0].Children.ToArray();
                    nodes.Clear();
                    foreach (var arr in arrs)
                    {
                        if (arr.ID == 0 - GlobalContext.Current.User.OrganizationId)
                        {
                            nodes.Add(arr);
                        }
                    }
                    if (nodes.Count == 0)
                    {
                        nodes.AddRange(arrs);
                    }
                }
                var root = proxy.Get<List<EasyUiTreeNode>>($"audit:page:users:nodes:{pid}:1");

                if (root == null || root.Count == 0 || level <= 1)
                {
                    return nodes;
                }
                if (nodes != null && nodes.Count > 0)
                    root[0].Children.AddRange(nodes);
                return root;
            }
        }

        #endregion

        #region ����ҳ��ı༭�û�


        /// <summary>
        ///     �������Ͷ�Ӧ��Ȩ���û�
        /// </summary>
        public void CacheTypeUser()
        {
            LogRecorder.BeginStepMonitor("CacheEditUser");
            AllPowers = _rpAccess.All();
            using (var proxy = new RedisProxy(RedisProxy.DbAuthority))
            {
                //CacheUser(proxy, "edit", _edits);
                //CacheUser(proxy, "audit", _audits);
            }

            LogRecorder.EndStepMonitor();
        }

        readonly List<string> _edits = new List<string>
        {
            "deny","pass","back"
        };

        /// <summary>
        ///     ����ҳ��������û�
        /// </summary>
        private void CacheUser(RedisProxy proxy, string name, List<string> actions)
        {
            var ponAccess = new PositionPersonnelDataAccess();
            Dictionary<string, List<long>> types = new Dictionary<string, List<long>>();
            foreach (var page in _pages.Where(p => p.ItemType == PageItemType.Page))
            {
                if (!_actions.ContainsKey(page.Id))
                    continue;
                var items = _actions[page.Id].Where(p => actions.Contains(p.ExtendValue)).ToArray();
                if (items.Length == 0)
                    continue;
                var itemIds = items.Select(p => p.Id).ToList();
                var pwoers = AllPowers.Where(p => itemIds.Contains(p.PageItemId)).ToArray();
                if (pwoers.Length == 0)
                    continue;
                var roles = pwoers.Select(p => p.RoleId).Distinct().ToArray();

                List<long> personnels;
                if (types.ContainsKey(page.Config.SystemType))
                    personnels = types[page.Config.SystemType];
                else
                    types.Add(page.Config.SystemType, personnels = new List<long>());
                var prAccess = new OrganizePositionDataAccess();
                foreach (var role in roles)
                {
                    var pRoleIds = prAccess.LoadValues(p => p.Id, Convert.ToInt64, p => p.RoleId == role);
                    var pons = ponAccess.LoadValues(p => p.UserId, Convert.ToInt64, p => pRoleIds.Contains(p.OrganizePositionId) && p.AuditState == AuditStateType.Pass);
                    personnels.AddRange(pons);
                    foreach (var pid in pRoleIds)
                    {
                        pons = ponAccess.LoadValues(p => p.UserId, Convert.ToInt64, p => p.OrganizePositionId == pid && p.AuditState == AuditStateType.Pass);
                        personnels.AddRange(pons);
                    }
                }
            }
            foreach (var type in types)
            {
                var users = type.Value.Distinct().ToArray();
                var keys = $"users:{name}:{type.Key}";
                proxy.Set(keys, users);
                //System.Diagnostics.Trace.WriteLine(users.LinkToString(","), keys);
            }
        }

        /// <summary>
        ///     �õ�����ҳ��ı༭�û�
        /// </summary>
        public List<int> GetEditUsers(Type type)
        {
            using (var proxy = new RedisProxy(RedisProxy.DbAuthority))
            {
                return proxy.Get<List<int>>($"users:edit:{type.FullName}");
            }
        }

        /// <summary>
        ///     �õ�����ҳ��������û�
        /// </summary>
        public List<int> GetAuditUsers(Type type)
        {
            using (var proxy = new RedisProxy(RedisProxy.DbAuthority))
            {
                return proxy.Get<List<int>>($"users:audit:{type.FullName}");
            }
        }
        #endregion

        #region Ȩ�޻���

        /// <summary>
        ///     �����ɫ��ҳ��Ȩ������
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="roleId"></param>
        private void CacheRolePower(RedisProxy proxy, long roleId)
        {
            foreach (var page in _pages)
            {
                if (AllPowers == null || roleId <= 1)
                {
                    proxy.Set(ToRolePageKey(roleId, page.Id, "page"), new RolePowerData
                    {
                        RoleId = roleId,
                        PageItemId = page.Id,
                        Power = RolePowerType.Allow
                    });
                }
                else
                {
                    var power = AllPowers.FirstOrDefault(p => p.RoleId == roleId && p.PageItemId == page.Id);
                    if (power == null)
                        continue;
                    proxy.Set(ToRolePageKey(roleId, page.Id, "page"), power);
                }
                CacheRoleAction(roleId, page, proxy);
            }
        }
        /// <summary>
        /// �����ɫ��ҳ��Ȩ������
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="page"></param>
        /// <param name="proxy"></param>
        private void CacheRoleAction(long roleId, PageItemData page, RedisProxy proxy)
        {
            if (!_actions.ContainsKey(page.Id))
                return;
            var dictionary = new Dictionary<string, long>(StringComparer.OrdinalIgnoreCase);

            var actions = roleId <= 1
                ? _actions[page.Id].ToArray()
                : _actions[page.Id].Where(p => AllPowers.Any(a => a.PageItemId == p.Id)).ToArray();
            foreach (var bp in actions)
            {
                if (string.IsNullOrEmpty(bp.ExtendValue) || dictionary.ContainsKey(bp.ExtendValue))
                    continue;
                dictionary.Add(bp.ExtendValue, bp.Id);
            }

            if (page.AuditPage > 0 && _actions.ContainsKey(page.AuditPage))
            {
                var friendsItemDatas = roleId <= 1
                    ? _actions[page.AuditPage].ToArray()
                    : _actions[page.AuditPage].Where(p => AllPowers.Any(a => a.PageItemId == p.Id)).ToArray();
                foreach (var bp in friendsItemDatas)
                {
                    if (string.IsNullOrEmpty(bp.ExtendValue) || dictionary.ContainsKey(bp.ExtendValue))
                        continue;
                    dictionary.Add(bp.ExtendValue, bp.Id);
                }
            }
            AddActionSynonym(dictionary, "list", "details");
            AddActionSynonym(dictionary, "update", "details");
            foreach (var action in dictionary.Keys)
                proxy.SetValue(ToRolePageKey(roleId, page.Id, "action", action), 1);
            proxy.Set(ToRolePageKey(roleId, page.Id, "btns"), actions.Select(p => p.Name));
        }


        /// <summary>
        /// ͬ��ʼ���
        /// </summary>
        /// <param name="actions"></param>
        /// <param name="action"></param>
        /// <param name="synonyms"></param>
        private void AddActionSynonym(Dictionary<string, long> actions, string action, params string[] synonyms)
        {
            if (!actions.TryGetValue(action, out var id))
                return;
            foreach (var synonym in synonyms)
                if (!actions.ContainsKey(synonym))
                    actions.Add(synonym, id);
        }

        #endregion

        #region Ȩ��У��

        /// <summary>
        ///     �����û��ĵ�ҳ��ɫȨ��
        /// </summary>
        /// <param name="roleId">��ɫID</param>
        /// <param name="pageId">ҳ��ID</param>
        /// <returns></returns>
        public IRolePower LoadPagePower(long roleId, long pageId)
        {
            if (roleId == 1)
                roleId = 0;
            using (var proxy = new RedisProxy(RedisProxy.DbSystem))
            {
                return proxy.Get<RolePowerData>(ToRolePageKey(roleId, pageId, "page"));
            }
        }

        /// <summary>
        ///     �����û��Ľ�ɫȨ��
        /// </summary>
        /// <param name="roleId">��ɫID</param>
        /// <returns></returns>
        public List<IRolePower> LoadUserPowers(long roleId)
        {
            using (var proxy = new RedisProxy(RedisProxy.DbSystem))
            {
                var key = roleId == 1 || GlobalContext.Current.IsSystemMode
                    ? ToRolePageKey(roleId, 0, "page")
                    : ToRolePageKey(roleId, -1, "page");
                var vl = proxy.GetAll<RolePowerData>(key);
                return vl == null || vl.Count == 0 ? new List<IRolePower>() : vl.ToList(p => (IRolePower)p);
            }
        }

        /// <summary>
        ///     ����ҳ������İ�ť����
        /// </summary>
        /// <param name="roleId">��ɫID</param>
        /// <param name="pageId">ҳ��ID</param>
        /// <param name="action">����</param>
        /// <returns>�Ƿ��ִ��ҳ�涯��</returns>
        public bool CanDoAction(long roleId, long pageId, string action)
        {
            if (GlobalContext.Current.IsSystemMode || roleId == 1 || roleId == int.MaxValue)
                return true;
            using (var proxy = new RedisProxy(RedisProxy.DbSystem))
            {
                return proxy.GetValue<long>(ToRolePageKey(roleId, pageId, "action", action)) == 1;
            }
        }

        /// <summary>
        ///     ����ҳ������İ�ť����
        /// </summary>
        /// <param name="roleId">��ɫID</param>
        /// <param name="pageId">ҳ��ID</param>
        /// <returns>��ť���ü���</returns>
        public List<string> LoadButtons(long roleId, long pageId)
        {
            using (var proxy = new RedisProxy(RedisProxy.DbSystem))
            {
                if (GlobalContext.Current.IsSystemMode || roleId == 1 || roleId == int.MaxValue)
                    roleId = 0;
                return proxy.Get<List<string>>(ToRolePageKey(roleId, pageId, "btns"));
            }
        }

        #endregion
    }
}