using System.Linq;

namespace Agebull.EntityModel.RobotCoder
{
    public sealed class EntityValidateBuilder : EntityBuilderBase
    {
        /// <summary>
        /// 基本代码
        /// </summary>
        public override string BaseCode=> ValidateCode();

        /// <summary>
        /// 校验代码
        /// </summary>
        /// <returns></returns>
        public string ValidateCode()
        {
            var coder = new EntityValidateCoder {Entity = Entity};
            var code = coder.Code(Columns.Where(p => !p.DbInnerField && !p.IsSystemField && !p.CustomWrite));
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
            {(Entity.NoDataBase || Entity.PrimaryColumn== null ? "" : "result.Id = " + Entity.PrimaryColumn.Name + ".ToString()") }; 
            base.Validate(result);{code}
            ValidateEx(result);
        }}";
        }
    }
}