// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// ����:bull2
// ����:CodeRefactor-Agebull.Common.WpfMvvmBase
// ����:2014-11-26
// �޸�:2014-12-07
// *****************************************************/

#region ����

using Agebull.EntityModel;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

#endregion

namespace Agebull.Common.Mvvm
{
    /// <summary>
    ///     ��ʾһ������ڵ�
    /// </summary>
    public abstract class CommandItemBase : CommandConfig
    {
        #region ״̬

        string AutoIcon(string name)
        {
            if(name == null)
                return "Icon";
            if (name.Contains("����") || name.Contains("����"))
                return "����";
            if (name.Contains("���") || name.Contains("ɾ��"))
                return "ɾ��";
            if (name.Contains("����"))
                return "����";
            return "Icon";
        }

        /// <summary>
        ///     ͼ��
        /// </summary>
        public string Image
        {
            get => IsRoot
                ? null
                : IconMap.Instance[IconName ?? AutoIcon(Caption)];
        }

        /// <summary>
        ///     �Ƿ��
        /// </summary>
        public bool IsRoot { get; set; }

        /// <summary>
        ///     �Ƿ���
        /// </summary>
        public bool IsLine { get; set; }

        /// <summary>
        /// ��ʾ�ָ���
        /// </summary>
        public static CommandItem Line { get; } = new CommandItem
        {
            IsLine = true
        };


        private bool _isChecked;

        /// <summary>
        ///     �Ƿ�ѡ��
        /// </summary>
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                _isChecked = value;
                RaisePropertyChanged(nameof(IsChecked));
            }
        }
        private bool _isBusy;

        /// <summary>
        ///     ͼ��
        /// </summary>
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (_isBusy == value)
                    return;
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        private Visibility _visibility;

        /// <summary>
        ///     �ɼ�
        /// </summary>
        public Visibility Visibility
        {
            get => _visibility;
            set
            {
                if (_visibility == value)
                    return;
                _visibility = value;
                RaisePropertyChanged(() => Visibility);
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public object Source { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public object Parameter => Source;
        #endregion

        #region ����

        private ICommand _command;


        /// <summary>
        ///     ��Ӧ������
        /// </summary>
        public ICommand Command
        {
            get => _command;
            protected set
            {
                _command = value;
                if (value is INotifyPropertyChanged pp)
                    pp.PropertyChanged += OnCommandPropertyChanged;
            }
        }

        protected void OnCommandPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!(sender is IStatusCommand cmd))
                return;
            switch (e.PropertyName)
            {
                case "IsBusy":
                    IsBusy = cmd.IsBusy;
                    break;
                case "Visibility":
                    Visibility = cmd.Visibility;
                    break;
            }
        }
        /// <summary>
        /// ִ��
        /// </summary>
        public abstract void Execute(object arg);
        /// <summary>
        /// ׼������
        /// </summary>
        public Func<CommandItemBase, bool> OnPrepare { get; set; }

        #endregion

        #region �Ӽ�

        /// <summary>
        /// ���а�ť
        /// </summary>
        public NotificationList<CommandItemBase> Items { get; set; } = new NotificationList<CommandItemBase>();

        #endregion

        #region ����

        /// <summary>
        /// ��ʾ��Ϣ
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        public static void ShowMessageBox(string title, string message)
        {
            WorkContext.SynchronousContext.InvokeInUiThread(() => MessageBox.Show(message, title));
        }
        /// <summary>
        /// ��ʾ��Ϣ
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        public void ShowMessageBox(string message)
        {
            WorkContext.SynchronousContext.InvokeInUiThread(() => MessageBox.Show(message, Caption ?? Name));
        }
        #endregion
    }

}