using System.Windows;
using Agebull.CodeRefactor.SolutionManager;
using Agebull.Common.SimpleDesign;

namespace Agebull.Common.Config.Designer.DataBase.Mysql
{
    internal class DataRelationViewModel : ExtendViewModelBase<DataRelationModel>
    {
        /// <summary>
        /// Ö÷Ãæ°å
        /// </summary>
        public override FrameworkElement Body { get; } = new RelationPanel();
        
    }
}