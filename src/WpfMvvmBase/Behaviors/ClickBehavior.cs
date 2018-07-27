// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// ����:bull2
// ����:CodeRefactor-Gboxt.Common.WpfMvvmBase
// ����:2014-11-27
// �޸�:2014-11-29
// *****************************************************/

#region ����

using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

#endregion

namespace Agebull.Common.Mvvm
{
    /// <summary>
    ///     �����¼���Ϊ��
    /// </summary>
    public sealed class ClickBehavior : Behavior<UIElement>
    {
        /// <summary>
        ///     �󶨵�����
        /// </summary>
        public static readonly DependencyProperty CommandProperty;

        /// <summary>
        ///     �󶨵�����
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty;

        /// <summary>
        ///     �󶨵�����
        /// </summary>
        public static readonly DependencyProperty IsDoubleClickProperty;

        static ClickBehavior()
        {
            CommandProperty = DependencyProperty.Register("Command",
                    typeof (ICommand),
                    typeof (ClickBehavior),
                    new UIPropertyMetadata(null, OnCommandPropertyChanged));

            CommandParameterProperty = DependencyProperty.Register("CommandProperty",
                    typeof (object),
                    typeof (ClickBehavior));

            IsDoubleClickProperty = DependencyProperty.Register("IsDoubleClick",
                    typeof (bool),
                    typeof (ClickBehavior));
        }

        /// <summary>
        ///     ����
        /// </summary>
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        ///     �Ƿ�˫��
        /// </summary>
        public bool IsDoubleClick
        {
            get => (bool)GetValue(IsDoubleClickProperty);
            set => SetValue(IsDoubleClickProperty, value);
        }

        /// <summary>
        ///     �������
        /// </summary>
        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        private static void OnCommandPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is ClickBehavior eb) || Equals(e.OldValue, e.NewValue))
            {
                return;
            }
            if (e.OldValue is ICommand cmd)
            {
                cmd.CanExecuteChanged -= eb.OnCanExecuteChanged;
            }
            cmd = e.NewValue as ICommand;
            if (cmd != null)
            {
                cmd.CanExecuteChanged += eb.OnCanExecuteChanged;
            }
        }

        private void OnCanExecuteChanged(object sender, EventArgs e)
        {
            AssociatedObject.IsEnabled = Command.CanExecute(CommandProperty);
        }

        /// <summary>
        ///     ����Ϊ���ӵ� AssociatedObject ����á�
        /// </summary>
        /// <remarks>
        ///     ������Ա㽫���ܹҹ��� AssociatedObject��
        /// </remarks>
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.MouseLeftButtonDown += OnMouseLeftButtonDown;
        }

        /// <summary>
        ///     ����Ϊ���� AssociatedObject ����ʱ��������ʵ�ʷ���֮ǰ�����á�
        /// </summary>
        /// <remarks>
        ///     ������Ա㽫���ܴ� AssociatedObject �н���ҹ���
        /// </remarks>
        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.MouseLeftButtonDown -= OnMouseLeftButtonDown;
        }

        /// <summary>
        ///     ����ʱִ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount > 1 && !IsDoubleClick)
            {
                return;
            }
            e.Handled = true;
            ICommand cmd = Command;
            object par = CommandParameter;
            if (cmd.CanExecute(par))
            {
                cmd.CanExecute(par);
            }
        }
    }
}
