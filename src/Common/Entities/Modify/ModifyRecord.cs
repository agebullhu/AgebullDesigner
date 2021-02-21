// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.Common.WpfMvvmBase
// 建立:2014-12-09
// 修改:2014-12-09
// *****************************************************/

#region 引用


#endregion

using System;
using System.Collections.Generic;
using System.Linq;

namespace Agebull.EntityModel
{
    public class ModifyRecord
    {
        public readonly Dictionary<string, ValueRecordItem> valueRecords = new Dictionary<string, ValueRecordItem>();
        private bool isModify;



        public int Modified { get; set; }

        public object Me { get; set; }

        public bool IsNew { get; set; }

        public bool IsModify
        {
            get => isModify;
            set => isModify = value;
        }

        public void SetIsNew()
        {
            IsModify = true;
            IsNew = true;
            Modified = 0;
        }
        public void Check()
        {
            if (IsNew)
            {
                IsModify = true;
                return;
            }
            foreach (var item in valueRecords.Values)
                item.Check();
            Modified = valueRecords.Values.Count(p => p.IsModify);
            IsModify = IsNew || Modified > 0;
        }

        public void Reset(bool isSaved)
        {
            foreach (var key in valueRecords.Keys)
            {
                if (valueRecords[key].Type == 1)
                {
                    valueRecords[key].Original = valueRecords[key].Current;
                }
                else if (valueRecords[key].Type > 1)
                    valueRecords.Remove(key);
            }
            if (IsNew && isSaved)
                IsNew = false;
            Check();
        }

        public void Add(string propertyName, bool isModify)
        {
            if (valueRecords.TryGetValue(propertyName, out var item))
            {
                item.IsModify = isModify;
            }
            else
            {
                valueRecords.Add(propertyName, item = new ValueRecordItem(0, propertyName, this)
                {
                    IsModify = isModify
                });
            }
            Check();
        }

        public void Add(string propertyName, IModifyObject value)
        {
            if (valueRecords.TryGetValue(propertyName, out var item))
            {
                item.Original = value;
            }
            else
            {
                valueRecords.Add(propertyName, item = new ValueRecordItem(1, propertyName, this)
                {
                    Original = value
                });
            }
            Check();
        }
        public void Record(string propertyName, object oldValue, object newValue)
        {
            if (valueRecords.TryGetValue(propertyName, out var item))
            {
                item.Current = newValue;
            }
            else
            {
                valueRecords.Add(propertyName, item = new ValueRecordItem(1, propertyName, this)
                {
                    Original = oldValue,
                    Current = newValue
                });
            }
            Check();
        }


        public void Record(string propertyName, string oldValue, string newValue)
        {
            if (valueRecords.TryGetValue(propertyName, out var item))
            {
                item.Current2 = newValue;
            }
            else
            {
                valueRecords.Add(propertyName, item = new ValueRecordItem(2, propertyName, this)
                {
                    Original2 = oldValue,
                    Current2 = newValue
                });
            }
            Check();
        }

        public void Record(string propertyName, bool oldValue, bool newValue)
        {
            if (valueRecords.TryGetValue(propertyName, out var item))
            {
                item.Current3 = newValue;
            }
            else
            {
                valueRecords.Add(propertyName, new ValueRecordItem(3, propertyName, this)
                {
                    Original3 = oldValue,
                    Current3 = newValue
                });
            }
            Check();
        }
    }
}
