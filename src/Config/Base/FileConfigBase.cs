using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Agebull.Common;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    ///     单独保存的配置
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public abstract partial class FileConfigBase : ClassifyConfig
    {
        /// <summary>
        ///     保存地址
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        private string _fileName;


        /// <summary>
        ///     保存地址
        /// </summary>
        [IgnoreDataMember,JsonIgnore,Category("系统"), DisplayName("保存地址")]
        public string SaveFileName
        {
            get => _fileName;
            set
            {
                if (Equals(_fileName, value))
                {
                    return;
                }
                BeforePropertyChanged(nameof(SaveFileName), _fileName, value);
                _fileName = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                RaisePropertyChanged(() => SaveFileName);
            }
        }
        /// <summary>
        /// 取文件名
        /// </summary>
        /// <returns></returns>
        public string GetFileName()
        {
            if (string.IsNullOrEmpty(Name))
            {
                return Name = Guid.NewGuid().ToString("N").ToUpper() + ".json";
            }
            return Name.Trim().Replace(' ', '_').Replace('>', '_').Replace('<', '_') + ".json";
        }

    }
}