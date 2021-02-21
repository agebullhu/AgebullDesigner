using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config.V2021
{
    /// <summary>
    /// 页面内容类型
    /// </summary>
    public enum PageContentType
    {
        None,
        Main,
        Details
    }

    /// <summary>
    /// 页面内容
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class PageContentConfig : UserInterfaceConfig, IChildrenConfig
    {
        private PageConfig page;

        ConfigBase IChildrenConfig.Parent { get => Page as ConfigBase; set => Page = value as PageConfig; }

        public PageConfig Page
        {
            get => page; set
            {
                page = value;
                OnPropertyChanged(nameof(Page));
                OnPropertyChanged("IChildrenConfig.Parent");
            }
        }

        /// <summary>
        /// 页面内容类型
        /// </summary>
        public PageContentType ContentType { get; set; }

        /// <summary>
        /// 包含的区域
        /// </summary>
        public NotificationList<PageRegionConfig> Regions { get; set; }
    }
}