using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using System.Collections.Specialized;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// ����������ô�����
    /// </summary>
    public sealed class SolutionTrigger : ConfigTriggerBase<SolutionConfig>
    {
        /// <summary>
        /// �����¼�����
        /// </summary>
        protected override void OnLoad()
        {
            SolutionModel model = new SolutionModel
            {
                Solution = Target
            };

            model.RepairByLoaded();
            model.ResetStatus();
            model.OnSolutionLoad();

            foreach (var project in Target.Projects)
            {
                project.OnLoad();
            }

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
                case nameof(Target.IdDataType):
                    Target.Foreach<FieldConfig>(p =>
                    {
                        if (p.IsPrimaryKey || p.IsLinkField)
                            p.DataType = Target.IdDataType;
                    });
                    break;
                case nameof(Target.UserIdDataType):
                    Target.Foreach<FieldConfig>(p =>
                    {
                        if (p.IsUserId)
                            p.DataType = Target.UserIdDataType;
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
                case nameof(Target.RootPath):
                    SolutionModel model = new SolutionModel
                    {
                        Solution = Target
                    };
                    model.CheckProjectPath((string)oldValue, (string)newValue);
                    break;
            }
        }
    }
}