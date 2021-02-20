// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 配置:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// 建立:2014-11-20
// 修改:2014-11-29
// *****************************************************/

#region 引用

using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;

#endregion

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 导入导出相关模型
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal class EntityEditCommands : DesignCommondBase<EntityConfig>
    {
        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = AddNewProperty,
                Caption = "导入字段",
                SignleSoruce = true,
                IsButton = true,
                Catalog = "实体",
                IconName = "字段",
                SoruceView = "entity"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = CopyTable,
                Caption = "复制实体",
                SoruceView = "entity",
                IsButton = true,
                SignleSoruce = true,
                Catalog = "实体",
                IconName = "复制"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = DeleteEntity,
                Caption = "删除实体",
                SoruceView = "entity",
                IsButton = false,
                SignleSoruce = true,
                Catalog = "实体",
                IconName = "删除"
            });
        }


        public void DeleteEntity(EntityConfig entity)
        {
            if (entity == null ||
                MessageBox.Show($"确认删除{entity.Name}({entity.Caption})吗?", "对象编辑", MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes)
            {
                return;
            }
            entity.Project.Remove(entity);
        }


        public void CopyTable(EntityConfig entity)
        {
            Context.CopiedTable = new EntityConfig();
            Context.CopiedTable.Copy(entity);
            Context.CopiedTables.Add(Context.CopiedTable);
            RaisePropertyChanged(() => Context.CopiedTableCounts);
        }

        /// <summary>
        /// 新增属性
        /// </summary>
        /// <param name="entity"></param>
        public void AddNewProperty(EntityConfig entity)
        {
            CommandIoc.AddFieldsCommand(entity);
        }
    }
}
