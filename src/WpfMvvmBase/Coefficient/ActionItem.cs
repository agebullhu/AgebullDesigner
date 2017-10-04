// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.Common.WpfMvvmBase
// 建立:2014-11-26
// 修改:2014-12-07
// *****************************************************/

#region 引用

using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Gboxt.Common.DataAccess.Schemas;

#endregion

namespace Gboxt.Common.WpfMvvmBase.Commands
{
    /// <summary>
    ///     表示一个命令集合的节点
    /// </summary>
    public class ActionItem : SimpleConfig, ICommandItemBuilder
    {

        //固定参数
        public Dictionary<string, object> FixArguments = new Dictionary<string, object>();

        /// <summary>
        ///     动作开始
        /// </summary>
        public Func<object, bool> Begin { get; set; }

        /// <summary>
        ///     实际操作
        /// </summary>
        public Action<RuntimeActionItem, object> Action { get; set; }

        /// <summary>
        ///     动作结束
        /// </summary>
        public Action<CommandStatus, Exception, bool> End { get; set; }


        /// <summary>
        ///     不显示为按钮
        /// </summary>
        public bool NoButton
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
        ///     可以使用的源类型,号分开
        /// </summary>
        public string SourceType
        {
            get;
            set;
        }

        /// <summary>
        ///     特征
        /// </summary>
        public string Tag
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

        private ImageSource _image;


        /// <summary>
        ///     图标
        /// </summary>
        public virtual ImageSource Image
        {
            get
            {
                return _image ?? Application.Current.Resources[IconName ?? "imgDefault"] as BitmapImage;
            }
            set
            {
                _image = value;
            }
        }

        CommandItem ICommandItemBuilder.ToCommand(object arg, Func<object, IEnumerator> enumerator)
        {
            var item = new RuntimeActionItem
            {
                Action = this,
                Parameter = arg,
                ToEnumerator = enumerator
            };

            return new CommandItem
            {
                Name = Caption,
                Parameter = arg,
                IconName = IconName,
                SourceType = SourceType,
                Catalog = Catalog,
                Caption = Caption,
                Description = Description,
                NoButton = NoButton,
                Image = _image,
                Command = new AsyncCommand<object, bool>(item.Prepare, item.Run, item.End)
            };
        }
    }
}
