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
            var dbPath = GlobalConfig.CheckPath(project.ModelPath, "DataAccess", "DataBase");
            var db = new DataBaseBuilder
            {
                Project = project
            };
            db.CreateBaseCode(dbPath);
            db.CreateExtendCode(dbPath);
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
        public override void CreateEntityCode(ProjectConfig project, EntityConfig schema)
        {
            if (schema.NoDataBase)
                return;
            var cls = schema.Classify;
            if (cls == "数据实体" || string.IsNullOrWhiteSpace(cls))
                cls = null;
            var root = project.ModelPath;
            var entityPath = IOHelper.CheckPath(root, "Entity");
            Message = entityPath;
            if (cls != null)
            {
                entityPath = IOHelper.CheckPath(entityPath,cls);
            }
            CreateCode<EntityBuilder>(project, schema, IOHelper.CheckPath(entityPath, "Model"));
            if (Solution.HaseValidateCode)
                CreateCode<EntityValidateBuilder>(project, schema, IOHelper.CheckPath(entityPath, "Validate"));

            var accessPath = IOHelper.CheckPath(root, "DataAccess");
            accessPath = IOHelper.CheckPath(accessPath, cls ?? "DataAccess");
            Message = accessPath;

            switch (project.DbType)
            {
                case DataBaseType.SqlServer:
                    CreateCode<SqlServerAccessBuilder>(project, schema, accessPath);
                    break;
                case DataBaseType.Sqlite:
                    CreateCode<SqliteAccessBuilder>(project, schema, accessPath);
                    break;
                default:
                    CreateCode<MySqlAccessBuilder>(project, schema, accessPath);
                    break;
            }
            var blPath = IOHelper.CheckPath(root, "Business");
            blPath = IOHelper.CheckPath(blPath, cls ?? "None");
            CreateCode<BusinessBuilder>(project, schema, blPath);
            Message = blPath;
        }
    }
}
