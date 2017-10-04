/*/ /****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 配置:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// 建立:2014-11-20
// 修改:2014-11-29
// ***************************************************** /

#region 引用

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Agebull.EntityModel;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;
using Agebull.EntityModel.Designer;
using Agebull.EntityModel.Designer;

#endregion

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// Cpp相关的模型
    /// </summary>
    public class CppModel : DesignModelBase
    {
        protected override void CreateCommands(List<CommandItem> commands)
        {
            commands.Add(new CommandItem
            {
                NoButton = true,
                Command = new DelegateCommand(RepairTag),
                Name = "修复标签",
                Image = Application.Current.Resources["tree_item"] as ImageSource
            });
        }

        #region 树形支持

        private ClassifyGroupConfig<TypedefItem> _typeofClassifyGroup;


        internal void AddTypedefItem(ConfigTreeItem<SolutionConfig> item)
        {
            if (Context.Solution.SolutionType == SolutionType.Cpp)
            {
                _typeofClassifyGroup =
                    new ClassifyGroupConfig<TypedefItem>(Context.Solution.TypedefItems, p => p.Tag,
                        (name, cfg) => cfg.Tag = name);
                item.Items.Add(new ConfigTreeItem<SolutionConfig>(Context.Solution)
                {
                    IsAssist = true,
                    Header = "第三方类型",
                    HeaderField = null,
                    CreateChildFunc = CreateTypeofClassifyGroupItem,
                    SoruceItemsExpression = () => _typeofClassifyGroup.Classifies,
                    SoruceTypeIcon = Application.Current.Resources["tree_Folder"] as BitmapImage
                });
            }
        }
        private TreeItem CreateTypeofClassifyGroupItem(object arg)
        {
            var child = (ClassifyItem<TypedefItem>)arg;
            return new ConfigTreeItem<ClassifyItem<TypedefItem>>(child)
            {
                IsExpanded = true,
                Header = child.Name,
                HeaderField = "Name",
                HeaderExtendExpression = p => p.Name,
                CreateChildFunc = CreateTypedefTreeItem,
                FriendItems = child.Items,
                SoruceTypeIcon = Application.Current.Resources["tree_Folder"] as BitmapImage
            };
        }

        internal TreeItem CreateTypedefTreeItem(object arg)
        {
            var typedef = (TypedefItem)arg;
            var node = new ConfigTreeItem<TypedefItem>(typedef)
            {
                IsExpanded = true,
                SoruceTypeIcon = Application.Current.Resources["tree_Type"] as BitmapImage
            };
            var icon = Application.Current.Resources["tree_Child4"] as BitmapImage;

            if (typedef.Items != null && typedef.Items.Count > 0)
                foreach (var item in typedef.Items.Values)
                    node.Items.Add(new ConfigTreeItem<EnumItem>(item)
                    {
                        HeaderField = "Name,Value,Caption",
                        HeaderExtendExpression = p => $"{p.Name}:{p.Value}〖{p.Caption}〗",
                        SoruceTypeIcon = icon
                    });
            return node;
        }

        internal void AddCppApiNode(ConfigTreeItem<SolutionConfig> node)
        {
            if (Context.Solution.SolutionType != SolutionType.Cpp)
                return;
            var item = new ConfigTreeItem<SolutionConfig>(Context.Solution)
            {
                IsAssist = true,
                IsExpanded = true,
                Header = "接口",
                HeaderField = null,
                SoruceTypeIcon = Application.Current.Resources["tree_Folder"] as BitmapImage
            };
            node.Items.Add(item);
            var group1 = new ClassifyGroupConfig<ApiItem>(Context.Solution.ApiItems, p => p.Classify,
                (name, cfg) => cfg.Classify = name);
            var aitem = new ConfigTreeItem<SolutionConfig>(Context.Solution)
            {
                IsAssist = true,
                Header = "API接口",
                HeaderField = null,
                CreateChildFunc = CreateApiClassifiesTreeItem,
                SoruceItemsExpression = () => group1.Classifies,
                SoruceTypeIcon = Application.Current.Resources["tree_Folder"] as BitmapImage
            };
            item.Items.Add(aitem);
            Model.CppModel.AddSolutionApiCommand(aitem);
            var group2 = new ClassifyGroupConfig<NotifyItem>(Context.Solution.NotifyItems, p => p.Classify,
                (name, cfg) => cfg.Classify = name);
            var nitem = new ConfigTreeItem<SolutionConfig>(Context.Solution)
            {
                IsAssist = true,
                Header = "Notify接口",
                HeaderField = null,
                CreateChildFunc = CreateNotifyClassifiesTreeItem,
                SoruceItemsExpression = () => group2.Classifies,
                SoruceTypeIcon = Application.Current.Resources["tree_Folder"] as BitmapImage
            };
            item.Items.Add(nitem);
            Model.CppModel.AddSolutionNotifyCommand(nitem);
        }


        public TreeItem CreateApiClassifiesTreeItem(object charg)
        {
            var child = (ClassifyItem<ApiItem>)charg;
            return new ConfigTreeItem<ClassifyItem<ApiItem>>(child)
            {
                IsAssist = true,
                Header = child.Name,
                HeaderField = "Name",
                HeaderExtendExpression = p => p.Name,
                CreateChildFunc = CreateApiItemTreeItem,
                FriendItems = child.Items,
                SoruceItemsExpression = () => child.Items,
                SoruceTypeIcon = Application.Current.Resources["tree_Folder"] as BitmapImage
            };
        }


        private TreeItem CreateNotifyClassifiesTreeItem(object charg)
        {
            var child = (ClassifyItem<NotifyItem>)charg;
            return new ConfigTreeItem<ClassifyItem<NotifyItem>>(child)
            {
                IsAssist = true,
                Header = child.Name,
                HeaderField = "Name",
                HeaderExtendExpression = p => p.Name,
                CreateChildFunc = CreateNotifyItemTreeItem,
                FriendItems = child.Items,
                SoruceItemsExpression = () => child.Items,
                SoruceTypeIcon = Application.Current.Resources["tree_Folder"] as BitmapImage
            };
        }

        private TreeItem CreateNotifyItemTreeItem(object arg)
        {
            var child = (NotifyItem)arg;
            var item = new ConfigTreeItem<NotifyItem>(child)
            {
                IsAssist = true,
                //IsExpanded = true,
                Header = child.Name,
                HeaderField = "Name,Caption,NotifyEntity,Friend,IsCommandResult",
                HeaderExtendExpression = m =>
                {
                    if (!m.IsCommandResult)
                        return $"{m.Caption}〖{m.Name}({m.NotifyEntity})〗";
                    var fri = GlobalConfig.GetApi(p => p.FriendKey == m.Key) ?? GlobalConfig.GetApi(p => p.Friend == m.Name);
                    return fri == null
                        ? $"{m.Caption}〖{m.Name}({m.NotifyEntity})〗"
                        : $"{m.Caption}〖{m.Name}({m.NotifyEntity})〗<=({fri.Caption})";
                },
                SoruceTypeIcon =
                    Application.Current.Resources[child.IsCommandResult ? "img_link" : "tree_item"] as BitmapImage
            };
            AddNotifyCommand(item, child);
            var friend = GlobalConfig.GetEntity(child.NotifyEntity);
            if (friend != null)
                item.Items.Add(Model.Tree.CreateEntityTreeItem(friend));
            friend = GlobalConfig.GetEntity(child.ClientEntity);
            if (friend != null)
                item.Items.Add(Model.Tree.CreateEntityTreeItem(friend));
            CheckNotifyColor(item);
            item.ModelPropertyChanged += TreeModelPropertyChangedByNotifyItem;
            return item;
        }

        private void TreeModelPropertyChangedByNotifyItem(object sender, PropertyChangedEventArgs e)
        {
            var item = (TreeItem)sender;
            if (e.PropertyName == "Friend")
                CheckNotifyColor(item);
        }

        private static void CheckNotifyColor(TreeItem item)
        {
            var child = (NotifyItem)item.Source;
            var api = GlobalConfig.GetApi(p => p.FriendKey == child.Key) ?? GlobalConfig.GetApi(p => p.Friend == child.Name);

            if (api == null && child.IsCommandResult)
                item.Color = Brushes.Silver;
            else if (api != null && !child.IsCommandResult)
                item.Color = Brushes.Yellow;
            else if (!child.LocalCommand)
                if (child.EsEntity == null)
                {
                    item.Color = Brushes.Red;
                }
                else if (child.LocalEntity != null)
                {
                    item.Color = Brushes.Blue;
                }
                else
                {
                    if (child.EsEntity.Properties.Count == 0)
                        item.Color = Brushes.Blue;
                    else if (child.EsEntity.Properties.Count == 1 && child.EsEntity.Properties[0].Name == "ClientNo")
                        item.Color = Brushes.Blue;
                    else
                        item.Color = Brushes.LightCoral;
                }
            else if (child.LocalEntity == null)
                item.Color = Brushes.Red;
            else
                item.Color = Brushes.Black;
        }

        public TreeItem CreateApiItemTreeItem(object arg)
        {
            var child = (ApiItem)arg;
            var item = new ConfigTreeItem<ApiItem>(child)
            {
                //IsExpanded = true,
                Header = child.Name,
                HeaderField = "Name,Caption,CallArg,Friend",
                HeaderExtendExpression = m =>
                {
                    var fri = GlobalConfig.GetNotify(p => p.FriendKey == m.Key) ??
                              GlobalConfig.GetNotify(p => p.Friend == m.Name);
                    return fri == null
                        ? $"{m.Caption}〖{m.Name}({m.CallArg})〗"
                        : $"{m.Caption}〖{m.Name}({m.CallArg})〗=>{fri.Caption}";
                },
                SoruceTypeIcon =
                    Application.Current.Resources[child.IsUserCommand ? "img_cus" : "tree_item"] as BitmapImage
            };
            var notify = GlobalConfig.GetNotify(p => p.FriendKey == child.Key) ??
                         GlobalConfig.GetNotify(p => p.Friend == child.Name);
            if (notify != null)
            {
                item.Items.Add(CreateNotifyItemTreeItem(notify));
                child.ResultArg = notify.NotifyEntity;
            }
            var item2 = new ConfigTreeItem<ApiItem>(child)
            {
                IsAssist = true,
                Header = "参数",
                HeaderField = null,
                SoruceTypeIcon = Application.Current.Resources["tree_item"] as BitmapImage
            };
            var friend = GlobalConfig.GetEntity(child.CallArg);
            if (friend != null)
                item2.Items.Add(Model.Tree.CreateEntityTreeItem(friend));
            friend = GlobalConfig.GetEntity(child.ResultArg);
            if (friend != null)
                item2.Items.Add(Model.Tree.CreateEntityTreeItem(friend));

            item.Items.Add(item2);
            if (notify == null)
            {
                item2 = new ConfigTreeItem<ApiItem>(child)
                {
                    IsAssist = true,
                    Header = "返回",
                    HeaderField = null,
                    Color = Brushes.Blue,
                    SoruceTypeIcon = Application.Current.Resources["tree_item"] as BitmapImage
                };
                friend = GlobalConfig.GetEntity(child.ResultArg);
                if (friend != null)
                    item2.Items.Add(Model.Tree.CreateEntityTreeItem(friend));
                item.Items.Add(item2);
            }
            CheckApiColor(item);
            item.ModelPropertyChanged += TreeModelPropertyChangedByApiItem;
            return item;
        }

        private void TreeModelPropertyChangedByApiItem(object sender, PropertyChangedEventArgs e)
        {
            var item = (TreeItem)sender;
            if (e.PropertyName == "Friend")
                CheckApiColor(item);
        }

        private static void CheckApiColor(TreeItem item)
        {
            var child = (ApiItem)item.Source;
            var notify = GlobalConfig.GetNotify(p => p.FriendKey == child.Key) ??
                         GlobalConfig.GetNotify(p => p.Friend == child.Name);
            if (notify == null)
                item.Color = Brushes.Silver;
            else if (!child.LocalCommand)
                if (child.EsEntity == null)
                {
                    item.Color = Brushes.Red;
                }
                else if (child.LocalEntity != null)
                {
                    item.Color = Brushes.Blue;
                }
                else
                {
                    if (child.EsEntity.Properties.Count == 0)
                        item.Color = Brushes.Blue;
                    else if (child.EsEntity.Properties.Count == 1 && child.EsEntity.Properties[0].Name == "ClientNo")
                        item.Color = Brushes.Blue;
                    else
                        item.Color = Brushes.LightCoral;
                }
            else if (child.LocalEntity == null)
                item.Color = Brushes.Red;
            else
                item.Color = Brushes.Black;
        }

        #endregion

        #region 操作命令


        internal void AddSolutionApiCommand(TreeItem item)
        {
            if (!SolutionConfig.Current.IsWeb)
            {
                item.Commands.Add(new CommandItem
                {
                    Command = new DelegateCommand(ApiRefactor),
                    Name = "API接口分析",
                    Image = Application.Current.Resources["tree_item"] as ImageSource
                });
                item.Commands.Add(new CommandItem
                {
                    Command = new DelegateCommand(ApiToClient),
                    Name = "API接口转客户端",
                    Image = Application.Current.Resources["tree_item"] as ImageSource
                });
            }
        }

        internal void AddSolutionNotifyCommand(TreeItem item)
        {
            if (!SolutionConfig.Current.IsWeb)
            {
                item.Commands.Add(new CommandItem
                {
                    Command = new DelegateCommand(AddNotify),
                    Name = "新增Notify接口",
                    Image = Application.Current.Resources["img_add"] as ImageSource
                });
                item.Commands.Add(new CommandItem
                {
                    Command = new DelegateCommand(NotifyRefactor),
                    Name = "Notify接口分析",
                    Image = Application.Current.Resources["tree_item"] as ImageSource
                });
                item.Commands.Add(new CommandItem
                {
                    Command = new DelegateCommand(NotifyToClient),
                    Name = "Notify接口转客户端",
                    Image = Application.Current.Resources["tree_item"] as ImageSource
                });
            }
        }

        internal void AddProjectTypedefCommand(TreeItem item, ProjectConfig project)
        {
            if (!SolutionConfig.Current.IsWeb)
                item.Commands.Add(new CommandItem
                {
                    NoButton = true,
                    Command = new DelegateCommand(CppRefactor),
                    Name = "C++代码分析",
                    Image = Application.Current.Resources["cpp"] as ImageSource
                });

        }

        internal void AddNotifyCommand(TreeItem item, NotifyItem notifyItem)
        {
            if (!SolutionConfig.Current.IsWeb)
                item.Commands.Add(new CommandItem
                {
                    Command = new DelegateCommand<NotifyItem>(SetApiFriend),
                    Parameter = notifyItem,
                    Name = "和上一个选择关联(如果是Api的话,否则清空)",
                    Image = Application.Current.Resources["cpp"] as ImageSource
                });

        }

        /// <summary>
        /// 和上一个选择关联(如果是Api的话,否则清空)
        /// </summary>
        /// <param name="item"></param>
        public void SetApiFriend(NotifyItem item)
        {
            var api = Context.PreSelectConfig as ApiItem;
            if (api != null)
            {
                api.FriendKey = item.Key;
                item.FriendKey = api.Key;
                api.Friend = item.Name;
                item.Friend = api.Name;
            }
            else
            {
                item.FriendKey = Guid.Empty;
                item.Friend = null;
            }
        }


        /// <summary>
        ///     C++代码分析
        /// </summary>
        /// <returns></returns>
        internal void CppRefactor()
        {
            if (Model.Context.SelectProject == null)
            {
                MessageBox.Show("选一个项目吧");
                return;
            }
            CommandIoc.CppRefactorCommand(Model.Context.SelectProject);
        }



        /// <summary>
        ///    API代码分析
        /// </summary>
        /// <returns></returns>
        internal void ApiRefactor()
        {
            CommandIoc.ApiRefactorCommand();
            Model.Tree.SelectItem.ReShow();
            Model.Tree.SelectItem.IsExpanded = true;
        }

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        internal void ApiToClient()
        {
            var model = new SolutionModel
            {
                Solution = SolutionConfig.Current
            };
            model.ApiArgToClient();
            Model.Tree.SelectItem.ReShow();
            Model.Tree.SelectItem.IsExpanded = true;
        }

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        internal void AddNotify()
        {
            CommandIoc.AddNotifyCommand();
            Model.Tree.SelectItem.ReShow();
            Model.Tree.SelectItem.IsExpanded = true;
        }
        /// <summary>
        ///     C++代码分析
        /// </summary>
        /// <returns></returns>
        internal void NotifyRefactor()
        {
            CommandIoc.NotifyRefactorCommand();
            Model.Tree.SelectItem.ReShow();
            Model.Tree.SelectItem.IsExpanded = true;
        }

        /// <summary>
        ///     C++代码分析
        /// </summary>
        /// <returns></returns>
        internal void NotifyToClient()
        {
            var model = new SolutionModel();
            model.NotifyArgToClient();
            Model.Tree.SelectItem.ReShow();
            Model.Tree.SelectItem.IsExpanded = true;
        }

        #endregion


        public void RepairTag()
        {
            try
            {
                var tables = Context.GetSelectEntities();
                foreach (var entity in tables)
                {
                    RepairTag(entity);
                }
            }
            catch (Exception ex)
            {
                Context.CurrentTrace.TraceMessage.Status = ex.ToString();
            }
        }

        /// <summary>
        ///     自动修复
        /// </summary>
        public void RepairTag(EntityConfig Entity)
        {
            if (Entity.IsFreeze)
                return;
            if (Entity.IsReference)
            {
                foreach (var col in Entity.Properties)
                {
                    col.Tag = $"{Entity.Tag},{col.CppType},{col.Name}";
                }
                return;
            }
            if (string.IsNullOrWhiteSpace(Entity.Tag))
                return;
            EntityConfig friend =GlobalConfig. GetEntity(p => p.IsReference && p != Entity && !string.IsNullOrEmpty(p.Tag) && Entity.Tag.Contains(p.Tag));
            if (friend == null)
                return;
            foreach (var col in Entity.Properties)
            {
                if (col.Discard)
                {
                    continue;
                }
                RepairTag(friend, Entity.Tag, col);
                col.IsModify = true;
            }
            Entity.IsModify = true;
        }


        internal bool RepairTag(EntityConfig friend, string head,PropertyConfig Property)
        {
            if (Property.IsSystemField)
            {
                Property.Tag = "[SYSTEM]";
                return false;
            }
            if (friend == null)
            {
                Property.Tag = null;
                return false;
            }
            string tag = Property.Tag ?? "";
            var link = friend.Properties.FirstOrDefault(p => tag == p.Tag) ??
                       friend.Properties.FirstOrDefault(p => p.Name == Property.Name);
            if (link == null)
            {
                Property.Tag = null;
                //Property.CppName = null;
                if (Property.EnumConfig != null && Property.EnumConfig.Items.Count <= 1)
                {
                    Property.EnumConfig.IsDelete = true;
                    Property.EnumConfig = null;
                    Property.CustomType = null;
                }
                else if (Property.EnumConfig == null && Property.CustomType != null)
                {
                    Property.CustomType = null;
                }
                return false;
            }
            tag = head + "," + link.CppType + "," + link.Name;
            if (friend.Tag != null && friend.Tag.Contains(tag + ",[Skip]"))
                return false;
            Property.Tag = tag;
            var cpptype = CppTypeHelper.ToCppLastType(link.CppType);

            var stru = cpptype as EntityConfig;
            if (stru != null)
            {
                link.CppLastType = Property.CppType = GlobalConfig.GetEntity(p => p.Tag == stru.Tag && p != stru)?.Name;
                return true;
            }
            var type = cpptype as TypedefItem;
            if (type == null)
            {
                Property.CppType = link.CppType;
                Property.CppLastType = link.CppType;
                return true;
            }
            Property.CppType = type.KeyWork;
            Property.CppLastType = type.KeyWork;
            if (type.ArrayLen != null)
            {
                if (type.KeyWork == "char" && !string.IsNullOrWhiteSpace(type.ArrayLen))
                {
                    int len;
                    if (Int32.TryParse(type.ArrayLen, out len))
                    {
                        link.Datalen = len;
                        Property.Datalen = len;
                    }
                    else
                    {
                        link.Datalen = 100;
                        Property.Datalen = 100;
                        link.ArrayLen = type.ArrayLen;
                    }
                }
                else
                {
                    link.ArrayLen = type.ArrayLen;
                    Property.ArrayLen = type.ArrayLen;
                }
            }

            if (type.Items.Count <= 0)
            {
                Property.EnumConfig = null;
                Property.CustomType = null;
                return true;
            }
            return false;
        }
    }
}
*/