using System.ComponentModel.Composition;
using Gboxt.Common.DataAccess.Schemas;

namespace Agebull.Common.SimpleDesign.WebApplition.EasyUi
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class WebserviceProjectBuilde : ProjectBuilder, IAutoRegister
    {
        /// <summary>
        /// ����
        /// </summary>
        protected override string Name => "Webservice";

        /// <summary>
        /// ����
        /// </summary>
        public override string Caption => Name;
        /// <summary>
        /// ִ���Զ�ע��
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            RegistBuilder<WebserviceProjectBuilde>();
        }

        /// <summary>
        /// ������Ŀ����
        /// </summary>
        /// <param name="project"></param>
        public override void CreateProjectCode(ProjectConfig project)
        {
            var wcf = new WebServiceBuilder();
            wcf.CreateBaseCode(null);
            wcf.CreateExtendCode(null);
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
