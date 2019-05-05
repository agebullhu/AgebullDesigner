using System;
using System.Diagnostics;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder.VUE
{
    /// <summary>
    ///     页面代码生成器
    /// </summary>
    public class PorjectVUEGenerator : CoderWithEntity
    {
        #region 继承实现
        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_VUE_JS";

        /// <summary>
        ///     生成基础代码
        /// </summary>
        protected override void CreateBaCode(string path)
        {
            if (Entity.IsInternal || Entity.NoDataBase || Entity.DenyScope.HasFlag(AccessScopeType.Client))
                return;

            var folder = !string.IsNullOrWhiteSpace(Entity.PageFolder)
                ? Entity.PageFolder
                    : string.IsNullOrEmpty(Entity.Classify)
                        ? Entity.Name
                        : $"{Entity.Classify}\\{Entity.Name}";

            var file = ConfigPath(Entity, "File_VUE_HTML", path, folder, "index.htm");

            var coder = new VueCoder
            {
                Entity = Entity,
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
        protected override void CreateExCode(string path)
        {
            if (Entity.IsInternal || Entity.NoDataBase || Entity.DenyScope.HasFlag(AccessScopeType.Client))
                return;

            var folder = !string.IsNullOrWhiteSpace(Entity.PageFolder)
                ? Entity.PageFolder
                    : string.IsNullOrEmpty(Entity.Classify)
                        ? Entity.Name
                        : $"{Entity.Classify}\\{Entity.Name}";

            var file = ConfigPath(Entity, "File_VUE_JS", path, folder, "script.js");

            var coder = new VueCoder
            {
                Entity = Entity,
                Project = Project
            };
            WriteFile(file, coder.ScriptCode());
        }

        #endregion
    }
}