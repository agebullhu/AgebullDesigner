using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
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
        /// <param name="ext"></param>
        /// <returns></returns>
        public string GetFileName(string ext)
        {
            if (string.IsNullOrEmpty(Name))
            {
                return Name = Guid.NewGuid().ToString("N").ToUpper() + ext;
            }
            return Name.Trim().Replace(' ', '_').Replace('>', '_').Replace('<', '_') + ext;
        }
    }
}