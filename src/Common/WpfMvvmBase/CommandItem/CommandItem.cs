// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.Common.WpfMvvmBase
// 建立:2014-11-26
// 修改:2014-12-07
// *****************************************************/

#region 引用

using System;
using System.Diagnostics;
using System.Windows;

#endregion

namespace Agebull.Common.Mvvm
{
    /// <summary>
    ///     表示一个命令节点
    /// </summary>
    public class CommandItem : CommandItemBase
    {
        #region 命令

        public CommandItem()
        {
            SignleSoruce = true;
            Command = new DelegateCommand<object>(DoAction);
        }

        /// <summary>
        ///     对应的命令
        /// </summary>
        public Action<object> Action
        {
            get;
            set;
        }


        void DoAction(object arg)
        {
            if (DoConfirm && MessageBox.Show(ConfirmMessage ?? $"确认执行【{Caption ?? Name}】操作吗?", "对象编辑", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }
            if (OnPrepare != null && !OnPrepare(this))
            {
                Trace.WriteLine($"无法执行：{Caption ?? Name}");
            }
            Trace.WriteLine($"执行命令：{Caption ?? Name}");
            try
            {
                Action?.Invoke(arg);
                Trace.WriteLine("执行成功");
            }
            catch (Exception e)
            {
                Trace.WriteLine(e, GetType().FullName);
                Trace.WriteLine($"发生异常：{e.Message}");
            }
        }

        /// <summary>
        /// 执行
        /// </summary>
        public override void Execute(object arg)
        {
            Action?.Invoke(arg);
        }
        #endregion
    }

    /// <summary>
    ///     表示一个命令无参数节点
    /// </summary>
    public class SimpleCommandItem : CommandItemBase
    {
        #region 命令

        public SimpleCommandItem()
        {
            SignleSoruce = true;
            Command = new DelegateCommand<object>(DoAction);
        }

        /// <summary>
        ///     对应的命令
        /// </summary>
        public Action Action
        {
            get;
            set;
        }


        void DoAction(object arg)
        {
            if (DoConfirm && MessageBox.Show(ConfirmMessage ?? $"确认执行【{Caption ?? Name}】操作吗?", "对象编辑", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }
            if (OnPrepare != null && !OnPrepare(this))
            {
                Trace.WriteLine($"无法执行：{Caption ?? Name}");
            }
            Trace.WriteLine($"执行命令：{Caption ?? Name}");
            try
            {
                Action?.Invoke();
                Trace.WriteLine("执行成功");
            }
            catch (Exception e)
            {
                Trace.WriteLine(e, GetType().FullName);
                Trace.WriteLine($"发生异常：{e.Message}");
            }
        }

        /// <summary>
        /// 执行
        /// </summary>
        public override void Execute(object arg)
        {
            Action?.Invoke();
        }
        #endregion
    }

    /// <summary>
    ///     表示一个命令节点
    /// </summary>
    public class CommandItem<TArgument> : CommandItemBase
        where TArgument : class
    {
        #region 命令

        public CommandItem()
        {
            SignleSoruce = true;
            Command = new DelegateCommand(DoAction);
        }

        /// <summary>
        ///     对应的命令
        /// </summary>
        public Action<TArgument> Action
        {
            get;
            set;
        }


        /// <summary>
        ///     对应的命令
        /// </summary>
        public TArgument Argument => Source as TArgument;

        void DoAction()
        {
            if (DoConfirm && MessageBox.Show(ConfirmMessage ?? $"确认执行【{Caption ?? Name}】操作吗?", "对象编辑", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }
            if (OnPrepare == null || OnPrepare(this))
                Action?.Invoke(Argument);
        }

        /// <summary>
        /// 执行
        /// </summary>
        public override void Execute(object arg)
        {
            Action?.Invoke(arg as TArgument);
        }
        #endregion
    }
}