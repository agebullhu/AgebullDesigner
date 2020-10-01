using System.ComponentModel.Composition;
using Agebull.Common;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer.WebApi
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class WebApiBuilder : IAutoRegister
    {
        /// <summary>
        /// 执行自动注册
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            NormalCodeModel.RegistBuilder<WebApiBuilder<EntityConfig>, EntityConfig>();
            NormalCodeModel.RegistBuilder<WebApiBuilder<ModelConfig>, ModelConfig>();
        }

    }
    public sealed class WebApiBuilder<TModelConfig> : ProjectBuilder<TModelConfig>
            where TModelConfig : ProjectChildConfigBase, IEntityConfig
    {

        /// <summary>
        /// 名称
        /// </summary>
        public override string Name => "Web Api";

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
            Message = project.Caption;
            {
                var path = project.GetApiPath("Contract");
                var builder = new ApiInterfaceBuilder<TModelConfig>
                {
                    Project = project
                };
                builder.WriteCustomCode(path);
            }
            {
                var path = project.GetApiPath("Contract");
                var builder = new ApiProxyBuilder<TModelConfig>
                {
                    Project = project
                };
                builder.WriteCustomCode(path);
            }
            {
                var path = project.GetApiPath("Logical");
                var builder = new ApiControlerBuillder<TModelConfig>
                {
                    Project = project
                };
                builder.WriteCustomCode(path);
            }
            {
                var path = project.GetApiPath("Logical");
                var builder = new ApiLogicalBuilder<TModelConfig>
                {
                    Project = project
                };
                builder.WriteCustomCode(path);
            }
            {
                var path = GetDocumentPath(project);
                var builder = new ApiMarkBuilder<TModelConfig>
                {
                    Project = project
                };
                builder.WriteCustomCode(path);
            }
            {
                string path;
                if (!string.IsNullOrWhiteSpace(project.ModelFolder))
                {
                    var folders = project.ModelFolder.Split('\\');
                    path = folders.Length == 1
                        ? project.GetPath("Test", "UnitTest")
                        : project.GetPath(folders[0], "Test", "UnitTest");
                }
                else
                {
                    path = project.GetPath("Test", "UnitTest");
                }
                var builder = new UnitTestBuilder<ModelConfig>
                {
                    Project = project
                };
                builder.WriteCustomCode(path);
            }
        }
        /// <summary>
        /// 生成实体代码
        /// </summary>
        /// <param name="project"></param>
        /// <param name="entity"></param>
        public override void CreateModelCode(ProjectConfig project, TModelConfig entity)
        {
            if (entity.ExtendConfigListBool["NoApi"])
                return;
            Message = entity.Caption;
            {
                var path = project.GetApiPath("Contract");
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
}
