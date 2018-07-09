using System;
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
            ConfigLoader loader = new ConfigLoader();
            loader.LoadSolution(IOHelper.CheckPath(GlobalConfig.ProgramRoot, "Global"), "global.json", true);
            GlobalConfig.GlobalSolution = loader._solution;
            if (string.IsNullOrEmpty(sluFile) ||
                string.Equals(sluFile, GlobalConfig.GlobalSolution.SaveFileName, StringComparison.OrdinalIgnoreCase))
                return GlobalConfig.GlobalSolution;
            loader = new ConfigLoader();
            loader.LoadSolution(Path.GetDirectoryName(sluFile), Path.GetFileName(sluFile), false);

            return loader._solution;
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
                    Debug.WriteLine(exception);
                    _solution = new SolutionConfig();
                }
                SolutionConfig.SetCurrentSolution(_solution  );
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
            var path = Path.Combine(directory, "Projects");
            var prjs = IOHelper.GetAllFiles(path, "prj");
            foreach (var prjFile in prjs)
            {
                ProjectConfig project = DeSerializer<ProjectConfig>(prjFile);
                if (project.IsDelete)
                    continue;
                _solution.Add(project);
                var dir = Path.Combine(path, project.Name);
                LoadEntity(project, dir);
                LoadEnums(project, dir);
                LoadApi(project, dir);
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
            string path = Path.Combine(directory, "entities");
            foreach (var entFile in IOHelper.GetAllFiles(path, "ent"))
            {
                var entity = DeSerializer<EntityConfig>(entFile);
                if (entity.IsDelete)
                    continue;
                foreach (var field in entity.Properties)
                {
                    if (field.DbType != null)
                        field.DbType = field.DbType.ToUpper();
                }
                project.Add(entity);
            }
        }

        private void LoadEnums(ProjectConfig project, string directory)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            string path = Path.Combine(directory, "enums");
            var enums = IOHelper.GetAllFiles(path, "enm");
            foreach (var file in enums)
            {
                var type = DeSerializer<EnumConfig>(file);
                type.SaveFileName = file;
                type.IsFreeze = true;
                if (type.IsDelete)
                    continue;
                project.Add(type);
            }
        }

        private void LoadApi(ProjectConfig project,string directory)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            string path = Path.Combine(directory, "apis");
            var apis = IOHelper.GetAllFiles(path, "ent");
            foreach (var file in apis)
            {
                var api = DeSerializer<ApiItem>(file);
                api.SaveFileName = file;
                api.IsFreeze = false;
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
                else using (var pteam = File.OpenRead(file))
                    {
                        config = (TConfig)new DataContractSerializer(typeof(TConfig)).ReadObject(pteam);
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