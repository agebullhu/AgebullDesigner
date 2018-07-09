using System.ComponentModel.Composition;
using Agebull.Common;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.Mobile
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class MobileProjectBuilde : ProjectBuilder, IAutoRegister
    {
        /// <summary>
        /// ����
        /// </summary>
        protected override string Name => "Mobile";

        /// <summary>
        /// ����
        /// </summary>
        public override string Caption => Name;
        /// <summary>
        /// ִ���Զ�ע��
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            RegistBuilder<MobileProjectBuilde>();
        }

        /// <summary>
        /// ������Ŀ����
        /// </summary>
        /// <param name="project"></param>
        public override void CreateProjectCode(ProjectConfig project)
        {
            var code = new MobileGlobalCoder();
            code.CreateBaseCode(project.MobileCsPath);
        }

        /// <summary>
        /// ����ʵ�����
        /// </summary>
        /// <param name="project"></param>
        /// <param name="schema"></param>
        public override void CreateEntityCode(ProjectConfig project, EntityConfig schema)
        {
            IOHelper.CheckPath(schema.Parent.MobileCsPath);

            Message = "��������" + schema.Caption + "...";
            if (!schema.IsReference && !string.IsNullOrWhiteSpace(project.MobileCsPath))
            {
                var entityPath = IOHelper.CheckPath(project.MobileCsPath);
                var builder = new MobileEntityCoder
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
                var structCoder = new CppStructCoder
                {
                    Entity = schema,
                    Project = project
                };
                structCoder.CreateBaseCode(cppPath);
                structCoder.CreateExtendCode(cppPath);
                if (!schema.IsClass)
                {
                    var modelCoder = new CppModelCoder
                    {
                        Entity = schema,
                        Project = project
                    };
                    modelCoder.CreateBaseCode(cppPath);
                    modelCoder.CreateExtendCode(cppPath);
                    if (!schema.IsReference)
                    {
                        var accessCoder = new CppAccessCoder
                        {
                            Entity = schema,
                            Project = project
                        };
                        accessCoder.CreateBaseCode(cppPath);
                        accessCoder.CreateExtendCode(cppPath);
                    }
                }
            }

            Message = schema.Caption + "�����";
        }
    }
}
