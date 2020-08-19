using System.ComponentModel.Composition;
using Agebull.Common;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.EasyUi
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class ProjectApiBuilde : ProjectBuilder, IAutoRegister
    {
        /// <summary>
        /// 名称
        /// </summary>
        protected override string Name => "Edit Api";

        /// <summary>
        /// 标题
        /// </summary>
        public override string Caption => Name;
        /// <summary>
        /// 执行自动注册
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            RegistBuilder<ProjectApiBuilde>();
        }

        /// <summary>
        /// 生成项目代码
        /// </summary>
        /// <param name="project"></param>
        public override void CreateProjectCode(ProjectConfig project)
        {

        }

        /// <summary>
        /// 生成实体代码
        /// </summary>
        /// <param name="project"></param>
        /// <param name="schema"></param>
        public override void CreateEntityCode(ProjectConfig project, EntityConfig schema)
        {
            var cls = schema.Classify.IsEmptyClassify() ? null : schema.Classify;

            var businessPath = IOHelper.CheckPath(project.ModelPath, "Business");
            if(cls != null)
                businessPath = IOHelper.CheckPath(businessPath, cls);
            var builder = new BusinessBuilder
            {
                Entity = schema,
                Project = project
            };
            builder.CreateBaseCode(businessPath);
            builder.CreateExtendCode(businessPath);

            var apiPath = project.ApiPath;
            if (cls != null)
                apiPath = IOHelper.CheckPath(apiPath, cls);
            var pg = new ProjectApiActionCoder
            {
                Entity = schema,
                Project = schema.Parent,
            };
            pg.CreateBaseCode(apiPath);
            pg.CreateExtendCode(apiPath);
        }
    }
}