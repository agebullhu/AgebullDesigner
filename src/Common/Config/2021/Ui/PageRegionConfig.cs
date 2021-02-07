using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config.V2021
{
    /// <summary>
    /// 页面节点组合类型
    /// </summary>
    public enum ItemAssembleType
    {
        Flow,
        Tab
    }

    /// <summary>
    /// 页面内容类型
    /// </summary>
    public enum PageRegionType
    {
        Top,
        Left,
        Right,
        Bottom,
        Center,
        Dialog
    }

    /// <summary>
    /// 页面区域
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class PageRegionConfig : UserInterfaceConfig, IChildrenConfig
    {
        private PageContentConfig content;

        ConfigBase IChildrenConfig.Parent { get => Content as ConfigBase; set => Content = value as PageContentConfig; }

        /// <summary>
        /// 所在哪部分内容中
        /// </summary>
        public PageContentConfig Content
        {
            get => content; set
            {
                content = value;
                OnPropertyChanged(nameof(Content));
                OnPropertyChanged("IChildrenConfig.Parent");
            }
        }

        /// <summary>
        /// 页面区域类型
        /// </summary>
        public PageRegionType RegionType { get; set; }

        /// <summary>
        /// 页面节点组合类型
        /// </summary>
        public ItemAssembleType AssembleType { get; set; }

        /// <summary>
        /// 宽
        /// </summary>
        public string Width { get; set; }

        /// <summary>
        /// 高
        /// </summary>
        public string Height { get; set; }

        /// <summary>
        /// 包含的节点
        /// </summary>
        public NotificationList<PageItemConfig> Items { get; set; }

    }

}