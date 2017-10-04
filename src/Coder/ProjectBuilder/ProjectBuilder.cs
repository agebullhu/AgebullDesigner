using System;
using System.Collections.Generic;
using Agebull.Common.DataModel;
using Gboxt.Common.DataAccess.Schemas;

namespace Agebull.Common.SimpleDesign
{
    /// <summary>
    /// 项目代码生成基类
    /// </summary>
    public abstract class ProjectBuilder : CoderBase
    {
        /// <summary>
        /// 得到当前的消息跟踪器
        /// </summary>
        public Action<string> MessageSetter { get; set; }

        /// <summary>
        /// 当前跟踪消息
        /// </summary>
        public string Message
        {
            set
            {
                MessageSetter(value);
            }
        }

        private TraceMessage _traceMessage;
        /// <summary>
        /// 当前跟踪消息
        /// </summary>
        public TraceMessage TraceMessage
        {
            get
            {
                return _traceMessage ?? DataModel.TraceMessage.DefaultTrace;
            }
            set
            {
                _traceMessage = value;
            }
        }
        /// <summary>
        /// 取得默认的根路径
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        protected virtual string RootPath(ProjectConfig project)
        {
            return project["project_path_" + Name] ?? (project["project_path_" + Name] = project.ModelPath);
        }

        /// <summary>
        /// 名称
        /// </summary>
        protected abstract string Name { get; }

        /// <summary>
        /// 图标
        /// </summary>
        public virtual string Icon => "img_code";


        /// <summary>
        /// 标题
        /// </summary>
        public abstract string Caption { get; }


        /// <summary>
        /// 当前解决方案
        /// </summary>
        protected SolutionConfig Solution => SolutionConfig.Current;

        /// <summary>
        /// 准备生成实体代码
        /// </summary>
        /// <param name="project"></param>
        public virtual bool Validate(ProjectConfig project)
        {
            return true;
        }

        /// <summary>
        /// 准备生成实体代码
        /// </summary>
        /// <param name="project"></param>
        /// <param name="schema"></param>
        public virtual bool Validate(ProjectConfig project, EntityConfig schema)
        {
            return true;
        }

        /// <summary>
        /// 生成实体代码
        /// </summary>
        /// <param name="project"></param>
        /// <param name="schema"></param>
        public virtual void CreateEntityCode(ProjectConfig project, EntityConfig schema)
        {

        }

        /// <summary>
        /// 生成项目代码
        /// </summary>
        /// <param name="project"></param>
        public virtual void CreateProjectCode(ProjectConfig project)
        {

        }

        /// <summary>
        /// 注册的项目代码生成器
        /// </summary>
        public static readonly Dictionary<string, Func<ProjectBuilder>> Builders =
            new Dictionary<string, Func<ProjectBuilder>>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 注册的项目生成器
        /// </summary>
        /// <returns></returns>
        public static void RegistBuilder<TBuilder>()
            where TBuilder : ProjectBuilder, new()
        {
            var builder = new TBuilder();
            if (Builders.ContainsKey(builder.Name))
                throw new ArgumentException("已注册名称为" + builder.Name + "的项目生成器，不应该重复注册");
            Builders.Add(builder.Name, () => new TBuilder());
        }

        /// <summary>
        /// 生成合适的项目生成器
        /// </summary>
        /// <param name="type">项目类型</param>
        /// <returns>项目生成器</returns>
        public static ProjectBuilder CreateBuilder(string type)
        {
            if (string.IsNullOrEmpty(type))
                throw new ArgumentException("空名称的项目生成器是不允许的");
            if (Builders.ContainsKey(type))
                return Builders[type]();
            throw new ArgumentException("名称为" + type + "的项目生成器未注册");
        }
        /// <summary>
        /// 实体生成器的简单调用
        /// </summary>
        /// <typeparam name="TEntityCoder"></typeparam>
        /// <param name="project"></param>
        /// <param name="schema"></param>
        /// <param name="path"></param>
        protected static void CreateCode<TEntityCoder>(ProjectConfig project, EntityConfig schema, string path)
            where TEntityCoder : CoderWithEntity, new()
        {
            var builder = new TEntityCoder
            {
                Entity = schema,
                Project = project
            };
            builder.CreateBaseCode(path);
            builder.CreateExtendCode(path);
        }
    }
}