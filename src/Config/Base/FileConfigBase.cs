using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Gboxt.Common.DataAccess.Schemas
{
    /// <summary>
    ///     �������������
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public abstract partial class FileConfigBase : ClassifyConfig
    {
        /// <summary>
        ///     �����ַ
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        private string _fileName;


        /// <summary>
        ///     �����ַ
        /// </summary>
        [IgnoreDataMember,JsonIgnore,Category("ϵͳ"), DisplayName("�����ַ")]
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