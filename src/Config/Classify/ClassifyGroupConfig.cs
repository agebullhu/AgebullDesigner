/*using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// ��������
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public class ClassifyGroupConfig : ConfigBase
    {
        /// <summary>
        /// �����ļ���
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        private readonly NotificationList<EntityConfig> _collection;
        /// <summary>
        /// ���·���ķ���
        /// </summary>
        private readonly Action<string, EntityConfig> _updateAction;
        /// <summary>
        /// �����LAMBDA���ʽ
        /// </summary>
        private readonly Func<EntityConfig, string> _groupFunc;
        /// <summary>
        /// �������������
        /// </summary>
        private readonly string _propertyName;

        /// <summary>
        /// �ڵ�
        /// </summary>
        public ConfigCollection<EntityClassify> Classifies { get; set; } 

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="group"></param>
        /// <param name="updateAction"></param>
        /// <param name="classify"></param>
        public ClassifyGroupConfig(NotificationList<EntityConfig> collection, Expression<Func<EntityConfig, string>> group, 
            Action<string, EntityConfig> updateAction, ConfigCollection<EntityClassify> classify=null)
        {
            _collection = collection;
            _collection.CollectionChanged += CollectionChanged;
            _groupFunc = group.Compile();
            _propertyName = ((MemberExpression)group.Body).Member.Name;
            _updateAction = updateAction;
            Classifies = classify ?? new ConfigCollection<EntityClassify>();
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
            var items = new Dictionary<string, EntityClassify>();
            var array = Classifies.ToArray();
            foreach (var item in array)
            {
                if (string.IsNullOrWhiteSpace(item.Name))
                    item.Name = item.Caption;
                if (!string.IsNullOrWhiteSpace(item.Name) && !items.ContainsKey(item.Name))
                    items.Add(item.Name, item);
                else
                    Classifies.Remove(item);
            }
            foreach (var g in _collection.GroupBy(_groupFunc).ToArray())
            {
                string nmae = g.Key ?? "δ����";
                if (!items.ContainsKey(nmae))
                {
                    var group = new EntityClassify(_updateAction)
                    {
                        Name = nmae,
                        Caption = nmae,
                        Items = new ConfigCollection<EntityConfig>()
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
            if (e.Name != _propertyName)
                return;
            needReGroup++;
            Task.Factory.StartNew(DoReGroup);
        }

    }
}*/