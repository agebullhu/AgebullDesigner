using System.IO;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    /// <summary>
    /// 解决方案生成器
    /// </summary>
    public sealed class SolutionBuilder : CoderWithProject
    {
        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_Model_Solution_cs";

        /// <summary>
        ///     生成基础代码
        /// </summary>
        protected override void CreateCustomCode(string path)
        {
            new SolutionAnalysis().SyncSolutionFile();
        }

    }


}