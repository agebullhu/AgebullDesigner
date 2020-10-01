using System;
using System.Diagnostics;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder.VUE
{
    /// <summary>
    ///     页面代码生成器
    /// </summary>
    public class PorjectVUEGenerator<TModel> : CoderWithModel<TModel>
        where TModel : ProjectChildConfigBase, IEntityConfig
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
            if (Model.IsInternal || Model.NoDataBase || Model.DenyScope.HasFlag(AccessScopeType.Client))
                return;


            var file = ConfigPath(Model, "File_VUE_HTML", path, Model.PagePath(), "index.htm");

            var coder = new VueCoder<TModel>
            {
                Model = Model,
                Project = Project
            };
            try
            {
                var code =coder.HtmlCode();
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
            if (Model.IsInternal || Model.NoDataBase || Model.DenyScope.HasFlag(AccessScopeType.Client))
                return;

            var file = ConfigPath(Model, "File_VUE_JS", path, Model.PagePath(), "script.js");

            var coder = new VueCoder<TModel>
            {
                Model = Model,
                Project = Project
            };
            WriteFile(file, coder.ScriptCode());
        }

        #endregion
    }
}