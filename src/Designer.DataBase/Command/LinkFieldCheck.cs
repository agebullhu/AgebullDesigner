using System;
using System.ComponentModel.Composition;
using System.Linq;
using Agebull.EntityModel.Designer;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

namespace Agebull.Common.Config.Designer
{
    /// <summary>
    /// 命令注册器
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class LinkFieldCheck : EntityCommandBase, IAutoRegister
    {
        #region 注册

        public LinkFieldCheck()
        {
            Name = "Link Field Check";
            Caption = "关系连接修复";
            Catalog = "工具";
            SignleSoruce = false;
            //Editor = "DataRelation";
            
            WorkView = "database,model";
        }
        /// <summary>
        /// 注册代码
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            CommandCoefficient.RegisterCommand<LinkFieldCheck>();
        }


        #endregion


        /// <summary>
        /// 执行器
        /// </summary>
        public override bool Execute(EntityConfig entity)
        {
            StateMessage = "正在检查:" + entity.Caption + "...";
            bool re= DataBaseHelper.CheckFieldLink(entity);
            StateMessage = "检查完成:" + entity.Caption;
            return re;
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