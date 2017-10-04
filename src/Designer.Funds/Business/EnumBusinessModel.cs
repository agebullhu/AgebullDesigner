using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Gboxt.Common.DataAccess.Schemas
{
    internal class EnumBusinessModel : ConfigModelBase
    {

        public static void RepairEnum(EnumConfig config)
        {
            var type = GetTypedefByTag(config.Tag);
            if (type != null)
                RepairByTypedef(type);
        }

        public static EnumConfig RepairByTypedef(TypedefItem type)
        {
            var tag = type.Tag + "," + type.Name;
            var enumcfg = SolutionConfig.Current.Enums.FirstOrDefault(p => p.Tag == tag);
            if (enumcfg != null)
            {
                if (type.Items.Count == 0)
                {
                    enumcfg.IsDelete = true;
                    return null;
                }
                FindEnumOld(type, enumcfg, enumcfg.Tag);
                return enumcfg;
            }
            if (type.Items.Count == 0)
            {
                return null;
            }
            string name = type.Name;
            if (type.Name[0] == 'T')
                name = type.Name.Substring(1);
            var words = ToWords(name, true);
            while (words.Last() == "Type")
            {
                words.RemoveAt(words.Count - 1);
            }

            name = words.LinkToString() + "Type";
            enumcfg = SolutionConfig.Current.Enums.FirstOrDefault(p => p.Name == name);
            if (enumcfg != null)
            {
                FindEnumOld(type, enumcfg, enumcfg.Tag);
                return enumcfg;
            }
            enumcfg = new EnumConfig
            {
                Name = name,
                Caption = type.Caption.Replace("类型", "") + "类型",
                Description = type.Description.Replace("类型", "") + "类型",
                Tag = tag,
                Items = new ObservableCollection<EnumItem>()
            };
            int id = 0;
            var name_head = words.LinkToString();
            foreach (var item in type.Items.Values)
            {
                enumcfg.Items.Add(new EnumItem
                {
                    Name = ToWords(item.Name.ToLower(), true).LinkToString().Replace(name_head, "").Replace("Type", ""),
                    Caption = item.Caption,
                    Tag = tag + "," + item.Name,
                    Value = (++id).ToString()
                });
            }
            SolutionConfig.Current.Enums.Add(enumcfg);
            return enumcfg;
        }

        private static void FindEnumOld(TypedefItem type, EnumConfig enumConfig, string tag)
        {
            string typeName = type.Name;
            if (typeName[0] == 'T' && typeName[1] >= 'A' && typeName[1] <= 'Z')
                typeName = typeName.Substring(1);
            var words = ToWords(typeName);
            foreach (var item in type.Items.Keys)
            {
                var words2 = item.Split('_');
                StringBuilder sb = new StringBuilder();
                int i;
                for (i = 0; i < words.Count && i < words2.Length; i++)
                {
                    if (!string.Equals(words[i], words2[i], StringComparison.OrdinalIgnoreCase))
                        break;
                }
                for (;i < words2.Length; i++)
                {
                    sb.Append(words2[i].ToLower().ToUWord());
                }
                string i_tag = tag + "," + item;
                var name = sb.ToString();
                var name2 = words2.LinkToString();
                var oi = enumConfig.Items.FirstOrDefault(p => name.Equals(p.Name,StringComparison.OrdinalIgnoreCase)) ??
                    enumConfig.Items.FirstOrDefault(p => name2.Equals(p.Name, StringComparison.OrdinalIgnoreCase));
                if (oi != null)
                    oi.Tag = tag + "," + item;
                else if (!enumConfig.Items.Any(p => i_tag.Equals(p.Tag)))
                    Debug.WriteLine(item);
            }
            enumConfig.Tag = tag;
            if (enumConfig.Items.All(p => !string.Equals(p.Name, "None", StringComparison.OrdinalIgnoreCase)))
            {
                enumConfig.Items.Add(new EnumItem
                {
                    Name = "None",
                    Caption = "未知",
                    Value = "0"
                });
            }
            var items = enumConfig.Items.OrderBy(p => p.Value).ToArray();
            enumConfig.Items.Clear();
            enumConfig.Items.AddRange(items);
        }
    }
}
