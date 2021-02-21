using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;
using System.ComponentModel.Composition;

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
            EditorManager.Registe2<ModelConfig, ModelReleasePanel>("关系连接","关联");
            EditorManager.Registe2<ModelConfig, ModelFieldsPanel>("连接字段", "字段", "Entity", "Model");

            EditorManager.Registe2<EntityConfig, FieldsPanel>("字段", "字段");
            EditorManager.Registe2<IEntityConfig, RegularPanel>("校验", "检查", "Model");
            EditorManager.Registe2<IEntityConfig, ModelCodePanel>("对象", "C#", "Entity", "Model");

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