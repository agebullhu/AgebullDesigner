using System.ComponentModel.Composition;
using Agebull.EntityModel.Designer;
using Agebull.EntityModel.Config;

namespace Agebull.Common.Config.Designer.EasyUi
{
    /// <summary>
    /// 命令注册器
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
            EditorManager.Registe<ModelConfig, ModelReleasePanel>("关系连接");
            EditorManager.Registe<ModelConfig, ModelFieldsPanel>("连接字段", "Entity", "Model");

            EditorManager.Registe<EntityConfig, FieldsPanel>("字段");
            EditorManager.Registe<IEntityConfig, RegularPanel>("校验", "Model");
            EditorManager.Registe<IEntityConfig, ModelCodePanel>("对象", "Entity", "Model");

            CommandIoc.EditEntityCommand = EditEntityCommand;
            CommandIoc.AddFieldsCommand = AddFieldsCommand;
        }

        /// <summary>
        /// 新增实体的方法(UI后期实现)
        /// </summary>
        private static bool EditEntityCommand(EntityConfig entity)
        {
            var window = new NewEntityWindow();
            var vm = (NewEntityViewModel)window.DataContext;
            vm.IsNew = true;
            vm.Model.Entity = entity;
            vm.Title = "新增实体";
            var dl = window.ShowDialog();
            return dl != null && dl.Value;
        }

        /// <summary>
        /// 新增字段
        /// </summary>
        private static bool AddFieldsCommand(EntityConfig entity)
        {
            var window = new NewEntityWindow();
            var vm = (NewEntityViewModel)window.DataContext;
            vm.Model.Entity = entity;
            vm.Title = "新增字段";
            var dl = window.ShowDialog();
            return dl != null && dl.Value;
        }
    }
}