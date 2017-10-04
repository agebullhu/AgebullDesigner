using System.ComponentModel.Composition;
using Agebull.Common.SimpleDesign.AspNet;
using Gboxt.Common.DataAccess.Schemas;

namespace Agebull.Common.SimpleDesign.WebApplition.EasyUi
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
            var pg = new PageGenerator
            {
                Entity = schema,
                Project = schema.Parent,
            };
            pg.CreateBaseCode(project.PagePath);
        }
    }
}