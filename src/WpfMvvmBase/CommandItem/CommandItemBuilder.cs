using System;
using System.Collections;
using System.Windows.Input;

namespace Agebull.Common.Mvvm
{
    /// <summary>
    /// 表示一个命令生成器
    /// </summary>
    public class CommandItemBuilder : CommandConfig, ICommandItemBuilder
    {
        /// <summary>
        /// 命令
        /// </summary>
        public ICommand Command { get; set; }

        /// <summary>
        /// 转为命令对象
        /// </summary>
        /// <returns>命令对象</returns>
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