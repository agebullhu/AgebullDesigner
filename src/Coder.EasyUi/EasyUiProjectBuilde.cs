using System.ComponentModel.Composition;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;
using Agebull.EntityModel.RobotCoder.AspNet;

namespace Agebull.EntityModel.RobotCoder.EasyUi
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class EasyUiProjectBuilde : ProjectBuilder, IAutoRegister
    {
        /// <summary>
        /// 名称
        /// </summary>
        protected override string Name => "EasyUi";

        /// <summary>
        /// 标题
        /// </summary>
        public override string Caption => Name;
        /// <summary>
        /// 执行自动注册
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            RegistBuilder<EasyUiProjectBuilde>();
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
            if (schema.IsInternal)
            {
                TraceMessage.Track = $"{schema.Caption}:内部类,跳过界面生成";
                return;
            }
            EasyUiModel.CheckExport(schema, false);
            var pg = new PageGenerator
            {
                Entity = schema,
                Project = schema.Parent,
            };
            pg.CreateBaseCode(project.PagePath);
            pg.CreateExtendCode(project.ApiPath);
        }
    }
}