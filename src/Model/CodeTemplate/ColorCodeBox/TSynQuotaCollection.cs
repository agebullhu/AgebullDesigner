using System;
using System.Collections;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 字符串定义集合，一般的开发语言，只有一个
    /// </summary>
    public class TSynQuotaCollection
    {
        public TSynQuotaCollection()
        {

            list = new ArrayList();
  
        }
        private readonly ArrayList list;
        public void Add(ColorBlock quota)
        {
            list.Add(quota);
        }
        public ColorBlock this[int index]
        {
            get
            {
                if (index <= list.Count)
                {
                    return (ColorBlock)list[index];
                }
                throw new Exception ("超出数组的下届");
            }
        }
        public void Clear()
        {
            list.Clear();
        }
        public int Count => list.Count;
    }
}