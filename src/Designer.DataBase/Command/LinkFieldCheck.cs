using System;
using System.ComponentModel.Composition;
using System.Linq;
using Agebull.EntityModel.Designer;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

namespace Agebull.Common.Config.Designer
{
    /// <summary>
    /// ��ϵ���Ӽ��
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class LinkFieldCheck : EntityCommandBase, IAutoRegister
    {
        #region ע��

        public LinkFieldCheck()
        {
            Name = "Link Field Check";
            Caption = "��ϵ���Ӽ��";
            Catalog = "����";
            ViewModel = "database,model";
            NoButton = true;
        }
        /// <summary>
        /// ע�����
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            CommandCoefficient.RegisterCommand<EntityConfig, LinkFieldCheck>();
        }


        #endregion
        

        /// <summary>
        /// ִ����
        /// </summary>
        public override bool Execute(EntityConfig entity)
        {
            StateMessage = "���ڼ��:" + entity.Caption + "...";
            foreach (var field in entity.Properties)
            {
                if (!string.IsNullOrWhiteSpace(field.LinkTable))
                {
                    if (field.LinkTable == entity.Name || field.LinkTable == entity.ReadTableName ||
                        field.LinkTable == entity.SaveTableName)
                    {
                        field.LinkTable = null;
                        field.IsLinkCaption = false;
                        field.IsLinkKey = false;
                        field.IsLinkField = false;
                        continue;
                    }

                    var table = GlobalConfig.GetEntity(
                        p => p.Name == field.LinkTable || p.SaveTable == field.LinkTable ||
                             p.ReadTableName == field.LinkTable);
                    if (table != null && table != entity)
                    {
                        var pro = table.Properties.FirstOrDefault(p => p.Name == field.LinkField || p.ColumnName == field.LinkField);
                        if (pro != null)
                        {
                            field.IsLinkField = true;
                            field.IsLinkKey = pro.IsPrimaryKey;
                            field.IsLinkCaption = pro.IsCaption;
                            if (!field.IsLinkKey)
                                field.IsCompute = true;
                            field.ReferenceKey = field.Key;
                            continue;
                        }
                    }
                }
                field.LinkTable = field.LinkField = null;
                field.IsLinkField = field.IsLinkKey = field.IsLinkCaption = false;
                field.ReferenceKey = Guid.Empty;
            }
            StateMessage = "������:" + entity.Caption;
            return true;
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