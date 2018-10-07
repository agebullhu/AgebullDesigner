// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// ����:bull2
// ����:CodeRefactor-Agebull.Common.WpfMvvmBase
// ����:2014-11-24
// �޸�:2014-12-07
// *****************************************************/

#region ����

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Agebull.Common.Logging;

#endregion

namespace Agebull.Common.Mvvm
{
    /// <summary>
    ///     �޲����ı�׼ί������
    /// </summary>
    public sealed class DelegateCommand : IStatusCommand
    {
        /// <summary>
        /// �շ���,�Ա�������һ��ʲôҲ����������
        /// </summary>
        private static void EmptyAction()
        {

        }

        public object Tag { get; set; }

        /// <summary>
        ///     �����޸��¼�(����Ϊ�ձ�ʾɾ��)
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                propertyChanged -= value;
                propertyChanged += value;
            }
            remove => propertyChanged -= value;
        }

        /// <summary>
        ///     �����޸��¼�
        /// </summary>
        private event PropertyChangedEventHandler propertyChanged;

        /// <summary>
        ///     ���������޸��¼�(������ֹģʽӰ��)
        /// </summary>
        /// <param name="propertyName">����</param>
        private void RaisePropertyChanged(string propertyName)
        {
            if (propertyChanged == null)
            {
                return;
            }
            RaisePropertyChangedInner(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     ���������޸��¼�
        /// </summary>
        /// <param name="args">����</param>
        private void RaisePropertyChangedInner(PropertyChangedEventArgs args)
        {
            try
            {
                propertyChanged?.Invoke(this, args);
            }
            catch (Exception ex)
            {
                LogRecorder.Exception(ex);
            }
        }


        /// <summary>
        ///     ���ӻ�
        /// </summary>
        public Visibility Visibility => _canExecuteAction == null || _canExecuteAction() ? Visibility.Visible : Visibility.Collapsed;

        private static DelegateCommand _emptyCommand;
        /// <summary>
        /// ������
        /// </summary>
        public static ICommand EmptyCommand => _emptyCommand ?? (_emptyCommand = new DelegateCommand(EmptyAction));

        /// <summary>
        ///     Ĭ������Ϊ��ļ��executeAction�ܷ�ִ��״̬�ķ���,Ŀ���Ƿ�ֹ������������ͬ���õ�Action�˷��ڴ�
        /// </summary>
        /// <returns></returns>
        public static readonly Func<bool> DefaultCanExecuteAction = () => true;
        /// <summary>
        ///     ���executeAction�ܷ�ִ��״̬�ķ���
        /// </summary>
        private readonly Func<bool> _canExecuteAction;

        /// <summary>
        ///     �������巽��
        /// </summary>
        private readonly Action _executeAction;

        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="executeAction">�������巽��</param>
        public DelegateCommand(Action executeAction)
            : this(executeAction, DefaultCanExecuteAction)
        {
        }

        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="executeAction">�������巽��</param>
        /// <param name="canExecuteAction">���executeAction�ܷ�ִ��״̬�ķ���</param>
        public DelegateCommand(Action executeAction, Func<bool> canExecuteAction)
        {
            if (executeAction == null || canExecuteAction == null)
            {
                throw new ArgumentNullException("executeAction", @"���������Ϊ��");
            }
            _executeAction = executeAction;
            _canExecuteAction = canExecuteAction;
        }

        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="executeAction">�������巽��</param>
        /// <param name="canExecuteAction">���executeAction�ܷ�ִ��״̬�ķ���</param>
        /// <param name="model">���仯�¼���ģ��</param>
        public DelegateCommand(Action executeAction, INotifyPropertyChanged model, Func<bool> canExecuteAction)
            : this(executeAction, canExecuteAction)
        {
            model.PropertyChanged += OnModelPropertyChanged;
        }

        /// <summary>
        ///     �Ƿ���æ
        /// </summary>
        public bool IsBusy => false;

        /// <summary>
        ///     ��ǰ״̬
        /// </summary>
        public CommandStatus Status => CommandStatus.None;

        /// <summary>
        ///     �����ڵ��ô�����ʱ���õķ�����
        /// </summary>
        /// <param name="parameter">������ʹ�õ����ݡ�����������Ҫ�������ݣ���ö����������Ϊ null��</param>
        void ICommand.Execute(object parameter)
        {
            Execute();
        }

        /// <summary>
        ///     ��ִ��״̬�仯���¼�
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        ///     ��������ȷ���������Ƿ�������䵱ǰ״̬��ִ�еķ�����
        /// </summary>
        /// <returns>
        ///     �������ִ�д������Ϊ true������Ϊ false��
        /// </returns>
        /// <param name="parameter">������ʹ�õ����ݡ�����������Ҫ�������ݣ���ö����������Ϊ null��</param>
        bool ICommand.CanExecute(object parameter)
        {
            return _canExecuteAction == null || _canExecuteAction();
        }

        /// <summary>
        ///     ���ݰ󶨵�ģ�͸��¿�ִ��״̬
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaiseCanExecuteChanged();
            RaisePropertyChanged(nameof(Visibility));
        }

        /// <summary>
        ///     ִ������
        /// </summary>
        public void Execute()
        {
            _executeAction();
        }

        /// <summary>
        ///     ������ִ��״̬�仯���¼�
        /// </summary>
        private void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     ������ִ��״̬�仯���¼�
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged();
        }
        #region �ܷ�ִ�д���

        private INotifyPropertyChanged _detect;

        /// <summary>
        ///     ����ִ��״̬�仯�Ķ���
        /// </summary>
        public INotifyPropertyChanged Detect
        {
            get => _detect;
            set
            {
                if (_detect != null)
                {
                    _detect.PropertyChanged += OnDetectPropertyChanged;
                }
                if (Equals(_detect, value))
                {
                    return;
                }
                _detect = value;
                value.PropertyChanged += OnDetectPropertyChanged;
                OnCanExecuteChanged();
            }
        }

        private void OnDetectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }

        #endregion
    }

    /// <summary>
    ///     �������ı�׼ί������
    /// </summary>
    /// <typeparam name="TParameter">��������</typeparam>
    public sealed class DelegateCommand<TParameter> : IStatusCommand
            where TParameter : class
    {
        #region ����

        /// <summary>
        ///     Ĭ������Ϊ��ļ��executeAction�ܷ�ִ��״̬�ķ���,Ŀ���Ƿ�ֹ������������ͬ���õ�Action�˷��ڴ�
        /// </summary>
        /// <returns></returns>
        public static readonly Func<TParameter, bool> DefaultCanExecuteAction = p => true;

        /// <summary>
        ///     ���executeAction�ܷ�ִ��״̬�ķ���
        /// </summary>
        private readonly Func<TParameter, bool> _canExecuteAction;

        /// <summary>
        ///     �������巽��
        /// </summary>
        private readonly Action<TParameter> _executeAction;

        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="executeAction">�������巽��</param>
        public DelegateCommand(Action<TParameter> executeAction)
            : this(executeAction, null, null)
        {
        }
        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="executeAction">�������巽��</param>
        /// <param name="model">���仯�¼���ģ��</param>
        public DelegateCommand(Action<TParameter> executeAction, TParameter model)
            : this(executeAction, model, null)
        {
        }
        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="executeAction">�������巽��</param>
        /// <param name="canExecuteAction">���executeAction�ܷ�ִ��״̬�ķ���</param>
        public DelegateCommand(Action<TParameter> executeAction, Func<TParameter, bool> canExecuteAction)
            : this(executeAction, null, canExecuteAction)
        {
        }
        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="executeAction">�������巽��</param>
        /// <param name="canExecuteAction">���executeAction�ܷ�ִ��״̬�ķ���</param>
        /// <param name="model">���仯�¼���ģ��</param>
        public DelegateCommand(Action<TParameter> executeAction, TParameter model, Func<TParameter, bool> canExecuteAction)
        {
            if (executeAction == null)
            {
                throw new ArgumentNullException("executeAction", @"���������Ϊ��");
            }
            _executeAction = executeAction;
            _canExecuteAction = canExecuteAction;
            Parameter = model;
            if (model is INotifyPropertyChanged changed)
            {
                changed.PropertyChanged += OnModelPropertyChanged;
            }
        }

        /// <summary>
        ///     ���ݰ󶨵�ģ�͸��¿�ִ��״̬
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaiseCanExecuteChanged();
            RaisePropertyChanged(nameof(Visibility));
        }

        #endregion

        #region �ܷ�ִ�д���

        private INotifyPropertyChanged _detect;

        /// <summary>
        ///     ����ִ��״̬�仯�Ķ���
        /// </summary>
        public INotifyPropertyChanged Detect
        {
            get => _detect;
            set
            {
                if (_detect != null)
                {
                    _detect.PropertyChanged += OnDetectPropertyChanged;
                }
                if (Equals(_detect, value))
                {
                    return;
                }
                _detect = value;
                value.PropertyChanged += OnDetectPropertyChanged;
                OnCanExecuteChanged();
            }
        }

        private void OnDetectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }

        #endregion
        #region ����

        private TParameter _parameter;
        /// <summary>
        ///     �������)
        /// </summary>
        /// <remarks>
        ///     ���Parameter��Ϊ��,ִ������ʱ���ԴӰ󶨴��ݵĲ���,ʹ��Parameter��Ϊ��������executeAction
        ///     ���ParameterΪ��,���֮ǰһ��,���Ӱ󶨴��ݵĲ���תΪTParameter����(ʹ��as)����executeAction
        /// </remarks>
        public TParameter Parameter
        {
            get => _parameter;
            set
            {
                if (_parameter == value)
                    return;
                
                _parameter = value;
                Detect= value as INotifyPropertyChanged;
                RaisePropertyChanged(nameof(Parameter));
            }
        }

        /// <summary>
        ///     �Ƿ���æ
        /// </summary>
        public bool IsBusy => false;

        /// <summary>
        ///     ��ǰ״̬
        /// </summary>
        public CommandStatus Status => CommandStatus.None;

        #endregion

        #region ����ʵ��

        /// <summary>
        ///     �����ڵ��ô�����ʱ���õķ�����
        /// </summary>
        /// <param name="parameter">�󶨴��ݵĲ���(���������Parameter����,������Ч)</param>
        /// <remarks>
        ///     ���Parameter��Ϊ��,ִ������ʱ���ԴӰ󶨴��ݵĲ���,ʹ��Parameter��Ϊ��������executeAction
        ///     ���ParameterΪ��,���֮ǰһ��,���Ӱ󶨴��ݵĲ���תΪTParameter����(ʹ��as)����executeAction
        /// </remarks>
        void ICommand.Execute(object parameter)
        {
            _executeAction(Parameter ?? parameter as TParameter);
        }

        /// <summary>
        ///     ��������ȷ���������Ƿ�������䵱ǰ״̬��ִ�еķ�����
        /// </summary>
        /// <returns>
        ///     �������ִ�д������Ϊ true������Ϊ false��
        /// </returns>
        /// <param name="parameter">������ʹ�õ����ݡ�����������Ҫ�������ݣ���ö����������Ϊ null��</param>
        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute(parameter as TParameter);
        }

        /// <summary>
        ///     ��ִ��״̬�仯���¼�
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        ///     �����ڵ��ô�����ʱ���õķ�����
        /// </summary>
        /// <param name="parameter">���ݵĲ���(��ʹ������Parameter����,Ҳʹ���������)</param>
        public void Execute(TParameter parameter)
        {
            _executeAction(parameter);
        }

        /// <summary>
        ///     ִ�з���
        /// </summary>
        /// <remarks>
        ///     ���Parameter��Ϊ��,ִ������ʱ���ԴӰ󶨴��ݵĲ���,ʹ��Parameter��Ϊ��������executeAction
        ///     ���ParameterΪ��,ʹ��null��Ϊ��������executeAction
        /// </remarks>
        public void Execute()
        {
            _executeAction(Parameter);
        }

        /// <summary>
        ///     ����Ƿ����ִ��
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(TParameter parameter)
        {
            return _canExecuteAction == null || _canExecuteAction(Parameter ?? parameter);
        }

        /// <summary>
        ///     ������ִ��״̬�仯���¼�
        /// </summary>
        private void OnCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        ///     ������ִ��״̬�仯���¼�
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged();
        }

        #endregion


        /// <summary>
        ///     �����޸��¼�(����Ϊ�ձ�ʾɾ��)
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                propertyChanged -= value;
                propertyChanged += value;
            }
            remove => propertyChanged -= value;
        }

        /// <summary>
        ///     �����޸��¼�
        /// </summary>
        private event PropertyChangedEventHandler propertyChanged;

        /// <summary>
        ///     ���������޸��¼�(������ֹģʽӰ��)
        /// </summary>
        /// <param name="propertyName">����</param>
        private void RaisePropertyChanged(string propertyName)
        {
            if (propertyChanged == null)
            {
                return;
            }
            RaisePropertyChangedInner(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     ���������޸��¼�
        /// </summary>
        /// <param name="args">����</param>
        private void RaisePropertyChangedInner(PropertyChangedEventArgs args)
        {
            try
            {
                propertyChanged?.Invoke(this, args);
            }
            catch (Exception ex)
            {
                LogRecorder.Exception(ex);
            }
        }
        private Visibility _visibility;

        /// <summary>
        ///     ����
        /// </summary>
        public Visibility Visibility
        {
            get => _visibility;
            set
            {
                if (_visibility == value)
                {
                    return;
                }
                _visibility = value;
                RaisePropertyChanged(nameof(Visibility));
            }
        }
    }
}
