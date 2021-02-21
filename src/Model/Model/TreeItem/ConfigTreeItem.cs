using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    ///     树节点模型
    /// </summary>
    public class ConfigTreeItem<TModel> : TreeItem<TModel>
        where TModel : ConfigBase, new()
    {

        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="model"></param>
        public ConfigTreeItem(TModel model)
            : base(model)
        {
            InitDef();
        }

        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="model"></param>
        /// <param name="haseChildsAction"></param>
        /// <param name="loadChildsAction"></param>
        public ConfigTreeItem(TModel model, Func<TModel, IList> loadChildsAction, Func<TModel, bool> haseChildsAction)
            : base(model, loadChildsAction, haseChildsAction)
        {
            InitDef();
        }

        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="model"></param>
        /// <param name="createItemsAction"></param>
        /// <param name="haseChildsAction"></param>
        public ConfigTreeItem(TModel model, Func<TModel, List<TreeItem>> createItemsAction, Func<TModel, bool> haseChildsAction)
            : base(model, createItemsAction, haseChildsAction)
        {
            InitDef();
        }

        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="header"></param>
        /// <param name="model"></param>
        /// <param name="createItemsAction"></param>
        /// <param name="loadChildsAction"></param>
        public ConfigTreeItem(string header, TModel model, Func<TModel, List<TreeItem>> createItemsAction, Func<TModel, IList> loadChildsAction)
            : base(header, model, createItemsAction, loadChildsAction)
        {
            InitDef();
        }

        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="model"></param>
        /// <param name="createItemsAction"></param>
        public ConfigTreeItem(TModel model, Func<TModel, List<TreeItem>> createItemsAction)
            : base(model, createItemsAction)
        {
            InitDef();
        }

        /// <summary>
        /// 是否辅助节点
        /// </summary>
        public bool IsAssist { get; set; }

        #region 构造

        protected override void OnSourceModify()
        {
            if (IsAssist)
                return;
            if (!Equals(BackgroundColor, Source.IsModify ? Brushes.Red : Brushes.Transparent))
                BackgroundColor = Source.IsModify ? Brushes.Red : Brushes.Transparent;

            base.OnSourceModify();
        }

        private void InitDef()
        {
            if (Model is ConfigBase pp)
            {
                pp.Option.PropertyChanged += OnStatePropertyChanged;
            }
            HeaderField = "Name,Caption,Abbreviation";
            HeaderExtendExpression = FormatTitle;
            StatusField = "IsDelete,IsFreeze,Discard,IsModify";
            StatusIconName = SyncStatusImageAutomatic();

            if (Source != null)
                Source.PropertyChanged += OnModelPropertyChanged;
        }

        /// <summary>
        /// 同步自动处理
        /// </summary>
        protected override string SyncStatusImageAutomatic()
        {
            if (Model.IsDelete)
            {
                return "删除状态";
            }
            else if (Model.IsDiscard)
            {
                return "废弃状态";
            }
            else if (Model.Option.IsFreeze)
            {
                return "锁定状态";
            }
            else if (Model.IsModify)
            {
                return "修改状态";
            }
            else return "正常状态";
        }


        private string FormatTitle(TModel m)
        {
            if (m == null)
                return "...";
            var pi = m as IndependenceConfigBase;
            return pi?.Abbreviation == null
                ? $"{m.Caption}({m.Name})"
                : $"{m.Caption}({m.Name})[{pi.Abbreviation}]";
        }

        private void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ModelPropertyChanged?.Invoke(this, e);
        }

        protected override void CreateCommandList(List<CommandItemBase> commands)
        {
            var treeItem = Parent as TreeItem;
            commands.Add(new CommandItem
            {
                Source = this,
                Caption = "刷新",
                Catalog = "视图",
                Action = arg => ReBuildChild(),
                IconName = "刷新"
            });
            base.CreateCommandList(commands);
        }
        public event EventHandler<PropertyChangedEventArgs> ModelPropertyChanged;
        /// <summary>
        /// 重新生成子级
        /// </summary>
        /// <returns></returns>
        private void ReBuildChild()
        {
            var treeItem = Parent as TreeItem;

            var items = treeItem.CreateChild(Source);
            ClearItems();
            foreach (var item in items)
                Items.AddRange(item.Items);
        }

        #endregion

    }
}