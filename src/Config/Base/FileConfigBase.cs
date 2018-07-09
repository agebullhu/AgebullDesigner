using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
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
        /// ȡ�ļ���
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