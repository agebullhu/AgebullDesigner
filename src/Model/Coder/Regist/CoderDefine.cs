using Agebull.EntityModel.Config;
using System;

namespace Agebull.EntityModel.RobotCoder
{
    /// <summary>
    /// 代码片断定义
    /// </summary>
    public class CoderDefine :SimpleConfig
    {
        public CoderDefine() : base(true){}
        /// <summary>
        /// 方法
        /// </summary>
        public Func<ConfigBase, string> Func { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public override string Name { get; set; }

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