using System;
using Agebull.EntityModel.Config;

namespace Agebull.Common.Mvvm
{
    /// <summary>
    /// ��ʾһ������������
    /// </summary>
    public interface ICommandItem
    {
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
        ///     ����ʾΪ��ť
        /// </summary>
        bool NoButton { get; set; }

        /// <summary>
        ///     ����
        /// </summary>
        string Catalog { get; set; }

        /// <summary>
        ///     �ӽ�
        /// </summary>
        string ViewModel { get; set; }

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
        bool Signle { get; set; }

        /// <summary>
        ///     Ŀ������
        /// </summary>
        Type SourceType { get; set; }

        /// <summary>
        ///     ͼ��
        /// </summary>
        string IconName { get; set; }
    }

    /// <summary>
    /// ��ʾһ������������
    /// </summary>
    public static class ICommandItemExtend
    {
        /// <summary>
        /// ����һ�����󲢸���
        /// </summary>
        /// <typeparam name="TCommandItem">ICommandItemʵ��</typeparam>
        /// <param name="sour">Դ</param>
        /// <returns></returns>
        public static TCommandItem CopyCreate<TCommandItem>(this ICommandItem sour) where TCommandItem : class, ICommandItem, new()
        {
            var item = new TCommandItem();
            CopyFrom(item, sour);
            return item;
        }
        /// <summary>
        /// ��Դ�и���
        /// </summary>
        /// <param name="dest">Ŀ��</param>
        /// <param name="sour">Դ</param>
        public static void CopyFrom(this ICommandItem dest, ICommandItem sour)
        {
            dest.Name = sour.Name ?? sour.Caption;
            dest.Caption = sour.Caption?? sour.Name;
            dest.Description = sour.Description;
            dest.NoButton = sour.NoButton;
            dest.Signle = sour.Signle;
            dest.Catalog = sour.Catalog;
            dest.ViewModel = sour.ViewModel;
            dest.SourceType = sour.SourceType;
            dest.IconName = sour.IconName;
        }
    }
}