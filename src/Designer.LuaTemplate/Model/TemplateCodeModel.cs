// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 配置:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// 建立:2014-11-20
// 修改:2014-11-29
// *****************************************************/

#region 引用

using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Agebull.Common;
using Agebull.Common.LUA;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.RobotCoder.CodeTemplate;
using Agebull.EntityModel.RobotCoder.CodeTemplate.LuaTemplate;
using Application = System.Windows.Application;
using Clipboard = System.Windows.Clipboard;
using Color = System.Drawing.Color;
using MessageBox = System.Windows.MessageBox;

#endregion

namespace Agebull.EntityModel.Designer
{
    internal class TemplateCodeModel : DesignModelBase
    {
        #region 基础

        public TemplateCodeModel()
        {
            CreateTree();
        }

        protected override void DoInitialize()
        {
            base.DoInitialize();
            LoadTemplates(null);
        }


        private ConfigCollection<TemplateClassify> Templates { get; } = new ConfigCollection<TemplateClassify>();

        private int _tabIndex;
        private string _classify;
        private TemplateConfig _currentTemplateConfig;

        public TemplateConfig CurrentTemplateConfig
        {
            get => _currentTemplateConfig;
            set
            {
                if (_currentTemplateConfig == value)
                    return;
                _currentTemplateConfig = value;
                RaisePropertyChanged(nameof(CurrentTemplateConfig));
                if (value != null)
                {
                    Classify = _currentTemplateConfig?.Classify;
                    CheckTemplate(_currentTemplateConfig);
                }
                _currentTemplateConfig = value;
                CurrentTemplate = value?.Template;
                CurrentLua = value?.Code;
            }
        }

        /// <summary>
        /// 分类
        /// </summary>
        /// 
        public string Classify
        {
            get => _classify;
            set
            {
                if (_classify == value)
                    return;
                _classify = value;
                RaisePropertyChanged(nameof(Classify));
            }
        }

        private string _extendCode;

        public string ExtendCode
        {
            get => _extendCode;
            set
            {
                if (Equals(_extendCode, value))
                    return;
                _extendCode = value;
                RaisePropertyChanged(() => ExtendCode);
                TabIndex = 2;
            }
        }


        #endregion

        #region 系统命令

        /// <summary>
        /// 生成命令对象
        /// </summary>
        /// <param name="commands"></param>
        protected override void CreateCommands(NotificationList<CommandItemBase> commands)
        {
            commands.AddRange(new CommandItemBase[]
            {
                new CommandItem
                {
                    Action = CreateNew,
                    Caption = "新增模板",
                    Image = Application.Current.Resources["img_add"] as ImageSource
                },
                new CommandItem
                {
                    Action = SaveTemplate,
                    Caption = "代码解析",
                    Image = Application.Current.Resources["img_save"] as ImageSource
                },
                new CommandItem
                {
                    Action = CheckTemplate,
                    Caption = "代码检查",
                    Image = Application.Current.Resources["img_flush"] as ImageSource
                },
                new CommandItem
                {
                    Action = DeleteTemplate,
                    Caption = "删除模板",
                    Image = Application.Current.Resources["img_del"] as ImageSource
                },
                new AsyncCommandItem<TemplateConfig, string>(RunLuaPrepare, RunLua, RunLuaEnd)
                {
                    NoConfirm=true,
                    Caption = "生成代码",
                    Image = Application.Current.Resources["img_code"] as ImageSource
                },
                new CommandItem
                {
                    NoConfirm=true,
                    Action =arg=>Clipboard.SetText(ExtendCode ?? ""),
                    Caption = "复制代码",
                    Image = Application.Current.Resources["img_file"] as ImageSource
                },
                new CommandItem
                {
                    Action = LoadTemplates,
                    Caption = "重新载入",
                    Image = Application.Current.Resources["img_flush"] as ImageSource
                }
            });
        }

        #endregion
        #region 树
        public TreeRoot LuaTreeRoot { get; private set; }

        public TreeRoot TemplateTreeRoot { get; private set; }

        private void CreateTree()
        {
            TemplateTreeRoot = new TreeRoot
            {
                CreateChildFunc = CreateClassifyTreeItem,
                FriendItems = Templates
            };
            TemplateTreeRoot.SelectItemChanged += OnTemplateTreeSelectItemChanged;
            LuaTreeRoot = new TreeRoot
            {
                CreateChildFunc = CreateLuaTreeItem,
                FriendItems = Elements
            };
            LuaTreeRoot.SelectItemChanged += OnLuaTreeSelectItemChanged;
        }
        #region 语法树

