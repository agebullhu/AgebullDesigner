using Agebull.EntityModel.Common;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Agebull.Common.Organizations
{
    /// <summary>
    /// 职位组织关联
    /// </summary>
    [DataContract]
    sealed partial class OrganizePositionData : EditDataObject, ITitle
    {
        /// <summary>
        ///     标题
        /// </summary>
        /// <value>int</value>
        string ITitle.Title => $"职位设置：{Position}";
    }
}