using System;
using System.Collections.Generic;
using Agebull.EntityModel.Config;
using Newtonsoft.Json;

namespace Agebull.EntityModel.RobotCoder.Upgrade
{
    /// <summary>
    /// ��������
    /// </summary>
    public class UpgradeRoot : ConfigBase
    {
        /// <summary>
        /// ���������ö���
        /// </summary>
        public ConfigCollection<ClassUpgradeConfig> ConfigTypes { get; } = new ConfigCollection<ClassUpgradeConfig>();
    }

    /// <summary>
    /// ��������
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class UpgradeConfig : FileConfigBase
    {
        /// <summary>
        /// �Ƿ���DataMember����DataContract����
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool IsDataAttribute { get; set; }
        /// <summary>
        /// �Ƿ���JSON����
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool IsJsonAttribute { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Category { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string TypeName { get; set; }
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool IsArray { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public Type ConfigType { get; set; }
    }
    /// <summary>
    /// ������
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class ClassUpgradeConfig : UpgradeConfig
    {
        /// <summary>
        /// ����
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, MethodsUpgradeConfig> Methods = new Dictionary<string, MethodsUpgradeConfig>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// �ֶ�
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, FieldUpgradeConfig> Fields = new Dictionary<string, FieldUpgradeConfig>(StringComparer.OrdinalIgnoreCase);
        /// <summary>
        /// ����
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, PropertyUpgradeConfig> Properties = new Dictionary<string, PropertyUpgradeConfig>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// ����
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BaseType { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public ClassifyGroupConfig<PropertyUpgradeConfig> ClassifyGroup { get; set; }
    }
    /// <summary>
    /// �ֶ�����
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class MethodsUpgradeConfig : UpgradeConfig
    {
        /// <summary>
        /// ���ض���
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ClassUpgradeConfig Result { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, ClassUpgradeConfig> Argument { get; set; }
    }

    /// <summary>
    /// �ֶ�����
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class FieldUpgradeConfig : UpgradeConfig
    {
        /// <summary>
        /// JSON���л�����
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string JsonName { get; set; }
        /// <summary>
        /// ��Ӧ����������
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string PairProperty { get; set; }
    }
    /// <summary>
    /// ��������
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class PropertyUpgradeConfig : UpgradeConfig
    {
        /// <summary>
        /// JSON���л�����
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string JsonName { get; set; }
        /// <summary>
        /// ��Ӧ���ֶ�
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string PairField { get; set; }

        /// <summary>
        /// �Ƿ��б�
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool IsList { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; }

        /// <summary>
        /// �ܷ��
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool CanRead { get; set; }
        /// <summary>
        /// �ܷ�д
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool CanWrite { get; set; }

        public FieldUpgradeConfig Field { get; set; }
    }
}