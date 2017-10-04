using System.Collections;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    ///     实体实现接口的命令
    /// </summary>
    public class TypeConvert
    {
        #region 命令注入

        public static void Register()
        {
            CommandCoefficient.RegisterConvert<EntityConfig, PropertyConfig>(Entity2Property);
            CommandCoefficient.RegisterConvert<ProjectConfig, PropertyConfig>(Project2Property);
            CommandCoefficient.RegisterConvert<SolutionConfig, PropertyConfig>(Solution2Property);
            CommandCoefficient.RegisterConvert<ProjectConfig, EntityConfig>(Project2Entity);
            CommandCoefficient.RegisterConvert<SolutionConfig, EntityConfig>(Solution2Entity);
            CommandCoefficient.RegisterConvert<SolutionConfig, ProjectConfig>(Solution2Project);
        }
        
        #endregion

        #region 弱类型


        public static IEnumerator Entity2Property(object entity)
        {
            foreach (var p in ((EntityConfig)entity).Properties)
            {
                yield return p;
            }
        }
        public static IEnumerator Project2Property(object project)
        {
            foreach (var entity in ((ProjectConfig)project).Entities)
            {
                foreach (var p in entity.Properties)
                {
                    yield return p;
                }
            }
        }
        public static IEnumerator Solution2Property(object solution)
        {
            foreach (var project in ((SolutionConfig)solution).Projects)
            {
                foreach (var entity in project.Entities)
                {
                    foreach (var p in entity.Properties)
                    {
                        yield return p;
                    }
                }
            }
        }
        public static IEnumerator Project2Entity(object project)
        {
            foreach (var entity in ((ProjectConfig)project).Entities)
            {
                yield return entity;
            }
        }
        public static IEnumerator Solution2Entity(object solution)
        {
            foreach (var project in ((SolutionConfig)solution).Projects)
            {
                foreach (var entity in project.Entities)
                {
                    yield return entity;
                }
            }
        }
        public static IEnumerator Solution2Project(object solution)
        {
            foreach (var project in ((SolutionConfig)solution).Projects)
            {
                yield return project;
            }
        }

        #endregion
        #region 强类型


        public static IEnumerator Entity2Property(EntityConfig entity)
        {
            foreach (var p in entity.Properties)
            {
                yield return p;
            }
        }
        public static IEnumerator Project2Property(ProjectConfig project)
        {
            foreach (var entity in project.Entities)
            {
                foreach (var p in entity.Properties)
                {
                    yield return p;
                }
            }
        }
        public static IEnumerator Solution2Property(SolutionConfig solution)
        {
            foreach (var project in solution.Projects)
            {
                foreach (var entity in project.Entities)
                {
                    foreach (var p in entity.Properties)
                    {
                        yield return p;
                    }
                }
            }
        }
        public static IEnumerator Project2Entity(ProjectConfig project)
        {
            foreach (var entity in project.Entities)
            {
                yield return entity;
            }
        }
        public static IEnumerator Solution2Entity(SolutionConfig solution)
        {
            foreach (var project in solution.Projects)
            {
                foreach (var entity in project.Entities)
                {
                    yield return entity;
                }
            }
        }
        public static IEnumerator Solution2Project(SolutionConfig solution)
        {
            foreach (var project in solution.Projects)
            {
                yield return project;
            }
        }

        #endregion
    }
}