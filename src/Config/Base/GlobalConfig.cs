using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Agebull.Common.DataModel;

namespace Gboxt.Common.DataAccess.Schemas
{
    /// <summary>
    ///     全局配置
    /// </summary>
    public class GlobalConfig : NotificationObject
    {
        #region 类型取得

        /// <summary>
        ///     取得实体对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static EntityConfig GetEntity(string name)
        {
            if (name == null)
                return null;
            if (name.Length > 4 && name.LastIndexOf("Data", StringComparison.Ordinal) == name.Length - 4)
                name = name.Substring(0, name.Length - 4);
            return GetEntity(p => p.Name == name);
        }

        /// <summary>
        ///     取得类型定义对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static TypedefItem GetTypedef(string name)
        {
            return TypedefItems.FirstOrDefault(p => p.Name == name);
        }

        /// <summary>
        ///     取得枚举对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static EnumConfig GetEnum(string name)
        {
            return Enums.FirstOrDefault(p => p.Name == name);
        }

        /// <summary>
        ///     取得工程对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ProjectConfig GetProject(string name)
        {
            return Projects.FirstOrDefault(p => p.Name == name);
        }

        /// <summary>
        ///     查找实体对象
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public static EntityConfig Find(Func<EntityConfig, bool> func)
        {
            return GetEntity(func);
        }

        /// <summary>
        ///     查找类型定义对象
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public static TypedefItem Find(Func<TypedefItem, bool> func)
        {
            return TypedefItems.FirstOrDefault(func);
        }

        /// <summary>
        ///     查找枚举对象
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public static EnumConfig Find(Func<EnumConfig, bool> func)
        {
            return Enums.FirstOrDefault(func);
        }

        /// <summary>
        ///     查找项目对象
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public static ProjectConfig Find(Func<ProjectConfig, bool> func)
        {
            return Projects.FirstOrDefault(func);
        }

        /// <summary>
        ///     查找API对象
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public static ApiItem Find(Func<ApiItem, bool> func)
        {
            return ApiItems.FirstOrDefault(func);
        }

        /// <summary>
        ///     查找通知对象
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public static NotifyItem Find(Func<NotifyItem, bool> func)
        {
            return NotifyItems.FirstOrDefault(func);
        }

        /// <summary>
        ///     查找实体对象
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public static EntityConfig GetEntity(Func<EntityConfig, bool> func)
        {
            return Entities.FirstOrDefault(func);
        }

        /// <summary>
        ///     查找类型定义对象
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public static TypedefItem GetTypedef(Func<TypedefItem, bool> func)
        {
            return TypedefItems.FirstOrDefault(func);
        }

        /// <summary>
        ///     通过标签查找类型定义对象
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static TypedefItem GetTypedefByTag(string tag)
        {
            if (string.IsNullOrWhiteSpace(tag))
                return null;
            var array = tag.Split(',');
            if (array.Length != 2)
                return null;
            return TypedefItems.FirstOrDefault(p => p.Tag == array[0] && p.Name == array[1]);
        }

        /// <summary>
        ///     查找枚举对象
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public static EnumConfig GetEnum(Func<EnumConfig, bool> func)
        {
            return Enums.FirstOrDefault(func);
        }

        /// <summary>
        ///     查找项目对象
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public static ProjectConfig GetProject(Func<ProjectConfig, bool> func)
        {
            return Projects.FirstOrDefault(func);
        }

        /// <summary>
        ///     查找API对象
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public static ApiItem GetApi(Func<ApiItem, bool> func)
        {
            return ApiItems.FirstOrDefault(func);
        }

        /// <summary>
        ///     查找通知对象
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public static NotifyItem GetNotify(Func<NotifyItem, bool> func)
        {
            return NotifyItems.FirstOrDefault(func);
        }

        #endregion

        #region Key查找表

        /// <summary>
        ///     配置查找表
        /// </summary>
        [IgnoreDataMember]
        public static Dictionary<Guid, ConfigBase> ConfigDictionary = new Dictionary<Guid, ConfigBase>();


        /// <summary>
        ///     加入配置
        /// </summary>
        /// <param name="config"></param>
        public static void AddConfig(ConfigBase config)
        {
            if (config.Key == Guid.Empty)
                return;
            if (!ConfigDictionary.ContainsKey(config.Key))
            {
                ConfigDictionary.Add(config.Key, config);
            }
            else
            {
                ConfigDictionary[config.Key] = config;
            }
        }

