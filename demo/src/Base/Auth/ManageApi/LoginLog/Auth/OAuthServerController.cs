using System.Linq;
using System.Text;
using Agebull.Common.Ioc;

using Agebull.Common.WebApi;
using Gboxt.Common.DataModel;
using System.Web.Http;
using Agebull.Common.OAuth;

namespace FlyshCloud.OAuth.InternalService.Api.Controllers
{
    /// <summary>
    /// 身份验证服务API
    /// </summary>
    //[EnableCors("*", "*", "*")]
    public class OAuthServerController : ApiController
    {

        /// <summary>
        /// 生成AT
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("v1/oauth/accessToken/put")]
        //[ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Anymouse)]
        public ApiResponseMessage<ApiResult<AccessTokenResponse>> CreateAccessToken([FromBody]CreateAccessTokenRequest request)
        {
            string message = null;
            if (request == null || !request.Validate(out message))
            {
                return Request.ToResponse(ApiResult.Error<AccessTokenResponse>(ErrorCode.Auth_AccessToken_TimeOut, message));
            }

            //var key = DataKeyBuilder.ToKey("sys", "lk", "au", "did", request.DeviceId);
            //using (var proxy = new RedisProxy(RedisProxy.DbAuthority))
            {
                //if (!proxy.Client.SetEntryIfNotExists(key, "CreateAccessToken"))
                //{
                //    LogRecorder.MonitorTrace("重复申请AT");
                //    return Request.ToResponse(ApiResult<AccessTokenResponse>.ErrorResult(ErrorCode.DenyAccess, message));
                //}
                //proxy.Client.Expire(key, 10);
                var bl = IocHelper.Create<IOAuthBusiness>();
                var result = bl.CreateAccessToken(request);
                return Request.ToResponse(result);
            }
        }

        /// <summary>
        /// 校验AT并得到用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/oauth/verifyToken")]
        //[ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Anymouse)]
        public ApiResponseMessage<ApiResult<LoginUserInfo>> VerifyAccessToken([FromBody]TokenArgument request)
        {
            string message = null;
            if (request == null || !request.Validate(out message))
            {
                return Request.ToResponse(ApiResult.Error<LoginUserInfo>(ErrorCode.Auth_AccessToken_TimeOut, message));
            }
            var bl = IocHelper.Create<IOAuthBusiness>();
            var result = bl.VerifyAccessToken(request);
            return Request.ToResponse(result);
        }

        /// <summary>
        /// 刷新AT
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/oauth/refreshToken")]
        //[ApiAccessOptionFilter(ApiAccessOption.Public | ApiAccessOption.Internal | ApiAccessOption.Anymouse)]
        public ApiResponseMessage<ApiResult<AccessTokenResponse>> RefreshAccessToken([FromBody]RefreshAccessTokenRequest request)
        {
            string message = null;
            if (request == null || !request.Validate(out message))
            {
                return Request.ToResponse(ApiResult.Error<AccessTokenResponse>(ErrorCode.Auth_RefreshToken_Unknow, message));
            }
            /*/拒绝异步同时请求
            var key = DataKeyBuilder.ToKey("sys", "lk", "au", "rt", request.RefreshToken);
            using (var proxy = new RedisProxy(RedisProxy.DbSystem))
            {
                if (!proxy.Client.SetEntryIfNotExists(key, "RefreshAccessToken"))
                {
                    LogRecorder.MonitorTrace("重复刷新AT");
                    return Request.ToResponse(ApiResult<AccessTokenResponse>.ErrorResult(ErrorCode.DenyAccess, message));
                }
            }*/
            var bl = IocHelper.Create<IOAuthBusiness>();
            var result = bl.RefreshAccessToken(request);
            if (result.Success)
                result.ResultData.Profile = null;
            return Request.ToResponse(result);
        }

