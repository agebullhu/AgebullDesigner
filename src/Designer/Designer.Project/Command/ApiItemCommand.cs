using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 扩展节点
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public sealed class ApiItemCommand : DesignCommondBase<ApiItem>
    {
        /// <summary>
        /// 生成命令对象
        /// </summary>
        /// <returns></returns>
        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            commands.Add(new CommandItemBuilder
            {
                Catalog = "编辑",
                SignleSoruce = true,
                IsButton = true,
                Caption = "增加参数",
                Action = AddArgument,
                SoruceView = "api",
                IconName = "tree_Open"
            });
            commands.Add(new CommandItemBuilder
            {
                Catalog = "编辑",
                SignleSoruce = true,
                IsButton = true,
                Caption = "增加返回值",
                SoruceView = "api",
                Action = AddResult,
                IconName = "tree_item"
            });
        }

        public void AddResult(object arg)
        {
            if (!(arg is ApiItem cls))
            {
                MessageBox.Show("返回值已存在");
                return;
            }
            var project = cls.Parent;
            EntityConfig entity;

            if (string.IsNullOrWhiteSpace(cls.ResultArg))
                cls.ResultArg = $"{cls.Name}Response";
            entity = new EntityConfig
            {
                Parent = project,
                Name = cls.ResultArg,
                Caption = $"{cls.Caption}返回值",
                Classify = cls.Name,
                NoDataBase = true
            };
            if (CommandIoc.EditEntityCommand(entity))
            {
                project.Add(entity);
                cls.Result = entity;
                return;
            }
            project.Remove(entity);
        }

        public void AddArgument(object arg)
        {
            if (!(arg is ApiItem cls))
            {
                MessageBox.Show("参数已存在");
                return;
            }
            var project = cls.Parent;
            EntityConfig entity;
            if (string.IsNullOrWhiteSpace(cls.CallArg))
                cls.CallArg = $"{cls.Name}Argument";
            entity = new EntityConfig
            {
                Parent = project,
                Name = cls.CallArg,
                Caption = $"{cls.Caption}请求参数",
                Classify = cls.Name,
                NoDataBase = true
            };
            if (CommandIoc.EditEntityCommand(entity))
            {
                project.Add(entity);
                cls.Argument = entity;
                return;
            }
            project.Remove(entity);
        }
    }
}