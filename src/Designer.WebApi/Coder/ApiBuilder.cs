using System.ComponentModel.Composition;
using Agebull.Common;
using Agebull.EntityModel.Config;
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
            Message = project.Caption;
            {
                var path = project.GetApiPath("Contract");
                var builder = new ApiInterfaceBuilder
                {
                    Project = project
                };
                builder.CreateExtendCode(path);
            }
            {
                var path = project.GetApiPath("Contract");
                var builder = new ApiProxyBuilder
                {
                    Project = project
                };
                builder.CreateExtendCode(path);
            }
            {
                var path = project.GetApiPath("WebApi");
                var builder = new ApiControlerBuilder
                {
                    Project = project
                };
                builder.CreateExtendCode(path);
            }
            {
                var path = project.GetApiPath("Logical");
                var builder = new ApiLogicalBuilder
                {
                    Project = project
                };
                builder.CreateExtendCode(path);
            }
            {
                var path = project.GetApiPath("WebApi");
                var builder = new ApiControlerBuilder
                {
                    Project = project
                };
                builder.CreateExtendCode(path);
            }
            {
                var path = GetDocumentPath(project);
                var builder = new ApiMarkBuilder
                {
                    Project = project
                };
                builder.CreateBaseCode(path);
                builder.CreateExtendCode(path);
            }
        }
        /// <summary>
        /// ����ʵ�����
        /// </summary>
        /// <param name="project"></param>
        /// <param name="schema"></param>
        public override void CreateEntityCode(ProjectConfig project, EntityConfig schema)
        {
            Message = schema.Caption;
            {
                var path = project.GetApiPath("Contract");
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
                var path = project.GetApiPath("Contract");
                builder.CreateBaseCode(path);
            }
            {
                var path = project.GetApiPath("Contract");
                var builder = new ApiProxyBuilder
                {
                    Project = project,
                    Entity = schema
                };
                builder.CreateBaseCode(path);
            }

            {
                var path = project.GetApiPath("Logical");
                var builder = new ApiLogicalBuilder
                {
                    Project = project,
                    Entity = schema
                };
                builder.CreateBaseCode(path);
            }
            {
                var path = project.GetApiPath("WebApi");
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
