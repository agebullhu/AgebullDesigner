// // /*****************************************************
// // (c)2016-2017 Copy right Agebull
// // 作者:
// // 工程:Agebull.Common.UserCenter
// // 建立:2016-06-12
// // 修改:2016-06-17
// // *****************************************************/

#region 引用

using System;
using System.Collections.Generic;
using Agebull.Common.AppManage;
using Agebull.Common.AppManage.BusinessLogic;
using Agebull.EntityModel.Redis;
using Agebull.Common.Logging;


using Agebull.Common.OAuth;
using Agebull.Common.OAuth;
using Agebull.Common.UserCenter.BusinessLogic;
using Agebull.Common.Context;
using Agebull.MicroZero.ZeroApis;

#endregion

namespace Agebull.Common.UserCenter
{

    /// <summary>
    ///     权限检查对象
    /// </summary>
    public class AuthorityChecker : IPowerChecker
    {
        #region 用户信息

        /// <summary>
        ///     取当前登录用户信息
        /// </summary>
        /// <returns></returns>
        public static string GetLoginUserInfo()
        {
            return GlobalContext.Current.Token;
        }

        /// <summary>
        /// 保存页面的动作
        /// </summary>
        void IPowerChecker.SavePageAction(int pageid, string name, string title, string action, string type)
        {
            PageItemBusinessLogic.SavePageAction(pageid, name, title, action, type);
        }

        /// <summary>
        ///     重新载入用户信息
        /// </summary>
        void IPowerChecker.ReloadLoginUserInfo()
        {
        }

        #endregion

        #region 页面配置

        /// <summary>
        ///     载入页面配置
        /// </summary>
        /// <param name="url">不包含域名的相对url</param>
        /// <returns>页面配置</returns>
        public IPageItem LoadPageConfig(string url)
        {
            return PageItemBusinessLogic.GetPageItem(url);
        }

        /// <summary>
        ///     载入页面关联的按钮配置
        /// </summary>
        /// <param name="loginUser">登录用户</param>
        /// <param name="page">页面,不能为空</param>
        /// <returns>按钮配置集合</returns>
        List<string> IPowerChecker.LoadPageButtons(ILoginUserInfo loginUser, IPageItem page)
        {
            if (page == null)
            {
                return new List<string>();
            }
            return GlobalContext.Current.IsSystemMode
                ? PageItemBusinessLogic.LoadPageButtons(page.Id)
                : RoleCache.Instance.LoadButtons(loginUser.RoleId, page.Id);
        }

        #endregion

        #region 角色权限

        /// <summary>
        /// 默认写死的权限
        /// </summary>
        private static readonly Dictionary<string, bool> Defaults = new Dictionary<string, bool>
        {
            {"list",true },
            {"details",true },
            {"tree",true },
            {"eid",true }
        };

        /// <summary>
        ///     载入页面关联的按钮配置
        /// </summary>
        /// <param name="loginUser">登录用户</param>
        /// <param name="page">页面</param>
        /// <param name="action">动作</param>
        /// <returns>是否可执行页面动作</returns>
        bool IPowerChecker.CanDoAction(ILoginUserInfo loginUser, IPageItem page, string action)
        {
            if (GlobalContext.Current .IsSystemMode)
                return true;

            if (Defaults.ContainsKey(action))
                return Defaults[action];

            return page != null && RoleCache.Instance.CanDoAction(loginUser.RoleId, page.Id, action);
        }

        /// <summary>
        ///     载入用户账号信息
        /// </summary>
        bool IPowerChecker.LoadAuthority(string page)
        {
            var user = GlobalContext.Customer;
            LogRecorder.MonitorTrace($"当前用户:{user.NickName}({user.UserId})**{user.NickName}");
            BusinessContext.Context.PageItem = LoadPageConfig(page);
            if (BusinessContext.Context.PageItem == null)
            {
                LogRecorder.MonitorTrace("非法页面");
                return false;
            }
            if (BusinessContext.Context.PageItem.IsPublic)
            {
                LogRecorder.MonitorTrace("公共页面");
                BusinessContext.Context.CurrentPagePower = new SimpleRolePower
                {
                    Id = -1,
                    PageItemId = -1,
                    Power = RolePowerType.Allow,
                    RoleId = user.RoleId
                };
            }
            else
            {
                

                if (BusinessContext.Context.PageItem.IsHide)
                {
                    LogRecorder.MonitorTrace("隐藏页面");
                    BusinessContext.Context.CurrentPagePower = new SimpleRolePower
                    {
                        Id = -1,
                        PageItemId = -1,
                        Power = RolePowerType.Allow,
                        RoleId = user.RoleId
                    };
                }
                else
                {
                    BusinessContext.Context.CurrentPagePower = LoadPagePower(GlobalContext.Current.User,
                        BusinessContext.Context.PageItem);
                    if (BusinessContext.Context.CurrentPagePower == null)
                    {
                        LogRecorder.MonitorTrace("非法访问");
                        return false;
                    }

                    LogRecorder.MonitorTrace("授权访问");
                }
            }

            return true;
        }

