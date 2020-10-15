using System;
using System.Collections;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// �ַ������弯�ϣ�һ��Ŀ������ԣ�ֻ��һ��
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
                throw new Exception ("����������½�");
            }
        }
        public void Clear()
        {
            list.Clear();
        }
        public int Count => list.Count;
    }
}