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
            try
            {
                if (SignleSoruce)
                {
                    if (para is TTargetType config)
                    {
                        Action(config);
                        return true;
                    }
                    MessageBox.Show($"参数为空或不是目标类型{typeof(TTargetType)}");
                    return false;
                }
                else if (para is ConfigBase config)
                {
                    Task.Factory.StartNew(() => config.Foreach(Action));
                }
                return true;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e, GetType().FullName);
                return false;
            }
        }
    }

}