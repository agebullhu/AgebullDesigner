using System.ComponentModel.Composition;
using Agebull.CodeRefactor.SolutionManager;
using Agebull.Common.SimpleDesign;
using Gboxt.Common.DataAccess.Schemas;

namespace Agebull.Common.Config.Designer.DataBase.Mysql
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
            DesignerManager.Registe<EntityConfig, DataBaseViewModel>("数据库设计");
            DesignerManager.Registe<EntityConfig, DataRelationViewModel>("数据关系");
        }
        

        #endregion

    }
}