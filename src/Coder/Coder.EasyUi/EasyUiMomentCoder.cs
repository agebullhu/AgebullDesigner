using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.EasyUi
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class EasyUiMomentCoder : MomentCoderBase, IAutoRegister
    {
        #region 注册

        void IAutoRegister.AutoRegist()
        {
            MomentCoder.RegisteCoder("Web-EasyUi", "下拉列表方法", "xml", ApiCode);
            MomentCoder.RegisteCoder("Web-EasyUi", "下拉列表选择", "xml", ApiSwitch);




            MomentCoder.RegisteCoder<ProjectConfig>("Web-EasyUi", "工作流注入式编辑代码", "cs", WorkflowInfo);
            MomentCoder.RegisteCoder<ProjectConfig>("Web-EasyUi", "对象名称(JS)","js", DataInfoCs);
            MomentCoder.RegisteCoder<ProjectConfig>("Web-EasyUi", "对象名称(CS)", "cs", DataInfoCs);
            MomentCoder.RegisteCoder< ProjectConfig>("Web-EasyUi", "数据校验(CS)", "cs",WorkflowInfo);
        }
        #endregion

        #region Form


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string IndexPage(EntityConfig entity)
        {
            var coder = new EasyUiIndexPageCoder();
            return coder.BaseCode(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GridDetailsPage(EntityConfig entity)
        {

            var coder = new EasyUiListDetailsPageCoder();
            return coder.BaseCode(entity);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string EasyUiScriptJs(EntityConfig entity)
        {
            var coder = new EasyUiScriptCoder();
            return coder.BaseCode(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string PageJs(EntityConfig entity)
        {
            var coder = new EasyUiPageScriptCoder();
            return coder.BaseCode(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string FormJs(EntityConfig entity)
        {
            var coder = new EasyUiPageScriptCoder();
            return coder.ExtendCode(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string FormCode(EntityConfig entity)
        {
            var coder = new EasyUiFormCoder();
            return coder.BaseCode(entity);
        }
        #endregion

        #region 下拉列表支持

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static string ApiSwitch(EntityConfig entity)
        {
            var isApi = entity.Properties.FirstOrDefault(p => p.IsCaption);
            if (isApi != null)
            {
                return $@"
                case ""{entity.Name.ToLower()}"":
                    Get{entity.Name}();
                    break;";
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static string ApiCode(EntityConfig entity)
        {
            var isApi = entity.Properties.FirstOrDefault(p => p.IsCaption);
            if (isApi != null)
            {
                return $@"

        /// <summary>
        /// 取{entity.Caption}的下拉列表数据
        /// </summary>
        private void Get{entity.Name}()
        {{
            var access = new {entity.Name}DataAccess();
            var result = access.All().Select(p => new EasyComboValues(p.{entity.PrimaryColumn.Name}, p.{isApi.Name})).ToList();
            result.Insert(0, new EasyComboValues
            {{
                Key = 0,
                Value = ""-""
            }});
            SetCustomJsonResult(result);
        }}";
            }
            return null;
        }

        #endregion

        #region 工作流支持

        private static string WorkflowInfo(ProjectConfig project)
        {
            var jsonBuilder = new StringBuilder();
            jsonBuilder.Append(@"
            switch (job.EntityType)
            {");
            foreach (var entity in project.Entities)
            {
                var formPath = entity["File_Web_Form"]?.Replace('\\', '/');
                jsonBuilder.Append($@"
                case 0x{entity.Identity:X}://{entity.Caption}
                    job.ScriptUrl = $""/{formPath?.MulitReplace2(".js", ".htm", ".html", ".aspx", ".cshtml")}?_a_={{job.LinkId}}"";
                    job.FormUrl = $""/{formPath}?_a_={{job.LinkId}}"";
                    job.ReadUrl = $""/{entity["File_Web_Item"]?.Replace('\\', '/')}?_a_={{job.LinkId}}"";
                    job.ActionUrl = $""/{entity["File_Web_Action"]?.Replace('\\', '/')}?_a_={{job.LinkId}}"";
                    break;");
            }
            jsonBuilder.Append(@"
            }");
            return jsonBuilder.ToString();
        }


        private static string EasyUiInfo(EntityConfig entity)
        {
            var jsonBuilder = new StringBuilder();
            foreach (PropertyConfig field in entity.PublishProperty)
            {
                string ext = null;
                if (field.CsType.ToLower() == "bool")
                    ext = " ? \"是\" : \"否\"";
                jsonBuilder.AppendFormat(@"
                <div class='infoField'>
                    <div class='infoLabel' title='{3}'>{0}:</div>
                    <div class='infoValue_s'>@(Model.{1}{2})</div>
                </div>"
                    , field.Caption ?? field.Name
                    , field.Name
                    , ext
                    , field.Description);
            }
            return jsonBuilder.ToString();
        }


        private static string DataInfoCs(ProjectConfig project)
        {
            var jsonBuilder = new StringBuilder();
            jsonBuilder.Append(@"
            switch (entityType)
            {");
            foreach (var entity in project.Entities.Where(p => p.Interfaces?.Contains("IAudit") ?? false))
            {
                jsonBuilder.Append($@"
                case 0x{entity.Identity:X}://{entity.Caption}
                    return new {entity.Name}DataAccess().First(id);");
            }
            jsonBuilder.Append(@"
            }");
            return jsonBuilder.ToString();
        }

        #endregion
        #region MVC支持
        /*
        private static string MvcMenu(EntityConfig entity)
        {
            var entity = config as EntityConfig;
            if (entity == null)
                return "请选择一个实体模型";
            return
                $@"
           <div iconcls='icon-page' onclick=""javascript:location.href = '@Url.Action(""Index"", ""{
                    entity.Name}"")'"">
           {entity.Caption ?? entity.Description ?? entity.Name
                    }
           </div>";
        }

        static string EasyUiForm(EntityConfig entity)
        {
            var entity = config as EntityConfig;
            if (entity == null)
                return "请选择一个实体模型";
            var jsonBuilder = new StringBuilder();

            jsonBuilder.AppendFormat(@"
<form name='{0}Form' id='{0}Form'>
    <div style='width:490px;display: block;'>", entity.Name.ToLowerInvariant());
            foreach (PropertyConfig field in entity.PublishProperty)
            {
                string ext = null;
                if (field.IsRequired || !field.CanEmpty)
                    ext = " data-options='required:true'";
                string type = " easyui-text";
                jsonBuilder.AppendFormat(@"
       <div class='inputField'>
            <div class='inputRegion'>
                <div class='inputLabel'>{0}:</div>
                <input name='{1}' class='inputValue inputS {4}'{3}/>
            </div>
            <div class='inputHelp'>{2}</div>
        </div>"
                    , field.Caption, field.Name, field.Description, ext, type);

            }
            jsonBuilder.Append(@"
    </div>
</form>");
            return jsonBuilder.ToString();
        }

        private static string FormSaveCode(EntityConfig entity)
        {
            var entity = config as EntityConfig;
            if (entity == null)
                return "请选择一个实体模型";
            return EasyUiHelperCoder.InputConvert2(entity);
        }

        private static string EasyUiGrid(EntityConfig entity)
        {
            var entity = config as EntityConfig;
            if (entity == null)
                return "请选择一个实体模型";
            var jsonBuilder = new StringBuilder();
            foreach (PropertyConfig field in entity.PublishProperty)
            {
                string align = field.CsType == "string" ? "left " : "center";
                string sortable = field.CsType == "string" ? "true " : "false";
                jsonBuilder.AppendFormat(@"
{{ width: 100, align: '{0}', sortable: {1}, field: '{2}', title: '{3}' }},"
                    , align, sortable, field.Name, field.Caption ?? field.Name);
            }
            return jsonBuilder.ToString();
        }
        */
        #endregion
    }
}