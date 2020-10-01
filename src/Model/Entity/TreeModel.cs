using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 树形呈现模型
    /// </summary>
    public class TreeModel : DesignModelBase
    {
        #region 构造

        /// <summary>
        ///     当前配置树
        /// </summary>
        public TreeRoot TreeRoot { get; }

        /// <summary>
        /// 
        /// </summary>
        public TreeModel()
        {
            TreeRoot = new TreeRoot();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        protected override void DoInitialize()
        {
        }
        /// <summary>
        /// 同步解决方案变更
        /// </summary>
        public override void OnSolutionChanged()
        {
            using (WorkModelScope.CreateScope(WorkModel.Show))
                CreateTree();
        }
        /// <summary>
        /// 
        /// </summary>
        public void CreateTree()
        {
            TreeRoot.SelectItemChanged -= OnTreeSelectItemChanged;
            TreeRoot.Items.Clear();
            var node = new ConfigTreeItem<SolutionConfig>(Context.Solution)
            {
                IsExpanded = true,
                Header = Context.Solution.Caption,
                HeaderField = "Caption,Name",
                HeaderExtendExpression = p => p.Caption ?? p.Name,
                CreateChildFunc = CreateProjectTreeItem,
                SoruceItemsExpression = () => Context.Solution.ProjectList,
                SoruceTypeIcon = Application.Current.Resources["tree_Solution"] as BitmapImage
            };
            TreeRoot.Items.Add(node);
            TreeRoot.SelectItemChanged += OnTreeSelectItemChanged;
        }

        #endregion


        #region 构建

        #region 项目

        private TreeItem CreateProjectTreeItem(object arg)
        {
            var project = (ProjectConfig)arg;
            TreeItem node = new ConfigTreeItem<ProjectConfig>(project)
            {
                IsAssist = true,
                IsExpanded = true,
                SoruceTypeIcon = Application.Current.Resources["tree_Folder"] as BitmapImage
            };
            var eitem = new ConfigTreeItem<ProjectConfig>(project)
            {
                IsAssist = true,
                Header = "实体",
                SoruceView = "entity",
                HeaderField = null,
                CreateChildFunc = CreateEntityClassifiesTreeItem,
                SoruceItemsExpression = () => project.Classifies,
                SoruceTypeIcon = Application.Current.Resources["tree_Folder"] as BitmapImage
            };
            node.Items.Add(eitem);
            var mitem = new ConfigTreeItem<ProjectConfig>(project)
            {
                IsAssist = true,
                Header = "模型",
                SoruceView = "model",
                HeaderField = null,
                CreateChildFunc = CreateModelTreeItem,
                SoruceItemsExpression = () => project.Models,
                SoruceTypeIcon = Application.Current.Resources["tree_Folder"] as BitmapImage
            };
            node.Items.Add(mitem);
            //Model.CppModel.AddCppApiNode(node);
            AddCustomTypeNode(node, project);

            var citem = new ConfigTreeItem<ProjectConfig>(project)
            {
                IsAssist = true,
                Header = "API",
                Tag = "API",
                SoruceView = "api",
                HeaderField = null,
                CreateChildFunc = CreateApiItemTreeItem,
                SoruceItemsExpression = () => project.ApiItems,
                SoruceTypeIcon = Application.Current.Resources["tree_Folder"] as BitmapImage
            };
            node.Items.Add(citem);
            return node;
        }

        #endregion

        #region 实体

        private TreeItem CreateEntityClassifiesTreeItem(object charg)
        {
            var child = (EntityClassify)charg;
            return new ConfigTreeItem<EntityClassify>(child)
            {
                IsAssist = true,
                Friend = child.Project,
                FriendView = "entity",
                IsExpanded = false,
                Tag = "Entity",
                CreateChildFunc = CreateEntityTreeItem,
                FriendItems = child.Items,
                SoruceItemsExpression = () => child.Items,
                SoruceTypeIcon = Application.Current.Resources["tree_Folder"] as BitmapImage
            };
        }

        internal TreeItem CreateEntityTreeItem(object arg)
        {
            if (!(arg is EntityConfig entity))
                return null;
            foreach (var col in entity.Properties)
                col.Entity = entity;
            var tableItem = new ConfigTreeItem<EntityConfig>(entity)
            {
                CreateChildFunc = CreateFieldTreeItem,
                SoruceItemsExpression = () => entity.Properties,
                CustomPropertyChanged = Entity_PropertyChanged
            };
            return tableItem;
        }

        private void Entity_PropertyChanged(TreeItem item, NotificationObject arg, string name)
        {
            var entity = (EntityConfig)arg;
            switch (name)
            {
                case null:
                case nameof(EntityConfig.NoDataBase):
                    item.SoruceTypeIcon = Application.Current.Resources[entity.NoDataBase ? "tree_Type" : "tree_Child4"] as BitmapImage;
                    break;
            }
        }


        private TreeItem CreateFieldTreeItem(object arg)
        {
            var property = (FieldConfig)arg;
            return new ConfigTreeItem<FieldConfig>(property)
            {
                Color = property.IsSystemField ? Brushes.Blue : Brushes.Black,
                FontWeight = property.IsCompute ? FontWeights.Thin : FontWeights.DemiBold,
                CustomPropertyChanged = FieldPropertyChanged
            };
        }

        private void FieldPropertyChanged(TreeItem item, NotificationObject arg, string name)
        {
            var property = (FieldConfig)arg;
            switch (name)
            {
                case null:
                case nameof(FieldConfig.IsPrimaryKey):
                case nameof(FieldConfig.IsEnum):
                case nameof(FieldConfig.IsDiscard):
                case nameof(FieldConfig.DbInnerField):
                case nameof(FieldConfig.IsInterfaceField):
                case nameof(FieldConfig.CustomType):
                case nameof(FieldConfig.CustomWrite):
                case nameof(FieldConfig.IsCompute):
                case nameof(FieldConfig.ComputeGetCode):
                case nameof(FieldConfig.ComputeSetCode):
                    if (property.IsPrimaryKey)
                        item.SoruceTypeIcon = Application.Current.Resources["tree_default"] as BitmapImage;
                    else if (property.IsDiscard)
                        item.SoruceTypeIcon = Application.Current.Resources["img_clear"] as BitmapImage;
                    else if (property.DbInnerField)
                        item.SoruceTypeIcon = Application.Current.Resources["img_lock"] as BitmapImage;
                    else if (property.IsInterfaceField)
                        item.SoruceTypeIcon = Application.Current.Resources["img_face"] as BitmapImage;
                    else if (property.IsEnum)
                        item.SoruceTypeIcon = Application.Current.Resources["tree_Child4"] as BitmapImage;
                    else if (!string.IsNullOrWhiteSpace(property.CustomType))
                        item.SoruceTypeIcon = Application.Current.Resources["img_man"] as BitmapImage;
                    else if (property.CustomWrite)
                        item.SoruceTypeIcon = Application.Current.Resources["tree_item"] as BitmapImage;
                    else if (!string.IsNullOrWhiteSpace(property.ComputeGetCode) || !string.IsNullOrWhiteSpace(property.ComputeSetCode))
                        item.SoruceTypeIcon = Application.Current.Resources["img_code"] as BitmapImage;
                    else if (property.IsCompute)
                        item.SoruceTypeIcon = Application.Current.Resources["img_sum"] as BitmapImage;
                    else
                        item.SoruceTypeIcon = Application.Current.Resources["tree_File"] as BitmapImage;
                    break;
            }
            switch (name)
            {
                case null:
                case nameof(FieldConfig.CustomType):
                case nameof(FieldConfig.ReferenceType):
                    item.Items.Clear();
                    if (property.CustomType == null)
                        break;
                    property.EnumConfig = GlobalConfig.GetEnum(property.CustomType);
                    if (property.EnumConfig == null)
                        break;
                    item.Items.Add(CreateEnumTreeItem(property.EnumConfig));
                    return;
                case nameof(FieldConfig.EnumConfig):
                    item.Items.Clear();
                    if (property.EnumConfig == null)
                        return;
                    item.Items.Add(CreateEnumTreeItem(property.EnumConfig));
                    break;
            }
        }


        private bool LoopCheck(TreeItemBase item, object source)
        {
            if (item == null)
                return false;
            if (item.Source != null && item.Source == source)
                return true;
            return LoopCheck(item.Parent, source);
        }


        private TreeItem CreateCommandTreeItem(object arg)
        {
            var cmd = (UserCommandConfig)arg;
            var colItem = new ConfigTreeItem<UserCommandConfig>(cmd)
            {
                IsExpanded = true,
                HeaderField = "Caption",
                HeaderExtendExpression = p => p.Caption,
                SoruceTypeIcon = Application.Current.Resources["tree_Folder"] as BitmapImage
            };
            return colItem;
        }

        #endregion
        #region 模型

        internal TreeItem CreateModelTreeItem(object arg)
        {
            if (!(arg is ModelConfig entity))
                return null;
            foreach (var col in entity.Properties)
                col.Model = entity;
            var tableItem = new ConfigTreeItem<ModelConfig>(entity)
            {
                CreateChildFunc = CreatePropertyTreeItem,
                SoruceItemsExpression = () => entity.Properties,
                CustomPropertyChanged = ModelPropertyChanged
            };
            return tableItem;
        }

        private TreeItem CreatePropertyTreeItem(object arg)
        {
            var property = (PropertyConfig)arg;
            return new ConfigTreeItem<PropertyConfig>(property)
            {
                Color = property.IsSystemField ? Brushes.Blue : Brushes.Black,
                FontWeight = property.IsCompute ? FontWeights.Thin : FontWeights.DemiBold,
                CustomPropertyChanged = Property_PropertyChanged
            };
        }

        private void Property_PropertyChanged(TreeItem item, NotificationObject arg, string name)
        {
            var cfg = (PropertyConfig)arg;
            var property = cfg.Field;
            switch (name)
            {
                case null:
                case nameof(FieldConfig.IsPrimaryKey):
                case nameof(FieldConfig.IsEnum):
                case nameof(FieldConfig.IsDiscard):
                case nameof(FieldConfig.DbInnerField):
                case nameof(FieldConfig.IsInterfaceField):
                case nameof(FieldConfig.CustomType):
                case nameof(FieldConfig.CustomWrite):
                case nameof(FieldConfig.IsCompute):
                case nameof(FieldConfig.ComputeGetCode):
                case nameof(FieldConfig.ComputeSetCode):
                    if (property.IsPrimaryKey)
                        item.SoruceTypeIcon = Application.Current.Resources["tree_default"] as BitmapImage;
                    else if (property.IsDiscard)
                        item.SoruceTypeIcon = Application.Current.Resources["img_clear"] as BitmapImage;
                    else if (property.DbInnerField)
                        item.SoruceTypeIcon = Application.Current.Resources["img_lock"] as BitmapImage;
                    else if (property.IsInterfaceField)
                        item.SoruceTypeIcon = Application.Current.Resources["img_face"] as BitmapImage;
                    else if (property.IsEnum)
                        item.SoruceTypeIcon = Application.Current.Resources["tree_Child4"] as BitmapImage;
                    else if (!string.IsNullOrWhiteSpace(property.CustomType))
                        item.SoruceTypeIcon = Application.Current.Resources["img_man"] as BitmapImage;
                    else if (property.CustomWrite)
                        item.SoruceTypeIcon = Application.Current.Resources["tree_item"] as BitmapImage;
                    else if (!string.IsNullOrWhiteSpace(property.ComputeGetCode) || !string.IsNullOrWhiteSpace(property.ComputeSetCode))
                        item.SoruceTypeIcon = Application.Current.Resources["img_code"] as BitmapImage;
                    else if (property.IsCompute)
                        item.SoruceTypeIcon = Application.Current.Resources["img_sum"] as BitmapImage;
                    else
                        item.SoruceTypeIcon = Application.Current.Resources["tree_File"] as BitmapImage;
                    break;
            }
            switch (name)
            {
                case null:
                case nameof(FieldConfig.CustomType):
                case nameof(FieldConfig.ReferenceType):
                    item.Items.Clear();
                    if (property.CustomType == null)
                        break;
                    property.EnumConfig = GlobalConfig.GetEnum(property.CustomType);
                    if (property.EnumConfig == null)
                        break;
                    item.Items.Add(CreateEnumTreeItem(property.EnumConfig));
                    return;
                case nameof(FieldConfig.EnumConfig):
                    item.Items.Clear();
                    if (property.EnumConfig == null)
                        return;
                    item.Items.Add(CreateEnumTreeItem(property.EnumConfig));
                    break;
            }
        }

        private void ModelPropertyChanged(TreeItem item, NotificationObject arg, string name)
        {
            var entity = (ModelConfig)arg;
            switch (name)
            {
                case null:
                case nameof(ModelConfig.NoDataBase):
                    item.SoruceTypeIcon = Application.Current.Resources[entity.NoDataBase ? "tree_Type" : "tree_Child4"] as BitmapImage;
                    break;
            }
        }
        #endregion
        #region 枚举

        private void AddCustomTypeNode(TreeItem node, ProjectConfig project)
        {
            node.Items.Add(new ConfigTreeItem<ProjectConfig>(project)
            {
                IsAssist = true,
                //IsExpanded = true,
                Header = "枚举",
                SoruceView = "enum",
                HeaderField = null,
                CreateChildFunc = CreateEnumTreeItem,
                SoruceItemsExpression = () => project.Enums,
                SoruceTypeIcon = Application.Current.Resources["tree_Folder"] as BitmapImage
            });
        }


        private TreeItem CreateEnumTreeItem(object arg)
        {
            var enumConfig = (EnumConfig)arg;
            return new ConfigTreeItem<EnumConfig>(enumConfig)
            {
                CreateChildFunc = CreateEnumItem,
                SoruceItemsExpression = () => enumConfig.Items,
                SoruceTypeIcon = Application.Current.Resources["tree_Type"] as BitmapImage
            };
        }
        private TreeItem CreateEnumItem(object arg)
        {
            return new ConfigTreeItem<EnumItem>((EnumItem)arg)
            {
                HeaderField = "Name,Value,Caption",
                HeaderExtendExpression = p => $"{p.Name}:{p.Value}〖{p.Caption}〗",
                SoruceTypeIcon = Application.Current.Resources["tree_Child4"] as BitmapImage
            };
        }

        #endregion

        #region 接口

        public TreeItem CreateApiItemTreeItem(object arg)
        {
            var child = (ApiItem)arg;
            var item = new ConfigTreeItem<ApiItem>(child)
            {
                Header = child.Name,
                HeaderField = "Name,Caption",
                HeaderExtendExpression = m => $"{m.Caption}",
                SoruceTypeIcon = Application.Current.Resources["tree_item"] as BitmapImage
            };
            var item2 = new ConfigTreeItem<ApiItem>(child)
            {
                IsAssist = true,
                SoruceView = "argument",
                Header = $"参数{(child.CallArg == null ? null : "-" + child.CallArg)}",
                HeaderField = "CallArg",
                HeaderExtendExpression = m => $"参数{(m.CallArg == null ? null : "-" + m.CallArg)}",

                SoruceTypeIcon = Application.Current.Resources["tree_item"] as BitmapImage
            };
            if (child.Argument != null)
            {
                item2.Items.Add(Model.Tree.CreateEntityTreeItem(child.Argument));
                item2.ModelPropertyChanged += ApiPropertyChangedBy;
            }
            item.Items.Add(item2);
            var item3 = new ConfigTreeItem<ApiItem>(child)
            {
                IsAssist = true,
                SoruceView = "result",
                Header = $"参数{(child.ResultArg == null ? null : "-" + child.ResultArg)}",
                HeaderField = "ResultArg",
                HeaderExtendExpression = m => $"参数{(m.ResultArg == null ? null : "-" + m.ResultArg)}",
                SoruceTypeIcon = Application.Current.Resources["tree_item"] as BitmapImage
            };
            if (child.Result != null)
            {
                item3.Items.Add(Model.Tree.CreateEntityTreeItem(child.Result));
                item3.ModelPropertyChanged += ApiPropertyChangedBy;
            }
            item.Items.Add(item3);
            CheckApiColor(item);
            item.ModelPropertyChanged += TreeModelPropertyChangedByApiItem;
            return item;
        }

        private void ApiPropertyChangedBy(object sender, PropertyChangedEventArgs e)
        {
            var item = (TreeItem)sender;
            var child = (ApiItem)item.Source;
            switch (e.PropertyName)
            {
                case "Argument":
                    item.Items.Clear();
                    if (child.Argument != null)
                        item.Items.Add(Model.Tree.CreateEntityTreeItem(child.Argument));
                    break;
                case "Result":
                    item.Items.Clear();
                    if (child.Result != null)
                        item.Items.Add(Model.Tree.CreateEntityTreeItem(child.Result));
                    break;
            }
        }

        private void TreeModelPropertyChangedByApiItem(object sender, PropertyChangedEventArgs e)
        {
            var item = (TreeItem)sender;
            if (e.PropertyName == "Argument" || e.PropertyName == "Result")
                CheckApiColor(item);
        }

        private static void CheckApiColor(TreeItem item)
        {
            var child = (ApiItem)item.Source;
            if (child.Argument != null)
            {
                item.Color = Brushes.Red;
            }
            else if (child.Result != null)
            {
                item.Color = Brushes.Blue;
            }
            else
            {
                item.Color = Brushes.Black;
            }
        }

        #endregion

        #endregion


        #region 选择

        private void OnTreeSelectItemChanged(object sender, EventArgs e)
        {
            SetSelect(sender as TreeItem);
        }

        /// <summary>
        ///     当前选择
        /// </summary>
        public TreeItem SelectItem => TreeRoot.SelectItem;

        public void SetSelectEntity(EntityConfig entity)
        {
            SetSelect(TreeRoot.Find(entity));
        }

        public void SetSelect(TreeItem item)
        {
            TreeRoot.SelectItem = item;
            Model.Context.OnTreeSelectItemChanged(item);
            Editor.SyncMenu(item);
            RaisePropertyChanged(nameof(SelectItem));
        }

        #endregion

        #region 查找
        /// <summary>
        /// 查找
        /// </summary>
        public CommandItem FindCommand => new CommandItem
        {
            Action = Find,
            IconName = "img_find"
        };

        /// <summary>
        /// 查找
        /// </summary>
        public bool Like(string dest, params string[] srcs) => srcs.Any(src => src != null && src.IndexOf(dest, 0, StringComparison.OrdinalIgnoreCase) >= 0);

        /// <summary>
        /// 查找
        /// </summary>
        public void Find(object arg)
        {
            if (string.IsNullOrWhiteSpace(Context.FindKey))
            {
                TreeRoot.Foreach(p =>
                {
                    if (p.Visibility != Visibility.Visible)
                        p.Visibility = Visibility.Visible;
                    p.IsExpanded = false;
                });
                return;
            }

            var count = 0;
            Find(TreeRoot, ref count);
            Context.StateMessage = $"查找成功-{count}个";
        }

        /// <summary>
        /// 找对应节点
        /// </summary>
        /// <returns></returns>
        public bool Find(TreeItemBase par, ref int count)
        {
            bool hase = false;
            if (par.Source is ConfigBase cfg && Like(Context.FindKey, cfg.Name, cfg.Caption, cfg.Description))
            {
                count++;
                hase = true;
            }
            foreach (var item in par.Items)
            {
                if (Find(item, ref count))
                {
                    hase = true;
                    item.IsExpanded = true;
                }
            }
            par.Visibility = hase ? Visibility.Visible : Visibility.Collapsed;

            return hase;
        }
        #endregion
    }
}