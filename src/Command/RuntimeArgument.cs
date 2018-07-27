using System.Collections.Generic;
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
        public IList<EntityConfig> Entities { get; private set; }

        /// <summary>
        /// 当前关联项目
        /// </summary>
        public List<ProjectConfig> Projects { get; private set; }

        /// <summary>
        /// 默认的取当前实体的方法 
        /// </summary>
        /// <returns></returns>
        void GetEntities()
        {
            var list = new List<EntityConfig>();
            if (_argument is EntityConfig entityConfig)
            {
                list.Add(entityConfig);
            }
            else
            {
                if (_argument is PropertyConfig propertyConfig)
                {
                    list.Add(propertyConfig.Parent);
                }
                else
                {
                    list.AddRange(_argument is ProjectConfig projectConfig
                        ? projectConfig.Entities
                        : SolutionConfig.Current.Entities);
                }
            }

            Projects = new List<ProjectConfig>();
            foreach (var entity in list)
            {
                var project = entity.Parent;
                if (project == null)
                    continue;
                if (!Projects.Contains(project))
                    Projects.Add(project);
            }
            Entities = list;
        }
    }
}