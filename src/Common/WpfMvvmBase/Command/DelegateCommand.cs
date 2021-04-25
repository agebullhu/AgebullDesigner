// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.Common.WpfMvvmBase
// 建立:2014-11-24
// 修改:2014-12-07
// *****************************************************/

#region 引用

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

#endregion

namespace Agebull.Common.Mvvm
{
    /// <summary>
    ///     无参数的标准委托命令
    /// </summary>
    public sealed class DelegateCommand : CommandBase, IStatusCommand
    {
        #region 空方法
        /// <summary>
        /// 空方法,以便于生成一个什么也不做的命令
        /// </summary>
        private static void EmptyAction()
        {

        }
        private static DelegateCommand _emptyCommand;
        /// <summary>
        /// 空命令
        /// </summary>
        public static ICommand EmptyCommand => _emptyCommand ??= new DelegateCommand(EmptyAction);
        #endregion

        public object Tag { get; set; }



        /// <summary>
        ///     默认总是为真的检测executeAction能否执行状态的方法,目的是防止构建无数的相同无用的Action浪费内存
        /// </summary>
        /// <returns></returns>
        public static readonly Func<bool> DefaultCanExecuteAction = () => true;
        /// <summary>
        ///     检测executeAction能否执行状态的方法
        /// </summary>
        private readonly Func<bool> _canExecuteAction;

        /// <summary>
        ///     命令主体方法
        /// </summary>
        private readonly Action _executeAction;

        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="executeAction">命令主体方法</param>
        public DelegateCommand(Action executeAction)
            : this(executeAction, DefaultCanExecuteAction)
        {
        }

        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="executeAction">命令主体方法</param>
        /// <param name="canExecuteAction">检测executeAction能否执行状态的方法</param>
        public DelegateCommand(Action executeAction, Func<bool> canExecuteAction)
        {
            if (executeAction == null || canExecuteAction == null)
            {
                throw new ArgumentNullException("executeAction", @"命令方法不能为空");
            }
            _executeAction = executeAction;
            _canExecuteAction = canExecuteAction;
        }

        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="executeAction">命令主体方法</param>
        /// <param name="canExecuteAction">检测executeAction能否执行状态的方法</param>
        /// <param name="model">侦测变化事件的模型</param>
        public DelegateCommand(Action executeAction, INotifyPropertyChanged model, Func<bool> canExecuteAction)
            : this(executeAction, canExecuteAction)
        {
            model.PropertyChanged += OnModelPropertyChanged;
        }

        /// <summary>
        ///     定义在调用此命令时调用的方法。
        /// </summary>
        /// <param name="parameter">此命令使用的数据。如果此命令不需要传递数据，则该对象可以设置为 null。</param>
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
        ///     定义用于确定此命令是否可以在其当前状态下执行的方法。
        /// </summary>
        /// <returns>
        ///     如果可以执行此命令，则为 true；否则为 false。
        /// </returns>
        /// <param name="parameter">此命令使用的数据。如果此命令不需要传递数据，则该对象可以设置为 null。</param>
        bool ICommand.CanExecute(object parameter)
        {
            return !IsBusy &&( _canExecuteAction == null || _canExecuteAction());
        }

        /// <summary>
        ///     根据绑定的模型更新可执行状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }

    }

    /// <summary>
    ///     带参数的标准委托命令
    /// </summary>
    /// <typeparam name="TParameter">参数类型</typeparam>
    public sealed class DelegateCommand<TParameter> :CommandBase, IStatusCommand
            where TParameter : class
    {
        #region 构造

        /// <summary>
        ///     默认总是为真的检测executeAction能否执行状态的方法,目的是防止构建无数的相同无用的Action浪费内存
        /// </summary>
        /// <returns></returns>
        public static readonly Func<TParameter, bool> DefaultCanExecuteAction = p => true;

        /// <summary>
        ///     检测executeAction能否执行状态的方法
        /// </summary>
        private readonly Func<TParameter, bool> _canExecuteAction;

        /// <summary>
        ///     命令主体方法
        /// </summary>
        private readonly Action<TParameter> _executeAction;

        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="executeAction">命令主体方法</param>
        public DelegateCommand(Action<TParameter> executeAction)
            : this(executeAction, null, null)
        {
        }
        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="executeAction">命令主体方法</param>
        /// <param name="model">侦测变化事件的模型</param>
        public DelegateCommand(Action<TParameter> executeAction, TParameter model)
            : this(executeAction, model, null)
        {
        }
        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="executeAction">命令主体方法</param>
        /// <param name="canExecuteAction">检测executeAction能否执行状态的方法</param>
        public DelegateCommand(Action<TParameter> executeAction, Func<TParameter, bool> canExecuteAction)
            : this(executeAction, null, canExecuteAction)
        {
        }
        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="executeAction">命令主体方法</param>
        /// <param name="canExecuteAction">检测executeAction能否执行状态的方法</param>
        /// <param name="model">侦测变化事件的模型</param>
        public DelegateCommand(Action<TParameter> executeAction, TParameter model, Func<TParameter, bool> canExecuteAction)
        {
            if (executeAction == null)
            {
                throw new ArgumentNullException("executeAction", @"命令方法不能为空");
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
        ///     根据绑定的模型更新可执行状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }

        #endregion

        #region 定义

        private TParameter _parameter;
        /// <summary>
        ///     命令参数)
        /// </summary>
        /// <remarks>
        ///     如果Parameter不为空,执行命令时忽略从绑定传递的参数,使用Parameter作为参数调用executeAction
        ///     如果Parameter为空,则和之前一致,将从绑定传递的参数转为TParameter类型(使用as)调用executeAction
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

        #region 基础实现

        /// <summary>
        ///     定义在调用此命令时调用的方法。
        /// </summary>
        /// <param name="parameter">绑定传递的参数(如果已设置Parameter属性,参数无效)</param>
        /// <remarks>
        ///     如果Parameter不为空,执行命令时忽略从绑定传递的参数,使用Parameter作为参数调用executeAction
        ///     如果Parameter为空,则和之前一致,将从绑定传递的参数转为TParameter类型(使用as)调用executeAction
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
        ///     定义用于确定此命令是否可以在其当前状态下执行的方法。
        /// </summary>
        /// <returns>
        ///     如果可以执行此命令，则为 true；否则为 false。
        /// </returns>
        /// <param name="parameter">此命令使用的数据。如果此命令不需要传递数据，则该对象可以设置为 null。</param>
        bool ICommand.CanExecute(object parameter)
        {
            return !IsBusy && (_canExecuteAction == null || _canExecuteAction((parameter as TParameter) ?? Parameter));
        }

        #endregion

    }
}
