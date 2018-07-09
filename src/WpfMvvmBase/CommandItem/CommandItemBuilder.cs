using System;
using System.Collections;
using System.Windows.Input;

namespace Agebull.Common.Mvvm
{
    /// <summary>
    /// ��ʾһ������������
    /// </summary>
    public class CommandItemBuilder : CommandConfig, ICommandItemBuilder
    {
        /// <summary>
        /// ����
        /// </summary>
        public ICommand Command { get; set; }

        /// <summary>
        /// תΪ�������
        /// </summary>
        /// <returns>�������</returns>
        public CommandItem ToCommand(object arg, Func<object, IEnumerator> enumerator)
        {
            var item = this.CopyCreate<CommandItem>();
            item.Parameter = arg;
            item.Command = Command;
            return item;
        }
    }
}