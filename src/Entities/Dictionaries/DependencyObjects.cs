// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// ����:bull2
// ����:CodeRefactor-Gboxt.Common.WpfMvvmBase
// ����:2014-11-29
// �޸�:2014-11-29
// *****************************************************/

#region ����

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

#endregion

namespace Agebull.EntityModel
{
    /// <summary>
    ///     ���������ֵ�
    /// </summary>
    /// <remarks>
    ///     ��������ΪIgnoreDataMember����,�������������л�
    /// </remarks>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public sealed class DependencyObjects
    {
        [DataMember]
        private readonly Dictionary<Type, object> _dictionary = new Dictionary<Type, object>();

        /// <summary>
        ///     ����һ�����Ͷ���
        /// </summary>
        /// <remarks>
        ///     ���ַ���ֻ����һ��,����θ���,ֻ�����һ������
        /// </remarks>
        public void Annex<T>(T value)
        {
            Type type = typeof (T);
            if (_dictionary.ContainsKey(type))
            {
                if (Equals(value, default( T )))
                {
                    _dictionary.Remove(type);
                }
                else
                {
                    _dictionary[type] = value;
                }
            }
            else if (!Equals(value, default( T )))
            {
                _dictionary.Add(type, value);
            }
        }

        /// <summary>
        ///     ȡ��һ�����͵���չ����(�����Զ��������ǰ����)
        /// </summary>
        /// <returns></returns>
        public T AutoDependency<T>() where T : class, new()
        {
            if (_dictionary.TryGetValue(typeof(T), out object value1))
            {
                return value1 as T;
            }
            T value = new T();
            _dictionary.Add(typeof (T), value);
            return value;
        }
        /// <summary>
        ///     ȡ��һ�����͵���չ����(��Ҫ����)
        /// </summary>
        /// <returns></returns>
        public T Dependency<T>() where T : class
        {
            return _dictionary.TryGetValue(typeof(T), out object value1) ? value1 as T : null;
        }
    }
}
