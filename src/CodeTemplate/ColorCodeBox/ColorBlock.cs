using System;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 字符串定义，在着色的时候，作为一个整体处理
    /// </summary>
    public struct ColorBlock
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="head">字符串的头部引号字符</param>
        /// <param name="tail">字符串的尾部引号字符</param>
        /// <param name="shiftStr">转义字符串</param>
        public ColorBlock(char head,char tail,string shiftStr)
        {
            QuotaHead = head;
            QuotaTail = tail;
            _ShiftStr = shiftStr;
        }
        /// <summary>
        /// 字符串的头部引号字符，
        /// </summary>
        public char QuotaHead;
        /// <summary>
        /// 字符串的尾部引号字符，
        /// </summary>
        public char QuotaTail;
        /// <summary>
        /// 转义字符串，只能为0到2个字符
        /// </summary>
        private string _ShiftStr;
        public string ShiftStr
        {
            get => _ShiftStr;
            set
            {
                if (value.Length > 2)
                {
                    throw new Exception ("转义字符串,只能为一个或者连个字符");
                }
                _ShiftStr = value;
            }
        }
    }
}