using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 属性相关操作命令
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal class PropertyModel : DesignCommondBase<PropertyConfig>
    {
        #region 操作命令

        /// <summary>
        /// 生成命令对象
        /// </summary>
        /// <returns></returns>
        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            commands.Add(new CommandItemBuilder<PropertyConfig>
            {
                SignleSoruce = false,
                WorkView= "adv",
                Catalog = "字段",
                Action = CheckName,
                Caption = "属性名称大驼峰",
                IconName = "tree_item",
                ConfirmMessage= "确认执行【字段名称规范】的操作吗?"
            });

            commands.Add(new CommandItemBuilder<PropertyConfig>
            {
                SignleSoruce = false,
                WorkView = "adv",
                Catalog = "字段",
                Action = CheckCaption,
                Caption = "标题与注释拆解",
                Description = "第一个[标点]后解析为说明",
                IconName = "tree_item",
                ConfirmMessage = "确认执行【字段名称规范】的操作吗?"
            });
            commands.Add(new CommandItemBuilder<PropertyConfig>
            {
                Catalog = "工具",
                WorkView = "adv",
                Action = UpdateCustomType,
                Caption = "修复用户类型",
                IconName = "img_modify"
            });
            commands.Add(new CommandItemBuilder<PropertyConfig>
            {
                Action = p => p.Parent.Remove(p),
                Catalog = "编辑",
                Caption = "删除字段",
                SignleSoruce = true,
                IconName = "img_del"
            });
        }

        #endregion

        public void UpdateCustomType(PropertyConfig field)
        {
            if (string.IsNullOrWhiteSpace(field.CustomType))
            {
                field.CustomType = null;
            }
            else if (field.CustomType.Contains("[]"))
            {
                field.IsArray = true;
                field.CsType = field.CustomType.Split('[')[0];
            }
            else if (field.CustomType.IndexOf("List<", StringComparison.Ordinal) >= 0)
            {
                field.IsArray = true;
                field.CsType = field.CustomType.Split('<', '>')[1];
            }
            else
            {
                field.EnumConfig = SolutionConfig.Current.Enums.FirstOrDefault(p => p.Name == field.CustomType);
            }
        }

        public void CheckName(PropertyConfig property)
        {
            var bak = property.DbFieldName;
            if (string.IsNullOrWhiteSpace(bak))
                bak = property.Name;
            property.Name = GlobalConfig.ToLinkWordName(bak, null,true);
            property.DbFieldName = bak;
        }
        public void CheckCaption(PropertyConfig property)
        {
            if (string.IsNullOrWhiteSpace(property.Caption))
                return;
            var caption = property.Caption;
            for (var idx = 0;idx < caption.Length; idx++)
            {
                if (char.IsPunctuation(caption[idx]))
                {
                    property.Caption = caption.Substring(0,idx);
                    property.Description = caption;
                    return;
                }
            }
        }

    }
}