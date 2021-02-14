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
        #region ע��

        /// <summary>
        /// ע�����
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            CoderManager.RegisteCoder("�ĵ�", "���", "md", Makedown);
            CoderManager.RegisteCoder("�ĵ�", "����", "md", Makedown2);
            CoderManager.RegisteCoder("�ĵ�", "JSON", "json", Json);
        }
        #endregion

        private static string Makedown(EntityConfig config)
        {
            StringBuilder code = new StringBuilder();
            code.AppendLine(@"|������|����|����|����|˵��|ʾ��|
|:-|:-|:-|:-|");
            foreach (var field in config.Properties)
            {
                code.Append($@"|{field.JsonName ?? field.Name}|{field.CsType}|{(field.IsRequired ? "��" : "��")}|{field.Caption}|");
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
            code.AppendLine(@"|������|����|����|˵��|
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