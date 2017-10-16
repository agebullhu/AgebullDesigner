using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows;
using System.Windows.Baml2006;
using System.Windows.Markup;
using System.Windows.Resources;
using System.Xml;
using Agebull.EntityModel;
using Agebull.EntityModel.Designer;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer.NewConfig;

namespace Agebull.Common.Config.Designer.EasyUi
{
    /// <summary>
    /// 关系连接检查
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class Register : IAutoRegister
    {
        /// <summary>
        /// 注册代码
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            var name = GetType().Assembly.FullName.Split(',')[0];
            //var file = Path.GetFileNameWithoutExtension();
            var uri = new Uri($"/{name};component/DataTemplate/ConfigDataTemplate.xaml", UriKind.Relative);

            StreamResourceInfo info = Application.GetResourceStream(uri);
            // ReSharper disable PossibleNullReferenceException
            if (info != null)
            {
                var asm = XamlReader.Load(new Baml2006Reader(info.Stream)) as ResourceDictionary;
                DataTemplateResource.RegistResource(asm);
            }

            GlobalTrigger.RegistTrigger<ConfigTrigger>();
            GlobalTrigger.RegistTrigger<ParentConfigTrigger>();
            GlobalTrigger.RegistTrigger<PropertyTrigger>();
            GlobalTrigger.RegistTrigger<EntityTrigger>();
            GlobalTrigger.RegistTrigger<ProjectTrigger>();
            GlobalTrigger.RegistTrigger<SolutionTrigger>();

            CommandIoc.NewConfigCommand = NewConfigCommand;
        }

        /// <summary>
        /// 生成新配置
        /// </summary>
        /// <returns></returns>
        public static bool NewConfigCommand(string title,ConfigBase config)
        {
            var window = new NewConfigWindow
            {
                Title = title 
            };
            var vm = (NewConfigViewModel)window.DataContext;
            vm.Config = config;
            if (window.ShowDialog() != true)
                return false;
            GlobalTrigger.OnCreate(vm.Config);
            return true;
        }
    }
}