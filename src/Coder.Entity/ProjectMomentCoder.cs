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
            MomentCoder.RegisteCoder("枚举", "枚举(C#)", "cs",(EnumFunc));
            MomentCoder.RegisteCoder("枚举", "枚举(C++)","cpp", (EnumCpp));
        }
        #endregion

        #region 枚举(C++)

        public static string EnumCpp(ConfigBase config)
        {
            var code = new StringBuilder();
            var enumConfig = config as EnumConfig;
            if (enumConfig != null)
            {
                EnumCpp(code, enumConfig);
            }
            else
            {
                foreach (var em in SolutionConfig.Current.Enums)
                    EnumCpp(code, em);
            }
            return code.ToString();
        }

        private static void EnumCpp(StringBuilder code, EnumConfig config)
        {
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
                int vl;
                if (int.TryParse(item.Value, out vl))
                    code.Append($@"
    {item.Name} = 0x{vl:X},");
                else
                    code.Append($@"
    {item.Name} = item.Value,");
            }
            code.Append(@"
};");
        }
        #endregion

        #region 枚举(C#)

        public static string EnumFunc(ConfigBase config)
        {
            var code = new StringBuilder();
            var enumConfig = config as EnumConfig;
            if (enumConfig != null)
            {
                EnumCode(code, enumConfig);
            }
            else
            {
                var project = config as ProjectConfig;
                if (project != null)
                {
                    List<EnumConfig> enums = new List<EnumConfig>();
                    foreach (var entity in project.Entities)
                    {
                        foreach (var ef in entity.Properties.Where(p => p.EnumConfig != null))
                            if (!enums.Contains(ef.EnumConfig))
                                enums.Add(ef.EnumConfig);
                            ;
                    }
                    enums.ForEach(p => EnumCode(code, p));
                }
                else
                    foreach (var em in SolutionConfig.Current.Enums)
                        EnumCode(code, em);
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
                int vl;
                if (int.TryParse(item.Value, out vl))
                    code.Append($@"
        {item.Name} = 0x{vl:X},");
                else
                    code.Append($@"
        {item.Name} = item.Value,");
            }
            code.Append(@"
    }");
        }
        #endregion


    }
}