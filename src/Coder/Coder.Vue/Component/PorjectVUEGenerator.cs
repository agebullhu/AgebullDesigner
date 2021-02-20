using Agebull.Common;
using Agebull.EntityModel.Config;
using System.IO;

namespace Agebull.EntityModel.RobotCoder.VueComponents
{
    /// <summary>
    ///     页面代码生成器
    /// </summary>
    public class PorjectVUEGenerator : CoderWithModel
    {
        #region 继承实现
        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_VUE_JS";


        /// <summary>
        ///     生成基础代码
        /// </summary>
        protected override void CreateDesignerCode(string path)
        {
            var dir = IOHelper.CheckPath(SolutionConfig.Current.PagePath, Model.Project.PageFolder, Model.PageFolder);

            WriteFile(Path.Combine(dir, "index.css"), IndexComponent.CssCode(Model));
            WriteFile(Path.Combine(dir, "index.htm"), IndexComponent.HtmlCode(Model));
            WriteFile(Path.Combine(dir, "form.htm"), FormComponent.HtmlCode(Model));
            WriteFile(Path.Combine(dir, "list.htm"), ListComponent.HtmlCode(Model));

        }

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected override void CreateCustomCode(string path)
        {
            var dir = IOHelper.CheckPath(SolutionConfig.Current.PagePath, Model.Project.PageFolder, Model.PageFolder);
            WriteFile(Path.Combine(dir, "list.js"), ListComponent.JsCode(Model));
            WriteFile(Path.Combine(dir, "form.js"), FormComponent.JsCode(Model));
            WriteFile(Path.Combine(dir, "index.js"), IndexComponent.JsCode(Model));
            WriteFile(Path.Combine(dir, "common.js"), CommonComponent.JsCode(Model));
        }

        #endregion
    }
}