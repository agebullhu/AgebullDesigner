using Agebull.EntityModel.Config;
using System.Linq;
using System.Text;

namespace Agebull.EntityModel.RobotCoder
{
    public sealed class EntityValidateBuilder<TModel> : ModelBuilderBase<TModel>
        where TModel : ProjectChildConfigBase, IEntityConfig
    {
        /// <summary>
        /// 基本代码
        /// </summary>
        public override string BaseCode => Model.HaseValidateCode ? ValidateCode() : null;

        /// <summary>
        /// 基本代码
        /// </summary>
        protected override bool CanWrite => Model.HaseValidateCode;
        
        /// <summary>
        /// 校验代码
        /// </summary>
        /// <returns></returns>
        public string ValidateCode()
        {
            var coder = new EntityValidateCoder {Model = Model};
            var code = coder.Code(Columns.Where(p => !p.DbInnerField && !p.IsSystemField && !p.CustomWrite));
            var rela = new StringBuilder();
            if(Model is ModelConfig model)
            foreach (var relation in model.Releations.Where(p => p.ModelType != ReleationModelType.ExtensionProperty).OrderBy(p => p.Index))
            {
                if(relation.ModelType == ReleationModelType.Children)
                {
                    rela.Append($@"
            if({relation.Name} != null)
                foreach(var ch in {relation.Name})
                    ch?.Validate(result);");
                }
                else
                {
                    rela.Append($@"
            {relation.Name}?.Validate(result);");
                }
            }

            return $@"

        /// <summary>
        /// 扩展校验
        /// </summary>
        /// <param name=""result"">结果存放处</param>
        partial void ValidateEx(ValidateResult result);

        /// <summary>
        /// 数据校验
        /// </summary>
        /// <param name=""result"">结果存放处</param>
        public override void Validate(ValidateResult result)
        {{
            {(Model.NoDataBase || Model.PrimaryColumn== null ? "" : "result.Id = " + Model.PrimaryColumn.Name + "?.ToString()") }; 
            base.Validate(result);{code}
            ValidateEx(result);{rela}
        }}";
        }
    }
}