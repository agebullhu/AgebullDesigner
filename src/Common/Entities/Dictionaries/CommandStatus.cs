// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// ����:bull2
// ����:CodeRefactor-Agebull.Common.WpfMvvmBase
// ����:2014-11-26
// �޸�:2014-12-07
// *****************************************************/

namespace Agebull.Common.Mvvm
{
    /// <summary>
    ///     ����״̬
    /// </summary>
    public enum CommandStatus
    {
        /// <summary>
        ///     δִ��
        /// </summary>
        None,

        /// <summary>
        ///     δִ��
        /// </summary>
        Disable,

        /// <summary>
        ///     �����쳣
        /// </summary>
        Faulted,

        /// <summary>
        ///     ִ����
        /// </summary>
        Executing,

        /// <summary>
        ///     ִ�����
        /// </summary>
        Succeed
    }
}
