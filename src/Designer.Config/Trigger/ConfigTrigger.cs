using System;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// TargetConfig������
    /// </summary>
    public class ConfigTrigger : ConfigBaseTrigger<ConfigBase>
    {
        /// <summary>
        /// �����¼�����
        /// </summary>
        protected override void OnLoad()
        {
            if (TargetConfig.Key == Guid.Empty)
                TargetConfig.Key = Guid.NewGuid();

            GlobalConfig.AddConfig(TargetConfig);
        }

        /// <summary>
        ///     ���������޸�ǰ�¼�
        /// </summary>
        /// <param name="property">����</param>
        /// <param name="oldValue">��ֵ</param>
        /// <param name="newValue">��ֵ</param>
        protected override void BeforePropertyChangedInner(string property, object oldValue, object newValue)
        {
            switch (property)
            {
                case nameof(TargetConfig.Key):
                    GlobalConfig.ConfigDictionary.Remove((Guid)oldValue);
                    break;
                case nameof(TargetConfig.Name):
                    var now = (string)newValue;
                    if (!string.IsNullOrWhiteSpace(now) && !TargetConfig.OldNames.Contains(now) && now != "NewField")
                        TargetConfig.OldNames.Add(now);
                    TargetConfig.RaisePropertyChanged(nameof(TargetConfig.NameHistory));
                    break;
            }
        }

        /// <summary>
        /// �����¼�����
        /// </summary>
        /// <param name="property"></param>
        protected override void OnPropertyChangedInner(string property)
        {
            switch (property)
            {
                case nameof(TargetConfig.Key):
                    GlobalConfig.ConfigDictionary.Add(TargetConfig.Key, TargetConfig);
                    break;
            }
        }
    }
}