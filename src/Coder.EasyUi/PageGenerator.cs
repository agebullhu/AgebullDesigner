﻿using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder.EasyUi;

namespace Agebull.EntityModel.RobotCoder.AspNet
{
    /// <summary>
    ///     页面代码生成器
    /// </summary>
    public class PageGenerator : CoderWithEntity
    {
        #region 继承实现
        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_AspNet_Page";

        /// <summary>
        ///     生成基础代码
        /// </summary>
        protected override void CreateBaCode(string path)
        {
            if (Entity.IsInternal || Entity.NoDataBase || Entity.DenyScope.HasFlag(AccessScopeType.Client))
                return;
            var folder = !string.IsNullOrWhiteSpace(Entity.PageFolder)
                ? Entity.PageFolder
                    : string.IsNullOrWhiteSpace(Entity.Classify)
                        ? Entity.Name
                        : $"{Entity.Classify}\\{Entity.Name}";
            //file = ConfigPath(Entity, "File_Web_Action", path, folder, "Action.aspx");
            //{
            //    WriteFile(file, ActionAspxCode());
            //}
            //file = ConfigPath(Entity, "File_Web_Export", path, folder, "Export.aspx");
            //{
            //    WriteFile(file, ExportAspxCode());
            //}
            {
                var file = ConfigPath(Entity, "File_Web_Form", path, folder, "form.htm");
                {
                    var coder = new EasyUiFormCoder();
                    WriteFile(file, coder.BaseCode(Entity));
                }
            }
            //file = ConfigPath(Entity, "File_Web_Item", path, folder, "Item.aspx");
            //{
            //    var coder = new EasyUiItemCoder
            //    {
            //        Entity = Entity,
            //        Project = Project
            //    };
            //    WriteFile(file, coder.Code());
            //}

            //if (Entity.ListDetails)
            //{
            //    var file = ConfigPath(Entity, "File_Web_Details", path, folder, "Details.aspx");
            //    {
            //        var coder = new EasyUiListDetailsPageCoder();
            //        WriteFile(file, coder.BaseCode(Entity));
            //    }
            //}
            {
                var coder = new EasyUiScriptCoder();
                var file = ConfigPath(Entity, "File_Web_Script_js", path, folder, "script.js");
                WriteFile(file, coder.BaseCode(Entity));
                //file = ConfigPath(Entity, "File_Web_Form_js", path, folder, "form.js");
                //WriteFile(file, coder.FormJsCode());
                //file = ConfigPath(Entity, "File_Web_Page_js", path, folder, "page.js");
                //WriteFile(file, coder.PageJsCode());
            }
            {

                var file = ConfigPath(Entity, "File_Web_Index", path, folder, "index.htm");
                {
                    var coder = new EasyUiIndexPageCoder();
                    WriteFile(file, coder.BaseCode(Entity));
                }
            }
        }

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected override void CreateExCode(string path)
        {
        }

        #endregion

    }
}