        /// <summary>
        ///     取得配置对象
        /// </summary>
        /// <typeparam name="TConfig"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TConfig GetConfig<TConfig>(string key)
            where TConfig : ConfigBase
        {
            if (key == null)
                return null;
            var guid = new Guid(key);
            if (guid == Guid.Empty)
                return null;
            ConfigBase config;
            ConfigDictionary.TryGetValue(guid, out config);
            return config as TConfig;
        }

        /// <summary>
        ///     取得配置对象
        /// </summary>
        /// <typeparam name="TConfig"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TConfig GetConfig<TConfig>(Guid key)
            where TConfig : ConfigBase
        {
            if (key == Guid.Empty)
                return null;
            ConfigBase config;
            ConfigDictionary.TryGetValue(key, out config);
            return config as TConfig;
        }

        #endregion

        #region 文本辅助

        /// <summary>
        ///     到首字母大写名称格式
        /// </summary>
        /// <param name="str">旧名称</param>
        /// <returns>新名称</returns>
        public static string ToWordName(string str)
        {
            try
            {
                var words = ToWords(str);
                var sb = new StringBuilder();
                words.ForEach(p => { sb.Append(p.ToUWord()); });
                return sb.ToString();
            }
            catch (Exception)
            {
                return str;
            }
        }

        /// <summary>
        ///     到驼峰名称格式
        /// </summary>
        /// <param name="str">旧名称</param>
        /// <param name="head">名称头</param>
        /// <returns>新名称</returns>
        public static string ToTfWordName(string str, string head = "")
        {
            try
            {
                var words = ToWords(str);
                var sb = new StringBuilder();
                sb.Append(head);
                sb.Append(words[0].ToLower());
                for (var index = 1; index < words.Count; index++)
                {
                    var word = words[index];
                    sb.Append(word.ToUWord());
                }
                return sb.ToString();
            }
            catch (Exception)
            {
                return str;
            }
        }

        /// <summary>
        ///     到名称格式
        /// </summary>
        /// <param name="str">旧名称</param>
        /// <param name="link">连接字符</param>
        /// <param name="uWord">是否大写</param>
        /// <returns>新名称</returns>
        public static string ToLinkWordName(string str, string link, bool uWord)
        {
            try
            {
                var words = ToWords(str);
                var sb = new StringBuilder();
                var preEn = words[0][0] < 255;
                sb.Append(words[0]);
                for (var index = 1; index < words.Count; index++)
                {
                    var word = words[index];
                    if (word[0] < 255 && !preEn || preEn)
                        sb.Append(link);
                    sb.Append(uWord ? word.ToUWord() : word.ToLower());
                    preEn = word[0] < 255;
                }
                return sb.ToString();
            }
            catch (Exception)
            {
                return str;
            }
        }

        /// <summary>
        ///     到名称格式
        /// </summary>
        /// <param name="words">单词组</param>
        /// <param name="link">连接字符</param>
        /// <param name="uWord">是否大写</param>
        /// <returns>名称</returns>
        public static string ToName(List<string> words, char link = '_', bool uWord = false)
        {
            if (words.Count == 0)
                return string.Empty;
            var sb = new StringBuilder();
            var preEn = words[0][0] < 255;
            sb.Append(words[0]);
            for (var index = 1; index < words.Count; index++)
            {
                var word = words[index];
                if (word[0] < 255 && !preEn || preEn)
                    sb.Append(link);
                sb.Append(uWord ? word.ToUWord() : word.ToLower());
                preEn = word[0] < 255;
            }
            return sb.ToString();
        }

        /// <summary>
        ///     拆分到单词
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<string> SplitWords(string str)
        {
            var words = new List<string>();
            if (string.IsNullOrWhiteSpace(str))
                return words;
            str = str.Replace("ID", "Id").Replace("URL", "Url");
            var baseWords = str.Split(NoneLanguageChar, StringSplitOptions.RemoveEmptyEntries);
            var sb = new StringBuilder();
            foreach (var word in baseWords)
            {
                if (word.All(c => c > 255))
                {
                    words.Add(word);
                    continue;
                }
                if (word.All(c => c >= 'A' && c <= 'Z' || c >= '0' && c <= '9'))
                {
                    words.Add(word);
                    continue;
                }
                if (word.All(c => c >= 'a' && c <= 'z' || c >= '0' && c <= '9'))
                {
                    words.Add(word);
                    continue;
                }
                foreach (var c in word)
                {
                    if (c >= 'a' && c <= 'z' || c >= '0' && c <= '9')
                    {
                        sb.Append(c);
                        continue;
                    }
                    if (c >= 'A' && c <= 'Z' || c > 255)
                    {
                        if (sb.Length > 0)
                        {
                            words.Add(sb.ToString());
                            sb.Clear();
                        }
                        sb.Append(c);
                        continue;
                    }
                    if (sb.Length > 0)
                    {
                        words.Add(sb.ToString());
                        sb.Clear();
                    }
                }
                if (sb.Length > 0)
                {
                    words.Add(sb.ToString());
                    sb.Clear();
                }
            }
            //var result = new List<string>();
            //string pre = null;
            //foreach (var word in words)
            //{
            //    if (pre != null)
            //        if (pre == "I" && word == "D")
            //        {
            //            if (result.Count > 0)
            //                result.RemoveAt(result.Count - 1);
            //            result.Add("ID");
            //            continue;
            //        }
            //    result.Add(word);
            //    pre = word;
            //}
            return words;
        }

