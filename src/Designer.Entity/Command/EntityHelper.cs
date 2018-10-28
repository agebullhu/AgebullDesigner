using System.ComponentModel.Composition;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.Common.Config.Designer
{
    /// <summary>
    /// 规范实体名
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class EntityHelper : EntityCommandBase, IAutoRegister
    {
        #region 注册

        public EntityHelper()
        {
            Name = "规范实体名";
            Caption = "规范实体名([Name]Data)";
            Catalog = "工具";
            ViewModel = "entity";
        }

        /// <summary>
        /// 注册代码
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            CommandCoefficient.RegisterCommand<EntityHelper>();
        }


        #endregion

        public override bool Prepare(RuntimeArgument argument)
        {
            return true;
        }

        /// <summary>
        /// 执行器
        /// </summary>
        public override bool Execute(EntityConfig entity)
        {
            StateMessage = entity.Caption + "...";
            entity.EntityName = entity.Name + "Data";
            return true;
        }

        /// <summary>
        /// 执行器
        /// </summary>
        public override bool Execute(ProjectConfig project)
        {
            return true;
        }
    }
}