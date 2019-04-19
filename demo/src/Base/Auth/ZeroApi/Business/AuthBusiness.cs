using System;
using System.Configuration;
using Agebull.Common.Configuration;
using Agebull.Common.Context;
using Agebull.Common.Logging;
using Agebull.Common.OAuth;
using Agebull.Common.Organizations;
using Agebull.Common.Organizations.DataAccess;
using Agebull.EntityModel.Common;
using Agebull.EntityModel.MySql;
using Agebull.EntityModel.Redis;
using Agebull.MicroZero.PubSub;
using Agebull.MicroZero.ZeroApis;
using Agebull.OAuth.Contract;
using HPC.Projects;
using ConfigurationManager = Agebull.Common.Configuration.ConfigurationManager;
using UserData = Agebull.Common.Organizations.UserData;

namespace Agebull.OAuth.Business
{
    /// <summary>
    ///     Auth校验实现
    /// </summary>
    public class AuthBusiness : IOAuthBusiness
    {
        #region 配置

        /// <summary>
        ///     以小时为单位的AccessToken过期时间
        /// </summary>
        protected static int AccessTokenExpiresHour = ConfigurationManager.AppSettings.GetInt("AccessTokenExpiresHour", 2);


        /// <summary>
        ///     以小时为单位的RefreshToken过期时间
        /// </summary>
        protected static int RefreshTokenExpiresHour = ConfigurationManager.AppSettings.GetInt("RefreshTokenExpiresHour", 72);

        #endregion

        #region 接口实现

        /// <summary>
        ///     生成DeviceId
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        ApiValueResult IOAuthBusiness.GetDeviceId(DeviceArgument arg)
        {
            using (MonitorScope.CreateScope("GetDeviceId"))
            {
                try
                {
                    string did;
                    using (MonitorScope.CreateScope("db"))
                    {
                        did = CreateDeviceId(arg);
                    }

                    return ApiValueResult.Succees(did);
                }
                catch (Exception e)
                {
                    LogRecorder.Exception(e, "GetDeviceId");
                    return ApiValueResult.ErrorResult(ErrorCode.LocalError);
                }
            }
        }

        /// <summary>
        ///     Refresh access token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ApiResult<AccessTokenResponse> IOAuthBusiness.RefreshAccessToken(RefreshAccessTokenRequest request)
        {
            using (MonitorScope.CreateScope("RefreshAccessToken"))
            {
                if (request == null)
                    return ApiResult<AccessTokenResponse>.ErrorResult(ErrorCode.LogicalError);

                LogRecorder.MonitorTrace($"Argument:AccessToken={request.AccessToken},RefreshToken={request.RefreshToken}");

                using (MySqlDataBaseScope.CreateScope<UserCenterDb>())
                {
                    return RefreshAccessToken(request);
                }
            }
        }


        /// <summary>
        ///     检查AT(来自登录用户)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ApiResult<LoginUserInfo> IOAuthBusiness.VerifyAccessToken(TokenArgument request)
        {
            using (MonitorScope.CreateScope("VerifyAccessToken"))
            {
                if (string.IsNullOrWhiteSpace(request?.Token))
                    return ApiResult.Error<LoginUserInfo>(ErrorCode.Auth_AccessToken_Unknow);
                LogRecorder.MonitorTrace($"Argument:Token={request.Token}");
                using (MySqlDataBaseScope.CreateScope<UserCenterDb>())
                {
                    return VerifyAccessToken(request.Token);
                }
            }
        }

        /// <summary>
        ///     Create access token
        /// </summary>
        /// <returns></returns>
        ApiResult<AccessTokenResponse> IOAuthBusiness.CreateAccessToken(long userId, string account, string type, string deviceId)
        {
            using (MonitorScope.CreateScope("CreateAccessToken"))
            {
                return CreateAccessToken(userId, deviceId);
            }
        }

        /// <summary>
        ///     取得用户信息
        /// </summary>
        /// <param name="value">令牌或用户ID</param>
        /// <returns>用户信息</returns>
        ApiResult<LoginUserInfo> IOAuthBusiness.GetLoginUser(string value)
        {
            using (MonitorScope.CreateScope("GetLoginUser"))
            {
                return GetLoginUser(value);
            }
        }

        #endregion

        #region AT

