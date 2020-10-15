using System.ComponentModel.Composition;
using Agebull.Common;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.EasyUi
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class ProjectApiBuildeRegister : IAutoRegister
    {
        /// <summary>
        /// 执行自动注册
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            NormalCodeModel.RegistBuilder<ProjectApiBuilde<EntityConfig>, EntityConfig>();
            NormalCodeModel.RegistBuilder<ProjectApiBuilde<ModelConfig>, ModelConfig>();
        }

    }
    public sealed class ProjectApiBuilde<TModelConfig> : ProjectBuilder<TModelConfig>
            where TModelConfig : ProjectChildConfigBase, IEntityConfig
    {
        /// <summary>
        /// 名称
        /// </summary>
        public override string Name => "Edit Api";

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
        public override void CreateModelCode(ProjectConfig project, TModelConfig schema)
        {
            var cls = schema.Classify.IsEmptyClassify() ? null : schema.Classify;

            var businessPath = IOHelper.CheckPath(project.ModelPath, "Business");
            if (cls != null)
                businessPath = IOHelper.CheckPath(businessPath, cls);
            var builder = new BusinessBuilder<TModelConfig>
            {
                Model = schema,
                Project = project
            };
            builder.WriteDesignerCode(businessPath);
            builder.WriteCustomCode(businessPath);

            var apiPath = project.ApiPath;
            if (cls != null)
                apiPath = IOHelper.CheckPath(apiPath, cls);
            var pg = new ProjectApiActionCoder<TModelConfig>
            {
                Model = schema,
                Project = schema.Parent,
            };
            pg.WriteDesignerCode(apiPath);
            pg.WriteCustomCode(apiPath);
        }
    }
}