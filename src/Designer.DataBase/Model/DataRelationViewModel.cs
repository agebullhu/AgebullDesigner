using System.Windows;
using Agebull.EntityModel.Designer;

namespace Agebull.Common.Config.Designer.DataBase.Mysql
{
    internal class DataRelationViewModel : ExtendViewModelBase<DataRelationModel>
    {
        public DataRelationViewModel()
        {
            EditorName = "DataRelation";
        }
        /// <summary>
        /// Ö÷Ãæ°å
        /// </summary>
        public override FrameworkElement Body { get; } = new RelationPanel();
        
    }
}