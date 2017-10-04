using Agebull.Common.DataModel;
using Agebull.Common.Designer.NewConfig;
using Agebull.Common.SimpleDesign.View;
using Gboxt.Common.DataAccess.Schemas;

namespace Agebull.CodeRefactor.SolutionManager
{
    /// <summary>
    /// 命令注入器
    /// </summary>
    public static class CommandIocReal
    {
        /// <summary>
        /// 依赖注册
        /// </summary>
        public static void Regist()
        {
            CommandIoc.NewEntityCommand = NewEntityCommand;
            CommandIoc.AddFieldsCommand = AddFieldsCommand;
            CommandIoc.EditPropertyEnumCommand = EditEnumCommand;
            CommandIoc.EditEnumCommand = EditEnumCommand;
            CommandIoc.NewConfigCommand = NewConfigCommand;
        }

        /// <summary>
        /// 生成新配置
        /// </summary>
        /// <returns></returns>
        public static bool NewConfigCommand(ConfigBase config)
        {
            var window = new NewConfigWindow();
            var vm = (NewConfigViewModel)window.DataContext;
            vm.Config = config;
            if (window.ShowDialog() != true)
                return false;
            GlobalTrigger.OnCreate(vm.Config);
            return true;
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
        /// <summary>
        /// 枚举编辑的方法(UI后期实现)
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
                Caption = property.Caption + "枚举类型",
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
        /// 枚举编辑的方法(UI后期实现)
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
