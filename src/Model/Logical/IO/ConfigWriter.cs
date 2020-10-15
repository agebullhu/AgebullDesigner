using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Agebull.EntityModel.Config;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 配置读写器
    /// </summary>
    public class ConfigWriter
    {
        /// <summary>
        /// 保存通知对象
        /// </summary>
        public static void SaveConfig<TConfig>(TConfig config, string dir, bool checkState)
            where TConfig : FileConfigBase
        {
            SaveConfig(Path.Combine(dir, config.GetFileName()), config, checkState);
        }

        /// <summary>
        /// 保存通知对象
        /// </summary>
        public static void SaveConfig<TConfig>(string filename, TConfig config, bool checkState)
            where TConfig : FileConfigBase
        {
            if (config.SaveFileName != null)
            {
                if (File.Exists(config.SaveFileName) && checkState)
                {
                    if (config.Option.IsDelete)
                    {
                        var old = ConfigLoader.DeSerializer<TConfig>(config.SaveFileName);
                        if (old != null)
                        {
                            if (old.IsDelete)
                                return;
                            config = old;
                            config.Option.IsDelete = true;
                        }
                    }
                    if (config.Option.IsLock)
                        return;
                }

                if (!string.Equals(config.SaveFileName, filename, StringComparison.OrdinalIgnoreCase) && File.Exists(config.SaveFileName))
                {
                    File.Delete(config.SaveFileName);
                }
            }
            if (config.Option.CanLock)
                config.Option.LockConfig();
            try
            {
                string json = JsonConvert.SerializeObject(config);
                File.WriteAllText(filename, json, Encoding.UTF8);
                config.SaveFileName = filename;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.ToString());
            }
        }
    }
}