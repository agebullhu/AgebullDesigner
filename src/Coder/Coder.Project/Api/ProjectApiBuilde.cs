using System.ComponentModel.Composition;
using System.IO;
using System.Text;
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
            NormalCodeModel.RegistBuilder<ProjectApiBuilde>();
        }

    }
    public sealed class ProjectApiBuilde  : ProjectBuilder
    {
        /// <summary>
        /// 名称
        /// </summary>
        public override string Name => "Edit Api";

        /// <summary>
        /// 标题
        /// </summary>
        public override string Caption => Name;
        string EventCode(IEntityConfig entity) => entity.EnableDataEvent
            ?            $@"
        /// <summary>
        /// 实时记录
        /// </summary>
        [Route(""{entity.EntityName}"")]
        public async Task {entity.EntityName}Event(EntityEventArgument argument)
        {{
            switch (argument.OperatorType)
            {{
                case DataOperatorType.Insert:
                case DataOperatorType.Update:
                    break;
                default:
                    return;
            }}
            var data = JsonConvert.DeserializeObject<{entity.EntityName }> (argument.Value);
            //TODO:处理代码
        }}"
            : null;

        /// <summary>
        /// 生成项目代码
        /// </summary>
        /// <param name="project"></param>
        public override void CreateProjectCode(ProjectConfig project)
        {
            var entityEvents = new StringBuilder();

            foreach (var entity in project.Entities)
            {
                entityEvents.Append(EventCode(entity));
            }
            foreach (var entity in project.Models)
            {
                entityEvents.Append(EventCode(entity));
            }

            var code= $@"
using System.Threading.Tasks;
using Newtonsoft.Json;
using Agebull.EntityModel.Common;
using Agebull.EntityModel.Events;
using ZeroTeam.MessageMVC;
using ZeroTeam.MessageMVC.ZeroApis;
using {project.NameSpace}.DataAccess;

namespace {project.NameSpace}.Events
{{
    /// <summary>
    ///  监控记录
    /// </summary>
    [NetEvent(""EntityEvent"")]
    [Route(""{project.Name}"")]
    public partial class {project.Name}EntityEventController : IApiController
    {{
    /*{entityEvents}
    */
    }}
}}";
            WriteFile(Path.Combine(project.ApiPath,$"{project.Name}EntityEventController.cs"),code);
        }

        /// <summary>
        /// 生成实体代码
        /// </summary>
        /// <param name="project"></param>
        /// <param name="schema"></param>
        public override void CreateModelCode(ProjectConfig project, IEntityConfig schema)
        {
            if (!schema.EnableEditApi)
                return;
            var cls = schema.Classify.IsEmptyClassify() ? null : schema.Classify;

            var businessPath = IOHelper.CheckPath(project.ModelPath, "Business");
            if (!project.NoClassify && cls != null)
                businessPath = IOHelper.CheckPath(businessPath, cls);
            var builder = new BusinessBuilder
            {
                Model = schema,
                Project = project
            };
            builder.WriteDesignerCode(businessPath);
            builder.WriteCustomCode(businessPath);

            var apiPath = project.ApiPath;
            if (!project.NoClassify && cls != null)
                apiPath = IOHelper.CheckPath(apiPath, cls);
            var pg = new ProjectApiActionCoder
            {
                Model = schema,
                Project = schema.Parent,
            };
            pg.WriteDesignerCode(apiPath);
            pg.WriteCustomCode(apiPath);
        }
    }
    
}