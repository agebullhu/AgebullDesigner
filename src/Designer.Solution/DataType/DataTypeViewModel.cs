// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// 建立:2014-11-20
// 修改:2014-11-27
// *****************************************************/

#region 引用

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.RobotCoder;

#endregion

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 命令注册器
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public sealed class DataTypeViewRegister : IAutoRegister
    {
        /// <summary>
        /// 注册代码
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            DesignerManager.Registe<SolutionConfig, DataTypePanel>("数据类型映射");
        }
    }

    /// <summary>
    /// 命令注册器
    /// </summary>
    public sealed class DataTypeViewModel : ExtendViewModelBase<DesignModelBase>
    {
        protected override NotificationList<CommandItemBase> CreateCommands()
        {
            return new NotificationList<CommandItemBase>
            {
                new CommandItem
                {
                    Name="New",
                    Caption = "新增数据类型",
                    IsButton=true,
                    Action = arg => Model.Context.Solution.DataTypeMap.Add(new DataTypeMapConfig())
                },
                new CommandItem
                {
                    Name="Sync",
                    Caption = "从C#语言解析字段数据类型",
                    IsButton=true,
                    Action = arg =>Model.Context.Solution.Foreach<PropertyConfig>(DataTypeHelper.CsDataType)
                },
                new CommandItem
                {
                    Name="Sync",
                    Caption = "标准化字段数据类型",
                    IsButton=true,
                    Action = arg=>Model.Context.Solution.Foreach<PropertyConfig>(DataTypeHelper.StandardDataType)
                }
            };
        }

    }
}