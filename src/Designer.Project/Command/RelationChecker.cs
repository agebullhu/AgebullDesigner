using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 实体关联关系检查器
    /// </summary>
    public class RelationChecker
    {
        internal static string Prepare()
        {
            return null;
        }

        /// <summary>
        ///     分析程序集
        /// </summary>
        /// <returns></returns>
        internal static int DoChecke(EntityConfig file)
        {
            return 0;
        }

        internal static void End(CommandStatus status, Exception ex, int count)
        {
            if (status != CommandStatus.Succeed)
            {
                MessageBox.Show("错误");
                return;
            }
            MessageBox.Show("完成");
        }
    }
}
