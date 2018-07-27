using System;
using System.Collections.ObjectModel;
using System.Linq;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Config
{
    internal class EnumBusinessModel : ConfigModelBase
    {

        public static void RepairEnum(EnumConfig config)
        {
            var type = CppProject.Instance.GetTypedef(config.Key);
            if (type != null)
                RepairByTypedef(config.Parent, type);
        }

        public static EnumConfig RepairByTypedef(ProjectConfig project, TypedefItem type)
        {
            var enumcfg = SolutionConfig.Current.Enums.FirstOrDefault(p => p.Option.ReferenceKey == type.Key);
            if (enumcfg != null)
            {
                if (type.Items.Count == 0)
                {
                    enumcfg.Option.IsDelete = true;
                    return null;
                }
                FindEnumOld(type, enumcfg);
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
                FindEnumOld(type, enumcfg);
                return enumcfg;
            }
            enumcfg = new EnumConfig
            {
                Name = name,
                Caption = type.Caption.Replace("类型", "") + "类型",
                Description = type.Description.Replace("类型", "") + "类型",
                Items = new ObservableCollection<EnumItem>()
            };
            int id = 0;
            var name_head = words.LinkToString();
            foreach (var item in type.Items.Values)
            {
                var eitem = new EnumItem
                {
                    Name = ToWords(item.Name.ToLower(), true).LinkToString().Replace(name_head, "").Replace("Type", ""),
                    Caption = item.Caption,
                    Value = (++id).ToString()
                };
                enumcfg.Add(eitem);
                eitem.Option.ReferenceKey = item.Key;
            }
            enumcfg.Option.ReferenceKey = type.Key;
            project.Add(enumcfg);
            return enumcfg;
        }

        private static void FindEnumOld(TypedefItem type, EnumConfig enumConfig)
        {
            foreach (var item in type.Items.Values)
            {
                var oi = enumConfig.Items.FirstOrDefault(p => p.Option.ReferenceKey == item.Key);
                if (oi != null)
                    continue;
                enumConfig.Add(oi = new EnumItem
                {
                    Name = CoderBase.ToWordName(item.Name),
                    Value = item.Value 
                });
                oi.Option.ReferenceKey = item.Key;
            }
            enumConfig.Option.ReferenceKey = type.Key;
            if (enumConfig.Items.All(p => !string.Equals(p.Name, "None", StringComparison.OrdinalIgnoreCase)))
            {
                enumConfig.Add(new EnumItem
                {
                    Name = "None",
                    Caption = "未知",
                    Value = "0"
                });
            }
            var items = enumConfig.Items.OrderBy(p => p.Value).ToArray();
            enumConfig.Items.Clear();
            foreach (var item in items)
                enumConfig.Add(item);
        }
    }
}
