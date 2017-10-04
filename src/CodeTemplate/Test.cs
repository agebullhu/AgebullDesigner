using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using CodeTemplate;
using Gboxt.Common.DataAccess.Schemas;
using Newtonsoft.Json;

namespace Agebull.CodeRefactor.CodeTemplate
{
    public static class Test
    {
        [STAThread]
        public static void Main()
        {
            var tmp = File.ReadAllText(@"C:\spring\Designe\template.lua");
            var code = LUA.TemplateParse.Analyze("test", tmp);
            Clipboard.SetText(code);
            Debug.WriteLine(code);
            var lua = new Lua();
            var context = new LuaContext
            {
                Entity = new EntityConfig
                {
                    Name = "test",
                    Abbreviation = "a",
                    Properties = new ObservableCollection<PropertyConfig>
                    {
                        new PropertyConfig()
                        {
                            IsPrimaryKey=true,
                            Name="test",
                            Caption="Caption",
                        }
                    },
                    Parent = new ProjectConfig
                    {
                        Abbreviation = "a",
                        Name = "test",
                        Caption = "Caption",
                    }
                }
            };
            var jentity = JsonConvert.SerializeObject(context.Entity);
            var cd = $"Entity = {jentity}";
            lua.DoString(cd);
            lua["Entity"] = context.Entity;
            lua.DoString("pro = Entity.Properties");
            var pro = lua["pro"];
            var table = pro as LuaTable;
            var type = context.GetType();
            lua.RegisterFunction("getEntity", context, type.GetMethod("GetEntity"));
            lua.DoString(code);

            var last = lua["strResult"];
            if (last != null)
                Clipboard.SetText(last.ToString());
            Console.WriteLine(last);
            Console.ReadKey();
        }


    }
}
