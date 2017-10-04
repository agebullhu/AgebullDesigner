using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gboxt.Common.DataAccess.Schemas;

namespace Agebull.Common.SimpleDesign
{
    /// <summary>
    /// 代码片断基类
    /// </summary>
    public class MomentCoderBase : CoderBase
    {
        /// <summary>
        /// 当前表对象
        /// </summary>
        public EntityConfig Entity { get; set; }
        /// <summary>
        ///     数据库配置对象
        /// </summary>
        public ProjectConfig Project { get; set; }

        /// <summary>
        ///     表配置集合
        /// </summary>
        public IEnumerable<EntityConfig> Entities => Project.Entities;

        /// <summary>
        /// 执行以实体为目标的动作
        /// </summary>
        /// <param name="config"></param>
        /// <param name="EntityCoder"></param>
        /// <param name="ProjectCoder"></param>
        /// <returns></returns>
        public string RunByEntity(ConfigBase config, Func<string> EntityCoder, Func<ProjectConfig, string> ProjectCoder = null)
        {
            Entity = config as EntityConfig;
            if (Entity != null)
            {
                return EntityCoder();
            }
            var col = config as PropertyConfig;
            if (col != null)
            {
                Entity = col.Parent;
                return EntityCoder();
            }
            StringBuilder code = new StringBuilder();
            Project = config as ProjectConfig;
            if (Project != null)
            {
                if (ProjectCoder != null)
                    code.Append(ProjectCoder(Project));
                foreach (EntityConfig ent in Project.Entities)
                {
                    Entity = ent;
                    code.AppendLine();
                    code.Append(EntityCoder());
                }
                return code.ToString();
            }
            var solution = GlobalConfig.CurrentConfig as SolutionConfig;
            if (solution == null)
                return code.ToString();
            foreach (var project in solution.Projects)
            {
                Project = project;
                if (ProjectCoder != null)
                    code.Append(ProjectCoder(Project));
                foreach (EntityConfig ent in Project.Entities)
                {
                    Entity = ent;
                    code.AppendLine();
                    code.Append(EntityCoder());
                }
            }
            return code.ToString();
        }

        /// <summary>
        /// 执行实体及项目的动作
        /// </summary>
        /// <param name="config"></param>
        /// <param name="entityCoder"></param>
        /// <param name="projectCoder"></param>
        /// <returns></returns>
        public string Run(ConfigBase config, Func<EntityConfig, string> entityCoder, Func<ProjectConfig, string> projectCoder = null)
        {
            Entity = config as EntityConfig;
            if (Entity != null)
            {
                return entityCoder(Entity);
            }
            var col = config as PropertyConfig;
            if (col != null)
            {
                return entityCoder(col.Parent);
            }
            StringBuilder code = new StringBuilder();
            Project = config as ProjectConfig;
            if (Project != null)
            {
                if (projectCoder != null)
                    code.Append(projectCoder(Project));
                foreach (EntityConfig ent in Project.Entities)
                {
                    Entity = ent;
                    code.AppendLine();
                    code.Append(entityCoder(ent));
                }
                return code.ToString();
            }
            var solution = GlobalConfig.CurrentConfig as SolutionConfig;
            if (solution == null)
                return code.ToString();
            foreach (var project in solution.Projects)
            {
                Project = project;
                if (projectCoder != null)
                    code.Append(projectCoder(Project));
                foreach (EntityConfig ent in Project.Entities)
                {
                    Entity = ent;
                    code.AppendLine();
                    code.Append(entityCoder(ent));
                }
            }
            return code.ToString();
        }

        /// <summary>
        /// 执行特定类型的动作
        /// </summary>
        /// <typeparam name="TConfig"></typeparam>
        /// <param name="config"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public string RunBy<TConfig>(ConfigBase config, Func<TConfig, string> func) where TConfig : ConfigBase
        {
            var config1 = config as TConfig;
            return config1 != null ? func(config1) : null;
        }

        #region 系统当前选择对象

        #region 快捷遍历

        #region Property


        private static void ForeachByCurrent(Action<PropertyConfig> action, PropertyConfig property)
        {
            if (property != null)
                action(property);
        }
        private static void ForeachByCurrent(Action<PropertyConfig> action, EntityConfig entity)
        {
            foreach (var property in entity.Properties)
                ForeachByCurrent(action, property);
        }
        private static void ForeachByCurrent(Action<PropertyConfig> action, ProjectConfig project)
        {
            foreach (var entity in project.Entities)
                ForeachByCurrent(action, entity);
        }
        private static void ForeachByCurrent(Action<PropertyConfig> action, SolutionConfig solution)
        {
            foreach (var project in solution.Projects)
                ForeachByCurrent(action, project);
        }


