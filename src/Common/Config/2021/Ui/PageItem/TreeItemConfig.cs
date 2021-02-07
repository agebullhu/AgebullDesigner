using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config.V2021
{
    /// <summary>
    /// 页面区域
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class TreeItemConfig : PageItemConfig
    {
        /// <summary>
        /// 树使用的Api接口
        /// </summary>
        public string ApiName { get; set; }

    }

}