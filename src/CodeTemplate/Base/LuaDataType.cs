namespace Agebull.CodeRefactor.CodeAnalyze
{
    /// <summary>
    /// LUA����������
    /// </summary>
    public enum LuaDataType
    {
        /// <summary>
        /// δ֪
        /// </summary>
        None,
        /// <summary>
        /// ��
        /// </summary>
        Void,
        /// <summary>
        /// ��
        /// </summary>
        Nil,
        /// <summary>
        /// ����
        /// </summary>
        Bool,
        /// <summary>
        /// ����
        /// </summary>
        Number,
        /// <summary>
        /// �ı�
        /// </summary>
        String,
        /// <summary>
        /// ��������
        /// </summary>
        Function,
        /// <summary>
        /// ������
        /// </summary>
        Table,
        /// <summary>
        /// ��������
        /// </summary>
        Iterator,
        /// <summary>
        /// ����Ӽ�
        /// </summary>
        Mulit,
        /// <summary>
        /// ��������
        /// </summary>
        BaseType,
        /// <summary>
        /// ����
        /// </summary>
        Confirm,
        /// <summary>
        /// ����
        /// </summary>
        Error
    }
}