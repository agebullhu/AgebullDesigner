using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    ///     表示一个命令节点
    /// </summary>
    public class IteratorCommandItem<TTargetType> : CommandItemBase
        where TTargetType : class
    {
        /// <summary>
        /// 构造
        /// </summary>
        public IteratorCommandItem()
        {
            SignleSoruce = false;
            TargetType = typeof(TTargetType);
            Command = new DelegateCommand<object>(RunExecute);
        }

        /// <summary>
        ///     代替类型
        /// </summary>
        public sealed override Type SuppertType => typeof(ConfigBase);

        /// <summary>
        /// 动作
        /// </summary>
        public Action<TTargetType> Action;

        /// <summary>
        /// 执行
        /// </summary>
        private void RunExecute(object arg)
        {
            if (DoConfirm && MessageBox.Show(ConfirmMessage ?? $"是否确认执行【{Caption ?? Name}】操作", "对象编辑", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }
            if (OnPrepare != null && !OnPrepare(this))
            {
                ShowMessageBox("准备失败");
                return;
            }
            DoExecute(arg ?? GlobalConfig.CurrentConfig);
        }

        /// <summary>
        /// 执行
        /// </summary>
        public override void Execute(object arg)
        {
            DoExecute(arg);
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        private bool DoExecute(object para)
        {
            if (SignleSoruce)
            {
                if (!(para is TTargetType config))
                {
                    ShowMessageBox($"参数为空或不是目标类型{typeof(TTargetType)}");
                    return false;
                }

                try
                {
                    Trace.WriteLine($"执行命令：{Caption ?? Name}");
                    Action(config);
                    Trace.WriteLine($"执行完成：{Caption ?? Name}");
                    if (ShowResultMessage)
                        ShowMessageBox("执行成功");
                    return true;
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e, GetType().FullName);
                    ShowMessageBox($"发生异常：{e.Message}", Caption ?? Name);
                    return false;
                }
            }
            else if (para is ConfigBase config)
            {
                Task.Factory.StartNew(DoForeach, config);
            }
            return true;
        }

        void DoAction(TTargetType arg)
        {
            Trace.WriteLine($"=> {arg}");
            Action(arg);
        }

        void DoForeach(object arg)
        {
            try
            {
                var config = (ConfigBase)arg;
                Trace.WriteLine($"执行命令：{Caption ?? Name}");
                config.Preorder<TTargetType>(DoAction);
                Trace.WriteLine($"执行成功：{Caption ?? Name}");
                if (ShowResultMessage)
                    ShowMessageBox("执行成功");
            }
            catch (Exception e)
            {
                Trace.WriteLine(e, GetType().FullName);
                ShowMessageBox($"发生异常：{e.Message}");
            }
        }
    }
}