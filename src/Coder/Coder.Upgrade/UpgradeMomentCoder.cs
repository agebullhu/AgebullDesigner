using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.Upgrade
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class UpgradeMomentCoder : MomentCoderBase, IAutoRegister
    {
        #region ע��

        void IAutoRegister.AutoRegist()
        {
            MomentCoder.RegisteCoder("����", "CopyFrom", "cs", UpdateConfig);
            //MomentCoder.RegisteCoder("Web-Vue", "Script", "js", ScriptCode);
        }
        #endregion

        string UpdateConfig()
        {
            var baseType = typeof(SimpleConfig);
            var code = new StringBuilder();
            var types = typeof(ConfigBase).Assembly.GetTypes();
            foreach(var type in types.Where(p=>p.IsSubclassOf(baseType)))
            {
                CopyCode(type, code);
            }
            CopyCode(typeof(IFieldConfig), code);
            CopyCode(typeof(IEntityConfig), code);
            return code.ToString();
        }
        BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;
        void CopyCode(Type type, StringBuilder code)
        {
            var properties = type.GetProperties(bindingFlags);
            code.Append($@"
    partial class {type.Name}
    {{
        /// <summary>
        /// �ֶθ���
        /// </summary>
        /// <param name=""dest"">����Դ</param>
        protected override void CopyFrom(SimpleConfig dest)
        {{
            base.CopyFrom(dest);
            if (dest is {type.Name} cfg)
                CopyProperty(cfg);
        }}

        /// <summary>
        /// �ֶθ���
        /// </summary>
        /// <param name=""dest"">����Դ</param>
        public void Copy({type.Name} dest)
        {{
            CopyFrom(dest);
        }}

        /// <summary>
        /// �ֶθ���
        /// </summary>
        /// <param name=""dest"">����Դ</param>
        /// <returns></returns>
        public void CopyProperty({type.Name} dest)
        {{");
            foreach (var property in properties.Where(p=>p.CanWrite && p.CanRead))
            {
                code.Append($@"
            {property.Name} = dest.{property.Name};");
            }
            code.Append(@"
        }
    }
");
        }
    }
}