using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;
using System.ComponentModel.Composition;

namespace Agebull.Common.Config.Designer.DataBase.Mysql
{
    /// <summary>
    /// 命令注册器
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class Register : IAutoRegister
    {
        /// <summary>
        /// 注册代码
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            CodeStyleManager.Regist<MysqlGeneralStyle>();
            CodeStyleManager.Regist<MysqlSuccinctStyle>();
            EditorManager.Registe<IEntityConfig, DataBasePanel>("数据库", 1, "DataBase");
            EditorManager.Registe<IEntityConfig, RelationPanel>("数据关系", 2, "DataBase");
        }
    }
}