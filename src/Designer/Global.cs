using System.Diagnostics;
using System.Windows;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 全局对象
    /// </summary>
    public class Global
    {
        /// <summary>
        /// 全局初始化
        /// </summary>
        public static void Init()
        {
            RegistTrigger();

            CommandIocReal.Regist();
            var importer = new RegisterImporter();
            importer.AutoRegist();


            WorkContext.SynchronousContext = new DispatcherSynchronousContext
            {
                Dispatcher = Application.Current.Dispatcher
            };
            Trace.Listeners.Add(new MessageTraceListener());
            //if (!VersionControlItem.Current.SvnLock())
            //{
            //    MessageBox.Show("文本正被别人锁定,请不要修改设计");
            //}
        }
        /// <summary>
        /// 注册配置属性触发器
        /// </summary>
        static void RegistTrigger()
        {
            GlobalTrigger.RegistTrigger<ConfigTrigger>();
            GlobalTrigger.RegistTrigger<ParentConfigTrigger>();
            GlobalTrigger.RegistTrigger<PropertyTrigger>();
            GlobalTrigger.RegistTrigger<EntityTrigger>();
            GlobalTrigger.RegistTrigger<ProjectTrigger>();
            GlobalTrigger.RegistTrigger<SolutionTrigger>();
        }
    }
}
