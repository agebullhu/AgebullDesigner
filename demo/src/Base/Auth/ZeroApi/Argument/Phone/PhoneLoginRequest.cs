﻿/*design by:agebull designer date:2017/11/7 21:14:55*/

using Gboxt.Common.DataModel;

namespace Agebull.Common.OAuth
{
    sealed partial class PhoneLoginRequest : Gboxt.Common.DataModel.IApiArgument
    {
        /// <summary>
        /// 扩展校验
        /// </summary>
        /// <param name="result">结果存放处</param>
        partial void ValidateEx(ValidateResult result)
        {
            //string message;
            //if (!Validater.CheckPhoneNumber(this.MobilePhone, out message))
            //{
            //    result.Add(nameof(MobilePhone), nameof(MobilePhone), message);
            //}
            //if (!Validater.CheckPassword(this.UserPassword, out message))
            //{
            //    result.Add(nameof(UserPassword), nameof(UserPassword), message);
            //}
            //if (!Validater.CheckVerificationCode(this.VerificationCode, out message))
            //{
            //    result.Add(nameof(VerificationCode), nameof(VerificationCode), message);
            //}
        }
    }
}