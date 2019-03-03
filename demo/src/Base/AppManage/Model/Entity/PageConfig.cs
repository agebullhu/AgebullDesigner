using System.Runtime.Serialization;
using Gboxt.Common.DataModel;
using Newtonsoft.Json;

namespace Agebull.Common.AppManage
{
    public static class EnumHelper
    {
        /// <summary>
        ///     Ӧ�ù�����������ת��
        /// </summary>
        public static string ToCaption(this ClassifyType value)
        {
            switch (value)
            {
                case ClassifyType.None:
                    return "��Ȩ��";
                case ClassifyType.Hospital:
                    return "ҽԺ";
                case ClassifyType.App:
                    return "AppӦ��";
                default:
                    return "Ӧ�ù�������(����)";
            }
        }
    }

    /// <summary>
    /// Ӧ�ù�������
    /// </summary>
    /// <remark>
    /// Ӧ�ù�������
    /// </remark>
    public enum ClassifyType
    {
        /// <summary>
        /// ��Ȩ��
        /// </summary>
        None = 0x0,
        /// <summary>
        /// ҽԺ
        /// </summary>
        Hospital = 0x1,
        /// <summary>
        /// AppӦ��
        /// </summary>
        App = 0x2,
    }
    /// <summary>
    /// ҳ������
    /// </summary>
    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class PageConfig : IApiArgument
    {
        /// <summary>
        /// ҳ��ID
        /// </summary>
        [JsonProperty("pid", NullValueHandling = NullValueHandling.Ignore)]
        public long PageId { get; set; }

        /// <summary>
        /// ϵͳ�ڲ�������
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string SystemType { get; set; }
        /// <summary>
        /// �Ƿ���ʾ
        /// </summary>
        [JsonProperty("hide", NullValueHandling = NullValueHandling.Ignore)]
        public bool Hide { get; set; }
        /// <summary>
        /// �Ƿ���������
        /// </summary>
        [JsonProperty("audit", NullValueHandling = NullValueHandling.Ignore)]
        public bool Audit { get; set; }

        /// <summary>
        /// �ܷ�������
        /// </summary>
        [JsonProperty("level_audit", NullValueHandling = NullValueHandling.Ignore)]
        public bool LevelAudit { get; set; }

        /// <summary>
        /// ����ҳ��
        /// </summary>
        [JsonProperty("audit_page", NullValueHandling = NullValueHandling.Ignore)]
        public int AuditPage { get; set; }

        /// <summary>
        /// ��ҳ��
        /// </summary>
        [JsonProperty("master_page", NullValueHandling = NullValueHandling.Ignore)]
        public int MasterPage { get; set; }

        /// <summary>
        /// �Ƿ����ݹ���
        /// </summary>
        [JsonProperty("data_state", NullValueHandling = NullValueHandling.Ignore)]
        public bool DataState { get; set; }

        /// <summary>
        /// ��׼�༭
        /// </summary>
        [JsonProperty("edit", NullValueHandling = NullValueHandling.Ignore)]
        public bool Edit { get; set; }

        bool IApiArgument.Validate(out string message)
        {
            message = null;
            return true;
        }
    }
}