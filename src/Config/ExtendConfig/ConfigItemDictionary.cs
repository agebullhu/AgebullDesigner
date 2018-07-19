using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// ��չ���ýڵ�
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class ConfigItemDictionary
    {
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="items"></param>
        public ConfigItemDictionary(Dictionary<string, Dictionary<string, string>> items)
        {
            Items = items;
        }

        /// <summary>
        /// �ڵ㣨���ã�
        /// </summary>
        public Dictionary<string, Dictionary<string, string>> Items { get; }

        /// <summary>
        /// ��д��չ����
        /// </summary>
        /// <param name="classify">����</param>
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
            }
        }
    }
}