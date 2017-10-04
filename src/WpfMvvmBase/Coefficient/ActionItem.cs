// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// ����:bull2
// ����:CodeRefactor-Agebull.Common.WpfMvvmBase
// ����:2014-11-26
// �޸�:2014-12-07
// *****************************************************/

#region ����

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
    ///     ��ʾһ������ϵĽڵ�
    /// </summary>
    public class ActionItem : SimpleConfig, ICommandItemBuilder
    {

        //�̶�����
        public Dictionary<string, object> FixArguments = new Dictionary<string, object>();

        /// <summary>
        ///     ������ʼ
        /// </summary>
        public Func<object, bool> Begin { get; set; }

        /// <summary>
        ///     ʵ�ʲ���
        /// </summary>
        public Action<RuntimeActionItem, object> Action { get; set; }

        /// <summary>
        ///     ��������
        /// </summary>
        public Action<CommandStatus, Exception, bool> End { get; set; }


        /// <summary>
        ///     ����ʾΪ��ť
        /// </summary>
        public bool NoButton
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
        ///     ����ʹ�õ�Դ����,�ŷֿ�
        /// </summary>
        public string SourceType
        {
            get;
            set;
        }

        /// <summary>
        ///     ����
        /// </summary>
        public string Tag
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

        private ImageSource _image;


        /// <summary>
        ///     ͼ��
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
