using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// Target������
    /// </summary>
    public sealed class OptionTrigger : EventTrigger<ConfigDesignOption>, IEventTrigger
    {
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
                case nameof(ConfigDesignOption.IsDelete):
                    GlobalConfig.RemoveConfig(TargetConfig);
                    break;
            }
        }
    }
}