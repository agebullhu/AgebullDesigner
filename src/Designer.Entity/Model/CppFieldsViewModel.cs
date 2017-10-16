using System.Collections.ObjectModel;
using System.Windows;

namespace Agebull.EntityModel.Designer
{
    internal class CppFieldsViewModel : ExtendViewModelBase<EntityDesignModelEx>
    {
        public CppFieldsViewModel()
        {
            Catalog = "C++";
        }
        /// <summary>
        /// 主面板
        /// </summary>
        public override FrameworkElement Body { get; } = new CppFieldsPanel();
    }
}