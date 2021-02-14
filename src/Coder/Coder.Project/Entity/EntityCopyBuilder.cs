using System.Text;

namespace Agebull.EntityModel.RobotCoder
{
    public sealed class EntityCopyBuilder : ModelBuilderBase
    {
        #region 基础

        /// <summary>
        /// 是否客户端代码
        /// </summary>
        protected override bool IsClient => false;

        public override string BaseCode => $@"
        #region 复制
        {Copy()}
        #endregion";

        protected override string Folder => "Struct";

        #endregion

        #region 复制

        private string Copy()
        {
            var code = new StringBuilder();
            var type = IsClient ? "EntityObjectBase" : "DataObjectBase";
            code.Append($@"

        partial void CopyExtendValue({Model.EntityName} source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name=""source"">复制的源字段</param>
        protected override void CopyValueInner({type} source)
        {{");

            if (!string.IsNullOrWhiteSpace(Model.ModelBase))
                code.AppendLine(@"
            base.CopyValueInner(source);");

            code.Append($@"
            var sourceEntity = source as {Model.EntityName};
            if(sourceEntity == null)
                return;");

            foreach (var property in ReadWriteColumns)
            {
                if (property.IsCustomCompute)
                {
                    if (property.CanSet)
                        code.Append($@"
            this.{property.Name} = sourceEntity.{property.Name};");
                    continue;
                }
                code.Append($@"
            this._{property.Name.ToLWord()} = sourceEntity._{property.Name.ToLWord()};");
            }
            code.Append(@"
            CopyExtendValue(sourceEntity);");
            if (!IsClient)
                code.Append(@"
            this.__status.IsModified = true;");
            code.Append(@"
        }");
            //return code.ToString();
            code.Append($@"

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name=""source"">复制的源字段</param>
        public void Copy({Model.EntityName} source)
        {{");

            if (!string.IsNullOrWhiteSpace(Model.ModelBase))
                code.AppendLine(@"
            base.CopyValueInner(source);");


            foreach (var property in ReadWriteColumns)
            {
                code.Append($@"
                this.{property.Name} = source.{property.Name};");
            }
            code.Append(@"
        }");


            return code.ToString();
        }
        #endregion
    }
}