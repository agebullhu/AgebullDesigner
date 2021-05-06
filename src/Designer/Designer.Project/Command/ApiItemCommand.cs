using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;

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
            commands.Add(new CommandItemBuilder<ApiItem>
            {
                Catalog = "编辑",
                SignleSoruce = true,
                IsButton = true,
                Caption = "增加参数",
                Action = AddArgument,
                SoruceView = "api",
                IconName = "增加"
            });
            commands.Add(new CommandItemBuilder<ApiItem>
            {
                Catalog = "编辑",
                SignleSoruce = true,
                IsButton = true,
                Caption = "增加返回值",
                SoruceView = "api",
                Action = AddResult,
                IconName = "增加"
            });
        }

        public void AddResult(object arg)
        {
            if (arg is not ApiItem cls)
            {
                MessageBox.Show("返回值已存在");
                return;
            }
            var project = cls.Project;
            EntityConfig entity;

            if (string.IsNullOrWhiteSpace(cls.ResultArg))
                cls.ResultArg = $"{cls.Name}Response";
            entity = new EntityConfig
            {
                Project = project,
                Name = cls.ResultArg,
                Caption = $"{cls.Caption}返回值",
                Classify = cls.Name
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
            if (arg is not ApiItem cls)
            {
                MessageBox.Show("参数已存在");
                return;
            }
            var project = cls.Project;
            EntityConfig entity;
            if (string.IsNullOrWhiteSpace(cls.CallArg))
                cls.CallArg = $"{cls.Name}Argument";
            entity = new EntityConfig
            {
                Project = project,
                Name = cls.CallArg,
                Caption = $"{cls.Caption}请求参数",
                Classify = cls.Name
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