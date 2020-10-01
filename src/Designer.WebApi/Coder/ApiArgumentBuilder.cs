using System.ComponentModel.Composition;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer.WebApi
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class EntityModelBuilderRegister : IAutoRegister
    {
        /// <summary>
        /// 执行自动注册
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            NormalCodeModel.RegistBuilder<ApiArgumentBuilder<EntityConfig>, EntityConfig>();
            NormalCodeModel.RegistBuilder<ApiArgumentBuilder<ModelConfig>, ModelConfig>();
        }

    }
    public sealed class ApiArgumentBuilder<TModelConfig> : ProjectBuilder<TModelConfig>
            where TModelConfig : ProjectChildConfigBase, IEntityConfig
    {

        /// <summary>
        /// 名称
        /// </summary>
        public override string Name => "Api Argument";

        /// <summary>
        /// 标题
        /// </summary>
        public override string Caption => "API参数";

        /// <summary>
        /// 生成项目代码
        /// </summary>
        /// <param name="project"></param>
        public override void CreateProjectCode(ProjectConfig project)
        {
            var path = GlobalConfig.CheckPath(project.FormatPath("Contract"));
            var eb = new EnumBuilder
            {
                Project = project
            };
            eb.WriteDesignerCode(path);
        }
        /// <summary>
        /// 生成实体代码
        /// </summary>
        /// <param name="project"></param>
        /// <param name="entity"></param>
        public override void CreateModelCode(ProjectConfig project, TModelConfig entity)
        {
            Message = entity.Caption;
            var path = GlobalConfig.CheckPath(project.FormatPath("Contract"));
            var builder = new EntityBuilder<TModelConfig>
            {
                Project = project,
                Model = entity
            };
            builder.WriteDesignerCode(path);
            builder.WriteCustomCode(path);
            
        }
    }
}