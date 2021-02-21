using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config.V2021
{
    /// <summary>
    /// 页面区域
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public abstract class PageItemConfig : UserInterfaceConfig, IChildrenConfig
    {
        private PageRegionConfig region;

        ConfigBase IChildrenConfig.Parent { get => Region as PageRegionConfig; set => Region = value as PageRegionConfig; }

        /// <summary>
        /// 构造
        /// </summary>
        protected PageItemConfig()
        {
            ItemType = GetType().Name;
        }

        /// <summary>
        /// 所在哪部分内容中
        /// </summary>
        public PageRegionConfig Region
        {
            get => region; set
            {
                region = value;
                OnPropertyChanged(nameof(Region));
                OnPropertyChanged("IChildrenConfig.Parent");
            }
        }

        /// <summary>
        /// 所在哪部分内容中
        /// </summary>
        public PageContentType InContent { get; set; }

        /// <summary>
        /// 所在内容中的区域
        /// </summary>
        public PageRegionType InRegion { get; set; }

        /// <summary>
        /// 宽
        /// </summary>
        public string Width { get; set; }

        /// <summary>
        /// 高
        /// </summary>
        public string Height { get; set; }

        /// <summary>
        /// 节点类型，用于反序列化
        /// </summary>
        public string ItemType { get; set; }

    }

}