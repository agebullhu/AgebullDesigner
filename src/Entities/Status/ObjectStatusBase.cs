using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Agebull.Common.DataModel
{
    /// <summary>
    /// ����״̬����
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public class ObjectStatusBase
    {
        /// <summary>
        /// ��Ӧ�Ķ���
        /// </summary>
        [IgnoreDataMember]
        public NotificationObject Object { get; private set; }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        internal void Initialize(NotificationObject obj)
        {
            this.Object = obj;
            this.InitializeInner();
        }

        /// <summary>
        /// ��ʼ����ʵ��
        /// </summary>
        protected virtual void InitializeInner()
        {

        }

        private AttributeDictionary _attribute;

        private DependencyDelegates _dependencyFunction;

        private DependencyObjects _dependencyObject;

        /// <summary>
        ///     �����ֵ�
        /// </summary>
        [DataMember]
        public AttributeDictionary Attribute
        {
            get
            {
                return this._attribute ?? (this._attribute = new AttributeDictionary());
            }
            set
            {
                this._attribute = value;
            }
        }

        /// <summary>
        ///     ���������ֵ�
        /// </summary>
        [IgnoreDataMember]
        public DependencyDelegates DependencyDelegates
        {
            get
            {
                return this._dependencyFunction ?? (this._dependencyFunction = new DependencyDelegates());
            }
            set
            {
                this._dependencyFunction = value;
            }
        }

        /// <summary>
        ///     ���������ֵ�
        /// </summary>
        [IgnoreDataMember]
        public DependencyObjects DependencyObjects
        {
            get
            {
                return this._dependencyObject ?? (this._dependencyObject = new DependencyObjects());
            }
            set
            {
                this._dependencyObject = value;
            }
        }
        private IFunctionDictionary _modelFunction;

        /// <summary>
        ///     ���������ֵ�
        /// </summary>
        [IgnoreDataMember]
        public IFunctionDictionary ModelFunction
        {
            get
            {
                return this._modelFunction ?? (this._modelFunction = new ModelFunctionDictionary<EntityObjectBase>());
            }
            set
            {
                this._modelFunction = value;
            }
        }
    }
}