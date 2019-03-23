/*design by:agebull designer date:2018/9/2 10:50:01*/
using Agebull.EntityModel.Common;
using Agebull.EntityModel.EasyUI;
using Newtonsoft.Json;
using System.Runtime.Serialization;


namespace Agebull.Common.Organizations
{
    /// <summary>
    /// 机构
    /// </summary>
    [DataContract]
    sealed partial class OrganizationData : EditDataObject, ITitle
    {
        /// <summary>
        ///     标题
        /// </summary>
        /// <value>int</value>
        string ITitle.Title => $"组织机构：{FullName}";
        /// <summary>
        ///     是否已选择
        /// </summary>
        [JsonProperty("checked")]
        public bool IsSelect { get; set; }

        /// <summary>
        ///     表格树关闭状态
        /// </summary>
        [JsonProperty("state")]
        public string TreeState => "open";

        /// <summary>
        ///     是否展开
        /// </summary>
        [JsonProperty("IsOpen")]
        public bool IsOpen { get; set; }

        //[JsonProperty("children", NullValueHandling = NullValueHandling.Ignore)]
        //public List<OrganizationData> Childrens { get; set; } = new List<OrganizationData>();


        /// <summary>
        ///     子级
        /// </summary>
        [JsonProperty("children", NullValueHandling = NullValueHandling.Ignore)]
        public OrganizationData[] Children { get; set; }

        public EasyUiTreeNode ToEasyUiTreeNode()
        {
            return new EasyUiTreeNode
            {
                ID = Id,
                Icon = Type == OrganizationType.Organization ? "icon-com" : "icon-bm",
                IsOpen = Type != OrganizationType.Organization,
                Attributes = "org",
                Text = FullName
            };
        }
    }
}