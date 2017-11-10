using System.ComponentModel.Composition;
using Agebull.Common;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.Cpp
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class CppProjectBuilde : ProjectBuilder, IAutoRegister
    {
        /// <summary>
        /// ����
        /// </summary>
        protected override string Name => "Cpp";

        /// <summary>
        /// ����
        /// </summary>
        public override string Caption => Name;
        /// <summary>
        /// ִ���Զ�ע��
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            RegistBuilder<CppProjectBuilde>();
        }

        /// <summary>
        /// ������Ŀ����
        /// </summary>
        /// <param name="project"></param>
        public override void CreateProjectCode(ProjectConfig project)
        {

        }

        /// <summary>
        /// ����ʵ�����
        /// </summary>
        /// <param name="project"></param>
        /// <param name="schema"></param>
        public override void CreateEntityCode(ProjectConfig project, EntityConfig schema)
        {
            Message = "��������" + schema.Caption + "...";
            if (!schema.IsReference)
            {
                var entityPath = IOHelper.CheckPath(project.ClientCsPath);
                var builder = new ClientEntityCoder
                {
                    Entity = schema,
                    Project = project
                };
                builder.CreateBaseCode(entityPath);
                builder.CreateExtendCode(entityPath);
            }
            if (!string.IsNullOrEmpty(project.CppCodePath))
            {
                var cppPath = IOHelper.CheckPath(project.CppCodePath);
                var builder = new CppStructCoder
                {
                    Entity = schema,
                    Project = project
                };
                builder.CreateBaseCode(cppPath);
                builder.CreateExtendCode(cppPath);
            }
            if (!schema.IsClass && !string.IsNullOrEmpty(project.CppCodePath))
            {
                var cppPath = IOHelper.CheckPath(project.CppCodePath);
                {
                    var builder = new CppModelCoder
                    {
                        Entity = schema,
                        Project = project
                    };
                    builder.CreateBaseCode(cppPath);
                    builder.CreateExtendCode(cppPath);
                }
                if (!schema.IsReference)
                {
                    var builder = new CppAccessCoder
                    {
                        Entity = schema,
                        Project = project
                    };
                    builder.CreateBaseCode(cppPath);
                    builder.CreateExtendCode(cppPath);
                }
            }

            Message = schema.Caption + "�����";
        }

    }


    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class CppDataFactoryProjectBuilde : ProjectBuilder, IAutoRegister
    {
        /// <summary>
        /// ����
        /// </summary>
        protected override string Name => "CppDataFactory";

        /// <summary>
        /// ����
        /// </summary>
        public override string Caption => Name;
        /// <summary>
        /// ִ���Զ�ע��
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            RegistBuilder<CppDataFactoryProjectBuilde>();
        }

        /// <summary>
        /// ������Ŀ����
        /// </summary>
        /// <param name="project"></param>
        public override void CreateProjectCode(ProjectConfig project)
        {
            var builder = new CppDataFactoryCode();
            builder.CreateBaseCode(null);
            builder.CreateExtendCode(null);
        }

        /// <summary>
        /// ����ʵ�����
        /// </summary>
        /// <param name="project"></param>
        /// <param name="schema"></param>
        public override void CreateEntityCode(ProjectConfig project, EntityConfig schema)
        {
        }
    }

    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class EsFundsProjectBuilde : ProjectBuilder, IAutoRegister
    {
        /// <summary>
        /// ����
        /// </summary>
        protected override string Name => "EsFunds";

        /// <summary>
        /// ����
        /// </summary>
        public override string Caption => "��ʢ�ڻ��ӿڴ���";
        /// <summary>
        /// ִ���Զ�ע��
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            RegistBuilder<EsFundsProjectBuilde>();
        }
        /// <summary>
        /// ������Ŀ����
        /// </summary>
        /// <param name="project"></param>
        public override void CreateProjectCode(ProjectConfig project)
        {
            var coder = new EsPublishCoder
            {
                Project = project
            };
            coder.CreateBaseCode(null);
        }

        /// <summary>
        /// ����ʵ�����
        /// </summary>
        /// <param name="project"></param>
        /// <param name="schema"></param>
        public override void CreateEntityCode(ProjectConfig project, EntityConfig schema)
        {
        }
    }
}
