using System.ComponentModel.Composition;
using System.IO;
using Agebull.Common;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.WebApi
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class EntityModelBuilderRegister : IAutoRegister
    {
        /// <summary>
        /// 执行自动注册
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            NormalCodeModel.RegistBuilder<EntityModelBuilder<EntityConfig>, EntityConfig>();
            NormalCodeModel.RegistBuilder<EntityModelBuilder<ModelConfig>, ModelConfig>();
        }

    }
    public sealed class EntityModelBuilder<TModelConfig> : ProjectBuilder<TModelConfig>
            where TModelConfig : ProjectChildConfigBase, IEntityConfig
    {
        /// <summary>
        /// 生成项目代码
        /// </summary>
        /// <param name="project"></param>
        public override void CreateProjectCode(ProjectConfig project)
        {
            var dbPath = GlobalConfig.CheckPath(project.ModelPath, "DataAccess");
            var db = new DataBaseBuilder
            {
                Project = project
            };
            db.WriteDesignerCode(dbPath);
            db.WriteCustomCode(dbPath);
        }

        /// <summary>
        /// 名称
        /// </summary>
        public override string Name => "Entity & Model";

        /// <summary>
        /// 标题
        /// </summary>
        public override string Caption => Name;

        /// <summary>
        /// 生成实体代码
        /// </summary>
        /// <param name="project"></param>
        /// <param name="schema"></param>
        public override void CreateModelCode(ProjectConfig project, TModelConfig schema)
        {
            if (schema.NoDataBase)
                return;
            var cls = project.NoClassify || schema.Classify.IsEmptyClassify() ? null : schema.Classify;

            var root = project.ModelPath;
            var entityPath = IOHelper.CheckPath(root, "Entity");
            Message = entityPath;
            if (cls != null)
            {
                entityPath = IOHelper.CheckPath(entityPath, cls);
            }
            CreateCode<EntityBuilder<TModelConfig>>(project, schema, entityPath);
            entityPath = IOHelper.CheckPath(root, "Validate");
            if (cls != null)
            {
                entityPath = IOHelper.CheckPath(entityPath, cls);
            }
            CreateCode<EntityValidateBuilder<TModelConfig>>(project, schema, entityPath);

            var accessPath = IOHelper.CheckPath(root, "DataAccess");
            if (cls != null)
                accessPath = IOHelper.CheckPath(accessPath, cls);
            Message = accessPath;

            switch (project.DbType)
            {
                case DataBaseType.SqlServer:
                    CreateCode<SqlServerAccessBuilder<TModelConfig>>(project, schema, accessPath);
                    break;
                case DataBaseType.Sqlite:
                    CreateCode<SqliteAccessBuilder<TModelConfig>>(project, schema, accessPath);
                    break;
                default:
                    CreateCode<MySqlAccessBuilder<TModelConfig>>(project, schema, accessPath);
                    break;
            }
        }
    }
}
