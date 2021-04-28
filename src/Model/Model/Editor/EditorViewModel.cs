using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 扩展ViewModel基类
    /// </summary>
    public class EditorViewModel : EditorViewModelBase<DesignModelBase>
    {

    }
    /// <summary>
    /// 扩展ViewModel基类
    /// </summary>
    public class PropertyGridViewModel : EditorViewModelBase<DesignModelBase>
    {
        #region 属性表格

        PropertyGrid PropertyGrid;

        private void GetPropertyGrid(DependencyObject obj)
        {
            var host = obj as WindowsFormsHost;
            // ReSharper disable PossibleNullReferenceException
            PropertyGrid = host.Child as PropertyGrid;
            PropertyGrid.PropertyValueChanged += PropertyGrid_PropertyValueChanged;
            // ReSharper restore PossibleNullReferenceException
        }

        /// <summary>
        ///     设置当前编辑的对象
        /// </summary>
        public override void SetContextConfig(DataModelDesignModel baseModel, ConfigBase cfg)
        {
            base.SetContextConfig(baseModel, cfg);
            PropertyGrid.SelectedObject = cfg;
        }

        private void PropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            string name = e.ChangedItem?.PropertyDescriptor?.Name;
            if (name == null)
                return;
            foreach (var obj in PropertyGrid.SelectedObjects)
            {
                var config = obj as ConfigBase;
                config?.RaisePropertyChanged(name);
            }
        }

        public DependencyAction PropertyGridHostBehavior => new()
        {
            AttachAction = GetPropertyGrid
        };
        #endregion
    }
}