using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config
{
    public sealed partial class EntityConfig
    {
        #region 子级
        /// <summary>
        /// 加入子级
        /// </summary>
        /// <param name="propertyConfig"></param>
        public void Add(FieldConfig propertyConfig)
        {
            if (Properties.Any(p => p.Name == propertyConfig.Name) || !Properties.TryAdd(propertyConfig))
                return;
            propertyConfig.Entity = this;
            if (WorkContext.InLoding || WorkContext.InSaving || WorkContext.InRepair)
                return;
            propertyConfig.Identity = ++MaxIdentity;
            propertyConfig.Index = Properties.Count == 0 ? 1 : Properties.Max(p => p.Index) + 1;
            MaxIdentity = Properties.Max(p => p.Identity);
        }

        /// <summary>
        /// 加入子级
        /// </summary>
        /// <param name="propertyConfig"></param>
        public void Remove(FieldConfig propertyConfig)
        {
            Properties.Remove(propertyConfig);
        }

        #endregion

    }
}