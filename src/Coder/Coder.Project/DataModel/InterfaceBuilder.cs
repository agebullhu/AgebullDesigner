using System.IO;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    public sealed class InterfaceBuilder<TModel> : CoderWithModel<TModel>
        where TModel : ProjectChildConfigBase, IEntityConfig
    {
        #region 属性
        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_Model_Interface_cs";

        private string Properties()
        {
            var code = new StringBuilder();
            foreach (var field in Model.PublishProperty)
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
                        , field.Caption.ToRemString()
                        , field.Name
                        , field.LastCsType
                        , field.Description.ToRemString());
            }
            return code.ToString();
        }

        #endregion

        #region 主体代码


        /// <summary>
        ///     生成实体代码
        /// </summary>
        protected override void CreateDesignerCode(string path)
        {
            string file = Path.Combine(path, Model.Name + ".Interface.cs");

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
                , Model.Description
                , Model.Name
                , Properties());
            SaveCode(file, code);
        }

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected override void CreateCustomCode(string path)
        {
        }

        #endregion
    }
}