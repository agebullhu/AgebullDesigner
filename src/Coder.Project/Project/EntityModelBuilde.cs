using System.ComponentModel.Composition;
using System.IO;
using Agebull.Common;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.WebApi
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class EntityModelBuilde : ProjectBuilder, IAutoRegister
    {
        /// <summary>
        /// 执行自动注册
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            RegistBuilder<EntityModelBuilde>();
        }

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
        protected override string Name => "Entity & Model";

        /// <summary>
        /// 标题
        /// </summary>
        public override string Caption => Name;

        /// <summary>
        /// 生成实体代码
        /// </summary>
        /// <param name="project"></param>
        /// <param name="schema"></param>
        public override void CreateEntityCode(ProjectConfig project, ModelConfig schema)
        {
            if (schema.NoDataBase)
                return;
            var cls = schema.Classify.IsEmptyClassify() ? null : schema.Classify;

            var root = project.ModelPath;
            var entityPath = IOHelper.CheckPath(root, "Entity");
            Message = entityPath;
            if (cls != null)
            {
                entityPath = IOHelper.CheckPath(entityPath, cls);
            }
            CreateCode<EntityBuilder<ModelConfig>, ModelConfig>(project, schema, entityPath);
            entityPath = IOHelper.CheckPath(root, "Validate");
            if (cls != null)
            {
                entityPath = IOHelper.CheckPath(entityPath, cls);
            }
            CreateCode<EntityValidateBuilder<ModelConfig>, ModelConfig>(project, schema, entityPath);

            var accessPath = IOHelper.CheckPath(root, "DataAccess");
            if (cls != null)
                accessPath = IOHelper.CheckPath(accessPath, cls);
            Message = accessPath;

            switch (project.DbType)
            {
                case DataBaseType.SqlServer:
                    CreateCode<SqlServerAccessBuilder<ModelConfig>, ModelConfig>(project, schema, accessPath);
                    break;
                case DataBaseType.Sqlite:
                    CreateCode<SqliteAccessBuilder<ModelConfig>, ModelConfig>(project, schema, accessPath);
                    break;
                default:
                    CreateCode<MySqlAccessBuilder<ModelConfig>, ModelConfig>(project, schema, accessPath);
                    break;
            }
            var blPath = IOHelper.CheckPath(root, "Business");
            if (cls != null)
                blPath = IOHelper.CheckPath(blPath, cls);
            CreateCode<BusinessBuilder<ModelConfig>, ModelConfig>(project, schema, blPath);
            Message = blPath;
        }
    }
}