        public readonly NotificationList<AnalyzeBlock> Elements = new NotificationList<AnalyzeBlock>();


        private void CreateLuaTree()
        {
            Task.Factory.StartNew(DoWordSplit, CurrentTemplate);
        }

        private void DoWordSplit(object arg)
        {
            var spliter = new LuaWordSpliter();
            spliter.SplitTemplateWords((string)arg);

            LuaWordTypeAnalyzer.Analyze(spliter.Root, spliter.Config);
            BeginInvokeInUiThread(() =>
            {
                if (Elements.Count > 0)
                    Elements.Clear();
                Elements.Add(spliter.Root);
                LangEditor.ShowErrorWords(spliter.Words);
            });
        }

        public void DoLuaAnalyze(object arg)
        {
            string text = (string)arg;
            WordAnalyze analyze = new WordAnalyze();
            analyze.Reset(text);
            analyze.Analyze();

            TemplateParse parser = new TemplateParse
            {
                WordElements = analyze.Elements,
                Config = CurrentTemplateConfig
            };
            parser.Analyze();
            var la = new LuaAnalyzer
            {
                TemplateElements = parser.TemplateElements
            };
            la.DoAnalyze(parser.TemplateElements, CurrentTemplateConfig);

            BeginInvokeInUiThread(() =>
            {
                if (Elements.Count > 0)
                    Elements.Clear();
                Elements.Add(la.Root);
                LangEditor.ShowErrorWords(la.Words);
            });
            //
            //Trace.WriteLine(analyzer.PrintTree(analyzer.Root));
        }

        private void OnLuaTreeSelectItemChanged(object sender, EventArgs e)
        {
            if (!(sender is TreeItem<AnalyzeUnitBase> value))
                return;
            SelectObject = value.Model;
            if (value.Model.Start < 0 || value.Model.Lenght < 1)
                return;
            if (TabIndex != 3)
                TabIndex = 0;
            LangEditor.SelectionBackColor = Color.White;
            LangEditor.Select(value.Model.Start, value.Model.Lenght);
            LangEditor.SelectionBackColor = Color.Silver;
        }
        private TreeItem CreateLuaTreeItem(object arg)
        {
            if (arg is AnalyzeBlock block)
            {
                return new TreeItem<AnalyzeUnitBase>(block)
                {
                    IsExpanded = block.ItemRace > CodeItemRace.Value,
                    Header = $"{block.Name ?? block.Word ?? "$"}{(block.IsError ? "×" : "√")}{block.ValueType}",
                    CreateChildFunc = CreateLuaTreeItem,
                    FriendItems = block.Elements,
                    SoruceTypeIcon = Application.Current.Resources["tree_Folder"] as BitmapImage
                };
            }

            if (arg is WordUnit unit)
            {
                return new TreeItem<AnalyzeUnitBase>(unit)
                {
                    Header = unit.IsKeyWord
                        ? unit.Word
                        : $"{unit.Name ?? unit.Word ?? "_root_"}{(unit.IsError ? "×" : "√")}{unit.ValueType}",
                    SoruceTypeIcon = Application.Current.Resources["img_file"] as BitmapImage
                };
            }
            return new TreeItem("???")
            {
                SoruceTypeIcon = Application.Current.Resources["img_file"] as BitmapImage
            };
        }

        #endregion

        #region 模板树

        private void OnTemplateTreeSelectItemChanged(object sender, EventArgs e)
        {
            switch (sender)
            {
                case TreeItem<TemplateConfig> value:
                    CurrentTemplateConfig = value.Model;
                    SelectObject = value.Model;
                    break;
                case TreeItem item:
                    Classify = item.Header;
                    break;
            }
        }

        private TreeItem CreateClassifyTreeItem(object arg)
        {
            var classify = (TemplateClassify)arg;
            return new TreeItem<TemplateClassify>(classify)
            {
                IsExpanded = true,
                Header = classify.Name,
                HeaderField = "Name",
                HeaderExtendExpression = p => p.Name,
                CreateChildFunc = CreateTemplateTreeItem,
                FriendItems = classify.Templates,
                SoruceTypeIcon = Application.Current.Resources["tree_Folder"] as BitmapImage
            };
        }

        private TreeItem CreateTemplateTreeItem(object arg)
        {
            var template = (TemplateConfig)arg;
            return new TreeItem<TemplateConfig>(template)
            {
                IsExpanded = true,
                Header = template.Caption,
                HeaderField = "Caption",
                HeaderExtendExpression = p => p.Caption,
                SoruceTypeIcon = Application.Current.Resources["img_file"] as BitmapImage
            };
        }

