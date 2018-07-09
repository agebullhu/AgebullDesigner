using System;
using Agebull.EntityModel;

namespace Agebull.Common.Mvvm
{
    /// <summary>
    /// ��ʾһ������������
    /// </summary>
    public class CommandConfig : NotificationObject, ICommandItem
    {

        /// <summary>
        ///     ����
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        ///     ����
        /// </summary>
        public string Caption
        {
            get;
            set;
        }

        /// <summary>
        ///     ˵��
        /// </summary>
        public string Description
        {
            get;
            set;
        }
        /// <summary>
        ///     ����ʾΪ��ť
        /// </summary>
        public bool NoButton
        {
            get;
            set;
        }

        /// <summary>
        ///     ͼ��
        /// </summary>
        public string IconName
        {
            get;
            set;
        }

        /// <summary>
        ///     ViewModel
        /// </summary>
        public string ViewModel
        {
            get;
            set;
        }

        /// <summary>
        ///     ��Եı༭��
        /// </summary>
        public string Editor
        {
            get;
            set;
        }

        /// <summary>
        ///     ֻ�ܵ�������
        /// </summary>
        public bool Signle
        {
            get;
            set;
        }

        /// <summary>
        ///     ����
        /// </summary>
        public string Catalog
        {
            get;
            set;
        }


        /// <summary>
        ///     Ŀ������
        /// </summary>
        public Type SourceType
        {
            get;
            set;
        }

    }
}