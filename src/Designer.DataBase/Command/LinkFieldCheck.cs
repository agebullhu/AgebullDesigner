using System;
using System.ComponentModel.Composition;
using System.Linq;
using Agebull.EntityModel.Designer;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

namespace Agebull.Common.Config.Designer
{
    /// <summary>
    /// ����ע����
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class LinkFieldCheck : EntityCommandBase, IAutoRegister
    {
        #region ע��

        public LinkFieldCheck()
        {
            Name = "Link Field Check";
            Caption = "��ϵ�����޸�";
            Catalog = "����";
            SignleSoruce = false;
            //Editor = "DataRelation";
            
            WorkView = "database,model";
        }
        /// <summary>
        /// ע�����
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            CommandCoefficient.RegisterCommand<LinkFieldCheck>();
        }


        #endregion


        /// <summary>
        /// ִ����
        /// </summary>
        public override bool Execute(EntityConfig entity)
        {
            StateMessage = "���ڼ��:" + entity.Caption + "...";
            bool re= DataBaseHelper.CheckFieldLink(entity);
            StateMessage = "������:" + entity.Caption;
            return re;
        }

        /// <summary>
        /// ִ����
        /// </summary>
        public override bool Execute(ProjectConfig project)
        {
            return true;
        }

    }
}