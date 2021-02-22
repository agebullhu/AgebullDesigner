using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace Agebull.Common.Mvvm
{
    /// <summary>
    ///     无参数的标准委托命令
    /// </summary>
    public sealed class SimpleCommand : CommandBase, IStatusCommand
    {
        /// <summary>
        ///     命令主体方法
        /// </summary>
        private readonly Action _executeAction;

        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="argument">参数</param>
        /// <param name="executeAction">命令主体方法</param>
        public SimpleCommand(Action executeAction)
        {
            _executeAction = executeAction;
        }

        bool ICommand.CanExecute(object parameter)
        {
            return !IsBusy;
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
            }
        }
    }
}