using System;
using System.Collections.Generic;
using Agebull.Common.DataModel;
using Gboxt.Common.DataAccess.Schemas;

namespace Agebull.Common.SimpleDesign
{
    /// <summary>
    /// ��Ŀ�������ɻ���
    /// </summary>
    public abstract class ProjectBuilder : CoderBase
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
            set
            {
                MessageSetter(value);
            }
        }

        private TraceMessage _traceMessage;
        /// <summary>
        /// ��ǰ������Ϣ
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
        /// ȡ��Ĭ�ϵĸ�·��
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        protected virtual string RootPath(ProjectConfig project)
        {
            return project["project_path_" + Name] ?? (project["project_path_" + Name] = project.ModelPath);
        }

        /// <summary>
        /// ����
        /// </summary>
        protected abstract string Name { get; }

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
        /// <param name="project"></param>
        /// <param name="schema"></param>
        public virtual bool Validate(ProjectConfig project, EntityConfig schema)
        {
            return true;
        }

        /// <summary>
        /// ����ʵ�����
        /// </summary>
        /// <param name="project"></param>
        /// <param name="schema"></param>
        public virtual void CreateEntityCode(ProjectConfig project, EntityConfig schema)
        {

        }

        /// <summary>
        /// ������Ŀ����
        /// </summary>
        /// <param name="project"></param>
        public virtual void CreateProjectCode(ProjectConfig project)
        {

        }

        /// <summary>
        /// ע�����Ŀ����������
        /// </summary>
        public static readonly Dictionary<string, Func<ProjectBuilder>> Builders =
            new Dictionary<string, Func<ProjectBuilder>>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// ע�����Ŀ������
        /// </summary>
        /// <returns></returns>
        public static void RegistBuilder<TBuilder>()
            where TBuilder : ProjectBuilder, new()
        {
            var builder = new TBuilder();
            if (Builders.ContainsKey(builder.Name))
                throw new ArgumentException("��ע������Ϊ" + builder.Name + "����Ŀ����������Ӧ���ظ�ע��");
            Builders.Add(builder.Name, () => new TBuilder());
        }

        /// <summary>
        /// ���ɺ��ʵ���Ŀ������
        /// </summary>
        /// <param name="type">��Ŀ����</param>
        /// <returns>��Ŀ������</returns>
        public static ProjectBuilder CreateBuilder(string type)
        {
            if (string.IsNullOrEmpty(type))
                throw new ArgumentException("�����Ƶ���Ŀ�������ǲ������");
            if (Builders.ContainsKey(type))
                return Builders[type]();
            throw new ArgumentException("����Ϊ" + type + "����Ŀ������δע��");
        }
        /// <summary>
        /// ʵ���������ļ򵥵���
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