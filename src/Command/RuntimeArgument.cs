using System.Collections.Generic;
using System.Linq;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 运行参数
    /// </summary>
    public class RuntimeArgument
    {
        /// <summary>
        /// 参数
        /// </summary>
        public object Argument
        {
            get => _argument;
            set
            {
                _argument = value;
                GetEntities();
            }
        }

        private object _argument;

        /// <summary>
        /// 当前实体对象
        /// </summary>
        public IList<ModelConfig> Entities { get; private set; }

        /// <summary>
        /// 当前关联项目
        /// </summary>
        public List<ProjectConfig> Projects { get; private set; }

        /// <summary>
        /// 默认的取当前实体的方法 
        /// </summary>
        /// <returns></returns>
        private void GetEntities()
        {
            var list = new NotificationList<ModelConfig>();
            switch (_argument)
            {
                case ModelConfig entityConfig:
                    list.Add(entityConfig);
                    break;
                case ProjectConfig projectConfig:
                    projectConfig.Models.Foreach(p => list.TryAdd(p));
                    break;
                default:
                    SolutionConfig.Current.Models.Foreach(p => list.TryAdd(p));
                    break;
            }
            Entities = list;

            Projects = new List<ProjectConfig>();
            foreach (var entity in list)
            {
                var project = entity.Parent;
                if (project == null)
                    continue;
                if (!Projects.Contains(project))
                    Projects.Add(project);
            }
        }
    }
}