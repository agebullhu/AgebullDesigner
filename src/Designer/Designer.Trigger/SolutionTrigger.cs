using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using System.Collections.Specialized;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// ����������ô�����
    /// </summary>
    public sealed class SolutionTrigger : ConfigTriggerBase<SolutionConfig>, IEventTrigger
    {
        /// <summary>
        /// �����¼�����
        /// </summary>
        public override void OnLoad()
        {
            SolutionModel model = new()
            {
                Solution = TargetConfig
            };
            model.RepairByLoaded();
            model.OnSolutionLoad();

            foreach (var project in TargetConfig.Projects)
            {
                project.OnLoad();
            }

            TargetConfig.Solution.ResetStatus();
            CommandCoefficient.ClearCommand();
        }

        /// <summary>
        /// �����¼�����
        /// </summary>
        /// <param name="property"></param>
        protected override void OnPropertyChangedInner(string property)
        {
            switch (property)
            {
                case nameof(TargetConfig.IdDataType):
                    TargetConfig.Preorder<FieldConfig>(p =>
                    {
                        if (p.IsPrimaryKey || p.IsLinkField)
                            p.DataType = TargetConfig.IdDataType;
                    });
                    break;
            }
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
                case nameof(TargetConfig.RootPath):
                    SolutionModel model = new()
                    {
                        Solution = TargetConfig
                    };
                    model.CheckProjectPath((string)oldValue, (string)newValue);
                    break;
            }
        }
    }
}