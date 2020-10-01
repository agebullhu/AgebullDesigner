using System;
using System.Linq;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    /// <summary>
    /// ���������
    /// </summary>
    public abstract class CoderWithModel<TModel> : CoderWithProject
        where TModel:ProjectChildConfigBase,IEntityConfig
    {
        /// <summary>
        /// ��ǰ����
        /// </summary>
        public sealed override ConfigBase CurrentConfig => (ConfigBase)Model ?? base.Project;

        /// <inheritdoc />
        public sealed override ProjectConfig Project
        {
            get => Model?.Parent ?? base.Project;
            set => base.Project = value;
        }

        /// <summary>
        /// ��ǰ�����
        /// </summary>
        public TModel Model { get; set; }

        /// <summary>
        /// ��ǰ�����
        /// </summary>
        public IFieldConfig PrimaryProperty => Model.PrimaryColumn;

        /// <summary>
        /// ����Ŀ¼
        /// </summary>
        protected virtual string Folder => string.IsNullOrWhiteSpace(Model.Classify) || Model.Classify.Equals("None", StringComparison.InvariantCulture)
                ? Model.Name
                : $"{Model.Classify}\\{Model.Name}";

        /// <summary>
        /// �Ƿ��д
        /// </summary>
        protected override bool CanWrite => base.CanWrite && Model != null && !Model.IsFreeze && !Model.IsDiscard;

        /// <summary>
        /// ȡ������������������
        /// </summary>
        /// <typeparam name="TBuilder"></typeparam>
        /// <returns></returns>
        public string GetBaseCode<TBuilder>() where TBuilder : ModelBuilderBase<TModel>, new()
        {
            using (CodeGeneratorScope.CreateScope(Model))
            {
                var builder = new TBuilder
                {
                    Model = Model
                };
                return builder.BaseCode;
            }
        }
        /// <summary>
        /// ȡ������������������
        /// </summary>
        /// <typeparam name="TBuilder"></typeparam>
        /// <returns></returns>
        public string GetExtendCode<TBuilder>()
            where TBuilder : ModelBuilderBase<TModel>, new()
        {
            using (CodeGeneratorScope.CreateScope(Model))
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