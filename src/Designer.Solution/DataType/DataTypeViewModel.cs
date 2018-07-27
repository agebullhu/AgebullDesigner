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

#endregion

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 关系连接检查
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
    /// 关系连接检查
    /// </summary>
    public sealed class DataTypeViewModel : ExtendViewModelBase<DesignModelBase>
    {
        protected override ObservableCollection<CommandItemBase> CreateCommands()
        {

            return new ObservableCollection<CommandItemBase>
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
                    Action = CsDataType
                },
                new CommandItem
                {
                    Name="Sync",
                    Caption = "标准化字段数据类型",
                    IsButton=true,
                    Action = StandardDataType
                }
            };
        }
        void CsDataType(object arg)
        {
            Model.Context.Solution.Foreach<PropertyConfig>(CsDataType);
        }

        void CsDataType(PropertyConfig arg)
        {
            string name = arg.IsEnum ? "Enum" : arg.CsType;
            var dataType = Model.Context.Solution.DataTypeMap.FirstOrDefault(p =>
                string.Equals(p.CSharp, name, StringComparison.OrdinalIgnoreCase)) ??
                           Model.Context.Solution.DataTypeMap.FirstOrDefault(p =>
                               string.Equals(p.CSharp, "object", StringComparison.OrdinalIgnoreCase));
            if (dataType != null)
            {
                switch (arg.Parent.Parent.DbType)
                {
                    case DataBaseType.SqlServer:
                        arg.DbType = dataType.SqlServer;
                        break;
                    case DataBaseType.MySql:
                        arg.DbType = dataType.MySql;
                        break;
                }

                arg.DataType = dataType.Name;
                arg.CsType = dataType.CSharp;
                arg.CppType = dataType.Cpp;
                arg.Datalen = dataType.Datalen;
                arg.Scale = dataType.Scale;
            }
        }

        void StandardDataType(object arg)
        {
            Model.Context.Solution.Foreach<PropertyConfig>(StandardDataType);
        }

        void StandardDataType(PropertyConfig arg)
        {
            var dataType = Model.Context.Solution.DataTypeMap.FirstOrDefault(p =>
                string.Equals(p.Name, arg.DataType, StringComparison.OrdinalIgnoreCase));
            if (dataType == null)
            {
                Model.Context.Solution.DataTypeMap.Add(dataType = new DataTypeMapConfig
                {
                    Name = arg.DataType
                });
            }
            else
            {
                switch (arg.Parent.Parent.DbType)
                {
                    case DataBaseType.SqlServer:
                        arg.DbType = dataType.SqlServer;
                        break;
                    case DataBaseType.MySql:
                        arg.DbType = dataType.MySql;
                        break;
                }
                arg.DataType = dataType.Name;
                arg.CsType = dataType.CSharp;
                arg.CppType = dataType.Cpp;
                arg.Datalen = dataType.Datalen;
                arg.Scale = dataType.Scale;
            }
        }
    }
}