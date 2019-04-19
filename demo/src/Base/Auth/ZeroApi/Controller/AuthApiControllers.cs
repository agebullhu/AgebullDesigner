using Agebull.Common.Ioc;
using Agebull.Common.OAuth;
using Agebull.Common.Organizations;
using Agebull.EntityModel.Common;
using Agebull.MicroZero;
using Agebull.MicroZero.ZeroApis;

namespace Agebull.UserCenter.WebApi
{
    /// <summary>
    ///     令牌服务
    /// </summary>
    public class AuthApiControllers : ApiController
    {
        /// <summary>
        ///     校验AT并得到用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("v1/verify/at")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Anymouse)]
        public ApiResult<LoginUserInfo> VerifyAccessToken(TokenArgument request)
        {
            string message = null;
            if (request == null || !request.Validate(out message))
                return ApiResult<LoginUserInfo>.ErrorResult(ErrorCode.Auth_AccessToken_TimeOut, message);
            var bl = IocHelper.Create<IOAuthBusiness>();
            var result = bl.VerifyAccessToken(request);
            return result;
        }

        /// <summary>
        ///     校验DeviceId
        /// </summary>
        /// <param name="request">令牌</param>
        /// <returns></returns>
        [Route("v1/verify/did")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Anymouse)]
        public ApiResult<LoginUserInfo> ValidateDeviceId(TokenArgument request)
        {
            if (string.IsNullOrWhiteSpace(request.Token) || request.Token.Contains("."))
                return ApiResult<LoginUserInfo>.ErrorResult(ErrorCode.Auth_Device_Unknow);
            var bl = IocHelper.Create<IOAuthBusiness>();
            var result = bl.ValidateDeviceId(request.Token);
            return result;
        }


        /// <summary>
        ///     获取用户信息
        /// </summary>
        /// <param name="argument">用户ID 或者用户AccessToken 或者 DeviceId</param>
        /// <returns></returns>
        [Route("v1/oauth/user")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Anymouse)]
        public ApiResult<LoginUserInfo> GetLoginUser(Argument argument)
        {
            var bl = IocHelper.Create<IOAuthBusiness>();
            var result = bl.GetLoginUser(argument.Value);
            return result;
        }
    }
}