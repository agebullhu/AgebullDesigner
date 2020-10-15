using System;
using System.Diagnostics;
using System.Windows;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;
using System.Threading.Tasks;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    ///     ��ʾһ������ڵ�
    /// </summary>
    public class IteratorCommandItem<TTargetType> : CommandItemBase
        where TTargetType : ConfigBase
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
                    if (!(para is TTargetType config))
                    {
                        MessageBox.Show($"����Ϊ�ջ���Ŀ������{typeof(TTargetType)}");
                        return false;
                    }
                    Action(config);
                }
                else
                {
                    if (!(para is ConfigBase config))
                    {
                        MessageBox.Show($"����Ϊ�ջ���Ŀ������{typeof(ConfigBase)}");
                        return false;
                    }
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