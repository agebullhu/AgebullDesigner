using Newtonsoft.Json;
using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 扩展配置节点
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class ConfigItemList
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="items"></param>
        public ConfigItemList(NotificationList<ConfigItem> items)
        {
            Items = items;
        }

        /// <summary>
        /// 节点（引用）
        /// </summary>
        public NotificationList<ConfigItem> Items { get; }

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
                value = value.SafeTrim();
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


        /// <summary>
        ///     清除文件信息
        /// </summary>
        public void ClearFileConfig()
        {
            foreach (var kv in Items.Where(p => p.Name != null && p.Name.IndexOf("File_", StringComparison.OrdinalIgnoreCase) == 0).ToArray())
            {
                Items.Remove(kv);
            }
        }
        /// <summary>
        /// 清除所有
        /// </summary>
        public void Clear()
        {
            Items.Clear();
        }
    }
}