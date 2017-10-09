using System.Windows.Controls;

namespace Agebull.EntityModel.Designer.Card
{
    /// <summary>
    /// ConfigForm.xaml 的交互逻辑
    /// </summary>
    public partial class ConfigForm : UserControl
    {
        public ConfigForm()
        {
            InitializeComponent();
            foreach (var res in DataTemplateResource.Resources)
                this.Resources.MergedDictionaries.Add(res);
        }
    }
}
