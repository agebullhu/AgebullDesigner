using System.ComponentModel.Composition;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.EasyUi
{
    /// <summary>
    /// API代码生成
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class ProjectApiActionCoderRegister : IAutoRegister
    {
        /// <summary>
        /// 执行自动注册
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            MomentCoder.RegisteCoder("Web-Api", "表单读取", "cs", ProjectApiActionCoder<EntityConfig>.ReadFormValue);
            MomentCoder.RegisteCoder("Web-Api", "ApiController.cs", "cs", new ProjectApiActionCoder<EntityConfig>().BaseCode);
            MomentCoder.RegisteCoder("Web-Api", "ApiController.Designer.cs", "cs", new ProjectApiActionCoder<EntityConfig>().ExtendCode);
            
            MomentCoder.RegisteCoder("Web-Api", "表单读取", "cs", ProjectApiActionCoder<ModelConfig>.ReadFormValue);
            MomentCoder.RegisteCoder("Web-Api", "ApiController.cs", "cs", new ProjectApiActionCoder<ModelConfig>().BaseCode);
            MomentCoder.RegisteCoder("Web-Api", "ApiController.Designer.cs", "cs",new ProjectApiActionCoder<ModelConfig>().ExtendCode);
        }
    }
}