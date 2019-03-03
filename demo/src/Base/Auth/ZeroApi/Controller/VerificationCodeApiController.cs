using Agebull.ZeroNet.ZeroApi;
using System.ComponentModel;

using Gboxt.Common.DataModel;
using Agebull.Common.OAuth;
using Agebull.Common.Rpc;

namespace FenXiang.InternetPro.UnifiedIdentity.UserCard.Model.WebApi.EntityApi
{
    /// <summary>
    /// 图片验证码 和 短信验证码
    /// </summary>
    [Category("验证码")]
    public class VerificationCodeApiController : ZeroApiController
    {

        /// <summary>
        /// 缓存图片验证码(5分钟后自动失效)
        /// </summary>
        /// <param name="data">图片验证码参数</param>
        /// <returns>如果为真并返回结果数据</returns>
        [Route("v1/verification/cacheImg")]
        [ApiAccessOptionFilter(ApiAccessOption.Public | ApiAccessOption.Internal )]
        public ApiResult CacheImg(CacheImgReq data)
        {
            var vr = data.Validate();
            if (!vr.succeed)
                return ApiResult.Error(ErrorCode.LogicalError, vr.ToString());

   
            string RpcToken = data.RpcToken;
            string VerificationCode = data.VerificationCode;

            VerificationCodeHelper.CacheImg(VerificationCode, RpcToken);
            return new ApiResult();
        }

        /// <summary>
        /// 验证图片验证码
        /// </summary>
        /// <param name="data">图片验证码参数</param>
        /// <returns>如果为真并返回结果数据</returns>
        [Route("v1/verification/validataImg")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal)]
        public ApiResult ValidateImg(CacheImgReq data)
        {
            var vr = data.Validate();
            if (!vr.succeed)
                return ApiResult.Error(ErrorCode.LogicalError, vr.ToString());


            string RpcToken = data.RpcToken;
            string VerificationCode = data.VerificationCode;

            var result = new ApiResult();
            result.Success=VerificationCodeHelper.ValidateImg(VerificationCode, RpcToken);
            return result;
        }


        /// <summary>
        /// 缓存短信验证码(5分钟后自动失效)
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns>如果为真并返回结果数据</returns>
        [Route("v1/verification/cacheSms")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal)]
        public ApiResult CacheSms(CacheSmsReq data)
        {
            var vr = data.Validate();
            if (!vr.succeed)
                return ApiResult.Error(ErrorCode.LogicalError, vr.ToString());

            string vc = data.SmsVerificationCode;
            string phone = data.Phone;
            VerificationCodeHelper.CacheSms(vc,phone);
            return new ApiResult();
        }

        /// <summary>
        /// 验证短信验证码
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns>如果为真并返回结果数据</returns>
        [Route("v1/verification/validateSms")]
        [ApiAccessOptionFilter(ApiAccessOption.Public | ApiAccessOption.Internal)]
        public ApiResult ValidateSms(CacheSmsReq data)
        {
            var vr = data.Validate();
            if (!vr.succeed)
                return ApiResult.Error(ErrorCode.LogicalError, vr.ToString());

            string vc = data.SmsVerificationCode;
            string phone = data.Phone;

            var result = new ApiResult();
            result.Success = VerificationCodeHelper.ValidateSms(vc, phone);
            return result;
        }

    }
}

