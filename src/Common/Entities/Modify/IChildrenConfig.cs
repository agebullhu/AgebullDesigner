// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.Common.WpfMvvmBase
// 建立:2014-12-09
// 修改:2014-12-09
// *****************************************************/

#region 引用


#endregion


namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 属性配置
    /// </summary>
    public interface IChildrenConfig : ISimpleConfig
    {
        /// <summary>
        /// 上级
        /// </summary>
        ISimpleConfig Parent { get; set; }

        /// <summary>
        /// 载入后的同步
        /// </summary>
        /// <returns></returns>
        void OnLoad(string property, ISimpleConfig parent)
        {
            Parent = parent;
            parent.ValueRecords.Add(property, this);
        }
    }
}
