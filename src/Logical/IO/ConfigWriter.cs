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
                SaveSolution(solution, Path.GetDirectoryName(filename));
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        private static void SaveGlobal()
        {
            var global = Path.GetDirectoryName(Path.GetDirectoryName(typeof(ConfigWriter).Assembly.Location));
            var directory = IOHelper.CheckPath(global, "Global");
            GlobalConfig.GlobalSolution.SaveFileName = Path.Combine(directory, "global.json");
            SaveSolution(GlobalConfig.GlobalSolution, directory);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="solution"></param>
        /// <param name="directory"></param>
        private static void SaveSolution(SolutionConfig solution, string directory)
        {
            var writer = new ConfigWriter
            {
                Solution = solution,
                Directory = directory
            };
            writer.SaveSolution();
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void SaveSolution()
        {
            using (WorkModelScope.CreateScope(WorkModel.Saving))
            {
                SaveProjects();
                SaveEnums();
                SaveApies();
                Serializer(Solution.SaveFileName, Solution);
            }
            //VersionControlItem.Current.TfsCheckIn();
        }

        private void SaveProjects()
        {
            foreach (var project in Solution.Projects.ToArray())
            {
                SaveProject(project);
            }
        }


        private void SaveEnums()
        {
            var path = IOHelper.CheckPath(Directory, "Enums");
            foreach (var type in Solution.Enums.ToArray())
            {
                SaveEnum(type, path);
            }
        }

        /// <summary>
        /// 保存API对象
        /// </summary>
        public void SaveApies()
        {
            var path = IOHelper.CheckPath(Directory, "apis");
            foreach (var api in Solution.ApiItems.ToArray())
            {
                Save(api, path, ".ent");
            }
        }

        /// <summary>
        /// 检查对象是否可以保存
        /// </summary>
        /// <param name="config">对象</param>
        /// <param name="fileName">保存路径</param>
        /// <returns>是否可以保存</returns>
        public bool CheckCanSave(FileConfigBase config, string fileName)
        {
            if (!File.Exists(fileName))
                return true;
            //if (config.IsReference && config.OriginalState.HasFlag(ConfigStateType.IsReference))
            //    return false;
            //if (config.IsFreeze && config.OriginalState.HasFlag(ConfigStateType.IsFreeze))
            //    return false;
            return !(config.Discard && config.OriginalState.HasFlag(ConfigStateType.IsDiscard));
        }

        public static void DeleteOldFile(FileConfigBase config, string fileName, bool haseChild)
        {
            if (config.SaveFileName == null || string.Equals(config.SaveFileName, fileName, StringComparison.OrdinalIgnoreCase))
                return;
            File.Delete(config.SaveFileName);
            if (haseChild)
                IOHelper.DeleteDirectory(Path.Combine(config.SaveFileName));
        }

        private void SaveEnum(EnumConfig type, string path, bool checkState = true)
        {
            var filename = Path.Combine(path, type.GetFileName(".enm"));
            if (checkState && !CheckCanSave(type, filename))
                return;
            DeleteOldFile(type, filename, false);

            if (type.IsDelete)
            {
                type.Parent.Remove(type);
            }
            else
            {
                foreach (var field in type.Items.Where(p => p.IsDelete).ToArray())
                {
                    type.Items.Remove(field);
                }
            }
            Serializer(filename, type);
        }
        /// <summary>
        /// 保存解决方案
        /// </summary>
        /// <param name="project"></param>
        /// <param name="checkState"></param>
        public void SaveProject(ProjectConfig project, bool checkState = true)
        {
            string filename = Path.Combine(Directory, "Projects", project.GetFileName(".prj"));
            DeleteOldFile(project, filename, true);

            IOHelper.CheckPath(Directory, "Projects", project.Name);
            foreach (var entity in project.Entities.ToArray())
            {
                SaveEntity(entity, checkState);
            }

            if (project.IsDelete)
            {
                SolutionConfig.Current.ProjectList.Remove(project);
            }
            Serializer(filename, project);
        }
        /// <summary>
        /// 保存实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="checkState"></param>
        public void SaveEntity(EntityConfig entity, bool checkState = true)
        {
            var filename = Path.Combine(Directory, "Projects", entity.Parent.Name, entity.GetFileName(".ent"));
            if (checkState && !CheckCanSave(entity, filename))
                return;
            DeleteOldFile(entity, filename, false);
            if (entity.IsDelete)
            {
                entity.Parent.Remove(entity);
            }
            else
            {
                //删除字段处理
                foreach (var field in entity.Properties.Where(p => p.IsDelete).ToArray())
                {
                    entity.Properties.Remove(field);
                }
                //删除命令处理
                foreach (var field in entity.Commands.Where(p => p.IsDelete).ToArray())
                {
                    entity.Commands.Remove(field);
                }
            }

            Serializer(filename, entity);
        }
        #endregion

        #region 基础支持

        /// <summary>
        /// 解决方案目录
        /// </summary>
        public string Directory;

        /// <summary>
        ///     表结构对象
        /// </summary>
        public SolutionConfig Solution;

        /// <summary>
        /// 保存通知对象
        /// </summary>
        /// <param name="config"></param>
        /// <param name="path"></param>
        /// <param name="ext"></param>
        /// <param name="checkState"></param>
        public void Save<TConfig>(TConfig config, string path, string ext, bool checkState = true)
            where TConfig : FileConfigBase
        {
            var filename = Path.Combine(path, config.GetFileName(ext));
            if (checkState && !CheckCanSave(config, filename))
                return;
            DeleteOldFile(config, filename, false);
            Serializer(filename, config);
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="TConfig"></typeparam>
        /// <param name="filename"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static bool Serializer<TConfig>(string filename, TConfig config)
            where TConfig : FileConfigBase
        {
            try
            {
                string json = JsonConvert.SerializeObject(config);
                File.WriteAllText(filename, json, Encoding.UTF8);
                config.SaveFileName = filename;
                return true;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.ToString());
                return false;
            }
            //using (var psteam = File.Create(filename))
            //{
            //    if (Equals(args, default(T)))
            //        return false;
            //    psteam.SetLength(1L);
            //    new DataContractSerializer(typeof(T)).WriteObject(psteam, (object)args);
            //    psteam.Flush();
            //    psteam.Close();
            //    return true;
            //}
        }

        #endregion
    }
}