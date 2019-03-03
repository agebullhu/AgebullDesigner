using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace Agebull.Common.OAuth
{
    /// <summary>
    /// 密码加密算法
    /// </summary>
    public class RsaPasswordEncrypt : IEncrypt
    {
        /// <summary>
        /// 私钥
        /// </summary>
        public const string PrivateKey = @"<RSAKeyValue>
	<Modulus>ylKFYc5qU07Nzc3NEQYBNtk5Uo+yBwsCA8FEBdY9BUJ5DhV5W7i4yt0XgExSJ6M+E8mNjwtJCHWHw+ozXw5/3SJT/tqNQw5yI+fb/XKD8MPlibBRBOHevCiwmqeVpYTNuIv2bYtUGd9TkMExmCHsAcluJY/FWrY1zeKzu4jD3yE=</Modulus>
	<Exponent>AQAB</Exponent>
	<P>7HzcULvUSqSYkVZmE9T8BYJaBhg9p652tNyqrWlMsmtpI82Msb2wOynMeNnlEUoBRzbWIFQ62BLk6OX0u/BKTw==</P>
	<Q>2wQDM8hH4DAqdvmxTLw6SQQLkjGlSB/0NnqmrtOmET+AhMPJol5NLKmcl+FKsq6amdtHSvLxckRrn1ylWnGTjw==</Q>
	<DP>W/GPlrPwLbUgvCDjPwKnmVy8s3HpRrBF5ruEgDdYYmXwUsMv2ErvoZD2tmXA8Z/cL1valGcrGab+6K2/IPKjcQ==</DP>
	<DQ>RnQB+7RfurSD1IX6DO836KkOd9bUjmEJFEVcPT0EXOQKmarJwBuJw+ZD42+rsdMNFJU/V9LS7Kkp3bfV0URb7Q==</DQ>
	<InverseQ>L7fRz2KnV+RqQPA4jwvGDZi1+LCx6ETsZSlIGSVfZBOMLABYBq/nZpUpMc2rwfU5c7IVQuChK1ENDPWcQNe6PQ==</InverseQ>
	<D>c4x/muVdeii2h0RzSraoOOAEye9BWJ5jzyswozjA7uEQ7Ac9oTG1cD6m7mIoRXUIvhpNW5WxZ0BggnaeUjqgsPGFRA9J+MjkF/B0ILi9bNodtSp34F40DYHBHYgHp0eAjrH2CTVDOZbSPVidt2t42AnFjezxl6eJkQP3OofuCxU=</D>
</RSAKeyValue>";

        /// <summary>
        /// 公钥
        /// </summary>
        public const string PublicKey = @"<RSAKeyValue>
    <Modulus>ylKFYc5qU07Nzc3NEQYBNtk5Uo+yBwsCA8FEBdY9BUJ5DhV5W7i4yt0XgExSJ6M+E8mNjwtJCHWHw+ozXw5/3SJT/tqNQw5yI+fb/XKD8MPlibBRBOHevCiwmqeVpYTNuIv2bYtUGd9TkMExmCHsAcluJY/FWrY1zeKzu4jD3yE=</Modulus>
    <Exponent>AQAB</Exponent>
</RSAKeyValue>";

        ///// <summary>
        ///// 获取私钥
        ///// </summary>
        ///// <returns></returns>
        //public static string PrivateKey()
        //{
        //    return privateKey;//File.ReadAllText(Path.Combine(HttpRuntime.AppDomainAppPath, @"EncryptedFile\PrivateKey.xml"));
        //}
        ///// <summary>
        ///// 获取公钥
        ///// </summary>
        ///// <returns></returns>
        //public static string PublicKey()
        //{
        //    return _publicKey;//File.ReadAllText(Path.Combine(HttpRuntime.AppDomainAppPath, @"EncryptedFile\PublicKey.xml"));
        //}

        /// <summary>
        /// 公钥加密
        /// </summary>
        /// <param name="encryptString">要加密字符</param>
        /// <returns></returns>
        public static string RsaEncrypt(string encryptString)
        {
            if (encryptString == null)
                return null;
            var rsa = RSA.Create();
            FromXmlStringExtensions(rsa, PublicKey);
            var plainTextBArray = Encoding.Unicode.GetBytes(encryptString);
            var cypherTextBArray = rsa.Encrypt(plainTextBArray, RSAEncryptionPadding.OaepSHA1);
            return Convert.ToBase64String(cypherTextBArray);
        }
        /// <summary>
        /// 私钥解密认证
        /// </summary>
        /// <param name="decryptString">加密后字符</param>
        /// <returns></returns>
        public static string RsaDecrypt(string decryptString)
        {
            try
            {
                if (string.IsNullOrEmpty(decryptString))
                    return null;
                var rsa = RSA.Create();
                FromXmlStringExtensions(rsa, PrivateKey);
                var plainTextBArray = Convert.FromBase64String(decryptString);
                var dypherTextBArray = rsa.Decrypt(plainTextBArray, RSAEncryptionPadding.OaepSHA1);
                return Encoding.Unicode.GetString(dypherTextBArray);
            }
            catch (Exception)
            {
                return decryptString;
            }
        }

        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="str">传入的明文</param>
        /// <param name="key">密钥</param>
        /// <returns>返回密文</returns>
        private static string Encrypt(string str, string key)
        {
            var encoder = Encoding.UTF8;
            string result;
            try
            {
                result = BytesToHexString(Encrypt(encoder.GetBytes(str), encoder.GetBytes(key)));
                //result = System.Convert.ToBase64String(Encrypt(encoder.GetBytes(str), encoder.GetBytes(key)));
            }
            catch (Exception)
            {
                return null;
            }
            return result;
        }

        private static string BytesToHexString(byte[] src)
        {
            var stringBuilder = new StringBuilder("");
            if (src == null || src.Length <= 0)
            {
                return null;
            }
            foreach (var t in src)
            {
                var v = t & 0xFF;
                var hv = v.ToString("x");
                if (hv.Length < 2)
                {
                    stringBuilder.Append(0);
                }
                stringBuilder.Append(hv);
            }
            return stringBuilder.ToString();
        }

        private static readonly char[] HexDigits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'd', 'd', 'e', 'f' };
        internal static string ToHexString(byte[] bytes)
        {
            var chars = new char[bytes.Length * 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                int b = bytes[i];
                chars[i * 2] = HexDigits[b >> 4];
                chars[i * 2 + 1] = HexDigits[b & 0xF];
            }
            return new string(chars);
        }

        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="str">传入的加密字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>返回明文</returns>
        private static string Decrypt(string str, string key)
        {
            var encoder = Encoding.UTF8;
            string result;
            try
            {
                result = encoder.GetString(Decrypt(
                    StrToToHexByte(str),
                    //System.Convert.FromBase64String(str),
                    encoder.GetBytes(key)));
            }
            catch (Exception)
            {
                return null;
            }
            return result;
        }
        private static byte[] StrToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if (hexString.Length % 2 != 0)
                hexString += " ";
            var returnBytes = new byte[hexString.Length / 2];
            for (var i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        private static byte[] Encrypt(byte[] data, byte[] key)
        {
            if (data.Length == 0)
            {
                return data;
            }
            return ToByteArray(Encrypt(ToUInt32Array(data, true), ToUInt32Array(key, false)), false);
        }
        private static byte[] Decrypt(byte[] data, byte[] key)
        {
            if (data.Length == 0)
            {
                return data;
            }
            return ToByteArray(Decrypt(ToUInt32Array(data, false), ToUInt32Array(key, false)), true);
        }
        private static uint[] Encrypt(uint[] v, uint[] k)
        {
            var n = v.Length - 1;
            if (n < 1)
            {
                return v;
            }
            if (k.Length < 4)
            {
                var key = new uint[4];
                k.CopyTo(key, 0);
                k = key;
            }

            var z = v[n];
            const uint delta = 0x9E3779B9;
            uint sum = 0;
            int q = 6 + 52 / (n + 1);
            while (q-- > 0)
            {
                sum = unchecked(sum + delta);
                var e = sum >> 2 & 3;
                int p;
                uint y;
                for (p = 0; p < n; p++)
                {
                    y = v[p + 1];
                    z = unchecked(v[p] += (z >> 5 ^ y << 2) + (y >> 3 ^ z << 4) ^ (sum ^ y) + (k[p & 3 ^ e] ^ z));
                }
                y = v[0];
                z = unchecked(v[n] += (z >> 5 ^ y << 2) + (y >> 3 ^ z << 4) ^ (sum ^ y) + (k[p & 3 ^ e] ^ z));
            }
            return v;
        }
        private static uint[] Decrypt(uint[] v, uint[] k)
        {
            var n = v.Length - 1;
            if (n < 1)
            {
                return v;
            }
            if (k.Length < 4)
            {
                var key = new uint[4];
                k.CopyTo(key, 0);
                k = key;
            }

            uint y = v[0];
            const uint delta = 0x9E3779B9;
            int q = 6 + 52 / (n + 1);
            var sum = unchecked((uint)(q * delta));
            while (sum != 0)
            {
                var e = sum >> 2 & 3;
                int p;
                uint z;
                for (p = n; p > 0; p--)
                {
                    z = v[p - 1];
                    y = unchecked(v[p] -= (z >> 5 ^ y << 2) + (y >> 3 ^ z << 4) ^ (sum ^ y) + (k[p & 3 ^ e] ^ z));
                }
                z = v[n];
                y = unchecked(v[0] -= (z >> 5 ^ y << 2) + (y >> 3 ^ z << 4) ^ (sum ^ y) + (k[p & 3 ^ e] ^ z));
                sum = unchecked(sum - delta);
            }
            return v;
        }
        private static uint[] ToUInt32Array(byte[] data, bool includeLength)
        {
            var n = (data.Length & 3) == 0 ? data.Length >> 2 : (data.Length >> 2) + 1;
            uint[] result;
            if (includeLength)
            {
                result = new uint[n + 1];
                result[n] = (uint)data.Length;
            }
            else
            {
                result = new uint[n];
            }
            n = data.Length;
            for (var i = 0; i < n; i++)
            {
                result[i >> 2] |= (uint)data[i] << ((i & 3) << 3);
            }
            return result;
        }
        private static byte[] ToByteArray(uint[] data, bool includeLength)
        {
            int n;
            if (includeLength)
            {
                n = (int)data[data.Length - 1];
            }
            else
            {
                n = data.Length << 2;
            }
            var result = new byte[n];
            for (var i = 0; i < n; i++)
            {
                result[i] = (byte)(data[i >> 2] >> ((i & 3) << 3));
            }
            return result;
        }
        // 处理 下面两种方式都会出现的 Operation is not supported on this platform 异常
        // RSA.Create().FromXmlString(privateKey) 
        // new RSACryptoServiceProvider().FromXmlString(privateKey) 
        /// <summary>
        /// 扩展FromXmlString
        /// </summary>
        /// <param name="rsa"></param>
        /// <param name="xmlString"></param>
        private static void FromXmlStringExtensions(RSA rsa, string xmlString)
        {
            var parameters = new RSAParameters();

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            if (xmlDoc.DocumentElement != null && xmlDoc.DocumentElement.Name.Equals("RSAKeyValue"))
            {
                foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "Modulus":
                            parameters.Modulus = string.IsNullOrEmpty(node.InnerText)
                                ? null
                                : Convert.FromBase64String(node.InnerText);
                            break;
                        case "Exponent":
                            parameters.Exponent = string.IsNullOrEmpty(node.InnerText)
                                ? null
                                : Convert.FromBase64String(node.InnerText);
                            break;
                        case "P":
                            parameters.P = string.IsNullOrEmpty(node.InnerText)
                                ? null
                                : Convert.FromBase64String(node.InnerText);
                            break;
                        case "Q":
                            parameters.Q = string.IsNullOrEmpty(node.InnerText)
                                ? null
                                : Convert.FromBase64String(node.InnerText);
                            break;
                        case "DP":
                            parameters.DP = string.IsNullOrEmpty(node.InnerText)
                                ? null
                                : Convert.FromBase64String(node.InnerText);
                            break;
                        case "DQ":
                            parameters.DQ = string.IsNullOrEmpty(node.InnerText)
                                ? null
                                : Convert.FromBase64String(node.InnerText);
                            break;
                        case "InverseQ":
                            parameters.InverseQ = string.IsNullOrEmpty(node.InnerText)
                                ? null
                                : Convert.FromBase64String(node.InnerText);
                            break;
                        case "D":
                            parameters.D = string.IsNullOrEmpty(node.InnerText)
                                ? null
                                : Convert.FromBase64String(node.InnerText);
                            break;
                    }
                }
            }
            else
            {
                throw new Exception("Invalid XML RSA key.");
            }

            rsa.ImportParameters(parameters);
        }

        string IEncrypt.Encrypt(string encryptString)
        {
            return RsaEncrypt(encryptString);
        }

        string IEncrypt.Decrypt(string decryptString)
        {
            return RsaDecrypt(decryptString);
        }
    }

    /// <summary>
    /// RSA加解密 使用OpenSSL的公钥加密/私钥解密
    /// 作者：李志强
    /// 创建时间：2017年10月30日15:50:14
    /// QQ:501232752
    /// </summary>
    internal class RsaHelper
    {
        private readonly RSA _privateKeyRsaProvider;
        private readonly RSA _publicKeyRsaProvider;
        private readonly HashAlgorithmName _hashAlgorithmName;
        private readonly Encoding _encoding;

        /// <summary>
        /// 实例化RSAHelper
        /// </summary>
        /// <param name="rsaType">加密算法类型 RSA SHA1;RSA2 SHA256 密钥长度至少为2048</param>
        /// <param name="encoding">编码类型</param>
        /// <param name="privateKey">私钥</param>
        /// <param name="publicKey">公钥</param>
        public RsaHelper(RsaType rsaType, Encoding encoding, string privateKey, string publicKey = null)
        {
            _encoding = encoding;
            if (!string.IsNullOrEmpty(privateKey))
            {
                _privateKeyRsaProvider = CreateRsaProviderFromPrivateKey(privateKey);
            }

            if (!string.IsNullOrEmpty(publicKey))
            {
                _publicKeyRsaProvider = CreateRsaProviderFromPublicKey(publicKey);
            }

            _hashAlgorithmName = rsaType == RsaType.Rsa ? HashAlgorithmName.SHA1 : HashAlgorithmName.SHA256;
        }

        #region 使用私钥签名

        /// <summary>
        /// 使用私钥签名
        /// </summary>
        /// <param name="data">原始数据</param>
        /// <returns></returns>
        public string Sign(string data)
        {
            byte[] dataBytes = _encoding.GetBytes(data);

            var signatureBytes = _privateKeyRsaProvider.SignData(dataBytes, _hashAlgorithmName, RSASignaturePadding.Pkcs1);

            return Convert.ToBase64String(signatureBytes);
        }

        #endregion

        #region 使用公钥验证签名

        /// <summary>
        /// 使用公钥验证签名
        /// </summary>
        /// <param name="data">原始数据</param>
        /// <param name="sign">签名</param>
        /// <returns></returns>
        public bool Verify(string data, string sign)
        {
            byte[] dataBytes = _encoding.GetBytes(data);
            byte[] signBytes = Convert.FromBase64String(sign);

            var verify = _publicKeyRsaProvider.VerifyData(dataBytes, signBytes, _hashAlgorithmName, RSASignaturePadding.Pkcs1);

            return verify;
        }

        #endregion

        #region 解密

        public string Decrypt(string cipherText)
        {
            if (_privateKeyRsaProvider == null)
            {
                throw new Exception("_privateKeyRsaProvider is null");
            }
            return Encoding.UTF8.GetString(_privateKeyRsaProvider.Decrypt(Convert.FromBase64String(cipherText), RSAEncryptionPadding.Pkcs1));
        }

        #endregion

        #region 加密

        public string Encrypt(string text)
        {
            if (_publicKeyRsaProvider == null)
            {
                throw new Exception("_publicKeyRsaProvider is null");
            }
            return Convert.ToBase64String(_publicKeyRsaProvider.Encrypt(Encoding.UTF8.GetBytes(text), RSAEncryptionPadding.Pkcs1));
        }

        #endregion

        #region 使用私钥创建RSA实例

        public RSA CreateRsaProviderFromPrivateKey(string privateKey)
        {
            var privateKeyBits = Convert.FromBase64String(privateKey);

            var rsa = RSA.Create();
            var rsaParameters = new RSAParameters();

            using (BinaryReader binr = new BinaryReader(new MemoryStream(privateKeyBits)))
            {
                var twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130)
                    binr.ReadByte();
                else if (twobytes == 0x8230)
                    binr.ReadInt16();
                else
                    throw new Exception("Unexpected value read binr.ReadUInt16()");

                twobytes = binr.ReadUInt16();
                if (twobytes != 0x0102)
                    throw new Exception("Unexpected version");

                var bt = binr.ReadByte();
                if (bt != 0x00)
                    throw new Exception("Unexpected value read binr.ReadByte()");

                rsaParameters.Modulus = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.Exponent = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.D = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.P = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.Q = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.DP = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.DQ = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.InverseQ = binr.ReadBytes(GetIntegerSize(binr));
            }

            rsa.ImportParameters(rsaParameters);
            return rsa;
        }

        #endregion

        #region 使用公钥创建RSA实例

        public RSA CreateRsaProviderFromPublicKey(string publicKeyString)
        {
            // encoded OID sequence for  PKCS #1 rsaEncryption szOID_RSA_RSA = "1.2.840.113549.1.1.1"
            byte[] seqOid = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };

            var x509Key = Convert.FromBase64String(publicKeyString);

            // ---------  Set up stream to read the asn.1 encoded SubjectPublicKeyInfo blob  ------
            using (MemoryStream mem = new MemoryStream(x509Key))
            {
                using (BinaryReader binr = new BinaryReader(mem))  //wrap Memory Stream with BinaryReader for easy reading
                {
                    var twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                        binr.ReadByte();    //advance 1 byte
                    else if (twobytes == 0x8230)
                        binr.ReadInt16();   //advance 2 bytes
                    else
                        return null;

                    var seq = binr.ReadBytes(15);
                    if (!CompareBytearrays(seq, seqOid))    //make sure Sequence for OID is correct
                        return null;

                    twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8103) //data read as little endian order (actual data order for Bit String is 03 81)
                        binr.ReadByte();    //advance 1 byte
                    else if (twobytes == 0x8203)
                        binr.ReadInt16();   //advance 2 bytes
                    else
                        return null;

                    var bt = binr.ReadByte();
                    if (bt != 0x00)     //expect null byte next
                        return null;

                    twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                        binr.ReadByte();    //advance 1 byte
                    else if (twobytes == 0x8230)
                        binr.ReadInt16();   //advance 2 bytes
                    else
                        return null;

                    twobytes = binr.ReadUInt16();
                    byte lowbyte;
                    byte highbyte = 0x00;

                    if (twobytes == 0x8102) //data read as little endian order (actual data order for Integer is 02 81)
                        lowbyte = binr.ReadByte();  // read next bytes which is bytes in modulus
                    else if (twobytes == 0x8202)
                    {
                        highbyte = binr.ReadByte(); //advance 2 bytes
                        lowbyte = binr.ReadByte();
                    }
                    else
                        return null;
                    byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };   //reverse byte order since asn.1 key uses big endian order
                    int modsize = BitConverter.ToInt32(modint, 0);

                    int firstbyte = binr.PeekChar();
                    if (firstbyte == 0x00)
                    {   //if first byte (highest order) of modulus is zero, don't include it
                        binr.ReadByte();    //skip this null byte
                        modsize -= 1;   //reduce modulus buffer size by 1
                    }

                    byte[] modulus = binr.ReadBytes(modsize);   //read the modulus bytes

                    if (binr.ReadByte() != 0x02)            //expect an Integer for the exponent data
                        return null;
                    int expbytes = binr.ReadByte();        // should only need one byte for actual exponent data (for all useful values)
                    byte[] exponent = binr.ReadBytes(expbytes);

                    // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                    var rsa = RSA.Create();
                    RSAParameters rsaKeyInfo = new RSAParameters
                    {
                        Modulus = modulus,
                        Exponent = exponent
                    };
                    rsa.ImportParameters(rsaKeyInfo);

                    return rsa;
                }

            }
        }

        #endregion

        #region 导入密钥算法

        private int GetIntegerSize(BinaryReader binr)
        {
            int count;
            var bt = binr.ReadByte();
            if (bt != 0x02)
                return 0;
            bt = binr.ReadByte();

            if (bt == 0x81)
                count = binr.ReadByte();
            else
            if (bt == 0x82)
            {
                var highbyte = binr.ReadByte();
                var lowbyte = binr.ReadByte();
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                count = BitConverter.ToInt32(modint, 0);
            }
            else
            {
                count = bt;
            }

            while (binr.ReadByte() == 0x00)
            {
                count -= 1;
            }
            binr.BaseStream.Seek(-1, SeekOrigin.Current);
            return count;
        }

        private static bool CompareBytearrays(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;
            int i = 0;
            foreach (byte c in a)
            {
                if (c != b[i])
                    return false;
                i++;
            }
            return true;
        }

        #endregion

    }

    /// <summary>
    /// RSA算法类型
    /// </summary>
    public enum RsaType
    {
        /// <summary>
        /// SHA1
        /// </summary>
        Rsa = 0,
        /// <summary>
        /// RSA2 密钥长度至少为2048
        /// SHA256
        /// </summary>
        Rsa2
    }
}
