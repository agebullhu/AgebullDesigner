// /***********************************************************************************************************************
// 工程：Agebull.EntityModel.Designer
// 项目：CodeRefactor
// 文件：DataAccessDesignModel.cs
// 作者：Administrator/
// 建立：2015－07－13 12:26
// ****************************************************文件说明**********************************************************
// 对应文档：
// 说明摘要：
// 作者备注：
// ****************************************************修改记录**********************************************************
// 日期：
// 人员：
// 说明：
// ************************************************************************************************************************
// 日期：
// 人员：
// 说明：
// ************************************************************************************************************************
// 日期：
// 人员：
// 说明：
// ***********************************************************************************************************************/

#region 命名空间引用

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Agebull.EntityModel.Config;

#endregion

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 数据模型设计模型
    /// </summary>
    public sealed class DataModelDesignModel : ModelBase
    {
        #region 设计对象 

        public DesignContext Context { get; }
        public DesignGlobal Global { get; }
        public TreeModel Tree { get; }
        public ConfigIoModel  ConfigIo { get; }
        public NormalCodeModel NormalCode { get; }

        
        public ExtendConfigModel ExtendConfig { get; }

        
        public static DataModelDesignModel Current { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        public DataModelDesignModel()
        {
            Current = this;
            Context = new DesignContext();

            Global = new DesignGlobal
            {
                Model = this,
                Dispatcher = Dispatcher,
                Context = Context
            };
            GlobalConfig.SetGlobal(Global);
            ExtendConfig = new ExtendConfigModel
            {
                Model = this,
                Dispatcher = Dispatcher,
                Context = Context
            };
            Tree = new TreeModel
            {
                Model = this,
                Dispatcher = Dispatcher,
                Context = Context
            };
            ConfigIo = new ConfigIoModel
            {
                Model = this,
                Dispatcher = Dispatcher,
                Context = Context
            };
            NormalCode = new NormalCodeModel
            {
                Model = this,
                Dispatcher = Dispatcher,
                Context = Context
            };
        }

        #endregion
        
        #region 初始化

        /// <summary>
        ///     初始化
        /// </summary>
        protected override void DoInitialize()
        {
            base.DoInitialize();
            ExtendConfig.Initialize();
            Context.Initialize();
            ConfigIo.Initialize();
            NormalCode.Initialize();
            Context.PropertyChanged += Context_PropertyChanged;
            //foreach (var ex in ExtendModels.Values)
            //{
            //    ex.Model = this;
            //    ex.Dispatcher = Dispatcher;
            //    ex.Context = Context;
            //    ex.Initialize();
            //}
        }
        /// <summary>
        /// 保证载入后选择正常
        /// </summary>
        internal void FirstSelect()
        {
            GlobalConfig.CurrentConfig = null;
            Tree.SelectItem = null;
            Tree.SelectItem = Tree.TreeRoot.Items[0];
            GlobalConfig.CurrentSolution.GodMode = true;
        }
        /// <summary>
        /// 新增对象
        /// </summary>
        /// <typeparam name="TConfig"></typeparam>
        /// <param name="title"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public bool CreateNew<TConfig>(string title,out TConfig config)
            where TConfig : ConfigBase, new()
        {
            config = new TConfig();
            return CommandIoc.NewConfigCommand(title, config);
        }

        #endregion

        #region 扩展对象

        /// <summary>
        /// 扩展对象插入的控件
        /// </summary>
        internal TabControl ExtendControl { get; set; }

        /// <summary>
        /// 扩展模型
        /// </summary>
        public Dictionary<string, DesignModelBase> ExtendModels { get; } = new Dictionary<string, DesignModelBase>();

        private void Context_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Context.SelectConfig))
                AddExtend();
        }


        private void AddExtend()
        {
            ExtendModels.Clear();
            ExtendControl.Items.Clear();
            while (ExtendControl.Items.Count > 1)
                ExtendControl.Items.RemoveAt(1);
            if (Context.SelectConfig == null)
            {
                return;
            }
            Dictionary<string, Func<ExtendViewModelBase>> exts;
            if (!DesignerManager.ExtendDictionary.TryGetValue(Context.SelectConfig.GetType(), out exts))
                return;
            foreach (var ext in exts)
            {
                var vm = ext.Value();
                vm.DesignModel.Catalog = vm.Catalog;
                vm.BaseModel = this;
                vm.DesignModel.Initialize();
                this.ExtendModels.Add(ext.Key, vm.DesignModel);
                var item = new TabItem
                {
                    Header = ext.Key,
                    Content = new ExtendPanel
                    {
                        DataContext = vm
                    },
                    DataContext = ExtendControl.DataContext 
                };
                int idx = ExtendControl.Items.Add(item);
                if (!Context.ChildrenJobs.ContainsKey(ext.Key))
                    Context.ChildrenJobs.Add(ext.Key, idx);
                else
                    Trace.WriteLine(ext.Key, "同名扩展设计器");
            }
            Context.RaisePropertyChanged(nameof(Context.Jobs));
        }
        
        #endregion

    }
}