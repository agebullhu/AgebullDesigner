using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// Target������
    /// </summary>
    public class ConfigTriggerBase<TConfig> : EventTrigger<TConfig>
        where TConfig : ConfigBase
    {
        /// <summary>
        ///     ���������޸�ǰ�¼�
        /// </summary>
        /// <param name="property">����</param>
        /// <param name="oldValue">��ֵ</param>
        /// <param name="newValue">��ֵ</param>
        protected sealed override void BeforePropertyChanged(string property, object oldValue, object newValue)
        {
            if (Target.Option.IsReadonly)
                return;
            BeforePropertyChangedInner(property, oldValue, newValue);
        }

        /// <summary>
        ///     ���������޸�ǰ�¼�
        /// </summary>
        /// <param name="property">����</param>
        /// <param name="oldValue">��ֵ</param>
        /// <param name="newValue">��ֵ</param>
        protected virtual void BeforePropertyChangedInner(string property, object oldValue, object newValue) { }

        /// <summary>
        /// �����¼�����
        /// </summary>
        /// <param name="property"></param>
        protected sealed override void OnPropertyChanged(string property)
        {
            if (Target.Option.IsReadonly)
                return;
            OnPropertyChangedInner(property);
        }

        /// <summary>
        /// �����¼�����
        /// </summary>
        /// <param name="property"></param>
        protected virtual void OnPropertyChangedInner(string property) { }
    }
}