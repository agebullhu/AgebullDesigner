using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agebull.EntityModel.Config.V2021;

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
        #endregion


        #region 构建

        #region 解决方案

        /// <summary>
        /// 
        /// </summary>
        public void CreateTree()
        {
            TreeRoot.SelectItemChanged -= OnTreeSelectItemChanged;
            TreeRoot.ClearItems();

            TreeRoot.Items.Add(new ConfigTreeItem<SolutionConfig>(Context.Solution)
            {
                IsExpanded = true,
                Header = Context.Solution.Caption,
                HeaderField = "Caption,Name",
                HeaderExtendExpression = p => p.Caption ?? p.Name,
                CreateChildrenFunc = CreateProjectTreeItem,
                SoruceItemsExpression = () => Context.Solution.ProjectList,
                SoruceTypeIconName = "解决方案"
            });
            TreeRoot.SelectItemChanged += OnTreeSelectItemChanged;
        }

        #endregion

        #region 项目

        private List<TreeItem> CreateProjectTreeItem(object arg)
        {
            var items = new List<TreeItem>();
            if (arg is ProjectConfig project)
            {
                items.Add(new ConfigTreeItem<ProjectConfig>(project)
                {
                    CreateChildrenFunc = CreateProjectClassifiesTreeItem,
                    IsAssist = true,
                    IsExpanded = false,
                    SoruceTypeIconName = "项目"
                });
            }
            else
            {
                items.Add(new TreeItem(arg));
            }
            return items;
        }

        private List<TreeItem> CreateProjectClassifiesTreeItem(object arg)
        {
            var items = new List<TreeItem>();
            if (arg is ProjectConfig project)
            {
                if (project.NoClassify)
                    items.Add(new ConfigTreeItem<ProjectConfig>(project)
                    {
                        IsAssist = true,
                        Header = "实体",
                        SoruceView = "entity",
                        HeaderField = null,
                        Tag = nameof(EntityConfig),
                        CreateChildFunc = CreateEntityTreeItem,
                        SoruceItemsExpression = () => project.Entities,
                        SoruceTypeIconName = "实体"
                    });
                else
                    items.Add(new ConfigTreeItem<ProjectConfig>(project)
                    {
                        IsAssist = true,
                        Header = "实体",
                        SoruceView = "entity",
                        HeaderField = null,
                        Tag = nameof(EntityConfig),
                        CreateChildFunc = CreateEntityClassifiesTreeItem,
                        SoruceItemsExpression = () => project.Classifies,
                        SoruceTypeIconName = "实体"
                    });

                items.Add(new ConfigTreeItem<ProjectConfig>(project)
                {
                    IsAssist = true,
                    Header = "模型",
                    Tag = nameof(ModelConfig),
                    SoruceView = "model",
                    HeaderField = null,
                    CreateChildFunc = CreateModelTreeItem,
                    SoruceItemsExpression = () => project.Models,
                    SoruceTypeIconName = "模型"
                });
                //Model.CppModel.AddCppApiNode(node);
                items.Add(new ConfigTreeItem<ProjectConfig>(project)
                {
                    IsAssist = true,
                    //IsExpanded = true,
                    Header = "枚举",
                    SoruceView = "enum",
                    HeaderField = null,
                    CreateChildFunc = CreateEnumTreeItem,
                    SoruceItemsExpression = () => project.Enums,
                    SoruceTypeIconName = "枚举"
                });

                //items.Add(new ConfigTreeItem<ProjectConfig>(project)
                //{
                //    IsAssist = true,
                //    Header = "API",
                //    Tag = "API",
                //    SoruceView = "api",
                //    HeaderField = null,
                //    CreateChildFunc = CreateApiItemTreeItem,
                //    SoruceItemsExpression = () => project.ApiItems,
                //    SoruceTypeIcon = Application.Current.Resources["tree_Folder"] as BitmapImage
                //});
            }
            else
            {
                items.Add(new TreeItem(arg));
            }
            return items;
        }
        #endregion

        #region 实体

        private TreeItem CreateEntityClassifiesTreeItem(object charg)
        {
            if (charg is EntityClassify classify)
                return new ConfigTreeItem<EntityClassify>(classify)
                {
                    IsAssist = true,
                    Friend = classify,
                    FriendView = "entity,classify",
                    SoruceView = "entity,classify",
                    IsExpanded = false,
                    Tag = nameof(EntityClassify),
                    CreateChildFunc = CreateEntityTreeItem,
                    FriendItems = classify.Items,
                    SoruceItemsExpression = () => classify.Items,
                    SoruceTypeIconName = "类别"
                };
            if (charg is SimpleConfig config)
                return new TreeItem<SimpleConfig>(config)
                {
                    Header = config.Caption,
                    IsExpanded = false,
                    Tag = nameof(SimpleConfig),
                    SoruceTypeIconName = "类别"
                };
            return null;
        }

        internal TreeItem CreateEntityTreeItem(object arg)
        {
            if (!(arg is EntityConfig entity))
                return new TreeItem(arg);

            foreach (var col in entity.Properties)
                col.Entity = entity;
            return new ConfigTreeItem<EntityConfig>(entity)
            {
                Tag = nameof(EntityConfig),
                
                SoruceView = "entity",
                CreateChildFunc = CreateFieldTreeItem,
                SoruceTypeIconName = "实体",
                SoruceItemsExpression = () => entity.Properties,
                CustomPropertyChanged = Entity_PropertyChanged
            };
        }
        BitmapImage EntityIcon(EntityConfig entity) =>
           Application.Current.Resources[entity.IsLinkTable
                ? "img_switch"
                : entity.EnableDataBase ? "tree_Child4" : "tree_Type"] as BitmapImage;

        private void Entity_PropertyChanged(TreeItem item, NotificationObject arg, string name)
        {
            switch (name)
            {
                case null:
                case nameof(EntityConfig.IsLinkTable):
                case nameof(EntityConfig.EnableDataBase):
                    item.SoruceTypeIconName = "数据库";
                    break;
            }
        }


        private TreeItem CreateFieldTreeItem(object arg)
        {
            if (!(arg is FieldConfig field))
                return new TreeItem(arg);

            return new ConfigTreeItem<FieldConfig>(field)
            {
                Tag = nameof(EntityConfig),
                SoruceView = "entity,field",
                Color = field.IsSystemField ? Brushes.Blue : Brushes.Black,
                FontWeight = field.IsCompute ? FontWeights.Thin : FontWeights.DemiBold,
                SoruceTypeIconName = "普通列",
                CustomPropertyChanged = Property_PropertyChanged
            };
        }

        private bool LoopCheck(TreeItemBase item, object source)
        {
            if (item == null)
                return false;
            if (item.Source != null && item.Source == source)
                return true;
            return LoopCheck(item.Parent, source);
        }


        #endregion

        #region 模型

        internal TreeItem CreateModelTreeItem(object arg)
        {
            if (!(arg is ModelConfig entity))
                return new TreeItem(arg);

            foreach (var col in entity.Properties)
                col.Model = entity;
            var tableItem = new ConfigTreeItem<ModelConfig>(entity)
            {
                Tag = nameof(ModelConfig),
                FriendView = "model",
                CreateChildFunc = CreatePropertyTreeItem,
                SoruceItemsExpression = () => entity.Properties,
                //CustomPropertyChanged = ModelPropertyChanged,
                SoruceTypeIconName = "模型"
            };
            return tableItem;
        }
        private TreeItem CreateCommandTreeItem(object arg)
        {
            if (!(arg is UserCommandConfig cmd))
                return new TreeItem(arg);

            var colItem = new ConfigTreeItem<UserCommandConfig>(cmd)
            {
                IsExpanded = true,
                FriendView = "model,command",
                HeaderField = "Caption",
                HeaderExtendExpression = p => p.Caption,
                SoruceTypeIconName = "命令"
            };
            return colItem;
        }


        private TreeItem CreatePropertyTreeItem(object arg)
        {
            if (!(arg is PropertyConfig property))
                return new TreeItem(arg);

            return new ConfigTreeItem<PropertyConfig>(property)
            {
                Tag = nameof(ModelConfig),
                SoruceTypeIconName = "普通列",
                FriendView = "model,property",
                Color = property.IsSystemField ? Brushes.Blue : Brushes.Black,
                FontWeight = property.IsCompute ? FontWeights.Thin : FontWeights.DemiBold,
                CustomPropertyChanged = Property_PropertyChanged
            };
        }

        private void Property_PropertyChanged(TreeItem item, NotificationObject arg, string name)
        {
            var property = (IPropertyConfig)arg ;
            switch (name)
            {
                case null:
                case nameof(FieldConfig.IsPrimaryKey):
                case nameof(FieldConfig.CsType):
                case nameof(FieldConfig.DataType):
                case nameof(FieldConfig.IsImage):
                case nameof(FieldConfig.IsTime):
                case nameof(FieldConfig.IsEnum):
                case nameof(FieldConfig.IsInterfaceField):
                case nameof(FieldConfig.CustomType):
                case nameof(FieldConfig.IsCompute):
                case nameof(FieldConfig.ComputeGetCode):
                case nameof(FieldConfig.ComputeSetCode):
                    item.SoruceTypeIconName = FieldIcon(property);
                    break;
            }
            switch (name)
            {
                case null:
                case nameof(FieldConfig.CustomType):
                case nameof(FieldConfig.ReferenceType):
                    item.ClearItems();
                    if (property.CustomType == null)
                        break;
                    property.EnumConfig = GlobalConfig.GetEnum(property.CustomType);
                    if (property.EnumConfig == null)
                        break;
                    item.Items.Add(CreateEnumTreeItem(property.EnumConfig));
                    return;
                case nameof(FieldConfig.EnumConfig):
                    item.ClearItems();
                    if (property.EnumConfig == null)
                        return;
                    item.Items.Add(CreateEnumTreeItem(property.EnumConfig));
                    break;
            }
        }
        string FieldIcon(IPropertyConfig property)
        {
            if (property.IsPrimaryKey)
                return "主键列";
            if (property.DataBaseField.IsLinkKey)
                return "外键列";
            if (property.DataBaseField.IsLinkField)
                return "链接列"; 
            if (property.IsInterfaceField)
                return "接口列";
            if (property.DataBaseField.IsText || property.DataBaseField.IsBlob)
                return "备注列";
            if (property.IsEnum || property.CustomType.IsPresent())
                return "枚举列";
            if (property.IsCompute || (property.ComputeGetCode.IsPresent() || property.ComputeSetCode.IsPresent()))
                return "计算列";
            if (property.IsTime || property.DataType.IsMe(nameof(DateTime)))
                return "时间列";
            if (property.DataType.Contains("Int") || property.DataType.IsMe(nameof(Decimal)) || property.DataType.IsMe(nameof(Double)) || property.DataType.IsMe(nameof(Single)))
                return "数字列";
            if (property.IsImage)
                return "图像列";
            else
                return  "普通列";
        }
        /*
        private void ModelPropertyChanged(TreeItem item, NotificationObject arg, string name)
        {
            var entity = (ModelConfig)arg;
            switch (name)
            {
                case null:
                case nameof(ModelConfig.EnableDataBase):
                    item.SoruceTypeIconName = Application.Current.Resources[entity.EnableDataBase ? "tree_Child4" : "tree_Type"] as BitmapImage;
                    break;
            }
        }*/


        #endregion

        #region 枚举

        private TreeItem CreateEnumTreeItem(object arg)
        {
            if (!(arg is EnumConfig enumConfig))
                return new TreeItem(arg);

            return new ConfigTreeItem<EnumConfig>(enumConfig)
            {
                CreateChildFunc = CreateEnumItem,
                FriendView = "enum,field",
                SoruceItemsExpression = () => enumConfig.Items,
                SoruceTypeIconName = "枚举"
            };
        }
        private TreeItem CreateEnumItem(object arg)
        {
            if (!(arg is EnumItem enumConfig))
                return new TreeItem(arg);

            return new ConfigTreeItem<EnumItem>(enumConfig)
            {
                HeaderField = "Name,Value,Caption",
                HeaderExtendExpression = p => $"{p.Name}:{p.Value}〖{p.Caption}〗",
                SoruceTypeIconName = "字段"
            };
        }

        #endregion

        #region 接口

        public TreeItem CreateApiItemTreeItem(object arg)
        {
            if (!(arg is ApiItem child))
                return new TreeItem(arg);

            var item = new ConfigTreeItem<ApiItem>(child)
            {
                Header = child.Name,
                FriendView = "api",
                HeaderField = "Name,Caption",
                HeaderExtendExpression = m => $"{m.Caption}",
                SoruceTypeIconName = "字段"
            };
            var item2 = new ConfigTreeItem<ApiItem>(child)
            {
                IsAssist = true,
                SoruceView = "api,argument",
                Header = $"参数{(child.CallArg == null ? null : "-" + child.CallArg)}",
                HeaderField = "CallArg",
                HeaderExtendExpression = m => $"参数{(m.CallArg == null ? null : "-" + m.CallArg)}",
                SoruceTypeIconName = "字段"
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
                SoruceView = "api,result",
                Header = $"参数{(child.ResultArg == null ? null : "-" + child.ResultArg)}",
                HeaderField = "ResultArg",
                HeaderExtendExpression = m => $"参数{(m.ResultArg == null ? null : "-" + m.ResultArg)}",
                SoruceTypeIconName = "字段"
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
                    item.ClearItems();
                    if (child.Argument != null)
                        item.Items.Add(Model.Tree.CreateEntityTreeItem(child.Argument));
                    break;
                case "Result":
                    item.ClearItems();
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
        public CommandItemBase FindCommand => new CommandItem
        {
            Action = Find,
            IconName = "查找"
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
            Task.Factory.StartNew(() =>
            {
                if (string.IsNullOrWhiteSpace(Context.FindKey))
                {
                    TreeRoot.Foreach(p =>
                    {
                        p.Visibility = Visibility.Visible;
                        p.IsExpanded = false;
                    }, false);
                }
                else
                {
                    var count = 0;
                    Find(TreeRoot, ref count);
                    Context.StateMessage = $"查找成功-{count}个";
                }
            });
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
                item.IsExpanded = true;
                if (Find(item, ref count))
                {
                    hase = true;
                    item.Visibility = Visibility.Visible;
                }
                else
                {
                    item.IsExpanded = false;
                    item.Visibility = Visibility.Collapsed;
                }
            }

            return hase;
        }
        #endregion
    }
}