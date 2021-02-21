// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// 建立:2014-11-20
// 修改:2014-11-27
// *****************************************************/

#region 引用

using Agebull.EntityModel.Config;
using System.ComponentModel.Composition;

#endregion

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 命令注册器
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public sealed class SolutionCommandRegister : IAutoRegister
    {
        /// <summary>
        /// 注册代码
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            WordMapModel.Reload();
            EditorManager.Registe2<SolutionConfig, DataTypePanel>("类型映射", "类型");
            EditorManager.Registe2<SolutionConfig, WordMapPanel>("数据字典", "类型");
        }
    }
}