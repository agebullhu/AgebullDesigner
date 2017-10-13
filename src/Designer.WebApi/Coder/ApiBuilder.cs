using System.ComponentModel.Composition;
using System.IO;
using Agebull.Common;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer.WebApi
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class ApiBuilder : ProjectBuilder, IAutoRegister
    {

        /// <summary>
        /// ����
        /// </summary>
        protected override string Name => "Entity Api";

        /// <summary>
        /// ����
        /// </summary>
        public override string Caption => Name;

        /// <summary>
        /// ִ���Զ�ע��
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            RegistBuilder<ApiBuilder>();
        }

        /// <summary>
        /// ������Ŀ����
        /// </summary>
        /// <param name="project"></param>
        public override void CreateProjectCode(ProjectConfig project)
        {
            var root = IOHelper.CheckPath(Path.GetDirectoryName(project.ModelPath), "Contract");
        }
        /// <summary>
        /// ����ʵ�����
        /// </summary>
        /// <param name="project"></param>
        /// <param name="schema"></param>
        public override void CreateEntityCode(ProjectConfig project, EntityConfig schema)
        {
            Message = schema.Caption;
            var root = IOHelper.CheckPath(Path.GetDirectoryName(Path.GetDirectoryName(project.ModelPath)),"Api");
            {
                var path = IOHelper.CheckPath(root, "Contract", "Entity");
                var builder = new EntityBuilder
                {
                    Project = project,
                    Entity = schema 
                };
                builder.CreateBaseCode(path);
                builder.CreateExtendCode(path);
            }
            {
                var builder = new ApiInterfaceBuilder
                {
                    Project = project,
                    Entity = schema
                };
                var path = IOHelper.CheckPath(root, "Contract", "Interface");
                builder.CreateBaseCode(path);
                path = IOHelper.CheckPath(root, "Proxy");
                builder.CreateExtendCode(path);
            }
            {
                var path = IOHelper.CheckPath(root, "Logical");
                var builder = new ApiLogicalBuilder
                {
                    Project = project,
                    Entity = schema
                };
                builder.CreateBaseCode(path);
            }
            {
                var path = IOHelper.CheckPath(root, "WebApi", "Controllers");
                var builder = new ApiControlerBuilder
                {
                    Project = project,
                    Entity = schema
                };
                builder.CreateBaseCode(path);
            }

        }
        
    }
}
