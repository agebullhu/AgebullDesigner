using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    /// <summary>
    /// 代码片断基类
    /// </summary>
    public class MomentCoderBase : CoderBase
    {
        #region 代码生成代理接口


        public static string DoCoder(Func<string> func)
        {
            using (WorkModelScope.CreateScope(WorkModel.Coder))
            {
                return func();
            }
        }

        public static string DoCoder<T>(Func<T, string> func, T t)
        {
            using (WorkModelScope.CreateScope(WorkModel.Coder))
            {
                return func(t);
            }
        }

        public static string DoCoder<T1, T2>(Func<T1, T2, string> func, T1 t1, T2 t2)
        {
            using (WorkModelScope.CreateScope(WorkModel.Coder))
            {
                return func(t1, t2);
            }
        }

        public static string DoCoder<T1, T2, T3>(Func<T1, T2, T3, string> func, T1 t1, T2 t2, T3 t3)
        {
            using (WorkModelScope.CreateScope(WorkModel.Coder))
            {
                return func(t1, t2, t3);
            }
        }

        public static string DoCoder<T1, T2, T3, T4>(Func<T1, T2, T3, T4, string> func, T1 t1, T2 t2, T3 t3, T4 t4)
        {
            using (WorkModelScope.CreateScope(WorkModel.Coder))
            {
                return func(t1, t2, t3, t4);
            }
        }

        public static string DoCoder<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, string> func, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5)
        {
            using (WorkModelScope.CreateScope(WorkModel.Coder))
            {
                return func(t1, t2, t3, t4, t5);
            }
        }

        public static string DoCoder<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, string> func, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6)
        {
            using (WorkModelScope.CreateScope(WorkModel.Coder))
            {
                return func(t1, t2, t3, t4, t5, t6);
            }
        }

        #endregion

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
            if (config is PropertyConfig col)
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
            if (!(GlobalConfig.CurrentConfig is SolutionConfig solution))
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
            if (config is PropertyConfig col)
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
            if (!(GlobalConfig.CurrentConfig is SolutionConfig solution))
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
            return config is TConfig config1 ? func(config1) : null;
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
            if (GlobalConfig.CurrentConfig is PropertyConfig property)
            {
                ForeachByCurrent(action, property);
                return;
            }
            if (GlobalConfig.CurrentConfig is EntityConfig entity)
            {
                ForeachByCurrent(action, entity);
                return;
            }
            if (GlobalConfig.CurrentConfig is ProjectConfig project)
            {
                ForeachByCurrent(action, project);
                return;
            }
            if (GlobalConfig.CurrentConfig is SolutionConfig solution)
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
            if (GlobalConfig.CurrentConfig is PropertyConfig property)
            {
                ForeachByCurrent(condition, action, property);
                return;
            }
            if (GlobalConfig.CurrentConfig is EntityConfig entity)
            {
                ForeachByCurrent(condition, action, entity);
                return;
            }
            if (GlobalConfig.CurrentConfig is ProjectConfig project)
            {
                ForeachByCurrent(condition, action, project);
                return;
            }
            if (GlobalConfig.CurrentConfig is SolutionConfig solution)
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
            if (GlobalConfig.CurrentConfig is EnumConfig enc)
            {
                action(enc);
                return;
            }
            if (GlobalConfig.CurrentConfig is PropertyConfig col)
            {
                action(col.EnumConfig);
                return;
            }
            if (GlobalConfig.CurrentConfig is EntityConfig entity)
            {
                ForeachByCurrent(action, entity);
                return;
            }
            if (GlobalConfig.CurrentConfig is ProjectConfig project)
            {
                ForeachByCurrent(action, project);
                return;
            }
            if (GlobalConfig.CurrentConfig is SolutionConfig solution)
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
            if (GlobalConfig.CurrentConfig is EnumConfig enc)
            {
                ForeachByCurrent(condition, action, enc);
                return;
            }
            if (GlobalConfig.CurrentConfig is PropertyConfig col)
            {
                ForeachByCurrent(condition, action, col.EnumConfig);
                return;
            }
            if (GlobalConfig.CurrentConfig is EntityConfig entity)
            {
                ForeachByCurrent(condition, action, entity);
                return;
            }
            if (GlobalConfig.CurrentConfig is ProjectConfig project)
            {
                ForeachByCurrent(condition, action, project);
                return;
            }
            if (GlobalConfig.CurrentConfig is SolutionConfig solution)
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
            if (GlobalConfig.CurrentConfig is PropertyConfig col)
            {
                action(col.Parent);
                return;
            }
            if (GlobalConfig.CurrentConfig is EntityConfig entity)
            {
                ForeachByCurrent(action, entity);
                return;
            }
            if (GlobalConfig.CurrentConfig is ProjectConfig project)
            {
                ForeachByCurrent(action, project);
                return;
            }
            if (GlobalConfig.CurrentConfig is SolutionConfig solution)
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
            if (GlobalConfig.CurrentConfig is PropertyConfig col)
            {
                ForeachByCurrent(condition, action, col.Parent);
                return;
            }
            if (GlobalConfig.CurrentConfig is EntityConfig entity)
            {
                ForeachByCurrent(condition, action, entity);
                return;
            }
            if (GlobalConfig.CurrentConfig is ProjectConfig project)
            {
                ForeachByCurrent(condition, action, project);
                return;
            }
            if (GlobalConfig.CurrentConfig is SolutionConfig solution)
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
            if (GlobalConfig.CurrentConfig is PropertyConfig col)
            {
                ForeachByCurrent(action, col.Parent);
                return;
            }
            if (GlobalConfig.CurrentConfig is ProjectConfig entity)
            {
                ForeachByCurrent(action, entity);
                return;
            }
            if (GlobalConfig.CurrentConfig is ProjectConfig project)
            {
                ForeachByCurrent(action, project);
                return;
            }
            if (GlobalConfig.CurrentConfig is SolutionConfig solution)
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
            if (GlobalConfig.CurrentConfig is PropertyConfig col)
            {
                ForeachByCurrent(condition, action, col.Parent);
                return;
            }
            if (GlobalConfig.CurrentConfig is ProjectConfig entity)
            {
                ForeachByCurrent(condition, action, entity);
                return;
            }
            if (GlobalConfig.CurrentConfig is ProjectConfig project)
            {
                ForeachByCurrent(condition, action, project);
                return;
            }
            if (GlobalConfig.CurrentConfig is SolutionConfig solution)
            {
                ForeachByCurrent(condition, action, solution);
            }
        }

        #endregion
        #endregion


        #endregion
    }
}