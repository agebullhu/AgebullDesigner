using System;

namespace Agebull.EntityModel.Config
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
        /// ��ɾ��
        /// </summary>
        Delete = 0x2,
        /// <summary>
        /// ������
        /// </summary>
        Freeze = 0x4,
        /// <summary>
        /// �ѷ���
        /// </summary>
        Discard = 0x8,
        /// <summary>
        /// ����
        /// </summary>
        /// <remarks>
        /// ��Դ���ö���,���ɸ���
        /// </remarks>
        Reference = 0x10,
        /// <summary>
        /// ����
        /// </summary>
        /// <remarks>
        /// Ԥ�������,���ɸ���
        /// </remarks>
        Predefined = 0x20,
        /// <summary>
        /// ����
        /// </summary>
        /// <remarks>
        /// ����
        /// </remarks>
        Lock = 0x10000
    }
}