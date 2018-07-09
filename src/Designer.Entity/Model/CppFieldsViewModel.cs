using System.Collections.ObjectModel;
using System.Windows;

namespace Agebull.EntityModel.Designer
{
    internal class CppFieldsViewModel : ExtendViewModelBase<EntityDesignModel>
    {
        public CppFieldsViewModel()
        {
            EditorName = "C++字段";
        }
        /// <summary>
        /// 主面板
        /// </summary>
        public override FrameworkElement Body { get; } = new CppFieldsPanel();
    }
}