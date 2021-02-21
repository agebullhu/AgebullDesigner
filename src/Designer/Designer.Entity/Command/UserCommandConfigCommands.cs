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
using System.Linq;

#endregion

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 导入导出相关模型
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal class UserCommandConfigCommands : DesignCommondBase<UserCommandConfig>
    {
        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            commands.Add(new CommandItemBuilder<UserCommandConfig>
            {
                Action = p => p.Parent?.Commands.Remove(p),
                Catalog = "编辑",
                SoruceView = "model",
                SignleSoruce = true,
                TargetType = typeof(UserCommandConfig),
                Caption = "删除命令",
                Editor = "Command",
                IconName = "删除"
            });
            commands.Add(new CommandItemBuilder<ModelConfig>
            {
                SignleSoruce = true,
                Action = AddCommand,
                Caption = "新增命令",
                Catalog = "编辑",
                IconName = "新增",
                SoruceView = "model",
                Editor = "Command",
                WorkView = "Model"
            });
            commands.Add(new CommandItemBuilder<ModelConfig>
            {
                SignleSoruce = true,
                Action = AddAuditCommand,
                Caption = "新增审核命令",
                Catalog = "编辑",
                Editor = "Command",
                IconName = "新增",
                SoruceView = "model",
                WorkView = "Model"
            });
        }

        /// <summary>
        /// 新增命令
        /// </summary>
        /// <param name="entity"></param>
        public void AddCommand(ModelConfig entity)
        {
            if (Model.CreateNew("新增命令", out UserCommandConfig config))
                entity.Add(config);
        }
        /// <summary>
        /// 新增审核命令
        /// </summary>
        /// <param name="entity"></param>
        public void AddAuditCommand(ModelConfig entity)
        {
            if (entity.Commands.Count != 0 && entity.Commands.Any(p => p.Name == "Pass"))
                return;
            entity.Add(new UserCommandConfig
            {
                Name = "Pass",
                Button = "btnPass",
                Caption = "审核通过",
                Description = "审核通过"
            });
            entity.Add(new UserCommandConfig
            {
                Name = "Deny",
                Button = "btnDeny",
                Caption = "拒绝通过",
                Description = "拒绝通过"
            });
        }
    }
}
