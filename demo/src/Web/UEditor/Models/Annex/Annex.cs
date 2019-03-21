/*design by:agebull designer date:2017/5/31 16:32:03*/
using System.Runtime.Serialization;
using Agebull.EntityModel.Common;
using Newtonsoft.Json;

namespace Agebull.ZeroNet.ManageApplication
{
    /// <summary>
    /// 附件
    /// </summary>
    [DataContract]
    sealed partial class AnnexData : EditDataObject
    {
        /// <summary>
        /// 上传的文件内容
        /// </summary>
        /// <remarks>
        /// 文件名称
        /// </remarks>
        [IgnoreDataMember, JsonIgnore]
        public byte[] Buffer { get; set; }
    }
}