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
    ///     ��չ�����ֵ�
    /// </summary>
    /// <remarks>
    ///     ��������ΪIgnoreDataMember����,�������������л�
    /// </remarks>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public abstract class FunctionDictionaryBase
    {
        /// <summary>
        /// �����Ƶķ����ֵ�
        /// </summary>
        [IgnoreDataMember]
        private readonly Dictionary<string, object> _nameDictionary = new Dictionary<string, object>();

        /// <summary>
        /// �����ֵ�
        /// </summary>
        [IgnoreDataMember]
        private readonly Dictionary<Type, object> _dependencyDictionary = new Dictionary<Type, object>();

        /// <summary>
        ///     ���ӷ���
        /// </summary>
        public void AnnexDelegate<TAction>(TAction action, string name = null) where TAction : class
        {
            if (string.IsNullOrEmpty(name))
            {
                var type = typeof(TAction);
                if (_dependencyDictionary.ContainsKey(type))
                {
                    if (Equals(action, null))
                    {
                        _dependencyDictionary.Remove(type);
                    }
                    else
                    {
                        _dependencyDictionary[type] = action;
                    }
                }
                else if (!Equals(action, null))
                {
                    _dependencyDictionary.Add(type, action);
                }
            }
            else if (_nameDictionary.ContainsKey(name))
            {
                if (Equals(action, null))
                {
                    _nameDictionary.Remove(name);
                }
                else
                {
                    _nameDictionary[name] = action;
                }
            }
            else if (!Equals(action, null))
            {
                _nameDictionary.Add(name, action);
            }
        }


        /// <summary>
        ///     ȡ���ڲ����Ӷ���
        /// </summary>
        /// <typeparam name="TAction">��������(�������޶�ΪAction��Func���������͵�ί��)</typeparam>
        /// <param name="name">��������</param>
        /// <returns>��������򷵻ط���,���򷵻ؿ�</returns>
        public TAction GetDelegate<TAction>(string name = null)where TAction : class
        {
            object value;
            if (string.IsNullOrEmpty(name))
            {
                var type = typeof(TAction);
                if (!_dependencyDictionary.TryGetValue(type, out value))
                {
                    return null;
                }
            }
            else
            {
                if (!_nameDictionary.TryGetValue(name, out value))
                {
                    return null;
                }
            }
            if (!(value is TAction))
            {
                return null;
            }
            return (TAction)value;
        }

        /// <summary>
        ///     �Ƿ��Ѹ��Ӷ���
        /// </summary>
        /// <typeparam name="TAction">��������(�������޶�ΪAction��Func���������͵�ί��)</typeparam>
        /// <param name="name">��������</param>
        /// <returns>��������򷵻ط���,���򷵻ؿ�</returns>
        public bool HaseDelegate<TAction>(string name = null) where TAction : class
        {
            return name == null
                ? _dependencyDictionary.ContainsKey( typeof(TAction)) 
                : _nameDictionary.ContainsKey(name);
        }
    }
}
