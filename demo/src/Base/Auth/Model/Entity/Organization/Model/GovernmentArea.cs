/*design by:agebull designer date:2019/3/2 23:07:34*/
#region
using Agebull.EntityModel.Common;
using Agebull.EntityModel.EasyUI;
using Newtonsoft.Json;
using System.Runtime.Serialization;

#endregion

namespace Agebull.Common.OAuth
{
    /// <summary>
    /// 行政区域
    /// </summary>
    [DataContract]
    sealed partial class GovernmentAreaData : EditDataObject, ITitle
    {
        /// <summary>
        ///     标题
        /// </summary>
        /// <value>int</value>
        string ITitle.Title => $"行政区域：{FullName}";

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
        public GovernmentAreaData[] Children { get; set; }

        /// <summary>
        /// 转为界面可用的树节点
        /// </summary>
        /// <returns></returns>
        public EasyUiTreeNode ToEasyUiTreeNode()
        {
            return new EasyUiTreeNode
            {
                ID = Id,
                Icon = "icon-area",
                IsOpen = true,
                Attributes = "area",
                Text = FullName
            };
        }

    }
}