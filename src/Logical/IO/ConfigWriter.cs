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
    /// ���ö�д��
    /// </summary>
    public class ConfigWriter
    {
        #region ����

        /// <summary>
        /// ����
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
                solution.FileName = filename;
                SaveSolution(solution, Path.GetDirectoryName(filename));
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        private static void SaveGlobal()
        {
            var global = Path.GetDirectoryName(Path.GetDirectoryName(typeof(ConfigWriter).Assembly.Location));
            var directory = IOHelper.CheckPath(global, "Global");
            GlobalConfig.GlobalSolution.FileName = Path.Combine(directory, "global.json");
            SaveSolution(GlobalConfig.GlobalSolution, directory);
        }
        /// <summary>
        /// ����
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
        /// ����
        /// </summary>
        private void SaveSolution()
        {
            using (LoadingModeScope.CreateScope())
            {
                SaveProjects();
                SaveTypedefs();
                SaveEnums();
                SaveApies();
                SaveNotifies();
                Serializer(Solution.FileName, Solution);
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

        private void SaveTypedefs()
        {
            var path = IOHelper.CheckPath(Directory, "Typedefs");
            foreach (var type in Solution.TypedefItems.ToArray())
            {
                SaveTypedef(type, path);
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
        /// ����֪ͨ����
        /// </summary>
        public void SaveNotifies()
        {
            string path = IOHelper.CheckPath(Directory, "notifies");
            foreach (var notify in Solution.NotifyItems.ToArray())
            {
                Save(notify, path, ".ent");
            }
        }
        /// <summary>
        /// ����API����
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
        /// �������Ƿ���Ա���
        /// </summary>
        /// <param name="config">����</param>
        /// <param name="fileName">����·��</param>
        /// <returns>�Ƿ���Ա���</returns>
        private bool CheckCanSave(FileConfigBase config, string fileName)
        {
            if (!File.Exists(fileName))
                return true;
            //if (config.IsReference && config.OriginalState.HasFlag(ConfigStateType.IsReference))
            //    return false;
            //if (config.IsFreeze && config.OriginalState.HasFlag(ConfigStateType.IsFreeze))
            //    return false;
            return !(config.Discard && config.OriginalState.HasFlag(ConfigStateType.IsDiscard));
        }

        private static void DeleteOldFile(FileConfigBase config, string fileName, bool haseChild)
        {
            if (config.FileName == null || string.Equals(config.FileName, fileName, StringComparison.OrdinalIgnoreCase))
                return;
            File.Delete(config.FileName);
            if (haseChild)
                IOHelper.DeleteDirectory(Path.Combine(config.FileName));
        }

        private void SaveEnum(EnumConfig type, string path, bool checkState = true)
        {
            var filename = Path.Combine(path, type.Name + ".enm");
            if (checkState && !CheckCanSave(type, filename))
                return;
            DeleteOldFile(type, filename, false);

            if (type.IsDelete)
            {
                Solution.Enums.Remove(type);
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

        private void SaveTypedef(TypedefItem type, string path, bool checkState = true)
        {
            var filename = Path.Combine(path, type.Name + ".typ");
            if (checkState && !CheckCanSave(type, filename))
                return;
            DeleteOldFile(type, filename, false);
            if (type.IsDelete)
            {
                Solution.TypedefItems.Remove(type);
            }
            else
            {
                foreach (var field in type.Items.Where(p => p.Value.IsDelete).ToArray())
                {
                    type.Items.Remove(field.Key);
                }
            }
            if (type.IsDelete)
                Solution.TypedefItems.Remove(type);
            Serializer(filename, type);
        }
        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="project"></param>
        /// <param name="checkState"></param>
        public void SaveProject(ProjectConfig project, bool checkState = true)
        {
            string filename = Path.Combine(Directory, "Projects", project.Name + ".prj");
            DeleteOldFile(project, filename, true);

            IOHelper.CheckPath(Directory, "Projects", project.Name);
            foreach (var entity in project.Entities.ToArray())
            {
                SaveEntity(entity, checkState);
            }

            if (project.IsDelete)
            {
                SolutionConfig.Current.Projects.Remove(project);
            }
            Serializer(filename, project);
        }
        /// <summary>
        /// ����ʵ��
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="checkState"></param>
        public void SaveEntity(EntityConfig entity, bool checkState = true)
        {
            var filename = Path.Combine(Directory, "Projects", entity.Parent.Name, entity.Name + ".ent");
            if (checkState && !CheckCanSave(entity, filename))
                return;
            DeleteOldFile(entity, filename, false);
            if (entity.IsDelete)
            {
                entity.Parent.Entities.Remove(entity);
            }
            else
            {
                //ɾ���ֶδ���
                foreach (var field in entity.Properties.Where(p => p.IsDelete).ToArray())
                {
                    entity.Properties.Remove(field);
                }
                //ɾ�������
                foreach (var field in entity.Commands.Where(p => p.IsDelete).ToArray())
                {
                    entity.Commands.Remove(field);
                }
            }

            Serializer(filename, entity);
        }
        #endregion

        #region ����֧��

        /// <summary>
        /// �������Ŀ¼
        /// </summary>
        public string Directory;

        /// <summary>
        ///     ��ṹ����
        /// </summary>
        public SolutionConfig Solution;

        /// <summary>
        /// ����֪ͨ����
        /// </summary>
        /// <param name="config"></param>
        /// <param name="path"></param>
        /// <param name="ext"></param>
        /// <param name="checkState"></param>
        private void Save<TConfig>(TConfig config, string path, string ext, bool checkState = true)
            where TConfig : FileConfigBase
        {
            var filename = Path.Combine(path, config.Name + ext);
            if (checkState && !CheckCanSave(config, filename))
                return;
            DeleteOldFile(config, filename, false);
            Serializer(filename, config);
        }

        /// <summary>
        /// ���л�
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
                config.FileName = filename;
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