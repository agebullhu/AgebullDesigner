using Agebull.EntityModel;
using System;

namespace Agebull.Common.Mvvm
{
    /// <summary>
    /// ��ʾһ������������
    /// </summary>
    public class CommandConfig : NotificationObject, ICommandItem
    {
        /// <summary>
        /// ��ʶ
        /// </summary>
        public string Key
        {
            get; 
            set;
        }

        /// <summary>
        ///     ����
        /// </summary>
        public string Name
        {
            get; set;
        }

        /// <summary>
        ///     ����
        /// </summary>
        public string Caption
        {
            get; set;
        }

        /// <summary>
        ///     ˵��
        /// </summary>
        public string Description
        {
            get; set;
        }

        /// <summary>
        ///     ��ʾΪ��ť
        /// </summary>
        public bool IsButton
        {
            get;
            set;
        }

        /// <summary>
        ///     ������ʾΪ��ť
        /// </summary>
        public bool CanButton
        {
            get;
            set;
        }

        /// <summary>
        ///     ����ʾΪ��ť
        /// </summary>
        public bool NoButton => !IsButton;

        /// <summary>
        ///     ͼ��
        /// </summary>
        public string IconName
        {
            get;
            set;
        }

        /// <summary>
        ///     �����ӽ�
        /// </summary>
        public string SoruceView
        {
            get;
            set;
        }

        /// <summary>
        ///     �����ӽ�
        /// </summary>
        public string WorkView
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
        public bool SignleSoruce
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

        private Type _targetType;
        /// <summary>
        ///     Ŀ������
        /// </summary>
        public Type TargetType
        {
            get => SignleSoruce ? _targetType : SuppertType ?? _targetType;
            set => _targetType = value;
        }


        /// <summary>
        ///     ��������
        /// </summary>
        public virtual Type SuppertType => null;

        /// <summary>
        /// ȷ����Ϣ
        /// </summary>
        public string ConfirmMessage { get; set; }

        /// <summary>
        /// ����ȷ��
        /// </summary>
        public bool NoConfirm { get; set; }

        /// <summary>
        /// ��ȷ��
        /// </summary>
        public bool DoConfirm => !string.IsNullOrWhiteSpace(ConfirmMessage);
    }
}