using System;
using System.Collections;

namespace Gboxt.Common.WpfMvvmBase.Commands
{
    /// <summary>
    /// ��ʾһ������������
    /// </summary>
    public interface ICommandItemBuilder : ICommandItem
    {
        /// <summary>
        /// תΪ�������
        /// </summary>
        /// <returns>�������</returns>
        CommandItem ToCommand(object arg, Func<object, IEnumerator> enumerator);
    }
}