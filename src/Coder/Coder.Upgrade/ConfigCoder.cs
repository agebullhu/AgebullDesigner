using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace Agebull.EntityModel.RobotCoder.Upgrade
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class ConfigCoder : MomentCoderBase, IAutoRegister
    {
        #region 注册

        void IAutoRegister.AutoRegist()
        {
            CoderManager.RegisteCoder("升级", "类定义", "cs", ClassDefault);
            CoderManager.RegisteCoder("升级", "接口定义", "cs", InterfaceDefault);
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
        [JsonIgnore]
        [Category(""{property.Group}""), DisplayName(@""{property.Caption}""), Description(@""{property.Description}"")]
        public {property.LastCsType} {property.Name}
        {{
            get => _{property.JsonName};
            set
            {{
                if (_{property.JsonName} == value)
                    return;
                BeforePropertyChange(nameof({property.Name}), _{property.JsonName}, value);
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
            if (dest is {entity.Interfaces} cfg)
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

        #region 接口

        string InterfaceDefault(IEntityConfig entity)
        {
            var config = entity as ConfigBase;
            var code = new StringBuilder();
            code.Append($@"
{config.RemCode().SpaceLine(4)}
    public interface {entity.Interfaces}
    {{");
            foreach (var property in entity.LastProperties)
            {
                var field = property as ConfigBase;
                code.Append($@"
{field.RemCode().SpaceLine(8)}
        {property.LastCsType} {property.Name} {{ get; }}");
            }
            code.Append(@"
    }");
            return code.ToString();
        }
        #endregion
    }
}