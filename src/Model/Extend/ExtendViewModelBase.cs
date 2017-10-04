using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using Agebull.Common.Mvvm;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 扩展ViewModel基类
    /// </summary>
    public abstract class ExtendViewModelBase : ViewModelBase
    {
        #region 基本设置

        /// <summary>
        ///     分类
        /// </summary>
        public string Catalog { get; set; }


        /// <summary>
        /// 基本模型
        /// </summary>
        public DataModelDesignModel BaseModel { get; set; }

        /// <summary>
        /// 上下文
        /// </summary>
        public DesignContext Context => BaseModel?.Context;
        
        #endregion

        #region 主面板

        /// <summary>
        /// 主面板注入动作
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
        /// 主面板
        /// </summary>
        public abstract FrameworkElement Body { get; }

        /// <summary>
        /// 主面板构造完成
        /// </summary>
        /// <param name="body"></param>
        protected abstract void OnBodyCreating(FrameworkElement body);


        /// <summary>
        ///     模型
        /// </summary>
        public abstract DesignModelBase DesignModel
        {
            get;
        }

        #endregion
    }

    /// <summary>
    /// 扩展ViewModel基类
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract class ExtendViewModelBase<TModel> : ExtendViewModelBase
    where TModel : DesignModelBase, new()
    {
        /// <summary>
        /// 构造
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
        /// 主面板构造完成
        /// </summary>
        /// <param name="body"></param>
        protected override void OnBodyCreating(FrameworkElement body)
        {
            body.DataContext = Model;
        }

        /// <summary>
        ///     模型
        /// </summary>
        public TModel Model
        {
            get;
        }

        /// <summary>
        ///     模型
        /// </summary>
        public sealed override DesignModelBase DesignModel => Model;


        /// <summary>
        ///     依赖对象字典
        /// </summary>
        [IgnoreDataMember]
        public ModelFunctionDictionary<TModel> ModelFunction { get; }
        
    }
}