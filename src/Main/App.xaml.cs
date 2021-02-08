using Agebull.Common.Configuration;
using Agebull.EntityModel.Designer.Card;
using BeetleX.FastHttpApi;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ZeroTeam.MessageMVC;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    ///     App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            EditorManager.Registe<ConfigForm>("基本信息", 255, "tree_Child4");
            EditorManager.Registe<CodePanel>("代码生成", 256, "img_code");
            EditorManager.Registe<DesignInfoPanel>("设计信息", 257, "img_check");
            EditorManager.Registe<PropertyPanel>("属性表格", 258, "img_ref");
            EditorManager.Registe<ExtendPanel>("扩展信息", 259, "img_check");
            EditorManager.Registe<TracePanel>("跟踪信息", 260, "ImgSubmit");

            AddInImporter.Importe();

            WorkContext.SynchronousContext = new DispatcherSynchronousContext
            {
                Dispatcher = Dispatcher
            };
            Trace.Listeners.Add(new MessageTraceListener());
            //ZeroApp.StartNoHost(services => services.AddMessageMvcFastHttpApi(), false, null);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            //ZeroFlowControl.Shutdown().Wait();
            base.OnExit(e);
        }
    }
}