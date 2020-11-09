using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Media;
using Agebull.Common;
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

    internal class JsonModel : ModelDesignModel
    {
        #region 操作命令

        /// <summary>
        /// 生成命令对象
        /// </summary>
        /// <returns></returns>
        public override void CreateCommands(IList<CommandItemBase> commands)
        {
            commands.Append(  new CommandItem
                {
                    Action = Format1,
                    IsButton=true,
                    Caption = "大驼峰(Json)",
                    Image = Application.Current.Resources["tree_Assembly"] as ImageSource
                },
                new CommandItem
                {
                    Action = Format2,
                    IsButton=true,
                    Caption = "小驼峰(Json)",
                    Image = Application.Current.Resources["tree_Assembly"] as ImageSource
                },
                new CommandItem
                {
                    Action = Format3,
                    IsButton=true,
                    Caption = "小写下划线(Json)",
                    Image = Application.Current.Resources["tree_Assembly"] as ImageSource
                },
                new CommandItem
                {
                    Action = Format4,
                    IsButton=true,
                    Caption = "大驼峰(API)",
                    Image = Application.Current.Resources["tree_Assembly"] as ImageSource
                },
                new CommandItem
                {
                    Action = Format5,
                    IsButton=true,
                    Caption = "小驼峰(API)",
                    Image = Application.Current.Resources["tree_Assembly"] as ImageSource
                },
                new CommandItem
                {
                    Action = Format6,
                    IsButton=true,
                    Caption = "小写下划线(API)",
                    Image = Application.Current.Resources["tree_Assembly"] as ImageSource
                },
                new CommandItem
                {
                    Action = Check,
                    IsButton=true,
                    Caption = "检查",
                    Image = Application.Current.Resources["tree_Assembly"] as ImageSource
                });
        }
        #endregion

        #region 扩展代码

        public void Format1(object arg)
        {
            foreach (var property in Context.SelectEntity.Properties)
            {
                property.JsonName = NameHelper.ToWordName(property.Name);
            }
        }
        internal void Format2(object arg)
        {
            foreach (var property in Context.SelectEntity.Properties)
            {
                property.JsonName = NameHelper.ToTfWordName(property.Name);
            }
        }
        internal void Format3(object arg)
        {
            foreach (var property in Context.SelectEntity.Properties)
            {
                property.JsonName = NameHelper.ToLinkWordName(property.Name, "_", false);
            }
        }
        #endregion


        #region 扩展代码

        public void Format4(object arg)
        {
            foreach (var property in Context.SelectEntity.Properties)
            {
                property.ApiArgumentName = NameHelper.ToWordName(property.Name);
            }
        }
        internal void Format5(object arg)
        {
            foreach (var property in Context.SelectEntity.Properties)
            {
                property.ApiArgumentName = NameHelper.ToTfWordName(property.Name);
            }
        }
        internal void Format6(object arg)
        {
            foreach (var property in Context.SelectEntity.Properties)
            {
                property.ApiArgumentName = NameHelper.ToLinkWordName(property.Name, "_", false);
            }
        }
        internal void Check(object arg)
        {
            foreach (var property in Context.SelectEntity.Properties)
            {
                property.NoneApiArgument = property.IsSystemField || property.IsPrimaryKey || property.InnerField || property.DbInnerField;

                if (property.NoneJson)
                    property.JsonName = null;
                else if (string.IsNullOrWhiteSpace(property.JsonName))
                    property.JsonName = property.Name;

                if (property.NoneApiArgument)
                    property.ApiArgumentName = null;
                else if(string.IsNullOrWhiteSpace(property.ApiArgumentName))
                    property.ApiArgumentName = property.JsonName;
            }
        }
        #endregion
    }
}