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
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using Agebull.Common;
using Agebull.Common.Config.Upgrader.View;

using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.RobotCoder.Upgrade;
using Application = System.Windows.Application;

#endregion

namespace Agebull.EntityModel.Designer
{
    public sealed class UpgradeModel : ModelBase
    {
        #region 初始化

        /// <summary>
        ///     初始化
        /// </summary>
        protected override void DoInitialize()
        {
            CommandCoefficient.RegisterCommand<ClassUpgradeConfig>(new CommandItemBuilder
            {
                Command = new DelegateCommand<ClassUpgradeConfig>(AddProperty),
                Name = "增加字段",
                IconName = "img_add"
            });
            base.DoInitialize();
            Load();
            CreateTree();
            TreeRoot.SelectItemChanged += OnTreeSelectItemChanged;
        }

        #endregion

        #region 分析
        /// <summary>
        /// 分析
        /// </summary>
        public void Analyze()
        {
            var up = new AssemblyUpgrader();
            var types = up.GetConfig();
            RootConfig.ConfigTypes.Clear();
            RootConfig.ConfigTypes.AddRange(types.Values);
        }

        /// <summary>
        /// 升级配置类
        /// </summary>
        /// <returns></returns>
        public void UpgradeCode()
        {
            foreach (var type in RootConfig.ConfigTypes)
            {
                var builder = new UpgradeEntityBuilder
                {
                    Config = type
                };
                builder.CreateBaseCode(@"C:\Projects\AgebullDesigner\Config\Upgrade");
            }
        }

        /// <summary>
        /// 升级配置类
        /// </summary>
        /// <returns></returns>
        public void Save()
        {
            var path = IOHelper.CheckPath(@"C:\Projects\AgebullDesigner", "UpgradeDocument");
            if (RootConfig.ConfigTypes.Count == 0)
            {
                MessageBox.Show(@"点错了吧");
                return;
            }
            foreach (var type in RootConfig.ConfigTypes)
            {
                var file = Path.Combine(path, type.Name + ".cfg");
                ConfigWriter.Serializer(file,type);
            }
        }

        /// <summary>
        /// 升级配置类
        /// </summary>
        /// <returns></returns>
        public void Load()
        {
            var path = IOHelper.CheckPath(@"C:\Projects\AgebullDesigner", "UpgradeDocument");
            var files = IOHelper.GetAllFiles(path, "cfg");
            RootConfig.ConfigTypes.Clear();
            foreach (var file in files)
            {
                RootConfig.ConfigTypes.Add(ConfigLoader.DeSerializer<ClassUpgradeConfig>(file));
            }
        }
        /// <summary>
        /// 增加属性
        /// </summary>
        /// <returns></returns>
        public void AddProperty(ClassUpgradeConfig config)
        {
            var window = new NewConfigWindow();
            window.ShowDialog();
            var property = (PropertyUpgradeConfig)window.DataContext ;
            if (property.Name == null)
                return;
            property.Classify = property.Category;
            config.Properties.Add(property.Name, property);
            var cl = config.ClassifyGroup.Classifies.FirstOrDefault(p => p.Classify == property.Category);
            if (cl == null)
            {
                config.ClassifyGroup.Classifies.Add(cl = new ClassifyItem<PropertyUpgradeConfig>
                {
                    Classify = property.Classify,
                    Caption = property.Classify,
                    Name=property.Category
                });
            }
            cl.Items.Add(property);
        }
        
        #endregion

        #region 树

        /// <summary>
        ///     当前配置树
        /// </summary>
        public TreeRoot TreeRoot { get; } = new TreeRoot();

        /// <summary>
        /// 根配置
        /// </summary>
        public UpgradeRoot RootConfig { get; } = new UpgradeRoot();

