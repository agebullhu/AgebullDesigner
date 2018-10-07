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
        private void GetEntities()
        {
            var list = new List<EntityConfig>();
            switch (_argument)
            {
                case EntityConfig entityConfig:
                    list.Add(entityConfig);
                    break;
                case EntityClassify classify:
                    list.AddRange(classify.Items);
                    break;
                case PropertyConfig propertyConfig:
                    list.Add(propertyConfig.Parent);
                    break;
                case ProjectConfig projectConfig:
                    list.AddRange(projectConfig.Entities);
                    break;
                default:
                    list.AddRange(SolutionConfig.Current.Entities);
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