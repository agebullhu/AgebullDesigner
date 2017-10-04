using System.Collections.Generic;
using System.ComponentModel.Composition;
using Agebull.Common;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

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
                Command = new DelegateCommand<ConfigTreeItem<PropertyConfig>>(p =>
                {
                    EditEnum(p.Model);
                }),
                SourceType = type,
                Name = "编辑枚举",
                Signle=true,
                NoButton=true,
                IconName = "tree_item"
            });
            commands.Add(new CommandItemBuilder
            {
                Command = new DelegateCommand<ConfigTreeItem<PropertyConfig>>(DeleteEnum),
                SourceType = type,
                Name = "删除枚举",
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


        public void EditEnum(PropertyConfig property)
        {
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
        /// <param name="p"></param>
        public void DeleteEnum(ConfigTreeItem<PropertyConfig> p)
        {
            p.Model.CustomType = null;
            if (p.Model.EnumConfig == null)
                return;
            p.Model.EnumConfig.IsDelete = true;
            p.Model.EnumConfig = null;
            p.ReShow();
        }

    }
}