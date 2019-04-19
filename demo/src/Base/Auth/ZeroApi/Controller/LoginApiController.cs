
using System.ComponentModel;
using Agebull.Common.Context;
using Agebull.Common.Ioc;
using Agebull.Common.Organizations;
using Agebull.EntityModel.Common;
using Agebull.MicroZero;

using Agebull.MicroZero.ZeroApis;

namespace Agebull.UserCenter.WebApi
{
    /// <summary>
    /// 登录
    /// </summary>
    [Category("登录")]
    public class LoginApiController : ApiController
    {

        /// <summary>
        ///  使用账号密码验证码登录
        /// </summary>
        /// <param name="arg">基于手机的账号登录参数</param>
        /// <returns>登录返回数据</returns>
        [Route("v1/login/account")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public ApiResult<LoginResponse> AccountLogin(PhoneLoginRequest arg)
        {
            var lg = IocHelper.Create<IUserApi>();
            var result = lg.AccountLogin(arg);
            return result;
        }
        
        /// <summary>
        /// 获取设备标识
        /// </summary>
        /// <returns></returns>
        [Route("v1/refresh/did"), Category("令牌")]
        [ApiAccessOptionFilter(ApiAccessOption.Public | ApiAccessOption.Internal)]
        public ApiValueResult GetDeviceId(DeviceArgument arg)
        {
            if (arg == null)
            {
                return ApiValueResult.ErrorResult(ErrorCode.LogicalError, "参数错误");
            }
            if (string.IsNullOrWhiteSpace(arg.DeviceId))
                arg.DeviceId = GlobalContext.Current.Token;
            arg.Version = 2;
            var bl = IocHelper.Create<IOAuthBusiness>();
            var result = bl.GetDeviceId(arg);
            return result;
        }



        /// <summary>
        /// 刷新令牌
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <returns></returns>

        [Route("v1/refresh/at"), Category("令牌")]
        [ApiAccessOptionFilter(ApiAccessOption.Public | ApiAccessOption.Internal )]
        public ApiResult<AccessTokenResponse> RefreshAccessToken(RefreshAccessTokenRequest request)
        {
            string message = null;
            if (request == null || !request.Validate(out message))
            {
                return ApiResult<AccessTokenResponse>.ErrorResult(ErrorCode.Auth_RefreshToken_Unknow, message);
            }
            /*/拒绝异步同时请求
            var key = DataKeyBuilder.ToKey("sys", "lk", "au", "rt", request.RefreshToken);
            var proxy = RedisHelper.GetRedis(RedisHelper.DbSystem);
            {
                if (!proxy.Client.SetEntryIfNotExists(key, "RefreshAccessToken"))
                {
                    LogRecorder.MonitorTrace("重复刷新AT");
                    return (ApiResult<AccessTokenResponse>.ErrorResult(ErrorCode.DenyAccess, message));
                }
            }*/
            var bl = IocHelper.Create<IOAuthBusiness>();
            var result = bl.RefreshAccessToken(request);
            if (result.Success)
                result.ResultData.Profile = null;
            return result;
        }


        /// <summary>
        ///     错误登录次数
        /// </summary>
        /// <param name="arg">手机号请求参数</param>
        /// <returns>错误登录次数返回值</returns>
        [Route("v1/login/errorcount")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public ApiResult<LoginErrorCountResponse> LoginErrorCount(MobilePhoneRequest arg)
        {
            var lg = IocHelper.Create<IUserApi>();
            var result = lg.LoginErrorCount(arg);
            return result;
        }


    }
}
