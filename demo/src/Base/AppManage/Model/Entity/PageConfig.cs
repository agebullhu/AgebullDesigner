using System.Runtime.Serialization;
using Gboxt.Common.DataModel;
using Newtonsoft.Json;

namespace Agebull.Common.AppManage
{
    public static class EnumHelper
    {
        /// <summary>
        ///     应用归类类型名称转换
        /// </summary>
        public static string ToCaption(this ClassifyType value)
        {
            switch (value)
            {
                case ClassifyType.None:
                    return "无权限";
                case ClassifyType.Hospital:
                    return "医院";
                case ClassifyType.App:
                    return "App应用";
                default:
                    return "应用归类类型(错误)";
            }
        }
    }

    /// <summary>
    /// 应用归类类型
    /// </summary>
    /// <remark>
    /// 应用归类类型
    /// </remark>
    public enum ClassifyType
    {
        /// <summary>
        /// 无权限
        /// </summary>
        None = 0x0,
        /// <summary>
        /// 医院
        /// </summary>
        Hospital = 0x1,
        /// <summary>
        /// App应用
        /// </summary>
        App = 0x2,
    }
    /// <summary>
    /// 页面配置
    /// </summary>
    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class PageConfig : IApiArgument
    {
        /// <summary>
        /// 页面ID
        /// </summary>
        [JsonProperty("pid", NullValueHandling = NullValueHandling.Ignore)]
        public long PageId { get; set; }

        /// <summary>
        /// 系统内部的类名
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string SystemType { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        [JsonProperty("hide", NullValueHandling = NullValueHandling.Ignore)]
        public bool Hide { get; set; }
        /// <summary>
        /// 是否审批对象
        /// </summary>
        [JsonProperty("audit", NullValueHandling = NullValueHandling.Ignore)]
        public bool Audit { get; set; }

        /// <summary>
        /// 能否逐级审批
        /// </summary>
        [JsonProperty("level_audit", NullValueHandling = NullValueHandling.Ignore)]
        public bool LevelAudit { get; set; }

        /// <summary>
        /// 审批页面
        /// </summary>
        [JsonProperty("audit_page", NullValueHandling = NullValueHandling.Ignore)]
        public int AuditPage { get; set; }

        /// <summary>
        /// 主页面
        /// </summary>
        [JsonProperty("master_page", NullValueHandling = NullValueHandling.Ignore)]
        public int MasterPage { get; set; }

        /// <summary>
        /// 是否数据管理
        /// </summary>
        [JsonProperty("data_state", NullValueHandling = NullValueHandling.Ignore)]
        public bool DataState { get; set; }

        /// <summary>
        /// 标准编辑
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