using System;
using System.Collections;

namespace Agebull.Common.Mvvm
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
        CommandItemBase ToCommand(object arg, Func<object, IEnumerator> enumerator);
    }
}