using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Text;
using Agebull.Common.LUA;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.Upgrade
{

    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class SelfMomentCoder : MomentCoderBase, IAutoRegister
    {
        #region ע��

        /// <summary>
        /// ע�����
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            MomentCoder.RegisteCoder("����������", "����תΪLUA�ṹ", "cs", ToLuaCode);
            MomentCoder.RegisteCoder("����������", "LUA����ע��", "cs", RegLuaFunc);
            MomentCoder.RegisteCoder("����������", "�������", "cs", WpfDataGrid);
            MomentCoder.RegisteCoder("����������", "����¼������", "cs", BaseDataForm);
            MomentCoder.RegisteCoder("����������", "����¼������", "cs", EntityDataForm);
            MomentCoder.RegisteCoder("����������", "ö���б�", "cs", EnumList);
            MomentCoder.RegisteCoder("����������", "�ֶ����õĸ���", "cs", ToCopyCode);
        }


        #endregion

        #region LUA֧�ִ�������

        public static string ToLuaCode(ConfigBase config)
        {
            var code = new StringBuilder();
            var assembly = typeof(ConfigBase).Assembly;
            var types = assembly.GetTypes().Where(p => p.IsSubclassOf(typeof(ConfigBase)));
            foreach (var type in types)
            {
                TypeLua(type, code);
            }
            return code.ToString();
        }

        private static void TypeLua(Type type, StringBuilder code)
        {
            var func = new StringBuilder();
            foreach (
                var pro in
                type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                    .Where(p => p.CanRead)
            )
            {
                if (pro.Name == "Parent")
                    continue;
                if (pro.PropertyType.IsSubclassOf(typeof(ConfigBase)))
                    func.AppendLine($@"
            if({pro.Name} != null)
                code.AppendLine($@""['{pro.Name}'] = {{{pro.Name}.GetLuaStruct()}},"");");
                else if (pro.PropertyType == typeof(string))
                    func.AppendLine($@"
            if(!string.IsNullOrWhiteSpace({pro.Name}))
                code.AppendLine($@""['{pro.Name}'] = '{{{pro.Name}.ToLuaString()}}',"");
            else
                code.AppendLine($@""['{pro.Name}'] = nil,"");");
                else if (pro.PropertyType.GetInterface(typeof(IDictionary).FullName) != null)
                {
                    func.AppendLine($@"
            idx = 0;
            code.AppendLine(""['{pro.Name}'] ={{"");
            foreach(var val in {pro.Name}.Values)
            {{
                if(idx > 0)
                    code.Append(',');
                code.AppendLine($@""[{{++idx}}] = {{val.GetLuaStruct()}}""); 
            }}
            code.AppendLine(""}},"");");
                }
                else if (pro.PropertyType.GetInterface(typeof(IEnumerable).FullName) != null)
                {
                    func.AppendLine($@"
            idx = 0;
            code.AppendLine(""['{pro.Name}'] ={{"");
            foreach(var val in {pro.Name})
            {{
                if(idx > 0)
                    code.Append(',');
                code.AppendLine($@""[{{++idx}}] = {{val.GetLuaStruct()}}""); 
            }}
            code.AppendLine(""}},"");");
                }
                else if (pro.PropertyType == typeof(bool))
                    func.AppendLine($@"
            code.AppendLine($@""['{pro.Name}'] ={{({pro.Name}.ToString().ToLower())}},"");");
                else if (pro.PropertyType == typeof(int) || pro.PropertyType == typeof(decimal) || pro.PropertyType == typeof(long))
                    func.AppendLine($@"
            code.AppendLine($@""['{pro.Name}'] ={{{pro.Name}}},"");");
                else if (pro.PropertyType.IsValueType)
                    func.AppendLine($@"
            code.AppendLine($@""['{pro.Name}'] ='{{{pro.Name}}}',"");");
                else
                    func.AppendLine($@"
            if({pro.Name} != null)
                code.AppendLine($@""['{pro.Name}'] ='{{{pro.Name}}}',"");");
            }
            if (func.Length == 0)
                return;
            code.Append($@"
    partial class {(type.IsGenericType ? type.Name.Replace("`1", "<TConfig>") : type.Name)}
    {{
        /// <summary>
        /// LUA�ṹ֧��
        /// </summary>
        /// <returns></returns>
        public override void GetLuaStruct(StringBuilder code)
        {{
            base.GetLuaStruct(code);
            int idx;{func}
        }}
    }}
");
        }


        public static string RegLuaFunc(ConfigBase config)
        {
            var code = new StringBuilder();
            var methods = typeof(LuaStringExtend).GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
            foreach (var method in methods)
            {
                code.AppendFormat($@"
        lua.RegisterFunction(""{LuaStringExtend.ToHumpName(method.Name)}"", null, exType.GetMethod(""{method.Name}""));");
            }
            return code.ToString();
        }
        #endregion

        #region ��������

        private static string WpfDataGrid(ConfigBase config)
        {
            var type = typeof(PropertyConfig);
            var pros = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var code = new StringBuilder();
            foreach (var field in pros.Where(p => p.CanRead))
            {
                var display = field.GetCustomAttribute<DisplayNameAttribute>();
                var caption = display != null ? display.DisplayName : field.Name;
                var model = field.CanWrite ? "TwoWay" : "OneWay";
                if (field.PropertyType == typeof(bool))
                {
                    code.Append($@"
<DataGridCheckBoxColumn Header =""{caption}"" Binding=""{{Binding {field.Name}, Mode={model} , UpdateSourceTrigger=PropertyChanged}}""/>");
                }
                else if (field.PropertyType == typeof(string))
                {
                    code.Append($@"
<DataGridTextColumn Header =""{caption}"" Binding=""{{Binding {field.Name}, Mode={model} , UpdateSourceTrigger=PropertyChanged}}""/>");
                }
                else
                {
                    code.Append($@"
<DataGridTextColumn Header =""{caption}"" Binding=""{{Binding {field.Name}, Mode={model},UpdateSourceTrigger=PropertyChanged,StringFormat=F2}}""/>");
                }
            }
            return code.ToString();

        }


        private static string BaseDataForm(ConfigBase config)
        {
            var code = new StringBuilder();
            TypeDataForm(code, typeof(SimpleConfig));
            TypeDataForm(code, typeof(ConfigBase));
            return code.ToString();
        }

        private static string EntityDataForm(ConfigBase config)
        {
            var code = new StringBuilder();
            TypeDataForm(code, typeof(UpgradeConfig));
            TypeDataForm(code, typeof(ClassUpgradeConfig));
            TypeDataForm(code, typeof(FieldUpgradeConfig));
            TypeDataForm(code, typeof(PropertyUpgradeConfig));
            return code.ToString();
        }
        
        private static void TypeDataForm(StringBuilder code, Type type)
        {
            var up = new AssemblyUpgrader();
            var dictionary = up.GetConfig(type.Assembly);
            ClassUpgradeConfig upgradeConfig;
            if (!dictionary.TryGetValue(type.Name, out upgradeConfig))
                return;

            code.Append($@"
        <DataTemplate x:Key=""{upgradeConfig.Name}Template"">
            <StackPanel DataContext=""{{Binding RelativeSource={{RelativeSource Mode=FindAncestor,AncestorType={{x:Type UserControl}}}},Path=DataContext}}"">");
            foreach (var category in upgradeConfig.Properties.Values.GroupBy(p => p.Category))
            {
                code.Append($@"
        <GroupBox Header=""{category.Key}"" Margin=""5"">
            <WrapPanel>");
                foreach (var field in category)
                {
                    var model = field.CanWrite ? "TwoWay" : "OneWay";
                    code.Append($@"
                <StackPanel>
                    <Label Content=""{field.Caption}""/>");

                    if (field.ConfigType == typeof(bool))
                    {
                        code.Append($@"
                    <ComboBox Style=""{{StaticResource BoolCombo}}"" SelectedValue=""{{Binding {field.Name},Mode={model}}}""/>");
                    }
                    else if (field.ConfigType.IsEnum)
                    {
                        code.Append($@"
                    <ComboBox ItemsSource=""{{x:Static defaults:EnumHelper.{field.TypeName}List}}"" SelectedValue=""{{Binding {field.Name},Mode={model}}}""/>");
                    }
                    else
                    {
                        code.Append($@"
                    <TextBox Text=""{{Binding {field.Name},Mode={model}}}""/>");
                    }
                    code.Append(@"
                </StackPanel>");
                }
                code.Append(@"
            </WrapPanel>
        </GroupBox>");
            }

            code.Append(@"
            </StackPanel>
        </DataTemplate>");
        }

        #endregion

        #region ö��

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        private static string EnumList(ConfigBase config)
        {
            var up = new AssemblyUpgrader();
            var dictionary = up.GetEnumConfig();
            var code = new StringBuilder();
            foreach (var cfg in dictionary.Values)
            {
                code.Append($@"
        /// <summary>
        /// {cfg.Caption}���͵��б�
        /// </summary>
        public static List<NameValue<{cfg.TypeName}>> {cfg.TypeName}List = new List<NameValue<{cfg.TypeName}>>
        {{");
                foreach (var field in cfg.Fields.Values)
                {
                    code.Append($@"
            new NameValue<{cfg.TypeName}>
            {{
                name = ""{field.Caption}"",
                value= {cfg.TypeName}.{field.Name}
            }},");
                }
                code.Append(@"
        };");
            }
            return code.ToString();
        }

        #endregion

        #region �ֶθ���

        private static string ToCopyCode(ConfigBase config)
        {
            StringBuilder code = new StringBuilder();
            GetCopy(typeof(PropertyConfig), code);
            return code.ToString();
        }

        private static void GetCopy(Type type, StringBuilder code)
        {
            var up = new AssemblyUpgrader();
            var dictionary = up.GetConfig();
            ClassUpgradeConfig upgradeConfig;
            if (!dictionary.TryGetValue(type.Name, out upgradeConfig))
                return;
            GetCopy(type.BaseType, code);
            code.Append($@"
            //{type.FullName}");

            foreach (var field in upgradeConfig.Properties.Values)
            {
                if (!field.CanWrite || !field.IsJsonAttribute)
                    continue;
                code.Append($@"
            {field.Name} = source.{field.Name};//{field.Caption.Replace('\n', ' ')}");
            }
            foreach (var field in upgradeConfig.Fields.Values)
            {
                if (!field.IsJsonAttribute)
                    continue;
                code.Append($@"
            {field.Name} = source.{field.Name};//{field.Caption.Replace('\n', ' ')}");
            }
        }

        #endregion
    }
}