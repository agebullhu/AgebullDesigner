using System.ComponentModel.Composition;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// ��չ�ڵ�
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class PropertyModel : IAutoRegister
    {
        void IAutoRegister.AutoRegist()
        {
            DesignerManager.Registe<EntityConfig, CppFieldsPanel>("C++�ֶ�", "Entity");
        }
    }
}