using System.Collections.Generic;

namespace Agebull.EntityModel.RobotCoder
{
    public class SolutionLine
    {
        /// <summary>
        /// 读出来的行
        /// </summary>
        public string Line { get; set; }

        /// <summary>
        /// 更新后的行
        /// </summary>
        public string NewLine { get; set; }

        /// <summary>
        /// 解析的单词
        /// </summary>
        public List<SolutionWord> Words { get; set; }

        /// <summary>
        /// 快速访问
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public string this[int idx] => Words[idx].Word;


        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 对应的类型
        /// </summary>
        public string Project { get; set; }

    }
}