using System.ComponentModel.Composition;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer.WebApi
{
    //[Export(typeof(IAutoRegister))]
    //[ExportMetadata("Symbol", '%')]
    internal sealed class EntityApiBuilder : ProjectBuilder, IAutoRegister
    {

        /// <summary>
        /// 名称
        /// </summary>
        protected override string Name => "Entity Api";

        /// <summary>
        /// 标题
        /// </summary>
        public override string Caption => Name;

        /// <summary>
        /// 执行自动注册
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            RegistBuilder<EntityApiBuilder>();
        }

        /// <summary>
        /// 生成项目代码
        /// </summary>
        /// <param name="project"></param>
        public override void CreateProjectCode(ProjectConfig project)
        {
            Message = project.Caption;
            {
                var path = project.GetApiPath("Contract");
                var builder = new ApiInterfaceBuilder<ModelConfig>
                {
                    Project = project
                };
                builder.WriteCustomCode(path);
            }
            {
                var path = project.GetApiPath("Contract");
                var builder = new ApiProxyBuilder<ModelConfig>
                {
                    Project = project
                };
                builder.WriteCustomCode(path);
            }
            {
                var path = project.GetApiPath("WebApi");
                var builder = new ApiControlerBuillder<ModelConfig>
                {
                    Project = project
                };
                builder.WriteCustomCode(path);
            }
            {
                var path = project.GetApiPath("Logical");
                var builder = new ApiLogicalBuilder<ModelConfig>
                {
                    Project = project
                };
                builder.WriteCustomCode(path);
            }
            {
                var path = GetDocumentPath(project);
                var builder = new ApiMarkBuilder<ModelConfig>
                {
                    Project = project
                };
                builder.WriteDesignerCode(path);
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
        public override void CreateEntityCode(ProjectConfig project, ModelConfig entity)
        {
            if (entity.ExtendConfigListBool["NoApi"])
                return;
            Message = entity.Caption;
            {
                var path = project.GetApiPath("Contract");
                var builder = new EntityBuilder<ModelConfig>
                {
                    Project = project,
                    Model = entity
                };
                builder.WriteDesignerCode(path);
                builder.WriteCustomCode(path);
            }
            if (entity.NoDataBase)
                return;
            {
                var path = project.GetApiPath("Contract");
                var builder = new ApiInterfaceBuilder<ModelConfig>
                {
                    Project = project,
                    Model = entity
                };
                builder.WriteDesignerCode(path);
            }
            {
                var path = project.GetApiPath("Contract");
                var builder = new ApiProxyBuilder<ModelConfig>
                {
                    Project = project,
                    Model = entity
                };
                builder.WriteDesignerCode(path);
            }

            {
                var path = project.GetApiPath("Logical");
                var builder = new ApiLogicalBuilder<ModelConfig>
                {
                    Project = project,
                    Model = entity
                };
                builder.WriteDesignerCode(path);
            }
            {
                var path = project.GetApiPath("WebApi");
                var builder = new ApiControlerBuillder<ModelConfig>
                {
                    Project = project,
                    Model = entity
                };
                builder.WriteDesignerCode(path);
            }
            {

                string path;
                if (!string.IsNullOrWhiteSpace(project.ModelFolder))
                {
                    var folders = project.ModelFolder.Split('\\');
                    if (folders.Length == 1)
                    {
                        path = project.GetPath("Test", "UnitTest");
                    }
                    else
                    {
                        path = project.GetPath(folders[0], "Test", "UnitTest");
                    }
                }
                else
                {
                    path = project.GetPath("Test", "UnitTest");
                }
                var builder = new UnitTestBuilder<ModelConfig>
                {
                    Project = project,
                    Model = entity
                };
                builder.WriteDesignerCode(path);
            }
        }
    }
}