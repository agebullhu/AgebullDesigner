using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;
using System.ComponentModel.Composition;

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
            EditorManager.Registe2<ModelConfig, ModelReleasePanel>("��ϵ����","����");
            EditorManager.Registe2<ModelConfig, ModelFieldsPanel>("�����ֶ�", "�ֶ�", "Entity", "Model");

            EditorManager.Registe2<EntityConfig, FieldsPanel>("�ֶ�", "�ֶ�");
            EditorManager.Registe2<IEntityConfig, RegularPanel>("У��", "���", "Model");
            EditorManager.Registe2<IEntityConfig, ModelCodePanel>("����", "C#", "Entity", "Model");

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