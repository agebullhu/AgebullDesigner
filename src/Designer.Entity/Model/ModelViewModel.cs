using System.Windows;

namespace Agebull.EntityModel.Designer
{
    internal class ModelViewModel : ExtendViewModelBase<EntityDesignModel>
    {
        public ModelViewModel()
        {
            EditorName = "Entity";
        }
        /// <summary>
        /// �����
        /// </summary>
        public override FrameworkElement Body { get; } = new ModelCodePanel();
    }
}