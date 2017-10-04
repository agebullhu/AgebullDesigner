using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Agebull.Common.DataModel;
using Gboxt.Common.DataAccess.Schemas;
using Gboxt.Common.WpfMvvmBase.Commands;

namespace Agebull.CodeRefactor.SolutionManager
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
            TreeRoot.SelectItemChanged += OnTreeSelectItemChanged;
        }

        /// <summary>
        /// 
        /// </summary>
        public void CreateTree()
        {
            TreeRoot.Items.Clear();
            var node = new ConfigTreeItem<SolutionConfig>(Context.Solution)
            {
                IsExpanded = true,
                HeaderField = "Caption",
                HeaderExtendExpression = p => p.Caption,
                SoruceTypeIcon = Application.Current.Resources["tree_Solution"] as BitmapImage
            };
            AddProjectNode(node);
            //Model.CppModel.AddCppApiNode(node);
            AddCustomTypeNode(node);
            TreeRoot.Items.Add(node);
            SelectItem = TreeRoot.Items[0];
        }

        #endregion

        #region 选择

        private void OnTreeSelectItemChanged(object sender, EventArgs e)
        {
            var item = sender as TreeItem;
            if (item != null)
                Model.Context.CurrentTrace.TraceMessage = item.Extend.DependencyObjects.AutoDependency<TraceMessage>();
            SelectItem = item;
            Model.Context.SetSelectItem(item?.Source as ConfigBase, item?.Header);
        }

        /// <summary>
        ///     当前选择
        /// </summary>
        public TreeItem SelectItem
        {
            get { return TreeRoot.SelectItem; }
            set
            {
                if (TreeRoot.SelectItem != value)
                    TreeRoot.SelectItem = value;
                RaisePropertyChanged(nameof(SelectItem));
            }
        }

        public void SetSelect(EntityConfig entity)
        {
            SelectItem = TreeRoot.Find(entity);
        }

        #endregion

        #region 查找
        /// <summary>
        /// 查找
        /// </summary>
        public CommandItem FindCommand => new CommandItem
        {
            Name = "查找",
            Command = new DelegateCommand(Find),
            Image = Application.Current.Resources["ImgSubmit"] as ImageSource
        };

        /// <summary>
        /// 查找
        /// </summary>
        public void Find()
        {
            if (string.IsNullOrWhiteSpace(Context.FindKey))
                return;
            var item = TreeRoot.Find(p =>
            {
                var cfg = p.Source as ConfigBase;
                if (cfg == null)
                    return false;
                if (p.Source == Context.SelectConfig)
                    return false;
                if (cfg.Name == Context.FindKey || cfg.Caption == Context.FindKey || cfg.Tag == Context.FindKey)
                    return true;
                if (p.Source is ApiItem || p.Source is NotifyItem || p.Source is TypedefItem || p.Source is EnumConfig)
                    return null;
                return false;
            }) ?? TreeRoot.Find(p =>
            {
                var cfg = p.Source as ConfigBase;
                if (cfg == null)
                    return false;
                if (p.Source == Context.SelectConfig)
                    return false;
                if (cfg.Name != null && cfg.Name.Contains(Context.FindKey) ||
                    cfg.Caption != null && cfg.Caption.Contains(Context.FindKey))
                    return true;
                if (p.Source is ApiItem || p.Source is NotifyItem || p.Source is TypedefItem || p.Source is EnumConfig)
                    return null;
                return false;
            });
            if (item == null)
            {
                Context.StateMessage = "查找失败";
                return;
            }
            SelectItem = item;
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

        #region 实体与字段

        internal TreeItem CreateEntityTreeItem(object arg)
        {
            var entity = (EntityConfig)arg;
            foreach (var col in entity.Properties)
                col.Parent = entity;
            foreach (var relation in entity.Releations)
                relation.Parent = entity;
            var icon = Application.Current.Resources[entity.IsClass ? "tree_Type" : "tree_Child4"] as BitmapImage;
            var tableItem = new ConfigTreeItem<EntityConfig>(entity)
            {
                SoruceTypeIcon = icon
            };
            var propertiesItem = new ConfigTreeItem<EntityConfig>(entity)
            {
                IsAssist = true,
                IsExpanded = true,
                Header = "字段",
                Catalog = "字段",
                HeaderField = null,
                CreateChildFunc = CreatePropertyTreeItem,
                SoruceItemsExpression = () => entity.Properties,
                SoruceTypeIcon = Application.Current.Resources["tree_Folder"] as BitmapImage
            };
            tableItem.Items.Add(propertiesItem);
            var cmdItem = new ConfigTreeItem<EntityConfig>(entity)
            {
                IsAssist = true,
                IsExpanded = true,
                Header = "命令",
                Catalog = "命令",
                HeaderField = null,
                CreateChildFunc = CreateCommandTreeItem,
                SoruceItemsExpression = () => entity.Commands,
                SoruceTypeIcon = Application.Current.Resources["tree_Folder"] as BitmapImage
            };
            tableItem.Items.Add(cmdItem);
            //var releations = new ConfigTreeItem<TableSchema>(entity)
            //{
            //    //Header = "关联",
            //    CreateChildFunc = CreateReleationItem,
            //    SoruceItemsExpression = () => entity.Releations,
            //    SoruceTypeIcon = Application.Current.Resources["tree_Folder"] as BitmapImage
            //};
            //tableItem.Items.Add(releations);
            return tableItem;
        }


        private TreeItem CreatePropertyTreeItem(object arg)
        {
            var property = (PropertyConfig)arg;
            BitmapImage icon;
            if (property.IsPrimaryKey)
                icon = Application.Current.Resources["tree_default"] as BitmapImage;
            else if (property.Discard)
                icon = Application.Current.Resources["img_clear"] as BitmapImage;
            else if (property.DbInnerField)
                icon = Application.Current.Resources["img_lock"] as BitmapImage;
            else if (property.IsInterfaceField)
                icon = Application.Current.Resources["img_face"] as BitmapImage;
            else if (!string.IsNullOrWhiteSpace(property.CustomType))
                icon = Application.Current.Resources["img_man"] as BitmapImage;
            else if (property.CustomWrite)
                icon = Application.Current.Resources["tree_item"] as BitmapImage;
            else if (!string.IsNullOrWhiteSpace(property.ComputeGetCode) ||
                     !string.IsNullOrWhiteSpace(property.ComputeSetCode))
                icon = Application.Current.Resources["img_code"] as BitmapImage;
            else if (property.IsCompute)
                icon = Application.Current.Resources["img_sum"] as BitmapImage;
            else
                icon = Application.Current.Resources["tree_File"] as BitmapImage;

            var colItem = new ConfigTreeItem<PropertyConfig>(property)
            {
                //IsExpanded = true,
                Color = property.IsSystemField ? Brushes.Blue : Brushes.Black,
                FontWeight = property.IsCompute ? FontWeights.Thin : FontWeights.DemiBold,
                SoruceTypeIcon = icon
            };
            //if (property.FieldType != null)
            //{
            //    colItem.Items.Add(new ConfigTreeItem<ColumnType>(property.FieldType)
            //    {
            //        //Header = "存储设置",
            //        SoruceTypeIcon = Application.Current.Resources["tree_default"] as BitmapImage
            //    });
            //}
            //if (property.PropertyType != null)
            //{
            //    colItem.Items.Add(new ConfigTreeItem<ColumnType>(property.PropertyType)
            //    {
            //        //Header = "代码设置",
            //        SoruceTypeIcon = Application.Current.Resources["tree_default"] as BitmapImage
            //    });
            //}
            //colItem.Commands.Add(new CommandItem
            //{
            //    Command = new DelegateCommand<PropertyConfig>(AddRelation, this, CanAddRelation),
            //    Name = "新增关联",
            //    Parameter = property,
            //    Image = Application.Current.Resources["tree_Open"] as ImageSource
            //});
            //PropertyChildTreeItem(colItem, property);

            return colItem;
        }


        public void PropertyChildTreeItem(TreeItem colItem, PropertyConfig property)
        {
            //if (property.CppLastType != null)
            //{
            //    var type = CppTypeHelper.ToCppLastType(property.CppLastType) as TypedefItem;
            //    if (type != null && type.Items.Count > 0)
            //        colItem.Items.Add(Model.CppModel.CreateTypedefTreeItem(type));
            //}
            if (property.EnumConfig == null)
                return;
            {
                colItem.Items.Add(CreateEnumTreeItem(property.EnumConfig));
                //var type = GlobalConfig.GetTypedefByTag(property.EnumConfig.Tag);
                //if (type != null)
                //    colItem.Items.Add(Model.CppModel.CreateTypedefTreeItem(type));
            }
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

        //private TreeItem CreateReleationItem(object relation)
        //{
        //    return CreateReleationItem((TableReleation)relation);
        //}

        //private ConfigTreeItem<TableReleation> CreateReleationItem(TableReleation relation)
        //{
        //    var colItem = new ConfigTreeItem<TableReleation>(relation)
        //    {
        //        //Header = relation.Friend,
        //        HeaderFieldExpression = p => p.Caption,
        //        SoruceTypeIcon = Application.Current.Resources["tree_Child2"] as BitmapImage
        //    };
        //    colItem.Commands.Add(new CommandItem
        //    {
        //        Command = new DelegateCommand<TableReleation>(p => p.Parent.Releations.Remove(p)),
        //        Parameter = relation,
        //        Name = "删除",
        //        Image = Application.Current.Resources["img_del"] as ImageSource
        //    });
        //    return colItem;
        //}

        #endregion

        #region 类型与枚举

        private void AddCustomTypeNode(ConfigTreeItem<SolutionConfig> node)
        {
            var item = new ConfigTreeItem<SolutionConfig>(Context.Solution)
            {
                IsAssist = true,
                IsExpanded = true,
                Header = "类型",
                HeaderField = null,
                SoruceTypeIcon = Application.Current.Resources["tree_Folder"] as BitmapImage
            };
            node.Items.Add(item);
            var mitem = new ConfigTreeItem<SolutionConfig>(Context.Solution)
            {
                IsAssist = true,
                //IsExpanded = true,
                Header = "枚举",
                HeaderField = null,
                CreateChildFunc = CreateEnumTreeItem,
                SoruceItemsExpression = () => Context.Solution.Enums,
                SoruceTypeIcon = Application.Current.Resources["tree_Folder"] as BitmapImage
            };
            item.Items.Add(mitem);

            //Model.CppModel.AddTypedefItem(item);
        }


        private TreeItem CreateEnumTreeItem(object arg)
        {
            var enumConfig = (EnumConfig)arg;
            var node = new ConfigTreeItem<EnumConfig>(enumConfig)
            {
                IsExpanded = true,
                SoruceTypeIcon = Application.Current.Resources["tree_Type"] as BitmapImage
            };
            var icon = Application.Current.Resources["tree_Child4"] as BitmapImage;
            if (enumConfig.Items != null && enumConfig.Items.Count > 0)
                foreach (var item in enumConfig.Items)
                    node.Items.Add(new ConfigTreeItem<EnumItem>(item)
                    {
                        HeaderField = "Name,Value,Caption",
                        HeaderExtendExpression = p => $"{p.Name}:{p.Value}〖{p.Caption}〗",
                        SoruceTypeIcon = icon
                    });
            return node;
        }

        #endregion

        #region 项目

        private void AddProjectNode(ConfigTreeItem<SolutionConfig> node)
        {
            var item = new ConfigTreeItem<SolutionConfig>(Context.Solution)
            {
                IsAssist = true,
                IsExpanded = true,
                Header = "项目",
                HeaderField = null,
                CreateChildFunc = CreateProjectTreeItem,
                SoruceItemsExpression = () => Context.Solution.Projects,
                SoruceTypeIcon = Application.Current.Resources["tree_Folder"] as BitmapImage
            };
            node.Items.Add(item);
        }

        private TreeItem CreateEntityClassifiesTreeItem(object charg)
        {
            var child = (ClassifyItem<EntityConfig>)charg;
            return new ConfigTreeItem<ClassifyItem<EntityConfig>>(child)
            {
                IsAssist = true,
                IsExpanded = true,
                CreateChildFunc = CreateEntityTreeItem,
                FriendItems = child.Items,
                SoruceItemsExpression = () => child.Items,
                SoruceTypeIcon = Application.Current.Resources["tree_Folder"] as BitmapImage
            };
        }

        private TreeItem CreateProjectTreeItem(object arg)
        {
            var project = (ProjectConfig)arg;
            TreeItem item;
            if (project.Entities.Count > 15)
            {
                var group = new ClassifyGroupConfig<EntityConfig>(project.Entities, p => p.Classify,
                    (name, cfg) => cfg.Classify = name, project.Classifies);

                if (SolutionConfig.Current.SolutionType == SolutionType.Cpp)
                {
                    item = new ConfigTreeItem<ProjectConfig>(project)
                    {
                        IsExpanded = !project.ReadOnly,
                        SoruceTypeIcon = Application.Current.Resources["tree_Folder"] as BitmapImage
                    };
                    item.Items.Add(new ConfigTreeItem<ProjectConfig>(project)
                    {
                        IsAssist = true,
                        IsExpanded = true,
                        Header = "实体",
                        HeaderField = null,
                        CreateChildFunc = CreateEntityClassifiesTreeItem,
                        SoruceItemsExpression = () => group.Classifies,
                        SoruceTypeIcon = Application.Current.Resources["tree_Folder"] as BitmapImage
                    });
                }
                else
                {
                    item = new ConfigTreeItem<ProjectConfig>(project)
                    {
                        IsAssist = true,
                        IsExpanded = true,
                        CreateChildFunc = CreateEntityClassifiesTreeItem,
                        SoruceItemsExpression = () => group.Classifies,
                        SoruceTypeIcon = Application.Current.Resources["tree_Folder"] as BitmapImage
                    };
                }
            }
            else
            {
                item = new ConfigTreeItem<ProjectConfig>(project)
                {
                    CreateChildFunc = CreateEntityTreeItem,
                    SoruceItemsExpression = () => project.Entities,
                    SoruceTypeIcon = Application.Current.Resources["tree_Folder"] as BitmapImage
                };
            }
            //if (SolutionConfig.Current.SolutionType != SolutionType.Cpp)
            //    return item;
            //var citem = new ConfigTreeItem<ProjectConfig>(project)
            //{
            //    IsAssist = true,
            //    Header = "API",
            //    HeaderField = null,
            //    CreateChildFunc = Model.CppModel.CreateApiItemTreeItem,
            //    SoruceItemsExpression = () => project.ApiItems,
            //    SoruceTypeIcon = Application.Current.Resources["tree_Folder"] as BitmapImage
            //};
            //Model.CppModel.AddProjectTypedefCommand(citem, project);
            //item.Items.Add(citem);
            return item;
        }

        #endregion

    }
}