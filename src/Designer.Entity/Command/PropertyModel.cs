using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
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
            commands.Add(new CommandItemBuilder
            {
                NoButton = true,
                Signle = false,
                Catalog = "编辑",

                Command = new DelegateCommand(ToEnglish),
                Caption = "字段翻译",
                Description = "通过百度翻译接口,将字段的英文名称翻译成中文(需要网络连接)",
                IconName = "imgBaidu"
            });

            commands.Add(new CommandItemBuilder
            {
                NoButton = true,
                Signle = false,
                Catalog = "字段",
                Command = new DelegateCommand(CheckName),
                Caption = "字段名称规范",
                Description= "第一个[逗号/括号]后解析为说明",
                IconName = "tree_item"
            });
        }

        #endregion

        public void CheckName()
        {
            if (MessageBox.Show("确认执行【字段名称规范】的操作吗?", "字段编辑", MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes)
            {
                return;
            }
            Foreach(CheckName);
        }
        public void CheckName(PropertyConfig property)
        {
            if (string.IsNullOrWhiteSpace(property.Caption))
                return;
            var words = property.Caption.Split(new[] { ',', '，', '(', '（' }, 2, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length > 1)
            {
                property.Caption = words[0];
                property.Description = words[1];
            }
        }

        public void ToEnglish()
        {
            if (MessageBox.Show("确认执行【字段翻译】的操作吗?", "字段编辑", MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes)
            {
                return;
            }
            Foreach(ToEnglish);
        }

        public void ToEnglish(PropertyConfig property)
        {
            try
            {
                var tables = Context.GetSelectEntities();
                foreach (var entity in tables)
                {
                    var model = new EntityBusinessModel { Entity = entity };
                    model.EnglishToChiness();
                }
            }
            catch (Exception ex)
            {
                Context.CurrentTrace.TraceMessage.Status = ex.ToString();
            }
        }
    }
}