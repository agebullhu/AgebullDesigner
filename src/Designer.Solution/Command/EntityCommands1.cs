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
using System.IO;
using System.Windows;
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
                Action = SaveEntity,
                Caption = "保存所选对象",
                SignleSoruce = true,
                IsButton = true,
                Catalog = "编辑",
                ConfirmMessage= "确认强制保存吗?\n要知道这存在一定破坏性!",
                IconName = "tree_item"
            });

            commands.Add(new CommandItemBuilder<string, string>(ValidatePrepare, Validate, ValidateEnd)
            {
                TargetType = typeof(EntityConfig),
                Caption = "检查设计",
                Catalog = "工具",
                IconName = "tree_item"
            });

            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                SignleSoruce=true,
                Action = AddCommand,
                Caption = "新增命令",
                Catalog = "编辑",
                IconName = "tree_Open",
                ViewModel = "Model"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                SignleSoruce = true,
                Action = AddAuditCommand,
                Caption = "新增审核命令",
                Catalog = "编辑",
                IconName = "tree_Open",
                ViewModel = "Model"
            });
        }

        /// <summary>
        /// 强制保存
        /// </summary>
        public void SaveEntity(object arg)
        {
            ConfigWriter writer = new ConfigWriter
            {
                Solution = Context.Solution,
            };
            if (Context.SelectProject != null)
            {
                writer.SaveProject(Context.SelectProject, Path.GetDirectoryName(Context.Solution.SaveFileName));
                return;
            }
            var tables = Context.GetSelectEntities();
            foreach (var entity in tables)
            {
                writer.SaveEntity(entity, Path.GetDirectoryName(Context.Solution.SaveFileName),true);
            }
        }
    }
}
