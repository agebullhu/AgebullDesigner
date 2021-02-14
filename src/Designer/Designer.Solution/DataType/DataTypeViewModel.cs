// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// 建立:2014-11-20
// 修改:2014-11-27
// *****************************************************/

#region 引用

using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder;
using System.Collections.Generic;

#endregion

namespace Agebull.EntityModel.Designer
{

    /// <summary>
    /// 命令注册器
    /// </summary>
    public sealed class DataTypeViewModel : EditorViewModelBase<DesignModelBase>
    {
        public override void CreateCommands(IList<CommandItemBase> commands)
        {
            commands.Append(new CommandItem
            {
                Name = "New",
                Caption = "新增数据类型",
                IsButton = true,
                Action = arg => Model.Context.Solution.DataTypeMap.Add(new DataTypeMapConfig())
            },
            new CommandItem
            {
                Name = "Sync",
                Caption = "从C#语言解析字段数据类型",
                IsButton = true,
                Action = arg => Model.Context.Solution.Foreach<FieldConfig>(DataTypeHelper.CsDataType)
            },
            new CommandItem
            {
                Name = "Sync",
                Caption = "标准化字段数据类型",
                IsButton = true,
                Action = arg => Model.Context.Solution.Foreach<FieldConfig>(DataTypeHelper.StandardDataType)
            });
            base.CreateCommands(commands);
        }

    }
}