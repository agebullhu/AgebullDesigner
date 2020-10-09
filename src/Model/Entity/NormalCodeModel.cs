// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 配置:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// 建立:2014-11-20
// 修改:2014-11-29
// *****************************************************/

#region 引用

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.RobotCoder;
using Cmd = System.Collections.Generic.Dictionary<string, System.Func<System.Action<System.Collections.Generic.Dictionary<string, string>>, Agebull.Common.Mvvm.CommandItemBase>>;
#endregion

namespace Agebull.EntityModel.Designer
{
    public class NormalCodeModel : DesignModelBase
    {
        /// <summary>
        ///     分类
        /// </summary>
        public NormalCodeModel()
        {
            EditorName = "Code";
        }

        #region 操作命令

        /// <summary>
        /// 生成命令对象
        /// </summary>
        /// <param name="commands"></param>
        protected override void CreateCommands(NotificationList<CommandItemBase> commands)
        {
            commands.Add(new CommandItem
            {
                IsButton = true,
                Action = arg => DoMomentCode(),
                Caption = "生成代码片断",
                Image = Application.Current.Resources["img_file"] as ImageSource
            });

            commands.Add(new CommandItem
            {
                IsButton = true,
                Action = arg => CopyCode(),
                Caption = "复制代码",
                Image = Application.Current.Resources["img_file"] as ImageSource
            });
            if (!Builders.TryGetValue(Context.SelectTag ?? "", out var cmd))
                return;
            foreach (var builder in cmd.Values)
            {
                commands.Add(builder(OnCodeSuccess));
            }
        }

        /// <summary>
        /// 注册的项目代码生成器
        /// </summary>
        static readonly Dictionary<string, Cmd> Builders =
            new Dictionary<string, Cmd>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 注册的项目生成器
        /// </summary>
        /// <returns></returns>
        public static void RegistBuilder<TBuilder, TModelConfig>()
            where TBuilder : ProjectBuilder<TModelConfig>, new()
            where TModelConfig : ProjectChildConfigBase, IEntityConfig
        {
            var builder = new TBuilder();
            var type = typeof(TModelConfig).Name;
            if (!Builders.TryGetValue(type, out var cmd))
                Builders.Add(type, cmd = new Cmd());

            if (cmd.ContainsKey(builder.Name))
                throw new ArgumentException("已注册名称为" + builder.Name + "的项目生成器，不应该重复注册");
            cmd.Add(builder.Name, OnCodeSuccess => new ProjectCodeCommand<TModelConfig>(() => new TBuilder())
            {
                Caption = builder.Caption,
                IconName = builder.Icon,
                OnCodeSuccess = OnCodeSuccess
            }.ToCommand(null));
        }

        #endregion

        #region 文件代码

        /// <summary>
        /// 代码命令树根
        /// </summary>
        public TreeRoot FileTreeRoot { get; } = new TreeRoot();

        internal WebBrowser Browser;


        private void OnCodeSuccess(Dictionary<string, string> fields)
        {
            FileTreeRoot.SelectItemChanged -= OnFileSelectItemChanged;
            FileTreeRoot.ClearItems();
            if (fields == null)
                return;
            int first = SolutionConfig.Current.RootPath == null ? 0 : SolutionConfig.Current.RootPath.Length;
            foreach (var file in fields)
            {
                string name = Path.GetFileName(file.Key);
                string path = Path.GetDirectoryName(file.Key);
                var folder = path?.Substring(first, path.Length - first) ?? "未知目录";
                folder = folder.Trim('\\', '/');
                var item = FileTreeRoot.Items.FirstOrDefault(p => p.Name == folder);
                if (item == null)
                {
                    FileTreeRoot.Items.Add(item = new TreeItem(file.Key)
                    {
                        Header = folder,
                        Name = folder,
                        IsExpanded = true,
                        SoruceTypeIcon = Application.Current.Resources["tree_Folder"] as BitmapImage
                    });
                }
                item.Items.Add(new TreeItem<SimpleConfig>(new SimpleConfig
                {
                    Name = name,
                    Remark = file.Value,
                })
                {
                    Header = name,
                    Name = name,
                    Tag = Path.GetExtension(file.Key)?.Trim('.'),
                    SoruceTypeIcon = Application.Current.Resources["img_code"] as BitmapImage
                });
            }

            ViewIndex = 1;
            FileTreeRoot.SelectItemChanged += OnFileSelectItemChanged;
        }