        /// <summary>
        /// 以属性配置为基础遍历
        /// </summary>
        /// <param name="action">动作</param>
        protected static void ForeachByCurrent(Action<PropertyConfig> action)
        {
            var property = GlobalConfig.CurrentConfig as PropertyConfig;
            if (property != null)
            {
                ForeachByCurrent(action, property);
                return;
            }
            var entity = GlobalConfig.CurrentConfig as EntityConfig;
            if (entity != null)
            {
                ForeachByCurrent(action, entity);
                return;
            }
            var project = GlobalConfig.CurrentConfig as ProjectConfig;
            if (project != null)
            {
                ForeachByCurrent(action, project);
                return;
            }
            var solution = GlobalConfig.CurrentConfig as SolutionConfig;
            if (solution != null)
            {
                ForeachByCurrent(action, solution);
            }
        }

        private static void ForeachByCurrent(Func<PropertyConfig, bool> condition, Action<PropertyConfig> action, PropertyConfig property)
        {
            if (property != null && condition(property))
                action(property);
        }
        private static void ForeachByCurrent(Func<PropertyConfig, bool> condition, Action<PropertyConfig> action, EntityConfig entity)
        {
            foreach (var property in entity.Properties)
                ForeachByCurrent(condition, action, property);
        }
        private static void ForeachByCurrent(Func<PropertyConfig, bool> condition, Action<PropertyConfig> action, ProjectConfig project)
        {
            foreach (var entity in project.Entities)
                ForeachByCurrent(condition, action, entity);
        }
        private static void ForeachByCurrent(Func<PropertyConfig, bool> condition, Action<PropertyConfig> action, SolutionConfig solution)
        {
            foreach (var project in solution.Projects)
                ForeachByCurrent(condition, action, project);
        }

        /// <summary>
        /// 以属性配置为基础遍历
        /// </summary>
        /// <param name="condition">执行条件</param>
        /// <param name="action">动作</param>
        protected static void ForeachByCurrent(Func<PropertyConfig, bool> condition, Action<PropertyConfig> action)
        {
            var property = GlobalConfig.CurrentConfig as PropertyConfig;
            if (property != null)
            {
                ForeachByCurrent(condition, action, property);
                return;
            }
            var entity = GlobalConfig.CurrentConfig as EntityConfig;
            if (entity != null)
            {
                ForeachByCurrent(condition, action, entity);
                return;
            }
            var project = GlobalConfig.CurrentConfig as ProjectConfig;
            if (project != null)
            {
                ForeachByCurrent(condition, action, project);
                return;
            }
            var solution = GlobalConfig.CurrentConfig as SolutionConfig;
            if (solution != null)
            {
                ForeachByCurrent(condition, action, solution);
            }
        }
        #endregion

        #region Enum



        private static void ForeachByCurrent(Action<EnumConfig> action, EnumConfig enumConfig)
        {
            if (enumConfig != null)
                action(enumConfig);
        }

        private static void ForeachByCurrent(Action<EnumConfig> action, EntityConfig entity)
        {
            foreach (var property in entity.Properties.Where(p => p.EnumConfig != null))
                action(property.EnumConfig);
        }
        private static void ForeachByCurrent(Action<EnumConfig> action, ProjectConfig project)
        {
            foreach (var entity in project.Entities)
                ForeachByCurrent(action, entity);
        }
        private static void ForeachByCurrent(Action<EnumConfig> action, SolutionConfig solution)
        {
            foreach (var project in solution.Enums)
                ForeachByCurrent(action, project);
        }

