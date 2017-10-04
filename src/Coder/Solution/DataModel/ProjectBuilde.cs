using Agebull.Common.SimpleDesign.AspNet;
using Gboxt.Common.DataAccess.Schemas;

namespace Agebull.Common.SimpleDesign.WebApplition
{
    internal sealed class ProjectBuilde : ProjectBuilder
    {
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
        /// 生成实体代码
        /// </summary>
        /// <param name="project"></param>
        /// <param name="schema"></param>
        public override void CreateEntityCode(ProjectConfig project, EntityConfig schema)
        {
            Message = schema.Caption;
            var path = project.ModelPath;
            var entityPath = IOHelper.CheckPath(path, "DataModel");
            {
                var builder = new EntityBuilder
                {
                    Entity = schema,
                    Project = project
                };
                builder.CreateBaseCode(entityPath);
                builder.CreateExtendCode(entityPath);
            }
            if (schema.IsClass)
                return;
            var exPath = IOHelper.CheckPath(path, "Extend");
            {
                var builder = new EntityExtendBuilder
                {
                    Entity = schema,
                    Project = project
                };
                builder.CreateBaseCode(exPath);
                builder.CreateExtendCode(exPath);
            }
            var coPath = IOHelper.CheckPath(path, "Combo");
            {
                var builder = new EntityComboBuilder
                {
                    Entity = schema,
                    Project = project
                };
                builder.CreateExtendCode(coPath);
            }

            var accessPath = IOHelper.CheckPath(path, "DataAccess");
            if (project.DbType == DataBaseType.MySql)
            {
                var builder = new MySqlAccessBuilder
                {
                    Entity = schema,
                    Project = project
                };
                builder.CreateBaseCode(accessPath);
                builder.CreateExtendCode(accessPath);
            }
            else
            {
                var builder = new SqlServerAccessBuilder
                {
                    Entity = schema,
                    Project = project
                };
                builder.CreateBaseCode(accessPath);
                builder.CreateExtendCode(accessPath);
            }
            //if (!string.IsNullOrEmpty(project.BusinessPath))
            {
                var businessPath =
                    IOHelper.CheckPath(
                        IOHelper.CheckPath(Solution.IsWeb ? project.ModelPath : project.BusinessPath,
                            "Business"));
                var builder = new BusinessBuilder
                {
                    Entity = schema,
                    Project = project
                };
                builder.CreateBaseCode(businessPath);
                builder.CreateExtendCode(businessPath);
            }

            var pg = new PageGenerator
            {
                Entity = schema,
                Project = project
            };
            pg.CreateExtendCode(IOHelper.CheckPath(Solution.IsWeb ? project.ModelPath : project.BusinessPath));
            pg.CreateBaseCode(project.PagePath);
        }
    }
}
