using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Gboxt.Common.DataAccess.Schemas;

namespace Agebull.Common.SimpleDesign
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class EntityMomentCoder : MomentCoderBase, IAutoRegister
    {
        #region 注册

        /// <summary>
        /// 注册代码
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            MomentCoder.RegisteCoder("WPF","表格列", cfg => Run(cfg, Views));
        }
        #endregion

        #region 测试视图

        public static string Views(ConfigBase config)
        {
            EntityConfig entityConfig = (EntityConfig)config;
            var code = new StringBuilder();
            foreach (var field in entityConfig.PublishProperty.Where(p => !p.DenyClient))
            {
                var ty = field.CppTypeObject as TypedefItem;

                if (ty != null && ty.Items.Count > 0)
                    code.AppendFormat(@"        
                <DataGridTextColumn Binding = ""{{Binding {0}2}}"" Header = ""{1}"" />", field.Name, field.Caption);
                else
                    code.AppendFormat(@"        
                <DataGridTextColumn Binding = ""{{Binding {0}}}"" Header = ""{1}"" />", field.Name, field.Caption);
            }
            return code.ToString();
            //var code = new StringBuilder();
            //code.AppendFormat(@"        
            //<TabItem Header=""{1}"">
            //    <Border BorderThickness = ""1"" Margin = ""0"" Padding = ""0"">
            //        <DataGrid AutoGenerateColumns = ""True"" FontSize = ""14""
            //                          CanUserAddRows = ""False""
            //                          CanUserDeleteRows = ""False""
            //                          DataContext = ""{{Binding Model}}""
            //                          ItemsSource = ""{{Binding List_{0}}}"">
            //            <i:Interaction.Behaviors>
            //                <behaviors:DataGridGenertColumnsBehavior />
            //            </i:Interaction.Behaviors>
            //        </DataGrid>
            //    </Border>
            //</TabItem> "
            //, entityConfig.Name
            //, entityConfig.Caption);
            //return code.ToString();
        }

        public static string ModelLists(ConfigBase config)
        {
            EntityConfig entityConfig = (EntityConfig)config;
            var code = new StringBuilder();
            code.AppendFormat(@"
        /// <summary>
        /// {1}
        /// </summary>
       public ObservableCollection<{0}> List_{0} {{ get; set; }} = new ObjectCollection<{0}>();"
            , entityConfig.Name
            , entityConfig.Caption);
            return code.ToString();
        }
        public static string SwitchType(ConfigBase config)
        {
            EntityConfig entityConfig = (EntityConfig)config;
            var code = new StringBuilder();
            code.AppendFormat(@"
                case ""{0}""://{1}
                    Sync(rKey, JsonConvert.DeserializeObject<{0}>(json));
                    return; "
            , entityConfig.Name
            , entityConfig.Caption);
            return code.ToString();
        }

        public static string AddFunction(ConfigBase config)
        {
            EntityConfig entityConfig = (EntityConfig)config;
            var code = new StringBuilder();
            code.AppendFormat(@"
        /// <summary>
        /// {1}
        /// </summary>
        void Sync(string rKey, {0} data)
        {{
            if (data == null)
                return;
            try
            {{
                var old = List_{0}.FirstOrDefault(p => p.key == rKey);
                if (old == null)
                {{
                    data.key = rKey;
                    Dispatcher.Invoke(() => List_{0}.Add(data));
                    return;
                }}
                Dispatcher.Invoke(() => old.CopyValue(data));
            }}
            catch (Exception ex)
            {{
                Trace.WriteLine(ex);
            }}
        }}"
            , entityConfig.Name
            , entityConfig.Caption);
            return code.ToString();
        }

        #endregion

    }
}