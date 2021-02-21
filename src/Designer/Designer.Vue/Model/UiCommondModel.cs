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
            commands.AddRange(new ICommandItemBuilder[]
            {
                new CommandItemBuilder<IEntityConfig>
                {
                    Catalog = "用户界面",
                    Caption = "UI快速组建",
                    Action = VueModel.CheckUi,
                    WorkView = "entity",
                    CanButton=true,
                    ConfirmMessage = "是否继续?"
                },
                new CommandItemBuilder<IEntityConfig>
                {
                    Action =VueModel.CheckSimple,
                    Caption = "界面字段初始化",
                    WorkView = "entity",
                    Catalog = "用户界面",
                    CanButton=true,
                    ConfirmMessage = "是否继续?"
                },
                new CommandItemBuilder<IEntityConfig>
                {
                    Action = VueModel.CheckUiType,
                    CanButton=true,
                    Catalog = "用户界面",
                    Editor = "Vue",
                    WorkView = "entity",
                    Caption = "控件类型修复"
                },
                new CommandItemBuilder<IEntityConfig>
                {
                    Action = VueModel.CheckQuery,
                    CanButton=true,
                    Catalog = "用户界面",
                    WorkView = "entity",
                    Editor = "Vue",
                    Caption = "初始化查询字段"
                },
                new CommandItemBuilder<IEntityConfig>
                {
                    Action = VueModel.CheckKeyShow,
                    Catalog = "用户界面",
                    Editor = "Vue",
                    WorkView = "entity",
                    Caption = "隐藏主外键"
                },
                new CommandItemBuilder<IEntityConfig>
                {
                    Action = VueModel.CheckSizeByLen,

                    Catalog = "用户界面",
                    Editor = "Vue",
                    WorkView = "entity",
                    CanButton=true,
                    Caption = "按文字计算宽度",
                    ConfirmMessage = "是否继续?"
                },
                new CommandItemBuilder<IEntityConfig>
                {
                    Action = VueModel.CheckSizeAuto,
                    Catalog = "用户界面",
                    Editor = "Vue",
                    WorkView = "entity",
                    CanButton=true,
                    Caption = "自适应宽度",
                    ConfirmMessage = "是否继续?"
                },
                new CommandItemBuilder<IEntityConfig>
                {
                    Action = VueModel.CheckExport,
                    Caption = "导出导出初始化",
                    NoConfirm=true,
                    WorkView = "entity",
                    Catalog = "用户界面",
                    Editor = "Vue",
                    ConfirmMessage = "是否继续?"
                }
            });
        }


        /*AuditDate AuditorId AuditState LastModifyDate LastReviserID AddDate AuthorID EntityType LinkId ParentId*/
    }
}