        /// <summary>
        ///     Create access token
        /// </summary>
        /// <returns></returns>
        public ApiResult<AccessTokenResponse> CreateAccessToken(UserData user, PersonData person, EmployeeData employee, string deviceId)
        {
            if (string.IsNullOrEmpty(deviceId) || "?" == deviceId)
            {
                deviceId = CreateDeviceId(new DeviceArgument());
            }
            var deviceData = GetDeviceInfo(deviceId, false);
            if (deviceData == null)
            {
                return ApiResult<AccessTokenResponse>.ErrorResult(4000, "无法取得设备标识");
            }
            var userProfile = UserHelper.CacheUserInfo(user, person, employee.OrgOID, employee.SiteSID);

            GlobalContext.SetUser(userProfile);

            var token = new AuthTokenItem
            {
                AccessToken = CreateAccessToken(),
                RefreshToken = CreateRefreshToken()
            };
            UserTokenData tokenData;
            var tAccess = new UserTokenDataAccess();
            tAccess.Insert(tokenData = new UserTokenData
            {
                UserId = userProfile.UserId,
                UserDeviceId = deviceData.Id,
                DeviceId = deviceData.DeviceId,
                RefreshToken = token.RefreshToken,
                AccessToken = token.AccessToken,
                AddDate = DateTime.Now,
                AccessTokenExpiresTime = DateTime.Now.AddHours(AccessTokenExpiresHour),
                RefreshTokenExpiresTime = DateTime.Now.AddHours(RefreshTokenExpiresHour)
            });
            Redis_UserToken_Cache(tokenData);
            return ApiResult.Succees(new AccessTokenResponse
            {
                AccessToken = token.AccessToken,
                RefreshToken = token.RefreshToken,
                Profile = userProfile
            });
        }

        /// <summary>
        ///     Create access token
        /// </summary>
        /// <returns></returns>
        public ApiResult<AccessTokenResponse> CreateAccessToken(UserData user, PersonData person, string deviceId)
        {
            if (string.IsNullOrEmpty(deviceId) || "?" == deviceId)
            {
                deviceId = CreateDeviceId(new DeviceArgument());
            }
            var deviceData = GetDeviceInfo(deviceId, false);
            if (deviceData == null)
            {
                return ApiResult<AccessTokenResponse>.ErrorResult(4000, "无法取得设备标识");
            }
            var userProfile = UserHelper.CacheUserInfo(user, person);

            GlobalContext.SetUser(userProfile);

            var token = new AuthTokenItem
            {
                AccessToken = CreateAccessToken(),
                RefreshToken = CreateRefreshToken()
            };
            UserTokenData tokenData;
            var tAccess = new UserTokenDataAccess();
            tAccess.Insert(tokenData = new UserTokenData
            {
                UserId = userProfile.UserId,
                UserDeviceId = deviceData.Id,
                DeviceId = deviceData.DeviceId,
                RefreshToken = token.RefreshToken,
                AccessToken = token.AccessToken,
                AddDate = DateTime.Now,
                AccessTokenExpiresTime = DateTime.Now.AddHours(AccessTokenExpiresHour),
                RefreshTokenExpiresTime = DateTime.Now.AddHours(RefreshTokenExpiresHour)
            });
            Redis_UserToken_Cache(tokenData);
            return ApiResult.Succees(new AccessTokenResponse
            {
                AccessToken = token.AccessToken,
                RefreshToken = token.RefreshToken,
                Profile = userProfile
            });
        }

        /// <summary>
        ///     Create access token
        /// </summary>
        /// <returns></returns>
        public ApiResult<AccessTokenResponse> CreateAccessToken(long userId, string deviceId)
        {
            if (string.IsNullOrEmpty(deviceId) || "?" == deviceId)
            {
                deviceId = CreateDeviceId(new DeviceArgument());
            }
            var deviceData = GetDeviceInfo(deviceId, false);
            if (deviceData == null)
            {
                return ApiResult<AccessTokenResponse>.ErrorResult(4000, "无法取得设备标识");
            }
            var userProfile = UserHelper.ReloadUserInfo(userId);
            if (userProfile == null)
                return ApiResult<AccessTokenResponse>.ErrorResult(4001, "无法取得用户信息");

            GlobalContext.SetUser(userProfile);

            var token = new AuthTokenItem
            {
                AccessToken = CreateAccessToken(),
                RefreshToken = CreateRefreshToken()
            };
            UserTokenData tokenData;
            var tAccess = new UserTokenDataAccess();
            tAccess.Insert(tokenData = new UserTokenData
            {
                UserId = userProfile.UserId,
                UserDeviceId = deviceData.Id,
                DeviceId = deviceData.DeviceId,
                RefreshToken = token.RefreshToken,
                AccessToken = token.AccessToken,
                AddDate = DateTime.Now,
                AccessTokenExpiresTime = DateTime.Now.AddHours(AccessTokenExpiresHour),
                RefreshTokenExpiresTime = DateTime.Now.AddHours(RefreshTokenExpiresHour)
            });
            Redis_UserToken_Cache(tokenData);
            return ApiResult.Succees(new AccessTokenResponse
            {
                AccessToken = token.AccessToken,
                RefreshToken = token.RefreshToken,
                Profile = userProfile
            });
        }

