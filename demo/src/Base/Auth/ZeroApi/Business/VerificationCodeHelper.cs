using Agebull.Common.Configuration;
using Agebull.Common.Context;
using Agebull.EntityModel.Redis;
using System;
using System.Diagnostics;


namespace Agebull.Common.OAuth
{

    /// <summary>
    /// 校验码辅助类
    /// </summary>
    public static class VerificationCodeHelper
    {

        #region 图片验证码

        /// <summary>
        /// 超级图片验证码 请勿设置的过于简单，可以不设置，若设置为"Ignore",则传递任何图片验证码皆可验证通过
        /// </summary>
        private static readonly string SupperImgVc = ConfigurationManager.AppSettings["SupperImgVc"];


        /// <summary>
        /// 检验图片验证码
        /// </summary>
        /// <param name="code"></param>
        /// <param name="RpcToken"></param>
        /// <returns></returns>
        public static bool ValidateImg(string code, string RpcToken = null)
        {
            //超级图片验证码
            if (!string.IsNullOrWhiteSpace(SupperImgVc))
            {
                if ("Ignore" == SupperImgVc)
                    return true;
                if (string.Equals(SupperImgVc, code, StringComparison.OrdinalIgnoreCase))
                    return true;
            }


            string codeFromRedis;
            using (var proxy = new RedisProxy(RedisProxy.DbSystem))
            {
                var key = RedisKeyBuilder.ToBusinessKey("vc", "img", RpcToken ?? GlobalContext.Current.Token);
                codeFromRedis = proxy.Get(key);
                if (codeFromRedis != null)
                {
                    proxy.RemoveKey(key);
                }
            }
            return string.Equals(codeFromRedis, code, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 缓存图片验证码(5分钟后自动失效)
        /// </summary>
        /// <param name="code"></param>
        /// <param name="RpcToken"></param>
        public static void CacheImg(string code,string RpcToken=null)
        {
            using (var proxy = new RedisProxy(RedisProxy.DbSystem))
            {
                
                var key = RedisKeyBuilder.ToBusinessKey("vc", "img", RpcToken??GlobalContext.Current.Token);
                proxy.Set(key, code, TimeSpan.FromMinutes(5));
            }
        }
        #endregion



        #region 短信验证码

        /// <summary>
        /// 超级短信验证码 请勿设置的过于简单，限制为 “6位数字” 或者 “4位字母数字(区分大小写）”
        /// </summary>
        private static readonly string supper = ConfigurationManager.AppSettings["SupperSmsVc"];

        /// <summary>
        /// 检验短信验证码
        /// </summary>
        public static bool ValidateSms(string vc, string phone)
        {
            if (string.IsNullOrWhiteSpace(vc))
                return false;
            if (string.Equals(supper, vc, StringComparison.OrdinalIgnoreCase))
                return true;
            string code;
            var key = RedisKeyBuilder.ToBusinessKey("vc", "sms", phone);
            using (var proxy = new RedisProxy(RedisProxy.DbSystem))
            {
                code = proxy.Get(key);
                if (code != null)
                {
                    proxy.Set(key, code, TimeSpan.FromSeconds(10));
                }
            }
            return string.Equals(code, vc, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 缓存短信验证码(5分钟后自动失效)
        /// </summary>
        /// <param name="vc"></param>
        /// <param name="phone"></param>
        public static void CacheSms(string vc, string phone)
        {
            var key = RedisKeyBuilder.ToBusinessKey("vc", "sms", phone);
            using (var proxy = new RedisProxy(RedisProxy.DbSystem))
            {
                proxy.Set(key, vc, TimeSpan.FromMinutes(5));
            }
            Trace.WriteLine($"短信：{vc}【{phone}】");
        }
        #endregion
    }
}