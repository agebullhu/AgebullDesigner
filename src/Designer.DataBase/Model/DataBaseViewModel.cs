using System.Windows;
using Agebull.CodeRefactor.SolutionManager;
using Agebull.Common.Designer.View;

namespace Agebull.Common.Config.Designer.DataBase.Mysql
{
    internal class DataBaseViewModel : ExtendViewModelBase<DataBaseModel>
    {
        /// <summary>
        /// �����
        /// </summary>
        public override FrameworkElement Body { get; } = new DataBasePanel();
        
    }
}