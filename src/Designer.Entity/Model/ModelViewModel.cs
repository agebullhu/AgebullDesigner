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
        /// Ö÷Ãæ°å
        /// </summary>
        public override FrameworkElement Body { get; } = new ModelCodePanel();
    }
}