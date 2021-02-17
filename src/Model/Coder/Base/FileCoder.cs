using Agebull.EntityModel.Config;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

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
        /// 当前对象
        /// </summary>
        public abstract ConfigBase CurrentConfig { get; }

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        public void WriteCustomCode(string path)
        {
            if (!CanWrite)
                return;
            using (CodeGeneratorScope.CreateScope(CurrentConfig, false))
            {
                try
                {
                    CreateCustomCode(path);
                }
                catch (Exception e)
                {
                    Trace.Write(e, this.GetTypeName());
                }
            }
        }

        /// <summary>
        ///     生成基础代码
        /// </summary>
        public void WriteDesignerCode(string path)
        {
            if (!CanWrite)
                return;
            using (CodeGeneratorScope.CreateScope(CurrentConfig, false))
            {
                try
                {
                    CreateDesignerCode(path);
                }
                catch (Exception e)
                {
                    Trace.Write(e, this.GetTypeName());
                }
            }
        }

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected virtual void CreateCustomCode(string path)
        {
        }

        /// <summary>
        ///     生成基础代码
        /// </summary>
        protected virtual void CreateDesignerCode(string path)
        {
        }

        #endregion

        #region 取得扩展配置的路径

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
            var old = config.Option[key];
            string full;
            if (!string.IsNullOrWhiteSpace(old))
            {
                full = Path.Combine(path, old);
                if (File.Exists(full))
                    return full;
            }

            if (string.IsNullOrWhiteSpace(defDir))
            {
                config.Option[key] = defName;
                full = Path.Combine(path, defName);
            }
            else
            {
                config.Option[key] = Path.Combine(defDir, defName);
                full = Path.Combine(path, defDir, defName);
            }
            GlobalConfig.CheckPaths(Path.GetDirectoryName(full));
            return full;
        }
        #endregion

        #region 保存代码


        /// <summary>
        /// 保存代码
        /// </summary>
        /// <returns>正确表示为C#注释的文本</returns>
        protected void SaveCode(string file, string code, bool overWirte = true)
        {
            WriteFile(file, code, overWirte);
        }


        /// <summary>
        /// 代码写入文件
        /// </summary>
        public void WriteFile(string file, string code, bool overWirte = true)
        {
            WorkContext.FileCodes?.AddOrSwitch(file, code);
            if (!WorkContext.WriteToFile)
            {
                Trace.WriteLine(file, "阻止写入");
                return;
            }

            if (File.Exists(file))
            {
                FileInfo f = new FileInfo(file);
                if (f.IsReadOnly)
                {
                    f.IsReadOnly = false;
                    f.Refresh();
                }
                using var reader = File.OpenText(file);
                var mark = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(mark) || !mark.Contains("此标记表明此文件可被设计器更新"))
                {
                    Trace.WriteLine(file, "无写入标识");
                    return;
                }
                reader.Close();
            }
            else
            {
                GlobalConfig.CheckPaths(Path.GetDirectoryName(file));
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
                    old = old.Split(new[] { '\n' }, 2)[1].Trim();
                }

                if (string.Equals(code, old))
                {
                    Trace.WriteLine(file, "内容相同");
                    return;
                }
            }
            else
            {
                var dir = Path.GetDirectoryName(file);
                var root = Path.GetPathRoot(dir);
                if (dir != null)
                {
                    var folders = dir.Substring(root.Length, dir.Length - root.Length).Split('\\');
                    GlobalConfig.CheckPath(root, folders);
                }
            }
            sb.Append(code);
            code = sb.ToString();
            File.WriteAllText(file, code, Encoding.UTF8);
            //using (TfsSourceCodeHelper helper = new TfsSourceCodeHelper(file))
            //{
            //    //helper.CheckOut();
            //    //helper.CheckIn(file);
            //}
            Trace.WriteLine(file, "文件写入");
        }
        #endregion
    }
}
