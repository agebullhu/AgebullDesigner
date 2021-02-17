using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// Target������
    /// </summary>
    public sealed class OptionTrigger : EventTrigger<ConfigDesignOption>, IEventTrigger
    {
        /// <summary>
        ///     ���������޸�ǰ�¼�
        /// </summary>
        /// <param name="property">����</param>
        /// <param name="oldValue">��ֵ</param>
        /// <param name="newValue">��ֵ</param>
        public override void BeforePropertyChanged(string property, object oldValue, object newValue)
        {
            switch (property)
            {
                case nameof(ConfigDesignOption.Key):
                    GlobalConfig.RemoveConfig(TargetConfig);
                    break;
            }
        }

        /// <summary>
        /// �����¼�����
        /// </summary>
        /// <param name="property"></param>
        public override void OnPropertyChanged(string property)
        {
            switch (property)
            {
                case nameof(ConfigDesignOption.Key):
                    GlobalConfig.AddConfig(TargetConfig);
                    break;
                case nameof(ConfigDesignOption.IsDiscard):
                case nameof(ConfigDesignOption.IsDelete):
                    GlobalConfig.RemoveConfig(TargetConfig);
                    break;
                case nameof(ConfigDesignOption.IsModify):
                    if (TargetConfig.IsModify && TargetConfig.Config != null)
                        TargetConfig.Config.IsModify = true;
                    break;
            }
        }
    }
}