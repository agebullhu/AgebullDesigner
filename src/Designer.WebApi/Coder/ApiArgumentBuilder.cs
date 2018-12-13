using System.ComponentModel.Composition;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer.WebApi
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class ApiArgumentBuilder : ProjectBuilder, IAutoRegister
    {

        /// <summary>
        /// ����
        /// </summary>
        protected override string Name => "Api Argument";

        /// <summary>
        /// ����
        /// </summary>
        public override string Caption => "API����";

        /// <summary>
        /// ִ���Զ�ע��
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            RegistBuilder<ApiArgumentBuilder>();
        }

        /// <summary>
        /// ������Ŀ����
        /// </summary>
        /// <param name="project"></param>
        public override void CreateProjectCode(ProjectConfig project)
        {
            var path = GlobalConfig.CheckPath(project.FormatPath("Contract"));
            var eb = new EnumBuilder
            {
                Project = project
            };
            eb.CreateBaseCode(path);
        }
        /// <summary>
        /// ����ʵ�����
        /// </summary>
        /// <param name="project"></param>
        /// <param name="entity"></param>
        public override void CreateEntityCode(ProjectConfig project, EntityConfig entity)
        {
            Message = entity.Caption;
            var path = GlobalConfig.CheckPath(project.FormatPath("Contract"));
            var builder = new EntityBuilder
            {
                Project = project,
                Entity = entity
            };
            builder.CreateBaseCode(path);
            builder.CreateExtendCode(path);
            
        }
    }
}