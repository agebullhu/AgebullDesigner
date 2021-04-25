using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using System.Collections.Specialized;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 解决方案配置触发器
    /// </summary>
    public sealed class ProjectTrigger : ConfigTriggerBase<ProjectConfig>, IEventTrigger
    {
        public override void OnAdded(object parent)
        {
            TargetConfig.DbType = DataBaseType.MySql;
            TargetConfig.NoClassify = true;
        }
    }
}