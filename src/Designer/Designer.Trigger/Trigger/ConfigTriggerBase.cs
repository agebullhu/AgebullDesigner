using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// TargetConfig������
    /// </summary>
    public abstract class ConfigTriggerBase<TConfig> : EventTrigger
        where TConfig : ConfigBase
    {
        /// <summary>
        /// ����
        /// </summary>
        protected ConfigTriggerBase()
        {
            TargetType = typeof(TConfig);
        }

        /// <summary>
        /// ��ǰ����
        /// </summary>
        public TConfig TargetConfig => (TConfig)Target  ;

        /// <summary>
        ///     ���������޸�ǰ�¼�
        /// </summary>
        /// <param name="property">����</param>
        /// <param name="oldValue">��ֵ</param>
        /// <param name="newValue">��ֵ</param>
        protected sealed override void BeforePropertyChanged(string property, object oldValue, object newValue)
        {
            if (TargetConfig.Option.IsReadonly)
                return;
            BeforePropertyChangedInner(property, oldValue, newValue);
        }

        /// <summary>
        ///     ���������޸�ǰ�¼�
        /// </summary>
        /// <param name="property">����</param>
        /// <param name="oldValue">��ֵ</param>
        /// <param name="newValue">��ֵ</param>
        protected abstract void BeforePropertyChangedInner(string property, object oldValue, object newValue);

        /// <summary>
        /// �����¼�����
        /// </summary>
        /// <param name="property"></param>
        protected sealed override void OnPropertyChanged(string property)
        {
            if (TargetConfig.Option.IsReadonly)
                return;
            OnPropertyChangedInner(property);
        }

        /// <summary>
        /// �����¼�����
        /// </summary>
        /// <param name="property"></param>
        protected abstract void OnPropertyChangedInner(string property);
    }

    /// <summary>
    /// TargetConfig������
    /// </summary>
    public abstract class ConfigBaseTriggerEx<TConfig> : ConfigTriggerBase<TConfig>
        where TConfig : ConfigBase
    {
        /// <summary>
        ///     ���������޸�ǰ�¼�
        /// </summary>
        /// <param name="property">����</param>
        /// <param name="oldValue">��ֵ</param>
        /// <param name="newValue">��ֵ</param>
        protected override void BeforePropertyChangedInner(string property, object oldValue, object newValue)
        {

        }

        /// <summary>
        /// �����¼�����
        /// </summary>
        /// <param name="property"></param>
        protected override void OnPropertyChangedInner(string property)
        {

        }
    }
}