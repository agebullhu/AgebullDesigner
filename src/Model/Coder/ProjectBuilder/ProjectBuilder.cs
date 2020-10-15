using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder
{
    /// <summary>
    /// ��Ŀ�������ɻ���
    /// </summary>
    public abstract class ProjectBuilder<TModelConfig> : CoderBase
            where TModelConfig : ProjectChildConfigBase, IEntityConfig
    {
        /// <summary>
        /// �õ���ǰ����Ϣ������
        /// </summary>
        public Action<string> MessageSetter { get; set; }

        /// <summary>
        /// ��ǰ������Ϣ
        /// </summary>
        public string Message
        {
            set => MessageSetter(value);
        }

        private TraceMessage _traceMessage;
        /// <summary>
        /// ��ǰ������Ϣ
        /// </summary>
        public TraceMessage TraceMessage
        {
            get => _traceMessage ?? TraceMessage.DefaultTrace;
            set => _traceMessage = value;
        }

        /// <summary>
        /// ����
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// ͼ��
        /// </summary>
        public virtual string Icon => "img_code";


        /// <summary>
        /// ����
        /// </summary>
        public abstract string Caption { get; }


        /// <summary>
        /// ��ǰ�������
        /// </summary>
        protected SolutionConfig Solution => SolutionConfig.Current;


        /// <summary>
        /// ׼������ʵ�����
        /// </summary>
        /// <param name="project"></param>
        public virtual bool Validate(ProjectConfig project)
        {
            return true;
        }

        /// <summary>
        /// ׼������ʵ�����
        /// </summary>
        /// <param name="schema"></param>
        public virtual bool Validate(TModelConfig schema)
        {
            return true;
        }

        /// <summary>
        /// ������Ŀ����
        /// </summary>
        /// <param name="project"></param>
        public virtual void CreateProjectCode(ProjectConfig project)
        {

        }

        /// <summary>
        /// ����ʵ�����
        /// </summary>
        /// <param name="project"></param>
        /// <param name="schema"></param>
        public virtual void CreateModelCode(ProjectConfig project, TModelConfig schema)
        {

        }

        /// <summary>
        /// ʵ���������ļ򵥵���
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