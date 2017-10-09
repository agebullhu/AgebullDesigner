using System.ComponentModel.Composition;
using Agebull.EntityModel;
using Agebull.EntityModel.Designer;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer.View;

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
            CommandIoc.EditPropertyEnumCommand = EditEnumCommand;
            CommandIoc.EditEnumCommand = EditEnumCommand;
        }

        /// <summary>
        /// ö�ٱ༭�ķ���(UI����ʵ��)
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        private static EnumConfig EditEnumCommand(PropertyConfig property)
        {
            var window = new EnumEdit();
            var vm = (EnumEditViewModel)window.DataContext;

            vm.Model.Config = property.EnumConfig ?? new EnumConfig
            {
                LinkField = property.Key,
                Name = property.Name + "Type",
                Caption = property.Caption + "ö������",
                Description = property.Description
            };
            if (window.ShowDialog() != true)
            {
                return null;
            }
            GlobalTrigger.OnCreate(vm.Model.Config);
            return vm.Model.Config;
        }

        /// <summary>
        /// ö�ٱ༭�ķ���(UI����ʵ��)
        /// </summary>
        /// <param name="enumConfig"></param>
        /// <returns></returns>
        private static void EditEnumCommand(EnumConfig enumConfig)
        {
            var window = new EnumEdit();
            var vm = (EnumEditViewModel)window.DataContext;
            vm.Model.Config = enumConfig;
            window.ShowDialog();
        }
    }
}