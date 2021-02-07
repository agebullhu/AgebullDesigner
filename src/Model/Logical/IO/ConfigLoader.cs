using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Agebull.Common;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Config.V2021;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            loader.LoadSolution(GlobalConfig.CheckPath(GlobalConfig.RootPath, "Global"), "global.json", true);
            GlobalConfig.GlobalSolution = loader._solution;

            GlobalConfig.GlobalSolution.Foreach(GlobalConfig.AddNormalConfig);
            var re = GlobalConfig.GlobalSolution;
            if (!string.IsNullOrWhiteSpace(sluFile) && !string.Equals(sluFile, GlobalConfig.GlobalSolution.SaveFileName, StringComparison.OrdinalIgnoreCase))
            {
                loader = new ConfigLoader();
                loader.LoadSolution(Path.GetDirectoryName(sluFile), Path.GetFileName(sluFile), false);

                loader._solution.Foreach(GlobalConfig.AddNormalConfig);
                re = GlobalConfig.LocalSolution = loader._solution;
            }
            return re;
        }

        private void LoadSolution(string directory, string file, bool isGlobal)
        {
            CommandCoefficient.ClearCommand();
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
                LoadV2018Projects(directory);
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
                    if (project == null || project.IsDelete)
                        continue;
                    _solution.Add(project);
                    LoadEntity(project, path);
                    LoadModel(project, path);
                    LoadEnums(project, path);
                    LoadApi(project, path);
                }

                if (_solution.ProjectList.Count != 0) return;
                var porject = new ProjectConfig
                {
                    Name = _solution.Name,
                    Caption = _solution.Caption,
                    Description = _solution.Description,
                };
                _solution.Add(porject);
            }
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
                    field.Entity = entity;
                    GlobalConfig.AddConfig(field);
                }
                LoadEntityExtend(entity, path);
                GlobalConfig.AddConfig(entity);
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
        private void LoadModel(ProjectConfig project, string directory)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            string path = Path.Combine(directory, "Model");
            if (!Directory.Exists(path))
                return;
            var files = IOHelper.GetAllFiles(path, "*.*");
            foreach (var entFile in files)
            {
                var model = DeSerializer<ModelConfig>(entFile);
                if (model == null || model.IsDelete)
                    continue;
                model.Entity = GlobalConfig.GetEntity(p => p.Key == model._entityKey);
                foreach (var field in model.Properties.ToArray())
                {
                    if (field.Field == null)
                    {
                        model.Properties.Remove(field);
                        continue;
                    }
                    field.Model = model;
                    GlobalConfig.AddConfig(field);
                }
                LoadEntityExtend(model, path);
                GlobalConfig.AddConfig(model);
                project.Add(model);
            }
        }
        private void LoadEntityExtend(IEntityConfig entity, string directory)
        {
            LoadDataTable(entity, directory);
            LoadPage(entity, directory);
        }

        private void LoadDataTable(IEntityConfig entity, string directory)
        {
            if (!entity.EnableDataBase)
            {
                entity.DataTable = null;
                return;
            }
            // ReSharper disable once AssignNullToNotNullAttribute
            string fileName = Path.Combine(directory,"Extend", PageConfig.GetFileName(entity));
            if (!File.Exists(fileName))
            {
                entity.DataTable = new DataTableConfig();
                entity.DataTable.Upgrade();
                return;
            }

            var model = DeSerializer<DataTableConfig>(fileName);
            if (model == null)
            {
                entity.DataTable = new DataTableConfig();
                entity.DataTable.Upgrade();
                return;
            }
            model.Option.IsDelete = false;
            entity.DataTable = model;
        }
        private void LoadPage(IEntityConfig entity, string directory)
        {
            if (!entity.EnableUI)
            {
                entity.Page = null;
                return;
            }
            entity.Page = new PageConfig { Entity = entity };
            // ReSharper disable once AssignNullToNotNullAttribute
            string fileName = Path.Combine(directory, "Extend", PageConfig.GetFileName(entity));
            if (!File.Exists(fileName))
            {
                entity.Page.Upgrade();
                return;
            }
            try
            {
                var json = File.ReadAllText(fileName, Encoding.UTF8);
                if (json.IsBlank() || json[0] != '<')
                {
                    entity.Page.Upgrade();
                    return;
                }
                entity.Page.SaveFileName = fileName;
                var jObject = JsonConvert.DeserializeObject(json) as JObject;
                entity.Page.Load(jObject);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.ToString());
            }
        }


        #endregion
        #region V2018版本兼容

        private void LoadV2018Projects(string directory)
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
                    return default;
                TConfig config;
                if (args[0] != '<')
                {
                    config = JsonConvert.DeserializeObject<TConfig>(args);
                }
                else
                {
                    using var pteam = File.OpenRead(file);
                    config = (TConfig)new DataContractSerializer(typeof(TConfig)).ReadObject(pteam);
                }
                config.SaveFileName = file;
                return config;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.ToString());
                return default;
            }
        }

        #endregion
    }
}