using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace Agebull.EntityModel
{
    /// <summary>提供泛型集合的基类。</summary>
    /// <typeparam name="T">集合中的元素类型。</typeparam>
    [Serializable]
    public class MyCollection<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection, IReadOnlyList<T>, IReadOnlyCollection<T>
    {
        private IList<T> items;
        [NonSerialized]
        private object _syncRoot;

        /// <summary>
        ///   初始化为空的 <see cref="T:System.Collections.ObjectModel.MyCollection`1" /> 类的新实例。
        /// </summary>
        
        public MyCollection()
        {
            this.items = (IList<T>) new List<T>();
        }

        /// <summary>
        ///   新实例初始化 <see cref="T:System.Collections.ObjectModel.MyCollection`1" /> 包装指定列表的类。
        /// </summary>
        /// <param name="list">用于包装由新的集合的列表。</param>
        /// <exception cref="T:System.ArgumentNullException">
        ///   <paramref name="list" /> 为 <see langword="null" />。
        /// </exception>
        
        public MyCollection(IList<T> list)
        {
            if (list == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.list);
            this.items = list;
        }

        /// <summary>
        ///   获取中实际包含的元素数目 <see cref="T:System.Collections.ObjectModel.MyCollection`1" />。
        /// </summary>
        /// <returns>
        ///   中实际包含的元素数目 <see cref="T:System.Collections.ObjectModel.MyCollection`1" />。
        /// </returns>
        
        public int Count
        {
             get
            {
                return this.items.Count;
            }
        }

        /// <summary>
        ///   获取 <see cref="T:System.Collections.Generic.IList`1" /> 包装 <see cref="T:System.Collections.ObjectModel.MyCollection`1" />。
        /// </summary>
        /// <returns>
        ///   一个 <see cref="T:System.Collections.Generic.IList`1" /> 包装 <see cref="T:System.Collections.ObjectModel.MyCollection`1" />。
        /// </returns>
        
        protected IList<T> Items
        {
             get
            {
                return this.items;
            }
        }

        /// <summary>获取或设置指定索引处的元素。</summary>
        /// <param name="index">要获取或设置的元素的从零开始的索引。</param>
        /// <returns>指定索引处的元素。</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///   <paramref name="index" /> 小于零。
        /// 
        ///   - 或 -
        /// 
        ///   <paramref name="index" /> 等于或大于 <see cref="P:System.Collections.ObjectModel.MyCollection`1.Count" />。
        /// </exception>
        
        public T this[int index]
        {
             get
            {
                return this.items[index];
            }
             set
            {
                if (this.items.IsReadOnly)
                    ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
                if (index < 0 || index >= this.items.Count)
                    ThrowHelper.ThrowArgumentOutOfRangeException();
                this.SetItem(index, value);
            }
        }

        /// <summary>
        ///   将对象添加到 <see cref="T:System.Collections.ObjectModel.MyCollection`1" /> 的结尾处。
        /// </summary>
        /// <param name="item">
        ///   要添加到 <see cref="T:System.Collections.ObjectModel.MyCollection`1" /> 末尾的对象。
        ///    对于引用类型，该值可以为 <see langword="null" />。
        /// </param>
        
        public void Add(T item)
        {
            if (this.items.IsReadOnly)
                ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
            this.InsertItem(this.items.Count, item);
        }

        /// <summary>
        ///   从 <see cref="T:System.Collections.ObjectModel.MyCollection`1" /> 中移除所有元素。
        /// </summary>
        
        public void Clear()
        {
            if (this.items.IsReadOnly)
                ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
            this.ClearItems();
        }

        /// <summary>
        ///   从目标数组的指定索引处开始将整个 <see cref="T:System.Collections.ObjectModel.MyCollection`1" /> 复制到兼容的一维 <see cref="T:System.Array" />。
        /// </summary>
        /// <param name="array">
        ///   一维 <see cref="T:System.Array" />，它是从 <see cref="T:System.Collections.ObjectModel.MyCollection`1" /> 复制的元素的目标。
        ///   <see cref="T:System.Array" /> 必须具有从零开始的索引。
        /// </param>
        /// <param name="index">
        ///   <paramref name="array" /> 中从零开始的索引，从此处开始复制。
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///   <paramref name="array" /> 为 <see langword="null" />。
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///   <paramref name="index" /> 小于零。
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///   源中的元素数目 <see cref="T:System.Collections.ObjectModel.MyCollection`1" /> 大于从的可用空间 <paramref name="index" /> 目标从头到尾 <paramref name="array" />。
        /// </exception>
        
        public void CopyTo(T[] array, int index)
        {
            this.items.CopyTo(array, index);
        }

        /// <summary>
        ///   确定某元素是否在 <see cref="T:System.Collections.ObjectModel.MyCollection`1" /> 中。
        /// </summary>
        /// <param name="item">
        ///   要在 <see cref="T:System.Collections.ObjectModel.MyCollection`1" /> 中定位的对象。
        ///    对于引用类型，该值可以为 <see langword="null" />。
        /// </param>
        /// <returns>
        ///   如果在 <see langword="true" /> 中找到 <paramref name="item" />，则为 <see cref="T:System.Collections.ObjectModel.MyCollection`1" />；否则为 <see langword="false" />。
        /// </returns>
        
        public bool Contains(T item)
        {
            return this.items.Contains(item);
        }

        /// <summary>
        ///   返回循环访问 <see cref="T:System.Collections.ObjectModel.MyCollection`1" /> 的枚举数。
        /// </summary>
        /// <returns>
        ///   用于 <see cref="T:System.Collections.Generic.IEnumerator`1" /> 的 <see cref="T:System.Collections.ObjectModel.MyCollection`1" />。
        /// </returns>
        
        public IEnumerator<T> GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        /// <summary>
        ///   搜索指定的对象，并返回整个 <see cref="T:System.Collections.ObjectModel.MyCollection`1" /> 中第一个匹配项的从零开始的索引。
        /// </summary>
        /// <param name="item">
        ///   要在 <see cref="T:System.Collections.Generic.List`1" /> 中定位的对象。
        ///    对于引用类型，该值可以为 <see langword="null" />。
        /// </param>
        /// <returns>
        ///   如果找到，则为整个 <paramref name="item" /> 中 <see cref="T:System.Collections.ObjectModel.MyCollection`1" /> 第一个匹配项的从零开始的索引；否则为 -1。
        /// </returns>
        
        public int IndexOf(T item)
        {
            return this.items.IndexOf(item);
        }

        /// <summary>
        ///   将元素插入 <see cref="T:System.Collections.ObjectModel.MyCollection`1" /> 的指定索引处。
        /// </summary>
        /// <param name="index">
        ///   应插入 <paramref name="item" /> 的从零开始的索引。
        /// </param>
        /// <param name="item">
        ///   要插入的对象。
        ///    对于引用类型，该值可以为 <see langword="null" />。
        /// </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///   <paramref name="index" /> 小于零。
        /// 
        ///   - 或 -
        /// 
        ///   <paramref name="index" /> 大于 <see cref="P:System.Collections.ObjectModel.MyCollection`1.Count" />。
        /// </exception>
        
        public void Insert(int index, T item)
        {
            if (this.items.IsReadOnly)
                ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
            if (index < 0 || index > this.items.Count)
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_ListInsert);
            this.InsertItem(index, item);
        }

        /// <summary>
        ///   从 <see cref="T:System.Collections.ObjectModel.MyCollection`1" /> 中移除特定对象的第一个匹配项。
        /// </summary>
        /// <param name="item">
        ///   要从 <see cref="T:System.Collections.ObjectModel.MyCollection`1" /> 中删除的对象。
        ///    对于引用类型，该值可以为 <see langword="null" />。
        /// </param>
        /// <returns>
        ///   如果成功移除了 <paramref name="item" />，则为 <see langword="true" />；否则为 <see langword="false" />。
        ///     如果在原始 <see langword="false" /> 中没有找到 <paramref name="item" />，则此方法也会返回 <see cref="T:System.Collections.ObjectModel.MyCollection`1" />。
        /// </returns>
        
        public bool Remove(T item)
        {
            if (this.items.IsReadOnly)
                ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
            int index = this.items.IndexOf(item);
            if (index < 0)
                return false;
            this.RemoveItem(index);
            return true;
        }

        /// <summary>
        ///   移除 <see cref="T:System.Collections.ObjectModel.MyCollection`1" /> 的指定索引处的元素。
        /// </summary>
        /// <param name="index">要移除的元素的从零开始的索引。</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///   <paramref name="index" /> 小于零。
        /// 
        ///   - 或 -
        /// 
        ///   <paramref name="index" /> 等于或大于 <see cref="P:System.Collections.ObjectModel.MyCollection`1.Count" />。
        /// </exception>
        
        public void RemoveAt(int index)
        {
            if (this.items.IsReadOnly)
                ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
            if (index < 0 || index >= this.items.Count)
                ThrowHelper.ThrowArgumentOutOfRangeException();
            this.RemoveItem(index);
        }

        /// <summary>
        ///   从 <see cref="T:System.Collections.ObjectModel.MyCollection`1" /> 中移除所有元素。
        /// </summary>
        
        protected virtual void ClearItems()
        {
            this.items.Clear();
        }

        /// <summary>
        ///   将元素插入 <see cref="T:System.Collections.ObjectModel.MyCollection`1" /> 的指定索引处。
        /// </summary>
        /// <param name="index">
        ///   应插入 <paramref name="item" /> 的从零开始的索引。
        /// </param>
        /// <param name="item">
        ///   要插入的对象。
        ///    对于引用类型，该值可以为 <see langword="null" />。
        /// </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///   <paramref name="index" /> 小于零。
        /// 
        ///   - 或 -
        /// 
        ///   <paramref name="index" /> 大于 <see cref="P:System.Collections.ObjectModel.MyCollection`1.Count" />。
        /// </exception>
        
        protected virtual void InsertItem(int index, T item)
        {
            this.items.Insert(index, item);
        }

        /// <summary>
        ///   移除 <see cref="T:System.Collections.ObjectModel.MyCollection`1" /> 的指定索引处的元素。
        /// </summary>
        /// <param name="index">要移除的元素的从零开始的索引。</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///   <paramref name="index" /> 小于零。
        /// 
        ///   - 或 -
        /// 
        ///   <paramref name="index" /> 等于或大于 <see cref="P:System.Collections.ObjectModel.MyCollection`1.Count" />。
        /// </exception>
        
        protected virtual void RemoveItem(int index)
        {
            this.items.RemoveAt(index);
        }

        /// <summary>替换指定索引处的元素。</summary>
        /// <param name="index">待替换元素的从零开始的索引。</param>
        /// <param name="item">
        ///   位于指定索引处的元素的新值。
        ///    对于引用类型，该值可以为 <see langword="null" />。
        /// </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///   <paramref name="index" /> 小于零。
        /// 
        ///   - 或 -
        /// 
        ///   <paramref name="index" /> 大于 <see cref="P:System.Collections.ObjectModel.MyCollection`1.Count" />。
        /// </exception>
        
        protected virtual void SetItem(int index, T item)
        {
            this.items[index] = item;
        }

        
        bool ICollection<T>.IsReadOnly
        {
             get
            {
                return this.items.IsReadOnly;
            }
        }

        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        
        bool ICollection.IsSynchronized
        {
             get
            {
                return false;
            }
        }

        
        object ICollection.SyncRoot
        {
             get
            {
                if (this._syncRoot == null)
                {
                    ICollection items = this.items as ICollection;
                    if (items != null)
                        this._syncRoot = items.SyncRoot;
                    else
                        Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), (object) null);
                }
                return this._syncRoot;
            }
        }

        
        void ICollection.CopyTo(Array array, int index)
        {
            if (array == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
            if (array.Rank != 1)
                ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
            if (array.GetLowerBound(0) != 0)
                ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
            if (index < 0)
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
            if (array.Length - index < this.Count)
                ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
            T[] array1 = array as T[];
            if (array1 != null)
            {
                this.items.CopyTo(array1, index);
            }
            else
            {
                Type elementType = array.GetType().GetElementType();
                Type c = typeof (T);
                if (!elementType.IsAssignableFrom(c) && !c.IsAssignableFrom(elementType))
                    ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
                object[] objArray = array as object[];
                if (objArray == null)
                    ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
                int count = this.items.Count;
                try
                {
                    for (int index1 = 0; index1 < count; ++index1)
                        objArray[index++] = (object) this.items[index1];
                }
                catch (ArrayTypeMismatchException ex)
                {
                    ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
                }
            }
        }

        
        object IList.this[int index]
        {
             get
            {
                return (object) this.items[index];
            }
             set
            {
                ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, ExceptionArgument.value);
                try
                {
                    this[index] = (T) value;
                }
                catch (InvalidCastException ex)
                {
                    ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof (T));
                }
            }
        }

        
        bool IList.IsReadOnly
        {
             get
            {
                return this.items.IsReadOnly;
            }
        }

        
        bool IList.IsFixedSize
        {
             get
            {
                IList items = this.items as IList;
                if (items != null)
                    return items.IsFixedSize;
                return this.items.IsReadOnly;
            }
        }

        
        int IList.Add(object value)
        {
            if (this.items.IsReadOnly)
                ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
            ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, ExceptionArgument.value);
            try
            {
                this.Add((T) value);
            }
            catch (InvalidCastException ex)
            {
                ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof (T));
            }
            return this.Count - 1;
        }

        
        bool IList.Contains(object value)
        {
            if (MyCollection<T>.IsCompatibleObject(value))
                return this.Contains((T) value);
            return false;
        }

        
        int IList.IndexOf(object value)
        {
            if (MyCollection<T>.IsCompatibleObject(value))
                return this.IndexOf((T) value);
            return -1;
        }

        
        void IList.Insert(int index, object value)
        {
            if (this.items.IsReadOnly)
                ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
            ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, ExceptionArgument.value);
            try
            {
                this.Insert(index, (T) value);
            }
            catch (InvalidCastException ex)
            {
                ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof (T));
            }
        }

        
        void IList.Remove(object value)
        {
            if (this.items.IsReadOnly)
                ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
            if (!MyCollection<T>.IsCompatibleObject(value))
                return;
            this.Remove((T) value);
        }

        private static bool IsCompatibleObject(object value)
        {
            if (value is T)
                return true;
            if (value == null)
                return (object) default (T) == null;
            return false;
        }
    }
}