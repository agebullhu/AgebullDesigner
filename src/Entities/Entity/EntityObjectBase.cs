using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Agebull.EntityModel
{
    /// <summary>
    ///     ʵ�����
    /// </summary>
    [DataContract, KnownType("GetKnownTypes")]
    public abstract class EntityObjectBase : NotificationObject, IEntityObject
    {
        #region ���л�����֧��

        /// <summary>
        ///     �Ǽ���֪����
        /// </summary>
        private static readonly List<Type> typeList = new List<Type>();

        /// <summary>
        ///     �Ǽ���֪����
        /// </summary>
        private static Type[] _types;

        /// <summary>
        ///     ��ע����������(��ֹƵ����ToArray�����ڴ�)
        /// </summary>
        private static bool _haseNew;

        /// <summary>
        ///     ȡ����������������
        /// </summary>
        /// <returns> </returns>
        public static Type[] KnownTypes => _haseNew ? ( _types = typeList.ToArray() ) : _types;

        /// <summary>
        ///     ����̳����͵���֪�����Ա�����ȷ���л�
        /// </summary>
        /// <param name="type"> </param>
        public static void RegisteSupperType(Type type)
        {
            if (typeList.Contains(type))
            {
                return;
            }
            typeList.Add(type);
            _haseNew = true;
        }

        /// <summary>
        ///     ����̳����͵���֪�����Ա�����ȷ���л�
        /// </summary>
        public static void RegisteSupperType<T>()
        {
            RegisteSupperType(typeof (T));
        }

        /// <summary>
        ///     ȡ����������������
        /// </summary>
        /// <returns> </returns>
        public static Type[] GetKnownTypes()
        {
            return KnownTypes;
        }

        #endregion

        #region ʵ�����֧��

        /// <summary>
        /// ����ֵ
        /// </summary>
        /// <param name="source">���Ƶ�Դ�ֶ�</param>
        public void CopyValue(IEntityObject source)
        {
            var entity = source as EntityObjectBase;
            if(entity != null)
                CopyValueInner(entity);
        }

        /// <summary>
        ///     ��������ֵ
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        public void SetValue(string property, object value)
        {
            SetValueInner(property, value);
        }

        /// <summary>
        ///     ��������ֵ
        /// </summary>
        /// <param name="property"></param>
        public object GetValue(string property)
        {
            return GetValueInner(property);
        }

        /// <summary>
        ///     ��������ֵ
        /// </summary>
        /// <param name="property"></param>
        public TValue GetValue<TValue>(string property)
        {
            return GetValueInner<TValue>(property);
        }
        #endregion

        #region �ڲ�ʵ��

        /// <summary>
        /// ����ֵ
        /// </summary>
        /// <param name="source">���Ƶ�Դ�ֶ�</param>
        protected abstract void CopyValueInner(EntityObjectBase source);

        /// <summary>
        ///     ��������ֵ
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        protected abstract void SetValueInner(string property, object value);

        /// <summary>
        ///     ��������ֵ
        /// </summary>
        /// <param name="property"></param>
        protected abstract object GetValueInner(string property);

        /// <summary>
        ///     ��������ֵ
        /// </summary>
        /// <param name="property"></param>
        protected virtual TValue GetValueInner<TValue>(string property)
        {
            return (TValue)GetValue(property);
        }
        #endregion

    }
}