using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Text;
using Agebull.Common.LUA;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.Upgrade
{

    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class SelfMomentCoder : MomentCoderBase, IAutoRegister
    {
        #region ×¢²á

        /// <summary>
        /// ×¢²á´úÂë
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            MomentCoder.RegisteCoder("×ÔÉý¼¶´úÂë", "×Ö¶Î¸´ÖÆ", "cs", arg => CreateCode<EntityConfig>(arg, GetCopy));
        }


        #endregion

        #region ×Ö¶Î¸´ÖÆ

        /// <summary>
        /// ×Ö¶Î¸´ÖÆ
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private static string GetCopy(EntityConfig entity)
        {
            StringBuilder code = new StringBuilder();
            

            code.Append($@"
    partial class {entity.EntityName}
    {{
        /// <summary>
        /// ×Ö¶Î¸´ÖÆ
        /// </summary>
        /// <param name=""dest""></param>
        /// <returns></returns>
        protected override void CopyFrom(SimpleConfig dest)
        {{
            base.CopyFrom(dest);
            var cfg = dest as {entity.EntityName};
            if(cfg == null)
                return;
");

            foreach (var field in entity.LastProperties)
            {
                if (!field.CanSet || field.IsPrivateField)
                    continue;
                if (field.IsArray)
                {
                    code.Append($@"
            foreach (var item in cfg.{field.Name})//{field.Caption.Replace('\n', ' ')}
            {{
                var child = new {field.CustomType}();
                child.Copy(item);
                {field.Name}.Add(child);
            }}");
                }
                else
                {

                    code.Append($@"
            {field.Name} = cfg.{field.Name};//{field.Caption.Replace('\n', ' ')}");
                }
            }

            code.Append(@"
        }

    }");
            return code.ToString();
        }

        #endregion
    }
}