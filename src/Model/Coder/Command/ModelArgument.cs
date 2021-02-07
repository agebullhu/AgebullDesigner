using System.Collections.Generic;
using System.Linq;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 运行参数
    /// </summary>
    public class ModelArgument
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
                GetModels();
            }
        }

        private object _argument;

        /// <summary>
        /// 当前实体对象
        /// </summary>
        public IList<IEntityConfig> Models { get; private set; }

        /// <summary>
        /// 当前关联项目
        /// </summary>
        public List<ProjectConfig> Projects { get; private set; }

        /// <summary>
        /// 默认的取当前实体的方法 
        /// </summary>
        /// <returns></returns>
        private void GetModels()
        {
            var list = new NotificationList<IEntityConfig>();
            switch (_argument)
            {
                case IEntityConfig model:
                    list.Add(model);
                    break;
                case IFieldConfig field:
                    list.Add(field.Parent);
                    break;
                case ProjectConfig projectConfig:
                    projectConfig.Models.Foreach(p => list.TryAdd(p));
                    projectConfig.Entities.Foreach(p => list.TryAdd(p));
                    break;
                default:
                    return;
            }
            Models = list;

            Projects = new List<ProjectConfig>();
            foreach (var entity in list)
            {
                var project = entity.Project;
                if (project == null)
                    continue;
                if (!Projects.Contains(project))
                    Projects.Add(project);
            }
        }
    }
}