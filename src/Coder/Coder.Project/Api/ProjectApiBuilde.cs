using System.IO;
using System.Text;
using Agebull.Common;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder.EasyUi
{
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
            ?            
            $@"
        /// <summary>
        /// {entity.Caption}
        /// </summary>
        [Route(""{entity.EntityName}"")]
        public Task {entity.EntityName}Event(EntityEventArgument argument), [FromServices] IEntityEventHandler<{entity.EntityName}> handler) => handler.OnEvent(argument);"
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
            //DataEventCode(project, entityEvents);
        }

        private void DataEventCode(ProjectConfig project, StringBuilder entityEvents)
        {
            var code = $@"
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
            var path = IOHelper.CheckPath(project.ApiPath, "Event");
            WriteFile(Path.Combine(path, $"{project.Name}EntityEventController.cs"), code);
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
            else
                apiPath = IOHelper.CheckPath(apiPath, "EditApi");
            var pg = new ProjectApiActionCoder
            {
                Model = schema,
                Project = schema.Project,
            };
            pg.WriteDesignerCode(apiPath);
            pg.WriteCustomCode(apiPath);
        }
    }
    
}