using System;
using Agebull.EntityModel;

namespace Agebull.Common.Mvvm
{
    /// <summary>
    /// 表示一个命令生成器
    /// </summary>
    public class CommandConfig : NotificationObject, ICommandItem
    {

        /// <summary>
        ///     名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        ///     标题
        /// </summary>
        public string Caption
        {
            get;
            set;
        }

        /// <summary>
        ///     说明
        /// </summary>
        public string Description
        {
            get;
            set;
        }
        /// <summary>
        ///     不显示为按钮
        /// </summary>
        public bool NoButton
        {
            get;
            set;
        }

        /// <summary>
        ///     图标
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
        ///     面对的编辑器
        /// </summary>
        public string Editor
        {
            get;
            set;
        }

        /// <summary>
        ///     只能单个操作
        /// </summary>
        public bool Signle
        {
            get;
            set;
        }

        /// <summary>
        ///     分类
        /// </summary>
        public string Catalog
        {
            get;
            set;
        }


        /// <summary>
        ///     目标类型
        /// </summary>
        public Type SourceType
        {
            get;
            set;
        }

    }
}