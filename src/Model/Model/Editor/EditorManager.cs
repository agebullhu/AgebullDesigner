using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 扩展设计器管理类
    /// </summary>
    public class EditorManager : NotificationObject
    {
        #region 对象注册

        internal static readonly Dictionary<Type, Dictionary<string, EditorOption>> Editors = new Dictionary<Type, Dictionary<string, EditorOption>>();

        internal static readonly Dictionary<string, EditorOption> GlobalEditors = new Dictionary<string, EditorOption>();

        /// <summary>
        /// 注册扩展
        /// </summary>
        /// <typeparam name="TEditor">扩展类型</typeparam>
        /// <param name="name">扩展名称</param>
        public static void Registe<TEditor>(string name, int idx, string icon = "img_modify")
            where TEditor : UserControl, new()
        {
            GlobalEditors[name] = new EditorOption
            {
                Name = name,
                Index = idx,
                Icon = icon,
                Create = () => new TEditor()
            };
        }

        /// <summary>
        /// 注册扩展
        /// </summary>
        /// <typeparam name="TEditor">扩展类型</typeparam>
        /// <typeparam name="TConfig">对应的类型</typeparam>
        /// <param name="name">扩展名称</param>
        /// <param name="filter">关联的工作模式</param>
        public static void Registe2<TConfig, TEditor>(string name, string icon, params string[] filter)
            where TEditor : UserControl, new()
        {
            Registe2<TConfig, TEditor>(name, icon, 0, filter);
        }

        /// <summary>
        /// 注册扩展
        /// </summary>
        /// <typeparam name="TEditor">扩展类型</typeparam>
        /// <typeparam name="TConfig">对应的类型</typeparam>
        /// <param name="name">扩展名称</param>
        /// <param name="filter">关联的工作模式</param>
        public static void Registe<TConfig, TEditor>(string name, params string[] filter)
            where TEditor : UserControl, new()
        {
            Registe<TConfig, TEditor>(name, 0, filter);
        }

        /// <summary>
        /// 注册扩展
        /// </summary>
        /// <typeparam name="TEditor">扩展类型</typeparam>
        /// <typeparam name="TConfig">对应的类型</typeparam>
        /// <param name="name">扩展名称</param>
        /// <param name="index">显示顺序</param>
        /// <param name="filter">关联的工作模式</param>
        public static void Registe2<TConfig, TEditor>(string name, string icon, int index, params string[] filter)
            where TEditor : UserControl, new()
        {
            if (!Editors.TryGetValue(typeof(TConfig), out var exts))
                Editors.Add(typeof(TConfig), exts = new Dictionary<string, EditorOption>(StringComparer.OrdinalIgnoreCase));
            exts[name] = new EditorOption
            {
                Name = name,
                Index = index,
                Icon = icon ?? "编辑器",
                Caption = name,
                Create = () => new TEditor()
            };
            if (filter.Length > 0)
                exts[name].Filter.AddRange(filter);
        }
        /// <summary>
        /// 注册扩展
        /// </summary>
        /// <typeparam name="TEditor">扩展类型</typeparam>
        /// <typeparam name="TConfig">对应的类型</typeparam>
        /// <param name="name">扩展名称</param>
        /// <param name="index">显示顺序</param>
        /// <param name="filter">关联的工作模式</param>
        public static void Registe<TConfig, TEditor>(string name, int index, params string[] filter)
            where TEditor : UserControl, new()
        {
            if (!Editors.TryGetValue(typeof(TConfig), out var exts))
                Editors.Add(typeof(TConfig), exts = new Dictionary<string, EditorOption>(StringComparer.OrdinalIgnoreCase));
            exts[name] = new EditorOption
            {
                Name = name,
                Index = index,
                Icon = "编辑器",
                Caption = name,
                Create = () => new TEditor()
            };
            if (filter.Length > 0)
                exts[name].Filter.AddRange(filter);
        }
        #endregion

        #region 控件管理

        /// <summary>
        /// 菜单
        /// </summary>
        internal NotificationList<CommandItemBase> ExtendEditors = new NotificationList<CommandItemBase>();

        /// <summary>
        /// 菜单
        /// </summary>
        readonly List<CommandItemBase> editors = new List<CommandItemBase>();

        internal EditorModel Model { get; set; }

        /// <summary>
        /// 扩展对象插入的控件
        /// </summary>
        internal static Border ExtendEditorPanel { get; set; }

        /// <summary>
        /// 当前已匹配的类型
        /// </summary>
        Type CurrentType;

        /// <summary>
        /// 当前已匹配的视角
        /// </summary>
        string CurrentView;

        /// <summary>
        /// 当前已匹配的视图模型
        /// </summary>
        EditorViewModelBase currentViewModel;

        /// <summary>
        /// 是否存在扩展编辑器
        /// </summary>
        internal bool HaseEditor { get; private set; }

        /// <summary>
        /// 是否使用扩展属性
        /// </summary>
        bool IsFriend;

        /// <summary>
        /// 当前编辑器名称
        /// </summary>
        public string CurrentEditorName
        {
            get => currentEditorName;
            set
            {
                currentEditorName = value;
                RaisePropertyChanged(nameof(CurrentEditorName));
            }
        }

        private string currentEditorName;

        ConfigBase CurrentConfig;

        bool IsGlobal;


        /// <summary>
        /// 历史对应
        /// </summary>
        readonly Dictionary<Type, string> history = new Dictionary<Type, string>();

        internal void OnEditorSelect(string editor)
        {
            if (CurrentConfig == null)
                return;
            currentEditorName = editor;
            FindEditor();
        }

        /// <summary>
        /// 构造或更新编辑器
        /// </summary>
        internal void CheckEditor()
        {
            if (Model.Context.SelectConfig == null)
            {
                ExtendEditors.Clear();
                CurrentConfig = null;
                CreateEmptyEditor();
                return;
            }
            var type = Model.Context.SelectConfig.GetType();

            if (CurrentType == type && CurrentView == Model.WorkView)
            {
                CurrentConfig = IsFriend ? Model.Context.SelectConfig.Friend : Model.Context.SelectConfig;
                if (currentViewModel != null)
                {
                    currentViewModel.SetContextConfig(Model.Model, CurrentConfig);
                }
                return;
            }

            CurrentType = type;
            if (!history.TryGetValue(type, out currentEditorName))
            {
                currentEditorName = null;
            }
            FindEditor();
            ExtendEditors.Clear();
            ExtendEditors.AddRange(editors.OrderBy(p => p.Index));
        }

        private void FindEditor()
        {
            CurrentView = Model.WorkView;
            currentViewModel = null;
            HaseEditor = false;
            editors.Clear();

            Type type = CurrentType;
            ReadEditors(type, typeof(ConfigBase), GlobalEditors, 2);
            foreach (var exts in Editors)
            {
                ReadEditors(type, exts.Key, exts.Value, 0);
            }
            if (Model.Context.SelectConfig.Friend != null)
            {
                type = Model.Context.SelectConfig.Friend.GetType();
                foreach (var exts in Editors)
                {
                    ReadEditors(type, exts.Key, exts.Value, 1);
                }
            }
            if (!HaseEditor)
                CreateEmptyEditor();
        }

        private void CreateEmptyEditor()
        {
            EditorModel.Screen.NowEditor = currentEditorName = null;
            HaseEditor = false;
            IsGlobal = false;
            IsFriend = false;

            WorkContext.SynchronousContext.InvokeInUiThread(() =>
            {
                ExtendEditorPanel.UpdateLayout();
                ExtendEditorPanel.Child = new TextBlock
                {
                    Text = "未匹配到编辑器",
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                };
                ExtendEditorPanel.Child.UpdateLayout();
                ExtendEditorPanel.UpdateLayout();
            });
        }

        private void ReadEditors(Type curType, Type editorType, Dictionary<string, EditorOption> exts, int type)
        {
            if (curType != editorType && !curType.IsSubclassOf(editorType) && !curType.IsSupperInterface(editorType))
                return;
            bool current = currentEditorName == null;
            foreach (var ext in exts.OrderBy(p => p.Value.Index))
            {
                if (CurrentView.IsPresent() && ext.Value.Filter.Count > 0 && !ext.Value.Filter.Contains(CurrentView, StringComparer.OrdinalIgnoreCase))
                    continue;
                editors.Add(new CommandItem<string>
                {
                    Index = ext.Value.Index,
                    Source = ext.Key,
                    Action = OnEditorSelect,
                    Caption = ext.Value.Caption,
                    IconName = ext.Value.Icon
                });
                if ((current && currentEditorName == null) || (!current && ext.Key == currentEditorName))
                {
                    CreateEditor(ext.Key, type, ext.Value, IsFriend ? Model.Context.SelectConfig.Friend : Model.Context.SelectConfig);
                }
            }
        }

        private void CreateEditor(string name, int type, EditorOption option, ConfigBase config)
        {
            CurrentConfig = config;
            HaseEditor = true;
            IsFriend = type == 1;
            IsGlobal = type == 2;
            history[CurrentType] = EditorModel.Screen.NowEditor = CurrentEditorName = name;

            WorkContext.SynchronousContext.InvokeInUiThread(ShowEditor,option);
        }

        private void ShowEditor(EditorOption option)
        {
            try
            {
                var editor = option.Create();
                if (editor.DataContext is EditorViewModelBase viewModel)
                {
                    currentViewModel = viewModel;
                    currentViewModel.SetContextConfig(Model.Model, CurrentConfig);
                }
                else
                {
                    currentViewModel = null;
                }
                if (IsGlobal)
                {
                    ExtendEditorPanel.Child = editor;
                }
                else
                {
                    ExtendEditorPanel.Child = new EditorPanel
                    {
                        Editor = editor,
                        DataContext = editor.DataContext,
                        VerticalAlignment = VerticalAlignment.Stretch,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                    };
                }
                //ExtendEditorPanel.Child.UpdateLayout();
                ExtendEditorPanel.UpdateLayout();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex, "ShowEditor");
            }
        }

        #endregion
    }
}