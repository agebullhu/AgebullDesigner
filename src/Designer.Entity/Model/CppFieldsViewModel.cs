using System.Windows;

namespace Agebull.EntityModel.Designer
{
    internal class CppFieldsViewModel : ExtendViewModelBase<EntityDesignModel>
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
    internal class FieldsViewModel : ExtendViewModelBase<EntityDesignModel>
    {
        public FieldsViewModel()
        {
            Catalog = "基本";
        }
        /// <summary>
        /// 主面板
        /// </summary>
        public override FrameworkElement Body { get; } = new FieldsPanel();
    }
    internal class ModelViewModel : ExtendViewModelBase<EntityDesignModel>
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
    internal class RegularViewModel : ExtendViewModelBase<EntityDesignModel>
    {
        public RegularViewModel()
        {
            Catalog = "规则";
        }
        /// <summary>
        /// 主面板
        /// </summary>
        public override FrameworkElement Body { get; } = new RegularPanel();


    }
}