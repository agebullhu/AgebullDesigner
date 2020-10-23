// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 配置:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// 建立:2014-11-20
// 修改:2014-11-29
// *****************************************************/

#region 引用

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.RobotCoder;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

#endregion

namespace Agebull.EntityModel.Designer
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class UiCommondModel : DesignCommondBase<EntityConfig>
    {
        /// <summary>
        /// 生成命令对象
        /// </summary>
        /// <param name="commands"></param>
        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            commands.AddRange(new[]
            {
                new CommandItemBuilder<EntityConfig>
                {
                    Catalog = "用户界面",
                    Caption = "UI快速组建",
                    Action = VueModel.CheckUi,
                    WorkView = "entity",
                    ConfirmMessage = "是否继续?"
                },
                new CommandItemBuilder<EntityConfig>
                {
                    Action =VueModel.CheckSimple,
                    Caption = "界面字段初始化",
                    WorkView = "entity",
                    Catalog = "用户界面",
                    ConfirmMessage = "是否继续?"
                }
            });
        }


        /*AuditDate AuditorId AuditState LastModifyDate LastReviserID AddDate AuthorID EntityType LinkId ParentId*/
    }
}
