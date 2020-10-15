using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// TargetConfig������
    /// </summary>
    public class OptionTrigger : EventTrigger
    {
        /// <summary>
        /// ����
        /// </summary>
        public OptionTrigger()
        {
            TargetType = typeof(ConfigDesignOption);
        }

        /// <summary>
        /// ��ǰ����
        /// </summary>
        public ConfigDesignOption Option => (ConfigDesignOption)Target;


        /// <summary>
        /// ��ǰ����
        /// </summary>
        public ConfigBase Config => Option?.Config;

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
                    GlobalConfig.RemoveConfig(Option);
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
                    GlobalConfig.AddConfig(Option);
                    break;
                case nameof(ConfigDesignOption.IsDiscard):
                case nameof(ConfigDesignOption.IsDelete):
                    GlobalConfig.RemoveConfig(Option);
                    break;
            }
        }
    }
}