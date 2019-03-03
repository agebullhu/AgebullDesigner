/*design by:agebull designer date:2019/3/2 23:07:34*/
#region
using System;
using System.Collections.Generic;
using System.Linq;
using Agebull.Common.Configuration;
using Gboxt.Common.DataModel;
using Agebull.Common.DataModel.BusinessLogic;
using Agebull.Common.DataModel.WebUI;
using Agebull.Common.Logging;
using Agebull.Common.OAuth.DataAccess;
#endregion

namespace Agebull.Common.OAuth.BusinessLogic
{
    /// <summary>
    /// 行政区域
    /// </summary>
    public sealed partial class GovernmentAreaBusinessLogic : BusinessLogicByStateData<GovernmentAreaData,GovernmentAreaDataAccess,UserCenterDb>
    {

        #region 编辑

        #region 基础继承

        /// <summary>
        ///     构造
        /// </summary>
        public GovernmentAreaBusinessLogic()
        {
            unityStateChanged = true;
        }

        /// <summary>
        ///     保存前的操作
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="isAdd">是否为新增</param>
        /// <returns>如果为否将阻止后续操作</returns>
        protected override bool OnSaving(GovernmentAreaData data, bool isAdd)
        {
            if (!isAdd)
                return true;
            if (data.ParentId > 0)
                return Access.ExistPrimaryKey(data.ParentId);
            data.ParentId = 0;
            return true;
        }

        /// <summary>
        ///     内部命令执行完成后的处理
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="cmd">命令</param>
        protected override void OnInnerCommand(GovernmentAreaData data, BusinessCommandType cmd)
        {
            SyncTreeInfo(data);
            OrganizationBusinessLogic.Cache();
        }

        #endregion


        #region 编辑树

        /// <summary>
        ///     载入完整的组织结构树(用于编辑)
        /// </summary>
        /// <returns></returns>
        public List<GovernmentAreaData> LoadEditTree()
        {
            var root = new GovernmentAreaData
            {
                ShortName = ConfigurationManager.GetAppSetting("orgShortName", "根"),
                FullName = ConfigurationManager.GetAppSetting("orgFullName", "根"),
            };
            var lists = Access.All(p => p.DataState < DataStateType.Delete);
            var results = new List<GovernmentAreaData>();
            foreach (var level in lists.GroupBy(p => p.OrgLevel)) results.AddRange(level.OrderBy(p => p.LevelIndex));
            LoadChildren(root, 0, results);
            return new List<GovernmentAreaData> { root };
        }

        /// <summary>
        ///     载入完整的组织结构树
        /// </summary>
        /// <returns></returns>
        public List<GovernmentAreaData> LoadTree(int pid)
        {
            var lists = Access.All(p => p.DataState < DataStateType.Delete);
            var root = lists.FirstOrDefault(p => p.Id == pid);
            if (root == null)
                return null;
            LoadChildren(root, pid, lists);
            return new List<GovernmentAreaData> { root };
        }

        /// <summary>
        ///     设置子级以构成树
        /// </summary>
        /// b
        /// <param name="par"></param>
        /// <param name="pid"></param>
        /// <param name="lists"></param>
        private void LoadChildren(GovernmentAreaData par, long pid, List<GovernmentAreaData> lists)
        {
            var childs = lists.Where(p => p.ParentId == pid).ToArray();
            if (childs.Length == 0)
                return;

            foreach (var level in childs.GroupBy(p => p.OrgLevel))
                foreach (var child in level.OrderBy(p => p.LevelIndex))
                    LoadChildren(child, child.Id, lists);
            par.Children = childs;
        }

        #endregion

        #region 导入

        /// <summary>
        ///     导入
        /// </summary>
        /// <param name="texts"></param>
        public void Import(string texts)
        {
            var lines = texts.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var preSp = 0;
            var stack = new FixStack<GovernmentAreaData>();
            stack.SetFix(new GovernmentAreaData());
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
                GovernmentAreaData data;
                Access.Insert(data = new GovernmentAreaData
                {
                    ParentId = stack.Current.Id,
                    OrgLevel = stack.StackCount,
                    FullName = words[0],
                    ShortName = words[0],
                    Code = words[1]
                });
                stack.Push(data);
            }

            SyncTreeInfo();
            OrganizationBusinessLogic.Cache();
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
        private void SyncTreeInfo(GovernmentAreaData entity)
        {
            if (entity == null || entity.Id == 0)
                return;
            try
            {
                if (entity.ParentId == 0)
                {
                    entity.TreeName = entity.ShortName;
                    SyncChild(entity);
                    return;
                }

                var parent = Access.LoadByPrimaryKey(entity.ParentId);
                if (parent == null)
                {
                    entity.ParentId = 0;
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
        private void SyncTreeInfo(GovernmentAreaData parent, GovernmentAreaData entity)
        {
            if (parent.DataState >= DataStateType.Disable)
                entity.DataState = parent.DataState;
            entity.TreeName = parent.TreeName + ">" + entity.ShortName;
            SyncChild(entity);
        }


        /// <summary>
        ///     同步更改到下级
        /// </summary>
        /// <param name="entity">数据</param>
        private void SyncChild(GovernmentAreaData entity)
        {
            Access.Update(entity);
            var children = Access.All(p => p.ParentId == entity.Id);
            foreach (var child in children)
                SyncTreeInfo(entity, child);
        }

        #endregion

        #endregion


        /// <summary>
        ///     载入完整的地区树(UI相关）
        /// </summary>
        /// <returns></returns>
        public List<EasyUiTreeNode> LoadComboTreeForUi()
        {
            var datas = Access.All(p => p.DataState <= DataStateType.Disable);
            var results = new List<EasyUiTreeNode>
            {
                new EasyUiTreeNode
                {
                    ID = 0,
                    Text = "-"
                }
            };
            foreach (var data in datas)
                results.Add(new EasyUiTreeNode
                {
                    ID = data.Id,
                    Icon = "icon-area",
                    IsOpen = true,
                    IsFolder = true,
                    Title = data.FullName,
                    Text = data.ShortName
                });

            return results;
        }
    }
}