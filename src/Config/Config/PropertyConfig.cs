// // /*****************************************************
// // (c)2016-2016 Copy right Agebull.hu
// // 作者:
// // 工程:CodeRefactor
// // 建立:2016-06-06
// // 修改:2016-06-22
// // *****************************************************/

#region 引用

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Gboxt.Common.DataAccess.Schemas
{
    partial class PropertyConfig
    {
        #region 运行时使用
        
        /// <summary>
        ///     取别名属性
        /// </summary>
        /// <returns></returns>
        public List<string> GetAliasPropertys()
        {
            if (string.IsNullOrWhiteSpace(Alias))
                return new List<string>();
            var alias =
                Alias.Split(new[] { ' ', ',', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Distinct();
            return
                alias.Where(p => !string.IsNullOrWhiteSpace(p) && p != PropertyName).Select(p => p.Trim()).ToList();
        }
        
        #endregion
        
    }
}