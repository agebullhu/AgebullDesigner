using System.Text;
using Agebull.Common.LUA;

namespace Agebull.EntityModel.Config
{
    partial class ConfigBase
    {
        /// <summary>
        ///     LUA结构支持
        /// </summary>
        /// <returns></returns>
        public virtual void GetLuaStruct(StringBuilder code)
        {
            if (!string.IsNullOrWhiteSpace(Name))
                code.AppendLine($@"['Name'] = '{Name.ToLuaString()}',");

            if (!string.IsNullOrWhiteSpace(Caption))
                code.AppendLine($@"['Caption'] = ""{Caption.ToLuaString()}"",");

            if (!string.IsNullOrWhiteSpace(Description))
                code.AppendLine($@"['Description'] = '{Description.ToLuaString()}',");

            code.AppendLine($@"['IsReference'] = {IsReference.ToString().ToLower()},");

            code.AppendLine($@"['Key'] = '{Key}',");

            code.AppendLine($@"['Identity'] = {Identity},");

            code.AppendLine($@"['Index'] = {Index},");

            //code.AppendLine($@"['Discard'] = {(Discard.ToString().ToLower())},");

            //code.AppendLine($@"['IsFreeze'] = {(IsFreeze.ToString().ToLower())},");

            //code.AppendLine($@"['IsDelete'] = {(IsDelete.ToString().ToLower())},");

            if (!string.IsNullOrWhiteSpace(Tag))
                code.AppendLine($@"['Tag'] = '{Tag.ToLuaString()}',");

            //if (!string.IsNullOrWhiteSpace(NameHistory))
            //    code.AppendLine($@"['NameHistory'] = ""{NameHistory.ToLuaString()}"",");

            //int idx = 0;
            //code.Append("'OldNames':'{");
            //foreach (var val in OldNames)
            //    code.AppendLine($@"{++idx}:{val.GetLuaStruct()},");
        }

        /// <summary>
        ///     LUA结构支持
        /// </summary>
        /// <returns></returns>
        public string GetLuaStruct()
        {
            var code = new StringBuilder();
            GetLuaStruct(code);
            return "{" + code.ToString().TrimEnd('\r', '\n', ' ', '\t', ',') + "}";
        }
    }
}