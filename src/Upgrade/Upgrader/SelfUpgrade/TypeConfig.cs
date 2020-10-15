using System;
using System.Collections.Generic;
using Agebull.EntityModel.Config;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Designer.AssemblyAnalyzer
{
    /// <summary>
    /// ������
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class TypeConfig : UpgradeConfig
    {
        /// <summary>
        /// ����
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public NetType NetType { get; set; }

        /// <summary>
        /// ���Ͳ���
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<string> Generics { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<MethodConfig> Methods { get; set; } = new List<MethodConfig>();

        /// <summary>
        /// �ֶ�
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Dictionary<string, FieldConfig> Fields { get; set; } = new Dictionary<string, FieldConfig>(StringComparer.OrdinalIgnoreCase);
        /// <summary>
        /// ����
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Dictionary<string, PropertyUpgradeConfig> Properties { get; set; } = new Dictionary<string, PropertyUpgradeConfig>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// ����
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string BaseType { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<string> Interfaces { get; set; } = new List<string>();

    }
    /// <summary>
    /// .NET������
    /// </summary>
    public enum NetType
    {
        /// <summary>
        /// ��
        /// </summary>
        Class,
        /// <summary>
        /// �ṹ
        /// </summary>
        Struct,
        /// <summary>
        /// ö��
        /// </summary>
        Enum,
        /// <summary>
        /// �ӿ�
        /// </summary>
        Interface,
    }
}