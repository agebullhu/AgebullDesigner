using System;
using System.ComponentModel.Composition;
using System.Linq;
using Agebull.Common.SimpleDesign;
using Gboxt.Common.DataAccess.Schemas;
using WpfMvvmBase.Coefficient;

namespace Agebull.Common.Config.Designer
{
    /// <summary>
    /// 关系连接检查
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class LinkFieldCheck : EntityCommandBase, IAutoRegister
    {
        #region 注册

        public LinkFieldCheck()
        {
            Name = "Link Field Check";
            Caption = "关系连接检查";
            NoButton = true;
        }
        /// <summary>
        /// 注册代码
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            CommandCoefficient.RegisterCommand<EntityConfig, LinkFieldCheck>();
        }


        #endregion
        

        /// <summary>
        /// 执行器
        /// </summary>
        public override bool Execute(EntityConfig entity)
        {
            StateMessage = "正在检查:" + entity.Caption + "...";
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
            StateMessage = "检查完成:" + entity.Caption;
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