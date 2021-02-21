// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.Common.WpfMvvmBase
// 建立:2014-12-09
// 修改:2014-12-09
// *****************************************************/

#region 引用


#endregion

using Agebull.EntityModel.Config;
using System;

namespace Agebull.EntityModel
{
    /// <summary>
    /// 值记录节点
    /// </summary>
    public class ValueRecordItem
    {
        public ValueRecordItem(int type,string name, ModifyRecord record)
        {
            Type = type;
            Record = record;
            Name = name;
        }

        public int Type;

        public ModifyRecord Record { get; set; }

        public string Name;


        private object original;

        /// <summary>
        /// 初始值
        /// </summary>
        public object Original
        {
            get => original; set
            {
                if (original == value)
                    return;
                if (original is IModifyObject o2)
                    o2.IsModifyChanged -= OnIsModifyChanged;

                original = value;
                current = value;
                if (value is IModifyObject o)
                {
                    o.IsModifyChanged += OnIsModifyChanged;
                }
            }
        }

        object current;

        /// <summary>
        /// 初始值
        /// </summary>
        public object Current
        {
            get => original; set
            {
                if (current == value)
                    return;
                if (current is IModifyObject o2)
                    o2.IsModifyChanged -= OnIsModifyChanged;

                if (original is IModifyObject o3)
                    o3.IsModifyChanged -= OnIsModifyChanged;

                current = value;

                if (value is IModifyObject o)
                {
                    o.IsModifyChanged += OnIsModifyChanged;
                }
            }
        }
        public void Check()
        {
            switch (Type)
            {
                case 1:
                    if (original != current)
                        IsModify = true;
                    else if (original == null && current == null)
                        IsModify = false;
                    else if (original == null || current == null)
                        IsModify = true;
                    else if (current is IModifyObject o)
                        IsModify = o.IsModify;
                    else
                        IsModify = false;
                    break;
                case 2:
                    if (Current2.IsMissing() && Original2.IsMissing())
                        IsModify = false;
                    else if (Current2.IsMissing() || Original2.IsMissing())
                        IsModify = true;
                    else if (!Original2.Equals(Current2))
                        IsModify = true;
                    else
                        IsModify = false;
                    break;
                case 3:
                    IsModify = Original3 != Current3;
                    break;
            }
        }
        private void OnIsModifyChanged(object sender, IsModifyEventArgs e)
        {
            if (!Record.valueRecords.ContainsKey(Name))
                Record.valueRecords.Add(Name, this);
            Record.Check();
            if (Record.Me is IChildrenConfig children)
            {
                children.CheckModify();
                if (children.Parent is IModifyObject parent)
                    parent.CheckModify();
            }
        }
        /// <summary>
        /// 初始值
        /// </summary>
        public string Original2 { get; set; }

        /// <summary>
        /// 初始值
        /// </summary>
        public string Current2{ get; set; }

        /// <summary>
        /// 初始值
        /// </summary>
        public bool Original3 { get; set; }

        /// <summary>
        /// 初始值
        /// </summary>
        public bool Current3 { get; set; }

        public bool IsModify { get; set; }
    }
}
