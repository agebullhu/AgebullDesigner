using Agebull.Common;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer.NewConfig;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 实体配置相关模型
    /// </summary>
    public class WordMapModel : DesignModelBase, IGridSelectionBinding
    {
        #region 选中对象

        private IList _selectColumns;

        /// <summary>
        ///     当前选择
        /// </summary>
        public IList SelectColumns
        {
            get => _selectColumns;
            set
            {
                if (Equals(_selectColumns, value))
                    return;
                _selectColumns = value;
                RaisePropertyChanged(() => SelectColumns);
            }
        }
        #endregion

        #region 字典
        static WordMapModel()
        {
            wordItems.CollectionChanged += WordItems_CollectionChanged;
        }
        public static readonly Dictionary<string, WordItem> Maps = new Dictionary<string, WordItem>(StringComparer.OrdinalIgnoreCase);

        static readonly NotificationList<WordItem> wordItems = new NotificationList<WordItem>();

        private static void WordItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            static void Reset()
            {
                Maps.Clear();
                foreach (var item in wordItems)
                {
                    if (string.IsNullOrWhiteSpace(item.English))
                        continue;
                    if (!Maps.ContainsKey(item.English))
                        Maps.Add(item.English, item);
                    else Maps[item.English] = item;
                    if (!Maps.ContainsKey(item.Chiness))
                        Maps.Add(item.Chiness, item);
                    else Maps[item.Chiness] = item;
                }
            }

            void Add()
            {
                foreach (var item in e.NewItems.Cast<WordItem>())
                {
                    if (string.IsNullOrWhiteSpace(item.English))
                        continue;
                    if (!Maps.ContainsKey(item.English))
                        Maps.Add(item.English, item);
                    else Maps[item.English] = item;
                    if (!Maps.ContainsKey(item.Chiness))
                        Maps.Add(item.Chiness, item);
                    else Maps[item.Chiness] = item;
                }
            }
            void Remove()
            {
                foreach (var item in e.OldItems.Cast<WordItem>())
                {
                    if (string.IsNullOrWhiteSpace(item.English))
                        continue;
                    Maps.Remove(item.English);
                }
            }
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    Add();
                    break;
                case NotifyCollectionChangedAction.Remove:
                    Remove();
                    break;
                case NotifyCollectionChangedAction.Move:
                    Remove();
                    Add();
                    break;
                case NotifyCollectionChangedAction.Replace:
                case NotifyCollectionChangedAction.Reset:
                    Reset();
                    break;
            }
        }

        #endregion
        #region 查找

        static bool FindChiness(string eng, string next, out (bool joinNext, string chiness) ch)
        {
            if (next != null)
            {
                if (FindChiness(eng + next, out var chiness))
                {
                    ch = (true, chiness);
                    return true;
                }
            }
            {
                if (FindChiness(eng, out var chiness))
                {
                    ch = (false, chiness);
                    return true;
                }
                ch = (false, null);
            }

            return false;
        }

        static bool FindChiness(string eng, out string chiness)
        {
            if (Maps.TryGetValue(eng, out var item))
            {
                chiness = item.Chiness;
                return true;
            }
            if (eng.EndsWith("ies") && Maps.TryGetValue(eng.Substring(0, eng.Length - 3) + "y", out item))
            {
                chiness = item.Chiness;
                return true;
            }
            if (eng.EndsWith("es") &&
                (Maps.TryGetValue(eng.Substring(0, eng.Length - 3) + "e", out item) ||
                Maps.TryGetValue(eng.Substring(0, eng.Length - 3), out item)))
            {
                chiness = item.Chiness;
                return true;
            }

            if ((eng.EndsWith("ed") || eng.EndsWith("es")) &&
                (Maps.TryGetValue(eng.Substring(0, eng.Length - 3) + "e", out item) ||
                Maps.TryGetValue(eng.Substring(0, eng.Length - 3), out item)))
            {
                chiness = item.Chiness;
                return true;
            }

            if (eng.EndsWith("ing") &&
                (Maps.TryGetValue(eng.Substring(0, eng.Length - 3), out item)))
            {
                chiness = item.Chiness;
                return true;
            }

            if (eng.EndsWith("s") && Maps.TryGetValue(eng.Substring(0, eng.Length - 3), out item))
            {
                chiness = item.Chiness;
                return true;
            }
            chiness = null;
            return false;
        }

        #endregion
        #region 编辑

        public NotificationList<WordItem> WordItems => wordItems;

        public override void CreateCommands(IList<CommandItemBase> commands)
        {
            commands.Append(new CommandItem
            {
                Name = "Reload",
                IsButton = true,
                Caption = "重新载入",
                Action = p => Reload()
            },
            new CommandItem
            {
                Name = "New",
                Caption = "新增",
                IsButton = true,
                Action = arg => NewWord()
            },
            new CommandItem
            {
                Name = "Remove",
                Caption = "清理并保存",
                IsButton = true,
                Action = arg => Clear(wordItems.Count == 0 ? null : wordItems.ToList(), true)
            },
            new CommandItem
            {
                IsButton = true,
                Action = DeleteColumns,
                Caption = "删除选中",
                Image = Application.Current.Resources["img_del"] as ImageSource
            });
            base.CreateCommands(commands);
        }
        internal static void NewWord()
        {
            var window = new NewConfigWindow
            {
                Title = "新增字典"
            };
            var vm = (NewConfigViewModel)window.DataContext;
            vm.Config = new ConfigBase();
            if (window.ShowDialog() == true)
            {
                AddMap(vm.Config);
            }
        }

        internal static void AddMap(ConfigBase cfg)
        {
            if (Maps.TryGetValue(cfg.Name, out var item))
            {
                item.Chiness = cfg.Caption;
                item.Description = cfg.Description;
            }
            else
            {
                wordItems.Insert(0, new WordItem
                {
                    Chiness = cfg.Caption,
                    English = cfg.Name,
                    Description = cfg.Description
                });
            }
            Save();
        }

        public static void Reload()
        {
            try
            {
                var file = Path.Combine(GlobalConfig.RootPath, "Config", "words.json");
                if (!File.Exists(file))
                    return;
                var json = File.ReadAllText(file);
                var items = JsonConvert.DeserializeObject<List<WordItem>>(json);
                Clear(items, true);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
        }

        public static void Save()
        {
            try
            {
                var file = Path.Combine(GlobalConfig.RootPath, "Config", "words.json");
                var json = JsonConvert.SerializeObject(wordItems);
                File.WriteAllText(file, json);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
        }
        public void DeleteColumns(object arg)
        {
            if (SelectColumns == null || SelectColumns.Count == 0 ||
                MessageBox.Show("确认删除吗?", "对象编辑", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }
            foreach (var col in SelectColumns.OfType<WordItem>().ToArray())
            {
                wordItems.Remove(col);
            }
            SelectColumns = null;
        }

        static void Clear(List<WordItem> items, bool save = false)
        {
            wordItems.Clear();
            Maps.Clear();
            if (items != null)
                foreach (var item in items)
                {
                    if (string.IsNullOrWhiteSpace(item.Chiness) || string.IsNullOrEmpty(item.English))
                        continue;
                    item.Chiness = item.Chiness.Trim();
                    item.English = item.English.Trim();
                    if (!Maps.ContainsKey(item.English))
                        wordItems.Add(item);
                }
            if (save)
                Save();
        }
        #endregion
        #region 菜单

        /// <summary>
        ///     清除标题
        /// </summary>
        public static void ClearCaption(ConfigBase item)
        {
            item.Caption = null;
            item.Description = null;
        }

        /// <summary>
        ///     英译中
        /// </summary>
        public static void NameToEnglish(ConfigBase item)
        {
            if (string.IsNullOrWhiteSpace(item.Name))
                return;
            item.Name = ToEnglish(item.Name);
            if (string.IsNullOrWhiteSpace(item.Caption))
                item.Caption = null;
        }


        /// <summary>
        ///     英译中
        /// </summary>
        public static void CaptionToChiness(ConfigBase item)
        {
            if (string.IsNullOrWhiteSpace(item.Caption))
                return;
            item.Caption = ToChiness(item.Caption);
        }

        internal static void NameToCaption(ConfigBase config)
        {
            if (string.IsNullOrWhiteSpace(config.Name))
            {
                return;
            }
            config.Caption = ToChiness(config.Name);
        }

        internal static void ToChiness(ConfigBase config)
        {
            if (string.IsNullOrWhiteSpace(config.Caption))
            {
                config.Caption = ToChiness(config.Name);
            }
        }


        internal static void Name2CaptionChiness(ConfigBase config)
        {
            if (string.IsNullOrWhiteSpace(config.Name))
            {
                if (!string.IsNullOrWhiteSpace(config.Caption) && config.Caption[0] < 128)
                    config.Name = ToChiness(config.Caption);
            }
            else if (config.Name[0] < 128)
                config.Caption = ToChiness(config.Name);
        }
        #endregion
        #region 翻译

        /// <summary>
        /// 翻译中文到英文
        /// </summary>
        /// <param name="str">中文</param>
        /// <returns>英文</returns>
        static string ToChiness(string str)
        {
            try
            {
                if (FindChiness(str, out var mi))
                {
                    return mi;
                }

                var w = GlobalConfig.ToWords(str);
                var words = new List<string>();
                StringBuilder sb = new StringBuilder();
                w.ForEach(p =>
                {
                    if (p.Length == 1)
                    {
                        sb.Append(p);
                    }
                    else
                    {
                        if (sb.Length > 0)
                        {
                            words.Add(sb.ToString());
                            sb = new StringBuilder();
                        }
                        words.Add(p);
                    }
                });
                if (sb.Length > 0)
                {
                    words.Add(sb.ToString());
                }
                sb = new StringBuilder();
                for (var idx = 0; idx < words.Count; idx++)
                {
                    var word = words[idx];
                    if (!FindChiness(word, idx + 1 == words.Count ? null : words[idx + 1], out var res))
                        sb.Append(BaiduTranslator.Translate(word, false));
                    else
                    {
                        sb.Append(res.chiness);
                        if (res.joinNext)
                            idx++;
                    }
                }
                return sb.ToString();
                //Console.WriteLine(txt);
                //foreach (var ch in txt)
                //{
                //    if (ch != ' ' && ch < 'z')
                //        return BaiDuFanYi(sb.ToString(), false).Replace(" ", "");
                //}
                //return txt.Replace(" ", "");
            }
            catch (Exception exception)
            {
                Trace.WriteLine($@"通过百度翻译出错:{exception }");
                return str;
            }
        }

        /// <summary>
        /// 翻译中文到英文
        /// </summary>
        /// <param name="str">英文</param>
        /// <returns>中文</returns>
        static string ToEnglish(string str)
        {
            try
            {
                if (Maps.TryGetValue(str, out var item))
                {
                    return item.English;
                }

                var words = GlobalConfig.ToWords(str);
                StringBuilder sb = new StringBuilder();
                words.ForEach(word =>
                {
                    if (Maps.TryGetValue(word, out var i))
                        sb.Append(i.English);
                    else
                        sb.Append(BaiduTranslator.Translate(word, true));
                });
                return sb.ToString();
            }
            catch (Exception exception)
            {
                Trace.WriteLine($@"通过百度翻译出错:{exception }");
                return str;
            }
        }

        #endregion
    }
}
