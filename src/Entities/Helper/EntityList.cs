using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Agebull.Common.DataModel
{
    /// <summary>
    ///     业务实体对象列表
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public class EntityList<TEntity> : NotificationObject, IList, IList<TEntity>, INotifyCollectionChanged, IAtomLock
        where TEntity : NotificationObject
    {
        #region 基础数据


        #region 集合读取

        /// <summary>
        ///     取设下标对象
        /// </summary>
        /// <param name="idx"> </param>
        /// <returns> </returns>
        public TEntity this[int idx]
        {
            get { return this.This2(idx); }
            set { this.Insert(idx, value); }
        }

        /// <summary>
        ///     是否包含
        /// </summary>
        /// <param name="value"> </param>
        /// <returns> </returns>
        public TEntity Find(TEntity value)
        {
            return this.Entities.FirstOrDefault(p => p == value);
        }

        /// <summary>
        ///     所在位置
        /// </summary>
        /// <param name="value"> </param>
        /// <returns> </returns>
        public int IndexOf(TEntity value)
        {
            return this.IndexOf2(value);
        }

        /// <summary>
        ///     是否包含
        /// </summary>
        /// <param name="value"> </param>
        /// <returns> </returns>
        public bool Contains(TEntity value)
        {
            return this.Entities.Contains(value);
        }

        /// <summary>
        ///     是否包含
        /// </summary>
        /// <param name="value"> </param>
        /// <returns> </returns>
        public void SyncOrInsert(TEntity value)
        {
            this.SyncByUpdated(value);
        }

        /// <summary>
        ///     总行数
        /// </summary>
        public int Count => this.Entities.Count;

        /// <summary>
        ///     得到枚举器
        /// </summary>
        public IEnumerator<TEntity> GetEnumerator()
        {
            return this.Entities.GetEnumerator();
        }

        #endregion

        #region 加入


        /// <summary>
        ///     加入一批
        /// </summary>
        /// <param name="entities"> </param>
        public void AddRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                return;
            }
            foreach (TEntity entity in entities)
            {
                this.Add(entity);
            }
        }

        /// <summary>
        ///     加入或是同步
        /// </summary>
        /// <param name="entity"> </param>
        public void AddOrSwitch(TEntity entity)
        {
            if (entity == null)
            {
                return;
            }
            TEntity old = this.FirstOrDefault(p => p.Equals(entity));
            if (old == entity)
            {
                return;
            }
            if (old == null)
            {
                this.Add(entity);
            }
        }

        /// <summary>
        ///     加入或是同步
        /// </summary>
        public void AddOrSwitch(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                return;
            }
            foreach (TEntity entity in entities)
            {
                AddOrSwitch(entity);
            }
        }

        #endregion

        #region 清除


        /// <summary>
        ///     删除下标
        /// </summary>
        public void RemoveAt(int index)
        {
            this.RemoveAt2(index);
        }

        #endregion

        #region 同步

        /// <summary>
        ///     从一个集合中复制数据
        /// </summary>
        /// <param name="entities"></param>
        /// <remarks>集合中不存在的数据将被删除,原来存在的数据进行覆盖,原来不存在的加入</remarks>
        public virtual void CopyFrom(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                this.Clear();
                return;
            }
            var values = this.Entities.ToList();
            var entityObjectBases = entities as List<TEntity> ?? entities.ToList();
            if (entityObjectBases.Count == 0)
            {
                this.Clear();
                return;
            }
            //双方都有
            foreach (TEntity entity in entityObjectBases.ToArray())
            {
                var olds = this.Entities.Where(p => p == entity).ToArray();
                if (olds.Length == 0)
                {
                    this.Add(entity);
                }
                foreach (var old in olds)
                {
                    this.Add(entity);
                    values.Remove(old);
                }
            }
            //旧有新无
            foreach (TEntity oe in values)
            {
                this.Remove(oe);
            }
        }


#if CLIENT
        /// <summary>
        /// 清除临时数据
        /// </summary>
        public virtual void OnBeginUpdate()
        {
        }
