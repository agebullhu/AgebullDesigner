/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2017/6/27 9:11:33*/
using Agebull.EntityModel.Common;
using Agebull.EntityModel.EasyUI;
using Agebull.EntityModel.Redis;
using System.Collections.Generic;
using System.Linq;


namespace Agebull.Common.Organizations.DataAccess
{
    partial class OrganizePositionDataAccess
    {
        /// <summary>
        /// 下拉列表键
        /// </summary>
        private const string comboKey = "ui:combo:OrganizePosition";
        /// <summary>
        /// 下拉树键
        /// </summary>
        private const string treeKey = "ui:tree:OrganizePosition";

        /// <summary>
        /// 取得下拉列表值
        /// </summary>
        /// <returns></returns>
        public static List<EasyComboValues> GetComboValues()
        {
            using (var proxy = new RedisProxy(RedisProxy.DbComboCache))
            {
                var result = proxy.Get<List<EasyComboValues>>(comboKey);
                if (result == null)
                {
                    var access = new OrganizePositionDataAccess();
                    var datas = access.All(p => p.DataState == DataStateType.Enable);
                    result = new List<EasyComboValues>{EasyComboValues.Empty};
                    result.AddRange(datas.Select(p => new EasyComboValues(p.Id, p.Position)));
                    proxy.Set(comboKey, result);
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
                var result = proxy.Get<List<EasyUiTreeNode>>(treeKey);
                if (result == null)
                {
                    var access = new OrganizePositionDataAccess();
                    var datas = access.All(p => p.DataState == DataStateType.Enable);
                    result = new List<EasyUiTreeNode>{EasyUiTreeNode.EmptyNode};
                    result.AddRange(datas.Select(p => new EasyUiTreeNode
                    {
                        ID = p.Id,
                        Text = p.Position,
                        Title = p.Position,
                        IsOpen = true
                    }));
                    proxy.Set(treeKey, result);
                }
                return result;
            }
        }

        /// <summary>
        ///     保存完成后期处理(Insert或Update)
        /// </summary>
        /// <param name="entity"></param>
        protected sealed override void OnDataSaved(DataOperatorType operatorType, OrganizePositionData entity)
        {
            using (var proxy = new RedisProxy(RedisProxy.DbComboCache))
            {
                proxy.SetEntity(entity);
                proxy.RemoveKey(treeKey);
                proxy.RemoveKey(comboKey);
            }
        }
    }
}