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
        #region зЂВс

        void IAutoRegister.AutoRegist()
        {
            MomentCoder.RegisteCoder("Web-Vue", "Html", "html", HtmlCode);
            MomentCoder.RegisteCoder("Web-Vue", "Script", "js", ScriptCode);
        }
        #endregion

        #region Form

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string HtmlCode(EntityConfig entity)
        {
            var coder = new VueFormCoder
            {
                Entity = entity,
                Project = entity.Parent
            };
            return coder.HtmlCode();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string ScriptCode(EntityConfig entity)
        {
            var coder = new VueFormCoder
            {
                Entity = entity,
                Project = entity.Parent
            };
            return coder.ScriptCode();
        }
        #endregion
    }
}