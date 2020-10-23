using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    ///     ��ʾһ������ڵ�
    /// </summary>
    public class IteratorCommandItem<TTargetType> : CommandItemBase
        where TTargetType : class
    {
        /// <summary>
        /// ����
        /// </summary>
        public IteratorCommandItem()
        {
            SignleSoruce = false;
            TargetType = typeof(TTargetType);
            Command = new DelegateCommand<object>(RunExecute);
        }

        /// <summary>
        ///     ��������
        /// </summary>
        public sealed override Type SuppertType => typeof(ConfigBase);


        /// <summary>
        /// ����
        /// </summary>
        public Action<TTargetType> Action;

        /// <summary>
        /// ִ��
        /// </summary>
        private void RunExecute(object arg)
        {
            if (DoConfirm && MessageBox.Show(ConfirmMessage ?? $"�Ƿ�ȷ��ִ�С�{Caption ?? Name}������", "����༭", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
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
        /// ִ��
        /// </summary>
        public override void Execute(object arg)
        {
            DoExecute(arg);
        }
        /// <summary>
        /// ִ��
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
                    MessageBox.Show($"����Ϊ�ջ���Ŀ������{typeof(TTargetType)}");
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