        /// <summary>
        ///     检查AT(来自登录用户)
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public ApiResult<LoginUserInfo> VerifyAccessToken(string token)
        {
            var tokenData = Redis_UserToken_GetByAccessToken(token);
            if (tokenData == null)
            {
                LogRecorder.MonitorTrace($"AccessToken不存在或过期{token}");
                return ApiResult<LoginUserInfo>.ErrorResult(ErrorCode.Auth_AccessToken_TimeOut);
            }

            var userProfile = UserHelper.GetLoginUserInfo(tokenData.UserId);
            if (userProfile == null || userProfile.State != UserStateType.Enable)
            {
                LogRecorder.MonitorTrace($"用户不存在或禁用{tokenData.UserId}");
                return ApiResult<LoginUserInfo>.ErrorResult(ErrorCode.Auth_AccessToken_TimeOut);
            }

            var deviceInfo = GetDeviceInfo(tokenData.DeviceId, false);
            if (deviceInfo == null)
            {
                LogRecorder.MonitorTrace($"DeviceId不存在或过期{tokenData.DeviceId}");
                return ApiResult<LoginUserInfo>.ErrorResult(ErrorCode.Auth_AccessToken_TimeOut);
            }
            userProfile.AccessToken = token;
            userProfile.Os = deviceInfo.Os;
            userProfile.App = deviceInfo.App;
            //AuthEvents.RaiseUserStateChanegd(tokenData.UserId, "active", deviceInfo.App);
            return ApiResult.Succees(userProfile);
        }

        /// <summary>
        ///     刷新AT
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private ApiResult<AccessTokenResponse> RefreshAccessToken(RefreshAccessTokenRequest request)
        {
            var tokenData = Redis_UserToken_GetByAccessToken(request.AccessToken);
            if (tokenData != null)
            {
                return ApiResult.Succees(new AccessTokenResponse
                {
                    AccessToken = request.AccessToken,
                    RefreshToken = request.RefreshToken,
                    Profile = UserHelper.GetLoginUserInfo(tokenData.UserId)
                });
            }

            var token = Redis_UserToken_GetByRefreshToken(request.RefreshToken);
            if (token == null)
            {
                LogRecorder.MonitorTrace($"Redis中找不到RT{request.RefreshToken}");
                return ApiResult<AccessTokenResponse>.ErrorResult(ErrorCode.Auth_RefreshToken_Unknow);
            }

            Redis_UserToken_ClearByRefreshToken(request.RefreshToken);

            var userProfile = UserHelper.GetLoginUserInfo(token.UserId);
            if (userProfile == null || userProfile.State != UserStateType.Enable)
            {
                LogRecorder.MonitorTrace($"用户不存在或禁用{token.UserId}");
                return ApiResult<AccessTokenResponse>.ErrorResult(ErrorCode.Auth_RefreshToken_Unknow);
            }

            var deviceInfo = GetDeviceInfo(token.DeviceId, true);
            if (deviceInfo == null)
            {
                LogRecorder.MonitorTrace($"deviceInfo不存在或禁用{token.DeviceId}");
                return ApiResult<AccessTokenResponse>.ErrorResult(ErrorCode.Auth_RefreshToken_Unknow);
            }

            userProfile.Os = deviceInfo.Os;
            userProfile.App = deviceInfo.App;
            GlobalContext.SetUser(userProfile);

            var tAccess = new UserTokenDataAccess();
            UserTokenData userToken;
            tAccess.Insert(userToken = new UserTokenData
            {
                UserId = token.UserId,
                UserDeviceId = deviceInfo.Id,
                DeviceId = token.DeviceId,
                RefreshToken = CreateRefreshToken(),
                AccessToken = CreateAccessToken(),
                AddDate = DateTime.Now
            });
            Redis_UserToken_Cache(userToken);

            //AuthEvents.RaiseUserStateChanegd(token.UserId, "refresh", deviceInfo.App);
            return ApiResult.Succees(new AccessTokenResponse
            {
                AccessToken = userToken.AccessToken,
                RefreshToken = userToken.RefreshToken,
                Profile = userProfile
            });
        }


        private string CreateAccessToken()
        {
            return $"#{RandomOperate.Generate(12)}";
        }

