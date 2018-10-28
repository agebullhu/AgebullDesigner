using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
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
    public class EnumModel : DesignCommondBase<EnumConfig>
    {

        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            commands.Add(new CommandItemBuilder<EnumConfig>
            {
                Catalog = "编辑",
                Action = SortEnum,
                Caption = "排序",
                IconName = "tree_sum"
            });
            commands.Add(new CommandItemBuilder<PropertyConfig>
            {
                Catalog = "工具",
                Action = ResetEnumParent,
                Caption = "按使用重新分配枚举项目归属",
                IconName = "tree_sum"
            });
        }


        public void SortEnum(EnumConfig @enum)
        {
            var array = @enum.Items.ToArray();
            @enum.Items.Clear();
            foreach (var item in array.OrderBy(p => p.Number))
                @enum.Add(item);
            @enum.IsModify = true;
        }
        public void ResetEnumParent(PropertyConfig property)
        {
            if (string.IsNullOrEmpty(property.CustomType))
                return;
            property.EnumConfig = GlobalConfig.GetEnum(property.CustomType);
            if (property.EnumConfig != null)
            {
                property.Parent.Parent.Add(property.EnumConfig);
            }
        }
    }
}