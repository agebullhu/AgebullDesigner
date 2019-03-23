
using Agebull.EntityModel.Common;
using Agebull.MicroZero.ZeroApis;
using System.Linq;



namespace Agebull.Common.Organizations
{
    /// <summary>
    /// 用户信息API
    /// </summary>
    public partial class UserInfoApiLogical : IUserInfoApi
    {


        #region Phone


        /// <summary>
        /// 检查手机是否注册
        /// </summary>
        /// <param name="arg">手机号请求参数</param>
        /// <returns>操作结果</returns>
        ApiResult IUserInfoApi.MobileCheck(Argument arg)
        {
            //var vr = arg.Validate();
            //if (!vr.succeed)
            //    return ApiResult.Error(ErrorCode.LogicalError, vr.ToString());
            var result = new ApiResult();
            MobileCheck(arg, result);
            return result;
        }

        partial void MobileCheck(Argument arg, ApiResult result);


        /// <summary>
        /// 手机短信注册
        /// </summary>
        /// <param name="arg">基于手机号的注册请求参数</param>
        /// <returns>登录返回数据</returns>
        ApiResult<LoginResponse> IUserInfoApi.RegisterByPhone(RegByPhoneRequest arg)
        {
            var vr = arg.Validate();
            if (!vr.succeed)
                return ApiResult<LoginResponse>.ErrorResult(ErrorCode.LogicalError, vr.ToString());
            var result = new ApiResult<LoginResponse>();
            RegisteByPhone(arg, result);
            return result;
        }

        partial void RegisteByPhone(RegByPhoneRequest arg, ApiResult<LoginResponse> result);







        /// <summary>
        /// 短信登录(会校验短信验证码)。校验短信成功后，若用户不存在 则自动创建用户
        /// </summary>
        /// <param name="arg">短信登录参数</param>
        /// <returns>登录返回数据</returns>
        ApiResult<LoginResponse> IUserInfoApi.LoginBySms(LoginbySmsRequest arg)
        {
            var vr = arg.Validate();
            if (!vr.succeed)
                return ApiResult<LoginResponse>.ErrorResult(ErrorCode.LogicalError, vr.ToString());
            var result = new ApiResult<LoginResponse>();
            LoginBySms(arg, result);
            return result;
        }

        partial void LoginBySms(LoginbySmsRequest arg, ApiResult<LoginResponse> result);



        /// <summary>
        /// 基于手机号的账号密码登录
        /// </summary>
        /// <param name="arg">基于手机的账号登录参数</param>
        /// <returns>登录返回数据</returns>
        ApiResult<LoginResponse> IUserInfoApi.PhoneAccountLogin(PhoneLoginRequest arg)
        {
            var vr = arg.Validate();
            if (!vr.succeed)
                return ApiResult<LoginResponse>.ErrorResult(ErrorCode.LogicalError, vr.ToString());
            var result = new ApiResult<LoginResponse>();
            PhoneAccountLogin(arg, result);
            return result;
        }

        partial void PhoneAccountLogin(PhoneLoginRequest arg, ApiResult<LoginResponse> result);


        #endregion







        /// <summary>
        ///     账户登录:账户登录:
        /// </summary>
        /// <param name="arg">基于手机的账号登录参数</param>
        /// <returns>登录返回数据</returns>
        ApiResult<LoginResponse> IUserInfoApi.AccountLogin(PhoneLoginRequest arg)
        {
            var vr = arg.Validate();
            if (!vr.succeed)
                return ApiResult<LoginResponse>.ErrorResult(ErrorCode.LogicalError, vr.ToString());
            var result = new ApiResult<LoginResponse>();
            AccountLogin(arg,result);
            return result;
        }

        partial void AccountLogin(PhoneLoginRequest arg,ApiResult<LoginResponse> result);



        /// <summary>
        ///     通过手机验证码 重置密码
        /// </summary>
        /// <param name="arg">找回密码请求参数</param>
        /// <returns>操作结果</returns>
        ApiResult IUserInfoApi.FindPassword(FindPasswordRequest arg)
        {
            var vr = arg.Validate();
            if (!vr.succeed)
                return ApiResult.Error(ErrorCode.LogicalError, vr.ToString());
            var result = new ApiResult();
            FindPassword(arg,result);
            return result;
        }

        partial void FindPassword(FindPasswordRequest arg,ApiResult result);
        
        /// <summary>
        ///     错误登录次数:错误登录次数:
        /// </summary>
        /// <param name="arg">手机号请求参数</param>
        /// <returns>错误登录次数返回值</returns>
        ApiResult<LoginErrorCountResponse> IUserInfoApi.LoginErrorCount(MobilePhoneRequest arg)
        {
            var vr = arg.Validate();
            if (!vr.succeed)
                return ApiResult<LoginErrorCountResponse>.ErrorResult(ErrorCode.LogicalError, vr.ToString());
            var result = new ApiResult<LoginErrorCountResponse>();
            LoginErrorCount(arg,result);
            return result;
        }

        partial void LoginErrorCount(MobilePhoneRequest arg,ApiResult<LoginErrorCountResponse> result);





        
        /// <summary>
        ///     查询用户信息:查询用户信息:
        /// </summary>
        /// <param name="arg">用户基本信息</param>
        /// <returns>用户基本信息</returns>
        ApiResult<UserBaseInfo> IUserInfoApi.QueryUserInfo(UserBaseInfo arg)
        {
            var result = new ApiResult<UserBaseInfo>();
            QueryUserInfo(arg,result);
            return result;
        }

        partial void QueryUserInfo(UserBaseInfo arg,ApiResult<UserBaseInfo> result);
        
        /// <summary>
        ///     修改头像:修改头像:
        /// </summary>
        /// <param name="arg">修改头像参数</param>
        /// <returns>操作结果</returns>
        ApiResult IUserInfoApi.UpdateAvatar(AvatarRequest arg)
        {
            var vr = arg.Validate();
            if (!vr.succeed)
                return ApiResult.Error(ErrorCode.LogicalError, vr.ToString());
            var result = new ApiResult();
            UpdateAvatar(arg,result);
            return result;
        }

        partial void UpdateAvatar(AvatarRequest arg,ApiResult result);
        /// <summary>
        ///     修改昵称:修改昵称:
        /// </summary>
        /// <param name="arg">修改昵称参数</param>
        /// <returns>操作结果</returns>
        ApiResult IUserInfoApi.UpdateNickName(NickNameRequest arg)
        {
            var vr = arg.Validate();
            if (!vr.succeed)
                return ApiResult.Error(ErrorCode.LogicalError, vr.ToString());
            var result = new ApiResult();
            UpdateNickName(arg,result);
            return result;
        }

        partial void UpdateNickName(NickNameRequest arg,ApiResult result);
        /// <summary>
        ///     修改密码:修改密码:
        /// </summary>
        /// <param name="arg">修改密码参数</param>
        /// <returns>操作结果</returns>
        ApiResult IUserInfoApi.UpdatePassword(UpdatePasswordRequest arg)
        {
            var vr = arg.Validate();
            if (!vr.succeed)
                return ApiResult.Error(ErrorCode.LogicalError, vr.ToString());
            var result = new ApiResult();
            UpdatePassword(arg,result);
            return result;
        }

        partial void UpdatePassword(UpdatePasswordRequest arg,ApiResult result);


    }
}