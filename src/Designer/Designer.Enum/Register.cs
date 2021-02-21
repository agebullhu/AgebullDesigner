using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;
using Agebull.EntityModel.Designer.View;
using System.ComponentModel.Composition;

namespace Agebull.Common.Config.Designer.EasyUi
{
    /// <summary>
    /// ÃüÁî×¢²áÆ÷
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class Register : IAutoRegister
    {
        /// <summary>
        /// ×¢²á´úÂë
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            EditorManager.Registe2<EnumConfig, EnumEdit>("±à¼­Ã¶¾Ù", "Ã¶¾Ù");
        }
    }
}