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
            return new CommandItem
            {
                Name = Caption,
                Parameter = arg,
                IconName = IconName,
                SourceType = SourceType,
                Catalog = Catalog,
                Caption = Caption,
                Description = Description,
                NoButton = NoButton,
                Command = Command
            };
        }

    }
}