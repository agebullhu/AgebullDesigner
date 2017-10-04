using System.Collections.Generic;
using System.ComponentModel.Composition;
using Agebull.Common.SimpleDesign;
using Gboxt.Common.DataAccess.Schemas;
using Gboxt.Common.WpfMvvmBase.Commands;

namespace Agebull.CodeRefactor.SolutionManager
{
    /// <summary>
    /// ��չ�ڵ�
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class PropertyModel : DesignCommondBase<PropertyConfig>
    {
        /// <summary>
        /// �����������
        /// </summary>
        /// <returns></returns>
        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
        }

    }
}