        #endregion

        #endregion

        #region 载入模板文件

        private void LoadTemplates(object arg)
        {
            Templates.Clear();
            var path = GlobalConfig.CheckPath(GlobalConfig.ProgramRoot, "Templates");
            var folders = Directory.GetDirectories(path);
            foreach (var folder in folders)
            {
                var cla = new TemplateClassify
                {
                    Name = Path.GetFileName(folder)
                };
                foreach (var file in IOHelper.GetAllFiles(folder, "*.lt"))
                {
                    cla.Templates.Add(new TemplateConfig
                    {
                        Caption = Path.GetFileNameWithoutExtension(file),
                        Classify = cla.Name,
                        TemplatePath = file
                    });
                }
                Templates.Add(cla);
            }
        }


        #endregion

        #region 预处理模板文件

        private void CheckTemplate(TemplateConfig template)
        {
            if (string.IsNullOrWhiteSpace(template.Template))
            {
                template.Template = File.ReadAllText(template.TemplatePath);
            }
            if (string.IsNullOrWhiteSpace(template.Code))
            {
                LuaTemplateParse parser = new LuaTemplateParse
                {
                    Config = template
                };
                parser.Compile();
            }
        }

        #endregion


        #region LUA执行支持



        internal bool RunLuaPrepare(TemplateConfig config)
        {
            if (string.IsNullOrWhiteSpace(CurrentTemplateConfig.Template))
            {
                MessageBox.Show("模板内容为空");
                return false;
            }
            if (!string.IsNullOrWhiteSpace(CurrentTemplateConfig.ModelType) &&
                (Context.SelectConfig == null ||
                Context.SelectConfig.GetTypeName() != CurrentTemplateConfig.ModelType))
            {
                MessageBox.Show($"模板运行需要当前对象为{CurrentTemplateConfig.ModelType}类型");
                return false;
            }
            return true;
        }


        public string RunLua(TemplateConfig config)
        {
            var executer = new TemplateExecuter();
            var lua = executer.InitLua(Context.SelectConfig);
            lua.DoString(CurrentTemplateConfig.Code);
            lua.DoString($"{config.Name}()");
            var code = executer.ResultCode;
            return code != null ? code.FromLuaChar() : string.Empty;
        }

        internal void RunLuaEnd(CommandStatus status, Exception ex, string code)
        {
            ExtendCode = status != CommandStatus.Succeed ? ex?.ToString() : code;
            TabIndex = 2;
        }


        public int TabIndex
        {
            get => _tabIndex;
            set
            {
                if (_tabIndex == value)
                    return;
                _tabIndex = value;
                RaisePropertyChanged(nameof(TabIndex));
            }
        }

        #endregion

        #region 编辑

        private void CreateNew(object arg)
        {
            CurrentTemplateConfig = new TemplateConfig
            {
                Classify = Classify,
                Template = BaseTemplate
            };
            TabIndex = 0;
        }

        private void SaveTemplate(object arg)
        {
            if (string.IsNullOrWhiteSpace(CurrentTemplate))
            {
                MessageBox.Show("模板内容不能为空");
                return;
            }
            CurrentTemplateConfig.Template = CurrentTemplate;
            try
            {
                LuaTemplateParse parser = new LuaTemplateParse
                {
                    Config = CurrentTemplateConfig
                };
                parser.Compile();
            }
            catch (Exception e)
            {
                ExtendCode = e.ToString();
                TabIndex = 2;
                MessageBox.Show("模板解析发生错误");
                return;
            }
            CurrentLua = CurrentTemplateConfig.Code;
            CurrentTemplate = CurrentTemplateConfig.Template;

            var cl = CurrentTemplateConfig.Classify;

            if (CurrentTemplateConfig.TemplatePath != null)
                File.WriteAllText(CurrentTemplateConfig.TemplatePath, CurrentTemplateConfig.Template);

            if (string.IsNullOrWhiteSpace(CurrentTemplateConfig.Name))
            {
                MessageBox.Show("必须有一个名称(Name)");
                return;
            }
            if (string.IsNullOrWhiteSpace(CurrentTemplateConfig.Caption))
            {
                MessageBox.Show("必须有一个标题(Caption)");
                return;
            }
            if (string.IsNullOrWhiteSpace(CurrentTemplateConfig.Classify))
            {
                MessageBox.Show("必须有一个分类(Classify)");
                return;
            }
            if (cl != null && cl != CurrentTemplateConfig.Classify)
            {
                var cla = Templates.FirstOrDefault(p => p.Name == cl);
                cla?.Templates.Remove(CurrentTemplateConfig);
            }
            var ncla = Templates.FirstOrDefault(p => p.Name == CurrentTemplateConfig.Classify);
            if (ncla == null)
            {
                Templates.Add(ncla = new TemplateClassify { Name = CurrentTemplateConfig.Classify });
            }
            if (!ncla.Templates.Contains(CurrentTemplateConfig))
            {
                ncla.Templates.Add(CurrentTemplateConfig);
            }
            string path = GlobalConfig.CheckPath(GlobalConfig.ProgramRoot, "Templates", CurrentTemplateConfig.Classify);
            path = Path.Combine(path, CurrentTemplateConfig.Caption + ".lt");
            if (!string.IsNullOrWhiteSpace(CurrentTemplateConfig.TemplatePath) &&
                !string.Equals(CurrentTemplateConfig.TemplatePath, path, StringComparison.OrdinalIgnoreCase) &&
                File.Exists(CurrentTemplateConfig.TemplatePath))
            {
                File.Delete(CurrentTemplateConfig.TemplatePath);
            }
            CurrentTemplateConfig.TemplatePath = path;
            File.WriteAllText(path, CurrentTemplateConfig.Template);

        }


