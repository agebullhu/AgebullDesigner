using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

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
                IconName = "排序",
                SoruceView = "enum",
            });
            commands.Add(new CommandItemBuilder<FieldConfig>
            {
                Catalog = "工具",
                Action = ResetEnumParent,
                Caption = "按使用重新分配枚举项目归属",
                IconName = "修复",
                SoruceView = "enum",
            });
        }


        public void SortEnum(EnumConfig @enum)
        {
            var array = @enum.Items.ToArray();
            @enum.Items.Clear();
            foreach (var item in array.OrderBy(p => p.Number))
                @enum.Add(item);
        }
        public void ResetEnumParent(FieldConfig property)
        {
            if (string.IsNullOrWhiteSpace(property.CustomType))
                return;
            property.EnumConfig = GlobalConfig.GetEnum(property.CustomType);
            if (property.EnumConfig != null)
            {
                property.Entity.Project.Add(property.EnumConfig);
            }
        }
    }
}