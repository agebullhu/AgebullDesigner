using System.ComponentModel.Composition;
using Agebull.CodeRefactor.SolutionManager;
using Agebull.Common.SimpleDesign;
using Gboxt.Common.DataAccess.Schemas;

namespace Agebull.Common.Config.Designer
{
    /// <summary>
    /// 关系连接检查
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class Register : IAutoRegister
    {
        #region 注册
        
        /// <summary>
        /// 注册代码
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            DesignerManager.Registe<EntityConfig, TemplateCodeViewModel>("LUA模板");
        }


        #endregion
        
    }
}