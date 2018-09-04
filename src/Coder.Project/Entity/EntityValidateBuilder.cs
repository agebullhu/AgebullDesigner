namespace Agebull.EntityModel.RobotCoder
{
    public sealed class EntityValidateBuilder : EntityBuilderBase
    {
        public override string BaseCode=> ValidateCode();

        public string ValidateCode()
        {
            EntityValidateCoder coder = new EntityValidateCoder {Entity = Entity};
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
            base.Validate(result);{coder.Code()}
            ValidateEx(result);
        }}";
        }
    }
}