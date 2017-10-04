using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Agebull.Common.SimpleDesign;
using Gboxt.Common.DataAccess.Schemas;
using Gboxt.Common.WpfMvvmBase.Commands;

namespace Agebull.CodeRefactor.SolutionManager
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

            commands.Add(new CommandItemBuilder
            {
                Name = "查找原类型",
                Signle = true,
                NoButton = true,
                SourceType = typeof(EnumConfig).FullName,
                Command = new DelegateCommand<EnumConfig>(EnumBusinessModel.RepairEnum),
                IconName = "cpp"
            });
        }

    }
}
