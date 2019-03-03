using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Agebull.Common.OAuth
{
    /// <summary>
    /// 数据校验
    /// </summary>
    public class Validater
    {
        public static List<string> sensitiveWords;

        /// <summary>
        /// 校验昵称
        /// </summary>
        /// <param name="nikeName">电话号码</param>
        /// <param name="message">返回消息</param>
        /// <returns>是否通过</returns>
        public static bool CheckNikeName(string nikeName, out string message)
        {
            if (string.IsNullOrWhiteSpace(nikeName))
            {
                message = "请输入4-16字符昵称";
                return false;
            }
            if (Encoding.Default.GetByteCount(nikeName) < 4)
            {
                message = "昵称长度不够";
                return false;
            }
            if (Encoding.Default.GetByteCount(nikeName) > 16)
            {
                message = "昵称太长";
                return false;
            }
            if (!Regex.IsMatch(nikeName, @"^[A-Za-z0-9\u4E00-\u9FA5]+$"))
            {
                message = "您的昵称不符合使用规范";
                return false;
            }
            if (sensitiveWords != null)
            {
                if (sensitiveWords.Any(nikeName.Equals))
                {
                    message = "您的昵称不符合使用规范";
                    return false;
                }
            }
            message = null;
            return true;
        }

        /// <summary>
        /// 校验电话号码
        /// </summary>
        /// <param name="phone">电话号码</param>
        /// <param name="message">返回消息</param>
        /// <returns>是否通过</returns>
        public static bool CheckPhoneNumber(string phone, out string message)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                message = "请输入手机号";
                return false;
            }
            if (phone.Length != 11 || !Regex.IsMatch(phone, @"^1[3-9]\d{9}$"))
            {
                message = "手机号格式不正确";
                return false;
            }
            message = null;
            return true;
        }
        ///// <summary>
        ///// 校验短信校验码
        ///// </summary>
        ///// <param name="vc">短信校验码</param>
        ///// <param name="message">返回消息</param>
        ///// <returns>是否通过</returns>
        //public static bool CheckSmsVerificationCode(string vc, out string message)
        //{
        //    if (string.IsNullOrWhiteSpace(vc))
        //    {
        //        message = "请输入正确的验证码";
        //        return false;
        //    }
        //    if (vc.Length != 4 || !Regex.IsMatch(vc, @"^\d{6}$"))
        //    {
        //        message = "请输入正确的验证码";
        //        return false;
        //    }
        //    message = null;
        //    return true;
        //}

        /// <summary>
        /// 校验短信校验码
        /// </summary>
        /// <param name="vc">短信校验码</param>
        /// <param name="message">返回消息</param>
        /// <returns>是否通过</returns>
        public static bool CheckVerificationCode(string vc, out string message)
        {
            if (string.IsNullOrWhiteSpace(vc))
            {
                message = "请输入正确的验证码";
                return true;
            }
            switch (vc.Length)
            {
                case 6:
                    if (!Regex.IsMatch(vc, @"^[0-9]{6}$"))
                    {
                        message = "请输入正确的验证码";
                        return false;
                    }
                    break;
                case 4:
                    if (!Regex.IsMatch(vc, @"^[0-9a-zA-Z]{4}$"))
                    {
                        message = "请输入正确的验证码";
                        return false;
                    }
                    break;
                default:
                    message = "请输入正确的验证码";
                    return false;
            }
            message = null;
            return true;
        }

        /// <summary>
        /// 校验密码规则
        /// </summary>
        /// <param name="pwd">密码</param>
        /// <param name="message">返回消息</param>
        /// <returns>是否通过</returns>
        public static bool CheckPassword(string pwd, out string message)
        {
            int numCount = 0;
            int letterCount = 0;
            int symbolCount = 0;

            if (string.IsNullOrEmpty(pwd))
            {
                message = "请输入密码";
                return false;
            }
            if (pwd.Length < 6 || pwd.Length > 16)
            {
                message = "密码格式不正确";
                return false;
            }
            
            foreach (char ch in pwd)
            {
                if (ch <= 33 || ch >= 127)
                {
                    message = "密码格式不正确";
                    return false;
                }
                if (ch >= '0' && ch <= '9')
                    numCount++;
                else if ((ch >= 'a' && ch <= 'z') || ch >= 'A' && ch <= 'Z')
                    letterCount++;
                else
                    symbolCount++;
            }
            int cnt = 0;
            if (numCount > 0)
            {
                cnt++;
            }
            if (letterCount > 0)
            {
                cnt++;
            }
            if (symbolCount > 0)
            {
                cnt++;
            }
            if (cnt < 2)
            {
                message = "密码格式不正确";
                return false;
            }
            message = null;
            return true;
        }
    }
}
