// // /*****************************************************
// // (c)2016-2016 Copy right www.gboxt.com
// // 作者:
// // 工程:Agebull.DataModel
// // 建立:2016-06-12
// // 修改:2016-06-17
// // *****************************************************/

#region 引用

using System.Collections.Generic;
using Agebull.Common.DataModel;
using Agebull.Common.DataModel.Redis;
using Newtonsoft.Json;

#endregion

namespace Agebull.Common.UserCenter
{
    /// <summary>
    /// 页面用户个性化数据逻辑类
    /// </summary>
    public static class QueryHistory
    {
        /// <summary>
        ///     保存用户的查询历史
        /// </summary>
        /// <param name="userId">用户</param>
        /// <param name="pageId">关联页面</param>
        /// <param name="args">查询参数</param>
        public static void SaveQueryHistory(long userId, long pageId, Dictionary<string, string> args)
        {
            if (userId == 0 || pageId == 0)
                return;
            using (var proxy = new RedisProxy(RedisProxy.DbSystem))
            {
                var key = DataKeyBuilder.ToKey("qh", pageId, userId);
                proxy.Set(key, JsonConvert.SerializeObject(args));
            }
            //PageDataDataAccess pdAccess = new PageDataDataAccess();
            //if (pdAccess.Any(p => p.UserId == loginUser.Id && p.PageId == page.Id))
            //{
            //    pdAccess.SetValue(p => p.PageData, JsonConvert.SerializeObject(args),
            //        p => p.UserId == loginUser.Id && p.PageId == page.Id);
            //}
            //else
            //{
            //    pdAccess.Insert(new PageDataData
            //    {
            //        PageId = page.Id,
            //        UserId = loginUser.Id,
            //        PageData = JsonConvert.SerializeObject(args)
            //    });
            //}
        }

        /// <summary>
        ///     读取用户的查询历史
        /// </summary>
        /// <param name="userId">用户</param>
        /// <param name="pageId">关联页面</param>
        /// <returns>返回的是参数字典的JSON格式的文本</returns>
        public static string LoadQueryHistory(long userId, long pageId)
        {
            if (userId == 0 || pageId == 0)
                return "{'page': 0,'order':'asc','size':20}";
            using (var proxy = new RedisProxy(RedisProxy.DbSystem))
            {
                var key = DataKeyBuilder.ToKey("qh", pageId, userId);
                return proxy.Get(key) ?? "{'page': 0,'order':'asc','size':20}";
            }
            //PageDataDataAccess pdAccess = new PageDataDataAccess();
            //return pdAccess.LoadValue(p => p.PageData, p => p.PageId == page.ID && p.UserId == loginUser.Id);
        }
    }
}