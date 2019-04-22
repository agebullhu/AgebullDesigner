using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder.EasyUi;

namespace Agebull.EntityModel.RobotCoder.AspNet
{
    /// <summary>
    ///     页面代码生成器
    /// </summary>
    public class PorjectApiGenerator : CoderWithEntity
    {
        #region 继承实现
        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_API_CS";

        /// <summary>
        ///     生成基础代码
        /// </summary>
        protected override void CreateBaCode(string path)
        {
            if (Entity.IsInternal || Entity.NoDataBase || Entity.DenyScope.HasFlag(AccessScopeType.Client))
                return;
            var file = ConfigPath(Entity, "File_Web_Api_cs", path, Entity.Name, $"{Entity.Name}ApiController");
            var coder = new ProjectApiActionCoder();
            WriteFile(file + ".Designer.cs", coder.BaseCode(Entity));
        }

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected override void CreateExCode(string path)
        {
            if (Entity.IsInternal || Entity.NoDataBase || Entity.DenyScope.HasFlag(AccessScopeType.Client))
                return;
            var file = ConfigPath(Entity, "File_Web_Api_cs", path, Entity.Name, $"{Entity.Name}ApiController");
            var coder = new ProjectApiActionCoder();
            WriteFile(file + ".cs", coder.ExtendCode(Entity));
            //ExportCsCode(path);
        }

        #endregion

        #region Export
        /*
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
        */
        #endregion
    }
}