using System.ComponentModel.Composition;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// À©Õ¹½Úµã
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class PropertyModel : IAutoRegister
    {
        void IAutoRegister.AutoRegist()
        {
            DesignerManager.Registe<EntityConfig, CppFieldsPanel>("C++×Ö¶Î", "Entity");
        }
    }
}