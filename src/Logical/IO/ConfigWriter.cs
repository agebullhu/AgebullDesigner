using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Agebull.Common;
using Agebull.EntityModel.Config;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 配置读写器
    /// </summary>
    public class ConfigWriter
    {
        #region 保存

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="solution"></param>
        /// <param name="filename"></param>
        public static void Save(SolutionConfig solution, string filename)
        {
            if (solution == GlobalConfig.GlobalSolution)
            {
                SaveGlobal();
            }
            else
            {
                solution.SaveFileName = filename;
                SaveSolution(solution);
            }
            GlobalConfig.ClearConfigDictionary();
            solution.Foreach<ConfigBase>(p => p.Option.IsNormal, p => GlobalConfig.AddConfig(p.Option));
        }

        /// <summary>
        /// 保存
        /// </summary>
        private static void SaveGlobal()
        {
            var global = Path.GetDirectoryName(Path.GetDirectoryName(typeof(ConfigWriter).Assembly.Location));
            var directory = GlobalConfig.CheckPath(global, "Global");
            GlobalConfig.GlobalSolution.SaveFileName = Path.Combine(directory, "global.json");
            SaveSolution(GlobalConfig.GlobalSolution);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="solution"></param>
        private static void SaveSolution(SolutionConfig solution)
        {
            var writer = new ConfigWriter
            {
                Solution = solution,
            };
            writer.SaveSolution();
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void SaveSolution()
        {
            var directory = Path.GetDirectoryName(Solution.SaveFileName);
            using (WorkModelScope.CreateScope(WorkModel.Saving))
            {
                SaveProjects(directory);
                SaveConfig(Solution.SaveFileName, Solution, true);
            }

            Solution.Foreach<ConfigBase>(p => p.IsModify = false);
            //VersionControlItem.Current.TfsCheckIn();
        }

        private void SaveProjects(string directory)
        {
            foreach (var project in Solution.Projects.ToArray())
            {
                SaveProject(project, directory);
            }
        }

        /// <summary>
        /// 保存解决方案
        /// </summary>
        public string SaveProject(ProjectConfig project, string directory, bool checkState = true)
        {
            var dir = GlobalConfig.CheckPath(directory, project.Name);
            foreach (var cls in project.Classifies.ToArray())
            {
                foreach (var ch in cls.Items)
                {
                    ch.Classify = cls.IsDelete ? null : cls.Name;
                }

                if (cls.IsDelete)
                    project.Classifies.Remove(cls);
            }
            SavEntities(project, dir, checkState);
            SaveEnums(project, dir, checkState);
            SaveApies(project, dir, checkState);
            if (project.IsDelete)
            {
                SolutionConfig.Current.ProjectList.Remove(project);
            }
            SaveConfig(Path.Combine(dir, "project.json"), project, checkState);
            return dir;
        }
        private void SavEntities(ProjectConfig project, string dir, bool checkState)
        {
            var path = GlobalConfig.CheckPath(dir, "Entity");
            foreach (var entity in project.Entities.ToArray())
            {
                SaveEntity(entity, path, checkState);
            }
        }
        private void SaveEnums(ProjectConfig project, string dir, bool checkState)
        {
            var path = GlobalConfig.CheckPath(dir, "Enum");
            foreach (var type in project.Enums.ToArray())
            {
                if (type.IsDelete)
                {
                    project.Remove(type);
                }
                else
                {
                    //删除字段处理
                    foreach (var field in type.Items.Where(p => p.IsDelete).ToArray())
                    {
                        type.Remove(field);
                    }
                }

                SaveConfig(type, path, checkState);
            }
        }


        /// <summary>
        /// 保存API对象
        /// </summary>
        private void SaveApies(ProjectConfig project, string dir, bool checkState)
        {
            var path = GlobalConfig.CheckPath(dir, "Api");
            foreach (var api in project.ApiItems.ToArray())
            {
                if (api.IsDelete)
                {
                    project.Remove(api);
                }
                SaveConfig(api, path, checkState);
            }
        }

        /// <summary>
        /// 保存实体
        /// </summary>
        public void SaveEntity(EntityConfig entity, string dir, bool checkState)
        {
            if (entity.IsDelete)
            {
                entity.Parent.Remove(entity);
            }
            else
            {
                //删除字段处理
                foreach (var field in entity.Properties.Where(p => p.IsDelete).ToArray())
                {
                    entity.Remove(field);
                }

                //删除命令处理
                foreach (var field in entity.Commands.Where(p => p.IsDelete).ToArray())
                {
                    entity.Remove(field);
                }
            }

            SaveConfig(entity, dir, checkState);
        }
        #endregion

        #region 基础支持

        /// <summary>
        ///     表结构对象
        /// </summary>
        public SolutionConfig Solution;

        /// <summary>
        /// 保存通知对象
        /// </summary>
        public void SaveConfig<TConfig>(TConfig config, string dir, bool checkState)
            where TConfig : FileConfigBase
        {
            SaveConfig(Path.Combine(dir, config.GetFileName()), config, checkState);
        }

        /// <summary>
        /// 保存通知对象
        /// </summary>
        public void SaveConfig<TConfig>(string filename, TConfig config, bool checkState)
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

        #endregion
    }
}