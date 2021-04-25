using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace Agebull.Common.Mvvm
{
    /// <summary>
    ///     �޲����ı�׼ί������
    /// </summary>
    public sealed class SimpleCommand : CommandBase, IStatusCommand
    {
        /// <summary>
        ///     �������巽��
        /// </summary>
        private readonly Action _executeAction;

        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="argument">����</param>
        /// <param name="executeAction">�������巽��</param>
        public SimpleCommand(Action executeAction)
        {
            _executeAction = executeAction;
        }

        bool ICommand.CanExecute(object parameter)
        {
            return !IsBusy;
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
            }
        }
    }
}