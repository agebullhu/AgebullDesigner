using Agebull.Common;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;
using System.ComponentModel.Composition;

namespace Agebull.EntityModel.RobotCoder.WebApi
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class EntityModelBuilderRegister : IAutoRegister
    {
        /// <summary>
        /// ִ���Զ�ע��
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            CoderManager.RegistBuilder<EntityModelBuilder>();
        }
    }
    public sealed class EntityModelBuilder : ProjectBuilder
    {
        /// <summary>
        /// ����
        /// </summary>
        public override string Name => "ʵ��ģ��";

        public EntityModelBuilder()
        {
            Icon = "C#";
        }

        /// <summary>
        /// ������Ŀ����
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
            var enums = new CommonBuilder
            {
                Project = project
            };
            enums.WriteDesignerCode(project.ModelPath);
        }

        /// <summary>
        /// ����
        /// </summary>
        public override string Caption => Name;

        /// <summary>
        /// ����ʵ�����
        /// </summary>
        /// <param name="project"></param>
        /// <param name="schema"></param>
        public override void CreateModelCode(ProjectConfig project, IEntityConfig schema)
        {
            var cls = project.NoClassify || schema.Classify.IsEmptyClassify() ? null : schema.Classify;

            var root = project.ModelPath;
            var entityPath = IOHelper.CheckPath(root, "Entity");
            Message = entityPath;
            if (cls != null)
            {
                entityPath = IOHelper.CheckPath(entityPath, cls);
            }
            CreateCode<EntityBuilder>(project, schema, entityPath);


            if (schema.EnableValidate)
            {
                entityPath = IOHelper.CheckPath(root, "Validate");
                if (cls != null)
                {
                    entityPath = IOHelper.CheckPath(entityPath, cls);
                }
                CreateCode<EntityValidateBuilder>(project, schema, entityPath);
            }

            if (!schema.EnableDataBase)
                return;
            var accessPath = IOHelper.CheckPath(root, "DataAccess");
            if (cls != null)
                accessPath = IOHelper.CheckPath(accessPath, cls);
            Message = accessPath;

            switch (project.DbType)
            {
                //case DataBaseType.SqlServer:
                //    CreateCode<SqlServerAccessBuilder>(project, schema, accessPath);
                //    break;
                //case DataBaseType.Sqlite:
                //    CreateCode<SqliteAccessBuilder>(project, schema, accessPath);
                //    break;
                default:
                    CreateCode<MySqlAccessBuilder>(project, schema, accessPath);
                    break;
            }
        }
    }
}
