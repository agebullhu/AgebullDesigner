﻿// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 配置:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// 建立:2014-11-20
// 修改:2014-11-29
// *****************************************************/

#region 引用

using Agebull.Common;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

#endregion

namespace Agebull.EntityModel.Designer
{

    /// <summary>
    /// 导入导出相关模型
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal class UiCodeCommands : DesignCommondBase<IEntityConfig>
    {
        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            commands.Add(new CommandItemBuilder<IEntityConfig>
            {
                Action = PageName,
                Caption = "页面名称小驼峰",
                Catalog = "用户界面",
                Editor = "VUE",
                IconName = "tree_Open",
                SoruceView = "entity"
            });
            commands.Add(new CommandItemBuilder<IEntityConfig>
            {
                Action = ServiceName,
                Caption = "接口名称中划线",
                Catalog = "用户界面",
                Editor = "VUE",
                IconName = "tree_Open",
                SoruceView = "entity"
            });
            commands.Add(new CommandItemBuilder<IFieldConfig>
            {
                SignleSoruce = false,
                CanButton = true,
                WorkView = "adv",
                Catalog = "字段",
                Action = CheckJsonName,
                Caption = "Json名称小驼峰",
                IconName = "tree_item",
                Editor = "Json",
                ConfirmMessage = "确认执行【Json名称小驼峰】的操作吗?"
            });

        }

        public void CheckJsonName(IFieldConfig property)
        {
            property.JsonName = property.Name.ToLWord();
        }


        public void PageName(IEntityConfig entity)
        {
            entity.PageFolder = entity.Name.ToLWord();
        }


        public void ServiceName(IEntityConfig entity)
        {
            entity.ApiName = entity.Name.ToName('-');
        }

    }
}