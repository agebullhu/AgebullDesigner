using System.Windows;

namespace Agebull.EntityModel.Designer
{
    internal class TemplateCodeViewModel : ExtendViewModelBase<TemplateCodeModel>
    {
        /// <summary>
        /// 视图绑定成功后的初始化动作
        /// </summary>
        protected override void OnViewSeted()
        {
            base.OnViewSeted();
            Model.Model = BaseModel;
            Model.Context = Context;
        }
    }
}