using System.Windows;
using Agebull.CodeRefactor.SolutionManager;
using Agebull.Common.SimpleDesign;

namespace Agebull.Common.Config.Designer.DataBase.Mysql
{
    internal class DataRelationViewModel : ExtendViewModelBase<DataRelationModel>
    {
        /// <summary>
        /// �����
        /// </summary>
        public override FrameworkElement Body { get; } = new RelationPanel();
        
    }
}