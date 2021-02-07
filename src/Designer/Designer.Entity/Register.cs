using System.ComponentModel.Composition;
using Agebull.EntityModel.Designer;
using Agebull.EntityModel.Config;

namespace Agebull.Common.Config.Designer.EasyUi
{
    /// <summary>
    /// ����ע����
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
            EditorManager.Registe<ModelConfig, ModelReleasePanel>("��ϵ����");
            EditorManager.Registe<ModelConfig, ModelFieldsPanel>("�����ֶ�", "Entity", "Model");

            EditorManager.Registe<EntityConfig, FieldsPanel>("�ֶ�");
            EditorManager.Registe<IEntityConfig, RegularPanel>("У��", "Model");
            EditorManager.Registe<IEntityConfig, ModelCodePanel>("����", "Entity", "Model");

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