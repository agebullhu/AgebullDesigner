using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal class EnumMomentCoder : MomentCoderBase, IAutoRegister
    {
        #region 注册

        /// <summary>
        /// 注册代码
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            MomentCoder.RegisteCoder<EnumConfig>("枚举", "枚举(C++)", "cpp", (EnumCpp));
            MomentCoder.RegisteCoder<EnumConfig>("枚举", "枚举(JS)", "js", EnumJs);
            MomentCoder.RegisteCoder<EnumConfig>("枚举", "枚举(C#)", "cs", (EnumFunc));
            MomentCoder.RegisteCoder<EnumConfig>("枚举", "枚举名称扩展方法(Enum.Caption())", "cs", EnumCs);
        }
        #endregion

        #region 枚举(C++)

        public static string EnumCpp(EnumConfig config)
        {
            var code = new StringBuilder();
            code.Append($@"

/**
* @brief {ToRemString(config.Caption)}枚举
*/
    enum class {config.Name}Classify
{{");
            foreach (var item in config.Items)
            {
                code.Append($@"

    //{item.Caption}");
                code.Append(int.TryParse(item.Value, out int vl)
                    ? $@"
    {item.Name} = 0x{vl:X},"
                    : $@"
    {item.Name} = item.Value,");
            }
            code.Append(@"
};");
            return code.ToString();
        }
        #endregion

        #region 枚举(C#)

        public static string EnumFunc(ConfigBase config)
        {
            var code = new StringBuilder();
            switch (config)
            {
                case EnumConfig enumConfig:
                    EnumCode(code, enumConfig);
                    break;
                case ProjectConfig project:
                    List<EnumConfig> enums = new List<EnumConfig>();
                    foreach (var entity in project.Entities)
                    {
                        foreach (var ef in entity.LastProperties.Where(p => p.EnumConfig != null))
                            if (!enums.Contains(ef.EnumConfig))
                                enums.Add(ef.EnumConfig);

                    }
                    enums.ForEach(p => EnumCode(code, p));
                    break;
                default:
                    foreach (var em in SolutionConfig.Current.Enums)
                        EnumCode(code, em);
                    break;
            }
            return code.ToString();
        }

        private static void EnumCode(StringBuilder code, EnumConfig config)
        {
            code.Append($@"

    /// <summary>
    /// {ToRemString(config.Caption)}
    /// </summary>
    /// <remark>
    /// {ToRemString(config.Description)}
    /// </remark>");
            if (config.IsFlagEnum)
                code.Append(@"
    [Flags]");
            code.Append($@"
    public enum {config.Name}
    {{");
            foreach (var item in config.Items)
            {
                code.Append($@"
        /// <summary>
        /// {ToRemString(item.Caption)}
        /// </summary>");
                code.Append(int.TryParse(item.Value, out int vl)
                    ? $@"
        {item.Name} = 0x{vl:X},"
                    : $@"
        {item.Name} = item.Value,");
            }
            code.Append(@"
    }");
        }
        #endregion


        #region EnumJs

        private static string EnumJs(EnumConfig enumc)
        {
            var code = new StringBuilder();

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
            return code.ToString();
        }

        #endregion

        #region EnumCs

        private static string EnumCs(EnumConfig enumc)
        {
            var code = new StringBuilder();
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
                    return ""{enumc.Caption}(错误)"";
            }}
        }}
");
            return code.ToString();
        }
        #endregion
    }
}