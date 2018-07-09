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
    internal class UserCommandConfigCommands : ConfigCommands<UserCommandConfig>
    {
        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            commands.Add(new CommandItemBuilder
            {
                Command = new DelegateCommand<ConfigTreeItem<UserCommandConfig>>(p =>
                {
                    var par = p.Parent as ConfigTreeItem<EntityConfig>;
                    if (par == null)
                        return;
                    par.Model.Commands.Remove(p.Model);
                    par.Items.Remove(p);
                }),
                Catalog = "编辑",
                Signle = true,
                SourceType = typeof(UserCommandConfig),
                Caption = "删除命令",
                IconName = "img_del"
            });
        }
    }
}
