// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Gboxt.Common.WpfMvvmBase
// 建立:2014-11-27
// 修改:2014-11-29
// *****************************************************/

#region 引用

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media.Animation;

#endregion

namespace Agebull.Common.Mvvm
{
    public interface IAttachedObject
    {
        DependencyObject AssociatedObject
        {
            get;
        }

        void Attach(DependencyObject dependencyObject);

        void Detach();
    }
    public abstract class Behavior : Animatable, IAttachedObject
    {
        private Type associatedType;

        private DependencyObject associatedObject;

        protected Type AssociatedType
        {
            get
            {
                ReadPreamble();
                return associatedType;
            }
        }

        protected DependencyObject AssociatedObject
        {
            get
            {
                ReadPreamble();
                return associatedObject;
            }
        }

        DependencyObject IAttachedObject.AssociatedObject => AssociatedObject;

        internal event EventHandler AssociatedObjectChanged;

        internal Behavior(Type associatedType)
        {
            this.associatedType = associatedType;
        }

        protected virtual void OnAttached()
        {
        }

        protected virtual void OnDetaching()
        {
        }

        protected override Freezable CreateInstanceCore()
        {
            Type type = GetType();
            return (Freezable)Activator.CreateInstance(type);
        }

        private void OnAssociatedObjectChanged()
        {
            if (this.AssociatedObjectChanged != null)
            {
                this.AssociatedObjectChanged(this, new EventArgs());
            }
        }

        public void Attach(DependencyObject dependencyObject)
        {
            if (dependencyObject != AssociatedObject)
            {
                if (AssociatedObject != null)
                {
                    throw new InvalidOperationException("AssociatedObject is not null");
                }
                if (dependencyObject != null && !AssociatedType.IsAssignableFrom(dependencyObject.GetType()))
                {
                    throw new InvalidOperationException("dependencyObject is not null");
                }
                WritePreamble();
                associatedObject = dependencyObject;
                WritePostscript();
                OnAssociatedObjectChanged();
                OnAttached();
            }
        }

        public void Detach()
        {
            OnDetaching();
            WritePreamble();
            associatedObject = null;
            WritePostscript();
            OnAssociatedObjectChanged();
        }
    }


    public abstract class Behavior<T> : Behavior where T : DependencyObject
    {
        protected new T AssociatedObject => (T)base.AssociatedObject;

        protected Behavior()
            : base(typeof(T))
        {
        }
    }
}
