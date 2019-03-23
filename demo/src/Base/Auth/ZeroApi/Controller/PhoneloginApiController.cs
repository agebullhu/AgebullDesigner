/*design by:agebull designer date:2017/11/15 15:34:41*/

using System.ComponentModel;
using Agebull.Common.Ioc;
using Agebull.Common.OAuth;
using Agebull.Common.Organizations;
using Agebull.MicroZero;

using Agebull.MicroZero.ZeroApis;

namespace Agebull.UserCenter.WebApi
{
    /// <summary>
    /// Phone
    /// </summary>
    [Category("Phone")]
    public class PhoneloginApiController : ApiController
    {

        /// <summary>
        /// 检查手机是否注册
        /// </summary>
        /// <param name="arg">手机号码</param>
        /// <returns>操作结果</returns>
        [Route("v1/check/phone")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public ApiResult MobileCheck(Argument arg)
        {
            var lg = IocHelper.Create<IUserInfoApi>();
            var result = lg.MobileCheck(arg);
            return result;
        }



        ///// <summary>
        ///// 手机号验证
        ///// </summary>
        ///// <param name="argument">MobileInfoRequest</param>
        ///// <returns></returns>
        //[Route("v1/account/valiadatephone")]
        //[ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        //public ApiResult ValiadateMobile(MobilePhoneRequest argument)
        //{
        //    var lg = IocHelper.Create<IUserInfoApi>();
        //    return lg.MobileCheck(argument);
        //}





        /// <summary>
        /// 手机短信注册
        /// </summary>
        /// <param name="arg">基于手机号的注册请求参数</param>
        /// <returns>登录返回数据</returns>
        [Route("v1/register/phone")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public ApiResult<LoginResponse> RegisterByPhone(RegByPhoneRequest arg)
        {
            var lg = IocHelper.Create<IUserInfoApi>();
            var result = lg.RegisterByPhone(arg);
            return result;
        }







        /// <summary>
        /// 短信登录(会校验短信验证码)。校验短信成功后，若用户不存在 则自动创建用户
        /// </summary>
        /// <param name="arg">短信登录参数</param>
        /// <returns>登录返回数据</returns>
        [Route("v1/login/sms")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public ApiResult<LoginResponse> LoginBySms(LoginbySmsRequest arg)
        {
            var lg = IocHelper.Create<IUserInfoApi>();
            var result = lg.LoginBySms(arg);
            return result;
        }

        ///// <summary>
        ///// 基于手机号的账号密码登录
        ///// </summary>
        ///// <param name="arg">基于手机的账号登录参数</param>
        ///// <returns>登录返回数据</returns>
        //[Route("v1/login/phone")]
        //[ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        //public ApiResult<LoginResponse> PhoneAccountLogin(PhoneLoginRequest arg)
        //{
        //    var lg = IocHelper.Create<IUserInfoApi>();
        //    var result = lg.PhoneAccountLogin(arg);
        //    return result;
        //}



        /// <summary>
        /// 清除手机号的错误登录次数
        /// </summary>
        /// <param name="data">手机号</param>
        /// <returns>如果为真并返回结果数据</returns>
        [Route("v1/phone/clearErrorCount")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal)]
        public ApiResult ClearErrorCount(Argument data)
        {
            new UserInfoApiLogical().ClearErrorCount(data.Value);
            return new ApiResult();
        }



        /// <summary>
        /// 发送手机短信验证码
        /// </summary>
        /// <param name="data">手机号</param>
        /// <returns>登录返回数据</returns>
        [Route("v1/phone/sendSms")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public )]  //| ApiAccessOption.Anymouse
        public ApiResult PhoneSendSms(Argument data)
        {
            string phone = data.Value;

            //TODO: 
            string vc = "1234";
 
            VerificationCodeHelper.CacheSms(vc, phone);
            return new ApiResult(); 
        }

    }
}