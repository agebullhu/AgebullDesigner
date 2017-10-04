using System.ComponentModel.Composition;
using Agebull.CodeRefactor.SolutionManager;
using Agebull.Common.SimpleDesign;
using Gboxt.Common.DataAccess.Schemas;

namespace Agebull.Common.Config.Designer
{
    /// <summary>
    /// ��ϵ���Ӽ��
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class Register : IAutoRegister
    {
        #region ע��
        
        /// <summary>
        /// ע�����
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            DesignerManager.Registe<EntityConfig, TemplateCodeViewModel>("LUAģ��");
        }


        #endregion
        
    }
}