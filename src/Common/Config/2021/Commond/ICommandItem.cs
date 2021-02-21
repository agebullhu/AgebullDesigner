/*design by:agebull designer date:2017/7/12 22:06:40*/
/*****************************************************
©2008-2017 Copy right by agebull.hu(胡天水)
作者:agebull.hu(胡天水)
工程:Agebull.Common.Config
建立:2014-12-03
修改:2017-07-12
*****************************************************/

namespace Agebull.EntityModel.Config.V2021
{
    public interface ICommandItem
    {
        string Api { get; set; }
        string Button { get; set; }
        string Icon { get; set; }
        bool IsLocalAction { get; set; }
        bool IsMulitOperator { get; set; }
        bool IsSingleObject { get; set; }
        string JsMethod { get; set; }
        string ServiceCommand { get; set; }
    }
}