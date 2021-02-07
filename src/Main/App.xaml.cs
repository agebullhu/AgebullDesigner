using Agebull.EntityModel.Designer.Card;
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
        }

    }
}