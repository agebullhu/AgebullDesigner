// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// 建立:2014-11-20
// 修改:2014-11-27
// *****************************************************/

#region 引用

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using System.Windows.Media;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;
using Application = System.Windows.Application;
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
            set { Model.Context.SelectConfigs = value; }
        }

        public DataModelDesignViewModel()
        {
            TypeConvert.Register();
        }

        #endregion

        #region 工程自有命令

        private List<CommandItem> _solutionCommands;
        public List<CommandItem> SolutionCommands => _solutionCommands ?? (_solutionCommands = new List<CommandItem>
        {
            new CommandItem
            {
                Command = new DelegateCommand(Model.ConfigIo.CreateNew),
                Name = "新建",
                Image = Application.Current.Resources["img_add"] as ImageSource
            },
            new CommandItem
            {
                Command = new DelegateCommand(Model.ConfigIo.Load),
                Name = "打开",
                Image = Application.Current.Resources["img_open"] as ImageSource
            },
            new CommandItem
            {
                Command = new DelegateCommand(Model.ConfigIo.LoadGlobal),
                Name = "全局",
                Image = Application.Current.Resources["img_open"] as ImageSource
            },
            new CommandItem
            {
                Command = new DelegateCommand(Model.ConfigIo.ReLoad),
                Name = "重新载入",
                Image = Application.Current.Resources["img_redo"] as ImageSource
            },
            new CommandItem
            {
                Command = new DelegateCommand(Model.ConfigIo.Save),
                Name = "保存",
                Image = Application.Current.Resources["imgSave"] as ImageSource
            }
        });

        #endregion

        #region 属性表格


        private void GetPropertyGrid(DependencyObject obj)
        {
            var host = obj as WindowsFormsHost;
            // ReSharper disable PossibleNullReferenceException
            Model.Context.PropertyGrid = host.Child as PropertyGrid;
            Model.Context.PropertyGrid.PropertyValueChanged += PropertyGrid_PropertyValueChanged;
            // ReSharper restore PossibleNullReferenceException
        }

        private void PropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            string name = e.ChangedItem?.PropertyDescriptor?.Name;
            if (name == null)
                return;
            foreach (var obj in Model.Context.PropertyGrid.SelectedObjects)
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

        public DependencyAction TabControlBehavior => new DependencyAction
        {
            AttachAction =  obj => Model.ExtendControl = (TabControl)obj
        };

        #endregion
    }
}