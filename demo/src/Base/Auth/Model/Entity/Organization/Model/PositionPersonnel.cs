
using System;
using System.Runtime.Serialization;
using Agebull.EntityModel.Common;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Agebull.Common.OAuth
{
    /// <summary>
    /// 员工职位关联
    /// </summary>
    [DataContract, Serializable]
    sealed partial class PositionPersonnelData : EditDataObject, ITitle
    {
        /// <summary>
        ///     标题
        /// </summary>
        /// <value>int</value>
        string ITitle.Title => $"职位分配：{RealName}({Department}{Position})";

        /// <summary>
        /// 职位全称
        /// </summary>
        [JsonProperty("OrganizationPosition", NullValueHandling = NullValueHandling.Ignore)]
        public string OrganizationPosition => Department + Appellation;


        [JsonProperty("select")]
        public bool __IsSelected { get; set; }
    }
}