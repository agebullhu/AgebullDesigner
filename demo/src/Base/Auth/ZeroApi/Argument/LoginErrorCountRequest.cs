/*design by:agebull designer date:2017/11/13 21:07:31*/
using Gboxt.Common.DataModel;
using Agebull.Common.OAuth;

namespace Agebull.Common.UserCenter.WebApi
{
    sealed partial class LoginErrorCountRequest
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
        }
    }
}