using Agebull.EntityModel.Config.V2021;
using System;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 实体配置触发器
    /// </summary>
    public sealed class DataTableTrigger : EventTrigger<DataTableConfig>, IEventTrigger
    {
        /// <summary>
        /// 属性事件处理
        /// </summary>
        /// <param name="property"></param>
        void IEventTrigger.OnPropertyChanged(string property)
        {
            switch (property)
            {
                case nameof(TargetConfig.SaveTableName):
                    TargetConfig.RaisePropertyChanged(nameof(TargetConfig.ReadTableName));
                    TargetConfig.RaisePropertyChanged(nameof(TargetConfig.SaveTableName));
                    break;
                case nameof(TargetConfig.ReadTableName):
                    TargetConfig.RaisePropertyChanged(nameof(TargetConfig.SaveTableName));
                    TargetConfig.RaisePropertyChanged(nameof(TargetConfig.SaveTableName));
                    break;
            }
        }

        /// <summary>
        /// 规整对象
        /// </summary>
        public void Regularize()
        {
            if (TargetConfig.ReadTableName.IsMissing())
                TargetConfig.ReadTableName = TargetConfig.SaveTableName;
        }
    }
}


