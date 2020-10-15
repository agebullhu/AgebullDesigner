using System.Collections.Generic;
using System.Linq;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 运行参数
    /// </summary>
    public class ModelArgument<TModelConfig>
        where TModelConfig : ProjectChildConfigBase, IEntityConfig
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
        public IList<TModelConfig> Models { get; private set; }

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
            var list = new NotificationList<TModelConfig>();
            switch (_argument)
            {
                case TModelConfig model:
                    list.Add(model);
                    break;
                case ProjectConfig projectConfig:
                    if (typeof(TModelConfig) == typeof(ModelConfig))
                        projectConfig.Models.Foreach(p => list.TryAdd((TModelConfig)p));
                    else
                        projectConfig.Entities.Foreach(p => list.TryAdd((TModelConfig)p));
                    break;
                default:
                    if (typeof(TModelConfig) == typeof(ModelConfig))
                        SolutionConfig.Current.Models.Foreach(p => list.TryAdd((TModelConfig)p));
                    else
                        SolutionConfig.Current.Entities.Foreach(p => list.TryAdd((TModelConfig)p));
                    break;
            }
            Models = list;

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