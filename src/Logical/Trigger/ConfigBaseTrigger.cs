using Agebull.Common.DataModel;

namespace Gboxt.Common.DataAccess.Schemas
{
    /// <summary>
    /// TargetConfig������
    /// </summary>
    public abstract class ConfigBaseTrigger<TConfig> : EventTrigger
        where TConfig : ConfigBase
    {
        /// <summary>
        /// ����
        /// </summary>
        protected ConfigBaseTrigger()
        {
            TargetType = typeof(TConfig);
        }

        /// <summary>
        /// ��ǰ����
        /// </summary>
        public TConfig TargetConfig => (TConfig)Target;

        /// <summary>
        ///     ���������޸�ǰ�¼�
        /// </summary>
        /// <param name="property">����</param>
        /// <param name="oldValue">��ֵ</param>
        /// <param name="newValue">��ֵ</param>
        protected override void BeforePropertyChanged(string property, object oldValue, object newValue)
        {
            if (TargetConfig.IsPredefined)
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
        protected override void OnPropertyChanged(string property)
        {
            if (TargetConfig.IsPredefined)
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
    public abstract class ConfigBaseTriggerEx<TConfig> : ConfigBaseTrigger<TConfig>
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