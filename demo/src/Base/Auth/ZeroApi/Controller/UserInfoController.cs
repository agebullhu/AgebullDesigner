using System.ComponentModel;
using Agebull.Common.Ioc;
using Agebull.Common.Organizations;
using Agebull.MicroZero;

using Agebull.MicroZero.ZeroApis;

namespace Agebull.UserCenter.WebApi
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [Category("用户信息")]
    public class UserInfoController : ApiController
    {

        /// <summary>
        /// 通过手机验证码 重置密码
        /// </summary>
        /// <param name="argument">FindPasswordRequest</param>
        /// <returns></returns>
        [Route("v1/account/findpassword")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public ApiResult FindPassword(FindPasswordRequest argument)
        {
            var lg = IocHelper.Create<IUserApi>();
            return lg.FindPassword(argument);
        }


        /// <summary>
        ///     修改头像
        /// </summary>
        /// <param name="arg">修改头像参数</param>
        /// <returns>操作结果</returns>
        [Route("v1/update/avatar")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Customer)]
        public ApiResult UpdateAvatar(AvatarRequest arg)
        {
            var lg = IocHelper.Create<IUserApi>();
            var result = lg.UpdateAvatar(arg);
            return result;
        }

        /// <summary>
        ///     修改昵称
        /// </summary>
        /// <param name="arg">修改昵称参数</param>
        /// <returns>操作结果</returns>
        [Route("v1/update/name")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Customer)]
        public ApiResult UpdateNickName(NickNameRequest arg)

        {
            var lg = IocHelper.Create<IUserApi>();
            var result = lg.UpdateNickName(arg);
            return result;
        }

        /// <summary>
        ///  已登录用户 修改密码
        /// </summary>
        /// <param name="arg">修改密码参数</param>
        /// <returns>操作结果</returns>
        [Route("v1/update/password")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Customer)]
        public ApiResult UpdatePassword(UpdatePasswordRequest arg)
        {
            var lg = IocHelper.Create<IUserApi>();
            var result = lg.UpdatePassword(arg);
            return result;
        }
    }
}