        /// <summary>
        /// 以枚举配置为基础遍历
        /// </summary>
        /// <param name="action">动作</param>
        protected static void ForeachByCurrent(Action<EnumConfig> action)
        {
            var enc = GlobalConfig.CurrentConfig as EnumConfig;
            if (enc != null)
            {
                action(enc);
                return;
            }
            var col = GlobalConfig.CurrentConfig as PropertyConfig;
            if (col != null)
            {
                action(col.EnumConfig);
                return;
            }
            var entity = GlobalConfig.CurrentConfig as EntityConfig;
            if (entity != null)
            {
                ForeachByCurrent(action, entity);
                return;
            }
            var project = GlobalConfig.CurrentConfig as ProjectConfig;
            if (project != null)
            {
                ForeachByCurrent(action, project);
                return;
            }
            var solution = GlobalConfig.CurrentConfig as SolutionConfig;
            if (solution != null)
            {
                ForeachByCurrent(action, solution);
            }
        }


        private static void ForeachByCurrent(Func<EnumConfig, bool> condition, Action<EnumConfig> action, EnumConfig enumConfig)
        {
            if (enumConfig != null && condition(enumConfig))
                action(enumConfig);
        }

        private static void ForeachByCurrent(Func<EnumConfig, bool> condition, Action<EnumConfig> action, EntityConfig entity)
        {
            foreach (var property in entity.Properties.Where(p => p.EnumConfig != null))
                ForeachByCurrent(condition, action, property.EnumConfig);
        }
        private static void ForeachByCurrent(Func<EnumConfig, bool> condition, Action<EnumConfig> action, ProjectConfig project)
        {
            foreach (var entity in project.Entities)
                ForeachByCurrent(condition, action, entity);
        }
        private static void ForeachByCurrent(Func<EnumConfig, bool> condition, Action<EnumConfig> action, SolutionConfig solution)
        {
            foreach (var project in solution.Enums)
                ForeachByCurrent(condition, action, project);
        }

        /// <summary>
        /// 以属性配置为基础遍历
        /// </summary>
        /// <param name="condition">执行条件</param>
        /// <param name="action">动作</param>
        protected static void ForeachByCurrent(Func<EnumConfig, bool> condition, Action<EnumConfig> action)
        {
            var enc = GlobalConfig.CurrentConfig as EnumConfig;
            if (enc != null)
            {
                ForeachByCurrent(condition, action, enc);
                return;
            }
            var col = GlobalConfig.CurrentConfig as PropertyConfig;
            if (col != null)
            {
                ForeachByCurrent(condition, action, col.EnumConfig);
                return;
            }
            var entity = GlobalConfig.CurrentConfig as EntityConfig;
            if (entity != null)
            {
                ForeachByCurrent(condition, action, entity);
                return;
            }
            var project = GlobalConfig.CurrentConfig as ProjectConfig;
            if (project != null)
            {
                ForeachByCurrent(condition, action, project);
                return;
            }
            var solution = GlobalConfig.CurrentConfig as SolutionConfig;
            if (solution != null)
            {
                ForeachByCurrent(condition, action, solution);
            }
        }
        #endregion

        #region Entity


        private static void ForeachByCurrent(Action<EntityConfig> action, EntityConfig entity)
        {
            if (entity != null)
                action(entity);
        }
        private static void ForeachByCurrent(Action<EntityConfig> action, ProjectConfig project)
        {
            foreach (var entity in project.Entities)
                ForeachByCurrent(action, entity);
        }
        private static void ForeachByCurrent(Action<EntityConfig> action, SolutionConfig solution)
        {
            foreach (var project in solution.Projects)
                ForeachByCurrent(action, project);
        }

        /// <summary>
        /// 以实体配置为基础遍历
        /// </summary>
        /// <param name="action">动作</param>
        protected static void ForeachByCurrent(Action<EntityConfig> action)
        {
            var col = GlobalConfig.CurrentConfig as PropertyConfig;
            if (col != null)
            {
                action(col.Parent);
                return;
            }
            var entity = GlobalConfig.CurrentConfig as EntityConfig;
            if (entity != null)
            {
                ForeachByCurrent(action, entity);
                return;
            }
            var project = GlobalConfig.CurrentConfig as ProjectConfig;
            if (project != null)
            {
                ForeachByCurrent(action, project);
                return;
            }
            var solution = GlobalConfig.CurrentConfig as SolutionConfig;
            if (solution != null)
            {
                ForeachByCurrent(action, solution);
            }
        }


        private static void ForeachByCurrent(Func<EntityConfig, bool> condition, Action<EntityConfig> action, EntityConfig entity)
        {
            if (entity != null && condition(entity))
                action(entity);
        }
        private static void ForeachByCurrent(Func<EntityConfig, bool> condition, Action<EntityConfig> action, ProjectConfig project)
        {
            foreach (var entity in project.Entities)
                ForeachByCurrent(condition, action, entity);
        }
        private static void ForeachByCurrent(Func<EntityConfig, bool> condition, Action<EntityConfig> action, SolutionConfig solution)
        {
            foreach (var project in solution.Projects)
                ForeachByCurrent(condition, action, project);
        }

