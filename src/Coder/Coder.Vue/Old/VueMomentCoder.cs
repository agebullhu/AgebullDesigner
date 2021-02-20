using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;

namespace Agebull.EntityModel.RobotCoder.VUE
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class VueMomentCoder : MomentCoderBase, IAutoRegister
    {
        #region 注册

        void IAutoRegister.AutoRegist()
        {
            CoderManager.RegisteCoder("Web-Vue", "Html(el-container)", "html", HtmlContainerCode);
            CoderManager.RegisteCoder("Web-Vue", "Html(el-card)", "html", HtmlCode);
            CoderManager.RegisteCoder("Web-Vue", "Script", "js", ScriptCode);
            CoderManager.RegisteCoder("Web-Vue", "菜单（Js）", "js", MenuScriptCode);
            CoderManager.RegisteCoder<ProjectConfig>("Web-Vue", "菜单数据（Js）", "js", MenuJson);
            CoderManager.RegisteCoder("Web-Vue", "菜单（Html）", "html", MenuHtmlCode);
            CoderManager.RegisteCoder("Web-Vue", "详细", "html", HtmlDetailsCode);
        }
        #endregion

        #region 菜单

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string MenuHtmlCode(EntityConfig entity)
        {
            return $@"
        <el-menu-item index='menu_{entity.Project.Name.ToName()}_{entity.Name.ToName()}'>
            <i class='el-icon-reading'>&nbsp;</i>
            <span>{entity.Caption}</span>
        </el-menu-item>";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string MenuScriptCode(EntityConfig entity)
        {
            return $@"
                case 'menu_{entity.Project.Name.ToName()}_{entity.Name.ToName()}':
                    showIframe('/{entity.Project.PageRoot}/{(entity as IEntityConfig).PagePath('/')}/index.htm');
                    break;";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string MenuJson(ProjectConfig project)
        {
            /*
            if (!PageFolder.IsMissing())
                return PageFolder;
            if (Project.NoClassify || Classify.IsMissing() || Classify.Equals("None", StringComparison.InvariantCulture))
                return Abbreviation;
            var cls = Project.Classifies.FirstOrDefault(p => p.Name == Classify);
            return $"{cls?.Abbreviation ?? Classify.ToLWord()}{sp}{Abbreviation}";*/
            if(project.NoClassify)
            {
                var items = new List<object>();
                foreach (var entity in project.Entities)
                {
                    items.Add(new
                    {
                        title = entity.Caption,
                        path = entity.PageFolder
                    });
                }

                return JsonConvert.SerializeObject(new
                {
                    title = project.Caption,
                    path = project.PageFolder,
                    icon = "el-icon-notebook-1",
                    items
                }, Formatting.Indented);
            }
            else
            {
                var classifies = new List<object>();
                foreach (var classify in project.Classifies)
                {
                    var items = new List<object>();
                    foreach(var entity in classify.Items)
                    {
                        items.Add(new
                        {
                            title = entity.Caption,
                            path = entity.PageFolder
                        });
                    }
                    classifies.Add(new
                    {
                        title = classify.Caption,
                        root = project.PageFolder,
                        icon = "el-icon-notebook-1",
                        path = classify?.Abbreviation ?? classify.Name.ToLWord(),
                        items
                    });
                }

                return JsonConvert.SerializeObject(classifies, Formatting.Indented);
            }
        }

        #endregion

        #region Form

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string HtmlContainerCode(ModelConfig entity)
        {
            var coder = new VueHtmlCoder
            {
                Model = entity,
                Project = entity.Project
            };
            return coder.Container();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string HtmlCode(ModelConfig entity)
        {
            var coder = new VueHtmlCoder
            {
                Model = entity,
                Project = entity.Project
            };
            return coder.Container();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string ScriptCode(ModelConfig entity)
        {
            var coder = new VueScriptCoder
            {
                Model = entity,
                Project = entity.Project
            };
            return coder.ScriptCode();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string HtmlDetailsCode(EntityConfig entity)
        {
            var coder = new VueHtmlCoder
            {
                Model = entity,
                Project = entity.Project
            };
            return coder.EditPanelCode(0);
        }

        #endregion
    }
}