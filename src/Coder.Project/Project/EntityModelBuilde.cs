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
            var dbPath = IOHelper.CheckPath(project.ModelPath, "DataAccess", "DataBase");
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
            if (schema.IsClass)
                return;
            var rootPath = project.ModelPath;
            var entityPath = IOHelper.CheckPath(rootPath, "Entity");
            Message = entityPath;
            CreateCode<EntityBuilder>(project, schema, IOHelper.CheckPath(entityPath, "Model"));

            CreateCode<EntityValidateBuilder>(project, schema, entityPath);
            
            var accessPath = IOHelper.CheckPath(rootPath, "DataAccess", "DataAccess");
            Message = accessPath;

            if (project.DbType == DataBaseType.MySql)
            {
                CreateCode<MySqlAccessBuilder>(project, schema, accessPath);
            }
            else
            {
                CreateCode<SqlServerAccessBuilder>(project, schema, IOHelper.CheckPath(accessPath, "DataAccess"));
            }
            var blPath = IOHelper.CheckPath(rootPath, "Business", "EntityModel");
            CreateCode<BusinessBuilder>(project, schema, blPath);
            Message = blPath;
        }

    }
}
