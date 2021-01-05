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
            MomentCoder.RegisteCoder("Web-Api", "表单读取", "cs", ProjectApiActionCoder.ReadFormValue);
            MomentCoder.RegisteCoder("Web-Api", "ApiController.cs", "cs", new ProjectApiActionCoder().BaseCode);
            MomentCoder.RegisteCoder("Web-Api", "ApiController.Designer.cs", "cs", new ProjectApiActionCoder().ExtendCode);
            
        }
    }
}