using Agebull.Common.DataModel;

namespace Gboxt.Common.DataAccess.Schemas
{

    /// <summary>
    /// ParentTrigger������
    /// </summary>
    public class ParentConfigTrigger : ConfigBaseTriggerEx<ParentConfigBase>
    {
        /// <summary>
        /// �����¼�����
        /// </summary>
        protected override void OnLoad()
        {
            if (TargetConfig.MyChilds == null)
                return;
            foreach (var child in TargetConfig.MyChilds)
                GlobalTrigger.OnLoad(child);
        }
    }
}