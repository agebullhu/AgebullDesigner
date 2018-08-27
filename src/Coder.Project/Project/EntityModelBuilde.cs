using System.ComponentModel.Composition;
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
        /// ִ���Զ�ע��
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            RegistBuilder<EntityModelBuilde>();
        }

        /// <summary>
        /// ������Ŀ����
        /// </summary>
        /// <param name="project"></param>
        public override void CreateProjectCode(ProjectConfig project)
        {
            var dbPath = GlobalConfig.CheckPath(project.ModelPath,"DataAccess", "DataBase");
            var db = new DataBaseBuilder
            {
                Project = project
            };
            db.CreateBaseCode(dbPath);
            db.CreateExtendCode(dbPath);
        }

        /// <summary>
        /// ����
        /// </summary>
        protected override string Name => "Entity & Model";

        /// <summary>
        /// ����
        /// </summary>
        public override string Caption => Name;

        /// <summary>
        /// ����ʵ�����
        /// </summary>
        /// <param name="project"></param>
        /// <param name="schema"></param>
        public override void CreateEntityCode(ProjectConfig project, EntityConfig schema)
        {
            if (schema.NoDataBase)
                return;
            var entityPath = project.GetModelPath("Entity");
            Message = entityPath;
            CreateCode<EntityBuilder>(project, schema, entityPath);

            CreateCode<EntityValidateBuilder>(project, schema, entityPath);

            var accessPath = project.GetModelPath("DataAccess", "DataAccess");
            Message = accessPath;

            if (project.DbType == DataBaseType.MySql)
            {
                CreateCode<MySqlAccessBuilder>(project, schema, accessPath);
            }
            else
            {
                CreateCode<SqlServerAccessBuilder>(project, schema, accessPath);
            }
            var blPath = project.GetModelPath("Business");
            CreateCode<BusinessBuilder>(project, schema, blPath);
            Message = blPath;
        }

    }
}
