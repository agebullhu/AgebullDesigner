// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// ����:bull2
// ����:CodeRefactor-Agebull.Common.WpfMvvmBase
// ����:2014-11-27
// �޸�:2014-12-07
// *****************************************************/

#region ����

using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

#endregion

namespace Agebull.Common.Mvvm
{
    /// <summary>
    ///     �����ṩ״̬��Ϣ������
    /// </summary>
    public interface IStatus
    {
        /// <summary>
        ///     �Ƿ���æ
        /// </summary>
        bool IsBusy
        {
            get;
        }

        /// <summary>
        ///     ��ǰ״̬
        /// </summary>
        CommandStatus Status
        {
            get;
        }
        /// <summary>
        ///     ͼ��
        /// </summary>
        Visibility Visibility
        {
            get;
        }
    }
    /// <summary>
    ///     �����ṩ״̬��Ϣ������
    /// </summary>
    public interface IStatusCommand : ICommand, IStatus
    {
    }

    /// <summary>
    ///     ��ʾһ���첽����
    /// </summary>
    public interface IAsyncCommand : INotifyPropertyChanged, IStatusCommand
    {
    }
}
