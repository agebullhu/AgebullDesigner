namespace Gboxt.Common.DataAccess.Schemas
{
    /// <summary>
    /// ��ʾΪ����ڵ�
    /// </summary>
    public interface ICommandItem
    {
        /// <summary>
        /// API��Ӧ�������
        /// </summary>
        string CommandId { get; }

        /// <summary>
        /// APIԭʼ���������������
        /// </summary>
        string OrgArg { get; }

        /// <summary>
        /// �ͻ������������������
        /// </summary>
        string CurArg { get; }
        /// <summary>
        /// ԭʼ��������
        /// </summary>
        string DefaultCode { get; }
        /// <summary>
        /// ��������(��ת��)
        /// </summary>
        bool LocalCommand { get; }
        
    }
}