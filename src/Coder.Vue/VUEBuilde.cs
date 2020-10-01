using System.ComponentModel.Composition;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.VUE
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class VUEBuilde : ProjectBuilder, IAutoRegister
    {
        /// <summary>
        /// ����
        /// </summary>
        protected override string Name => "VUE";

        /// <summary>
        /// ����
        /// </summary>
        public override string Caption => Name;
        /// <summary>
        /// ִ���Զ�ע��
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            RegistBuilder<VUEBuilde>();
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
        public override void CreateEntityCode(ProjectConfig project, ModelConfig schema)
        {
            var pg = new PorjectVUEGenerator<ModelConfig>
            {
                Model = schema,
                Project = schema.Parent,
            };
            pg.WriteDesignerCode(project.PagePath);
            pg.WriteCustomCode(project.PagePath);
        }
    }
}