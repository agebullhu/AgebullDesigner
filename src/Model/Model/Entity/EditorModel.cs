using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using Application = System.Windows.Application;

namespace Agebull.EntityModel.Designer
{
    public class EditorModel : DesignModelBase
    {
        #region 初始化
        public EditorModel()
        {
            ExtendEditorManager = new EditorManager
            {
                Model = this
            };
        }

        public void CreateMenus(DataModelDesignModel model)
        {
            Menus = CreateMenus();
        }

        /// <summary>
        ///     初始化
        /// </summary>
        protected override void DoInitialize()
        {
            Dispatcher = Model.Dispatcher;
            OnPropertyChanged(nameof(Menus));
            ExtendEditorManager.CheckEditor();
            Context.PropertyChanged += Context_PropertyChanged;
        }

        /// <summary>
        /// 同步解决方案变更
        /// </summary>
        public override void OnSolutionChanged()
        {
            SolutionConfig.Current.WorkView = Screen.WorkView;
            ExtendEditorManager.CheckEditor();
        }

        private Action checkExtendAction;
        private Action CheckWindowAction => checkExtendAction ??= ExtendEditorManager.CheckEditor;

        #endregion

        #region 扩展对象

        public EditorManager ExtendEditorManager { get; }

        /// <summary>
        /// 菜单
        /// </summary>
        public NotificationList<CommandItemBase> ExtendEditors => ExtendEditorManager.ExtendEditors;

