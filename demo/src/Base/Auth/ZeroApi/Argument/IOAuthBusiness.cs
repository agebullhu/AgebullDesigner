using Agebull.Common.OAuth;
using Agebull.MicroZero.ZeroApis;
using HPC.Projects;

namespace Agebull.Common.Organizations
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
        ApiResult<AccessTokenResponse> CreateAccessToken(long userId, string account, string type, string token);

        /// <summary>
        /// 生成AccessToken
        /// </summary>
        ApiResult<AccessTokenResponse> CreateAccessToken(UserData user, PersonData person, EmployeeData employee, string token);

        /// <summary>
        /// 生成AccessToken
        /// </summary>
        ApiResult<AccessTokenResponse> CreateAccessToken(UserData user, PersonData person, string token);

        /// <summary>检查设备标识（来自未登录用户）</summary>
        /// <param name="token">令牌</param>
        /// <returns></returns>
        ApiResult<LoginUserInfo> ValidateDeviceId(string token);

        /// <summary>取得用户信息</summary>
        /// <param name="value">令牌或用户ID</param>
        /// <returns>用户信息</returns>
        ApiResult<LoginUserInfo> GetLoginUser(string value);
    }
}