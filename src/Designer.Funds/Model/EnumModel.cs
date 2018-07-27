using System.Collections.Generic;
using System.ComponentModel.Composition;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 扩展节点
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class EnumModel : DesignCommondBase<EnumConfig>
    {
        /// <summary>
        /// 生成命令对象
        /// </summary>
        /// <returns></returns>
        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            //if (SolutionConfig.Current.SolutionType != SolutionType.Cpp)
            //    return;
            commands.Add(new CommandItemBuilder<EnumConfig>
            {
                Caption = "枚举",
                SignleSoruce = true,
                TargetType = typeof(EnumConfig),
                Action = EnumBusinessModel.RepairEnum,
                Editor = "C++字段",
                IconName = "cpp"
            });
        }

    }
}
