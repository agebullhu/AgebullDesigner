using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
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
                Catalog = "编辑",
                Signle = true,
                Caption = "增加新接口",
                Command = new DelegateCommand(AddApi),
                IconName = "tree_Open"
            });
            
            commands.Add(new CommandItemBuilder
            {
                Catalog = "编辑",
                NoButton = true,
                Caption = "取消实体的API",
                Command = new DelegateCommand(ClearEntityApi),
                IconName = "tree_Open"
            });
        }

        /// <summary>
        /// 取消所有实体的API
        /// </summary>
        public void ClearEntityApi()
        {
            Foreach(ClearApi);
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
            ApiItem api;
            if (!Model.CreateNew("新增接口方法",out api))
            {
                return;
            }
            api.Method = HttpMethod.POST;
            Context.SelectProject.Add(api);
            var arg = AddEntity("参数");
            api.CallArg = arg?.Name;
            var result = AddEntity("返回值");
            api.ResultArg = result?.Name;

        }

        public EntityConfig AddEntity(string title)
        {
            EntityConfig entity;
            if (!Model.CreateNew(title,out entity))
            {
                return null;
            }
            entity.IsClass = true;
            Context.SelectProject.Add(entity);
            Model.Tree.SetSelectEntity(entity);
            Context.SelectColumns = null;
            var nentity = CommandIoc.AddFieldsCommand();
            if (nentity != null)
                entity.Properties.AddRange(nentity.Properties);
            return entity;
        }
    }
}
