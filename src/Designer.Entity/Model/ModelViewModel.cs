using System.Windows;

namespace Agebull.EntityModel.Designer
{
    internal class ModelViewModel : ExtendViewModelBase<EntityDesignModelEx>
    {
        /// <summary>
        /// �����
        /// </summary>
        public override FrameworkElement Body { get; } = new ModelCodePanel();
    }
}