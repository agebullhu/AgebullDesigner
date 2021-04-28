using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 实体配置相关模型
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class PropertyEnumModel : DesignCommondBase<FieldConfig>
    {

        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            var type = typeof(FieldConfig);
            commands.Add(new CommandItemBuilder<FieldConfig>
            {
                Catalog = "枚举",
                Action = ReadEnum,
                TargetType = type,
                Caption = "识别枚举",
                Description = "形如【类型，1操作，2返回，3未知】样式的说明文字",
                ConfirmMessage = "确认执行【识别枚举】操作吗?\n形如【类型，1操作，2返回，3未知样式】的说明文字",
                SoruceView = "entity",
                SignleSoruce = false,
                IconName = "枚举"
            });
            commands.Add(new CommandItemBuilder<FieldConfig>
            {
                Catalog = "枚举",
                Action = CheckEnum,
                SoruceView = "entity",
                TargetType = type,
                Caption = "刷新枚举引用",
                IconName = "引用"
            });
            commands.Add(new CommandItemBuilder<IPropertyConfig>
            {
                Catalog = "枚举",
                Action = BindEnum,
                TargetType = type,
                Caption = "绑定或新增枚举",
                SoruceView = "field",
                SignleSoruce = true,
                IconName = "新增"
            });
            commands.Add(new CommandItemBuilder<IPropertyConfig>
            {
                Catalog = "枚举",
                SoruceView = "entity",
                DoConfirm = true,
                Action = DeleteEnum,
                TargetType = type,
                Caption = "清除枚举绑定",
                SignleSoruce = true,
                IconName = "清除"
            });
        }

        public void BindEnum(IPropertyConfig property)
        {
            property.EnumConfig = GlobalConfig.GetEnum(property.CustomType);
            if (property.EnumConfig != null)
                return;
            if (MessageBox.Show("是否新增一个枚举?", "对象编辑", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;
            var enumConfig = new EnumConfig
            {
                Name = property.CustomType ?? (property.Name.Contains("Type") ? property.Name : property.Name + "Type"),
                Caption = property.Caption + "类型"
            };
            property.Entity.Project.Add(enumConfig);
            property.EnumConfig = enumConfig;
        }

        public void CheckEnum(IPropertyConfig property)
        {
            property.EnumConfig = GlobalConfig.GetEnum(property.CustomType);
        }

        /// <summary>
        /// 删除枚举
        /// </summary>
        public void DeleteEnum(IPropertyConfig property)
        {
            property.CustomType = null;
        }

        #region 识别枚举

        public void ReadEnum(FieldConfig column)
        {
            if (column.CsType == "bool")
                return;

            EnumConfig ec = column.EnumConfig;

            string desc = column.Description ?? column.Caption ?? column.Name;
            var line = desc.Trim(NameHelper.NoneLanguageChar) ?? "";

            StringBuilder sb = new();
            StringBuilder caption = new();
            bool preIsNumber = false;
            bool startEnum = false;
            bool isNew = false;
            var name = column.Name;
            if (name.Length <= 4 || !string.Equals(name.Substring(name.Length - 4, 4), "Type", StringComparison.OrdinalIgnoreCase))
            {
                name = name.ToUWord() + "Type";
            }
            if (ec == null)
            {
                ec = GlobalConfig.GetEnum(name);
                isNew = ec == null;
            }
            if (isNew)
            {
                ec = new EnumConfig
                {
                    Name = name,
                    Description = desc,
                    Caption = column.Caption
                };
            }
            else
            {
                ec.Items.Clear();
            }

            EnumItem ei = new();
            foreach (var c in line)
            {
                if (c >= '0' && c <= '9')
                {
                    if (!preIsNumber)
                    {
                        if (!startEnum)
                        {
                            caption.Append(sb);
                        }
                        else if (sb.Length > 0)
                        {
                            new List<string>().Add(sb.ToString());
                            ei.Caption = sb.ToString();
                        }
                        sb = new StringBuilder();
                        startEnum = true;
                    }
                    preIsNumber = true;
                }
                else if (preIsNumber && c != '.')
                {
                    if (sb.Length > 0)
                    {
                        ei = new EnumItem
                        {
                            Value = sb.ToString()
                        };
                        ec.Add(ei);
                        sb = new StringBuilder();
                    }
                    preIsNumber = false;
                }
                sb.Append(c);
            }

            if (!startEnum)
            {
                column.CustomType = null;
                return;
            }
            if (sb.Length > 0)
            {
                if (preIsNumber)
                {
                    ec.Add(new EnumItem
                    {
                        Value = sb.ToString()
                    });
                }
                else
                {
                    ei.Caption = sb.ToString();
                }
            }
            if (ec.Items.Count <= 1)
            {
                column.CustomType = null;
                return;
            }
            var items = ec.Items.ToArray();
            ec.Items.Clear();
            foreach (var item in items)
            {
                if (string.IsNullOrWhiteSpace(item.Caption))
                {
                    continue;
                }
                var arr = item.Caption.Trim(NameHelper.NoneNameChar).Split(NameHelper.NoneNameChar, StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length == 0)
                {
                    continue;
                }
                item.Caption = arr[0].MulitReplace2("", "表示", "代表", "是", "为").Trim(NameHelper.NoneLanguageChar);
                if (string.IsNullOrWhiteSpace(item.Name))
                    item.Name = item.Caption;
                ec.Items.Add(item);
            }
            if (ec.Items.Count > 1)
            {
                if (isNew)
                    column.Entity.Project.Add(ec);

                ec.Option.ReferenceKey = column.Option.Key;
                column.EnumConfig = ec;
                column.Description = line;
                if (caption.Length > 0)
                    ec.Caption = column.Caption = caption.ToString().Trim(NameHelper.NoneLanguageChar);
                Context.StateMessage = $@"解析得到枚举类型:{column.EnumConfig.Name},参考内容{column.EnumConfig.Description}";
            }
            else
            {
                column.CustomType = null;
            }
        }

        #endregion
    }
}