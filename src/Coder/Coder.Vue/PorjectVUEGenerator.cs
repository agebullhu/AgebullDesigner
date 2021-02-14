using Agebull.EntityModel.Config;
using System;
using System.Diagnostics;

namespace Agebull.EntityModel.RobotCoder.VUE
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
            var file = ConfigPath(Model as ConfigBase, "File_VUE_HTML", path, Model.PagePath(), "index.htm");

            var coder = new VueHtmlCoder
            {
                Model = Model,
                Project = Project
            };
            try
            {
                var code = coder.Card();
                WriteFile(file, code);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
        }

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected override void CreateCustomCode(string path)
        {
            var file = ConfigPath(Model as ConfigBase, "File_VUE_JS", path, Model.PagePath(), "script.js");

            var coder = new VueScriptCoder
            {
                Model = Model,
                Project = Project
            };
            WriteFile(file, coder.ScriptCode());
        }

        #endregion
    }
}