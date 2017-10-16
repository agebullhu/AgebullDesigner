using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;
using System.Windows;
using Agebull.Common;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 实体配置相关模型
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class EnumModel : DesignCommondBase<PropertyConfig>
    {

        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            string type = $"{typeof(PropertyConfig).Name}";
            commands.Add(new CommandItemBuilder
            {
                Command = new DelegateCommand(ReadEnum),
                SourceType = type,
                Name = "识别枚举(形如【类型，1操作，2返回，3未知】样式的说明文字)",
                Signle = false,
                NoButton = true,
                IconName = "tree_item"
            });
            commands.Add(new CommandItemBuilder
            {
                Command = new DelegateCommand(EditEnum),
                SourceType = type,
                Name = "编辑枚举",
                Signle = true,
                NoButton = true,
                IconName = "tree_item"
            });
            commands.Add(new CommandItemBuilder
            {
                Command = new DelegateCommand(DeleteEnum),
                SourceType = type,
                Name = "清除枚举",
                Signle = true,
                NoButton = true,
                IconName = "tree_item"
            });
            commands.Add(new CommandItemBuilder
            {
                Command = new DelegateCommand(EnumToEnglish),
                SourceType = $"{typeof(EnumConfig).Name}",
                Name = "翻译枚举",
                Signle = true,
                NoButton = true,
                IconName = "tree_item"
            });
        }

        /// <summary>
        ///     自动修复
        /// </summary>
        public void EnumToEnglish()
        {
            var en = Context.SelectConfig as EnumConfig;
            if (en == null)
                return;
            foreach (var item in en.Items)
            {
                if (string.IsNullOrWhiteSpace(item.Name) || item.Name[0] < 256)
                    continue;
                item.Name = BaiduFanYi.FanYiWord(item.Name);
            }
            en.IsModify = true;
        }


        public void EditEnum()
        {
            PropertyConfig property = Context.SelectProperty;
            var cfg = CommandIoc.EditPropertyEnumCommand(property);
            if (cfg == null)
            {
                return;
            }
            property.EnumConfig = cfg;
            Model.Tree.SelectItem.Items.Clear();
            Model.Tree.PropertyChildTreeItem(Model.Tree.SelectItem, property);
        }

        /// <summary>
        /// 删除枚举
        /// </summary>
        public void DeleteEnum()
        {
            PropertyConfig property = Context.SelectProperty;
            property.CustomType = null;
            if (property.EnumConfig == null)
                return;
            property.EnumConfig = null;
        }

        #region 识别枚举

        public void ReadEnum()
        {
            if (MessageBox.Show("确认执行【识别枚举(形如【类型，1操作，2返回，3未知】样式的说明文字)】操作吗?", "对象编辑", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Foreach(ReadEnum);
        }

        public void ReadEnum(PropertyConfig column)
        {
            if (column.CsType == "bool" || column.EnumConfig != null)
                return;
            CheckEnum(column);
            if (column.EnumConfig != null)
            {
                column.EnumConfig.Name = column.Name + "Type";
                column.EnumConfig.Caption = column.Caption + "自定义类型";
                column.CustomType = column.EnumConfig.Name;
                Context.StateMessage = $@"解析得到枚举类型:{column.EnumConfig.Name},参考内容{column.EnumConfig.Description}";
                SolutionConfig.Current.Enums.Add(column.EnumConfig);
            }
            else
            {
                column.CustomType = null;
            }
        }


        public static void CheckEnum(PropertyConfig column)
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
                            ec.Items.Add(ei);
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
                    ec.Items.Add(new EnumItem
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
                ec.LinkField = column.Key;
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
                    item.Caption = arr[0].MulitReplace2( "","表示","代表","是","为");
                    item.Name = BaiduFanYi.FanYiWord(item.Caption.MulitReplace2("", "表示"));
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