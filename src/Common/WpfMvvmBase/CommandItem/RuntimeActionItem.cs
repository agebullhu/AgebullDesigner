using System;
using System.Collections;
using System.Windows;
using Agebull.EntityModel;

namespace Agebull.Common.Mvvm
{
    ///// <summary>
    /////     ��ʾһ������ϵĽڵ�
    ///// </summary>
    //public class RuntimeActionItem : NotificationObject
    //{
    //    /// <summary>
    //    ///     ��������
    //    /// </summary>
    //    public ActionItem Action { get; set; }

    //    public Func<object, IEnumerator> ToEnumerator { get; set; }

    //    /// <summary>
    //    ///     ��Ӧ���������
    //    /// </summary>
    //    public object Parameter
    //    {
    //        get;
    //        set;
    //    }

    //    /// <summary>
    //    /// ִ��
    //    /// </summary>
    //    public bool Prepare(object arg)
    //    {
    //        if (!NoConfirm && MessageBox.Show($"ȷ��Ҫִ��{Action.Caption}��?", "����༭", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
    //        {
    //            return false;
    //        }
    //        if (Action.Begin == null)
    //            return true;
    //        if (ToEnumerator == null)
    //        {
    //            return Action.Begin.Invoke(arg);
    //        }
    //        bool success = true;
    //        var ie = ToEnumerator(arg);
    //        while (ie.MoveNext())
    //        {
    //            if (!Action.Begin.Invoke(ie.Current))
    //                success = false;
    //        }
    //        return success;
    //    }

    //    /// <summary>
    //    /// ִ��
    //    /// </summary>
    //    public bool Run(object arg)
    //    {
    //        if (ToEnumerator == null)
    //        {
    //            Action.Action?.Invoke(this, arg);
    //        }
    //        else
    //        {
    //            var ie = ToEnumerator(arg);
    //            while (ie.MoveNext())
    //                Action.Action?.Invoke(this, ie.Current);
    //        }
    //        return true;
    //    }
    //    /// <summary>
    //    /// ִ��
    //    /// </summary>
    //    public void End(CommandStatus status, Exception exception, bool arg)
    //    {
    //        Action.End?.Invoke(status, exception, arg);
    //    }
    //}
}