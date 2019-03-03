/*design by:agebull designer date:2018/9/2 13:00:40*/
using Newtonsoft.Json;
using Gboxt.Common.DataModel;
using Gboxt.Common.DataModel.MySql;

namespace Agebull.Common.AppManage.DataAccess
{
    /// <summary>
    /// 页面节点
    /// </summary>
    sealed partial class PageItemDataAccess : MySqlTable<PageItemData,AppManageDb>
    {
        ///// <summary>
        /////     保存完成后期处理(Insert或Update)
        ///// </summary>
        ///// <param name="entity"></param>
        //protected override void OnDataSaved(PageItemData entity)
        //{
        //    using (var proxy = new RedisProxy())
        //    {
        //        proxy.RefreshCache<PageItemData, PageItemDataAccess>(entity.Id);
        //        var keys = proxy.Client.SearchKeys("ui:PageTree:*");
        //        proxy.Client.RemoveAll(keys);
        //    }
        //    base.OnDataSaved(entity);
        //}
        /// <summary>保存前处理</summary>
        /// <param name="entity">保存的对象</param>
        /// <param name="operatorType">操作类型</param>
        protected override void OnPrepareSave(DataOperatorType operatorType, PageItemData entity)
        {
            entity.Json = JsonConvert.SerializeObject(entity.Config);
            base.OnPrepareSave(operatorType, entity);
        }
    }
}