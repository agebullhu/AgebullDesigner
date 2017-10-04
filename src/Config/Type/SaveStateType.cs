using System;

namespace Gboxt.Common.DataAccess.Schemas
{
    /// <summary>
    /// ����״̬����
    /// </summary>
    [Flags]
    public enum ConfigStateType
    {
        /// <summary>
        /// ��
        /// </summary>
        None = 0x0,
        /// <summary>
        /// ����
        /// </summary>
        /// <remarks>
        /// ��Դ�ǵ���ĵ���������,���ɸ���
        /// </remarks>
        IsReference = 0x1,
        /// <summary>
        /// ��ɾ��
        /// </summary>
        IsDelete = 0x2,
        /// <summary>
        /// ������
        /// </summary>
        IsFreeze = 0x4,
        /// <summary>
        /// �ѷ���
        /// </summary>
        IsDiscard = 0x8
    }
}