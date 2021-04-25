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
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

#endregion

namespace Agebull.Common.Mvvm
{
    /// <summary>
    ///     �޲����ı�׼ί������
    /// </summary>
    public sealed class DelegateCommand : CommandBase, IStatusCommand
    {
        #region �շ���
        /// <summary>
        /// �շ���,�Ա�������һ��ʲôҲ����������
        /// </summary>
        private static void EmptyAction()
        {

        }
        private static DelegateCommand _emptyCommand;
        /// <summary>
        /// ������
        /// </summary>
        public static ICommand EmptyCommand => _emptyCommand ??= new DelegateCommand(EmptyAction);
        #endregion

        public object Tag { get; set; }



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
        ///     �����ڵ��ô�����ʱ���õķ�����
        /// </summary>
        /// <param name="parameter">������ʹ�õ����ݡ�����������Ҫ�������ݣ���ö����������Ϊ null��</param>
        void ICommand.Execute(object parameter)
        {
            try
            {
                Status = CommandStatus.Executing;
                _executeAction();
                Status = CommandStatus.Succeed;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                Status = CommandStatus.Faulted;
            };
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
            return !IsBusy &&( _canExecuteAction == null || _canExecuteAction());
        }

        /// <summary>
        ///     ���ݰ󶨵�ģ�͸��¿�ִ��״̬
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }

    }

    /// <summary>
    ///     �������ı�׼ί������
    /// </summary>
    /// <typeparam name="TParameter">��������</typeparam>
    public sealed class DelegateCommand<TParameter> :CommandBase, IStatusCommand
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
                Detect = value as INotifyPropertyChanged;
                RaisePropertyChanged(nameof(Parameter));
            }
        }


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
            try
            {
                Status = CommandStatus.Executing;
                _executeAction(Parameter ?? parameter as TParameter);
                Status = CommandStatus.Succeed;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                Status = CommandStatus.Faulted;
            }
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
            return !IsBusy && (_canExecuteAction == null || _canExecuteAction((parameter as TParameter) ?? Parameter));
        }

        #endregion

    }
}
