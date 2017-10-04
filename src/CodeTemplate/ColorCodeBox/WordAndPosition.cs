namespace Agebull.CodeRefactor.CodeTemplate
{
    /// <summary>
    ///   ƥ�䵽�ĵ��ʵĽṹ��
    /// </summary>
    public class WordAndPosition
    {
        /// <summary>
        ///  ����:0 һ��,1�ַ���,2ע��,3��XML��Ϊ����ͷ,4��XML��Ϊ����β,5Ϊ����β,6Ϊ=��,7Ϊ����,8Ϊֵ
        /// </summary>
        public int Type;

        /// <summary>
        ///   ƥ��ĵ���
        /// </summary>
        public string Word;

        /// <summary>
        ///   ��ʼλ��
        /// </summary>
        public int Position;

        /// <summary>
        ///   ���ʳ���
        /// </summary>
        public int Length;

        /// <summary>
        ///   �Ƿ��ı�
        /// </summary>
        public bool IsString;

        /// <summary>
        /// Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> containing a fully qualified type name.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"{Word} - {Position} - {Length}";
        }
    }
}