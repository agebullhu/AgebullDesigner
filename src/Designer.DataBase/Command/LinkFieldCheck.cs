using System;
using System.ComponentModel.Composition;
using System.Linq;
using Agebull.Common.SimpleDesign;
using Gboxt.Common.DataAccess.Schemas;
using WpfMvvmBase.Coefficient;

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
                    var table = GlobalConfig.GetEntity(
                        p => p.Name == field.LinkTable || p.SaveTable == field.LinkTable ||
                             p.ReadTableName == field.LinkTable);
                    var pro = table?.Properties.FirstOrDefault(
                        p => p.Name == field.LinkField || p.ColumnName == field.LinkField);
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
                else
                {
                    field.LinkTable=field.LinkField = null;
                    field.IsLinkKey = field.IsLinkCaption = false;
                }
                field.IsLinkField = false;
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