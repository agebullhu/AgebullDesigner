// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Gboxt.Common.WpfMvvmBase
// 建立:2014-11-24
// 修改:2014-11-29
// *****************************************************/

#region 引用


#endregion

namespace Agebull.EntityModel
{
    /// <summary>
    /// 树节点标题视角
    /// </summary>
    public enum HeaderVisiable
    {
        None = 0,
        Caption = 0x1,
        Name = 0x2,
        Abbreviation = 0x4,
        State = 0x8,
        General = Caption | Name
    }

}