        /// <summary>
        /// 检查调用的ServiceKey（来自内部调用）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/oauth/getdid")]
        //[ApiAccessOptionFilter(ApiAccessOption.Public | ApiAccessOption.Internal | ApiAccessOption.Anymouse)]
        public ApiResponseMessage GetDeviceIdNew([FromBody]DeviceArgument arg)
        {
            if (arg == null || string.IsNullOrWhiteSpace(arg.App))
            {
                return Request.ToResponse(ApiResult.Error(ErrorCode.LogicalError,"参数错误"));
            }
            var args = new StringBuilder();
            if (Request.Headers.UserAgent != null)
            {
                foreach (var head in Request.Headers)
                    args.Append($"【{head.Key}】{head.Value.LinkToString('|')}");
                arg.DeviceInformation = args.ToString();
                arg.Os = "O";
                foreach (var agent in Request.Headers.UserAgent)
                {
                    if (!string.IsNullOrWhiteSpace(agent.Comment))
                    {
                        var con = agent.Comment.ToUpper();
                        if (con.Contains("WINDOWS"))
                        {
                            arg.Os = "WINDOWS";
                            break;
                        }
                        else if (con.Contains("IPHONE"))
                        {
                            arg.Os = "IOS";
                            break;
                        }
                        else if (con.Contains("ANDROID"))
                        {
                            arg.Os = "ANDROID";
                            break;
                        }
                    }
                }
                if (arg.DeviceInformation.Contains("MicroMessenger"))
                    arg.Browser = "MicroMessenger";
                else if (arg.DeviceInformation.Contains("AlipayClient"))
                    arg.Browser = "AlipayClient";
                else if (string.IsNullOrWhiteSpace(arg.Browser))
                    arg.Browser = "B";
            }
            else
            {
                arg.Os = "O";
                arg.Browser = "B";
            }
            arg.Version = 2;
            var bl = IocHelper.Create<IOAuthBusiness>();
            var result = bl.GetDeviceId(arg);
            return Request.ToResponse(result);
        }
        //【User-Agent】Mozilla/5.0|(Linux; Android 7.0; HUAWEI NXT-DL00 Build/HUAWEINXT-DL00; wv)|AppleWebKit/537.36|(KHTML, like Gecko)|Version/4.0|Chrome/53.0.2785.49|Mobile|MQQBrowser/6.2|TBS/043622|Safari/537.36|MicroMessenger/6.5.22.1160|NetType/WIFI|Language/zh_CN【Origin】http://10.5.202.233:8082【X-Requested-With】

        /// <summary>
        /// 检查调用的ServiceKey（来自内部调用）
        /// </summary>
        /// <param name="request">令牌</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/oauth/sk")]
        //[ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Anymouse)]
        public ApiResponseMessage ValidateServiceKey([FromBody]TokenArgument request)
        {
            var bl = IocHelper.Create<IBearValidater>();
            var result = bl.ValidateServiceKey(request?.Token);
            return Request.ToResponse(result);
        }

        /// <summary>
        /// 检查设备标识（来自未登录用户）
        /// </summary>
        /// <param name="request">令牌</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/oauth/did")]
        //[ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Anymouse)]
        public ApiResponseMessage<ApiResult<LoginUserInfo>> ValidateDeviceId([FromBody]TokenArgument request)
        {
            if (string.IsNullOrWhiteSpace(request.Token) || request.Token.Contains('.'))
                return Request.ToResponse(ApiResult.Error<LoginUserInfo>(ErrorCode.Auth_Device_Unknow));
            var bl = IocHelper.Create<IBearValidater>();
            var result = bl.ValidateDeviceId(request.Token);
            return Request.ToResponse(result);
        }

        /// <summary>
        /// 检查设备标识（来自未登录用户）
        /// </summary>
        /// <param name="request">用户ID</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/oauth/user")]
        //[ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Anymouse)]
        public ApiResponseMessage<ApiResult<LoginUserInfo>> GetLoginUser([FromBody]Argument<string> request)
        {
            var bl = IocHelper.Create<IBearValidater>();
            var result = bl.GetLoginUser(request.Value);
            return Request.ToResponse(result);
        }
    }
}
