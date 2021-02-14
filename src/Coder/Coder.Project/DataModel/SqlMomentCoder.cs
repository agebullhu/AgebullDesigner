using Agebull.EntityModel.Designer;
using System.ComponentModel.Composition;

namespace Agebull.EntityModel.RobotCoder.Project
{
    /// <summary>
    /// SQL代码片断
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal class SqlMomentCoder : MomentCoderBase, IAutoRegister
    {
        #region 注册

        /// <summary>
        /// 执行自动注册
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            CoderManager.RegisteCoder("EntityModel", "数据结构", "cs", DataBaseBuilder.EntityStruct);
        }

        #endregion

    }
}