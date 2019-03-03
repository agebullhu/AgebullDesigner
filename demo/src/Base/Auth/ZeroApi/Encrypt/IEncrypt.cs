namespace Agebull.Common.OAuth
{
    /// <summary>
    /// 密码加密解密算法
    /// </summary>
    public interface IEncrypt
    {

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="encryptString">待加密字符</param>
        /// <returns></returns>
        string Encrypt(string encryptString);

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="decryptString">待解密字符</param>
        /// <returns></returns>
        string Decrypt(string decryptString); 
    }
}
