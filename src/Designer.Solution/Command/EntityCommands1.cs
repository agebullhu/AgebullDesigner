// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 配置:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// 建立:2014-11-20
// 修改:2014-11-29
// *****************************************************/

#region 引用

using System.Collections.Generic;
using System.ComponentModel.Composition;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

#endregion

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 导入导出相关模型
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal class EntityCommands1 : ConfigCommands<EntityConfig>
    {
        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            commands.Add(new CommandItemBuilder
            {
                Command = new DelegateCommand(() => Model.ConfigIo.SaveEntity()),
                SourceType = typeof(EntityConfig),
                Caption = "强制实体",
                Signle = true,
                NoButton = true,
                Catalog = "编辑",
                IconName = "tree_item"
            });

            commands.Add(new CommandItemBuilder
            {
                SourceType = typeof(EntityConfig),
                Command = new AsyncCommand<string, string>(ValidatePrepare, Validate, ValidateEnd),
                Caption = "检查设计",
                Catalog = "工具",
                IconName = "tree_item"
            });

            commands.Add(new CommandItemBuilder
            {
                SourceType = typeof(EntityConfig),
                Command = new DelegateCommand<EntityConfig>(AddCommand),
                Caption = "新增命令",
                Catalog = "编辑",
                IconName = "tree_Open",
                ViewModel = "Model"
            });
            commands.Add(new CommandItemBuilder
            {
                SourceType = typeof(EntityConfig),
                Command = new DelegateCommand<EntityConfig>(AddAuditCommand),
                Caption = "新增审核命令",
                Catalog = "编辑",
                IconName = "tree_Open",
                ViewModel = "Model"
            });
        }
    }
}
