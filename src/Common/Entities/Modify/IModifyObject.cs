// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.Common.WpfMvvmBase
// 建立:2014-12-09
// 修改:2014-12-09
// *****************************************************/

#region 引用


#endregion

using System;

namespace Agebull.EntityModel
{

    /// <summary>
    ///     有属性通知的对象
    /// </summary>
    public interface IModifyObject
    {
        public bool IsModify { get; }

        void ResetModify(bool isSaved);
        void SetIsNew();

        void CheckModify();

        event IsModifyEventHandler IsModifyChanged;

        ModifyRecord ValueRecords { get; }
    }


    //
    // 摘要:
    //     Provides data for the System.ComponentModel.INotifyPropertyChanged.PropertyChanged
    //     event.
    public class IsModifyEventArgs : EventArgs
    {
        public IsModifyEventArgs(bool isModify) => IsModify = isModify;

        public bool IsModify { get; }
    }

    public delegate void IsModifyEventHandler(object sender, IsModifyEventArgs e);
}
