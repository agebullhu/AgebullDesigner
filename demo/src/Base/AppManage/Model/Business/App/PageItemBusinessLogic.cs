using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using Gboxt.Common.DataModel.MySql;


using Gboxt.Common.DataModel;
using Agebull.Common.DataModel;
using Agebull.Common.DataModel.BusinessLogic;
using Agebull.Common.DataModel.Redis;
using Agebull.Common.AppManage.DataAccess;
using Agebull.Common.Configuration;
using Agebull.Common.DataModel.WebUI;
using Agebull.Common.Logging;
using Agebull.Common.Rpc;
using Newtonsoft.Json;

namespace Agebull.Common.AppManage.BusinessLogic
{
    /// <summary>
    /// 页面节点
    /// </summary>
    public sealed partial class PageItemBusinessLogic : UiBusinessLogicBase<PageItemData, PageItemDataAccess, AppManageDb>
    {

        #region 动作保存

        /// <summary>
        /// 保存页面的动作
        /// </summary>
        public static void SavePageAction(long pid, string name, string title, string action, string type)
        {
            Save(pid, name, title, action, type);
        }
        static void Save(long pid, string name, string title, string action, string type)
        {
            var access = new PageItemDataAccess();
            if (!access.ExistPrimaryKey(pid))
                return;
            var item = access.First(p => p.ParentId == pid && p.ItemType >= PageItemType.Button && p.Name == name);
            if (item == null)
            {
                item = new PageItemData
                {
                    ParentId = pid,
                    ItemType = type == "action" ? PageItemType.Action : PageItemType.Button,
                    Name = name,
                    Caption = title,
                    ExtendValue = action,
                    Config = { SystemType = type }
                };
                access.Insert(item);
            }
            else
            {
                item.Caption = title;
                item.ExtendValue = action;
                item.Config.SystemType = type;
                item.ItemType = type == "action" ? PageItemType.Action : PageItemType.Button;
                access.Update(item);
            }
        }
        #endregion
        #region 保存逻辑
        /// <summary>
        ///     保存前的操作
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="isAdd">是否为新增</param>
        /// <returns>如果为否将阻止后续操作</returns>
        protected override bool OnSaving(PageItemData data, bool isAdd)
        {
            //var folder = Access.FirstOrDefault(p => p.ItemType == PageItemType.Folder && (p.Caption == data.Folder || p.Name == data.Folder));
            //if (folder == null)
            //{
            //    Access.Insert(folder = new PageItemData
            //    {
            //        Name = data.Folder,
            //        Caption = data.Folder,
            //        Memo = data.Folder,
            //        ParentId = 0,
            //        Folder = "Root",
            //        Url = "Folder",
            //        ItemType = PageItemType.Folder
            //    });
            //}
            //data.ParentId = folder.Id;
            //data.ItemType = PageItemType.Page;
            return true;
        }

        /// <summary>
        ///     保存完成后的操作
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="isAdd">是否为新增</param>
        /// <returns>如果为否将阻止后续操作</returns>
        protected override bool OnSaved(PageItemData data, bool isAdd)
        {
            using (MySqlDataBaseScope.CreateScope(Access.DataBase))
            {
                Cache(data, Access);
            }

            return true;
        }


        #endregion

        #region 级联删除

        /// <summary>删除对象前置处理</summary>
        protected override bool PrepareDelete(long id)
        {
            DeleteChild(id);
            return base.PrepareDelete(id);
        }

        void DeleteChild(long id)
        {
            var childs = Access.LoadValues(p => p.Id,Convert.ToInt64, p => p.ParentId == id);
            foreach (var ch in childs.Distinct())
            {
                if (ch <= 0 || ch == id)
                    return;
                DeleteChild(ch);
                Access.DeletePrimaryKey(ch);
            }
        }

        #endregion

        #region 检查类型绑定

        /// <summary>
        /// 检查类型绑定
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="types"></param>
        public bool BindType(long pid, List<Type> types)
        {
            using (MySqlDataBaseScope.CreateScope(Access.DataBase))
            {
                FindAndBindType(pid, types);
            }

            return true;
        }

