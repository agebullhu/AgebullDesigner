using System.Diagnostics;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    ///     App.xaml 的交互逻辑
    /// </summary>
    public partial class App
    {
        public App()
        {
            AddInImporter.Importe();
            WorkContext.SynchronousContext = new DispatcherSynchronousContext
            {
                Dispatcher = Dispatcher
            };
            Trace.Listeners.Add(new MessageTraceListener());
        }

    }
}