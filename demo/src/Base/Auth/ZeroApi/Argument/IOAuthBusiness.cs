using Agebull.MicroZero.ZeroApis;

namespace Agebull.Common.OAuth
{
    /// <summary>
    /// 用户认证接口
    /// </summary>
    public interface IOAuthBusiness
    {
        /// <summary>
        /// 更新AccessToken
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ApiResult<AccessTokenResponse> RefreshAccessToken(RefreshAccessTokenRequest request);

        /// <summary>
        /// 校验AccessToken
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ApiResult<LoginUserInfo> VerifyAccessToken(TokenArgument request);

        /// <summary>
        /// 取得设备识别号
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        ApiValueResult GetDeviceId(DeviceArgument arg);

        /// <summary>
        /// 生成AccessToken
        /// </summary>
        ApiResult<AccessTokenResponse> CreateAccessToken(UserData user, string account, string type, string token);

        /// <summary>检查调用的ServiceKey（来自内部调用）</summary>
        /// <param name="token">令牌</param>
        /// <returns></returns>
        ApiResult ValidateServiceKey(string token);

        /// <summary>检查AT(来自登录用户)</summary>
        /// <param name="token"></param>
        /// <returns></returns>
        ApiResult<LoginUserInfo> VerifyAccessToken(string token);

        /// <summary>检查设备标识（来自未登录用户）</summary>
        /// <param name="token">令牌</param>
        /// <returns></returns>
        ApiResult<LoginUserInfo> ValidateDeviceId(string token);

        /// <summary>检查设备标识（来自未登录用户）</summary>
        /// <param name="uid">用户ID</param>
        /// <returns></returns>
        ApiResult<LoginUserInfo> GetUserProfile(long uid);

        /// <summary>取得用户信息</summary>
        /// <param name="token">令牌</param>
        /// <returns>用户信息</returns>
        ApiResult<LoginUserInfo> GetLoginUser(string token);
    }
}