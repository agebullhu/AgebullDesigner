using System.ComponentModel.Composition;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;
using Agebull.EntityModel.RobotCoder.AspNet;

namespace Agebull.EntityModel.RobotCoder.EasyUi
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class EasyUiProjectBuilde : ProjectBuilder, IAutoRegister
    {
        /// <summary>
        /// ����
        /// </summary>
        protected override string Name => "EasyUi";

        /// <summary>
        /// ����
        /// </summary>
        public override string Caption => Name;
        /// <summary>
        /// ִ���Զ�ע��
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            RegistBuilder<EasyUiProjectBuilde>();
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
            if (!schema.HaseEasyUi)
            {
                TraceMessage.Track = $"{schema.Caption}:δ����ҳ������,�޷�����ҳ�����";
                return;
            }
            var pg = new PageGenerator
            {
                Model = schema,
                Project = schema.Parent,
            };
            pg.WriteDesignerCode(project.PagePath);
        }
    }
}