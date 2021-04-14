using Agebull.EntityModel;
using Agebull.EntityModel.Designer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ZeroCoder.Editor
{
    /// <summary>
    /// MofityPanel.xaml 的交互逻辑
    /// </summary>
    public partial class MofityPanel : UserControl
    {
        public MofityPanel()
        {
            InitializeComponent();
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(grid.SelectedItem is ValueRecordItem obj && obj.Current is IModifyObject mo)
            {
                var ctx = this.DataContext as EditorViewModel;
                ctx.Model.Context.ValueRecords = mo.ValueRecords;
            }
        }
    }
}
