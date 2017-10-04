// // /*****************************************************
// // (c)2016-2016 Copy right Agebull.hu
// // ����:
// // ����:CodeRefactor
// // ����:2016-06-06
// // �޸�:2016-06-22
// // *****************************************************/

#region ����

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Gboxt.Common.DataAccess.Schemas
{
    partial class PropertyConfig
    {
        #region ����ʱʹ��
        
        /// <summary>
        ///     ȡ��������
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