        private static string CreateRefreshToken()
        {
            return $"${RandomOperate.Generate(12)}";
        }
        #endregion

        #region User

        /// <summary>
        ///     取得用户信息
        /// </summary>
        /// <param name="value">令牌或用户ID</param>
        /// <returns>用户信息</returns>
        ApiResult<LoginUserInfo> GetLoginUser(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                LogRecorder.MonitorTrace("参数错误");
                return ApiResult.Error<LoginUserInfo>(ErrorCode.LogicalError, "参数错误");
            }

            if (value[0] == '#')
            {
                var tokenData = Redis_UserToken_GetByAccessToken(value);
                if (tokenData == null)
                {
                    LogRecorder.MonitorTrace($"AT无效或过期{value}");
                    return ApiResult.Error<LoginUserInfo>(ErrorCode.Auth_AccessToken_Unknow);
                }

                var user = UserHelper.GetLoginUserInfo(tokenData.UserId);
                if (user != null && user.State == UserStateType.Enable)
                    return ApiResult.Succees(user);

                LogRecorder.MonitorTrace($"{(user == null ? "错误" : "禁用")}的UserId{tokenData.UserId}");
                return ApiResult.Error<LoginUserInfo>(ErrorCode.Auth_User_Unknow);
            }

            if (long.TryParse(value, out var uid))
            {
                var user1 = UserHelper.GetLoginUserInfo(uid);
                if (user1 != null && user1.State == UserStateType.Enable)
                    return ApiResult.Succees(user1);
                LogRecorder.MonitorTrace($"不正确的UserId{uid}");
                return ApiResult.Error<LoginUserInfo>(ErrorCode.Auth_User_Unknow);
            }
            return ApiResult.Error<LoginUserInfo>(ErrorCode.LogicalError, "参数错误");
        }
        #endregion

        #region DeviceId


        /// <summary>
        ///     检查设备标识（来自未登录用户）
        /// </summary>
        /// <param name="token">令牌</param>
        /// <returns></returns>
        public ApiResult<LoginUserInfo> ValidateDeviceId(string token)
        {
            using (MonitorScope.CreateScope("ValidateDeviceId"))
            {
                var dv = GetDeviceInfo(token, false);

                if (string.IsNullOrWhiteSpace(dv?.Os) || string.IsNullOrWhiteSpace(dv.App))
                    return ApiResult<LoginUserInfo>.ErrorResult(ErrorCode.Auth_Device_Unknow);
                return ApiResult.Succees(UserHelper.AnymouseUser);
            }
        }

        /// <summary>
        ///     生成DeviceId
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private string CreateDeviceId(DeviceArgument arg)
        {
            //DeviceId(did) 格式： *VL8BCCBF_APP_LINUX

            arg.App = arg.App?.ToUpper() ?? "APP";
            arg.Os = arg.Os?.ToUpper() ?? "LINUX";
            var argDid = arg.DeviceId;
            var isOld = !string.IsNullOrEmpty(argDid) && arg.DeviceId[0] == '*';
            if (isOld)
            {
                var words = argDid.Split('_');
                if (words.Length != 3)
                {
                    isOld = false;
                }
                else
                {
                    if (words[1] != arg.App)
                    {
                        words[1] = arg.App;
                        isOld = false;
                    }

                    if (words[2] != arg.Os)
                    {
                        words[2] = arg.Os;
                        isOld = false;
                    }
                }
            }

            var access = new UserDeviceDataAccess();
            UserDeviceData deviceInfo = null;
            {
                if (isOld)
                {
                    deviceInfo = Redis_Device_Get(arg.DeviceId);
                    if (deviceInfo == null)
                        deviceInfo = access.FirstOrDefault(p => p.DeviceId == arg.DeviceId);
                    else if (deviceInfo.Os == arg.Os && deviceInfo.App == arg.App)
                        return deviceInfo.DeviceId;
                }

                if (deviceInfo == null || deviceInfo.Os != arg.Os || deviceInfo.App != arg.App)
                {
                    arg.DeviceId = string.Join("_", "*" + RandomOperate.Generate(8), arg.App, arg.Os);
                    access.Insert(deviceInfo = new UserDeviceData
                    {
                        DeviceId = arg.DeviceId,
                        Os = arg.Os,
                        App = arg.App,
                        DeviceInfo = arg.DeviceInformation,
                        AddDate = DateTime.Now
                    });
                }

                Redis_Device_Cache(deviceInfo);
            }
            //try
            //{
            //    DeviceInfos.Add(deviceInfo.DeviceId, deviceInfo);
            //}
            //catch
            //{
            //}
            return deviceInfo.DeviceId;
        }


