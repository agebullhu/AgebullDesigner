// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// ����:bull2
// ����:CodeRefactor-Gboxt.Common.WpfMvvmBase
// ����:2014-11-29
// �޸�:2014-11-29
// *****************************************************/

#region ����

using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

#endregion

namespace Agebull.EntityModel
{
    /// <summary>
    ///     ��չ�����ֵ�
    /// </summary>
    /// <remarks>
    ///     ��չ�ֵ����ΪDataMember����,����Ҫ�������л�ʱ,Ӧ�ñ�֤�ֵ��Value��������֪�������������л�,������WCF����ʱ�����
    /// </remarks>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public sealed class AttributeDictionary
    {
        [DataMember]
        private Dictionary<string, object> _dictionary;

        /// <summary>
        ///     ��չ�����ֵ�
        /// </summary>
        public Dictionary<string, object> Dictionary
        {
            get => _dictionary ?? (_dictionary = new Dictionary<string, object>() );
            set => _dictionary = value;
        }

        /// <summary>
        ///     ȡ�û�������չ����
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object this[string name]
        {
            get
            {
                if (string.IsNullOrEmpty(name))
                {
                    return null;
                }
                return Dictionary.TryGetValue(name, out object value) ? value : null;
            }
            set
            {
                if (Dictionary.ContainsKey(name))
                {
                    if (value == null)
                    {
                        Dictionary.Remove(name);
                    }
                    else
                    {
                        Dictionary[name] = value;
                    }
                }
                else if (value != null)
                {
                    Dictionary.Add(name, value);
                }
            }
        }

        /// <summary>
        ///     ȡ�û�������չ����
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public T Get<T>(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return default( T );
            }
            return Dictionary.TryGetValue(name, out object value) ? (T)value : default(T);
        }

        /// <summary>
        ///     ȡ�û�������չ����
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns>������óɹ�����True</returns>
        public bool Set<T>(string name, T value)
        {
            if (Dictionary.TryGetValue(name, out object value1))
            {
                if (Equals(value, default(T)))
                {
                    Dictionary.Remove(name);
                    return true;
                }
                if (Equals(value1, value))
                {
                    return false;
                }
                Dictionary[name] = value;
                return true;
            }
            if (Equals(value, default( T )))
            {
                return false;
            }
            Dictionary.Add(name, value);
            return true;
        }
    }
}
