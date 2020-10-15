using System.ComponentModel.Composition;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.Cpp
{
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
            coder.WriteDesignerCode(null);
        }

        /// <summary>
        /// ����ʵ�����
        /// </summary>
        /// <param name="project"></param>
        /// <param name="schema"></param>
        public override void CreateEntityCode(ProjectConfig project, ModelConfig schema)
        {
        }
    }
}