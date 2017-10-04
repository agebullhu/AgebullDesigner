using System.IO;
using Gboxt.Common.DataAccess.Schemas;

namespace Agebull.Common.SimpleDesign.AspNet
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
            return;
            if (Entity.IsInternal || Entity.DenyScope.HasFlag(AccessScopeType.Client))
                return;
            WebCode(path);
        }

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected override void CreateExCode(string path)
        {
            return;
            if (Entity.IsInternal || Entity.DenyScope.HasFlag(AccessScopeType.Client))
                return;
            IOHelper.CheckPath(path, "Page");
            ActionCsCode(path);
            ExportCsCode(path);
        }

        private void WebCode(string path)
        {
            var folder = ConfigPath(path, "File_Web_Script", $"{Entity.Parent.Name}\\{Entity.Name}");
            var file = $"{folder}\\Index.aspx";
            {
                var coder = new EasyUiIndexPageCoder
                {
                    Entity = Entity,
                    Project = Project
                };
                WriteFile(file, coder.Code());
            }
            file = $"{folder}\\Action.aspx";
            {
                WriteFile(file, ActionAspxCode());
            }
            file = $"{folder}\\Export.aspx";
            {
                WriteFile(file, ExportAspxCode());
            }
            file = ConfigPath(path, "File_Web_Form", $"{Entity.Parent.Name}\\{Entity.Name}\\Form.htm");
            {
                var coder = new EasyUiFormCoder
                {
                    Entity = Entity,
                    Project = Project
                };
                WriteFile(file, coder.Code());
            }
            file = ConfigPath(path, "File_Web_Item", $"{Entity.Parent.Name}\\{Entity.Name}\\Item.aspx");
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
                file = ConfigPath(path, "File_Web_Details", $"{Entity.Parent.Name}\\{Entity.Name}\\Details.aspx");
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
                if (folder.Contains(".js"))
                    folder = Path.GetDirectoryName(folder);
                // ReSharper disable once PossibleNullReferenceException
                Entity["File_Web_Script"] = folder.Replace(path, "").Trim('\\');
                var coder = new EasyUiPageScriptCoder
                {
                    Entity = Entity,
                    Project = Project
                };
                WriteFile($"{folder}\\script.js", coder.Code());
                WriteFile($"{folder}\\form.js", coder.FormJsCode());
                WriteFile($"{folder}\\page.js", coder.PageJsCode());
            }
        }

        #endregion

        #region Action

        private void ActionCsCode(string path)
        {
            var file = Entity.TryGetExtendConfig("File_Model_Action", $"Page\\{Entity.Name}\\{Entity.Name}PageAction");
            var coder = new ApiActionCoder
            {
                Entity = Entity,
                Project = Project
            };
            IOHelper.CheckPaths(path, Path.GetDirectoryName(file));
            WriteFile(SetPath(path, file, ".cs"), coder.Code());
            WriteFile(SetPath(path, file, ".Designer.cs"), coder.BaseCode());
        }

        private string ActionAspxCode()
        {
            return $@"<%@ Page Language='C#' AutoEventWireup='true'  Inherits='{NameSpace}.{Entity.Name}Page.Action' %>";
        }

        #endregion

        #region Export

        private void ExportCsCode(string path)
        {
            var file = Entity["File_Model_Action"];
            var coder = new ExportActionCoder
            {
                Entity = Entity,
                Project = Project
            };
            path =IOHelper.CheckPaths(path, Path.GetDirectoryName(file));
            WriteFile(Path.Combine(path, "Export.cs"), coder.Code());
        }

        private string ExportAspxCode()
        {
            return $@"<%@ Page Language='C#' AutoEventWireup='true'  Inherits='{NameSpace}.{Entity.Name}Page.ExportAction' %>";
        }

        #endregion
    }
}