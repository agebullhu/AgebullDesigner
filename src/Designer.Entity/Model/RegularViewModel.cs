using System.Windows;

namespace Agebull.EntityModel.Designer
{
    internal class RegularViewModel : ExtendViewModelBase<EntityDesignModel>
    {
        public RegularViewModel()
        {
            EditorName = "Regular";
        }
        /// <summary>
        /// 主面板
        /// </summary>
        public override FrameworkElement Body { get; } = new RegularPanel();

        
    }
}