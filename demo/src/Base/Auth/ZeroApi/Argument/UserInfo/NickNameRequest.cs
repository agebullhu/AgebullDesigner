/*design by:agebull designer date:2017/11/7 21:14:55*/


using Agebull.EntityModel.Common;
using Agebull.MicroZero.ZeroApis;

namespace Agebull.Common.Organizations
{
    sealed partial class NickNameRequest : MicroZero.ZeroApis.IApiArgument
    {
        /// <summary>
        /// 扩展校验
        /// </summary>
        /// <param name="result">结果存放处</param>
        partial void ValidateEx(ValidateResult result)
        {
            if (!Validater.CheckNikeName(this.Name, out string message))
            {
                result.Add(nameof(Name), nameof(Name), message);
            }
        }
    }
}