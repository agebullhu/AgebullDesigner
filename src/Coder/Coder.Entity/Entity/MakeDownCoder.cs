using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace Agebull.EntityModel.RobotCoder
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class MakeDownCoder : MomentCoderBase, IAutoRegister
    {
        #region 注册

        /// <summary>
        /// 注册代码
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            CoderManager.RegisteCoder("文档", "入参", "md", Makedown);
            CoderManager.RegisteCoder("文档", "出参", "md", Makedown2);
            CoderManager.RegisteCoder("文档", "JSON", "json", Json);
        }
        #endregion

        private static string Makedown(EntityConfig config)
        {
            StringBuilder code = new StringBuilder();
            code.AppendLine(@"|参数名|类型|必须|标题|说明|示例|
|:-|:-|:-|:-|");
            foreach (var field in config.Properties)
            {
                code.Append($@"|{field.JsonName ?? field.Name}|{field.CsType}|{(field.IsRequired ? "是" : "否")}|{field.Caption}|");
                if (field.EnumConfig != null)
                {
                    foreach (var item in field.EnumConfig.Items)
                    {
                        code.Append($@"{item.Value} {item.Caption},");
                    }
                }
                else
                    code.Append(field.Description);
                code.AppendLine("|");
            }
            return code.ToString();
        }

        private static string Makedown2(EntityConfig config)
        {
            StringBuilder code = new StringBuilder();
            code.AppendLine(@"|参数名|类型|标题|说明|
|:-|:-|:-|:-|");
            foreach (var field in config.Properties)
            {
                code.Append($@"|{field.JsonName ?? field.Name}|{field.CsType}|{field.Caption}|");
                if (field.EnumConfig != null)
                {
                    foreach (var item in field.EnumConfig.Items)
                    {
                        code.Append($@"{item.Value} {item.Caption},");
                    }
                }
                else
                    code.Append(field.Description);
                code.AppendLine("|");
            }
            return code.ToString();
        }

        private static string Json(EntityConfig config)
        {
            StringBuilder code = new StringBuilder();
            bool first = true;
            foreach (var field in config.Properties)
            {
                if (first)
                    first = false;
                else code.AppendLine(",");
                code.Append($@"            ""{ field.JsonName ?? field.Name}"" : ");
                if (field.IsEnum && field.EnumConfig != null)
                {
                    code.Append($@"""{ (field.EnumConfig.Items.FirstOrDefault()?.Value ?? "0")}""");
                }
                else switch (field.CsType)
                    {
                        case "string":
                            code.Append($@"""{field.HelloCode ?? field.Caption ?? field.Name}""");
                            break;
                        case "DateTime":
                            code.Append($@"""{field.HelloCode ?? "2018-01-01T12:00:00:00.000"}""'");
                            break;
                        case "bool":
                            code.Append($@"""{field.HelloCode ?? "false"}""");
                            break;
                        default:
                            code.Append($@"""{field.HelloCode ?? "0"}""");
                            break;
                    }
            }
            return code.ToString();
        }
    }
}