using Agebull.MicroZero.ZeroApis;

namespace Agebull.Common.OAuth
{
    /// <summary>
    /// 身份验证服务API
    /// </summary>
    public interface IUserInfoApi
    {

        #region Mobile

        /// <summary>
        /// 检查手机是否注册
        /// </summary>
        /// <param name="arg">手机号请求参数</param>
        /// <returns>操作结果</returns>
        ApiResult MobileCheck(Argument arg);




        #endregion

        /// <summary>
        ///     账户登录
        /// </summary>
        /// <param name="arg">基于手机的账号登录参数</param>
        /// <returns>登录返回数据</returns>
        ApiResult<LoginResponse> AccountLogin(PhoneLoginRequest arg);




        /// <summary>
        ///     通过手机验证码 重置密码
        /// </summary>
        /// <param name="arg">找回密码请求参数</param>
        /// <returns>操作结果</returns>
        ApiResult FindPassword(FindPasswordRequest arg);

        /// <summary>
        ///     短信登录:短信登录:
        /// </summary>
        /// <param name="arg">短信登录参数</param>
        /// <returns>登录返回数据</returns>
        ApiResult<LoginResponse> LoginBySms(LoginbySmsRequest arg);

        /// <summary>
        ///     错误登录次数:错误登录次数:
        /// </summary>
        /// <param name="arg">手机号请求参数</param>
        /// <returns>错误登录次数返回值</returns>
        ApiResult<LoginErrorCountResponse> LoginErrorCount(MobilePhoneRequest arg);



        /// <summary>
        ///     基本手机号的账号登录:基本手机号的账号登录:
        /// </summary>
        /// <param name="arg">基于手机的账号登录参数</param>
        /// <returns>登录返回数据</returns>
        ApiResult<LoginResponse> PhoneAccountLogin(PhoneLoginRequest arg);

        /// <summary>
        ///     查询用户信息:查询用户信息:
        /// </summary>
        /// <param name="arg">用户基本信息</param>
        /// <returns>用户基本信息</returns>
        ApiResult<UserBaseInfo> QueryUserInfo(UserBaseInfo arg);

        /// <summary>
        ///  手机短信注册
        /// </summary>
        /// <param name="arg">基于手机号的注册请求参数</param>
        /// <returns>登录返回数据</returns>
        ApiResult<LoginResponse> RegisterByPhone(RegByPhoneRequest arg);

        /// <summary>
        ///     修改头像:修改头像:
        /// </summary>
        /// <param name="arg">修改头像参数</param>
        /// <returns>操作结果</returns>
        ApiResult UpdateAvatar(AvatarRequest arg);

        /// <summary>
        ///     修改昵称:修改昵称:
        /// </summary>
        /// <param name="arg">修改昵称参数</param>
        /// <returns>操作结果</returns>
        ApiResult UpdateNickName(NickNameRequest arg);

        /// <summary>
        ///     修改密码:修改密码:
        /// </summary>
        /// <param name="arg">修改密码参数</param>
        /// <returns>操作结果</returns>
        ApiResult UpdatePassword(UpdatePasswordRequest arg);

    }
}