        /// <summary>
        /// 以实体配置为基础遍历
        /// </summary>
        /// <param name="condition">执行条件</param>
        /// <param name="action">动作</param>
        protected static void ForeachByCurrent(Func<EntityConfig, bool> condition, Action<EntityConfig> action)
        {
            var col = GlobalConfig.CurrentConfig as PropertyConfig;
            if (col != null)
            {
                ForeachByCurrent(condition, action, col.Parent);
                return;
            }
            var entity = GlobalConfig.CurrentConfig as EntityConfig;
            if (entity != null)
            {
                ForeachByCurrent(condition, action, entity);
                return;
            }
            var project = GlobalConfig.CurrentConfig as ProjectConfig;
            if (project != null)
            {
                ForeachByCurrent(condition, action, project);
                return;
            }
            var solution = GlobalConfig.CurrentConfig as SolutionConfig;
            if (solution != null)
            {
                ForeachByCurrent(condition, action, solution);
            }
        }

        #endregion

        #region Project


        private static void ForeachByCurrent(Action<ProjectConfig> action, EntityConfig entity)
        {
            ForeachByCurrent(action, entity?.Parent);
        }
        private static void ForeachByCurrent(Action<ProjectConfig> action, ProjectConfig project)
        {
            if (project != null)
                action(project);
        }
        private static void ForeachByCurrent(Action<ProjectConfig> action, SolutionConfig solution)
        {
            foreach (var project in solution.Projects)
                ForeachByCurrent(action, project);
        }

        /// <summary>
        /// 以项目配置为基础遍历
        /// </summary>
        /// <param name="action">动作</param>
        protected static void ForeachByCurrent(Action<ProjectConfig> action)
        {
            var col = GlobalConfig.CurrentConfig as PropertyConfig;
            if (col != null)
            {
                ForeachByCurrent(action, col.Parent);
                return;
            }
            var entity = GlobalConfig.CurrentConfig as ProjectConfig;
            if (entity != null)
            {
                ForeachByCurrent(action, entity);
                return;
            }
            var project = GlobalConfig.CurrentConfig as ProjectConfig;
            if (project != null)
            {
                ForeachByCurrent(action, project);
                return;
            }
            var solution = GlobalConfig.CurrentConfig as SolutionConfig;
            if (solution != null)
            {
                ForeachByCurrent(action, solution);
            }
        }


        private static void ForeachByCurrent(Func<ProjectConfig, bool> condition, Action<ProjectConfig> action, EntityConfig entity)
        {
            ForeachByCurrent(action, entity?.Parent);
        }
        private static void ForeachByCurrent(Func<ProjectConfig, bool> condition, Action<ProjectConfig> action, ProjectConfig project)
        {
            if (project != null && condition(project))
                action(project);
        }
        private static void ForeachByCurrent(Func<ProjectConfig, bool> condition, Action<ProjectConfig> action, SolutionConfig solution)
        {
            foreach (var project in solution.Projects)
                ForeachByCurrent(condition, action, project);
        }

        /// <summary>
        /// 以项目配置为基础遍历
        /// </summary>
        /// <param name="condition">执行条件</param>
        /// <param name="action">动作</param>
        protected static void ForeachByCurrent(Func<ProjectConfig, bool> condition, Action<ProjectConfig> action)
        {
            var col = GlobalConfig.CurrentConfig as PropertyConfig;
            if (col != null)
            {
                ForeachByCurrent(condition, action, col.Parent);
                return;
            }
            var entity = GlobalConfig.CurrentConfig as ProjectConfig;
            if (entity != null)
            {
                ForeachByCurrent(condition, action, entity);
                return;
            }
            var project = GlobalConfig.CurrentConfig as ProjectConfig;
            if (project != null)
            {
                ForeachByCurrent(condition, action, project);
                return;
            }
            var solution = GlobalConfig.CurrentConfig as SolutionConfig;
            if (solution != null)
            {
                ForeachByCurrent(condition, action, solution);
            }
        }

        #endregion
        #endregion


        #endregion
    }
}