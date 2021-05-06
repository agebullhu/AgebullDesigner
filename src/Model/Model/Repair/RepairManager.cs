using Agebull.Common;
using Agebull.EntityModel.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 修复命令管理器
    /// </summary>
    public class RepairManager
    {
        public static readonly List<RepairCommand> RepairCommands = new List<RepairCommand>();

        public static void Regist(RepairCommand command)
        {
            RepairCommands.Add(command);
        }

        /// <summary>
        /// 执行修复
        /// </summary>
        /// <param name="config"></param>
        public void Repair(ConfigBase config)
        {
            config.Preorder<ConfigBase>(DoRepair);
        }

        /// <summary>
        /// 执行修复
        /// </summary>
        /// <param name="config"></param>
        void DoRepair(ConfigBase config)
        {
            foreach(var cmd in RepairCommands)
            {
                if (cmd.IsSelected)
                    cmd.DoRepair(config);
            }
        }
    }

}