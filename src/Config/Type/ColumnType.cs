// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// ����:bull2
// ����:CodeRefactor-Gboxt.Common.SimpleDataAccess
// ����:2014-12-03
// �޸�:2014-12-03
// *****************************************************/

#region ����

using System;

#endregion

namespace Agebull.EntityModel.Config
{
    /// <summary>
    ///     �ֶ�ʹ�÷�Χ����
    /// </summary>
    [Flags]
    public enum AccessScopeType
    {
        /// <summary>
        /// ������
        /// </summary>
        None = 0x0,
        /// <summary>
        /// �ͻ���
        /// </summary>
        Client = 0x1,
        /// <summary>
        /// �����
        /// </summary>
        Server = 0x2,
        /// <summary>
        /// ȫ����
        /// </summary>
        All = 0x3
    }

    /// <summary>
    /// �洢��������
    /// </summary>
    [Flags]
    public enum StorageScreenType
    {
        None=0x0,
        Insert=0x1,
        Update=0x2,
        All= Insert| Update
    }
}