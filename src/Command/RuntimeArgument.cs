using System.Collections.Generic;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// ���в���
    /// </summary>
    public class RuntimeArgument
    {
        /// <summary>
        /// ����
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
        /// ��ǰʵ�����
        /// </summary>
        public IList<EntityConfig> Entities { get; private set; }

        /// <summary>
        /// ��ǰ������Ŀ
        /// </summary>
        public List<ProjectConfig> Projects { get; private set; }

        /// <summary>
        /// Ĭ�ϵ�ȡ��ǰʵ��ķ��� 
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