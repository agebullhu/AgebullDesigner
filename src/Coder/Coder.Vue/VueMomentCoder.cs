using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.VUE
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class VueMomentCoder : MomentCoderBase, IAutoRegister
    {
        #region ×¢²á

        void IAutoRegister.AutoRegist()
        {
            MomentCoder.RegisteCoder("Web-Vue", "Html", "html", HtmlCode);
            MomentCoder.RegisteCoder("Web-Vue", "Script", "js", ScriptCode);
            MomentCoder.RegisteCoder("Web-Vue", "²Ëµ¥£¨Js£©", "js", MenuScriptCode);
            MomentCoder.RegisteCoder("Web-Vue", "²Ëµ¥£¨Html£©", "html", MenuHtmlCode);
            MomentCoder.RegisteCoder("Web-Vue", "ÏêÏ¸", "html", HtmlDetailsCode);
        }
        #endregion

        #region ²Ëµ¥

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string MenuHtmlCode(EntityConfig entity)
        {
            return $@"
        <el-menu-item index='menu_{entity.Parent.Name.ToName()}_{entity.Name.ToName()}'>
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
                case 'menu_{entity.Parent.Name.ToName()}_{entity.Name.ToName()}':
                    showIframe('/{entity.Parent.PageRoot}/{(entity as IEntityConfig).PagePath('/')}/index.htm');
                    break;";
        }

        #endregion

        #region Form

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string HtmlCode(ModelConfig entity)
        {
            var coder = new VueHtmlCoder
            {
                Model = entity,
                Project = entity.Parent
            };
            return coder.HtmlCode();
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
                Project = entity.Parent
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
                Project = entity.Parent
            };
            return coder.HtmlDetailsCode(0);
        }

        #endregion
    }
}