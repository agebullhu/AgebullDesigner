namespace Agebull.EntityModel.RobotCoder
{
    public sealed class EntityValidateBuilder : EntityBuilderBase
    {
        public override string BaseCode=> ValidateCode();

        protected override string Folder => "Validate";
        
        #region ����У��

        public string ValidateCode()
        {
            EntityValidateCoder coder = new EntityValidateCoder {Entity = Entity};
            return $@"

        /// <summary>
        /// ��չУ��
        /// </summary>
        /// <param name=""result"">�����Ŵ�</param>
        partial void ValidateEx(ValidateResult result);

        /// <summary>
        /// ����У��
        /// </summary>
        /// <param name=""result"">�����Ŵ�</param>
        public override void Validate(ValidateResult result)
        {{
            {(Entity.IsClass || Entity.PrimaryColumn== null ? "" : "result.Id = " + Entity.PrimaryColumn.Name + ".ToString()") }; 
            base.Validate(result);{coder.Code()}
            ValidateEx(result);
        }}";
        }

        #endregion

    }
}