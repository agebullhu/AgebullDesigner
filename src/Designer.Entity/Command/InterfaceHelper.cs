using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Agebull.EntityModel.Designer;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

namespace Agebull.Common.Config.Designer
{
    /// <summary>
    /// 接口实现检查
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class InterfaceHelper : EntityCommandBase, IAutoRegister
    {
        #region 注册

        public InterfaceHelper()
        {
            Name = "Interface Check";
            Caption = "接口实现检查";
            NoButton = true;
        }
        /// <summary>
        /// 注册代码
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            CommandCoefficient.RegisterCommand<EntityConfig, InterfaceHelper>();
        }


        #endregion
        
        private List<EntityConfig> interfaces;
        public override void Prepare(RuntimeArgument argument)
        {
            interfaces = GlobalConfig.Entities.Where(p => p.IsInterface).ToList();
        }

        /// <summary>
        /// 执行器
        /// </summary>
        public override bool Execute(EntityConfig entity)
        {
            StateMessage = "正在检查:" + entity.Caption + "...";
            foreach (var it in interfaces)
            {
                if (string.IsNullOrWhiteSpace(entity.Interfaces))
                    continue;
                if (entity.Interfaces.Contains(it.Name))
                {
                    foreach (var field in it.Properties)
                    {
                        var pro = entity.Properties.FirstOrDefault(p => p.Name == field.Name);
                        if (pro != null)
                        {
                            pro.IsInterfaceField = true;
                            pro.ReferenceKey = field.Key;
                        }
                    }
                }
            }
            StateMessage = "检查完成:" + entity.Caption ;
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
