using System.Diagnostics;
using System.Windows;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    ///     App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //GlobalTrigger.RegistTrigger<EntityTrigger>();
            //GlobalTrigger.RegistTrigger<PropertyTrigger>();

            WorkContext.SynchronousContext = new DispatcherSynchronousContext
            {
                Dispatcher = Dispatcher
            };
            Trace.Listeners.Add(new MessageTraceListener());
            //if (!VersionControlItem.Current.SvnLock())
            //{
            //    MessageBox.Show("文本正被别人锁定,请不要修改设计");
            //}
        }

        protected override void OnExit(ExitEventArgs e)
        {
            //VersionControlItem.Current.SvnUnlock();
            base.OnExit(e);
        }
    }
}