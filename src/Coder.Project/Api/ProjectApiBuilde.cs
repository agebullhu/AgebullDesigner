using System.ComponentModel.Composition;
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
            var pg = new ProjectApiActionCoder
            {
                Entity = schema,
                Project = schema.Parent,
            };
            pg.CreateBaseCode(project.ApiPath);
            pg.CreateExtendCode(project.ApiPath);
        }
    }
}