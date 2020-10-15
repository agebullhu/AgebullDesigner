using Agebull.EntityModel.Config;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Designer.AssemblyAnalyzer
{
    /// <summary>
    /// 程序集配置
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class AssemblyConfig : FileConfigBase
    {
        /// <summary>
        /// 升级的配置对象
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ConfigCollection<TypeConfig> Types { get; } = new ConfigCollection<TypeConfig>();
    }
}