        /// <summary>
        /// 检查类型绑定
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="types"></param>
        public void FindAndBindType(long pid, List<Type> types)
        {
            if (pid < 0)
            {
                var ids = Access.LoadValues(p => p.Id, Convert.ToInt64, p => p.ItemType == PageItemType.Page);
                foreach (var id in ids)
                    FindAndBindType(id, types);
                return;
            }
            var children = Access.LoadValues(p => p.Id, Convert.ToInt64, p => p.ParentId == pid && p.ItemType <= PageItemType.Page);
            foreach (var id in children)
                FindAndBindType(id, types);

            BindPageType(pid, types);
        }

        private void BindPageType(long pid, List<Type> types)
        {
            var page = Access.LoadByPrimaryKey(pid);
            if (page == null || page.ItemType != PageItemType.Page)
                return;
            var type = FindType(types, page.Config.SystemType);/*, page.Name, page.Name + "Data",
                Path.GetFileNameWithoutExtension(Path.GetDirectoryName(page.Url ?? "__")) + "Data"*/
            if (type == null)
            {
                page.Config.SystemType = null;
            }
            else
            {
                page.DataState = type.IsSupperInterface(typeof(IStateData));
                page.Audit = type.IsSupperInterface(typeof(IAuditData));
                if (type.GetInterfaces().Any(p => p.Name == "ILevelAuditData"))
                    page.Config.LevelAudit = true;
                page.Config.SystemType = type.FullName;
                if (page.ExtendValue == "hide")
                {
                    page.Config.Hide = true;
                    page.ExtendValue = null;
                }
                Access.SetValue(p => p.Name, type.Name, page.Id);
            }
            Access.SetValue(p => p.Json, JsonConvert.SerializeObject(page.Config), page.Id);
        }

