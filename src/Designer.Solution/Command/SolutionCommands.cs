﻿// /*****************************************************
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
using Agebull.Common;

#endregion

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 导入导出相关模型
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal class SolutionCommands : DesignCommondBase<SolutionConfig>
    {
        /// <summary>
        /// 注册代码
        /// </summary>
        protected sealed override void DoRegist()
        {
            WordMap.Prepare();
        }
        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            commands.Add(new CommandItemBuilder
            {
                Catalog = "文件",
                SignleSoruce = false,
                SoruceView = "project",
                Caption = "重新载入名称字典",
                Action = p=> WordMap.Prepare(),
                IconName = "img_add"
            });
            commands.Add(new CommandItemBuilder
            {
                Catalog = "编辑",
                SoruceView = "project",
                IsButton = true,
                SignleSoruce = true,
                Caption = "新增项目",
                Action = AddProject,
                IconName = "img_add"
            }); 
        }

        /// <summary>
        /// 新增项目
        /// </summary>
        public void AddProject(object arg)
        {
            if (Model.CreateNew("新增项目", out ProjectConfig config))
                Context.Solution.Add(config);
        }
    }
}
