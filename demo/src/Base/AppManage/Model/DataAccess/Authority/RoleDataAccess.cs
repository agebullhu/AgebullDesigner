/*design by:agebull designer date:2018/9/2 12:32:49*/
using System.Collections.Generic;
using System.Linq;
using Agebull.EntityModel.Common;
using Agebull.EntityModel.MySql;
using Agebull.EntityModel.Redis;
using Agebull.EntityModel.EasyUI;

namespace Agebull.Common.AppManage.DataAccess
{
    /// <summary>
    /// 角色
    /// </summary>
    sealed partial class RoleDataAccess : DataStateTable<RoleData,AppManageDb>
    {
        /// <summary>
        /// 下拉列表键
        /// </summary>
        private const string ComboKey = "ui:combo:Role";
        /// <summary>
        /// 下拉树键
        /// </summary>
        private const string TreeKey = "ui:tree:Role";

        /// <summary>
        /// 取得下拉列表值
        /// </summary>
        /// <returns></returns>
        public static List<EasyComboValues> GetComboValues()
        {
            using (var proxy = new RedisProxy(RedisProxy.DbComboCache))
            {
                var result = proxy.Get<List<EasyComboValues>>(ComboKey);
                if (result == null)
                {
                    var access = new RoleDataAccess();
                    var datas = access.All();
                    result = new List<EasyComboValues> { EasyComboValues.Empty };
                    result.AddRange(datas.Select(p => new EasyComboValues(p.Id, p.Caption)));
                    proxy.Set(ComboKey, result);
                }
                return result;
            }
        }

        /// <summary>
        /// 取得下拉树值
        /// </summary>
        /// <returns></returns>
        public static List<EasyUiTreeNode> GetTreeValues()
        {
            using (var proxy = new RedisProxy(RedisProxy.DbComboCache))
            {
                var result = proxy.Get<List<EasyUiTreeNode>>(TreeKey);
                if (result == null)
                {
                    var access = new RoleDataAccess();
                    var datas = access.All();
                    result = new List<EasyUiTreeNode> { EasyUiTreeNode.EmptyNode };
                    result.AddRange(datas.Select(p => new EasyUiTreeNode
                    {
                        ID = p.Id,
                        Text = p.Caption,
                        Title = p.Caption,
                        IsOpen = true
                    }));
                    proxy.Set(TreeKey, result);
                }
                return result;
            }
        }

        /// <summary>
        ///     保存完成后期处理(Insert或Update)
        /// </summary>
        /// <param name="operatorType"></param>
        /// <param name="entity"></param>
        protected sealed override void OnDataSaved(DataOperatorType operatorType, RoleData entity)
        {
            using (var proxy = new RedisProxy(RedisProxy.DbComboCache))
            {
                proxy.RemoveKey(TreeKey);
                proxy.RemoveKey(ComboKey);
            }
        }
    }
}