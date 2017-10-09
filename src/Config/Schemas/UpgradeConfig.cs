using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using Agebull.EntityModel.Config;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Designer.AssemblyAnalyzer
{
    /// <summary>
    /// ��������
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class UpgradeConfig : SimpleConfig
    {
        /// <summary>
        /// ����
        /// </summary>
        /// <remark>
        /// ����(�����ÿ���)
        /// </remark>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Category("*���"), DisplayName("����"), Description("����(�����ÿ���)")]
        public string Classify
        {
            get;
            set;
        }
        /// <summary>
        /// ����������
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Dictionary<string, AttributeInfo> CustomAttributes { get; set; } = new Dictionary<string, AttributeInfo>();


        /// <summary>
        /// ����ֵ
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Dictionary<string, string> Values { get; set; } = new Dictionary<string, string>();
        
        /// <summary>
        /// �Ƿ���DataMember����DataContract����
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool IsDataAttribute { get; set; }
        /// <summary>
        /// JSON���л�����
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string JsonName { get; set; }
        /// <summary>
        /// �Ƿ���JSON����
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool IsJsonAttribute { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Category { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string TypeName { get; set; }
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool IsArray { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public Type ConfigType { get; set; }


        /// <summary>
        /// ȱʡֵ
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string DefaultValue { get; set; }
    }
}