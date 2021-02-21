using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Agebull.EntityModel.Designer
{
    public class EditorOption
    {
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 关联的工作模式
        /// </summary>
        public List<string> Filter { get; } = new List<string>();

        /// <summary>
        /// 构造器
        /// </summary>
        public Func<UserControl> Create { get; set; }
    }
}