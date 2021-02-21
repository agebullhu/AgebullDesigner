using Agebull.EntityModel.Config;
using System;

namespace Agebull.Common.Mvvm
{
    /// <summary>
    /// ��ʾһ������������
    /// </summary>
    public interface ICommandItem : IKey
    {
        /// <summary>
        /// ���
        /// </summary>
        int Index
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
        ///     ������ʾΪ��ť
        /// </summary>
        bool CanButton
        {
            get;
            set;
        }

        /// <summary>
        ///     ����ʾΪ��ť
        /// </summary>
        bool NoButton { get; }

        /// <summary>
        ///     ��ʾͼ��
        /// </summary>
        bool OnlyIcon
        {
            get;
            set;
        }
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
        /// ��ǩ
        /// </summary>
        string Tag { get; set; }

        /// <summary>
        ///     ����ȷ��
        /// </summary>
        bool NoConfirm
        {
            get;
            set;
        }

    }
    public static class CommandItemHelper
    {

        /// <summary>
        /// ��Դ�и���
        /// </summary>
        /// <param name="dest">Ŀ��</param>
        /// <param name="sour">Դ</param>
        public static void CopyFrom(this ICommandItem dist, ICommandItem sour)
        {
            dist.Key = sour.Key ?? sour.GetHashCode().ToString();
            dist.NoConfirm = sour.NoConfirm;
            dist.Name = sour.Name;
            dist.Tag = sour.Tag;
            dist.Index = sour.Index;
            dist.CanButton = sour.CanButton;
            dist.Caption = sour.Caption;
            dist.Description = sour.Description;
            dist.IsButton = sour.IsButton;
            dist.SignleSoruce = sour.SignleSoruce;
            dist.Catalog = sour.Catalog;
            dist.WorkView = sour.WorkView;
            dist.SoruceView = sour.SoruceView;
            dist.TargetType = sour.TargetType;
            dist.IconName = sour.IconName;
            dist.OnlyIcon = sour.OnlyIcon;
            dist.ConfirmMessage = sour.ConfirmMessage;
        }
    }
}