        private void CheckTemplate(object arg)
        {
            if (CurrentTemplateConfig == null)
                return;
            CurrentTemplateConfig.Template = CurrentTemplate;
            CurrentTemplateConfig.Code = CurrentLua;
            if (CurrentTemplateConfig.TemplatePath != null)
            {
                File.WriteAllText(CurrentTemplateConfig.TemplatePath, CurrentTemplateConfig.Template);
            }
            CurrentTemplate = CurrentTemplateConfig.Template;
            CurrentLua = CurrentTemplateConfig.Code;


            CreateLuaTree();
        }

        private void DeleteTemplate(object arg)
        {
            if (CurrentTemplateConfig == null ||
                MessageBox.Show("确认删除此模板吗?", "模板编辑", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }
            if (!string.IsNullOrWhiteSpace(CurrentTemplateConfig.TemplatePath) &&
                File.Exists(CurrentTemplateConfig.TemplatePath))
            {
                File.Delete(CurrentTemplateConfig.TemplatePath);
            }
            var par = TemplateTreeRoot.SelectItem.Parent as TreeItem<TemplateClassify>;
            par?.Model.Templates.Remove(CurrentTemplateConfig);
            CurrentTemplateConfig = null;
        }

        #endregion

        #region 基础模板内容

        private const string BaseTemplate = @"@#
	名称 : funcName
	标题 : 模板
	类别 : 默认
	模板说明 : 模板说明
	目标语言 : C#
	参数类型 : Project/Entity/Projct/Solution/Enum/Typedef/Command之一
	执行条件 : 
	保存路径 : 
#@";
        #endregion

        #region 属性表格

        public string CurrentTemplate
        {
            get => LangEditor?.Text;
            set
            {
                if (LangEditor != null)
                {
                    LangEditor.Text = value;
                    LangEditor.ShowWords(LuaWordSpliter.Do(value));
                }
            }
        }

        private LanguageEditor LangEditor { get; set; }


        public DependencyAction TemplateBehavior => new DependencyAction
        {
            AttachAction = obj =>
            {
                var host = (WindowsFormsHost)obj;
                LangEditor = (LanguageEditor)host.Child;
                LangEditor.AutoAnalyze = false;
            }
        };

        public string CurrentLua
        {

            get => LuaEditor?.Text;
            set
            {
                if (LuaEditor != null)
                    LuaEditor.Text = value;
            }
        }

        private LanguageEditor LuaEditor { get; set; }


        public DependencyAction LuaEditorBehavior => new DependencyAction
        {
            AttachAction = obj =>
            {
                var host = (WindowsFormsHost)obj;
                LuaEditor = (LanguageEditor)host.Child;
                LuaEditor.IsLua = true;
            }
        };

        private object SelectObject
        {
            set => PropertyGrid.SelectedObject = value;
        }
        protected PropertyGrid PropertyGrid;
        public DependencyAction PropertyGridBehavior => new DependencyAction
        {
            AttachAction = obj =>
            {
                var host = (WindowsFormsHost)obj;
                PropertyGrid = (PropertyGrid)host.Child;
            }
        };


        #endregion
    }
}