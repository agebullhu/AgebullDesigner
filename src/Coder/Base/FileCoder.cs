using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Agebull.Common;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{

    /// <summary>
    ///     文件代码生成基类
    /// </summary>
    public abstract class FileCoder : CoderBase
    {
        #region 流程

        /// <summary>
        /// 文件名所保存的配置名称（即这个配置名称的值是生成的文件名）
        /// </summary>
        protected abstract string FileSaveConfigName { get; }

        /// <summary>
        /// 是否可写
        /// </summary>
        protected virtual bool CanWrite => false;
        /// <summary>
        /// 是否扩展代码
        /// </summary>
        private bool CurrentIsExtend { get; set; }
        /// <summary>
        /// 是否扩展代码
        /// </summary>
        private string CurrentPath { get; set; }
        /// <summary>
        /// 当前对象
        /// </summary>
        public abstract ConfigBase CurrentConfig { get; }

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        public void CreateExtendCode(string path)
        {
            CurrentIsExtend = true;
            CurrentPath = path;
            if (CanWrite)
                CreateExCode(path);
        }

        /// <summary>
        ///     生成基础代码
        /// </summary>
        public void CreateBaseCode(string path)
        {
            CurrentIsExtend = false;
            CurrentPath = path;
            if (CanWrite)
                CreateBaCode(path);
        }
        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected virtual void CreateExCode(string path)
        {
        }

        /// <summary>
        ///     生成基础代码
        /// </summary>
        protected virtual void CreateBaCode(string path)
        {
        }

        #endregion

        #region 保存代码
        
        /// <summary>
        /// 取得扩展配置的路径
        /// </summary>
        /// <param name="config">对应配置</param>
        /// <param name="key">保存键名称</param>
        /// <param name="path">根路径</param>
        /// <param name="defDir">默认目录</param>
        /// <param name="defName">默认名称（扩展名随意）</param>
        /// <returns></returns>
        protected static string ConfigPath(ConfigBase config, string key, string path, string defDir, string defName)
        {
            key = key.ToLower();
            var old = config.TryGetExtendConfig(key, null);
            string full;
            if (old == null)
            {
                var p2 = Path.Combine(defDir, defName);
                config.ExtendConfig.Add(new ConfigItem { Name = key, Value = p2 });

                full = Path.Combine(path, defDir, defName);
            }
            else
            {
                full = Path.Combine(path, old);
            }

            string file = Path.GetFileName(full);
            full = IOHelper.CheckPaths(Path.GetDirectoryName(full));
            return Path.Combine(full, file);
        }
        #endregion

        #region 保存代码


        /// <summary>
        /// 保存代码
        /// </summary>
        /// <returns>正确表示为C#注释的文本</returns>
        protected void SaveCode(string file, string code, bool overWirte = true)
        {
            var name = FileSaveConfigName + (CurrentIsExtend ? "_ex" : "");
            var old = CurrentConfig[name];
            if (old == null)
            {
                CurrentConfig[name] = file.Replace(CurrentPath, "").Trim('\\');
            }
            else
            {
                file = Path.Combine(CurrentPath, old.Trim('\\'));
            }
            WriteFile(file, code, overWirte);
        }


        /// <summary>
        /// 代码写入文件
        /// </summary>
        public static void WriteFile(string file, string code, bool overWirte = true)
        {
            if (File.Exists(file))
            {
                FileInfo f = new FileInfo(file);
                if (f.IsReadOnly)
                {
                    f.IsReadOnly = false;
                    f.Refresh();
                }
                using (var reader = File.OpenText(file))
                {
                    var mark = reader.ReadLine();
                    if (String.IsNullOrWhiteSpace(mark) || !mark.Contains("此标记表明此文件可被设计器更新"))
                        return;
                    reader.Close();
                }
            }
            StringBuilder sb = new StringBuilder();
            if (overWirte)
            {
                // ReSharper disable once PossibleNullReferenceException
                var ex = Path.GetExtension(file).ToLower().Trim('.');
                switch (ex)
                {
                    case "htm":
                    {
                        sb.Append($"<!--此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:{DateTime.Now}-->\r\n");
                        break;
                    }
                    case "aspx":
                    {
                        sb.Append($"<%--此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:{DateTime.Now}--%>\r\n");
                        break;
                    }
                    default:
                    {
                        sb.Append($"/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:{DateTime.Now}*/\r\n");
                        break;
                    }
                }
            }
            code = code.Trim();
            if (File.Exists(file))
            {
                string old = File.ReadAllText(file, Encoding.UTF8);
                if (old.Contains('\n'))
                {
                    old = old.Split(new[] {'\n'}, 2)[1].Trim();
                }
                if (String.Equals(code, old))
                    return;
            }
            else
            {
                var dir = Path.GetDirectoryName(file);
                var root = Path.GetPathRoot(dir);
                var folders = dir.Substring(root.Length, dir.Length - root.Length).Split( '\\');
                IOHelper.CheckPath(root, folders);
            }
            sb.Append(code);
            code = sb.ToString();
            File.WriteAllText(file, code, Encoding.UTF8);
            //using (TfsSourceCodeHelper helper = new TfsSourceCodeHelper(file))
            //{
            //    //helper.CheckOut();
            //    //helper.CheckIn(file);
            //}
            Trace.WriteLine(file);
        }
        #endregion
    }
}
