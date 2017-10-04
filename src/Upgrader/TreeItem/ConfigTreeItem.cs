using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Agebull.Common.DataModel;
using Gboxt.Common.WpfMvvmBase.Commands;

namespace Gboxt.Common.DataAccess.Schemas
{
    /// <summary>
    ///     树节点模型
    /// </summary>
    public class ConfigTreeItem<TModel> : TreeItem<TModel>
        where TModel : ConfigBase
    {
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
            var par = Parent as TreeItem;
            if (par?.Source != null && par.Source.IsModify != Source.IsModify)
            {
                par.Source.IsModify = Source.IsModify;
            }
            base.OnSourceModify();
        }

        private void InitDef()
        {
            Source.PropertyChanged += OnModelPropertyChanged;
            HeaderField = "Name,Caption,Abbreviation";
            HeaderExtendExpression = m => $"{m.Caption}({m.Name})[{(m as ParentConfigBase)?.Abbreviation}]";//〖{m.Description}〗

            StatusField = "IsReference,IsDelete,IsFreeze,Discard";
            StatusExpression = p => GetImage(p);

        }

        protected override void CreateCommandList(List<CommandItem> commands)
        {
            commands.Add(new CommandItem
            {
                NoButton = true,
                Parameter = this,
                Name = "刷新",
                Command = new DelegateCommand(ReBuildChild),
                Image = Application.Current.Resources["img_flush"] as ImageSource
            });
            base.CreateCommandList(commands);
        }

        private void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ModelPropertyChanged?.Invoke(this, e);
        }

        public event EventHandler<PropertyChangedEventArgs> ModelPropertyChanged;
        /// <summary>
        /// 重新生成子级
        /// </summary>
        /// <returns></returns>
        private void ReBuildChild()
        {
            var treeItem = Parent as TreeItem;
            if (treeItem == null)
                return;
            TreeItem item = treeItem.CreateChild(Source);
            Items.Clear();
            Items.AddRange(item.Items);
        }

        private BitmapImage GetImage(TModel m)
        {
            var configBase = m as ParentConfigBase;
            bool isRef = configBase != null && configBase.IsReference;
            return isRef ? imgRef : m.IsDelete ? imgDel : m.Discard ? imgDiscard : m.IsFreeze ? imgLock : imgDefault;
        }
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

        #endregion
        #region 默认方法

        private static readonly BitmapImage imgRef = Application.Current.Resources["img_ref"] as BitmapImage;

        private static readonly BitmapImage imgLock = Application.Current.Resources["img_lock"] as BitmapImage;

        private static readonly BitmapImage imgDel = Application.Current.Resources["img_del"] as BitmapImage;

        private static readonly BitmapImage imgDiscard = Application.Current.Resources["img_discard"] as BitmapImage;

        private static readonly BitmapImage imgModify = Application.Current.Resources["img_modify"] as BitmapImage;

        private static readonly BitmapImage imgDefault = Application.Current.Resources["img_no_modify"] as BitmapImage;

        #endregion
        
    }
}