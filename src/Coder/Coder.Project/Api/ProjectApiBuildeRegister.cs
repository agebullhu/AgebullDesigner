using Agebull.EntityModel.Designer;
using System.ComponentModel.Composition;

namespace Agebull.EntityModel.RobotCoder.EasyUi
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class ProjectApiBuildeRegister : IAutoRegister
    {
        /// <summary>
        /// 执行自动注册
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            CoderManager.RegistBuilder<ProjectApiBuilde>();
        }

    }

}