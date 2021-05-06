using Agebull.EntityModel.Config;
using System;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 修复命令
    /// </summary>
    public abstract class RepairCommand : SimpleConfig
    {
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// 影响的属性
        /// </summary>
        public string Properties { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        public string Classify { get; set; }

        /// <summary>
        /// 目标编辑器
        /// </summary>
        public string Editor { get; set; }

        /// <summary>
        /// 目标视角
        /// </summary>
        public string View { get; set; }

        /// <summary>
        /// 目标对象
        /// </summary>
        public string TargetTypeName { get; set; }

        /// <summary>
        /// 目标对象
        /// </summary>
        public Type TargetType { get; set; }

        /// <summary>
        /// 执行修复
        /// </summary>
        /// <param name="config"></param>
        public abstract void DoRepair(ConfigBase config);
    }



    /// <summary>
    /// 修复命令
    /// </summary>
    public class RepairCommand<TConfig> : RepairCommand
        where TConfig : ConfigBase
    {
        /// <summary>
        /// 构造
        /// </summary>
        public RepairCommand()
        {
            TargetType = typeof(TConfig);
        }

        /// <summary>
        /// 条件
        /// </summary>
        public Func<TConfig, bool> Condition { get; set; }

        /// <summary>
        /// 方法
        /// </summary>
        public Action<TConfig> Action { get; set; }

        /// <summary>
        /// 执行修复动作
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public override void DoRepair(ConfigBase config)
        {
            if (config is TConfig target)
            {
                if (Condition == null || Condition(target))
                    Action.Invoke(target);
            }
        }
    }
}