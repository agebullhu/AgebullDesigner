using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;
using System.ComponentModel.Composition;

namespace Agebull.EntityModel.RobotCoder.VueComponents
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]

    internal class VUEBuilderRegister : IAutoRegister
    {
        /// <summary>
        /// 执行自动注册
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            CoderManager.RegistBuilder<VUEBuilder>();
        }
    }

    public class VUEBuilder : ProjectBuilder
    {
        public VUEBuilder()
        {
            Icon = "HTML";
        }
        /// <summary>
        /// 名称
        /// </summary>
        public override string Name => "前端界面";

        /// <summary>
        /// 标题
        /// </summary>
        public override string Caption => Name;

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
        public override void CreateModelCode(ProjectConfig project, IEntityConfig entity)
        {
            if (!entity.EnableUI)
                return;
            var pg = new PorjectVUEGenerator
            {
                Model = entity,
                Project = entity.Project,
            };
            pg.WriteDesignerCode(project.PagePath);
            pg.WriteCustomCode(project.PagePath);
        }
    }
}