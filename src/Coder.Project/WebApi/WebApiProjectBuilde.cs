using System.ComponentModel.Composition;
using Agebull.Common;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.WebApi
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class WebApiProjectBuilde : ProjectBuilder, IAutoRegister
    {
        /// <summary>
        /// 执行自动注册
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            RegistBuilder<WebApiProjectBuilde>();
        }

        /// <summary>
        /// 生成项目代码
        /// </summary>
        /// <param name="project"></param>
        public override void CreateProjectCode(ProjectConfig project)
        {
            var dbPath = IOHelper.CheckPath(project.ModelPath, "DataBase");
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
        protected override string Name => "WebApi";

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
            Message = schema.Caption;
            var rootPath = RootPath(project);
            var entityPath = IOHelper.CheckPath(rootPath, "Entity");
            CreateCode<EntityBuilder>(project, schema, IOHelper.CheckPath(entityPath, "Model"));

            if (schema.IsClass)
                return;
            CreateCode<EntityValidateBuilder>(project, schema, IOHelper.CheckPath(entityPath, "Extend"));

            var accessPath = IOHelper.CheckPath(rootPath, "DataAccess");

            if (project.DbType == DataBaseType.MySql)
            {
                CreateCode<MySqlAccessBuilder>(project, schema, IOHelper.CheckPath(accessPath, "DataAccess"));
            }
            else
            {
                CreateCode<SqlServerAccessBuilder>(project, schema, IOHelper.CheckPath(accessPath, "DataAccess"));
            }
            CreateCode<BusinessBuilder>(project, schema, IOHelper.CheckPath(accessPath, "DataBusiness"));
        }

    }
}
