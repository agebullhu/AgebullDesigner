using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder
{
    /// <summary>
    /// 项目代码生成基类
    /// </summary>
    public abstract class ProjectBuilder<TModelConfig> : CoderBase
            where TModelConfig : ProjectChildConfigBase, IEntityConfig
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
            set => MessageSetter(value);
        }

        private TraceMessage _traceMessage;
        /// <summary>
        /// 当前跟踪消息
        /// </summary>
        public TraceMessage TraceMessage
        {
            get => _traceMessage ?? TraceMessage.DefaultTrace;
            set => _traceMessage = value;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public abstract string Name { get; }

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
        /// <param name="schema"></param>
        public virtual bool Validate(TModelConfig schema)
        {
            return true;
        }

        /// <summary>
        /// 生成项目代码
        /// </summary>
        /// <param name="project"></param>
        public virtual void CreateProjectCode(ProjectConfig project)
        {

        }

        /// <summary>
        /// 生成实体代码
        /// </summary>
        /// <param name="project"></param>
        /// <param name="schema"></param>
        public virtual void CreateModelCode(ProjectConfig project, TModelConfig schema)
        {

        }

        /// <summary>
        /// 实体生成器的简单调用
        /// </summary>
        /// <typeparam name="TEntityCoder"></typeparam>
        /// <param name="project"></param>
        /// <param name="schema"></param>
        /// <param name="path"></param>
        protected static void CreateCode<TModelCoder>(ProjectConfig project, TModelConfig schema, string path)
            where TModelCoder : CoderWithModel<TModelConfig>, new()
        {
            var builder = new TModelCoder
            {
                Model = schema,
                Project = project
            };
            builder.WriteDesignerCode(path);
            builder.WriteCustomCode(path);
        }
    }
}