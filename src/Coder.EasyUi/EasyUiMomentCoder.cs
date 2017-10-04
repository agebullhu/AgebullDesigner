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
            MomentCoder.RegisteCoder("Web-EasyUi", "Index.aspx", IndexPage);
            MomentCoder.RegisteCoder("Web-EasyUi", "Form.htm", FormCode);
            MomentCoder.RegisteCoder("Web-EasyUi", "Form服务端读取", InputConvert);
            MomentCoder.RegisteCoder("Web-EasyUi", "Script.js", PageScript);
            MomentCoder.RegisteCoder("Web-EasyUi", "Details.aspx", GridDetailsPage);
            MomentCoder.RegisteCoder("Web-EasyUi", "下拉列表方法", ApiCode);
            MomentCoder.RegisteCoder("Web-EasyUi", "下拉列表选择", ApiSwitch);
            MomentCoder.RegisteCoder("Web-EasyUi", "Mvc菜单", MvcMenu);
            MomentCoder.RegisteCoder("Web-EasyUi", "EasyUi表单", EasyUiForm);
            MomentCoder.RegisteCoder("Web-EasyUi", "EasyUi表单保存", FormSaveCode);
            MomentCoder.RegisteCoder("Web-EasyUi", "EasyUi表格", EasyUiGrid);
            MomentCoder.RegisteCoder("Web-EasyUi", "EasyUi详情", EasyUiInfo);
            MomentCoder.RegisteCoder("Web-EasyUi", "枚举(JS)", EnumJs);
            MomentCoder.RegisteCoder("Web-EasyUi", "枚举(CS)名称", EnumCs);
            MomentCoder.RegisteCoder("Web-EasyUi", "工作流注入式编辑代码", WorkflowInfo);
            //MomentCoder.RegisteCoder("Web-EasyUi", "对象名称(JS)", DataInfoCs);
            MomentCoder.RegisteCoder("Web-EasyUi", "对象名称(CS)", DataInfoCs);
            //MomentCoder.RegisteCoder("Web-EasyUi", "数据校验(CS)", WorkflowInfo);
        }
        #endregion

        #region Form


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string IndexPage(ConfigBase config)
        {
            var entity = config as EntityConfig;
            if (entity == null)
                return "请选择一个实体模型";
            var coder = new EasyUiIndexPageCoder
            {
                Entity = entity,
                Project = entity.Parent
            };
            return coder.Code();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string InputConvert(ConfigBase config)
        {

            var entity = config as EntityConfig;
            if (entity == null)
                return "请选择一个实体模型";
            var coder = new EasyUiHelperCoder
            {
                Entity = entity,
                Project = entity.Parent
            };
            return coder.InputConvert();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GridDetailsPage(ConfigBase config)
        {

            var entity = config as EntityConfig;
            if (entity == null)
                return "请选择一个实体模型";
            var coder = new EasyUiListDetailsPageCoder
            {
                Entity = entity,
                Project = entity.Parent
            };
            return coder.Code();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string PageScript(ConfigBase config)
        {
            var entity = config as EntityConfig;
            if (entity == null)
                return "请选择一个实体模型";
            var coder = new EasyUiPageScriptCoder
            {
                Entity = entity,
                Project = entity.Parent
            };
            return coder.Code();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string FormCode(ConfigBase config)
        {
            var entity = config as EntityConfig;
            if (entity == null)
                return "请选择一个实体模型";
            var coder = new EasyUiFormCoder
            {
                Entity = entity,
                Project = entity.Parent
            };
            return coder.Code();
        }
        #endregion

        #region 下拉列表支持

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static string ApiSwitch(ConfigBase config)
        {
            var entity = config as EntityConfig;
            if (entity == null)
                return "请选择一个实体模型";
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
        private static string ApiCode(ConfigBase config)
        {
            var entity = config as EntityConfig;
            if (entity == null)
                return "请选择一个实体模型";
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

        #region EnumJs

        private static string EnumJs(ConfigBase config)
        {
            var code = new StringBuilder();
            if (config is EnumConfig)
            {
                TypeDefaultScript(code, config as EnumConfig);
            }
            else
            {
                foreach (var item in SolutionConfig.Current.Enums)
                {
                    TypeDefaultScript(code, item);
                }
            }
            return code.ToString();
        }
        /// <summary>
        ///     生成枚举
        /// </summary>
        public static void TypeDefaultScript(StringBuilder code, EnumConfig enumc)
        {
            code.Append($@"
/**
 * {enumc.Caption}
 */
var {enumc.Name.ToLWord()} = [");
            bool isFirst = true;
            foreach (var item in enumc.Items)
            {
                if (isFirst)
                    isFirst = false;
                else
                    code.Append(',');
                code.Append($@"
    {{ value: {item.Value}, text: '{item.Caption}' }}");
            }
            code.Append($@"
];

/**
 * {enumc.Caption}之表格格式化方法
 */
function {enumc.Name.ToLWord()}Format(value) {{
    return arrayFormat(value, {enumc.Name.ToLWord()});
}}
");
        }


        #endregion

        #region EnumCs

        private static string EnumCs(ConfigBase config)
        {
            var code = new StringBuilder();
            List<EnumConfig> doed = new List<EnumConfig>();
            ForeachByCurrent(enumc => EnumName(code, enumc, doed));
            return code.ToString();
        }
        /// <summary>
        ///     生成枚举
        /// </summary>
        public static void EnumName(StringBuilder code, EnumConfig enumc, List<EnumConfig> doed)
        {
            if (doed.Contains(enumc))
                return;
            doed.Add(enumc);
            code.Append($@"
        /// <summary>
        ///     {enumc.Caption}名称转换
        /// </summary>
        public static string ToCaption(this {enumc.Name} value)
        {{
            switch(value)
            {{");
            foreach (var item in enumc.Items)
            {
                code.Append($@"
                case {enumc.Name}.{item.Name}:
                    return ""{item.Caption}"";");
            }
            code.Append($@"
                default:
                    return ""{enumc.Caption}(未知)"";
            }}
        }}
");
        }


        #endregion
        #region 工作流支持

        private static string WorkflowInfo(ConfigBase config)
        {
            var jsonBuilder = new StringBuilder();
            jsonBuilder.Append(@"
            switch (job.EntityType)
            {");
            foreach (var entity in SolutionConfig.Current.Entities)
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


        private static string EasyUiInfo(ConfigBase config)
        {
            var entity = config as EntityConfig;
            if (entity == null)
                return "请选择一个实体模型";
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


        private static string DataInfoCs(ConfigBase config)
        {
            var jsonBuilder = new StringBuilder();
            jsonBuilder.Append(@"
            switch (entityType)
            {");
            foreach (var entity in SolutionConfig.Current.Entities.Where(p => p.Interfaces?.Contains("IAudit") ?? false))
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

        private static string MvcMenu(ConfigBase config)
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

        static string EasyUiForm(ConfigBase config)
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
                if (!field.CanEmpty)
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

        private static string FormSaveCode(ConfigBase config)
        {
            var entity = config as EntityConfig;
            if (entity == null)
                return "请选择一个实体模型";
            return EasyUiHelperCoder.InputConvert2(entity);
        }

        private static string EasyUiGrid(ConfigBase config)
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

        #endregion
    }
}