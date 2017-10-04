// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// ����:bull2
// ����:CodeRefactor-Agebull.Common.WpfMvvmBase
// ����:2014-12-09
// �޸�:2014-12-09
// *****************************************************/

#region ����

using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;

#endregion

namespace Agebull.Common.DataModel
{
    /// <summary>
    ///     ��ʾ��ʾ�������
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public abstract class StatusEntityObject<TStatus> : EntityObjectBase, INetObject, IExtendAttribute, IExtendDependencyObjects,IExtendDependencyDelegates,IExtendDelegates 
            where TStatus : SerializableStatus, new()
    {
        #region ״̬����

        /// <summary>
        ///     ״̬����
        /// </summary>
        [DataMember]
        private TStatus _status;

        /// <summary>
        ///     ״̬����
        /// </summary>
        [Browsable(false), IgnoreDataMember]
        public TStatus __EntityStatus
        {
            get
            {
                return this._status ?? ( this._status = this.CreateStatus() );
            }
        }

        /// <summary>
        ///     ����״̬����
        /// </summary>
        protected virtual TStatus CreateStatus()
        {
            var status = new TStatus();
            status.Initialize(this);
            return status;
        }

        #endregion

        #region ���л�״̬

        /// <summary>
        ///     ��ʾ��·������Դ
        /// </summary>
        NetObjectSource INetObject.NetObjectSource
        {
            get
            {
                return this.__EntityStatus.ObjectSource;
            }
            set
            {
                this.__EntityStatus.ObjectSource = value;
            }
        }

        /// <summary>
        ///     ���л���
        /// </summary>
        bool INetObject.IsSerializing
        {
            get
            {
                return this.__EntityStatus.IsSerializing;
            }
            set
            {
                this.__EntityStatus.IsSerializing = value;
            }
        }

        /// <summary>
        ///     �����л���
        /// </summary>
        bool INetObject.IsDeserializing
        {
            get
            {
                return this.__EntityStatus.IsDeserializing;
            }
            set
            {
                this.__EntityStatus.IsDeserializing = value;
            }
        }

        /// <summary>
        ///     ��ʼ�����л�ʱ�Ĵ���
        /// </summary>
        [OnDeserializing]
        private void OnDeserializing(StreamingContext context)
        {
            this.__EntityStatus.IsDeserializing = true;
            this.OnDeserializing();
        }

        /// <summary>
        ///     ��ɷ����л�ʱ�Ĵ���
        /// </summary>
        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
#if SERVICE
            IsFromClient = true;
#endif
            this.__EntityStatus.IsDeserializing = false;
            this.OnDeserialized();
        }

        /// <summary>
        ///     �޸���������֤����ʱ����ȷ��
        /// </summary>
        [OnSerializing]
        protected virtual void OnSerializing(StreamingContext context)
        {
            this.__EntityStatus.IsSerializing = true;
            this.OnSerializing();
        }

        /// <summary>
        ///     ������л��Ĵ���
        /// </summary>
        [OnSerialized]
        protected virtual void OnSerialized(StreamingContext context)
        {
            this.__EntityStatus.IsSerializing = false;
            this.OnSerialized();
        }

        #endregion

        #region ���л�����

        /// <summary>
        ///     ���ڽ��з����л�
        /// </summary>
        protected virtual void OnDeserializing()
        {
        }

        /// <summary>
        ///     ��ɷ����л�
        /// </summary>
        protected virtual void OnDeserialized()
        {
        }

        /// <summary>
        ///     ���ڽ������л�
        /// </summary>
        protected virtual void OnSerializing()
        {
        }

        /// <summary>
        ///     ������л��Ĵ���
        /// </summary>
        protected virtual void OnSerialized()
        {
        }

        #endregion

        #region ��չ����

        AttributeDictionary IExtendAttribute.Attribute
        {
            get
            {
                return __EntityStatus.Attribute;
            }
        }

        DependencyObjects IExtendDependencyObjects.Dependency
        {
            get
            {
                return __EntityStatus.DependencyObjects;
            }
        }

        DependencyDelegates IExtendDependencyDelegates.Delegates
        {
            get
            {
                return __EntityStatus.DependencyDelegates;
            }
        }

        IFunctionDictionary IExtendDelegates.Delegates
        {
            get
            {
                return __EntityStatus.ModelFunction;
            }
        }
        #endregion
    }

    /// <summary>
    ///     ��ʾ��ʾ�������
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public abstract class StatusEntityObject : StatusEntityObject<SerializableStatus>
    {
    }
}
