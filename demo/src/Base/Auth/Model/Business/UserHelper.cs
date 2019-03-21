using System;
using System.Collections.Generic;
using Agebull.Common.Configuration;
using Agebull.Common.Context;
using Agebull.Common.Logging;
using Agebull.Common.OAuth.DataAccess;
using Agebull.EntityModel.Common;
using Agebull.EntityModel.Redis;

namespace Agebull.Common.OAuth
{
    /// <summary>
    /// 用户辅助类
    /// </summary>
    public static class UserHelper
    {
        /// <summary>
        /// 取缺省昵称
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static string GetDefaultNikeName(string phone)
        {
            return string.IsNullOrWhiteSpace(phone) ? "游客" : $"{phone.Substring(0, 3)}****{phone.Substring(7, 4)}";
        }
        /// <summary>
        /// 缺省头像
        /// </summary>
        public static readonly string DefaultAvatarUrlPath = ConfigurationManager.AppSettings["DefaultAvatar"];

        /// <summary>
        /// 游客
        /// </summary>
        public static LoginUserInfo AnymouseUser = new LoginUserInfo
        {
            UserId = -1,
            State = UserStateType.None,
            NickName = "游客",
            Account = "Anymouse"
        };


        /// <summary>
        /// 生成新的用户
        /// </summary>
        /// <returns></returns>
        public static UserData NewUser() => new UserData
        {
            UserId = SmallFlakes<long>.Oxidize(),
            OpenId = NewOpenId(),
            UserType = AuthorizeType.Account,
            RegistSoure = AuthorizeType.Account,
            DeviceId = GlobalContext.Customer.DeviceId,
            AddDate = DateTime.Now,
            Os = GlobalContext.Customer.Os,
            Status = UserStatusType.Regist
        };


        /// <summary>
        /// 生成新的用户ID
        /// </summary>
        /// <returns></returns>
        public static string NewOpenId()
        {
            return RandomOperate.Generate(12);
        }

        /// <summary>
        ///     取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="reload"></param>
        /// <returns></returns>
        public static LoginUserInfo GetLoginUserInfo(long userId, bool reload)
        {
            if (userId <= 0)
            {
                GlobalContext.SetUser(AnymouseUser);
                return AnymouseUser;
            }
            LoginUserInfo response;
            if (!reload)
            {
                using (var proxy = new RedisProxy(RedisProxy.DbAuthority))
                {
                    response = proxy.Get<LoginUserInfo>(Redis_GetKey(userId));
                }
                if (response != null && response.UserId == userId)
                    return response;
            }


            //读数据库
            var access = new UserDataAccess();
            var user = access.FirstOrDefault(x => x.UserId == userId);
            if (user == null)
            {
                LogRecorder.MonitorTrace($"数据库中找不到用户ID,{userId}");
                //这是一个错误的用户（这样做是为了让redis可以一次命中，让无效ID对数据库的伤害最小；否则无效ID将导致最严重的系统消耗，一次REDIS，一次数据库）
                response = AnymouseUser;
            }
            else
            {
                response = CacheUserProfile(user);
            }
            return response;
        }
        static readonly Dictionary<long, long> CacheingUser = new Dictionary<long, long>();
        /// <summary>
        ///     取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static LoginUserInfo CacheUserProfile(long userId)
        {
            if (userId <= 0)
                return null;

            //读数据库
            var access = new UserDataAccess();
            var user = access.FirstOrDefault(x => x.UserId == userId);
            return user == null ? null : CacheUserProfile(user);
        }

        /// <summary>
        ///     取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static LoginUserInfo SyncUserProfile(long userId)
        {
            lock (CacheingUser)
            {
                if (CacheingUser.ContainsKey(userId))
                    return null;
            }
            return CacheUserProfile(userId);
        }

        /// <summary>
        ///     缓存用户
        /// </summary>
        /// <param name="user"></param>
        public static LoginUserInfo CacheUserProfile(UserData user)
        {
            bool doit = false;
            lock (CacheingUser)
            {
                if (!CacheingUser.ContainsKey(user.Id))
                {
                    CacheingUser.Add(user.Id, user.Id);
                    doit = true;
                }
            }
            try
            {
                var pAccess = new PersonDataAccess();

                var person = pAccess.LoadByPrimaryKey(user.UserId);
                return person != null ? CacheUserInfo(user, person) : null;
            }
            finally
            {
                if (doit)
                    lock (CacheingUser)
                        CacheingUser.Remove(user.Id);
            }
        }

        /// <summary>
        /// 缓存用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="person"></param>
        public static LoginUserInfo CacheUserInfo(UserData user, PersonData person)
        {
            bool doit = false;
            lock (CacheingUser)
            {
                if (!CacheingUser.ContainsKey(user.Id))
                {
                    CacheingUser.Add(user.Id, user.Id);
                    doit = true;
                }
            }
            try
            {
                var fu = new LoginUserInfo
                {
                    State = user.Status == UserStatusType.Regist
                        ? UserStateType.Enable
                        : UserStateType.Discard,
                    OpenId = user.OpenId,
                    
                    Phone = person.PhoneNumber,
                    Account = person.PhoneNumber,
                    UserId = person.UserId,
                    NickName = person.NickName,
                    AvatarUrl = person.AvatarUrl
                };

                using (var proxy = new RedisProxy(RedisProxy.DbAuthority))
                {
                    proxy.Set(Redis_GetKey(user.UserId), fu);
                }
                return fu;
            }
            finally
            {
                if (doit)
                    lock (CacheingUser)
                        CacheingUser.Remove(user.Id);
            }
        }


        private static string Redis_GetKey(long UserId)
        {
            return RedisKeyBuilder.ToAuthKey("user", "info", UserId);
        }

    }
}