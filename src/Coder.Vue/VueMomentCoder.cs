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
        #region 注册

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
        public static string FormCode(ConfigBase config)
        {
            var entity = config as EntityConfig;
            if (entity == null)
                return "请选择一个实体模型";
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