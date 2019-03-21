/*design by:agebull designer date:2017/11/7 21:14:55*/



using Agebull.EntityModel.Common;
using Agebull.MicroZero.ZeroApis;

namespace Agebull.Common.OAuth
{
    sealed partial class RegByPhoneRequest : MicroZero.ZeroApis.IApiArgument
    {
        /// <summary>
        /// 扩展校验
        /// </summary>
        /// <param name="result">结果存放处</param>
        partial void ValidateEx(ValidateResult result)
        {
            if (!Validater.CheckPhoneNumber(this.MobilePhone, out string message))
            {
                result.Add(nameof(MobilePhone), nameof(MobilePhone), message);
            }
            if (!Validater.CheckPassword(this.UserPassword, out message))
            {
                result.Add(nameof(UserPassword), nameof(UserPassword), message);
            }
            if (!Validater.CheckVerificationCode(this.SMSVerificationCode, out message))
            {
                result.Add(nameof(SMSVerificationCode), nameof(SMSVerificationCode), message);
            }
        }
    }
}