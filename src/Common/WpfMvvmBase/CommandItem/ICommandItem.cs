using Agebull.EntityModel.Config;
using System;

namespace Agebull.Common.Mvvm
{
    /// <summary>
    /// 表示一个命令生成器
    /// </summary>
    public interface ICommandItem : IKey
    {
        /// <summary>
        /// 序号
        /// </summary>
        int Index
        {
            get;
            set;
        }
        /// <summary>
        ///     名称
        /// </summary>
        string Name
        {
            get;
            set;
        }
        /// <summary>
        ///     标题
        /// </summary>
        string Caption
        {
            get;
            set;
        }

        /// <summary>
        ///     说明
        /// </summary>
        string Description
        {
            get;
            set;
        }
        /// <summary>
        ///     显示为按钮
        /// </summary>
        bool IsButton
        {
            get;
            set;
        }

        /// <summary>
        ///     可以显示为按钮
        /// </summary>
        bool CanButton
        {
            get;
            set;
        }

        /// <summary>
        ///     不显示为按钮
        /// </summary>
        bool NoButton { get; }

        /// <summary>
        ///     显示图标
        /// </summary>
        bool OnlyIcon
        {
            get;
            set;
        }
        /// <summary>
        ///     分类
        /// </summary>
        string Catalog { get; set; }

        /// <summary>
        ///     编辑视角
        /// </summary>
        string WorkView { get; set; }

        /// <summary>
        ///     类型视角
        /// </summary>
        string SoruceView { get; set; }

        /// <summary>
        ///     面对的编辑器
        /// </summary>
        string Editor
        {
            get;
            set;
        }

        /// <summary>
        ///     只能单个操作
        /// </summary>
        bool SignleSoruce { get; set; }

        /// <summary>
        ///     目标类型
        /// </summary>
        Type TargetType { get; set; }

        /// <summary>
        ///     图标
        /// </summary>
        string IconName { get; set; }

        /// <summary>
        /// 确认消息
        /// </summary>
        string ConfirmMessage { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        string Tag { get; set; }

        /// <summary>
        ///     无需确认
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
        /// 从源中复制
        /// </summary>
        /// <param name="dest">目标</param>
        /// <param name="sour">源</param>
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