using System.ComponentModel.Composition;
using Agebull.Common;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class WebAppProjectBuilde : ProjectBuilder, IAutoRegister
    {

        /// <summary>
        /// ����
        /// </summary>
        protected override string Name => "WebApp";

        /// <summary>
        /// ����
        /// </summary>
        public override string Caption => Name;
        /// <summary>
        /// ִ���Զ�ע��
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            RegistBuilder<WebAppProjectBuilde>();
        }

        /// <summary>
        /// ׼������ʵ�����
        /// </summary>
        /// <param name="schema"></param>
        public override bool Validate(EntityConfig schema)
        {
            var model = new EntityValidater
            {
                Entity = schema
            };
            return model.Validate(TraceMessage);
        }

        /// <summary>
        /// ������Ŀ����
        /// </summary>
        /// <param name="project"></param>
        public override void CreateProjectCode(ProjectConfig project)
        {
            var dbPath = GlobalConfig.CheckPath(project.ModelPath, "DataBase");
            var db = new DataBaseBuilder
            {
                Project = project
            };
            db.CreateBaseCode(dbPath);
            db.CreateExtendCode(dbPath);
        }

        /// <summary>
        /// ����ʵ�����
        /// </summary>
        /// <param name="project"></param>
        /// <param name="schema"></param>
        public override void CreateEntityCode(ProjectConfig project, EntityConfig schema)
        {
            Message = schema.Caption;
            var path = project.ModelPath;
            var entityPath = GlobalConfig.CheckPath(path, "DataModel");
            {
                var builder = new EntityBuilder
                {
                    Entity = schema,
                    Project = project
                };
                builder.CreateBaseCode(entityPath);
                builder.CreateExtendCode(entityPath);
            }
            if(Solution.HaseValidateCode)
            {
                var exPath = GlobalConfig.CheckPath(path, "Validate");
                var builder = new EntityValidateBuilder
                {
                    Entity = schema,
                    Project = project
                };
                builder.CreateBaseCode(exPath);
                builder.CreateExtendCode(exPath);
            }
            if (schema.NoDataBase)
                return;
            var coPath = GlobalConfig.CheckPath(path, "Combo");
            {
                var builder = new EntityComboBuilder
                {
                    Entity = schema,
                    Project = project
                };
                builder.CreateExtendCode(coPath);
            }

            var accessPath = GlobalConfig.CheckPath(path, "DataAccess");
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
            //if (!string.IsNullOrWhiteSpace(project.BusinessPath))
            {
                var businessPath =
                    GlobalConfig.CheckPath(
                        GlobalConfig.CheckPath(Solution.IsWeb ? project.ModelPath : project.BusinessPath,
                            "Business"));
                var builder = new BusinessBuilder
                {
                    Entity = schema,
                    Project = project
                };
                builder.CreateBaseCode(businessPath);
                builder.CreateExtendCode(businessPath);
            }
            
        }
    }
}
