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
using Agebull.EntityModel.Config;

#endregion

namespace Agebull.Common.Mvvm
{
    ///// <summary>
    /////     ��ʾһ������ϵĽڵ�
    ///// </summary>
    //public class ActionItem : CommandConfig, ICommandItemBuilder
    //{

    //    //�̶�����
    //    public Dictionary<string, object> FixArguments = new Dictionary<string, object>();

    //    /// <summary>
    //    ///     ������ʼ
    //    /// </summary>
    //    public Func<object, bool> Begin { get; set; }

    //    /// <summary>
    //    ///     ʵ�ʲ���
    //    /// </summary>
    //    public Action<RuntimeActionItem, object> Action { get; set; }

    //    /// <summary>
    //    ///     ��������
    //    /// </summary>
    //    public Action<CommandStatus, Exception, bool> End { get; set; }


    //    private ImageSource _image;


    //    /// <summary>
    //    ///     ͼ��
    //    /// </summary>
    //    public virtual ImageSource Image
    //    {
    //        get => _image ?? (IconName == null ? null : Application.Current.Resources[IconName] as BitmapImage);
    //        set => _image = value;
    //    }

    //    CommandItemBase ICommandItemBuilder.ToCommand(object arg, Func<object, IEnumerator> enumerator)
    //    {
    //        var item = new RuntimeActionItem
    //        {
    //            Action = this,
    //            Parameter = arg,
    //            ToEnumerator = enumerator
    //        };
    //        var r2 = new AsyncCommandItem<object, bool>(item.Prepare, item.Run, item.End);
    //        r2.CopyFrom(this);
    //        r2.Image = Image;
    //        r2.Source = arg;
    //        return r2;
    //    }
    //}
}
