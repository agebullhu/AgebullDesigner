using Agebull.EntityModel.Config;
using System.Diagnostics;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// Target������
    /// </summary>
    public sealed class ConfigTrigger : ConfigTriggerBase<ConfigBase>
    {
        /// <summary>
        ///     ���������޸�ǰ�¼�
        /// </summary>
        /// <param name="property">����</param>
        /// <param name="oldValue">��ֵ</param>
        /// <param name="newValue">��ֵ</param>
        protected override void BeforePropertyChangedInner(string property, object oldValue, object newValue)
        {
            Trace.WriteLineIf(SolutionConfig.Current.DetailTrace,
                $"[{oldValue}] => [{newValue}]",
                $"�����޸�({Target.GetType().Name }) : {Target.Name}.{property}");
            switch (property)
            {
                case nameof(Target.Name):
                    var option = Target.Option;
                    var now = (string)newValue;
                    if (!string.IsNullOrWhiteSpace(now) && !option.OldNames.Contains(now) && now != "NewField")
                        option.OldNames.Add(now);
                    option.RaisePropertyChanged(nameof(option.NameHistory));
                    break;
            }
        }
    }
}