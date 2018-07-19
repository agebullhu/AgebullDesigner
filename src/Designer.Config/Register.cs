using System.ComponentModel.Composition;
using Agebull.EntityModel.Designer;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer.NewConfig;

namespace Agebull.Common.Config.Designer.EasyUi
{
    /// <summary>
    /// ��ϵ���Ӽ��
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class Register : IAutoRegister
    {
        /// <summary>
        /// ע�����
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            //EditorExtend.RegistResource(GetType().Assembly,"DataTemplate/ConfigDataTemplate.xaml");

            CommandIoc.NewConfigCommand = NewConfigCommand;
        }

        /// <summary>
        /// ����������
        /// </summary>
        /// <returns></returns>
        public static bool NewConfigCommand(string title, ConfigBase config)
        {
            var window = new NewConfigWindow
            {
                Title = title 
            };
            var vm = (NewConfigViewModel)window.DataContext;
            vm.Config = config;
            return window.ShowDialog() == true;
        }
    }
}