        /// <summary>
        ///     载入用户的角色权限
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        List<IRolePower> IPowerChecker.LoadUserPowers(ILoginUserInfo loginUser)
        {
            return RoleCache.Instance.LoadUserPowers(loginUser.RoleId);
        }

        /// <summary>
        ///     载入用户的单页角色权限
        /// </summary>
        /// <param name="loginUser">登录用户</param>
        /// <param name="page">页面</param>
        /// <returns></returns>
        public IRolePower LoadPagePower(ILoginUserInfo loginUser, IPageItem page)
        {
            return page == null ? null : RoleCache.Instance.LoadPagePower(loginUser.RoleId, page.Id);
        }

        #endregion

        #region 查询历史

        /// <summary>
        ///     保存用户的查询历史
        /// </summary>
        /// <param name="loginUser">用户</param>
        /// <param name="page">关联页面</param>
        /// <param name="args">查询参数</param>
        void IPowerChecker.SaveQueryHistory(ILoginUserInfo loginUser, IPageItem page, Dictionary<string, string> args)
        {
            if (page == null || loginUser == null)
            {
                return;
            }
            QueryHistory.SaveQueryHistory(loginUser.UserId, page.Id, args);
        }

        /// <summary>
        ///     读取用户的查询历史
        /// </summary>
        /// <param name="loginUser">用户</param>
        /// <param name="page">关联页面</param>
        /// <returns>返回的是参数字典的JSON格式的文本</returns>
        string IPowerChecker.LoadQueryHistory(ILoginUserInfo loginUser, IPageItem page)
        {
            if (page == null || loginUser == null)
            {
                return null;
            }
            return QueryHistory.LoadQueryHistory(loginUser.UserId, page.Id);
        }

        #endregion


        #region 登录相关

        /// <summary>
        ///     载入用户
        /// </summary>
        /// <returns></returns>
        public static ILoginUserInfo LoadUser(string userHostAddress, string token)
        {
            if (string.IsNullOrEmpty(token) || token == "%")
                return LoginUserInfo.Anymouse;
            //if (userHostAddress == "::1")
            //    userHostAddress = "127.0.0.1";
            //int uid = CheckToken(userHostAddress, token);
            //if (uid <= 0)
            //    return LoginUser.Anymouse;
            return LoadUserInfo();
        }

        private static int CheckToken(string userHostAddress, string token)
        {
            if (token == "%")
                return -1;
            using (var proxy = new RedisProxy(RedisProxy.DbAuthority))
            {
                var ik = DataKeyBuilder.ToKey("login", token);
                var info = proxy.TryGet<LoginToken>(ik);
                if (info == null || info.TimeOut <= DateTime.Now)
                {
                    LogRecorder.RecordLoginLog("令牌{0}过期", token);
                    return 0;
                }
                if (info.Address != userHostAddress)
                {
                    LogRecorder.RecordLoginLog("IP【{0}】-【{1}】不相同", userHostAddress, info.Address);
                }
                info.TimeOut = DateTime.Now.AddMinutes(30);
                info.LastDateTime = DateTime.Now;
                proxy.Set(ik, info);
                return info.UserId;
            }
        }

        private static ILoginUserInfo LoadUserInfo()
        {
            return GlobalContext.Customer;
        }
        /// <summary>
        /// 用户登录的信息
        /// </summary>
        public class LoginToken
        {
            /// <summary>
            /// 用户ID
            /// </summary>
            public int UserId { get; set; }

            /// <summary>
            /// 登录IP
            /// </summary>
            public string Address { get; set; }

            /// <summary>
            /// 信息
            /// </summary>
            public string Token { get; set; }

            /// <summary>
            /// 登录时间
            /// </summary>
            public DateTime LoginDateTime { get; set; }

            /// <summary>
            /// 最后访问时间
            /// </summary>
            public DateTime LastDateTime { get; set; }

            /// <summary>
            /// 超时时间
            /// </summary>
            public DateTime TimeOut { get; set; }
        }
        #endregion

    }
}