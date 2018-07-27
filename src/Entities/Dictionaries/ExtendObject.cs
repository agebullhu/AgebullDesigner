// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// ����:bull2
// ����:CodeRefactor-Gboxt.Common.WpfMvvmBase
// ����:2014-11-29
// �޸�:2014-11-29
// *****************************************************/

#region ����

using System.Runtime.Serialization;
using Newtonsoft.Json;

#endregion

namespace Agebull.EntityModel
{
    /// <summary>
    /// ��ʾ����չ���ԵĶ���
    /// </summary>
    public interface IExtendAttribute
    {
        /// <summary>
        ///     �����ֵ�
        /// </summary>
        AttributeDictionary Attribute
        {
            get;
        }
    }
    /// <summary>
    /// ��ʾ���������ԵĶ���
    /// </summary>
    public interface IExtendDependencyObjects
    {
        /// <summary>
        ///     �����ֵ�
        /// </summary>
        DependencyObjects Dependency
        {
            get;
        }
    }
    /// <summary>
    /// ��ʾ����չ����(�������ͷ���)�Ķ���
    /// </summary>
    public interface IExtendDependencyDelegates
    {
        /// <summary>
        ///     ��չ�����ֵ�
        /// </summary>
        DependencyDelegates Delegates
        {
            get;
        }
    }
    /// <summary>
    /// ��ʾ����չ����(���Ʒ���)�Ķ���
    /// </summary>
    public interface IExtendDelegates
    {
        /// <summary>
        ///     ��չ�����ֵ�
        /// </summary>
        IFunctionDictionary Delegates
        {
            get;
        }
    }
    /// <summary>
    /// ��ʾ����չ����(���Ʒ���)�Ķ���
    /// </summary>
    public interface IExtendModelDelegates<TModel> where TModel : class
    {

        /// <summary>
        ///     ���������ֵ�
        /// </summary>
        ModelFunctionDictionary<TModel> ModelFunction
        {
            get;
        }
    }
    /// <summary>
    ///     ��չ����
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public class ExtendObject
    {
        private AttributeDictionary _attribute;

        private DependencyDelegates _dependencyFunction;

        private DependencyObjects _dependencyObject;

        /// <summary>
        ///     �����ֵ�
        /// </summary>
        [DataMember]
        public AttributeDictionary Attribute
        {
            get => _attribute ?? (_attribute = new AttributeDictionary() );
            set => _attribute = value;
        }

        /// <summary>
        ///     ���������ֵ�
        /// </summary>
        [IgnoreDataMember]
        public DependencyDelegates DependencyDelegates
        {
            get => _dependencyFunction ?? (_dependencyFunction = new DependencyDelegates() );
            set => _dependencyFunction = value;
        }

        /// <summary>
        ///     ���������ֵ�
        /// </summary>
        [IgnoreDataMember]
        public DependencyObjects DependencyObjects
        {
            get => _dependencyObject ?? (_dependencyObject = new DependencyObjects() );
            set => _dependencyObject = value;
        }
    }

    /// <summary>
    ///     ��չ����
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public sealed class ExtendObject<TModel> : ExtendObject
            where TModel : class
    {
        private ModelFunctionDictionary<TModel> _modelFunction;

        /// <summary>
        ///     ���������ֵ�
        /// </summary>
        [IgnoreDataMember]
        public ModelFunctionDictionary<TModel> ModelFunction
        {
            get => _modelFunction ?? (_modelFunction = new ModelFunctionDictionary<TModel>() );
            set => _modelFunction = value;
        }
    }
}
