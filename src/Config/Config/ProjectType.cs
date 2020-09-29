/*design by:agebull designer date:2017/7/12 22:06:39*/
/*****************************************************
©2008-2017 Copy right by agebull.hu(胡天水)
作者:agebull.hu(胡天水)
工程:Agebull.Common.Config
建立:2014-12-03
修改:2017-07-12
*****************************************************/

using System;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 项目类型
    /// </summary>
    [Flags]
    public enum ProjectType
    {
        /// <summary>
        /// BS应用
        /// </summary>
        WebApplition = 0x1,
        /// <summary>
        /// CS应用
        /// </summary>
        WpfApplition = 0x2,
        /// <summary>
        /// WebApi应用
        /// </summary>
        WebApi = 0x3,
    }
}