using System.IO;
using System.Linq;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{

    /// <summary>
    /// ���ö�д��
    /// </summary>
    public class SolutionWriter: ConfigWriter
    {
        SolutionConfig Solution;


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
                solution.SaveFileName = filename;
                SaveSolution(solution);
            }
            GlobalConfig.ClearConfigDictionary();
            solution.Foreach<ConfigBase>(p => p.Option.IsNormal, p => GlobalConfig.AddConfig(p.Option));
        }

        /// <summary>
        /// ����
        /// </summary>
        public static void SaveGlobal()
        {
            var directory = GlobalConfig.CheckPath(GlobalConfig.RootPath, "Global");
            GlobalConfig.GlobalSolution.SaveFileName = Path.Combine(directory, "global.json");
            SaveSolution(GlobalConfig.GlobalSolution);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="solution"></param>
        private static void SaveSolution(SolutionConfig solution)
        {
            var writer = new SolutionWriter
            {
                Solution = solution,
            };
            writer.SaveSolution();
        }

        /// <summary>
        /// ����
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
                ProjectWriter.SaveProject(project, directory);
            }
        }

    }
}