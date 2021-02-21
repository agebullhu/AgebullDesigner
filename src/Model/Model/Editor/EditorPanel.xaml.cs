using System.Windows;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// ExtendPanel.xaml 的交互逻辑
    /// </summary>
    public partial class EditorPanel
    {
        public EditorPanel()
        {
            InitializeComponent();
        }

        public UIElement Editor
        {
            get => editor.Child;
            set => editor.Child = value;
        }
    }
}