#endif

        /// <summary>
        ///     同步数据
        /// </summary>
        /// <param name="entity"></param>
        public virtual void SyncMessage(TEntity entity)
        {
            IEnumerable<TEntity> olds = this.Entities.Where(p => p == entity);
            bool any = olds.Any();
            if (!any)
            {
                this.Add(entity);
            }
        }

        /// <summary>
        ///     同步数据
        /// </summary>
        /// <param name="entity"></param>
        public virtual void SyncByUpdated(TEntity entity)
        {
            IEnumerable<TEntity> olds = this.Entities.Where(p => p == entity);
            bool any = olds.Any();
            if (!any)
            {
                this.Add(entity);
            }
        }

        #endregion

        #region 接口实现

        /// <summary>
        ///     按索引排序
        /// </summary>
        public void Sort<TKey>(Func<TEntity, TKey> sort)
        {
            var v = this.Entities.OrderBy(sort).ToArray();
            this.SetEmpty();
            foreach (var e in v)
            {
                this.Add(e);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="value"> </param>
        /// <returns> </returns>
        int IList.IndexOf(object value)
        {
            return this.IndexOf2(value as TEntity);
        }

        /// <summary>
        /// </summary>
        /// <param name="index"> </param>
        /// <param name="value"> </param>
        void IList.Insert(int index, object value)
        {
            this.Insert(index, value as TEntity);
        }

        /// <summary>
        ///     得到枚举器
        /// </summary>
        /// <returns> 枚举器 </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Entities.GetEnumerator();
        }

        /// <summary>
        ///     复制到--未实现
        /// </summary>
        /// <param name="array"> 数组 </param>
        /// <param name="arrayIndex"> 位置 </param>
        void ICollection<TEntity>.CopyTo(TEntity[] array, int arrayIndex)
        {
            foreach (TEntity k in this.Entities)
            {
                array[arrayIndex++] = k;
            }
        }

        bool IList.Contains(object value)
        {
            return this.Contains(value as TEntity);
        }

        int IList.Add(object value)
        {
            var entity = value as TEntity;
            if (entity == null)
            {
                return -1;
            }
            this.Add(entity);
            return this.IndexOf(entity);
        }

        bool ICollection<TEntity>.IsReadOnly => false;

        bool IList.IsReadOnly => false;

        bool IList.IsFixedSize => false;

        void IList.Remove(object value)
        {
            this.Remove(value as TEntity);
        }

        TEntity IList<TEntity>.this[int idx]
        {
            get { return this.This2(idx); }
            set { this.Insert(idx, value); }
        }

        object IList.this[int idx]
        {
            get { return this.This2(idx); }
            set { this.Insert(idx, value as TEntity); }
        }

        void ICollection.CopyTo(Array array, int index)
        {
            foreach (TEntity k in this.Entities)
            {
                array.SetValue(k, index++);
            }
        }

        bool ICollection.IsSynchronized => false;

        object ICollection.SyncRoot => 0;

        #endregion

        #region INotifyCollectionChanged

        /// <summary>
        ///     集合有没有修改
        /// </summary>
        public bool CollectionIsModify { get; set; }


        /// <summary>
        ///     集合内容增删
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add
            {
                this.collectionChanged -= value;
                this.collectionChanged += value;
            }
            remove { this.collectionChanged -= value; }
        }

        private event NotifyCollectionChangedEventHandler collectionChanged;
        /// <summary>
        /// 是否在编辑状态
        /// </summary>
        public bool IsEditing
        {
            get; private set;
        }
        /// <summary>
        /// 开始编辑
        /// </summary>
        public void BeginEdit()
        {
            IsEditing = true;
        }
        /// <summary>
        /// 结束编辑
        /// </summary>
        public void EndEdit()
        {
            IsEditing = false;
            RaiseCollectionChangedInner(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
        #endregion
        /// <summary>
        ///     发出集合修改事件
        /// </summary>
        void RaiseCollectionChangedInner(NotifyCollectionChangedEventArgs e)
        {
            this.collectionChanged?.Invoke(this, e);
        }

        /// <summary>
        ///     处理集合修改事件
        /// </summary>
        public void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (!IsEditing)
                InvokeInUiThread(RaiseCollectionChangedInner, e);
        }

        /// <summary>
        ///  对象被删除后续的处理
        /// </summary>
        void OnDelete(TEntity entity)
        {
            if (entity == null)
            {
                return;
            }
            this.CollectionIsModify = true;
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, entity));
        }


        /// <summary>
        /// 新对象加入后续的处理
        /// </summary>
        void OnAdd(TEntity entity)
        {
            if (entity == null)
            {
                return;
            }
            this.CollectionIsModify = true;
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, entity));
        }

        /// <summary>
        ///  对象被清除后续的处理
        /// </summary>
        void OnRemove(TEntity entity)
        {
            if (entity == null)
            {
                return;
            }
            this.CollectionIsModify = true;
            //特殊说明:如果使用理论上正确的NotifyCollectionChangedAction.Remove, entity两个参数来构造对象,在之前进行新增时,DataGrid将死去
            //永不停止地发出数组越界的异常,不知道是为什么,但用NotifyCollectionChangedAction.Reset可以解决这个问题,
            //估计是内部在对删除事件的处理会出现内部可重用的对象无法再找到的问题吧(对象析构过但UI对象都是符合DataGrid要重用之前用过的UI对象的原则)
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        /// <summary>
        /// 集合清空的后续处理
        /// </summary>
        void OnClear()
        {
            this.CollectionIsModify = true;
            //特殊说明:如果使用理论上正确的NotifyCollectionChangedAction.Remove, entity两个参数来构造对象,在之前进行新增时,DataGrid将死去
            //永不停止地发出数组越界的异常,不知道是为什么,但用NotifyCollectionChangedAction.Reset可以解决这个问题,
            //估计是内部在对删除事件的处理会出现内部可重用的对象无法再找到的问题吧(对象析构过但UI对象都是符合DataGrid要重用之前用过的UI对象的原则)
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        /// <summary>
        /// 对象交换的后续处理
        /// </summary>
        void OnSwitch(TEntity entity, TEntity old)
        {
            if (entity == null)
            {
                return;
            }
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, entity,
                old));
        }

        #endregion

        #region 原子锁

        private List<string> _atomLockNames;

        /// <summary>
        ///     正在被锁定,应该用AtomLockNames是否为空来表示，或自行处理
        /// </summary>
        bool IAtomLock.IsAtomLock => this._atomLockNames != null && this._atomLockNames.Count > 0;

        //private readonly Guid _lockId = Guid.NewGuid();

        ///// <summary>
        ///// 唯一标识
        ///// </summary>
        //Guid IAtomLock.Identity
        //{
        //    get
        //    {
        //        return this._lockId;
        //    }
        //}

        /// <summary>
        ///     正在被锁定名字集合
        /// </summary>
        List<string> IAtomLock.AtomLockNames
        {
            get { return this._atomLockNames; }
            set { this._atomLockNames = value; }
        }

        #endregion

        #region 基础数据

        /// <summary>
        ///   内部数据存储
        /// </summary>
        [DataMember]
        private List<TEntity> _entities;

        /// <summary>
        /// 内部列表
        /// </summary>
        public List<TEntity> Entities => (this._entities ?? (this._entities = new List<TEntity>()));

        /// <summary>
        /// 是否为空
        /// </summary>
        public bool IsEmpty => _entities == null || _entities.Count == 0;

        /// <summary>
        /// 是否为空
        /// </summary>
        public void SetEmpty()
        {
            if (!IsEmpty)
                _entities.Clear();
        }

        void RemoveAt2(int idx)
        {
            if (this.IsEmpty || idx >= Count || idx < 0)
                return;
            var en = _entities[idx];
            _entities.RemoveAt(idx);
            OnRemove(en);
        }

        int IndexOf2(TEntity entity)
        {
            if (entity == null)
                return -1;
            return this._entities.IndexOf(entity);
        }

        TEntity This2(int idx)
        {
            if (this.IsEmpty || idx >= Count || idx < 0)
                return null;
            return this.IsEmpty ? null : this._entities[idx];
        }

        /// <summary>
        ///   加入
        /// </summary>
        /// <param name="entity"> </param>
        public void Add(TEntity entity)
        {
            if (entity == null)
            {
                return;
            }
            var list = this._entities ?? (this._entities = new List<TEntity>());
            if (list.Contains(entity))
            {
                return;
            }
            list.Add(entity);
            OnAdd(entity);
        }

        public void Insert(int idx, TEntity entity)
        {
            if (entity == null)
                return;
            var list = this._entities ?? (this._entities = new List<TEntity>());
            if (idx <= 0)
                list.Insert(0, entity);
            else if (idx >= list.Count)
                list.Add(entity);
            else
                list.Insert(idx, entity);
            OnAdd(entity);
        }

        /// <summary>
        ///  从列表中清除
        /// </summary>
        /// <param name="entity"> </param>
        public bool Remove(TEntity entity)
        {
            if (this.IsEmpty)
                return false;
            if (!_entities.Remove(entity))
                return false;
            OnRemove(entity);
            return true;
        }

        /// <summary>
        ///   清除所有数据
        /// </summary>
        public void Clear()
        {
            if (this.IsEmpty)
                return;
            var old = this._entities.ToArray();
            foreach (var entity in old)
                Remove(entity);
            OnClear();
        }

        #endregion
    }
}