        /// <summary>
        ///     拆分为单词
        /// </summary>
        /// <param name="str"></param>
        /// <param name="uWord"></param>
        /// <returns></returns>
        public static List<string> ToWords(string str, bool uWord = false)
        {
            var words = SplitWords(str);
            if (!uWord || words.Count == 0)
                return words;
            return words.Select(p => p.ToUWord()).ToList();
        }

        /// <summary>
        ///     到规范文本
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToMyName(string str)
        {
            var words = SplitWords(str);
            if (words.Count == 0)
                return str;
            return ToName(words);
        }

        /// <summary>
        ///     转换为注释文本
        /// </summary>
        /// <param name="str">要转换后的文本</param>
        /// <param name="space">空格数量</param>
        /// <returns>正确表示为C#注释的文本</returns>
        protected static string ToRemString(string str, int space = 8)
        {
            if (string.IsNullOrWhiteSpace(str))
                return null;
            var rp = str.Replace("&", "或").Replace("\r", "").Replace("<", "〈").Replace(">", "〉");
            var sp = rp.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var sb = new StringBuilder();
            var isFirst = true;
            foreach (var line in sp)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    sb.AppendLine();
                    sb.Append(' ', space);
                    sb.Append("/// ");
                }
                sb.Append(line.Trim());
            }
            return sb.ToString();
        }

        #endregion

        #region 名称处理

        /// <summary>
        ///     一般分隔字符
        /// </summary>
        public static char[] SplitChar =
        {
            '　', ' ', '\t', '\n', '\r', ',', '，', ';', '；', ':', '：', '.', '．', '。'
        };

        /// <summary>
        ///     非名称分隔字符
        /// </summary>
        public static readonly char[] NoneNameChar =
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            '　', ' ', '\t', '\n', '\r', '\'', '"',
            ',', '，', ';', '；', ':', '：', '.', '．', '。', '*', '＊', '/', '／', '\\', '＼',
            '=', '<', '>', '[', ']', '{', '}', '(', ')', '＜', '＞', '［', '］', '｛', '｝', '（', '）',
            '-', '_', '+', '~', '`', '?', '&', '^', '%', '$', '#', '@', '!',
            '｀', '～', '＠', '＃', '＄', '％', '＾', '＆', '！'
        };

        /// <summary>
        ///     非编程语言分隔字符
        /// </summary>
        public static char[] NoneLanguageChar =
        {
            '　', ' ', '\t', '\n', '\r', '\'', '"',
            ',', '，', ';', '；', ':', '：', '.', '．', '。', '*', '＊', '/', '／', '\\', '＼',
            '=', '<', '>', '[', ']', '{', '}', '(', ')', '＜', '＞', '［', '］', '｛', '｝', '（', '）',
            '-', '_', '+', '~', '`', '?', '&', '^', '%', '$', '#', '@', '!',
            '｀', '～', '＠', '＃', '＄', '％', '＾', '＆', '！'
        };

        /// <summary>
        ///     空值分隔字符
        /// </summary>
        public static char[] EmptyChar =
        {
            '　', ' ', '\t', '\n', '\r'
        };

        /// <summary>
        ///     非名称对象分隔字符
        /// </summary>
        public static string[] NoneNameString =
        {
            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
            //"０", "１", "２", "３", "４", "５","６", "７", "８", "９",
            "　", " ", "\t", "\n", "\r", "\'", "\"",
            ",", "，", ";", "；", ":", "：", ".", "．", "。", "*", "＊", "/", "／", "\\", "＼",
            "=", "<", ">", "[", "]", "{", "}", "(", ")", "＜", "＞", "［", "］", "｛", "｝", "（", "）",
            "-", "_", "+", "~", "`", "?", "&", "^", "%", "$", "#", "@", "!",
            "｀", "～", "＠", "＃", "＄", "％", "＾", "＆", "！"
        };

        /// <summary>
        ///     非编辑语言名称分隔字符
        /// </summary>
        public static string[] NoneLanguageString =
        {
            "　", " ", "\t", "\n", "\r", "\'", "\"",
            ",", "，", ";", "；", ":", "：", ".", "．", "。", "*", "＊", "/", "／", "\\", "＼",
            "=", "<", ">", "[", "]", "{", "}", "(", ")", "＜", "＞", "［", "］", "｛", "｝", "（", "）",
            "-", "_", "+", "~", "`", "?", "&", "^", "%", "$", "#", "@", "!",
            "｀", "～", "＠", "＃", "＄", "％", "＾", "＆", "！"
        };

        #endregion

        #region 对象遍历

        /// <summary>
        ///     所有对象遍历
        /// </summary>
        public static void ForeachAll(Action<ConfigBase> action)
        {
            foreach (var solution in Solutions)
            {
                ForeachAll(solution, action);
            }
        }

        /// <summary>
        ///     所有对象遍历
        /// </summary>
        public static void ForeachAll(SolutionConfig solution, Action<ConfigBase> action)
        {
            action(solution);
            foreach (var project in solution.Projects)
            {
                action(project);
                foreach (var entity in project.Entities)
                {
                    action(entity);
                    foreach (var field in entity.Properties)
                        action(field);
                    foreach (var command in entity.Commands)
                        action(command);
                }
            }
            foreach (var config in solution.Enums)
            {
                action(config);
                foreach (var item in config.Items)
                    action(item);
            }
            foreach (var config in solution.TypedefItems)
            {
                action(config);
                foreach (var item in config.Items.Values)
                    action(item);
            }
            foreach (var item in solution.TypedefItems)
                action(item);
            foreach (var config in solution.ApiItems)
            {
                action(config);
            }
            foreach (var config in solution.NotifyItems)
            {
                action(config);
            }
        }

        #endregion

        #region 对象集合

        /// <summary>
        /// 设置设计器全局对象
        /// </summary>
        /// <param name="global"></param>
        public static void SetGlobal(IDesignGlobal global)
        {
            Global = global;
        }

        /// <summary>
        /// 上下文
        /// </summary>
        public static IDesignGlobal Global { get; private set; }

        /// <summary>
        /// 应用根目录
        /// </summary>
        public static string ProgramRoot => Global.ProgramRoot;

        /// <summary>
        /// 解决方案
        /// </summary>
        public static SolutionConfig GlobalSolution
        {
            get
            {
                return Global.GlobalSolution;
            }
            set
            {
                Global.GlobalSolution = value;
            }
        }

        /// <summary>
        /// 解决方案
        /// </summary>
        public static SolutionConfig CurrentSolution
        {
            get
            {
                return Global.CurrentSolution;
            }
            set
            {
                Global.CurrentSolution = value;
            }
        }
        /// <summary>
        /// 当前选择
        /// </summary>
        public static ConfigBase CurrentConfig
        {
            get
            {
                return Global.CurrentConfig;
            }
            set
            {
                Global.CurrentConfig = value;
            }
        }
        
        /// <summary>
        ///     解决方案集合
        /// </summary>
        public static ObservableCollection<SolutionConfig> Solutions => Global.Solutions;

        /// <summary>
        ///     枚举集合
        /// </summary>
        public static ObservableCollection<EnumConfig> Enums => Global.Enums;
        
        /// <summary>
        ///     类型(C++)集合
        /// </summary>
        public static ObservableCollection<TypedefItem> TypedefItems => Global.TypedefItems;
        
        /// <summary>
        ///     实体集合
        /// </summary>
        public static ObservableCollection<EntityConfig> Entities => Global.Entities;
        
        /// <summary>
        ///     项目集合
        /// </summary>
        public static ObservableCollection<ProjectConfig> Projects => Global.Projects;
        
        /// <summary>
        ///     API集合
        /// </summary>
        public static ObservableCollection<ApiItem> ApiItems => Global.ApiItems;
        
        /// <summary>
        ///     通知集合
        /// </summary>
        public static ObservableCollection<NotifyItem> NotifyItems => Global.NotifyItems;

        #endregion
    }
}