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
        #region ע��

        void IAutoRegister.AutoRegist()
        {
            MomentCoder.RegisteCoder("Web-EasyUi", "Form����˶�ȡ", "cs", InputConvert);
            MomentCoder.RegisteCoder("Web-EasyUi", "�����б���", "xml", ApiCode);
            MomentCoder.RegisteCoder("Web-EasyUi", "�����б�ѡ��", "xml", ApiSwitch);




            MomentCoder.RegisteCoder("Web-EasyUi", "������ע��ʽ�༭����", "cs", WorkflowInfo);
            //MomentCoder.RegisteCoder("Web-EasyUi", "��������(JS)","js", DataInfoCs);
            MomentCoder.RegisteCoder("Web-EasyUi", "��������(CS)", "cs", DataInfoCs);
            //MomentCoder.RegisteCoder("Web-EasyUi", "����У��(CS)", "cs",WorkflowInfo);
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
                return "��ѡ��һ��ʵ��ģ��";
            var coder = new EasyUiIndexPageCoder();
            return coder.BaseCode(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string InputConvert(ConfigBase config)
        {

            var entity = config as EntityConfig;
            if (entity == null)
                return "��ѡ��һ��ʵ��ģ��";
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
                return "��ѡ��һ��ʵ��ģ��";
            var coder = new EasyUiListDetailsPageCoder();
            return coder.BaseCode(entity);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string EasyUiScriptJs(ConfigBase config)
        {
            var entity = config as EntityConfig;
            if (entity == null)
                return "��ѡ��һ��ʵ��ģ��";
            var coder = new EasyUiScriptCoder();
            return coder.BaseCode(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string PageJs(ConfigBase config)
        {
            var entity = config as EntityConfig;
            if (entity == null)
                return "��ѡ��һ��ʵ��ģ��";
            var coder = new EasyUiPageScriptCoder();
            return coder.BaseCode(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string FormJs(ConfigBase config)
        {
            var entity = config as EntityConfig;
            if (entity == null)
                return "��ѡ��һ��ʵ��ģ��";
            var coder = new EasyUiPageScriptCoder();
            return coder.ExtendCode(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string FormCode(ConfigBase config)
        {
            var entity = config as EntityConfig;
            if (entity == null)
                return "��ѡ��һ��ʵ��ģ��";
            var coder = new EasyUiFormCoder();
            return coder.BaseCode(entity);
        }
        #endregion

        #region �����б�֧��

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static string ApiSwitch(ConfigBase config)
        {
            var entity = config as EntityConfig;
            if (entity == null)
                return "��ѡ��һ��ʵ��ģ��";
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
                return "��ѡ��һ��ʵ��ģ��";
            var isApi = entity.Properties.FirstOrDefault(p => p.IsCaption);
            if (isApi != null)
            {
                return $@"

        /// <summary>
        /// ȡ{entity.Caption}�������б�����
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

        #region ������֧��

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
                return "��ѡ��һ��ʵ��ģ��";
            var jsonBuilder = new StringBuilder();
            foreach (PropertyConfig field in entity.PublishProperty)
            {
                string ext = null;
                if (field.CsType.ToLower() == "bool")
                    ext = " ? \"��\" : \"��\"";
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
        #region MVC֧��
        /*
        private static string MvcMenu(ConfigBase config)
        {
            var entity = config as EntityConfig;
            if (entity == null)
                return "��ѡ��һ��ʵ��ģ��";
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
                return "��ѡ��һ��ʵ��ģ��";
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

        private static string FormSaveCode(ConfigBase config)
        {
            var entity = config as EntityConfig;
            if (entity == null)
                return "��ѡ��һ��ʵ��ģ��";
            return EasyUiHelperCoder.InputConvert2(entity);
        }

        private static string EasyUiGrid(ConfigBase config)
        {
            var entity = config as EntityConfig;
            if (entity == null)
                return "��ѡ��һ��ʵ��ģ��";
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