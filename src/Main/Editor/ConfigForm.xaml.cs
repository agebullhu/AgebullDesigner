namespace Agebull.EntityModel.Designer.Card
{
    /// <summary>
    /// ConfigForm.xaml 的交互逻辑
    /// </summary>
    public partial class ConfigForm
    {
        public ConfigForm()
        {
            InitializeComponent();
            foreach (var res in ResourceExtension.Resources)
                this.Resources.MergedDictionaries.Add(res);
        }
    }
}
