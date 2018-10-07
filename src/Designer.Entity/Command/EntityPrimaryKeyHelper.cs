using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.Common.Config.Designer
{
    /// <summary>
    /// 规范实体主键
    /// </summary>
    //[Export(typeof(IAutoRegister))]
    //[ExportMetadata("Symbol", '%')]
    internal sealed class EntityPrimaryKeyHelper : EntityCommandBase, IAutoRegister
    {
        
        #region 注册

        public EntityPrimaryKeyHelper()
        {
            Name = "规范实体主键";
            Caption = "规范实体主键";
            Catalog = "工具";
            ViewModel = "database";
            TargetType = typeof(EntityConfig);
        }

        /// <summary>
        /// 注册代码
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            CommandCoefficient.RegisterCommand<EntityPrimaryKeyHelper>();
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
            if (entity.Properties.Any(p => p.DbFieldName == "number_id"))
            {
                Trace.WriteLine($@"ALTER TABLE dbo.{entity.SaveTableName} ADD [number_id] int NOT NULL IDENTITY (1, 1);");
                Trace.WriteLine("GO");
            }
            if (entity.PrimaryColumn != null && (entity.PrimaryColumn.CsType == "int"|| entity.PrimaryColumn.CsType == "long"))
                return true;
            entity.Properties.Foreach(p => p.IsPrimaryKey = false);
            var pri = entity.Properties.FirstOrDefault(p => p.IsIdentity);
            if (pri != null)
            {
                pri.IsPrimaryKey = true;
                return true;
            }
            Trace.WriteLine($@"ALTER TABLE dbo.{entity.SaveTableName} ADD [number_id] int NOT NULL IDENTITY (1, 1);");
            Trace.WriteLine("GO");
            entity.Add(new PropertyConfig
            {
                Name = "NumberId",
                Caption = "数字ID",
                IsPrimaryKey = true,
                IsIdentity = true,
                CsType = "int",
                DbFieldName = "number_id",
                DbType = "int"
            });
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