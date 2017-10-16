using System.Windows;

namespace Agebull.EntityModel.Designer
{
    internal class ModelViewModel : ExtendViewModelBase<EntityDesignModelEx>
    {
        public ModelViewModel()
        {
            Catalog = "模型";
        }
        /// <summary>
        /// 主面板
        /// </summary>
        public override FrameworkElement Body { get; } = new ModelCodePanel();
    }
}