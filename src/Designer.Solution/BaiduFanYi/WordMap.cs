using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using Agebull.EntityModel.Config;
using Newtonsoft.Json;

namespace Agebull.Common
{
    /// <summary>
    /// 数据字典
    /// </summary>
    public class WordMap
    {
        public static readonly Dictionary<string, MapItem> Maps = new Dictionary<string, MapItem>(StringComparer.OrdinalIgnoreCase);

        public static void Prepare()
        {
            try
            {
                var path = Path.GetDirectoryName(Path.GetDirectoryName(typeof(EntityConfig).Assembly.Location));
                var file = Path.Combine(path, "Config", "words.json");
                if (!File.Exists(file))
                    return;
                var json = File.ReadAllText(file);
                var items = JsonConvert.DeserializeObject<List<MapItem>>(json);
                foreach(var item in items)
                {
                    if (string.IsNullOrWhiteSpace(item.English) || string.IsNullOrWhiteSpace(item.Chiness))
                        continue;
                    if (!Maps.ContainsKey(item.English))
                        Maps.Add(item.English, item);
                    else Maps[item.English] = item;
                    if (!Maps.ContainsKey(item.Chiness))
                        Maps.Add(item.Chiness, item);
                    else Maps[item.Chiness] = item;
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
        }
        //public MapItem this[string key]
        //{
        //    get
        //    {
        //        maps.TryGetValue(key, out var item);
        //        return item;
        //    }
        //}
    }

    /// <summary>
    /// 数据字典
    /// </summary>
    [JsonObject]
    public class MapItem
    {
        /// <summary>
        /// 英文
        /// </summary>
        [JsonProperty("en")]
        public string English { get; set; }
        /// <summary>
        /// 中文
        /// </summary>
        [JsonProperty("cn")]
        public string Chiness { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        [JsonProperty("des")]
        public string Description { get; set; }
    }
}
