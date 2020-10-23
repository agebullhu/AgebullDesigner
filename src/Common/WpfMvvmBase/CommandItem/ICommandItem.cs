using System;

namespace Agebull.Common.Mvvm
{
    /// <summary>
    /// ��ʾһ������������
    /// </summary>
    public interface ICommandItem
    {
        /// <summary>
        ///     ��ʶ
        /// </summary>
        Guid Id
        {
            get;
            set;
        }

        /// <summary>
        ///     ����
        /// </summary>
        string Name
        {
            get;
            set;
        }

        /// <summary>
        ///     ����
        /// </summary>
        string Caption
        {
            get;
            set;
        }

        /// <summary>
        ///     ˵��
        /// </summary>
        string Description
        {
            get;
            set;
        }
        /// <summary>
        ///     ��ʾΪ��ť
        /// </summary>
        bool IsButton
        {
            get;
            set;
        }
        /// <summary>
        ///     ����ʾΪ��ť
        /// </summary>
        bool NoButton { get; }

        /// <summary>
        ///     ����
        /// </summary>
        string Catalog { get; set; }

        /// <summary>
        ///     �༭�ӽ�
        /// </summary>
        string WorkView { get; set; }

        /// <summary>
        ///     �����ӽ�
        /// </summary>
        string SoruceView { get; set; }

        /// <summary>
        ///     ��Եı༭��
        /// </summary>
        string Editor
        {
            get;
            set;
        }

        /// <summary>
        ///     ֻ�ܵ�������
        /// </summary>
        bool SignleSoruce { get; set; }

        /// <summary>
        ///     Ŀ������
        /// </summary>
        Type TargetType { get; set; }

        /// <summary>
        ///     ͼ��
        /// </summary>
        string IconName { get; set; }

        /// <summary>
        /// ȷ����Ϣ
        /// </summary>
        string ConfirmMessage { get; set; }

        /// <summary>
        ///     ����ȷ��
        /// </summary>
        bool NoConfirm
        {
            get;
            set;
        }

    }

    /// <summary>
    /// ��ʾһ������������
    /// </summary>
    public static class ICommandItemExtend
    {
        /// <summary>
        /// ��Դ�и���
        /// </summary>
        /// <param name="dest">Ŀ��</param>
        /// <param name="sour">Դ</param>
        public static void CopyFrom(this ICommandItem dest, ICommandItem sour)
        {
            dest.Id = sour.Id;
            dest.NoConfirm = sour.NoConfirm;
            dest.Name = sour.Name;
            dest.Caption = sour.Caption;
            dest.Description = sour.Description;
            dest.IsButton = sour.IsButton;
            dest.SignleSoruce = sour.SignleSoruce;
            dest.Catalog = sour.Catalog;
            dest.WorkView = sour.WorkView;
            dest.SoruceView = sour.SoruceView;
            dest.TargetType = sour.TargetType;
            dest.IconName = sour.IconName;
            dest.ConfirmMessage = sour.ConfirmMessage;
        }
    }
}