using System.ComponentModel.Composition;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer.Vue
{
    /// <summary>
    /// ����ע����
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
            DesignerManager.Registe<EntityConfig, UiPanel>("�û�����", "Model");
        }


        #endregion
        
    }
}