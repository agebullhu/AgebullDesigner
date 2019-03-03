/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2017/11/15 15:34:41*/
using System;
using System.Web.Http;
using Agebull.Common.Logging;
using Gboxt.Common.DataModel;

namespace Agebull.Common.WebApi.Auth.WebApi
{
    /// <summary>
    /// 身份验证服务API
    /// </summary>
    public class UserInfoApiController : ApiController
    {
        /// <summary>
        ///     账户登录:账户登录:
        /// </summary>
        /// <param name="arg">基于手机的账号登录参数</param>
        /// <returns>登录返回数据</returns>
        [HttpPost, Route("v2/login/account")]
        //[ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public ApiResponseMessage<ApiResult<LoginResponse>> AccountLogin([FromBody]PhoneLoginRequest arg)
        {
            
            try
            {
                var lg = new UserInfoApiLogical() as IUserInfoApi;
                var result = lg.AccountLogin(arg);
                return Request.ToResponse(result);
            }
            catch (Exception ex)
            {
                LogRecorder.Exception(ex);
                return Request.ToResponse(ApiResult<LoginResponse>.ErrorResult(ErrorCode.LocalException));
            }
        }
        /// <summary>
        ///     活动注册:活动注册:
        /// </summary>
        /// <param name="arg">基于活动的注册</param>
        /// <returns>活动注册返回值</returns>
        [HttpPost, Route("v2/registe/activity")]
        //[ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public ApiResponseMessage<ApiResult<ActivityRegisteResponse>> ActivityRegiste([FromBody]ActivityRegisteRequest arg)
        {
            var lg = new UserInfoApiLogical() as IUserInfoApi;
            var result = lg.ActivityRegiste(arg);
            return Request.ToResponse(result);
        }
        /// <summary>
        ///     检查微信账号:检查微信账号:
        /// </summary>
        /// <param name="arg">微信认证</param>
        /// <returns>微信认证</returns>
        [HttpPost, Route("v2/check/wechat")]
        //[ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public ApiResponseMessage<ApiResult<Wechat>> CheckWechat([FromBody]Wechat arg)
        {
            var lg = new UserInfoApiLogical() as IUserInfoApi;
            var result = lg.CheckWechat(arg);
            return Request.ToResponse(result);
        }
        /// <summary>
        ///     找回密码:找回密码:
        /// </summary>
        /// <param name="arg">找回密码请求参数</param>
        /// <returns>操作结果</returns>
        [HttpPost, Route("v2/user/findpassword")]
        //[ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public ApiResponseMessage FindPassword([FromBody]FindPasswordRequest arg)
        {
            var lg = new UserInfoApiLogical() as IUserInfoApi;
            var result = lg.FindPassword(arg);
            return Request.ToResponse(result);
        }
        /// <summary>
        ///     短信登录:短信登录:
        /// </summary>
        /// <param name="arg">短信登录参数</param>
        /// <returns>登录返回数据</returns>
        [HttpPost, Route("v2/login/sms")]
        //[ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public ApiResponseMessage<ApiResult<LoginResponse>> LoginBySms([FromBody]LoginbySmsRequest arg)
        {
            var lg = new UserInfoApiLogical() as IUserInfoApi;
            var result = lg.LoginBySms(arg);
            return Request.ToResponse(result);
        }
        /// <summary>
        ///     错误登录次数:错误登录次数:
        /// </summary>
        /// <param name="arg">手机号请求参数</param>
        /// <returns>错误登录次数返回值</returns>
        [HttpPost, Route("v2/login/errorcount")]
        //[ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public ApiResponseMessage<ApiResult<LoginErrorCountResponse>> LoginErrorCount([FromBody]MobilePhoneRequest arg)
        {
            var lg = new UserInfoApiLogical() as IUserInfoApi;
            var result = lg.LoginErrorCount(arg);
            return Request.ToResponse(result);
        }
        /// <summary>
        ///     检查手机是否注册:检查手机是否注册:
        /// </summary>
        /// <param name="arg">手机号请求参数</param>
        /// <returns>操作结果</returns>
        [HttpPost, Route("v2/check/phone")]
        //[ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public ApiResponseMessage MobileCheck([FromBody]MobilePhoneRequest arg)
        {
            var lg = new UserInfoApiLogical() as IUserInfoApi;
            var result = lg.MobileCheck(arg);
            return Request.ToResponse(result);
        }
        /// <summary>
        ///     基本手机号的账号登录:基本手机号的账号登录:
        /// </summary>
        /// <param name="arg">基于手机的账号登录参数</param>
        /// <returns>登录返回数据</returns>
        [HttpPost, Route("v2/login/phone")]
        //[ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public ApiResponseMessage<ApiResult<LoginResponse>> PhoneAccountLogin([FromBody]PhoneLoginRequest arg)
        {
            var lg = new UserInfoApiLogical() as IUserInfoApi;
            var result = lg.PhoneAccountLogin(arg);
            return Request.ToResponse(result);
        }
        /// <summary>
        ///     基于手机的注册:基于手机的注册:
        /// </summary>
        /// <param name="arg">基于手机号的注册请求参数</param>
        /// <returns>登录返回数据</returns>
        [HttpPost, Route("v2/register/phone")]
        //[ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public ApiResponseMessage<ApiResult<LoginResponse>> RegisterByPhone([FromBody]RegByPhoneRequest arg)
        {
            var lg = new UserInfoApiLogical() as IUserInfoApi;
            var result = lg.RegisterByPhone(arg);
            return Request.ToResponse(result);
        }
        /// <summary>
        ///     修改头像:修改头像:
        /// </summary>
        /// <param name="arg">修改头像参数</param>
        /// <returns>操作结果</returns>
        [HttpPost, Route("v2/user/update/avatar")]
        //[ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public ApiResponseMessage UpdateAvatar([FromBody]AvatarRequest arg)
        {
            var lg = new UserInfoApiLogical() as IUserInfoApi;
            var result = lg.UpdateAvatar(arg);
            return Request.ToResponse(result);
        }
        /// <summary>
        ///     修改昵称:修改昵称:
        /// </summary>
        /// <param name="arg">修改昵称参数</param>
        /// <returns>操作结果</returns>
        [HttpPost, Route("v2/user/update/name")]
        //[ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public ApiResponseMessage UpdateNickName([FromBody]NickNameRequest arg)
        {
            var lg = new UserInfoApiLogical() as IUserInfoApi;
            var result = lg.UpdateNickName(arg);
            return Request.ToResponse(result);
        }
        /// <summary>
        ///     修改密码:修改密码:
        /// </summary>
        /// <param name="arg">修改密码参数</param>
        /// <returns>操作结果</returns>
        [HttpPost, Route("v2/user/update/password")]
        //[ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public ApiResponseMessage UpdatePassword([FromBody]UpdatePasswordRequest arg)
        {
            var lg = new UserInfoApiLogical() as IUserInfoApi;
            var result = lg.UpdatePassword(arg);
            return Request.ToResponse(result);
        }
        /// <summary>
        ///     微信登录:微信登录:
        /// </summary>
        /// <returns>登录返回数据</returns>
        [HttpPost, Route("v2/login/wechat")]
        //[ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public ApiResponseMessage<ApiResult<LoginResponse>> WeChatLogin()
        {
            var lg = new UserInfoApiLogical() as IUserInfoApi;
            var result = lg.WeChatLogin();
            return Request.ToResponse(result);
        }
        /// <summary>
        ///     微信用户注册:微信用户注册:
        /// </summary>
        /// <param name="arg">基于手机号的注册请求参数</param>
        /// <returns>登录返回数据</returns>
        [HttpPost, Route("v2/regist/wechat")]
        //[ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public ApiResponseMessage<ApiResult<LoginResponse>> WechatRigist([FromBody]RegByPhoneRequest arg)
        {
            var lg = new UserInfoApiLogical() as IUserInfoApi;
            var result = lg.WechatRigist(arg);
            return Request.ToResponse(result);
        }
        /// <summary>
        ///     查询用户信息:查询用户信息:
        /// </summary>
        /// <param name="arg">用户基本信息</param>
        /// <returns>用户基本信息</returns>
        [HttpPost, Route("v2/user/query")]
        //[ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public ApiResponseMessage<ApiResult<UserBaseInfo>> QueryUserInfo([FromBody]UserBaseInfo arg)
        {
            var lg = new UserInfoApiLogical() as IUserInfoApi;
            var result = lg.QueryUserInfo(arg);
            return Request.ToResponse(result);
        }
    }
}