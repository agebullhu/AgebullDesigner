using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 扩展配置节点
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public class ConfigItemList
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="items"></param>
        public ConfigItemList(List<ConfigItem> items)
        {
            Items = items;
        }
        /// <summary>
        /// 节点（引用）
        /// </summary>
        public List<ConfigItem> Items { get;}
        /// <summary>
        /// 读写扩展配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string this[string key]
        {
            get
            {
                return string.IsNullOrWhiteSpace(key) ? null : Items.FirstOrDefault(p => p.Name == key)?.Value;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(key))
                    return;
                var mv = Items.FirstOrDefault(p => p.Name == key);
                if (mv == null)
                {
                    Items.Add(new ConfigItem { Name = key, Value = value });
                }
                else
                {
                    mv.Value = value;
                }
            }
        }
    }
    /// <summary>
    /// 扩展配置节点
    /// </summary>
    public class ConfigItemListBool
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="items"></param>
        public ConfigItemListBool(List<ConfigItem> items)
        {
            Items = items;
        }
        /// <summary>
        /// 节点（引用）
        /// </summary>
        public List<ConfigItem> Items { get; }
        /// <summary>
        /// 读写扩展配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool this[string key]
        {
            get
            {
                return key != null && Items.FirstOrDefault(p => p.Name == key)?.Value == "1";
            }
            set
            {
                if (string.IsNullOrWhiteSpace(key))
                    return;
                var mv = Items.FirstOrDefault(p => p.Name == key);
                if (mv == null)
                {
                    Items.Add(new ConfigItem { Name = key, Value = value ? "1" : "0" });
                }
                else
                {
                    mv.Value = value ? "1" : "0";
                }
            }
        }
    }
}