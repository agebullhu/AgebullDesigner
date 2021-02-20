using Agebull.EntityModel.Designer.Card;
using BeetleX.FastHttpApi;
using System.Diagnostics;
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
            try
            {
                EditorManager.Registe<ConfigForm>("基本信息", 255, "基本信息");
                EditorManager.Registe<CodePanel>("代码生成", 256, "代码生成");
                EditorManager.Registe<DesignInfoPanel>("设计信息", 257, "设计信息");
                EditorManager.Registe<PropertyPanel>("属性表格", 258, "属性表格");
                EditorManager.Registe<ExtendPanel>("扩展信息", 259, "扩展信息");
                EditorManager.Registe<TracePanel>("跟踪信息", 260, "跟踪信息");

                WorkContext.SynchronousContext = new DispatcherSynchronousContext
                {
                    Dispatcher = Dispatcher
                };
                Trace.Listeners.Add(new MessageTraceListener());
                AddInImporter.Importe();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "启动异常");
            }

            ZeroApp.StartNoHost(services => services.AddMessageMvcFastHttpApi(), false, null);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            ZeroFlowControl.Shutdown().Wait();
            base.OnExit(e);
        }
    }
}