﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace Agebull.Common
{
    /// <summary>
    /// 百度翻译接口类
    /// </summary>
    public class BaiduTranslator
    {
        public static Dictionary<string, string> Cache = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 翻译
        /// </summary>
        /// <param name="str">文字</param>
        /// <param name="fromChiness">翻译源语言</param>
        /// <returns>结果</returns>
        internal static string Translate(string str, bool fromChiness)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return null;
            }
            str = str.Trim();
            if (Cache.TryGetValue(str, out var to))
            {
                return to;
            }
            Thread.Sleep(50);
            //var app = "CodeRefactor";
            //var appid = "20161104000031344";
            //var secretKey = "E5JzhwEjhVWL9FWzjY74";

            var random = new Random((int)DateTime.Now.Ticks % short.MaxValue);
            var salt = random.Next(32768, 65536);
            var sign1 = $@"20161104000031344{str}{salt}E5JzhwEjhVWL9FWzjY74";
            byte[] buf = Encoding.UTF8.GetBytes(sign1); //tbPass为输入密码的文本框
            MD5 md5 = new MD5CryptoServiceProvider();
            var output = md5.ComputeHash(buf);
            var sign = BitConverter.ToString(output).Replace("-", "").ToLower();
            var url =
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
                Cache.TryAdd(str, r.Result[0].Dest);
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
