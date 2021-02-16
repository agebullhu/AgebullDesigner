using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using System.Collections.Specialized;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 解决方案配置触发器
    /// </summary>
    public sealed class SolutionTrigger : ConfigTriggerBase<SolutionConfig>
    {
        /// <summary>
        /// 载入事件处理
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
        /// 属性事件处理
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
        ///     发出属性修改前事件
        /// </summary>
        /// <param name="property">属性</param>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
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