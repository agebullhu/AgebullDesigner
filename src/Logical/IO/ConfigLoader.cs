using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Agebull.Common;
using Agebull.EntityModel.Config;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 配置读写器
    /// </summary>
    public class ConfigLoader
    {
        #region 载入

        /// <summary>
        /// 载入
        /// </summary>
        /// <param name="sluFile"></param>
        public static SolutionConfig Load(string sluFile)
        {
            GlobalConfig.ClearConfigDictionary();
            ConfigLoader loader = new ConfigLoader();
            loader.LoadSolution(GlobalConfig.CheckPath(GlobalConfig.ProgramRoot, "Global"), "global.json", true);
            GlobalConfig.GlobalSolution = loader._solution;

            GlobalConfig.GlobalSolution.Foreach<ConfigBase>(p => p.Option.IsNormal, p => GlobalConfig.AddConfig(p.Option));
            var re = GlobalConfig.GlobalSolution;
            if (!string.IsNullOrWhiteSpace(sluFile) && !string.Equals(sluFile, GlobalConfig.GlobalSolution.SaveFileName, StringComparison.OrdinalIgnoreCase))
            {
                loader = new ConfigLoader();
                loader.LoadSolution(Path.GetDirectoryName(sluFile), Path.GetFileName(sluFile), false);

                loader._solution.Foreach<ConfigBase>(p => p.Option.IsNormal, p => GlobalConfig.AddConfig(p.Option));
                re = GlobalConfig.LocalSolution = loader._solution;
            }
            return re;
        }

        private void LoadSolution(string directory, string file, bool isGlobal)
        {
            var sluFile = Path.Combine(directory, file);
            using (WorkModelScope.CreateScope(WorkModel.Loding))
            {
                try
                {
                    _solution = DeSerializer<SolutionConfig>(sluFile) ?? new SolutionConfig
                    {
                        Name = "GlobalConfig",
                        Caption = "全局配置",
                        Description = "全局配置"
                    };
                }
                catch (Exception exception)
                {
                    Trace.WriteLine(exception);
                    _solution = new SolutionConfig();
                }
                SolutionConfig.SetCurrentSolution(_solution);
                _solution.IsGlobal = isGlobal;
                _solution.SaveFileName = sluFile;
                try
                {
                    LoadProjects(directory);
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex);
                }
                GlobalTrigger.OnLoad(_solution);
            }
        }

        private void LoadProjects(string directory)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            if (Directory.Exists(Path.Combine(directory, "Projects")))
            {
                LoadOldProjects(directory);
            }
            else
            {
                var folders = Directory.GetDirectories(directory, "*", SearchOption.TopDirectoryOnly);
                foreach (var folder in folders)
                {
                    var path = Path.Combine(directory, folder);
                    var file = Path.Combine(path, "project.json");
                    if (!File.Exists(file))
                        continue;
                    ProjectConfig project = DeSerializer<ProjectConfig>(file);
                    if (project==null || project.IsDelete)
                        continue;
                    _solution.Add(project);
                    LoadEntity(project, path);
                    LoadEnums(project, path);
                    LoadApi(project, path);
                }

                if (_solution.ProjectList.Count != 0) return;
                _solution.Add(new ProjectConfig
                {
                    Name = _solution.Name,
                    Caption = _solution.Caption,
                    Description = _solution.Description,
                });
            }
        }

        private void LoadOldProjects(string directory)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            var files = IOHelper.GetAllFiles(directory, "prj");
            foreach (var file in files)
            {
                ProjectConfig project = DeSerializer<ProjectConfig>(file);
                if (project.IsDelete)
                    continue;
                _solution.Add(project);
                var path = Path.Combine(Path.GetDirectoryName(file), project.Name);
                LoadEntity(project, path);
                LoadEnums(project, path);
                LoadApi(project, path);
            }

            if (_solution.ProjectList.Count != 0) return;
            _solution.Add(new ProjectConfig
            {
                Name = _solution.Name,
                Caption = _solution.Caption,
                Description = _solution.Description,
            });
        }
        private void LoadEntity(ProjectConfig project, string directory)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            string path = Path.Combine(directory, "Entity");
            var files = !Directory.Exists(path)
                ? IOHelper.GetAllFiles(directory, "ent")
                : IOHelper.GetAllFiles(path, "*.*");
            foreach (var entFile in files)
            {
                var entity = DeSerializer<EntityConfig>(entFile);
                if (entity == null || entity.IsDelete)
                    continue;
                foreach (var field in entity.Properties)
                {
                    field.Parent = entity;
                }
                project.Add(entity);
            }
        }

        private void LoadEnums(ProjectConfig project, string directory)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            string path = Path.Combine(directory, "Enum");
            var enums = IOHelper.GetAllFiles(path, "*.*");
            foreach (var file in enums)
            {
                var type = DeSerializer<EnumConfig>(file);
                if (type.IsDelete)
                    continue;
                project.Add(type);
            }
        }

        private void LoadApi(ProjectConfig project, string directory)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            string path = Path.Combine(directory, "Api");
            var apis = IOHelper.GetAllFiles(path, "*.*");
            foreach (var file in apis)
            {
                var api = DeSerializer<ApiItem>(file);
                if (api.IsDelete)
                    continue;
                project.Add(api);
            }
        }

        #endregion

        #region 基础支持

        /// <summary>
        ///     表结构对象
        /// </summary>
        private SolutionConfig _solution;


        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="TConfig"></typeparam>
        /// <param name="file"></param>
        /// <returns></returns>
        public static TConfig DeSerializer<TConfig>(string file)
            where TConfig : FileConfigBase
        {
            try
            {
                var args = File.ReadAllText(file, Encoding.UTF8);
                if (string.IsNullOrWhiteSpace(args))
                    return default(TConfig);
                TConfig config;
                if (args[0] != '<')
                {
                    config = JsonConvert.DeserializeObject<TConfig>(args);
                }
                else
                {
                    using (var pteam = File.OpenRead(file))
                    {
                        config = (TConfig)new DataContractSerializer(typeof(TConfig)).ReadObject(pteam);
                    }
                }
                config.SaveFileName = file;
                return config;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.ToString());
                return default(TConfig);
            }
        }

        #endregion
    }
}