        private void OnFileSelectItemChanged(object sender, EventArgs e)
        {
            var value = sender as TreeItem<SimpleConfig>;
            _codeType = value?.Tag ?? ".cs";
            switch (_codeType.ToLower().Trim('.'))
            {
                case "h":
                    _codeType = "cpp";
                    break;
                //case "md":
                //    _codeType = "makedown";
                //    break;
                case "aspx":
                case "htm":
                case "html":
                    _codeType = "xml";
                    break;
            }
            ExtendCode = value?.Model.Remark;
        }

        #endregion

        #region 树形菜单

        /// <summary>
        /// 代码命令树根
        /// </summary>
        public TreeRoot MomentTreeRoot { get; } = new TreeRoot();

        /// <summary>
        /// 初始化
        /// </summary>
        protected override void DoInitialize()
        {
            base.DoInitialize();

            foreach (var clasf in MomentCoder.Coders)
            {
                if (clasf.Value == null || clasf.Value.Count == 0)
                    continue;
                TreeItemBase parent = new TreeItem(clasf.Key)
                {
                    IsExpanded = false,
                    ItemsState = 3,
                    SoruceTypeIcon = Application.Current.Resources["tree_Folder"] as BitmapImage
                };
                MomentTreeRoot.Items.Add((TreeItem)parent);
                foreach (var item in clasf.Value)
                {
                    parent.Items.Add(new TreeItem<CoderDefine>(item.Value)
                    {
                        Header = item.Key,
                        ItemsState = 3,
                        SoruceTypeIcon = Application.Current.Resources["img_code"] as BitmapImage
                    });
                }
            }
            MomentTreeRoot.SelectItemChanged += OnMomentSelectItemChanged;
        }

        private void OnMomentSelectItemChanged(object sender, EventArgs e)
        {
            if (!(sender is TreeItem<CoderDefine> value))
                return;
            _codeType = value.Model.Lang;
            MomentCodeModel = value.Model.Func;
            if (MomentCodeModel != null)
            {
                DoMomentCode();
            }
            else
            {
                ExtendCode = "无生成器";
            }
        }

        /// <summary>
        /// 选中的代码片断对象的方法体
        /// </summary>
        private Func<ConfigBase, string> MomentCodeModel;

        private int _ViewIndex;
        public int ViewIndex
        {
            get => _ViewIndex;
            set
            {
                if (_ViewIndex == value)
                    return;
                _ViewIndex = value;
                OnPropertyChanged(nameof(ViewIndex));
            }
        }
        #endregion
        #region 代码片断
        /// <summary>
        /// 复制代码
        /// </summary>
        private void CopyCode()
        {
            if (string.IsNullOrWhiteSpace(ExtendCode)) return;
            try
            {
                Clipboard.SetText(ExtendCode);
            }
            catch (Exception e)
            {
                MessageBox.Show("因为【" + e.Message + "】未能复制", "复制代码");
            }
        }

        /// <summary>
        /// 生成的代码片断
        /// </summary>
        private string _codeType = "cs";

        /// <summary>
        /// 生成的代码片断
        /// </summary>
        private string _extendCode;

        /// <summary>
        /// 生成的代码片断
        /// </summary>
        public string ExtendCode
        {
            get => _extendCode;
            set
            {
                if (Equals(_extendCode, value))
                    return;
                _extendCode = value;
                var code = System.Web.HttpUtility.HtmlEncode(value ?? "");
                var html = $@"
<html style='padding:0;margin:0'>
    <head>
        <meta charset='utf-8'/>
        <meta http-equiv='X-UA-Compatible' content='IE=edge'>
        <meta name='viewport' content='width=device-width, initial-scale=1' />
        <meta name='referrer' content='never' />
        <link href='https://cdn.staticfile.org/highlight.js/9.15.10/styles/vs.min.css' rel='stylesheet'>  
        <script src='https://cdn.staticfile.org/jquery/3.5.1/jquery.min.js'></script>
        <script src='https://cdn.bootcss.com/highlight.js/9.13.1/highlight.min.js'></script>  
        <script>
            hljs.initHighlightingOnLoad();
            $(document).ready(function() {{
                    $('pre code').each(function(i, block) {{
                    hljs.highlightBlock(block);
                }});
            }});
        </script>
    <head>
    <body style='padding:0;margin:0'>
        <pre><code class='{_codeType}'>{code}</code></pre>
    </body>
</html>
";
                Browser.NavigateToString(html);
                Editor.ShowCode();
            }
        }

        private void DoMomentCode()
        {
            ViewIndex = 0;
            if (MomentCodeModel == null)
            {
                ExtendCode = "请选择一个生成方法";
                return;
            }
            try
            {
                ExtendCode = MomentCodeModel(Model.Context.SelectConfig);
            }
            catch (Exception e)
            {
                ExtendCode = $"因为【{e.Message}】未能生成对应的代码片断\n{e}";
            }
        }
        #endregion
    }
}