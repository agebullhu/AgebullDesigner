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
    /// ��չ�����������
    /// </summary>
    public class EditorManager : NotificationObject
    {
        #region ����ע��

        internal static readonly Dictionary<Type, Dictionary<string, EditorOption>> Editors = new Dictionary<Type, Dictionary<string, EditorOption>>();

        internal static readonly Dictionary<string, EditorOption> GlobalEditors = new Dictionary<string, EditorOption>();

        /// <summary>
        /// ע����չ
        /// </summary>
        /// <typeparam name="TEditor">��չ����</typeparam>
        /// <param name="name">��չ����</param>
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
        /// ע����չ
        /// </summary>
        /// <typeparam name="TEditor">��չ����</typeparam>
        /// <typeparam name="TConfig">��Ӧ������</typeparam>
        /// <param name="name">��չ����</param>
        /// <param name="filter">�����Ĺ���ģʽ</param>
        public static void Registe2<TConfig, TEditor>(string name, string icon, params string[] filter)
            where TEditor : UserControl, new()
        {
            Registe2<TConfig, TEditor>(name, icon, 0, filter);
        }

        /// <summary>
        /// ע����չ
        /// </summary>
        /// <typeparam name="TEditor">��չ����</typeparam>
        /// <typeparam name="TConfig">��Ӧ������</typeparam>
        /// <param name="name">��չ����</param>
        /// <param name="filter">�����Ĺ���ģʽ</param>
        public static void Registe<TConfig, TEditor>(string name, params string[] filter)
            where TEditor : UserControl, new()
        {
            Registe<TConfig, TEditor>(name, 0, filter);
        }

        /// <summary>
        /// ע����չ
        /// </summary>
        /// <typeparam name="TEditor">��չ����</typeparam>
        /// <typeparam name="TConfig">��Ӧ������</typeparam>
        /// <param name="name">��չ����</param>
        /// <param name="index">��ʾ˳��</param>
        /// <param name="filter">�����Ĺ���ģʽ</param>
        public static void Registe2<TConfig, TEditor>(string name, string icon, int index, params string[] filter)
            where TEditor : UserControl, new()
        {
            if (!Editors.TryGetValue(typeof(TConfig), out var exts))
                Editors.Add(typeof(TConfig), exts = new Dictionary<string, EditorOption>(StringComparer.OrdinalIgnoreCase));
            exts[name] = new EditorOption
            {
                Name = name,
                Index = index,
                Icon = icon ?? "�༭��",
                Caption = name,
                Create = () => new TEditor()
            };
            if (filter.Length > 0)
                exts[name].Filter.AddRange(filter);
        }
        /// <summary>
        /// ע����չ
        /// </summary>
        /// <typeparam name="TEditor">��չ����</typeparam>
        /// <typeparam name="TConfig">��Ӧ������</typeparam>
        /// <param name="name">��չ����</param>
        /// <param name="index">��ʾ˳��</param>
        /// <param name="filter">�����Ĺ���ģʽ</param>
        public static void Registe<TConfig, TEditor>(string name, int index, params string[] filter)
            where TEditor : UserControl, new()
        {
            if (!Editors.TryGetValue(typeof(TConfig), out var exts))
                Editors.Add(typeof(TConfig), exts = new Dictionary<string, EditorOption>(StringComparer.OrdinalIgnoreCase));
            exts[name] = new EditorOption
            {
                Name = name,
                Index = index,
                Icon = "�༭��",
                Caption = name,
                Create = () => new TEditor()
            };
            if (filter.Length > 0)
                exts[name].Filter.AddRange(filter);
        }
        #endregion

        #region �ؼ�����

        /// <summary>
        /// �˵�
        /// </summary>
        internal NotificationList<CommandItemBase> ExtendEditors = new NotificationList<CommandItemBase>();

        /// <summary>
        /// �˵�
        /// </summary>
        readonly List<CommandItemBase> editors = new List<CommandItemBase>();

        internal EditorModel Model { get; set; }

        /// <summary>
        /// ��չ�������Ŀؼ�
        /// </summary>
        internal static Border ExtendEditorPanel { get; set; }

        /// <summary>
        /// ��ǰ��ƥ�������
        /// </summary>
        Type CurrentType;

        /// <summary>
        /// ��ǰ��ƥ����ӽ�
        /// </summary>
        string CurrentView;

        /// <summary>
        /// ��ǰ��ƥ�����ͼģ��
        /// </summary>
        EditorViewModelBase currentViewModel;

        /// <summary>
        /// �Ƿ������չ�༭��
        /// </summary>
        internal bool HaseEditor { get; private set; }

        /// <summary>
        /// �Ƿ�ʹ����չ����
        /// </summary>
        bool IsFriend;

        /// <summary>
        /// ��ǰ�༭������
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
        /// ��ʷ��Ӧ
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
        /// �������±༭��
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
                    Text = "δƥ�䵽�༭��",
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