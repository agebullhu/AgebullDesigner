using System.ComponentModel.Composition;
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
            DesignerManager.Registe<EntityConfig, FieldsPanel>("�ֶλ���", "Entity");
            DesignerManager.Registe<EntityConfig, RegularPanel>("����У��", "Entity");
            DesignerManager.Registe<EntityConfig, ModelCodePanel>("ʵ��ģ��", "Entity");
            DesignerManager.Registe<EntityConfig, JsonPanel>("���л�����", "Entity", "Model");
            DesignerManager.Registe<EntityConfig, AllFieldsPanel>("�����ֶ�", short.MaxValue + 1);


            CommandIoc.EditEntityCommand = EditEntityCommand;
            CommandIoc.AddFieldsCommand = AddFieldsCommand;
        }

        /// <summary>
        /// ����ʵ��ķ���(UI����ʵ��)
        /// </summary>
        private static bool EditEntityCommand(EntityConfig entity)
        {
            var window = new NewEntityWindow();
            var vm = (NewEntityViewModel)window.DataContext;
            vm.IsNew = true;
            vm.Model.Entity = entity;
            vm.Title = "����ʵ��";
            var dl = window.ShowDialog();
            return dl != null && dl.Value;
        }

        /// <summary>
        /// �����ֶ�
        /// </summary>
        private static bool AddFieldsCommand(EntityConfig entity)
        {
            var window = new NewEntityWindow();
            var vm = (NewEntityViewModel)window.DataContext;
            vm.Model.Entity = entity;
            vm.Title = "�����ֶ�";
            var dl = window.ShowDialog();
            return dl != null && dl.Value;
        }
    }
}