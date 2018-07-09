using System.ComponentModel.Composition;
using Agebull.EntityModel;
using Agebull.EntityModel.Designer;
using Agebull.EntityModel.Config;

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
            DesignerManager.Registe<EntityConfig, FieldsViewModel>("字段基本", "Entity");
            DesignerManager.Registe<EntityConfig, CppFieldsViewModel>("C++字段", "Entity");
            DesignerManager.Registe<EntityConfig, RegularViewModel>("数据校验", "Entity");
            DesignerManager.Registe<EntityConfig, ModelViewModel>("实体模型", "Entity");
            DesignerManager.Registe<EntityConfig, JsonViewModel>("序列化设置", "Entity", "Model");
            DesignerManager.Registe<EntityConfig, AllFieldsViewModel>("所有字段", short.MaxValue + 1);
            

            CommandIoc.NewEntityCommand = NewEntityCommand;
            CommandIoc.AddFieldsCommand = AddFieldsCommand;
        }

        /// <summary>
        /// 新增实体的方法(UI后期实现)
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
        /// 新增实体的方法(UI后期实现)
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