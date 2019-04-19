using System;
using System.Collections.Concurrent;
using System.Threading;
using Agebull.Common.Configuration;
using Agebull.Common.Context;
using Agebull.Common.Logging;
using Agebull.Common.OAuth;
using Agebull.Common.Organizations.DataAccess;
using Agebull.EntityModel.Common;
using Agebull.EntityModel.Redis;

namespace Agebull.Common.Organizations
{
    /// <summary>
    /// 用户辅助类
    /// </summary>
    public static class UserHelper
    {
        #region 基本辅助



        private static string GetUserKey(long UserId)
        {
            return RedisKeyBuilder.ToAuthKey("user", "info", UserId);
        }
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
            AuthorizeScreen = AuthorizeType.Account,
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
        #endregion
        #region 获取用户

        /// <summary>
        ///     取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static LoginUserInfo GetLoginUserInfo(long userId)
        {
            if (userId <= 0)
            {
                GlobalContext.SetUser(AnymouseUser);
                return AnymouseUser;
            }
            LoginUserInfo response;
            using (var proxy = new RedisProxy(RedisProxy.DbAuthority))
            {
                response = proxy.Get<LoginUserInfo>(GetUserKey(userId));
            }
            if (response != null && response.UserId == userId)
                return response;
            return AnymouseUser;
        }

        /// <summary>
        ///     取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static LoginUserInfo ReloadUserInfo(long userId)
        {
            if (userId <= 0)
            {
                GlobalContext.SetUser(AnymouseUser);
                return AnymouseUser;
            }
            LoginUserInfo response;
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
                response = CacheUserInfo(user);
            }
            return response;
        }
        #endregion
        #region 缓存

        static readonly ConcurrentDictionary<long, long> CacheingUser = new ConcurrentDictionary<long, long>();

        /// <summary>
        ///     缓存用户
        /// </summary>
        /// <param name="person"></param>
        public static void CacheUserInfo(PersonData person)
        {
            if (!CacheingUser.TryAdd(person.UserId, person.UserId))
            {
                return;
            }
            try
            {
                CacheUserInfoInner(person);
            }
            finally
            {
                CacheingUser.TryRemove(person.UserId, out _);
            }
        }

        /// <summary>
        ///     取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static void CacheUserInfo(long userId)
        {
            if (userId <= 0)
                return;

            if (!CacheingUser.TryAdd(userId, userId))
            {
                return;
            }
            try
            {
                CacheUserInfoInner(userId);
            }
            finally
            {
                CacheingUser.TryRemove(userId, out _);
            }
        }

        /// <summary>
        ///     缓存用户
        /// </summary>
        /// <param name="user"></param>
        public static LoginUserInfo CacheUserInfo(UserData user)
        {
            if (!CacheingUser.TryAdd(user.Id, user.Id))
            {
                Thread.Sleep(100);
                return GetLoginUserInfo(user.UserId);
            }
            try
            {
                return CacheUserInfoInner(user);
            }
            finally
            {
                CacheingUser.TryRemove(user.UserId, out _);
            }
        }

        /// <summary>
        /// 缓存用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="person"></param>
        /// <param name="orgId"></param>
        /// <param name="groupId"></param>
        public static LoginUserInfo CacheUserInfo(UserData user, PersonData person, long orgId, long groupId)
        {
            if (!CacheingUser.TryAdd(user.Id, user.Id))
            {
                Thread.Sleep(100);
                return GetLoginUserInfo(user.UserId);
            }
            try
            {
                return CacheUserInfoInner(user, person, orgId, groupId);
            }
            finally
            {
                CacheingUser.TryRemove(user.UserId, out _);
            }
        }

        /// <summary>
        /// 缓存用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="person"></param>
        public static LoginUserInfo CacheUserInfo(UserData user, PersonData person)
        {
            if (!CacheingUser.TryAdd(user.Id, user.Id))
            {
                Thread.Sleep(100);
                return GetLoginUserInfo(user.UserId);
            }
            try
            {
                return CacheUserInfoInner(user, person, 0, 0);
            }
            finally
            {
                CacheingUser.TryRemove(user.UserId, out _);
            }
        }

        /// <summary>
        ///     取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        static void CacheUserInfoInner(long userId)
        {
            //读数据库
            var access = new UserDataAccess();
            var user = access.FirstOrDefault(x => x.UserId == userId);
            if (user != null)
                CacheUserInfoInner(user);
        }

        /// <summary>
        ///     缓存用户
        /// </summary>
        /// <param name="person"></param>
        static void CacheUserInfoInner(PersonData person)
        {
            //读数据库
            var access = new UserDataAccess();
            var user = access.FirstOrDefault(x => x.UserId == person.UserId);
            if (user != null)
                CacheUserInfoInner(user);
        }
        /// <summary>
        ///     缓存用户
        /// </summary>
        /// <param name="user"></param>
        static LoginUserInfo CacheUserInfoInner(UserData user)
        {
            var pAccess = new PersonDataAccess();

            var person = pAccess.LoadByPrimaryKey(user.UserId);
            return person != null ? CacheUserInfoInner(user, person, 0, 0) : null;
        }

        /// <summary>
        /// 缓存用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="person"></param>
        /// <param name="oid"></param>
        /// <param name="gid"></param>
        static LoginUserInfo CacheUserInfoInner(UserData user, PersonData person, long oid, long gid)
        {
            var fu = new LoginUserInfo
            {
                State = user.Status == UserStatusType.Regist
                        ? UserStateType.Enable
                        : UserStateType.Discard,
                OpenId = user.OpenId,
                OrganizationId = oid,
                GroupId = gid,
                Phone = person.PhoneNumber,
                Account = person.PhoneNumber,
                UserId = person.UserId,
                NickName = person.NickName,
                AvatarUrl = person.AvatarUrl
            };
            var aAccess = new AccountDataAccess();
            var accout = aAccess.FirstOrDefault(p => p.UserId == user.UserId && p.DataState <= DataStateType.Enable);
            if (accout != null)
            {
                fu.Role = accout.Role;
                fu.RoleId = accout.RoleId;
            }
            var pAccess = new PositionPersonnelDataAccess();
            var data = pAccess.FirstOrDefault(p => p.UserId == user.UserId && p.DataState <= DataStateType.Enable);
            if (data != null)
            {
                if (fu.RoleId == 0)
                {
                    fu.Role = data.Role;
                    fu.RoleId = data.RoleId;
                }
                fu.Organization = data.Organization;
                //fu.OrganizationId = data.OrganizationId;
                fu.Position = data.Position;
            }
            using (var proxy = new RedisProxy(RedisProxy.DbAuthority))
            {
                proxy.Set(GetUserKey(user.UserId), fu);
            }
            return fu;
        }

        /// <summary>
        /// 缓存用户
        /// </summary>
        public static void UpdateCache(LoginUserInfo fu)
        {
            using (var proxy = new RedisProxy(RedisProxy.DbAuthority))
            {
                proxy.Set(GetUserKey(fu.UserId), fu);
            }
        }
        #endregion

    }
}