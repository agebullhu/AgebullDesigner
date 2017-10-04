using System.IO;
using System.Text;
using Gboxt.Common.DataAccess.Schemas;

namespace Agebull.Common.SimpleDesign.WebApplition
{
    public sealed class InterfaceBuilder : CoderWithEntity
    {
        #region 属性
        /// <summary>
        /// 名称
        /// </summary>
        protected override string Name => "File_Model_Interface_cs";

        private string Properties()
        {
            var code = new StringBuilder();
            foreach (PropertyConfig field in Entity.PublishProperty)
            {
                if (field.IsPrimaryKey)
                    continue;
                code.AppendFormat(@"
        /// <summary>
        /// {0}
        /// </summary>
        /// <remarks>
        /// {3}
        /// </remarks>
        {2} {1}
        {{
            get;
            set;
        }}"
                        , ToRemString(field.Caption)
                        , field.Name
                        , field.LastCsType
                        , ToRemString(field.Description));
            }
            return code.ToString();
        }

        #endregion

        #region 主体代码


        /// <summary>
        ///     生成实体代码
        /// </summary>
        protected override void CreateBaCode(string path)
        {
            string file = Path.Combine(path, Entity.Name + ".Interface.cs");

            string code = string.Format(@"using System;
namespace {0}
{{
    /// <summary>
    /// {1}
    /// </summary>
    public interface I{2}
    {{{3}
    }}

    /*partial class {2} : I{2}
    {{
    }}*/

}}"
                , NameSpace
                , Entity.Description
                , Entity.Name
                , Properties());
            SaveCode(file, code);
        }

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected override void CreateExCode(string path)
        {
        }

        #endregion
    }
}