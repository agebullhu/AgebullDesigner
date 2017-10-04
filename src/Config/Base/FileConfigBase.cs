using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Gboxt.Common.DataAccess.Schemas
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
        public string FileName
        {
            get { return _fileName; }
            set
            {
                if (Equals(_fileName, value))
                {
                    return;
                }
                BeforePropertyChanged(nameof(FileName), _fileName, value);
                _fileName = value;
                RaisePropertyChanged(() => FileName);
            }
        }
    }
}