        /// <summary>
        ///     当前配置
        /// </summary>
        public ConfigBase SelectConfig
        {
            get { return GlobalConfig.CurrentConfig; }
            set
            {
                if (GlobalConfig.CurrentConfig == value)
                {
                    return;
                }
                GlobalConfig.CurrentConfig = value;
                RaisePropertyChanged(() => SelectConfig);
            }
        }
        public void CreateTree()
        {
            TreeRoot.Items.Clear();
            var node = new TreeItem<UpgradeRoot>(RootConfig)
            {
                IsExpanded = true,
                Header = "升级对象",
                CreateChildFunc = CreateClassItem,
                SoruceItemsExpression = () => RootConfig.ConfigTypes,
                SoruceTypeIcon = Application.Current.Resources["tree_Solution"] as BitmapImage
            };
            TreeRoot.Items.Add(node);
            SelectItem = node;
        }
        private void OnTreeSelectItemChanged(object sender, EventArgs e)
        {
            var item = sender as TreeItem;
            SelectItem = item;
            SelectConfig = item?.Source as ConfigBase;
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

        private TreeItem CreateClassItem(object arg)
        {
            var config = (ClassUpgradeConfig) arg;

            var configItem = new ConfigTreeItem<ClassUpgradeConfig>(config)
            {
                SoruceTypeIcon = Application.Current.Resources["tree_Type"] as BitmapImage
            };

            var fields = config.Fields.Values.ToDictionary(p => p.Name);
            var properties = new ConfigCollection<PropertyUpgradeConfig>();
            properties.AddRange(config.Properties.Values);
            config.ClassifyGroup = new ClassifyGroupConfig<PropertyUpgradeConfig>(properties, p => p.Category,
                (c, cfg) => cfg.Category = c);
            var propertiesItem = new ConfigTreeItem<ClassUpgradeConfig>(config)
            {
                IsAssist = true,
                IsExpanded = true,
                Header = "普通属性",
                HeaderField = null,
                CreateChildFunc = CreateClassifyNode,
                SoruceItemsExpression = () => config.ClassifyGroup.Classifies,
                SoruceTypeIcon = Application.Current.Resources["tree_Folder"] as BitmapImage
            };
            foreach (var pro in config.Properties.Values)
            {
                if (string.IsNullOrWhiteSpace(pro.PairField)|| !fields.ContainsKey(pro.PairField))
                    continue;
                pro.Field = fields[pro.PairField];
                fields.Remove(pro.PairField);
                pro.IsJsonAttribute = pro.Field.IsJsonAttribute;
                pro.IsDataAttribute = pro.Field.IsDataAttribute;
            }
            configItem.Items.Add(propertiesItem);
            var fieldItem = new ConfigTreeItem<ClassUpgradeConfig>(config)
            {
                IsAssist = true,
                IsExpanded = true,
                Header = "落单字段",
                HeaderField = null,
                SoruceTypeIcon = Application.Current.Resources["tree_Folder"] as BitmapImage
            };
            foreach (var field in fields.Values)
            {
                fieldItem.Items.Add(new ConfigTreeItem<FieldUpgradeConfig>(field)
                {
                    IsAssist = true,
                    IsExpanded = true,
                    Header = $"{field.Caption}〖{field.Name}〗",
                    HeaderField = "Caption,Name",
                    HeaderExtendExpression = m => $"{m.Caption}〖{m.Name}〗",
                    SoruceTypeIcon = Application.Current.Resources["tree_File"] as BitmapImage
                });
            }
            if (fieldItem.Items.Count > 0)
                configItem.Items.Add(fieldItem);
            return configItem;
        }

        private TreeItem CreateClassifyNode(object arg)
        {
            var child = (ClassifyItem<PropertyUpgradeConfig>)arg;
            return new ConfigTreeItem<ClassifyItem<PropertyUpgradeConfig>>(child)
            {
                IsExpanded = true,
                Header = child.Name,
                HeaderField = "Name",
                HeaderExtendExpression = p => p.Name,
                CreateChildFunc = CreateClassifyItemNode,
                FriendItems = child.Items,
                SoruceTypeIcon = Application.Current.Resources["tree_Folder"] as BitmapImage
            };
        }
        private TreeItem CreateClassifyItemNode(object arg)
        {
            var pro = (PropertyUpgradeConfig)arg;
            var node = new ConfigTreeItem<PropertyUpgradeConfig>(pro)
            {
                IsAssist = true,
                IsExpanded = true,
                Header = $"{pro.Caption}〖{pro.Name}〗",
                HeaderField = "Caption,Name",
                HeaderExtendExpression = m => $"{m.Caption}〖{m.Name}〗",
                SoruceTypeIcon = Application.Current.Resources["tree_item"] as BitmapImage
            };
            if (pro.Field != null)
            {
                node.Items.Add(new ConfigTreeItem<FieldUpgradeConfig>(pro.Field)
                {
                    IsAssist = true,
                    IsExpanded = true,
                    Header = $"{pro.Field.Caption}〖{pro.Field.Name}〗",
                    HeaderField = "Caption,Name",
                    HeaderExtendExpression = m => $"{m.Caption}〖{m.Name}〗",
                    SoruceTypeIcon = Application.Current.Resources["tree_File"] as BitmapImage
                });
            }
            return node;
        }
        #endregion


    }
}