        private void Context_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Model.Context.SelectConfig))
            {
                Model.Dispatcher.BeginInvoke(CheckWindowAction);
            }
        }
        #endregion

        #region 菜单

        /// <summary>
        /// 菜单
        /// </summary>
        public NotificationList<CommandItemBase> Menus { get; set; }

        private int fileMenuCount;
        /// <summary>
        /// 构造命令列表
        /// </summary>
        /// <returns></returns>
        NotificationList<CommandItemBase> CreateMenus()
        {
            #region 文件菜单

            fileMenu = new CommandItem
            {
                Caption = "文件",
                IsRoot = true,
                Items = new NotificationList<CommandItemBase>
                {
                    new SimpleCommandItem
                    {
                        Action = Model.ConfigIo.CreateNew,
                        Caption = "新建",
                        IconName  ="新文件"
                    },
                    new SimpleCommandItem
                    {
                        Action = Model.ConfigIo.Load,
                        Caption = "打开",
                        IconName  ="文件夹"
                    },
                    new SimpleCommandItem
                    {
                        Action = Model.ConfigIo.ReLoad,
                        Caption = "重新载入",
                        DoConfirm=true,
                        IconName  ="载入"
                    },
                    CommandItemBase.Line,
                    new SimpleCommandItem
                    {
                        Action = Model.ConfigIo.LoadGlobal,
                        Caption = "全局",
                        IconName  ="地球"
                    },
                    new SimpleCommandItem
                    {
                        Action = Model.ConfigIo.LoadLocal,
                        Caption = "本地",
                        IconName  ="本地"
                    },
                    CommandItemBase.Line,
                    new SimpleCommandItem
                    {
                        Caption = "保存实体",
                        SoruceView = "entity",
                        Action = Model.ConfigIo.SaveEntity,
                        IconName  ="保存"
                    },
                    new SimpleCommandItem
                    {
                        IsButton=true,
                        Action = Model.ConfigIo.SaveProject,
                        Caption = "保存项目",
                        IconName  ="保存"
                    },
                    new SimpleCommandItem
                    {
                        IsButton=false,
                        Action = Model.ConfigIo.SaveSolution,
                        Caption = "保存解决方案",
                        IconName  ="保存"
                    },
                    CommandItemBase.Line,
                    CommandItemBase.Line,
                    new SimpleCommandItem
                    {
                        Action = Application.Current.Shutdown,
                        Caption = "退出",
                        DoConfirm=true,
                        IconName  ="关闭"
                    }
                }
            };
            fileMenuCount = fileMenu.Items.Count;
            #endregion
            #region 窗口菜单

            windowMenu = new CommandItem
            {
                Caption = "窗口",
                IsRoot = true
            };

            foreach (var ed in EditorManager.GlobalEditors)
            {
                var item = new CommandItem<string>
                {
                    Index = ed.Value.Index,
                    Source = ed.Key,
                    Action = ExtendEditorManager.OnEditorSelect,
                    Caption = ed.Key,
                    IconName = ed.Value.Icon
                };
                item.Source = item;
                windowMenu.Items.Add(item);
            }
            #endregion
            #region 视角菜单

            viewMenu = new CommandItem
            {
                Caption = "设计",
                IsRoot = true
            };
            var vitem = new CommandItem<CommandItemBase>
            {
                Action = OnWorkView,
                Caption = "专家视角",
                IconName = "视角"
            };
            vitem.Source = vitem;
            viewMenu.Items.Add(vitem);
            vitem = new CommandItem<CommandItemBase>
            {
                Action = OnWorkView,
                Caption = "数据库设计",
                WorkView = "DataBase",
                IconName = "数据库"
            };
            vitem.Source = vitem;
            viewMenu.Items.Add(vitem);
            vitem = new CommandItem<CommandItemBase>
            {
                Action = OnWorkView,
                Caption = "实体设计",
                WorkView = "Entity",
                IconName = "实体"
            };
            vitem.Source = vitem;
            viewMenu.Items.Add(vitem);
            vitem = new CommandItem<CommandItemBase>
            {
                Action = OnWorkView,
                Caption = "模型设计",
                WorkView = "Model",
                IconName = "模型"
            };
            vitem.Source = vitem;
            viewMenu.Items.Add(vitem);
            vitem = new CommandItem<CommandItemBase>
            {
                Action = OnWorkView,
                Caption = "用户交互",
                WorkView = "UI",
                IconName = "UI"
            };
            vitem.Source = vitem;
            viewMenu.Items.Add(vitem);
            viewMenu.Items.Add(CommandItemBase.Line);
            vitem = new CommandItem<CommandItemBase>
            {
                Action = OnAdvancedView,
                Caption = "全部属性",
                IsChecked = AdvancedView,
                IconName = "全部"
            };
            vitem.Source = vitem;
            viewMenu.Items.Add(vitem);
            viewMenu.Items.Add(CommandItemBase.Line);
            #endregion
            return new NotificationList<CommandItemBase>
            {
                fileMenu,
                viewMenu,
                windowMenu
            };
        }
        internal CommandItem windowMenu;
        private CommandItemBase[] _buttons;


        /// <summary>
        ///     对应的命令集合
        /// </summary>
        public CommandItemBase[] Buttons
        {
            get => _buttons;
            set
            {
                _buttons = value;
                RaisePropertyChanged(nameof(Buttons));
            }
        }

        private CommandItemBase viewMenu, fileMenu;
        /// <summary>
        /// 同步菜单
        /// </summary>
        /// <param name="item"></param>
        internal void SyncMenu(TreeItem item)
        {
            if (item == null)
            {
                return;
            }
            var menus = Menus;

            while (menus.Count > 3)
                menus.RemoveAt(2);
            while (fileMenu.Items.Count > fileMenuCount)
                fileMenu.Items.RemoveAt(fileMenuCount - 2);
            while (viewMenu.Items.Count > 8)
                viewMenu.Items.RemoveAt(8);

            menus.Insert(2, new CommandItem
            {
                IsRoot = true,
                Caption = "编辑",
                Items = new NotificationList<CommandItemBase>()
            });
            menus.Insert(3, new CommandItem
            {
                IsRoot = true,
                Caption = "工具",
                Items = new NotificationList<CommandItemBase>()
            });
            if (item.Commands == null || item.Commands.Count == 0)
            {
                Buttons = new CommandItem[0];
                return;
            }

            var selType = Context.SelectConfig.GetType();
            bool preIsLine = false;
            foreach (var cmd in item.Commands)
            {
                if (cmd.IsLine)
                {
                    preIsLine = true;
                    continue;
                }
                if (cmd.SignleSoruce)
                {
                    if (cmd.TargetType != null && selType != cmd.TargetType && !selType.IsSubclassOf(cmd.TargetType))
                        continue;
                }
                else if (WorkView != null && cmd.WorkView != null && !cmd.WorkView.Contains(WorkView))
                {
                    continue;
                }
                string cl = cmd.Catalog ?? "其它";
                CommandItemBase sub;
                switch (cl)
                {
                    case "文件":
                        preIsLine = false;
                        fileMenu.Items.Insert(fileMenu.Items.Count - 2, cmd);
                        continue;
                    case "窗口":
                        sub = windowMenu;
                        break;
                    default:
                        sub = menus.FirstOrDefault(p => p.Caption == cl);
                        if (sub == null)
                            menus.Insert(menus.Count - 2, sub = new CommandItem
                            {
                                IsRoot = true,
                                Name = cl,
                                Caption = cl,
                                Items = new NotificationList<CommandItemBase>()
                            });
                        break;
                }
                if (preIsLine)
                {
                    if (sub.Items.Count > 0)
                        sub.Items.Add(CommandItemBase.Line);
                    preIsLine = false;
                }
                sub.Items.Add(cmd);
            }

            List<CommandItemBase> buttons = new List<CommandItemBase>();
            foreach (var menu in menus)
            {
                foreach (var cmd in menu.Items.Where(p => !p.IsLine && !p.NoButton))
                {
                    buttons.Add(cmd);
                }
            }
            Buttons = buttons.ToArray();

        }


        #endregion

        #region 业务视角

        CommandItemBase _preWorkViewItem;
        public void OnWorkView(CommandItemBase viewItem)
        {
            if (_preWorkViewItem != null)
                _preWorkViewItem.IsChecked = false;
            _preWorkViewItem = viewItem;
            viewItem.IsChecked = true;
            WorkView = viewItem.WorkView;
            ExtendEditorManager.CheckEditor();
            DataModelDesignModel.SaveUserScreen();
        }

        /// <summary>
        /// 工作视角
        /// </summary>
        public string WorkView
        {
            get => SolutionConfig.Current?.WorkView;
            set
            {
                if (SolutionConfig.Current != null)
                    SolutionConfig.Current.WorkView = value;
                if (Screen.WorkView != value)
                {
                    Screen.WorkView = value;
                }
            }
        }

        public void OnAdvancedView(CommandItemBase view)
        {
            view.IsChecked = AdvancedView = !view.IsChecked;
        }
        /// <summary>
        /// 高级视角
        /// </summary>
        public bool AdvancedView
        {
            get => SolutionConfig.Current?.AdvancedView ?? false;
            set
            {
                Screen.AdvancedView = value;
                if (SolutionConfig.Current != null)
                    SolutionConfig.Current.AdvancedView = value;
                DataModelDesignModel.SaveUserScreen();
            }
        }

        /// <summary>
        /// 用户操作的现场记录
        /// </summary>
        public static UserScreen Screen => DataModelDesignModel.Screen;

        #endregion

        #region EditPanel

        /// <summary>
        /// 临时显示跟踪窗口
        /// </summary>
        public void ShowTrace()
        {
            ExtendEditorManager.OnEditorSelect("跟踪信息");
        }
        /// <summary>
        /// 临时显示代码窗口
        /// </summary>
        public void ShowCode()
        {
            ExtendEditorManager.OnEditorSelect("代码生成");
        }

        #endregion

    }
}