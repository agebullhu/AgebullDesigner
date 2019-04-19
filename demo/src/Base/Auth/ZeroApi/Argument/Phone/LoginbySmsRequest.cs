/*design by:agebull designer date:2017/11/7 21:14:55*/


using Agebull.EntityModel.Common;

namespace Agebull.Common.Organizations
{
    sealed partial class LoginbySmsRequest : MicroZero.ZeroApis.IApiArgument
    {
        /// <summary>
        /// 扩展校验
        /// </summary>
        /// <param name="result">结果存放处</param>
        partial void ValidateEx(ValidateResult result)
        {
            if (!Validater.CheckVerificationCode(this.SMSVerificationCode, out string message))
            {
                result.Add(nameof(SMSVerificationCode), nameof(SMSVerificationCode), message);
            }
            if (!Validater.CheckPhoneNumber(this.MobilePhone, out message))
            {
                result.Add(nameof(MobilePhone), nameof(MobilePhone), message);
            }
        }
    }
}