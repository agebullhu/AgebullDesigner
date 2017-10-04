using System.Text;
using Agebull.Common.LUA;
using Gboxt.Common.DataAccess.Schemas;
using NLua;

namespace CodeTemplate.Analyze
{
    /// <summary>
    ///     模板执行器
    /// </summary>
    public class TemplateExecuter
    {
        private readonly StringBuilder _codeBuilder = new StringBuilder();
        public string ResultCode => _codeBuilder.ToString();

        public void Append(object arg)
        {
            if (arg == null)
                return;
            var s = arg as string;
            if (s != null)
            {
                _codeBuilder.Append(s.FromLuaChar());
            }
            else
            {
                _codeBuilder.Append(arg);
            }
        }

        public Lua InitLua(ConfigBase config)
        {
            var lua = new Lua();
            lua.RegisterFunction("appendCode", this, GetType().GetMethod("Append"));
            var exType = typeof(LuaStringExtend);
            lua.RegisterFunction("pinYin", null, exType.GetMethod("PinYin"));
            lua.RegisterFunction("shengMu", null, exType.GetMethod("ShengMu"));
            lua.RegisterFunction("toFieldName", null, exType.GetMethod("ToFieldName"));
            lua.RegisterFunction("encodeBase64", null, exType.GetMethod("EncodeBase64"));
            lua.RegisterFunction("decodeBase64", null, exType.GetMethod("DecodeBase64"));
            lua.RegisterFunction("toHungaryName", null, exType.GetMethod("ToHungaryName"));
            lua.RegisterFunction("toHumpName", null, exType.GetMethod("ToHumpName"));
            lua.RegisterFunction("toPascalName", null, exType.GetMethod("ToPascalName"));
            lua.RegisterFunction("toUnderName", null, exType.GetMethod("ToUnderName"));
            lua.RegisterFunction("spliteWord", null, exType.GetMethod("SpliteWord"));
            lua.RegisterFunction("toWordName", null, exType.GetMethod("ToWordName"));
            lua.RegisterFunction("toLinkWordName", null, exType.GetMethod("ToLinkWordName"));
            lua.RegisterFunction("toPluralism", null, exType.GetMethod("ToPluralism"));
            lua.RegisterFunction("listToString", null, exType.GetMethod("ListToString"));
            lua.RegisterFunction("dictionaryToString", null, exType.GetMethod("DictionaryToString"));
            lua.RegisterFunction("mulitReplace", null, exType.GetMethod("MulitReplace"));
            lua.RegisterFunction("mulitReplaceOne", null, exType.GetMethod("MulitReplaceOne"));
            lua.RegisterFunction("isNullOrDefault", null, exType.GetMethod("IsNullOrDefault"));
            lua.RegisterFunction("isName", null, exType.GetMethod("IsName"));
            lua.RegisterFunction("toUWord", null, exType.GetMethod("ToUWord"));
            lua.RegisterFunction("toLWord", null, exType.GetMethod("ToLWord"));
            lua.RegisterFunction("isEquals", null, exType.GetMethod("IsEquals"));
            lua.RegisterFunction("getLen", null, exType.GetMethod("GetLen"));
            lua.RegisterFunction("toDataBaseType", null, exType.GetMethod("ToDataBaseType"));

            if (config == null)
                return lua;
            var jentity = config.GetLuaStruct();
            var cd = $"model = {jentity}";
            lua.DoString(cd);
            return lua;
        }
    }
}