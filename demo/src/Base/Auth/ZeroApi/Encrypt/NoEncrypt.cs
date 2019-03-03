namespace Agebull.Common.OAuth
{
    /// <summary>
    /// 原字符串返回 无加密解密
    /// </summary>
    public class NoEncrypt : IEncrypt
    {

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="encryptString">待加密字符</param>
        /// <returns></returns>
        public string Encrypt(string encryptString)
        {
            return encryptString;
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="decryptString">待解密字符</param>
        /// <returns></returns>
        public string Decrypt(string decryptString)
        {
            return decryptString;
        }
    }
}
