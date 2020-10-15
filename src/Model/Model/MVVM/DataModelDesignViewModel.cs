// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// 建立:2014-11-20
// 修改:2014-11-27
// *****************************************************/

#region 引用

using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;
using PropertyGrid = System.Windows.Forms.PropertyGrid;
using PropertyValueChangedEventArgs = System.Windows.Forms.PropertyValueChangedEventArgs;

#endregion

namespace Agebull.EntityModel.Designer
{
    public sealed class DataModelDesignViewModel : ViewModelBase<DataModelDesignModel>, IGridSelectionBinding
    {
        #region 基础

        IList IGridSelectionBinding.SelectColumns
        {
            set => Model.Context.SelectConfigs = value;
        }

        /// <summary>
        /// 视图绑定成功后的初始化动作
        /// </summary>
        protected override void OnViewSeted()
        {
            GlobalTrigger.Dispatcher = Dispatcher;
            base.OnViewSeted();
        }

        //public DataModelDesignViewModel()
        //{
        //    TypeConvert.Register();
        //}

        #endregion

        #region 属性表格


        private void GetPropertyGrid(DependencyObject obj)
        {
            var host = obj as WindowsFormsHost;
            // ReSharper disable PossibleNullReferenceException
            Model.Editor.PropertyGrid = host.Child as PropertyGrid;
            Model.Editor.PropertyGrid.PropertyValueChanged += PropertyGrid_PropertyValueChanged;
            // ReSharper restore PossibleNullReferenceException
        }

        private void PropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            string name = e.ChangedItem?.PropertyDescriptor?.Name;
            if (name == null)
                return;
            foreach (var obj in Model.Editor.PropertyGrid.SelectedObjects)
            {
                var config = obj as ConfigBase;
                config?.RaisePropertyChanged(name);
            }
        }

        public DependencyAction PropertyGridHostBehavior => new DependencyAction
        {
            AttachAction = GetPropertyGrid
        };
        #endregion

        #region 扩展

        public DelegateCommand SaveCommand => new DelegateCommand(Model.ConfigIo.SaveProject);

        public DependencyAction TabControlBehavior => new DependencyAction
        {
            AttachAction = obj => Model.Editor.ExtendEditorPanel = (TabControl)obj
        };


        public DependencyAction WebBrowserBehavior => new DependencyAction
        {
            AttachAction = obj => Model.NormalCode.Browser = (WebBrowser)obj
        };
        #endregion
    }
}