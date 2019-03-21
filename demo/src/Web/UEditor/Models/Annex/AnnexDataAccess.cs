/*design by:agebull designer date:2017/6/14 1:30:12*/

using Agebull.Common.Ioc;
using Agebull.EntityModel.MySql;

namespace Agebull.ZeroNet.ManageApplication.DataAccess
{
    /// <summary>
    /// 附件
    /// </summary>
    sealed partial class AnnexDataAccess : DataStateTable<AnnexData, AnnexDb>
    {
        static AnnexDataAccess()
        {
            IocHelper.AddScoped<AnnexDb, AnnexDb>();
        }
        /// <summary>
        /// 附件(tb_project_annex):附件
        /// </summary>
        public const int Table_Annex = 0x20008;
    }
}