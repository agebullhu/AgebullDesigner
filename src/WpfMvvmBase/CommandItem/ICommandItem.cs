namespace Agebull.Common.Mvvm
{
    /// <summary>
    /// ��ʾһ������������
    /// </summary>
    public interface ICommandItem
    {
        /// <summary>
        ///     ����ʾΪ��ť
        /// </summary>
        bool NoButton
        {
            get;
        }

        /// <summary>
        ///     ����
        /// </summary>
        string Catalog
        {
            get;
            set;
        }
        /// <summary>
        ///     ֻ�ܵ�������
        /// </summary>
        bool Signle
        {
            get;
        }
        /// <summary>
        ///     Ŀ������
        /// </summary>
        string SourceType
        {
            get;
            set;
        }
        /// <summary>
        ///     ͼ��
        /// </summary>
        string IconName
        {
            get;
        }
    }
}