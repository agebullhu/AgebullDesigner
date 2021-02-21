using System;
using System.Text;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.Upgrade
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class UpgradeMomentCoder : MomentCoderBase, IAutoRegister
    {
        #region 注册

        void IAutoRegister.AutoRegist()
        {
            CoderManager.RegisteCoder("升级", "CopyFrom", "cs", UpdateConfig);
            CoderManager.RegisteCoder("升级", "类定义", "cs", ClassDefault);
            //CoderManager.RegisteCoder("Web-Vue", "Script", "js", ScriptCode);
        }
        #endregion
        #region 设计

        string ClassDefault(IEntityConfig entity)
        {
            var config = entity as ConfigBase;
            var code = new StringBuilder();
            code.Append($@"
namespace {entity.Project.NameSpace}
{{
{config.RemCode().SpaceLine(4)}
    [JsonObject(MemberSerialization.OptIn, ItemNullValueHandling = NullValueHandling.Ignore)]
    public partial class {entity.Name} : {entity.ModelBase}, {entity.Interfaces}
    {{
        #region 字段");
            foreach (var property in entity.LastProperties)
            {
                var field = property as ConfigBase;
                code.Append($@"

{field.RemCode().SpaceLine(8)}
        [DataMember, JsonProperty(""{property.JsonName}"", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal {property.LastCsType} _{property.JsonName};

{field.RemCode().SpaceLine(8)}
        [IgnoreDataMember, JsonIgnore]
        [Category(""{property.Group}""), DisplayName(@""{property.Caption}""), Description(@""{property.Description}"")]
        public {property.LastCsType} {property.Name}
        {{
            get => _{property.JsonName};
            set
            {{
                if (_{property.JsonName} == value)
                    return;
                BeforePropertyChanged(nameof({property.Name}), _{property.JsonName}, value);
                _{property.JsonName} = value;
                OnPropertyChanged(nameof({property.Name}));
            }}
        }}");
            }
            code.Append($@"
        #endregion 字段

        #region 兼容性升级

        /// <summary>
        /// 兼容性升级
        /// </summary>
        public void Upgrade()
        {{
            //Copy(Entity);
        }}

        #endregion

        #region 字段复制

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name=""dest"">复制源</param>
        public void Copy({entity.Interfaces} dest)
        {{
            CopyFrom((SimpleConfig)dest);
        }}

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name=""dest"">复制源</param>
        protected override void CopyFrom(SimpleConfig dest)
        {{
            base.CopyFrom(dest);
            if (dest is {entity.Name} cfg)
                CopyProperty(cfg);
        }}

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name=""dest"">复制源</param>
        /// <returns></returns>
        public void CopyProperty({entity.Interfaces} dest)
        {{");
            foreach (var property in entity.LastProperties.Where(p => p.CanGet && p.CanSet))
            {
                code.Append($@"
            {property.Name} = dest.{property.Name};");
            }
            code.Append(@"
        }
        #endregion 字段复制
    }
}");
            return code.ToString();
        }
        #endregion

        string UpdateConfig()
        {
            var baseType = typeof(SimpleConfig);
            var code = new StringBuilder();
            var types = typeof(ConfigBase).Assembly.GetTypes();
            foreach (var type in types.Where(p => p.IsSubclassOf(baseType)))
            {
                CopyCode(type, code);
            }
            CopyCode(typeof(IFieldConfig), code);
            CopyCode(typeof(IEntityConfig), code);
            return code.ToString();
        }

        readonly BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;
        void CopyCode(Type type, StringBuilder code)
        {
            var properties = type.GetProperties(bindingFlags);
            code.Append($@"
    partial class {type.Name}
    {{
        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name=""dest"">复制源</param>
        protected override void CopyFrom(SimpleConfig dest)
        {{
            base.CopyFrom(dest);
            if (dest is {type.Name} cfg)
                CopyProperty(cfg);
        }}

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name=""dest"">复制源</param>
        public void Copy({type.Name} dest)
        {{
            CopyFrom(dest);
        }}

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name=""dest"">复制源</param>
        /// <returns></returns>
        public void CopyProperty({type.Name} dest)
        {{");
            foreach (var property in properties.Where(p => p.CanWrite && p.CanRead))
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