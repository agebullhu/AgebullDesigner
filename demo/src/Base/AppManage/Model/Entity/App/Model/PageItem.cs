using System.Runtime.Serialization;
using Gboxt.Common.DataModel;
using Newtonsoft.Json;

namespace Agebull.Common.AppManage
{
    /// <summary>
    /// 页面节点
    /// </summary>
    [DataContract]
    sealed partial class PageItemData : EditDataObject, IPageItem
    {

        /// <summary>
        /// 页面的扩展配置
        /// </summary>
        [IgnoreDataMember, JsonIgnore] private PageConfig _config;

        /// <summary>
        /// 页面的扩展配置
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public PageConfig Config
        {
            get
            {
                if (_config != null)
                    return _config;
                if (string.IsNullOrWhiteSpace(Json))
                    return _config = new PageConfig();
                try
                {
                    _config = JsonConvert.DeserializeObject<PageConfig>(Json);
                }
                catch
                {
                    _config = new PageConfig();
                }
                return _config ?? (_config = new PageConfig());
            }
            set => Json = JsonConvert.SerializeObject(_config = value ?? new PageConfig());
        }


        /// <summary>
        /// 系统内部的类名
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string SystemType
        {
            get => Config.SystemType;
            set => Config.SystemType = value;
        }

        public string Path
        {
            get;
            set;
        }

        /// <summary>
        /// 是否显示
        /// </summary>
        [JsonProperty("hide", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsHide
        {
            get => Config.Hide;
            set => Config.Hide = value;
        }
        /// <summary>
        /// 是否审批对象
        /// </summary>
        [JsonProperty("audit", NullValueHandling = NullValueHandling.Ignore)]
        public bool Audit
        {
            get => Config.Audit;
            set => Config.Audit = value;
        }
        /// <summary>
        /// 审批页面
        /// </summary>
        [JsonProperty("audit_page", NullValueHandling = NullValueHandling.Ignore)]
        public int AuditPage
        {
            get => Config.AuditPage;
            set => Config.AuditPage = value;
        }
        /// <summary>
        /// 能否逐级审批
        /// </summary>
        [JsonProperty("level_audit", NullValueHandling = NullValueHandling.Ignore)]
        public bool LevelAudit
        {
            get => Config.LevelAudit;
            set => Config.LevelAudit = value;
        }
        /// <summary>
        /// 是否数据管理
        /// </summary>
        [JsonProperty("data_state", NullValueHandling = NullValueHandling.Ignore)]
        public bool DataState
        {
            get => Config.DataState;
            set => Config.DataState = value;
        }
        /// <summary>
        /// 标准编辑
        /// </summary>
        [JsonProperty("edit", NullValueHandling = NullValueHandling.Ignore)]
        public bool Edit
        {
            get => Config.Edit;
            set => Config.Edit = value;
        }

        /// <summary>
        /// 主页面
        /// </summary>
        [JsonProperty("master_page", NullValueHandling = NullValueHandling.Ignore)]
        public int MasterPage
        {
            get => Config.MasterPage;
            set => Config.MasterPage = value;
        }
        public bool IsPublic { get; set; }
    }
}