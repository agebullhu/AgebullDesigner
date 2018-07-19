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
using System.Windows.Interactivity;
using Agebull.Common.Logging;

#endregion

namespace Agebull.Common.Mvvm
{
    /// <summary>
    ///     ��ͨ�ؼ����¼���Ϊ��
    /// </summary>
    public class DependencyBehavior<TDependency> : Behavior<TDependency>
            where TDependency : DependencyObject
    {
        /// <summary>
        ///     �󶨶���ķ�������
        /// </summary>
        public static readonly DependencyProperty BehaviorActionProperty;

        private bool _isAttached;

        static DependencyBehavior()
        {
            BehaviorActionProperty = DependencyProperty.Register("BehaviorAction",
                    typeof (BehaviorAction<TDependency>),
                    typeof (DependencyBehavior<TDependency>),
                    new UIPropertyMetadata(null, OnBehaviorActionPropertyChanged));
        }

        /// <summary>
        ///     �󶨶���ķ�������
        /// </summary>
        public BehaviorAction<TDependency> BehaviorAction
        {
            get => (BehaviorAction<TDependency>)GetValue(BehaviorActionProperty);
            set => SetValue(BehaviorActionProperty, value);
        }

        private static void OnBehaviorActionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is DependencyBehavior<TDependency> eb) || !eb._isAttached || Equals(e.OldValue, e.NewValue))
            {
                return;
            }
            try
            {
                if (e.OldValue is BehaviorAction<TDependency> action && action.DetachAction != null)
                {
                    action.DetachAction(eb.AssociatedObject);
                }
                action = e.NewValue as BehaviorAction<TDependency>;
                if (action != null && action.AttachAction != null)
                {
                    action.AttachAction(eb.AssociatedObject);
                }
            }
            catch (Exception exception)
            {
                LogRecorder.Exception(exception);
            }
        }

        /// <summary>
        ///     ����Ϊ���ӵ� AssociatedObject ����á�
        /// </summary>
        /// <remarks>
        ///     ������Ա㽫���ܹҹ��� AssociatedObject��
        /// </remarks>
        protected override sealed void OnAttached()
        {
            base.OnAttached();
            _isAttached = true;
            if (BehaviorAction == null || BehaviorAction.AttachAction == null)
            {
                return;
            }
            try
            {
                BehaviorAction.AttachAction(AssociatedObject);
            }
            catch (Exception exception)
            {
                LogRecorder.Exception(exception);
            }
        }

        /// <summary>
        ///     ����Ϊ���� AssociatedObject ����ʱ��������ʵ�ʷ���֮ǰ�����á�
        /// </summary>
        /// <remarks>
        ///     ������Ա㽫���ܴ� AssociatedObject �н���ҹ���
        /// </remarks>
        protected override sealed void OnDetaching()
        {
            base.OnDetaching();
            if (AssociatedObject != null && BehaviorAction.DetachAction != null)
            {
                try
                {
                    BehaviorAction.DetachAction(AssociatedObject);
                }
                catch (Exception exception)
                {
                    LogRecorder.Exception(exception);
                }
            }
        }
    }

    /// <summary>
    ///     ��ͨ����������Ϊ����
    /// </summary>
    public sealed class DependencyBehavior : DependencyBehavior<DependencyObject>
    {
    }
    /// <summary>
    ///     ��ͨ����������Ϊ����
    /// </summary>
    public sealed class ElementBehavior : DependencyBehavior<FrameworkElement>
    {
    }
}
