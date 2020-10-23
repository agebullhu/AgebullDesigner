using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 扩展配置节点
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public class ConfigItemDictionary : NotificationObject
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="items"></param>
        public ConfigItemDictionary(Dictionary<string, Dictionary<string, string>> items)
        {
            Items = items;
        }

        /// <summary>
        /// 节点（引用）
        /// </summary>
        public Dictionary<string, Dictionary<string, string>> Items { get; }

        /// <summary>
        /// 读写扩展配置
        /// </summary>
        /// <param name="classify">分类</param>
        /// <param name="name"></param>
        /// <returns></returns>
        public string this[string classify, string name]
        {
            get => string.IsNullOrWhiteSpace(classify) || !Items.ContainsKey(classify) || string.IsNullOrWhiteSpace(name) || !Items[classify].ContainsKey(name)
                ? null
                : Items[classify][name];
            set
            {
                if (string.IsNullOrWhiteSpace(classify) || string.IsNullOrWhiteSpace(name))
                    return;
                if (!Items.ContainsKey(classify))
                {
                    Items.Add(classify, new Dictionary<string, string>
                    {
                        {name, value}
                    });
                }
                else if (!Items[classify].ContainsKey(name))
                {
                    Items[classify].Add(name, value);
                }
                else
                {
                    Items[classify][name] = value;
                }
                RaisePropertyChanged($"{classify}_{name}");
            }
        }
        /// <summary>
        /// 读写扩展配置
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string this[string name]
        {
            get
            {
                var sp = name?.Split('_');
                return sp == null || sp.Length == 1 ? this[GloblaKey, name] : this[sp[0], sp[1]];
            }

            set
            {
                var sp = name?.Split('_');
                if (sp == null || sp.Length == 1)
                {
                    this[GloblaKey, name] = value;
                    RaisePropertyChanged(name);
                }
                else
                {
                    this[sp[0], sp[1]] = value;
                    RaisePropertyChanged(name);
                }
            }
        }

        /// <summary>
        /// 所有名称
        /// </summary>
        public List<string> Names
        {
            get
            {
                List<string> names = new List<string>();
                foreach (var item in Items)
                {
                    if (item.Value == null)
                        continue;
                    foreach (var key in item.Value.Keys)
                    {
                        names.Add(item.Key == GloblaKey ? key : $"{item}.{key}");
                    }
                }
                return names;
            }
        }
        /// <summary>
        /// 全局对象键名
        /// </summary>
        public const string GloblaKey = "global";
    }


    /// <summary>
    /// 扩展配置节点
    /// </summary>
    public class ConfigItemListBool : NotificationObject
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="items"></param>
        public ConfigItemListBool(ConfigItemDictionary items)
        {
            _extend = items;
        }

        /// <summary>
        /// 节点（引用）
        /// </summary>
        private readonly ConfigItemDictionary _extend;

        /// <summary>
        /// 读写扩展配置
        /// </summary>
        /// <param name="classify">分类</param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool this[string classify, string name]
        {
            get => bool.TryParse(_extend[classify, name], out var vl) && vl;
            set
            {
                _extend[classify, name] = value.ToString();
                RaisePropertyChanged($"{classify}_{name}");
            }
        }

        /// <summary>
        /// 读写扩展配置
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool this[string name]
        {
            get => bool.TryParse(_extend[name], out var vl) && vl;
            set
            {
                _extend[name] = value.ToString();
                RaisePropertyChanged(name);
            }
        }
    }
}