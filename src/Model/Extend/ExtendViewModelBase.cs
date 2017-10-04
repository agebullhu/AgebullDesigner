using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using Agebull.Common.Mvvm;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// ��չViewModel����
    /// </summary>
    public abstract class ExtendViewModelBase : ViewModelBase
    {
        #region ��������

        /// <summary>
        ///     ����
        /// </summary>
        public string Catalog { get; set; }


        /// <summary>
        /// ����ģ��
        /// </summary>
        public DataModelDesignModel BaseModel { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        public DesignContext Context => BaseModel?.Context;
        
        #endregion

        #region �����

        /// <summary>
        /// �����ע�붯��
        /// </summary>
        public DependencyAction ContentBehavior => new DependencyAction
        {
            AttachAction = obj =>
            {
                var border = (Border)obj;
                var body = Body;
                OnBodyCreating(body);
                border.Child = Body;
            }
        };
        /// <summary>
        /// �����
        /// </summary>
        public abstract FrameworkElement Body { get; }

        /// <summary>
        /// ����幹�����
        /// </summary>
        /// <param name="body"></param>
        protected abstract void OnBodyCreating(FrameworkElement body);


        /// <summary>
        ///     ģ��
        /// </summary>
        public abstract DesignModelBase DesignModel
        {
            get;
        }

        #endregion
    }

    /// <summary>
    /// ��չViewModel����
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract class ExtendViewModelBase<TModel> : ExtendViewModelBase
    where TModel : DesignModelBase, new()
    {
        /// <summary>
        /// ����
        /// </summary>
        protected ExtendViewModelBase()
        {
            Model = new TModel();
            ModelFunction = new ModelFunctionDictionary<TModel>
            {
                Model = Model
            };
        }

        /// <summary>
        /// ����幹�����
        /// </summary>
        /// <param name="body"></param>
        protected override void OnBodyCreating(FrameworkElement body)
        {
            body.DataContext = Model;
        }

        /// <summary>
        ///     ģ��
        /// </summary>
        public TModel Model
        {
            get;
        }

        /// <summary>
        ///     ģ��
        /// </summary>
        public sealed override DesignModelBase DesignModel => Model;


        /// <summary>
        ///     ���������ֵ�
        /// </summary>
        [IgnoreDataMember]
        public ModelFunctionDictionary<TModel> ModelFunction { get; }
        
    }
}