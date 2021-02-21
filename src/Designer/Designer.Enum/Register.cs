using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;
using Agebull.EntityModel.Designer.View;
using System.ComponentModel.Composition;

namespace Agebull.Common.Config.Designer.EasyUi
{
    /// <summary>
    /// ����ע����
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class Register : IAutoRegister
    {
        /// <summary>
        /// ע�����
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            EditorManager.Registe2<EnumConfig, EnumEdit>("�༭ö��", "ö��");
        }
    }
}