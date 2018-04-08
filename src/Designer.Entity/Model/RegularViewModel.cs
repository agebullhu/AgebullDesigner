using System.Windows;

namespace Agebull.EntityModel.Designer
{
    internal class RegularViewModel : ExtendViewModelBase<EntityDesignModelEx>
    {
        /// <summary>
        /// 主面板
        /// </summary>
        public override FrameworkElement Body { get; } = new RegularPanel();


    }
}