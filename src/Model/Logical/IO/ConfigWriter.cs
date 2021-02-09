using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Config.V2021;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 配置写入
    /// </summary>
    public class ConfigWriter
    {
        /// <summary>
        /// 保存设计文件
        /// </summary>
        public static void SaveConfig<TConfig>(TConfig config, string dir, bool checkState)
            where TConfig : FileConfigBase
        {
            if (config != null)
                SaveConfig(Path.Combine(dir, config.GetFileName()), config, checkState);
        }

        /// <summary>
        /// 保存设计文件
        /// </summary>
        public static void SaveExtendConfig(FileConfigBase entity, EntityExtendConfig extend)
        {
            if (extend == null)
                return;
            var dir = Path.GetDirectoryName(entity.SaveFileName);
            SaveConfig(Path.Combine(dir,"Extend", extend.GetFileName()), extend, false);
        }
        /// <summary>
        /// 保存设计文件
        /// </summary>
        public static void SaveConfig<TConfig>(string filename, TConfig config, bool checkState)
            where TConfig : FileConfigBase
        {
            if (config == null)
                return;
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
                config.Foreach(p => p.IsModify = false);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// 删除设计文件
        /// </summary>
        public static void DeleteConfig<TConfig>(TConfig config)
            where TConfig : FileConfigBase
        {
            try
            {

                if (config.SaveFileName.IsNotBlank() && File.Exists(config.SaveFileName))
                    File.Delete(config.SaveFileName);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.ToString());
            }
        }

    }
}