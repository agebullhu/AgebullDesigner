using System.Windows;

namespace Agebull.EntityModel.Designer
{
    internal class TemplateCodeViewModel : ExtendViewModelBase<TemplateCodeModel>
    {
        /// <summary>
        /// �����
        /// </summary>
        public override FrameworkElement Body { get; } = new TemplatePanel();


        /// <summary>
        /// ��ͼ�󶨳ɹ���ĳ�ʼ������
        /// </summary>
        protected override void OnViewSeted()
        {
            base.OnViewSeted();
            Model.Model = BaseModel;
            Model.Context = Context;
        }
    }
}