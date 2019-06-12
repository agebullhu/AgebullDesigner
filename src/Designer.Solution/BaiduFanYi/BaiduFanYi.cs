﻿using Agebull.EntityModel.Config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;

namespace Agebull.Common
{
    /// <summary>
    /// 百度翻译接口类
    /// </summary>
    public class BaiduFanYi
    {
        /// <summary>
        /// 翻译中文到英文
        /// </summary>
        /// <param name="str">中文</param>
        /// <returns>英文</returns>
        public static string ToChiness(string str)
        {
            try
            {
                if (str.Length == 3 && str.Substring(1).Equals("ID", StringComparison.OrdinalIgnoreCase))
                    return "主键";
                if (WordMap.Maps.TryGetValue(str, out var mi))
                {
                    return mi.Chiness;
                }

                var w = GlobalConfig.ToWords(str);
                var words = new List<string>();
                StringBuilder sb = new StringBuilder();
                w.ForEach(p =>
                {
                    if (p.Length == 1)
                    {
                        sb.Append(p);
                    }
                    else
                    {
                        if (sb.Length > 0)
                        {
                            words.Add(sb.ToString());
                            sb = new StringBuilder();
                        }
                        words.Add(p);
                    }
                });
                if (sb.Length > 0)
                {
                    words.Add(sb.ToString());
                }
                sb = new StringBuilder();
                words.ForEach(p =>
                {
                    sb.Append(WordMap.Maps.TryGetValue(p, out var item) ? item.Chiness : p);
                    sb.Append(' ');
                });
                var txt = sb.ToString();
                foreach(var ch in txt)
                {
                    if(ch != ' ' && ch < 'z')
                        return BaiDuFanYi(sb.ToString(), false).Replace(" ", "");
                }
                return txt.Replace(" ","");
            }
            catch (Exception exception)
            {
                Trace.WriteLine($@"通过百度翻译出错:{exception }");
                return str;
            }
        }

        /// <summary>
        /// 翻译中文到英文单词(大驼峰)
        /// </summary>
        /// <param name="str">英文</param>
        /// <returns>中文</returns>
        public static string ToEnglishWord(string str)
        {
            string w = ToEnglish(str);
            if (string.IsNullOrWhiteSpace(w) || string.Equals(str, w, StringComparison.OrdinalIgnoreCase))
            {
                return str;
            }

            var words = GlobalConfig.ToWords(w);

            StringBuilder sb = new StringBuilder();
            words.ForEach(p =>
            {
                sb.Append(WordMap.Maps.TryGetValue(p, out var item) ? item.Chiness : p);
            });
            return sb.ToString();
        }

        /// <summary>
        /// 翻译中文到英文
        /// </summary>
        /// <param name="str">英文</param>
        /// <returns>中文</returns>
        public static string ToEnglish(string str)
        {
            try
            {
                if (WordMap.Maps.TryGetValue(str, out var item))
                {
                    return item.English;
                }

                var words = GlobalConfig.ToWords(str);
                StringBuilder sb = new StringBuilder();
                words.ForEach(p =>
                {
                    sb.Append(WordMap.Maps.TryGetValue(p, out var i) ? i.Chiness : p);
                    sb.Append(' ');
                });
                return BaiDuFanYi(sb.ToString(), true);
            }
            catch (Exception exception)
            {
                Trace.WriteLine($@"通过百度翻译出错:{exception }");
                return str;
            }
        }

        /// <summary>
        /// 翻译
        /// </summary>
        /// <param name="str">文字</param>
        /// <param name="fromChiness">翻译源语言</param>
        /// <returns>结果</returns>
        private static string BaiDuFanYi(string str, bool fromChiness)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return null;
            }

            str = str.Trim();
            //var app = "CodeRefactor";
            //var appid = "20161104000031344";
            //var secretKey = "E5JzhwEjhVWL9FWzjY74";
            var random = new Random((int)DateTime.Now.Ticks % short.MaxValue);
            var salt = random.Next(32768, 65536);
            var sign1 = $@"20161104000031344{str}{salt}E5JzhwEjhVWL9FWzjY74";
            byte[] buf = Encoding.UTF8.GetBytes(sign1); //tbPass为输入密码的文本框
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(buf);
            var sign = BitConverter.ToString(output).Replace("-", "").ToLower();
            string url =
                $@"http://api.fanyi.baidu.com/api/trans/vip/translate?appid=20161104000031344&from={(fromChiness ? "zh" : "auto")}&to={(!fromChiness ? "zh" : "en")}&q={str}&salt={salt}&sign={sign}";
            WebClient client = new WebClient(); //引用System.Net
            var buffer = client.DownloadData(url);
            //client_id为自己的api_id，q为翻译对象，from为翻译语言，to为翻译后语言
            string result = Encoding.UTF8.GetString(buffer);
            StringReader sr = new StringReader(result);
            JsonTextReader jsonReader = new JsonTextReader(sr); //引用Newtonsoft.Json 自带
            JsonSerializer serializer = new JsonSerializer();
            var r = serializer.Deserialize<FanYiResult>(jsonReader); //因为获取后的为json对象 ，实行转换
            if (string.IsNullOrWhiteSpace(r.ErrorCode) && r.Result != null && r.Result.Length > 0)
            {
                return r.Result[0].Dest; //dst为翻译后的值
            }

            return str;
        }

        [DataContract, JsonObject(MemberSerialization.OptIn)]
        internal class FanYiResult
        {
            /// <summary>
            /// 翻译源语言
            /// </summary>
            [JsonProperty("form")]
            public string From { get; set; }

            /// <summary>
            /// 译文语言
            /// </summary>
            [JsonProperty("to")]
            public string To { get; set; }

            /// <summary>
            /// 翻译结果
            /// </summary>
            [JsonProperty("trans_result")]
            public ResultItem[] Result { get; set; }

            /// <summary>
            /// 译文
            /// </summary>
            [JsonProperty("error_code")]
            public string ErrorCode { get; set; }

            /// <summary>
            /// 译文
            /// </summary>
            [JsonProperty("error_mmsg")]
            public string ErrorMessage { get; set; }
        }

        [DataContract, JsonObject(MemberSerialization.OptIn)]
        internal class ResultItem
        {
            /// <summary>
            /// 原文
            /// </summary>
            [JsonProperty("src")]
            public string Source { get; set; }

            /// <summary>
            /// 译文
            /// </summary>
            [JsonProperty("dst")]
            public string Dest { get; set; }
        }
    }
}
