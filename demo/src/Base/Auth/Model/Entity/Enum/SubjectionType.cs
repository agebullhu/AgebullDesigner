namespace Agebull.Common.Organizations
{


    /// <summary>
    /// �м�Ȩ������
    /// </summary>
    /// <remark>
    /// �м�Ȩ������
    /// </remark>
    public enum SubjectionType
    {
        /// <summary>
        /// û���κ�Ȩ����
        /// </summary>
        None = 0x0,
        /// <summary>
        /// ���ޱ��˵�����
        /// </summary>
        Self = 0x1,
        /// <summary>
        /// �����ŵ�����
        /// </summary>
        Department = 0x2,
        /// <summary>
        /// �����ż��¼�������
        /// </summary>
        DepartmentAndLower = 0x3,
        /// <summary>
        /// �����������
        /// </summary>
        Company = 0x4,
        /// <summary>
        /// �������¼������벿�ŵ�����
        /// </summary>
        CompanyAndLower = 0x5,
        /// <summary>
        /// �Զ���
        /// </summary>
        Custom = 0x6,
    }
}