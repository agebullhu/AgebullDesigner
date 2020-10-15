using System;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// �ַ������壬����ɫ��ʱ����Ϊһ�����崦��
    /// </summary>
    public struct ColorBlock
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="head">�ַ�����ͷ�������ַ�</param>
        /// <param name="tail">�ַ�����β�������ַ�</param>
        /// <param name="shiftStr">ת���ַ���</param>
        public ColorBlock(char head,char tail,string shiftStr)
        {
            QuotaHead = head;
            QuotaTail = tail;
            _ShiftStr = shiftStr;
        }
        /// <summary>
        /// �ַ�����ͷ�������ַ���
        /// </summary>
        public char QuotaHead;
        /// <summary>
        /// �ַ�����β�������ַ���
        /// </summary>
        public char QuotaTail;
        /// <summary>
        /// ת���ַ�����ֻ��Ϊ0��2���ַ�
        /// </summary>
        private string _ShiftStr;
        public string ShiftStr
        {
            get => _ShiftStr;
            set
            {
                if (value.Length > 2)
                {
                    throw new Exception ("ת���ַ���,ֻ��Ϊһ�����������ַ�");
                }
                _ShiftStr = value;
            }
        }
    }
}