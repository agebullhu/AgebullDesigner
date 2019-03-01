using Agebull.EntityModel.Config;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Designer.AssemblyAnalyzer
{
    /// <summary>
    /// ��������
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class AssemblyConfig : FileConfigBase
    {
        /// <summary>
        /// ���������ö���
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ConfigCollection<TypeConfig> Types { get; } = new ConfigCollection<TypeConfig>();
    }
}