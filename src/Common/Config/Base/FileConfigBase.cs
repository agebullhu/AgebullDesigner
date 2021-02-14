using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    ///     �������������
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public abstract partial class FileConfigBase : ConfigBase
    {
        /// <summary>
        ///     �����ַ
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        private string _fileName;


        /// <summary>
        ///     �����ַ
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Category("ϵͳ"), DisplayName("�����ַ")]
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
        /// <returns></returns>
        public virtual string GetFileName()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                return Name = Guid.NewGuid().ToString("N").ToUpper() + ".json";
            }
            return Name.Trim().Replace(' ', '_').Replace('>', '_').Replace('<', '_') + ".json";
        }
    }
}