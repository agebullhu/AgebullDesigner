using System.ComponentModel.Composition;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.Upgrade
{

    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class SelfMomentCoder : MomentCoderBase, IAutoRegister
    {
        #region ע��

        /// <summary>
        /// ע�����
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            MomentCoder.RegisteCoder("����������", "�ֶθ���", "cs", GetCopy);
        }


        #endregion

        #region �ֶθ���

        /// <summary>
        /// �ֶθ���
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
        /// �ֶθ���
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