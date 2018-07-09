using System.Windows;
using Agebull.EntityModel.Designer;

namespace Agebull.Common.Config.Designer.DataBase.Mysql
{
    internal class DataBaseViewModel : ExtendViewModelBase<DataBaseModel>
    {
        public DataBaseViewModel()
        {
            EditorName = "DataBase";
        }

        /// <summary>
        /// �����
        /// </summary>
        public override FrameworkElement Body { get; } = new DataBasePanel();
        
    }
}