        public void BindPageType(PageConfig config)
        {
            var page = Access.LoadByPrimaryKey(config.PageId);
            if (page == null || page.ItemType != PageItemType.Page)
                return;

            Access.SetValue(p => p.Json, JsonConvert.SerializeObject(page.Config), page.Id);
        }
        private static Type FindType(List<Type> types, params string[] names)
        {
            foreach (var name in names.Where(p => !string.IsNullOrWhiteSpace(p)))
            {
                var type = types.FirstOrDefault(p => string.Equals(p.FullName, name, StringComparison.OrdinalIgnoreCase) ||
                                                     string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
                if (type != null)
                    return type;
            }
            return null;
        }

        #endregion
        #region 检查标准按钮
        /// <summary>
        /// 检查标准的增删改查按钮
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="type">类型</param>
        public bool CheckNormalButtons(long pid, string type)
        {
            using (MySqlDataBaseScope.CreateScope(Access.DataBase))
            {
                DeleteButtonItem(p => p.ExtendValue == "list" || p.ExtendValue == "physical_delete");
                if (pid < 0)
                {
                    var ids = Access.LoadValues(p => p.Id, Convert.ToInt64, p => p.ItemType == PageItemType.Page);
                    foreach (var id in ids)
                        NormalButtons(id);
                }
                else
                {
                    CheckNormalButtonsInner(pid);
                }
            }

            return true;
        }

        /// <summary>
        /// 检查标准的增删改查按钮
        /// </summary>
        /// <param name="pid"></param>
        void CheckNormalButtonsInner(long pid)
        {
            var childs = Access.LoadValues(p => p.Id, Convert.ToInt64, p => p.ParentId == pid && p.ItemType <= PageItemType.Page);
            foreach (var id in childs)
            {
                CheckNormalButtonsInner(id);
            }
            NormalButtons(pid);
        }

        /// <summary>
        /// 检查标准的增删改查按钮
        /// </summary>
        /// <param name="pid"></param>
        void NormalButtons(long pid)
        {
            var page = Access.LoadByPrimaryKey(pid);
            if (page == null || page.ItemType != PageItemType.Page)
            {
                return;
            }
            //page.Config.Edit = true;
            if (page.Config.Edit)
            {
                SaveButtonItem(pid, "#btnAdd", "新增", "addnew");

                SaveButtonItem(pid, "#btnEdit", "详细", "details");
                if (Access.Any(p => p.ParentId == pid && p.ExtendValue == "details"))
                {
                    Save(pid, "#btnSave", "修改", "update", "extend");
                }

                SaveButtonItem(pid, "#btnValidate", "数据校验", "validate");
                SaveButtonItem(pid, "#btnDelete", "删除", "delete");
                SaveButtonItem(pid, "#btnExport", "导出", "export");
            }
            //page.Config.DataState = false;
            if (page.Config.DataState)
            {
                SaveButtonItem(pid, "#btnDisable", "禁用", "disable");
                SaveButtonItem(pid, "#btnEnable", "启用", "enable");
                SaveButtonItem(pid, "#btnDiscard", "废弃", "discard");
                SaveButtonItem(pid, "#btnReset", "还原", "reset");
                SaveButtonItem(pid, "#btnLock", "锁定", "lock");
            }
            else
            {
                DeleteButtonItem(p => p.ParentId == pid && p.ExtendValue == "disable");
                DeleteButtonItem(p => p.ParentId == pid && p.ExtendValue == "enable");
                DeleteButtonItem(p => p.ParentId == pid && p.ExtendValue == "reset");
                DeleteButtonItem(p => p.ParentId == pid && p.ExtendValue == "discard");
                DeleteButtonItem(p => p.ParentId == pid && p.ExtendValue == "lock");
                DeleteButtonItem(p => p.ParentId == pid && p.ExtendValue == "physical_delete");
            }

            if (page.Config.Audit)
            {
                SaveButtonItem(pid, "#btnPullback", "拉回", "pullback");
                SaveButtonItem(pid, "#btnSubmit", "提交审核", "submit");
                SaveButtonItem(pid, "#btnAuditPass", "审核(通过)", "pass");
                SaveButtonItem(pid, "#btnAuditDeny", "审核(否决)", "deny");
                SaveButtonItem(pid, "#btnAuditBack", "退回重做", "back");
                SaveButtonItem(pid, "#btnReAudit", "反审核", "reaudit");
            }
            else
            {
                DeleteButtonItem(p => p.ParentId == pid && p.ExtendValue == "pullback");
                DeleteButtonItem(p => p.ParentId == pid && p.ExtendValue == "submit");
                DeleteButtonItem(p => p.ParentId == pid && p.ExtendValue == "pass");
                DeleteButtonItem(p => p.ParentId == pid && p.ExtendValue == "deny");
                DeleteButtonItem(p => p.ParentId == pid && p.ExtendValue == "back");
                DeleteButtonItem(p => p.ParentId == pid && p.ExtendValue == "reaudit");
            }
            //Update(page);
        }

        private void DeleteButtonItem(Expression<Func<PageItemData, bool>> lambda)
        {
            foreach (var id in Access.LoadValues(p => p.Id, Convert.ToInt64, lambda))
                Access.PhysicalDelete(id);
        }


        private void SaveButtonItem(long pid, string element, string caption, string action, bool isButton = true)
        {
            var b = Access.All(p => p.ParentId == pid && p.ExtendValue == action);
            if (b.Count != 0)
            {
                for (int i = 1; i < b.Count; i++)
                    Access.PhysicalDelete(b[i].Id);
                b[0].Caption = caption;
                b[0].Name = isButton ? element : action;
                b[0].ExtendValue = action;
                b[0].ItemType = isButton ? PageItemType.Button : PageItemType.Action;
                b[0].Memo = caption;
                Access.Update(b[0]);
            }
            else
            {
                Access.Insert(new PageItemData
                {
                    ParentId = pid,
                    ItemType = isButton ? PageItemType.Button : PageItemType.Action,
                    Name = isButton ? element : action,
                    Caption = caption,
                    Memo = caption,
                    ExtendValue = action
                });
            }
        }

        #endregion
        #region 结构树

        /// <summary>
        ///     载入完整的结构树(UI相关）
        /// </summary>
        /// <returns></returns>
        public List<EasyUiTreeNode> LoadTreeForUi(long pid)
        {
            if (pid <= 0)
                return LoadTreeRootForUi();
            var root = new EasyUiTreeNode
            {
                IsFolder = true
            };
            LoadTreeForUi(pid, root);
            return root.Children;
        }

        /// <summary>
        ///     载入完整的结构树(UI相关）
        /// </summary>
        /// <returns></returns>
        public List<EasyUiTreeNode> LoadTreeRootForUi()
        {
            var rootNode = new EasyUiTreeNode
            {
                ID = -1,
                IsOpen = true,
                Icon = "icon-tree-folder",
                Attributes = "root",
                Text = "系统页面",
                IsFolder = true
            };
            var roots = Access.All(p => p.ItemType == PageItemType.Root, p => p.Index, false);
            foreach (var root in roots)
            {
                var node = new EasyUiTreeNode
                {
                    ID = root.Id,
                    IsOpen = true,
                    Tag = root.Name,
                    Icon = "icon-tree-folder",
                    Attributes = root.ItemType.ToString(),
                    Text = root.Caption,
                    IsFolder = true
                };
                LoadTreeForUi(root.Id, node);
                rootNode.Children.Add(node);
            }
            return new List<EasyUiTreeNode> { rootNode };
        }

        /// <summary>
        ///     载入完整的结构树(UI相关）
        /// </summary>
        /// <returns></returns>
        public void LoadTreeForUi(long pid, EasyUiTreeNode parent)
        {
            var childs = Access.All(p => p.ParentId == pid && p.ItemType <= PageItemType.Page);
            if (childs.Count == 0)
                return;
            foreach (var child in childs.OrderBy(p => p.Index))
            {
                var node = new EasyUiTreeNode
                {
                    ID = child.Id,
                    IsOpen = true,
                    Tag = child.Name,
                    Attributes = child.ItemType.ToString(),
                    Text = child.Caption,
                    IsFolder = true
                };
                switch (child.ItemType)
                {
                    case PageItemType.Folder:
                        node.Icon = "icon-tree-folder";
                        node.IsOpen = false;
                        break;
                    case PageItemType.Page:
                        node.Icon = "icon-tree-page";
                        break;
                    case PageItemType.Action:
                        node.Icon = "icon-tree-action";
                        break;
                    case PageItemType.Button:
                        node.Icon = "icon-tree-button";
                        break;
                }
                LoadTreeForUi(child.Id, node);
                parent.Children.Add(node);
            }
        }
        #endregion

        #region 静态化


        /// <summary>
        ///     静态化
        /// </summary>
        /// <returns></returns>
        public void ToHtml(long pid, string host, bool all)
        {
            if (pid < 0)
                pid = 0;
            host = host.TrimEnd('/') + "/";
            ToHtmlPage(pid, host, all);
        }

        /// <summary>
        ///     载入完整的结构树(UI相关）
        /// </summary>
        /// <returns></returns>
        public void ToHtmlPage(long pid, string host, bool all)
        {
            var childs = Access.All(p => p.ParentId == pid && p.ItemType <= PageItemType.Page);
            if (childs.Count == 0)
                return;
            foreach (var child in childs.OrderBy(p => p.Index))
            {
                if (child.ItemType == PageItemType.Page)
                {
                    ToHtmlPage(child, host, all);
                }
                else
                {
                    ToHtmlPage(child.Id, host, all);
                }
            }
        }

        private string root = ConfigurationManager.AppSettings["StaticFolder"];
        /// <summary>
        ///     载入完整的结构树(UI相关）
        /// </summary>
        /// <returns></returns>
        public void ToHtmlPage(PageItemData item, string host, bool all)
        {
            if (string.IsNullOrWhiteSpace(item.Url))
                return;
            host = host.Trim('/')+ '/';
            var folders = item.Url.Split(new char[] { '\\', '/' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            folders.RemoveAt(folders.Count - 1);
            var path = folders.Where(p => !p.Contains('.')).LinkToString("/");
            var folder = IOHelper.CheckPath(root, folders.ToArray());
            Save("http://" + host + path + "/index.aspx", Path.Combine(folder, "index.htm"));
            Save("http://" + host + path + "/script.js", Path.Combine(folder, "script.js"));
            Save("http://" + host + path + "/form.htm", Path.Combine(folder, "form.htm"));
            Access.SetValue(p => p.Url, "/" + path + "/index", item.Id);
        }
        /// <summary>
        ///     载入完整的结构树(UI相关）
        /// </summary>
        /// <returns></returns>
        public void Save(string url, string file)
        {
            try
            {
                if (File.Exists(file))
                    return;
                using (var fs = File.Create(file))
                {
                    var webClient = new WebClient();
                    using (var stream = webClient.OpenRead(url))
                    {
                        int len = 1;
                        do
                        {
                            var buf = new byte[512];
                            len = stream.Read(buf, 0, 512);
                            if (len > 0)
                                fs.Write(buf, 0, len);
                        } while (len > 0);
                    }
                }
            }
            catch (Exception e)
            {
                LogRecorder.Exception(e, url);
            }
        }
        #endregion
        #region 缓存

        /// <summary>
        /// 生成通过URL想找页面信息的键
        /// </summary>
        /// <param name="url">类型</param>
        /// <returns></returns>
        static string ToUrlKey(string url)
        {
            return $"page:{url.Split('?')[0].ToLower()}";
        }


        /// <summary>
        /// 生成通过URL想找页面信息的键
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static string ToPageKey(object type = null)
        {
            return type == null ? "page:*" : $"page:{type.ToString().ToLower()}";
        }
        /// <summary>
        ///     载入页面关联的按钮配置
        /// </summary>
        /// <param name="pageId">页面,不能为空</param>
        /// <returns>按钮配置集合</returns>
        public static List<string> LoadPageButtons(long pageId)
        {
            using (var proxy = new RedisProxy(RedisProxy.DbSystem))
            {
                return proxy.Get<List<string>>(ToPageKey("btns"));
            }
        }

        /// <summary>
        /// 通过ID取得页面信息
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public static PageItemData GetPageItem(long pid)
        {
            using (var proxy = new RedisProxy(RedisProxy.DbSystem))
            {
                return proxy.Get<PageItemData>(ToPageKey(pid)) ?? new PageItemData();
            }
        }

        /// <summary>
        /// 通过URL取得页面信息
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static PageItemData GetPageItem(string url)
        {
            if (string.Equals(url, "/Index.aspx", StringComparison.OrdinalIgnoreCase))
                return new PageItemData();
            PageItemData page;
            using (var proxy = new RedisProxy(RedisProxy.DbSystem))
            {
                page = proxy.Get<PageItemData>(ToUrlKey(url));
            }
            return page ?? new PageItemData();
        }

        /// <summary>
        /// 缓存页面数据
        /// </summary>
        public static void Cache()
        {

            {
                PageItemDataAccess access = new PageItemDataAccess();
                var items = access.All(p => p.ItemType == PageItemType.Page);
                foreach (var item in items)
                {
                    Cache(item, access);
                }
            }
        }

        /// <summary>
        /// 缓存页面数据
        /// </summary>
        static void Cache(PageItemData item, PageItemDataAccess piAccess)
        {
            if (string.IsNullOrWhiteSpace(item.Url))
            {
                return;
            }
            PageItemData master;
            if (item.MasterPage > 0)
            {
                PageItemDataAccess access = new PageItemDataAccess();
                master = access.LoadByPrimaryKey(item.MasterPage) ?? item;
            }
            else
            {
                master = item;
            }
            using (var proxy = new RedisProxy(RedisProxy.DbSystem))
            {
                proxy.Set(ToUrlKey(item.Url), master);
                proxy.Set(ToUrlKey(item.Name), master);
                proxy.Set(ToPageKey(item.Id), master);
                proxy.Set(DataKeyBuilder.DataKey<PageItemData>(item.Id), master);

                proxy.Set(ToPageKey("btns"), piAccess.All(p => p.ParentId == master.Id).Select(p => p.Name).ToList());

                if (master == item && !string.IsNullOrWhiteSpace(master.Config.SystemType))
                {
                    proxy.SetValue($"page:{item.Config.SystemType}", master.Id);
                }
            }
        }
        /// <summary>
        /// 取一个类型对应的页面,如果找不到,返回默认值
        /// </summary>
        public static PageItemData GetPageByDataType(Type type)
        {
            using (var proxy = new RedisProxy(RedisProxy.DbSystem))
            {
                var id = proxy.GetValue<int>(ToPageKey(type.FullName));
                if (id == 0)
                    return new PageItemData();
                return proxy.Get<PageItemData>(ToPageKey(id)) ?? new PageItemData();
            }
        }

        /// <summary>
        /// 清除页面相关缓存
        /// </summary>
        public static void ClearCache()
        {
            using (var proxy = new RedisProxy(RedisProxy.DbSystem))
            {
                proxy.FindAndRemoveKey(ToPageKey());
            }
        }
        #endregion

        #region 修改上级

        /// <summary>
        /// 修改上级
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="parent"></param>
        public bool SetParent(IEnumerable<long> ids, string parent)
        {
            var folder = Access.FirstOrDefault(p => p.ItemType <= PageItemType.Folder && (p.Name == parent || p.Caption == parent));
            if (folder == null)
            {
                GlobalContext.Current.LastMessage = "不存在的上级";
                return false;
            }

            LoopIds(ids, p =>
            {
                Access.SetValue(f => f.ParentId, folder.Id, p);
                return true;
            });
            return true;
        }

        #endregion
    }
}