using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;
using System.Windows;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 实体配置相关模型
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class PropertyEnumModel : DesignCommondBase<PropertyConfig>
    {

        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            var type = typeof(PropertyConfig);
            commands.Add(new CommandItemBuilder<PropertyConfig>
            {
                Catalog = "字段",
                Action = (ReadEnum),
                TargetType = type,
                Caption = "识别枚举",
                Description = "形如【类型，1操作，2返回，3未知】样式的说明文字",
                ConfirmMessage = "确认执行【识别枚举】操作吗?\n形如【类型，1操作，2返回，3未知样式】的说明文字",
                SignleSoruce = false,
                IconName = "tree_item"
            });
            commands.Add(new CommandItemBuilder<PropertyConfig>
            {
                Catalog = "字段",
                Action = (CheckEnum),
                TargetType = type,
                Caption = "刷新对象引用",
                IconName = "tree_item"
            });
            commands.Add(new CommandItemBuilder
            {
                Catalog = "字段",
                Action = (BindEnum),
                TargetType = type,
                Caption = "绑定或新增枚举",
                SignleSoruce = true,
                IconName = "tree_item"
            });
            commands.Add(new CommandItemBuilder
            {
                Catalog = "字段",
                Action = (DeleteEnum),
                TargetType = type,
                Caption = "清除枚举绑定",
                SignleSoruce = true,
                IconName = "tree_item"
            });
        }

        public void BindEnum(object arg)
        {
            PropertyConfig property = Context.SelectProperty;
            property.EnumConfig = null;
            if (property.CustomType != null)
            {
                property.EnumConfig = GlobalConfig.GetEnum(property.CustomType);
            }
            if (property.EnumConfig != null)
                return;
            if (MessageBox.Show("是否新增一个枚举?", "对象编辑", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;
            property.Parent.Parent.Add(property.EnumConfig = new EnumConfig
            {
                Name = property.CustomType ?? (property.Name.Contains("Type") ? property.Name : (property.Name + "Type")),
                Caption = property.Caption + "枚举类型"
            });
        }

        public void CheckEnum(PropertyConfig property)
        {
            property.EnumConfig = null;
            if (property.CustomType != null)
            {
                property.EnumConfig = GlobalConfig.GetEnum(property.CustomType);
            }
        }
        /// <summary>
        /// 删除枚举
        /// </summary>
        public void DeleteEnum(object arg)
        {
            PropertyConfig property = Context.SelectProperty;
            property.CustomType = null;
            property.EnumConfig = null;
        }

        #region 识别枚举

        public void ReadEnum(PropertyConfig column)
        {
            if (column.CsType == "bool" || column.EnumConfig != null)
                return;
            ReadPropertyEnum(column);
            if (column.EnumConfig != null)
            {
                column.EnumConfig.Name = column.Name + "Type";
                column.EnumConfig.Caption = column.Caption + "自定义类型";
                column.CustomType = column.EnumConfig.Name;
                Context.StateMessage = $@"解析得到枚举类型:{column.EnumConfig.Name},参考内容{column.EnumConfig.Description}";
                column.EnumConfig.Parent.Add(column.EnumConfig);
            }
            else
            {
                column.CustomType = null;
            }
        }


        public static void ReadPropertyEnum(PropertyConfig column)
        {
            var line = column.Description?.Trim(CoderBase.NoneLanguageChar) ?? "";

            StringBuilder sb = new StringBuilder();
            StringBuilder caption = new StringBuilder();
            bool preIsNumber = false;
            bool startEnum = false;
            EnumConfig ec = new EnumConfig
            {
                Name = column.Parent.Name.ToUWord() + column.Name.ToUWord(),
                Description = column.Description,
                Caption = column.Caption,
                Items = new ConfigCollection<EnumItem>()
            };
            EnumItem ei = new EnumItem();
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
                else
                {
                    if (preIsNumber)
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
                    }
                    preIsNumber = false;
                }
                sb.Append(c);
            }

            if (!startEnum)
                return;
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

            if (ec.Items.Count > 0)
            {
                ec.Option.ReferenceKey = column.Option.Key;
                column.EnumConfig = ec;
                column.CustomType = ec.Name;
                column.Description = line;
                foreach (var item in ec.Items)
                {
                    if (string.IsNullOrEmpty(item.Caption))
                    {
                        column.EnumConfig = null;
                        column.CustomType = null;
                        return;
                    }
                    var arr = item.Caption.Trim(CoderBase.NoneNameChar).Split(CoderBase.NoneNameChar, StringSplitOptions.RemoveEmptyEntries);
                    if (arr.Length == 0)
                    {
                        column.EnumConfig = null;
                        column.CustomType = null;
                        return;
                    }
                    item.Caption = arr[0].MulitReplace2("", "表示", "代表", "是", "为");
                    item.Name = item.Name;
                }
                if (caption.Length > 0)
                    column.Caption = caption.ToString();
            }
            else
            {
                column.EnumConfig = null;
                column.CustomType = null;
            }
        }

        #endregion
    }
}