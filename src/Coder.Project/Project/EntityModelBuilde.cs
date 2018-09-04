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
            var dbPath = GlobalConfig.CheckPath(project.ModelPath, "DataAccess", "DataBase");
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
            var cls = schema.Classify;
            if (cls == "����ʵ��" || string.IsNullOrWhiteSpace(cls))
                cls = null;
            var root = project.ModelPath;
            var entityPath = IOHelper.CheckPath(root, "Entity");
            Message = entityPath;
            if (cls != null)
            {
                entityPath = IOHelper.CheckPath(entityPath,cls);
            }
            CreateCode<EntityBuilder>(project, schema, IOHelper.CheckPath(entityPath, "Model"));
            CreateCode<EntityValidateBuilder>(project, schema, IOHelper.CheckPath(entityPath, "Validate"));

            var accessPath = IOHelper.CheckPath(root, "DataAccess");
            if (cls != null)
            {
                accessPath = IOHelper.CheckPath(accessPath,cls);
            }
            else
            {
                accessPath = IOHelper.CheckPath(accessPath, "DataAccess");
            }
            Message = accessPath;

            if (project.DbType == DataBaseType.MySql)
            {
                CreateCode<MySqlAccessBuilder>(project, schema, accessPath);
            }
            else
            {
                CreateCode<SqlServerAccessBuilder>(project, schema, accessPath);
            }
            var blPath = IOHelper.CheckPath(root, "Business");
            if (cls != null)
            {
                blPath = IOHelper.CheckPath(blPath,cls);
            }
            else
            {
                blPath = IOHelper.CheckPath(blPath, "Logical");
            }
            CreateCode<BusinessBuilder>(project, schema, blPath);
            Message = blPath;
        }
    }
}
