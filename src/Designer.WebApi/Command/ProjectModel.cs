using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 扩展节点
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class ApiCommonds : DesignCommondBase<ProjectConfig>
    {
        /// <summary>
        /// 生成命令对象
        /// </summary>
        /// <returns></returns>
        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            commands.Add(new CommandItemBuilder
            {
                IsButton = true,
                Catalog = "编辑",
                SignleSoruce = true,
                Caption = "增加新接口",
                Action = arg => AddApi(),
                IconName = "tree_Open"
            });
            
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Catalog = "编辑",
                Caption = "取消实体的API暴露",
                Action = ClearApi,
                IconName = "tree_Open"
            });
        }

        /// <summary>
        /// 取消实体的API
        /// </summary>
        public void ClearApi(EntityConfig entity)
        {
            entity.ExtendConfigListBool["NoApi"] = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public void AddApi()
        {
            if (Context.SelectProject == null)
            {
                MessageBox.Show("请选择一个项目");
                return;
            }

            if (!Model.CreateNew("新增接口方法",out ApiItem api))
            {
                return;
            }
            api.Method = HttpMethod.POST;
            Context.SelectProject.Add(api);
        }

    }
}
