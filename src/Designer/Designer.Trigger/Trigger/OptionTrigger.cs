using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// Target������
    /// </summary>
    public sealed class OptionTrigger : EventTrigger<ConfigDesignOption>
    {
        /// <summary>
        ///     ���������޸�ǰ�¼�
        /// </summary>
        /// <param name="property">����</param>
        /// <param name="oldValue">��ֵ</param>
        /// <param name="newValue">��ֵ</param>
        protected override void BeforePropertyChanged(string property, object oldValue, object newValue)
        {
            switch (property)
            {
                case nameof(ConfigDesignOption.Key):
                    GlobalConfig.RemoveConfig(Target);
                    break;
            }
        }

        /// <summary>
        /// �����¼�����
        /// </summary>
        /// <param name="property"></param>
        protected override void OnPropertyChanged(string property)
        {
            switch (property)
            {
                case nameof(ConfigDesignOption.Key):
                    GlobalConfig.AddConfig(Target);
                    break;
                case nameof(ConfigDesignOption.IsDiscard):
                case nameof(ConfigDesignOption.IsDelete):
                    GlobalConfig.RemoveConfig(Target);
                    break;
                case nameof(ConfigDesignOption.IsModify):
                    if (Target.IsModify && Target.Config != null)
                        Target.Config.IsModify = true;
                    break;
            }
        }
    }
}