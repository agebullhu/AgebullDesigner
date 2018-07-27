using System;
using System.ComponentModel;
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
            var group = new ClassifyGroupConfig<EntityConfig>(project.Entities, p => p.Classify,
                (name, cfg) => cfg.Classify = name, project.Classifies);

            var eitem = new ConfigTreeItem<ProjectConfig>(project)
            {
                IsAssist = true,
                Header = "实体",
                HeaderField = null,
                CreateChildFunc = CreateEntityClassifiesTreeItem,
                SoruceItemsExpression = () => group.Classifies,
                SoruceTypeIcon = Application.Current.Resources["tree_Folder"] as BitmapImage
            };
            node.Items.Add(eitem);
            //Model.CppModel.AddCppApiNode(node);
            AddCustomTypeNode(node, project);

            var citem = new ConfigTreeItem<ProjectConfig>(project)
            {
                IsAssist = true,
                Header = "API",
                Tag = "API",
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
            var child = (ClassifyItem<EntityConfig>)charg;
            return new ConfigTreeItem<ClassifyItem<EntityConfig>>(child)
            {
                IsAssist = true,
                IsExpanded = true,
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
                col.Parent = entity;
            foreach (var relation in entity.Releations)
                relation.Parent = entity;
            var tableItem = new ConfigTreeItem<EntityConfig>(entity)
            {
                Catalog = "字段",
                CreateChildFunc = CreatePropertyTreeItem,
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
            var property = (PropertyConfig)arg;
            switch (name)
            {
                case null:
                case nameof(PropertyConfig.IsPrimaryKey):
                case nameof(PropertyConfig.IsEnum):
                case nameof(PropertyConfig.IsDiscard):
                case nameof(PropertyConfig.DbInnerField):
                case nameof(PropertyConfig.IsInterfaceField):
                case nameof(PropertyConfig.CustomType):
                case nameof(PropertyConfig.CustomWrite):
                case nameof(PropertyConfig.IsCompute):
                case nameof(PropertyConfig.ComputeGetCode):
                case nameof(PropertyConfig.ComputeSetCode):
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
                case nameof(PropertyConfig.CustomType):
                case nameof(PropertyConfig.ReferenceType):
                    item.Items.Clear();
                    if (property.CustomType == null)
                        return;
                    property.EnumConfig = GlobalConfig.GetEnum(property.CustomType);
                    if (property.EnumConfig != null)
                    {
                        if (name == null)
                            item.Items.Add(CreateEnumTreeItem(property.EnumConfig));
                        return;
                    }
                    //var config = GlobalConfig.GetEntity(property.CustomType);
                    //if (config != null && !LoopCheck(item, config))
                    //    item.Items.Add(CreateEntityTreeItem(config));
                    break;
                case nameof(PropertyConfig.EnumConfig):
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

        #region 枚举

        private void AddCustomTypeNode(TreeItem node, ProjectConfig project)
        {
            node.Items.Add(new ConfigTreeItem<ProjectConfig>(project)
            {
                IsAssist = true,
                //IsExpanded = true,
                Header = "枚举",
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
                HeaderField = "Name,Caption,CallArg",
                HeaderExtendExpression = m => $"{m.Caption}〖{m.Name}({m.CallArg})〗",
                SoruceTypeIcon = Application.Current.Resources["tree_item"] as BitmapImage
            };
            var item2 = new ConfigTreeItem<ApiItem>(child)
            {
                IsAssist = true,
                Header = "参数",
                HeaderField = null,
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
                Header = "返回",
                HeaderField = null,
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
            if (e.PropertyName == "Argument")
            {
                item.Items.Clear();
                if (child.Argument != null)
                    item.Items.Add(Model.Tree.CreateEntityTreeItem(child.Argument));
            }
            if (e.PropertyName == "Result")
            {
                item.Items.Clear();
                if (child.Result != null)
                    item.Items.Add(Model.Tree.CreateEntityTreeItem(child.Result));
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
            Caption = "查找",
            Action = Find
        };

        /// <summary>
        /// 查找
        /// </summary>
        public void Find(object arg)
        {
            if (string.IsNullOrWhiteSpace(Context.FindKey))
                return;
            var item = TreeRoot.Find(p =>
            {
                if (!(p.Source is ConfigBase cfg))
                    return false;
                if (p.Source == Context.SelectConfig)
                    return false;
                if (cfg.Name == Context.FindKey || cfg.Caption == Context.FindKey || cfg.Option.ReferenceTag == Context.FindKey)
                    return true;
                if (p.Source is ApiItem || p.Source is EnumConfig)
                    return null;
                return false;
            }) ?? TreeRoot.Find(p =>
            {
                if (!(p.Source is ConfigBase cfg))
                    return false;
                if (p.Source == Context.SelectConfig)
                    return false;
                if (cfg.Name != null && cfg.Name.Contains(Context.FindKey) ||
                    cfg.Caption != null && cfg.Caption.Contains(Context.FindKey))
                    return true;
                if (p.Source is ApiItem || p.Source is EnumConfig)
                    return null;
                return false;
            });
            if (item == null)
            {
                Context.StateMessage = "查找失败";
                return;
            }
            SetSelect(item);
            item.IsSelected = true;
            item.IsUiSelected = true;
            while (item != null)
            {
                item.IsExpanded = true;
                item = item.Parent as TreeItem;
            }
            Context.StateMessage = "查找成功-" + Context.SelectConfig.Caption;

        }

        #endregion
    }
}