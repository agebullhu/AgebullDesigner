using System.ComponentModel.Composition;
using Agebull.EntityModel;
using Agebull.EntityModel.Designer;
using Agebull.EntityModel.Config;

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
            DesignerManager.Registe<EntityConfig, FieldsViewModel>("�ֶλ���", "Entity");
            DesignerManager.Registe<EntityConfig, CppFieldsViewModel>("C++�ֶ�", "Entity");
            DesignerManager.Registe<EntityConfig, RegularViewModel>("����У��", "Entity");
            DesignerManager.Registe<EntityConfig, ModelViewModel>("ʵ��ģ��", "Entity");
            DesignerManager.Registe<EntityConfig, JsonViewModel>("���л�����", "Entity", "Model");
            DesignerManager.Registe<EntityConfig, AllFieldsViewModel>("�����ֶ�", short.MaxValue + 1);
            

            CommandIoc.NewEntityCommand = NewEntityCommand;
            CommandIoc.AddFieldsCommand = AddFieldsCommand;
        }

        /// <summary>
        /// ����ʵ��ķ���(UI����ʵ��)
        /// </summary>
        private static EntityConfig NewEntityCommand()
        {
            var window = new NewEntityWindow();
            var vm = (NewEntityViewModel)window.DataContext;
            vm.IsNew = true;
            if (window.ShowDialog() != true)
            {
                return null;
            }
            GlobalTrigger.OnCreate(vm.Model.Entity);
            return vm.Model.Entity;
        }

        /// <summary>
        /// ����ʵ��ķ���(UI����ʵ��)
        /// </summary>
        private static EntityConfig AddFieldsCommand()
        {
            var window = new NewEntityWindow();
            var vm = (NewEntityViewModel)window.DataContext;
            if (window.ShowDialog() != true)
                return null;
            GlobalTrigger.OnCreate(vm.Model.Entity);
            return vm.Model.Entity;
        }
    }
}