namespace Agebull.CodeRefactor.CodeAnalyze
{
    /// <summary>
    /// ģ�����ݽڵ�����
    /// </summary>
    public enum TemplateUnitType
    {
        None,
        /// <summary>
        /// �ĵ�ע��
        /// </summary>
        Rem,
        /// <summary>
        /// �ĵ�����
        /// </summary>
        Config,
        /// <summary>
        /// ת���ַ�
        /// </summary>
        Sharp,
        /// <summary>
        /// �����ı�
        /// </summary>
        Content,
        /// <summary>
        /// ������ֵ(������)
        /// </summary>
        SimpleValue,
        /// <summary>
        /// ����ֵ(������)
        /// </summary>
        Value,
        /// <summary>
        /// ��ͨ����
        /// </summary>
        Code
    }
}