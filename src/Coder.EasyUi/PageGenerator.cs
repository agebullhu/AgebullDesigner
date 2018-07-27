using System.IO;
using Agebull.Common;
using Agebull.EntityModel.Config;
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
            WebCode(path);
        }

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected override void CreateExCode(string path)
        {
            if (Entity.IsInternal || Entity.NoDataBase || Entity.DenyScope.HasFlag(AccessScopeType.Client))
                return;
            GlobalConfig.CheckPath(path, "Page");
            ActionCsCode(path);
            ExportCsCode(path);
        }

        private void WebCode(string path)
        {
            var file = ConfigPath(Entity, "File_Web_Index", path, $"{Entity.Parent.Name}\\{Entity.Name}", "Index.aspx");
            {
                var coder = new EasyUiIndexPageCoder
                {
                    Entity = Entity,
                    Project = Project
                };
                WriteFile(file, coder.Code());
            }
            file = ConfigPath(Entity, "File_Web_Action", path, $"{Entity.Parent.Name}\\{Entity.Name}", "Action.aspx");
            {
                WriteFile(file, ActionAspxCode());
            }
            file = ConfigPath(Entity, "File_Web_Export", path, $"{Entity.Parent.Name}\\{Entity.Name}", "Export.aspx");
            {
                WriteFile(file, ExportAspxCode());
            }
            file = ConfigPath(Entity, "File_Web_Form", path, $"{Entity.Parent.Name}\\{Entity.Name}", "Form.htm");
            {
                var coder = new EasyUiFormCoder
                {
                    Entity = Entity,
                    Project = Project
                };
                WriteFile(file, coder.Code());
            }
            file = ConfigPath(Entity, "File_Web_Item", path, $"{Entity.Parent.Name}\\{Entity.Name}", "Item.aspx");
            {
                var coder = new EasyUiItemCoder
                {
                    Entity = Entity,
                    Project = Project
                };
                WriteFile(file, coder.Code());
            }

            if (Entity.ListDetails)
            {
                file = ConfigPath(Entity, "File_Web_Details", path, $"{Entity.Parent.Name}\\{Entity.Name}", "Details.aspx");
                {
                    var coder = new EasyUiListDetailsPageCoder
                    {
                        Entity = Entity,
                        Project = Project
                    };
                    WriteFile(file, coder.Code());
                }
            }
            {
                var coder = new EasyUiPageScriptCoder
                {
                    Entity = Entity,
                    Project = Project
                };
                file = ConfigPath(Entity, "File_Web_Script_js", path, $"{Entity.Parent.Name}\\{Entity.Name}", "script.js");
                WriteFile(file, coder.Code());
                file = ConfigPath(Entity, "File_Web_Form_js", path, $"{Entity.Parent.Name}\\{Entity.Name}", "form.js");
                WriteFile(file, coder.FormJsCode());
                file = ConfigPath(Entity, "File_Web_Page_js", path, $"{Entity.Parent.Name}\\{Entity.Name}", "page.js");
                WriteFile(file, coder.PageJsCode());
            }
        }

        #endregion

        #region Action

        private void ActionCsCode(string path)
        {
            var file = ConfigPath(Entity, "File_Web_Action_cs", path, $"{Entity.Parent.Name}\\{Entity.Name}", "PageAction");
            var coder = new ApiActionCoder
            {
                Entity = Entity,
                Project = Project
            };
            GlobalConfig.CheckPaths(Path.GetDirectoryName(file));
            WriteFile(file + ".cs", coder.Code());
            WriteFile(file + ".Designer.cs", coder.BaseCode());
        }

        private string ActionAspxCode()
        {
            return $@"<%@ Page Language='C#' AutoEventWireup='true'  Inherits='{NameSpace}.{Entity.Name}Page.Action' %>";
        }

        #endregion

        #region Export

        private void ExportCsCode(string path)
        {
            var file = ConfigPath(Entity, "File_Web_Export_cs", path, $"Page\\{Entity.Parent.Name}\\{Entity.Name}", "Export.cs");
            var coder = new ExportActionCoder
            {
                Entity = Entity,
                Project = Project
            };
            WriteFile(file, coder.Code());
        }

        private string ExportAspxCode()
        {
            return $@"<%@ Page Language='C#' AutoEventWireup='true'  Inherits='{NameSpace}.{Entity.Name}Page.ExportAction' %>";
        }

        #endregion
    }
}