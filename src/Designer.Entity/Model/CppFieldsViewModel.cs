using System.Windows;
using Agebull.Common.Designer;
using Agebull.Common.SimpleDesign;

namespace Agebull.CodeRefactor.SolutionManager
{
    internal class CppFieldsViewModel : ExtendViewModelBase<EntityDesignModel>
    {
        /// <summary>
        /// 主面板
        /// </summary>
        public override FrameworkElement Body { get; } = new CppFieldsPanel();
    }
    internal class FieldsViewModel : ExtendViewModelBase<EntityDesignModel>
    {
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