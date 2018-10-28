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
            MomentCoder.RegisteCoder("Web-Vue", "Form","xml", FormCode);
        }
        #endregion

        #region Form

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string FormCode(EntityConfig entity)
        {
            var coder = new VueFormCoder
            {
                Entity = entity,
                Project = entity.Parent
            };
            return coder.Code();
        }
        #endregion
    }
}