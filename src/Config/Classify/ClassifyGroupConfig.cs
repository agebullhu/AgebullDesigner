using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Agebull.Common.DataModel;
using Newtonsoft.Json;

namespace Gboxt.Common.DataAccess.Schemas
{
    /// <summary>
    /// ��������
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class ClassifyGroupConfig<TConfig> : ConfigBase
        where TConfig : ClassifyConfig, new()
    {
        /// <summary>
        /// �����ļ���
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        private readonly ObservableCollection<TConfig> _collection;
        /// <summary>
        /// ���·���ķ���
        /// </summary>
        private readonly Action<string, TConfig> _updateAction;
        /// <summary>
        /// �����LAMBDA���ʽ
        /// </summary>
        private readonly Func<TConfig, string> _groupFunc;
        /// <summary>
        /// �������������
        /// </summary>
        private readonly string _propertyName;

        /// <summary>
        /// �ڵ�
        /// </summary>
        public ConfigCollection<ClassifyItem<TConfig>> Classifies { get; set; } 

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="group"></param>
        /// <param name="updateAction"></param>
        /// <param name="classify"></param>
        public ClassifyGroupConfig(ObservableCollection<TConfig> collection, Expression<Func<TConfig, string>> group, 
            Action<string, TConfig> updateAction, ConfigCollection<ClassifyItem<TConfig>> classify=null)
        {
            _collection = collection;
            _collection.CollectionChanged += CollectionChanged;
            _groupFunc = group.Compile();
            _propertyName = ((MemberExpression)group.Body).Member.Name;
            _updateAction = updateAction;
            Classifies = classify ?? new ConfigCollection<ClassifyItem<TConfig>>();
            ReGroup();
        }
        /// <summary>
        /// ���·���
        /// </summary>
        private void DoReGroup()
        {
            Thread.Sleep(1000);
            needReGroup--;
            if (needReGroup > 0)
                return;
            ReGroup();
        }

        /// <summary>
        /// ���·���
        /// </summary>
        private void ReGroup()
        {
            var items = new Dictionary<string, ClassifyItem<TConfig>>();
            var array = Classifies.ToArray();
            foreach (var item in array)
            {
                if (string.IsNullOrWhiteSpace(item.Name))
                    item.Name = item.Caption;
                if (!items.ContainsKey(item.Name))
                    items.Add(item.Name, item);
                else
                    Classifies.Remove(item);
            }
            foreach (var g in _collection.GroupBy(_groupFunc).ToArray())
            {
                string nmae = g.Key ?? "δ����";
                if (!items.ContainsKey(nmae))
                {
                    var group = new ClassifyItem<TConfig>(_updateAction)
                    {
                        Name = nmae,
                        Caption = nmae,
                        Items = new ConfigCollection<TConfig>()
                    };
                    foreach (var item in g)
                    {
                        item.PropertyChanged -= Item_PropertyChanged;
                        item.PropertyChanged += Item_PropertyChanged;
                        group.Items.Add(item);
                    }
                    Classifies.Add(group);
                }
                else
                {
                    var group = items[nmae];
                    var old = group.Items.ToList();
                    foreach (var item in g)
                    {
                        if (!old.Contains(item))
                        {
                            item.PropertyChanged -= Item_PropertyChanged;
                            item.PropertyChanged += Item_PropertyChanged;
                            group.Items.Add(item);
                        }
                        else
                        {
                            old.Remove(item);
                        }
                    }
                    foreach (var item in old)
                    {
                        group.Items.Remove(item);
                    }
                }
            }
        }


        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            needReGroup++;
            Task.Factory.StartNew(DoReGroup);
        }

        private int needReGroup;
        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != _propertyName)
                return;
            needReGroup++;
            Task.Factory.StartNew(DoReGroup);
        }

    }
}