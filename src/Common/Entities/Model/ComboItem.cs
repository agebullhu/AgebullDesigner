
namespace Agebull.EntityModel
{
    /// <summary>
    /// 下拉列表节点
    /// </summary>
    public class ComboItem : ComboItem<string>
    {
        /// <summary>
        /// 构造
        /// </summary>
        public ComboItem()
        {

        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="n"></param>
        /// <param name="v"></param>
        public ComboItem(string n, string v)
            : base(n, v)
        {
        }
    }

    /// <summary>
    /// 下拉列表节点
    /// </summary>
    public class ComboItem<TValue>
    {
        /// <summary>
        /// 构造
        /// </summary>
        public ComboItem()
        {

        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="n"></param>
        /// <param name="v"></param>
        public ComboItem(string n, TValue v)
        {
            name = n;
            value = v;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public TValue value { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name => name;

        /// <summary>
        /// 值
        /// </summary>
        public TValue Value => value;
    }
}