using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Media;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer
{
    internal class JsonViewModel : ExtendViewModelBase<JsonModel>
    {
        public JsonViewModel()
        {
            EditorName = "Json";
        }
    }

    internal class JsonModel : EntityDesignModel
    {
        #region 操作命令

        public override NotificationList<CommandItemBase> CreateCommands()
        {
            return new NotificationList<CommandItemBase>
            {
                new CommandItem
                {
                    Action = Format1,
                    IsButton=true,
                    Caption = "前后端名称一致(大驼峰名称)",
                    Image = Application.Current.Resources["tree_Assembly"] as ImageSource
                },
                new CommandItem
                {
                    Action = Format2,
                    IsButton=true,
                    Caption = "小驼峰名称",
                    Image = Application.Current.Resources["tree_Assembly"] as ImageSource
                },
                new CommandItem
                {
                    Action = Format3,
                    IsButton=true,
                    Caption = "小写下划线名称(C风格)",
                    Image = Application.Current.Resources["tree_Assembly"] as ImageSource
                }
            };
        }
        #endregion

        #region 扩展代码

        public void Format1(object arg)
        {
            foreach (var property in Context.SelectEntity.Properties)
            {
                property.JsonName = null;
            }
        }
        internal void Format2(object arg)
        {
            foreach (var property in Context.SelectEntity.Properties)
            {
                property.JsonName = property.Name.ToLWord();
            }
        }
        internal void Format3(object arg)
        {
            foreach (var property in Context.SelectEntity.Properties)
            {
                property.JsonName = CoderBase.ToLinkWordName(property.Name, "_", false);
            }
        }
        #endregion
    }
}