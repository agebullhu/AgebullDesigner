namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// ��ʾΪ����ڵ�
    /// </summary>
    public interface ICommandItem
    {
        /// <summary>
        /// APIԭʼ���������������
        /// </summary>
        string OrgArg { get; }

        /// <summary>
        /// �ͻ������������������
        /// </summary>
        string CurArg { get; }
        /*// <summary>
        /// ԭʼ��������
        /// </summary>
        string DefaultCode { get; }
        /// <summary>
        /// API��Ӧ�������
        /// </summary>
        string CommandId { get; }

        /// <summary>
        /// ��������(��ת��)
        /// </summary>
        bool LocalCommand { get; }*/

    }
}