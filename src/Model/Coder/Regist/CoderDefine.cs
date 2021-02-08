using System;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    /// <summary>
    /// 代码片断定义
    /// </summary>
    public class CoderDefine
    {
        /// <summary>
        /// 方法
        /// </summary>
        public Func<ConfigBase, string> Func { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 语言类型
        /// </summary>
        public string Lang { get; set; }

        /// <summary>
        /// 自定义执行
        /// </summary>
        public bool CustomExecute { get; set; }
    }
}