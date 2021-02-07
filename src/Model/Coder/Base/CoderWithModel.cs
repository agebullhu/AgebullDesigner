using System;
using System.Linq;
using System.Resources;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    /// <summary>
    /// 表操作基类
    /// </summary>
    public abstract class CoderWithModel : CoderWithProject
    {
        /// <summary>
        /// 当前对象
        /// </summary>
        public sealed override ConfigBase CurrentConfig => (ConfigBase)Model ?? base.Project;

        /// <inheritdoc />
        public sealed override ProjectConfig Project
        {
            get => Model?.Project ?? base.Project;
            set => base.Project = value;
        }

        /// <summary>
        /// 当前表对象
        /// </summary>
        public IEntityConfig Model { get; set; }

        /// <summary>
        /// 当前表对象
        /// </summary>
        public IFieldConfig PrimaryProperty => Model.PrimaryColumn;

        /// <summary>
        /// 分类目录
        /// </summary>
        protected virtual string Folder => string.IsNullOrWhiteSpace(Model.Classify) || Model.Classify.Equals("None", StringComparison.InvariantCulture)
                ? Model.Name
                : $"{Model.Classify}\\{Model.Name}";

        /// <summary>
        /// 是否可写
        /// </summary>
        protected override bool CanWrite => base.CanWrite && Model != null && !Model.IsFreeze && !Model.IsDiscard;

        /// <summary>
        /// 取得其它生成器的能力
        /// </summary>
        /// <typeparam name="TBuilder"></typeparam>
        /// <returns></returns>
        public string GetBaseCode<TBuilder>() where TBuilder : ModelBuilderBase, new()
        {
            using (CodeGeneratorScope.CreateScope(Model as NotificationObject))
            {
                var builder = new TBuilder
                {
                    Model = Model
                };
                return builder.BaseCode;
            }
        }
        /// <summary>
        /// 取得其它生成器的能力
        /// </summary>
        /// <typeparam name="TBuilder"></typeparam>
        /// <returns></returns>
        public string GetExtendCode<TBuilder>()
            where TBuilder : ModelBuilderBase, new()
        {
            using (CodeGeneratorScope.CreateScope(Model as NotificationObject))
            {
                var builder = new TBuilder
                {
                    Model = Model
                };
                return builder.ExtendCode;
            }
        }

    }
}