        //static Dictionary<string, UserDeviceData> DeviceInfos = new Dictionary<string, UserDeviceData>(StringComparer.InvariantCultureIgnoreCase);
        /// <summary>
        ///     取得缓存DeviceId
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="reload"></param>
        internal UserDeviceData GetDeviceInfo(string deviceId, bool reload)
        {
            var deviceInfo = Redis_Device_Get(deviceId);
            if (deviceInfo != null || !reload)
                return deviceInfo;
            var dAccess = new UserDeviceDataAccess();
            deviceInfo = dAccess.FirstOrDefault(p => p.DeviceId == deviceId);
            if (deviceInfo == null)
                return null;
            if (string.IsNullOrWhiteSpace(deviceInfo.Os) || string.IsNullOrWhiteSpace(deviceInfo.App))
            {
                dAccess.DeletePrimaryKey(deviceInfo.Id);
                return null;
            }

            Redis_Device_Cache(deviceInfo);
            return deviceInfo;
        }

        #endregion

        #region Redis Cache

        /// <summary>
        ///     缓存DeviceData
        /// </summary>
        /// <param name="tokenData"></param>
        protected void Redis_Device_Cache(UserDeviceData tokenData)
        {
            using (var proxy = new RedisProxy(RedisProxy.DbAuthority))
            {
                proxy.Set(RedisKeyBuilder.ToAuthKey("token", "did", tokenData.DeviceId), tokenData, TimeSpan.FromHours(AccessTokenExpiresHour * 2));
            }
        }

        /// <summary>
        ///     获取缓存中的DeviceData
        /// </summary>
        /// <param name="did"></param>
        protected UserDeviceData Redis_Device_Get(string did)
        {
            using (var proxy = new RedisProxy(RedisProxy.DbAuthority))
            {
                return proxy.Get<UserDeviceData>(RedisKeyBuilder.ToAuthKey("token", "did", did));
            }
        }

        /// <summary>
        ///     缓存UserToken
        /// </summary>
        /// <param name="tokenData"></param>
        protected void Redis_UserToken_Cache(UserTokenData tokenData)
        {
            using (var proxy = new RedisProxy(RedisProxy.DbAuthority))
            {
                proxy.Set(RedisKeyBuilder.ToAuthKey("at", tokenData.AccessToken), tokenData, TimeSpan.FromHours(AccessTokenExpiresHour));
                proxy.Set(RedisKeyBuilder.ToAuthKey("rt", tokenData.RefreshToken), tokenData, TimeSpan.FromHours(RefreshTokenExpiresHour));
            }
        }

        /// <summary>
        ///     获取缓存中的UserToken
        /// </summary>
        /// <param name="accessToken"></param>
        protected UserTokenData Redis_UserToken_GetByAccessToken(string accessToken)
        {
            UserTokenData tokenData;
            using (var proxy = new RedisProxy(RedisProxy.DbAuthority))
            {
                tokenData = proxy.Get<UserTokenData>(RedisKeyBuilder.ToAuthKey("at", accessToken));
            }

            return tokenData;
        }

        /// <summary>
        ///     获取缓存中的UserToken
        /// </summary>
        /// <param name="refreshToken"></param>
        protected UserTokenData Redis_UserToken_GetByRefreshToken(string refreshToken)
        {
            UserTokenData tokenData;
            using (var proxy = new RedisProxy(RedisProxy.DbAuthority))
            {
                tokenData = proxy.Get<UserTokenData>(RedisKeyBuilder.ToAuthKey("rt", refreshToken));
            }

            if (tokenData != null)
                return tokenData;
            var access = new UserTokenDataAccess();
            tokenData = access.FirstOrDefault(p => p.RefreshToken == refreshToken);
            return tokenData;
        }

        /// <summary>
        ///     清除缓存中的UserToken
        /// </summary>
        /// <param name="refreshToken"></param>
        protected void Redis_UserToken_ClearByRefreshToken(string refreshToken)
        {
            //RefreshTokens.Remove(refreshToken);
            using (var proxy = new RedisProxy(RedisProxy.DbAuthority))
            {
                var info = proxy.Get<UserTokenData>(RedisKeyBuilder.ToAuthKey("rt", refreshToken));
                if (info == null)
                    return;
                proxy.RemoveKey(RedisKeyBuilder.ToAuthKey("rt", refreshToken));
                proxy.RemoveKey(RedisKeyBuilder.ToAuthKey("at", info.AccessToken));
            }
        }

        #endregion
    }
}