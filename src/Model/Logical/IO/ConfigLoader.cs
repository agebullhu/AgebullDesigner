using Agebull.Common;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Config.V2021;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

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
            GlobalConfig.GlobalSolution = LoadSolution(GlobalConfig.CheckPath(GlobalConfig.RootPath, "Global"), "global.json", true);

            GlobalConfig.GlobalSolution.Foreach(GlobalConfig.AddNormalConfig);
            var re = GlobalConfig.GlobalSolution;
            if (!string.IsNullOrWhiteSpace(sluFile) && !string.Equals(sluFile, GlobalConfig.GlobalSolution.SaveFileName, StringComparison.OrdinalIgnoreCase))
            {
                var solution = LoadSolution(Path.GetDirectoryName(sluFile), Path.GetFileName(sluFile), false);

                solution.Foreach(GlobalConfig.AddNormalConfig);
                re = GlobalConfig.LocalSolution = solution;
            }
            return re;
        }

        public static SolutionConfig LoadSolution(string directory, string file, bool isGlobal)
        {
            CommandCoefficient.ClearCommand();
            var sluFile = Path.Combine(directory, file);
            using (WorkModelScope.CreateScope(WorkModel.Loding))
            {
                SolutionConfig solution;
                try
                {
                    solution = DeSerializer<SolutionConfig>(sluFile) ?? new SolutionConfig
                    {
                        Name = "GlobalConfig",
                        Caption = "全局配置",
                        Description = "全局配置"
                    };
                }
                catch (Exception exception)
                {
                    Trace.WriteLine(exception);
                    solution = new SolutionConfig();
                }
                SolutionConfig.SetCurrentSolution(solution);
                solution.IsGlobal = isGlobal;
                solution.SaveFileName = sluFile;
                try
                {
                    LoadProjects(solution, directory);
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex);
                }
                GlobalTrigger.OnLoad(solution);
                return solution;
            }
        }

        public static void LoadProjects(SolutionConfig solution, string directory)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            if (Directory.Exists(Path.Combine(directory, "Projects")))
            {
                LoadV2018Projects(solution, directory);
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
                    solution.Add(project);
                    LoadEntity(project, path);
                    LoadModel(project, path);
                    LoadEnums(project, path);
                    LoadApi(project, path);
                }

                if (solution.ProjectList.Count != 0) return;
                var porject = new ProjectConfig
                {
                    Name = solution.Name,
                    Caption = solution.Caption,
                    Description = solution.Description,
                };
                solution.Add(porject);
            }
        }

        public static void LoadEntity(ProjectConfig project, string directory)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            string path = Path.Combine(directory, "Entity");
            var files = Directory.GetFiles(path, "*.json");
            foreach (var entFile in files)
            {
                EntityConfig entity = LoadEntity(path, entFile);
                if (entity != null)
                    project.Add(entity);
            }
        }

        private static EntityConfig LoadEntity(string path, string entFile)
        {
            var entity = DeSerializer<EntityConfig>(entFile);
            if (entity == null || entity.IsDelete)
                return null;
            foreach (var field in entity.Properties)
            {
                field.Entity = entity;
                GlobalConfig.AddConfig(field);
            }
            LoadEntityExtend(entity, path);
            GlobalConfig.AddConfig(entity);
            return entity;
        }

        public static void LoadEnums(ProjectConfig project, string directory)
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

        public static void LoadApi(ProjectConfig project, string directory)
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
        public static void LoadModel(ProjectConfig project, string directory)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            string path = Path.Combine(directory, "Model");
            if (!Directory.Exists(path))
                return;
            var files = IOHelper.GetAllFiles(path, "*.*");
            foreach (var entFile in files)
            {
                ModelConfig model = LoadModel(path, entFile);
                project.Add(model);
            }
        }

        private static ModelConfig LoadModel(string path, string entFile)
        {
            var model = DeSerializer<ModelConfig>(entFile);
            if (model == null || model.IsDelete)
                return null;
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
            return model;
        }

        public static void LoadEntityExtend(IEntityConfig entity, string directory)
        {
            LoadDataTable(entity, directory);
            //LoadPage(entity, directory);
        }

        public static void LoadDataTable(IEntityConfig entity, string directory)
        {
            if (!entity.EnableDataBase)
            {
                entity.DataTable = null;
                return;
            }
            var path = Path.GetDirectoryName(directory);
            // ReSharper disable once AssignNullToNotNullAttribute
            string fileName = Path.Combine(path, "DataBase", DataTableConfig.GetFileName(entity));
            if (!File.Exists(fileName))
            {
                entity.DataTable = null;
                return;
            }

            var model = DeSerializer<DataTableConfig>(fileName);
            if (model == null)
            {
                entity.DataTable = null;
                return;
            }
            model.SaveFileName = fileName;
            model.Entity = entity;
            model.Option.IsDelete = false;
            entity.DataTable = model;
        }
        /*
        public static void LoadPage(IEntityConfig entity, string directory)
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
                if (json.IsMissing() || json[0] != '<')
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
        */

        #endregion
        #region V2018版本兼容

        public static void LoadV2018Projects(SolutionConfig solution, string directory)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            var files = IOHelper.GetAllFiles(directory, "prj");
            foreach (var file in files)
            {
                ProjectConfig project = DeSerializer<ProjectConfig>(file);
                if (project.IsDelete)
                    continue;
                solution.Add(project);
                var path = Path.Combine(Path.GetDirectoryName(file), project.Name);
                LoadEntity(project, path);
                LoadEnums(project, path);
                LoadApi(project, path);
            }

            if (solution.ProjectList.Count != 0) return;
            solution.Add(new ProjectConfig
            {
                Name = solution.Name,
                Caption = solution.Caption,
                Description = solution.Description,
            });
        }


        #endregion
        #region 基础支持

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